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
    /// Interaction logic for A00024.xaml
    /// </summary>
    public partial class A00024 : Window
    {
        private const string _reportName = "A00024";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan _clsBaoCaoTinhHinhHuyDongVon;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public A00024()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
            _clsBaoCaoTinhHinhHuyDongVon = new clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan();
        }

        private IEnumerable<clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan> GetDataPrevMonth(string maChiNhanh, string ngayDuLieu)
        {
            return _clsBaoCaoTinhHinhHuyDongVon.GetAllData(maChiNhanh, ngayDuLieu);
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
                string maChiNhanh = cbxDviPsinhDLieu.SelectedValue.ToString();

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
                string path = $"{System.AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx";
                //var fs = File.OpenRead($"{System.AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
                var newFile = new FileInfo(saveFileDialog.FileName);
                var fileTemplate = new FileInfo(path);
                var exPackage = new ExcelPackage(newFile, fileTemplate);
                var excelSheet = exPackage.Workbook.Worksheets[1];
                string _ngayBaoCao = _createFileNameReport.NgayBaoCao(dateNgayPSinhDLieu);
                _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                   clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);
                var clsTinhHinhHuyDongTienGui = GetDataPrevMonth(cbxDviPsinhDLieu.SelectedValue.ToString(), date.ToString("yyyyMMdd"));
                var tienGuiTuNguyen = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T02" select a.SO_TIEN).Sum();
                var tienGuiQuyDinh = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T01" select a.SO_TIEN).Sum();
                var tienGuiDuoi6Thang = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN < 6 select a.SO_TIEN).Sum();
                var tienGui6Den12Thang = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN >= 6 && a.KY_HAN <= 12 select a.SO_TIEN).Sum();
                var tienGui12Den24Thang = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN > 12 && a.KY_HAN <= 24 select a.SO_TIEN).Sum();
                var tienGui24Den60Thang = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN > 24 && a.KY_HAN <= 60 select a.SO_TIEN).Sum();
                var tienGuiTu60Thang = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN > 60 select a.SO_TIEN).Sum();
                var tienGuiTietKiem = tienGuiTuNguyen + tienGuiQuyDinh + tienGuiDuoi6Thang + tienGui6Den12Thang + tienGui12Den24Thang + tienGui24Den60Thang + tienGuiTu60Thang;
                var tienGuiKhachHang = tienGuiTietKiem;
                excelSheet.Cells["F21"].Value = excelSheet.Cells["J21"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tienGuiKhachHang / dviTinh, 2));
                excelSheet.Cells["F26"].Value = excelSheet.Cells["J26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tienGuiTietKiem / dviTinh, 2));
                excelSheet.Cells["F27"].Value = excelSheet.Cells["J27"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((tienGuiTuNguyen + tienGuiDuoi6Thang) / dviTinh, 2));
                excelSheet.Cells["F28"].Value = excelSheet.Cells["J28"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tienGui6Den12Thang / dviTinh, 2));
                excelSheet.Cells["F29"].Value = excelSheet.Cells["J29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tienGui12Den24Thang / dviTinh, 2));
                excelSheet.Cells["F30"].Value = excelSheet.Cells["J30"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tienGui24Den60Thang / dviTinh, 2));
                excelSheet.Cells["F31"].Value = excelSheet.Cells["J31"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((tienGuiTu60Thang + tienGuiQuyDinh) / dviTinh, 2));
                excelSheet.Cells["F56"].Value = excelSheet.Cells["J56"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", Math.Round((tienGuiKhachHang) / dviTinh, 2));
                exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                SplashScreenManager.CloseDefaultWaitForm();
            }

            if (EasyDialog.ShowYesNoDialog("Tạo báo cáo thành công. Bạn có muốn tiếp tục không ?") != System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _clsWPFDanhMucChiNhanh = new clsWPF(cbxDviPsinhDLieu);
            _clsWPFDanhMucChiNhanh.GetDanhMucChiNhanh();
            cbxDviPsinhDLieu.SelectedIndex = 0;
            dateNgayPSinhDLieu.SelectedDate = DateTime.Now;
        }
    }
}