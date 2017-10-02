using DevExpress.Xpf.Core;
using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWpfApplication.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace ReportWpfApplication
{
    /// <summary>
    /// Interaction logic for G01254.xaml
    /// </summary>
    public partial class G01254 : DXWindow, IDisposable
    {
        private const string _reportName = "G01254";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan _clsBaoCaoTinhHinhHuyDongVon;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public void Dispose()
        {
            if (_clsBaoCaoTinhHinhHuyDongVon != null)
                _clsBaoCaoTinhHinhHuyDongVon.Dispose();
        }

        public G01254()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
            _clsBaoCaoTinhHinhHuyDongVon = new clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan();
        }

        private IEnumerable<clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan> GetDataPrevMonth(string maChiNhanh, string ngayDuLieu)
        {
            return _clsBaoCaoTinhHinhHuyDongVon.GetAllData(maChiNhanh, ngayDuLieu);
        }

        private void DXWindow_LoadedExtracted()
        {
            _clsWPFDanhMucChiNhanh = new clsWPF(cbxDviPsinhDLieu);
            _clsWPFDanhMucChiNhanh.GetDanhMucChiNhanh();
            dateNgayPSinhDLieu.SelectedDate = DateTime.Now;
        }

        private void DXWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DXWindow_LoadedExtracted();
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            EasyDialog.ShowInfoDialog("Chức năng này đã được sửa đổi. Hãy chọn chức năng Tạo báo cáo");
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            var date = (DateTime)(dateNgayPSinhDLieu.SelectedDate);
            clsSoLieuToanHeThongVaDonVi _clsSoLieuToanHeThongVaDonVi = new clsSoLieuToanHeThongVaDonVi();
            var _phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            foreach (var item in _phamViBaoCao)
            {
                cbxDviPsinhDLieu.SelectedValue = item.MA_DVI;
                string ngayBaoCao;
                string maDviPsinhDlieu;
                string fileName;
                _createFileNameReport = new clsGetFileNameReports(iDinhKyBaoCao);
                _createFileNameReport.GetFileNameReportMonthWpf(out ngayBaoCao, out maDviPsinhDlieu, out fileName,
                   cbxDviPsinhDLieu, _maDonViGui, _reportName, dateNgayPSinhDLieu);
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = @"Excel File (*.xlsx)|*.xlsx",
                    FilterIndex = 1,
                    FileName = fileName
                };
                if (saveFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
                SplashScreenManager.ShowDefaultWaitForm();
                var newFile = new FileInfo(saveFileDialog.FileName);
                var fileTemplate = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
                var exPackage = new ExcelPackage(newFile, fileTemplate);
                var excelSheet = exPackage.Workbook.Worksheets[1];
                string _ngayBaoCao = _createFileNameReport.NgayBaoCao(dateNgayPSinhDLieu);
                _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                   clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);
                string maChiNhanh = cbxDviPsinhDLieu.SelectedValue.ToString();
                string ngayDuLieu = ((DateTime)dateNgayPSinhDLieu.SelectedDate).ToString("yyyyMMdd");
                string ngayDauThang = clsGetLastDayOfMonth.GetFirstDayOfMont(((DateTime)dateNgayPSinhDLieu.SelectedDate).Month, ((DateTime)dateNgayPSinhDLieu.SelectedDate).Year);
                var clsTinhHinhHuyDongTienGui = GetDataPrevMonth(cbxDviPsinhDLieu.SelectedValue.ToString(), date.ToString("yyyyMMdd"));
                clsG01254LaiSuat _laiSuatList = new clsG01254LaiSuat();
                var laiSuatTienGui = _laiSuatList.GetAll(ngayDuLieu);
                var _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
                var bangCanDoi = _clsBangCanDoiTaiKhoanKeToan.GetAllData(maChiNhanh,ngayDuLieu);
                var noTK801 = (from a in bangCanDoi where a.F03 == "801" select a.F06).FirstOrDefault();
                var coTK801 = (from a in bangCanDoi where a.F03 == "801" select a.F07).FirstOrDefault();
                var coDauKyTK423 = (from a in bangCanDoi where a.F03 == "423" select a.F05).FirstOrDefault();
                var coCuoiKyTK423 = (from a in bangCanDoi where a.F03 == "423" select a.F09).FirstOrDefault();
                var tong423 = coDauKyTK423 + coCuoiKyTK423;
                decimal laiSuatBinhQuan = 0;
                if (tong423 > 0)
                {
                    laiSuatBinhQuan = Math.Round(((noTK801 - coTK801) / (tong423 / 2)) * 100 * 12, 2);
                }
                var laiSuatTKQD = laiSuatTienGui.Where(p => p.MA_LAI_SUAT == "TKQD" && p.KY_HAN == null).FirstOrDefault();
                var laiSuatTKTN = laiSuatTienGui.Where(p => p.MA_LAI_SUAT == "TKTN" && p.KY_HAN == null).FirstOrDefault();
                var laiSuatDuoi1Thang = laiSuatTienGui.Where(p => p.MA_LAI_SUAT == "DUOI1THANG" && p.KY_HAN == 1).FirstOrDefault();
                var laiSuat1Thang = laiSuatTienGui.Where(p => p.KY_HAN == 1 && p.MA_LAI_SUAT == "TK1THANG").FirstOrDefault();
                var laiSuat2Thang = laiSuatTienGui.Where(p => p.KY_HAN == 2).FirstOrDefault();
                var laiSuat3Thang = laiSuatTienGui.Where(p => p.KY_HAN == 3).FirstOrDefault();
                var laiSuat4Thang = laiSuatTienGui.Where(p => p.KY_HAN == 4).FirstOrDefault();
                var laiSuat5Thang = laiSuatTienGui.Where(p => p.KY_HAN == 5).FirstOrDefault();
                var laiSuat6Thang = laiSuatTienGui.Where(p => p.KY_HAN == 6).FirstOrDefault();
                var laiSuat7Thang = laiSuatTienGui.Where(p => p.KY_HAN == 7).FirstOrDefault();
                var laiSuat8Thang = laiSuatTienGui.Where(p => p.KY_HAN == 8).FirstOrDefault();
                var laiSuat9Thang = laiSuatTienGui.Where(p => p.KY_HAN == 9).FirstOrDefault();
                var laiSuat12Thang = laiSuatTienGui.Where(p => p.KY_HAN == 12).FirstOrDefault();
                var laiSuat15Thang = laiSuatTienGui.Where(p => p.KY_HAN == 15).FirstOrDefault();
                var laiSuat18Thang = laiSuatTienGui.Where(p => p.KY_HAN == 18).FirstOrDefault();
                var laiSuat24Thang = laiSuatTienGui.Where(p => p.KY_HAN == 24).FirstOrDefault();
                var laiSuat36Thang = laiSuatTienGui.Where(p => p.KY_HAN == 36).FirstOrDefault();
                var duTienGuiKhongKyHanTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T02" select a.SO_TIEN).Sum();
                var duTienGuiTu60ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T01" select a.SO_TIEN).Sum();
                var duTienGuiDuoi1ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN < 1 select a.SO_TIEN).Sum();
                var duTienGuiTu1Den3ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 1 && a.KY_HAN < 3 select a.SO_TIEN).Sum();
                var duTienGuiTu3Den6ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 3 && a.KY_HAN < 6 select a.SO_TIEN).Sum();
                var duTienGuiTu6Den9ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 6 && a.KY_HAN < 9 select a.SO_TIEN).Sum();
                var duTienGuiTu9Den12ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 9 && a.KY_HAN < 12 select a.SO_TIEN).Sum();
                var duTienGuiTu12Den24ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 12 && a.KY_HAN < 24 select a.SO_TIEN).Sum();
                var duTienGuiTu24Den60ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 24 && a.KY_HAN < 60 select a.SO_TIEN).Sum();
                var tongTienGuiTVien = (duTienGuiKhongKyHanTVien + duTienGuiDuoi1ThangTVien + duTienGuiTu1Den3ThangTVien +
                    duTienGuiTu3Den6ThangTVien + duTienGuiTu6Den9ThangTVien + duTienGuiTu9Den12ThangTVien +
                    duTienGuiTu12Den24ThangTVien + duTienGuiTu24Den60ThangTVien + duTienGuiTu60ThangTVien);
                var duTienGuiKhongKyHanCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T02" select a.SO_TIEN).Sum();
                var duTienGuiTu60ThangCNhan1 = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T01" select a.SO_TIEN).Sum();
                var duTienGuiTu60ThangCNhan2 = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 60 select a.SO_TIEN).Sum();
                var duTienGuiDuoi1ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN < 1 select a.SO_TIEN).Sum();
                var duTienGuiTu1Den3ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 1 && a.KY_HAN < 3 select a.SO_TIEN).Sum();
                var duTienGuiTu3Den6ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 3 && a.KY_HAN < 6 select a.SO_TIEN).Sum();
                var duTienGuiTu6Den9ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 6 && a.KY_HAN < 9 select a.SO_TIEN).Sum();
                var duTienGuiTu9Den12ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 9 && a.KY_HAN < 12 select a.SO_TIEN).Sum();
                var duTienGuiTu12Den24ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 12 && a.KY_HAN < 24 select a.SO_TIEN).Sum();
                var duTienGuiTu24Den60ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 24 && a.KY_HAN < 60 select a.SO_TIEN).Sum();

                var tongTienGuiCNhan = (duTienGuiKhongKyHanCNhan + duTienGuiTu60ThangCNhan1 + duTienGuiTu60ThangCNhan2 + duTienGuiDuoi1ThangCNhan
                    + duTienGuiTu1Den3ThangCNhan + duTienGuiTu3Den6ThangCNhan + duTienGuiTu6Den9ThangCNhan + duTienGuiTu9Den12ThangCNhan +
                    duTienGuiTu12Den24ThangCNhan + duTienGuiTu24Den60ThangCNhan);
                excelSheet.Cells["D18"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(tongTienGuiCNhan / dviTinh, 1));
                excelSheet.Cells["D19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiKhongKyHanCNhan / dviTinh, 1));
                excelSheet.Cells["D20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiDuoi1ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D21"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu1Den3ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D22"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu3Den6ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D23"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu6Den9ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D24"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu9Den12ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu12Den24ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu24Den60ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D27"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round((duTienGuiTu60ThangCNhan1 + duTienGuiTu60ThangCNhan2) / dviTinh, 1));
                excelSheet.Cells["D28"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round((tongTienGuiTVien) / dviTinh, 1));
                excelSheet.Cells["D29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round((duTienGuiKhongKyHanTVien) / dviTinh, 1));
                excelSheet.Cells["D30"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiDuoi1ThangTVien / dviTinh, 1));
                excelSheet.Cells["D31"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu1Den3ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D32"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu3Den6ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu6Den9ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu9Den12ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu12Den24ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu24Den60ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D37"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu60ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D58"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                         "{0:00.0}", Math.Round((tongTienGuiTVien + tongTienGuiCNhan) / dviTinh, 1));
                excelSheet.Cells["E18"].Value = excelSheet.Cells["E28"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", laiSuatBinhQuan);
                excelSheet.Cells["E19"].Value = excelSheet.Cells["E29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", laiSuatTKTN.LAI_SUAT);
                excelSheet.Cells["E20"].Value = excelSheet.Cells["E30"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                       "{0:00.00}", laiSuatDuoi1Thang.LAI_SUAT);
                excelSheet.Cells["E21"].Value = excelSheet.Cells["E31"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                      "{0:00.00}", laiSuat3Thang.LAI_SUAT);
                excelSheet.Cells["E22"].Value = excelSheet.Cells["E32"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                      "{0:00.00}", laiSuat5Thang.LAI_SUAT);
                excelSheet.Cells["E23"].Value = excelSheet.Cells["E33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                      "{0:00.00}", laiSuat9Thang.LAI_SUAT);
                excelSheet.Cells["E24"].Value = excelSheet.Cells["E34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                      "{0:00.00}", laiSuat12Thang.LAI_SUAT);
                excelSheet.Cells["E25"].Value = excelSheet.Cells["E35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                  "{0:00.00}", laiSuat24Thang.LAI_SUAT);
                excelSheet.Cells["E26"].Value = excelSheet.Cells["E36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuat36Thang.LAI_SUAT);
                excelSheet.Cells["E27"].Value = excelSheet.Cells["E37"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatTKQD.LAI_SUAT);
                exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                SplashScreenManager.CloseDefaultWaitForm();
            }

            if (EasyDialog.ShowYesNoDialog("Tạo báo cáo thành công. Bạn có muốn tiếp tục không ?") != System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnLaiSuat_Click(object sender, RoutedEventArgs e)
        {
            G01254LaiSuat laiSuat = new G01254LaiSuat() { NgayDL = ((DateTime)(dateNgayPSinhDLieu.SelectedDate)).ToString("yyyyMMdd") };
            laiSuat.ShowDialog();
        }
    }
}