using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWpfApplication.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Linq;
using System.Globalization;

namespace ReportWpfApplication
{
    /// <summary>
    /// Interaction logic for G01274.xaml
    /// </summary>
    public partial class G01274 : Window, IDisposable
    {
        private const string _reportName = "G01274";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan;
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public void Dispose()
        {
            if (_clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan != null)
            {
                _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan.Dispose();
                _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan = null;
            }
        }

        public G01274()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
            _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan = new clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan();
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string maChiNhanh = cbxDviPsinhDLieu.SelectedValue.ToString();
                string ngayDuLieu = ((DateTime)dateNgayPSinhDLieu.SelectedDate).ToString("yyyyMMdd");
                using (_clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan = new clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan())
                    grdReport.ItemsSource = _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan.GetAllData(maChiNhanh, ngayDuLieu);
            }
            catch (Exception ex)
            {
                EasyDialog.ShowErrorDialog(ex.Message);
            }
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            string ngayDuLieu = ((DateTime)dateNgayPSinhDLieu.SelectedDate).ToString("yyyyMMdd");
            clsSoLieuToanHeThongVaDonVi _clsSoLieuToanHeThongVaDonVi = new clsSoLieuToanHeThongVaDonVi();
            var _phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            foreach (var phamvi in _phamViBaoCao)
            {
                cbxDviPsinhDLieu.SelectedValue = phamvi.MA_DVI;
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
                var newFile = new FileInfo(saveFileDialog.FileName);
                var fileTemplate = new FileInfo($"{System.AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
                var exPackage = new ExcelPackage(newFile, fileTemplate);
                var excelSheet = exPackage.Workbook.Worksheets[1];
                string _ngayBaoCao = _createFileNameReport.NgayBaoCao(dateNgayPSinhDLieu);
                _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                   clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);
                var clsVayNgoai = _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan.GetAllData(maChiNhanh, ngayDuLieu);
                var vayTCTD = clsVayNgoai.Where(p => p.LOAI_NGUON == "4424").OrderBy(p => p.NGAY_VAY);
                int startRow = 21;
                int sTT = 1;
                excelSheet.InsertRow(startRow, vayTCTD.Count(), startRow - 1);
                foreach (var item in vayTCTD)
                {
                    excelSheet.SetValue(startRow, 2, "I." + sTT);
                    excelSheet.SetValue(startRow, 3, item.TEN_NGUON_VON);
                    excelSheet.SetValue(startRow, 4, FormatString(item.SO_TIEN_VAY));
                    excelSheet.SetValue(startRow, 5, FormatString(item.DU_NO));
                    if (item.KY_HAN <= 12)
                    {
                        excelSheet.SetValue(startRow, 6, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow, 7, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow, 8, item.NGAY_VAY);
                        excelSheet.SetValue(startRow, 9, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow, 10, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow, 11, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    else
                    {
                        excelSheet.SetValue(startRow, 12, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow, 13, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow, 14, item.NGAY_VAY);
                        excelSheet.SetValue(startRow, 15, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow, 16, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow, 17, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    startRow++;
                    sTT++;
                }
                int sTT1 = 1;
                int startRow1 = startRow + 1;
                var vayTCKHAC = clsVayNgoai.Where(p => p.LOAI_NGUON == "4425").OrderBy(p => p.NGAY_VAY);
                excelSheet.InsertRow(startRow1, vayTCKHAC.Count(), startRow - 1);
                foreach (var item in vayTCKHAC)
                {
                    excelSheet.SetValue(startRow1, 2, "II." + sTT1);
                    excelSheet.SetValue(startRow1, 3, item.TEN_NGUON_VON);
                    excelSheet.SetValue(startRow1, 4, FormatString(item.SO_TIEN_VAY));
                    excelSheet.SetValue(startRow1, 5, FormatString(item.DU_NO));
                    if (item.KY_HAN <= 12)
                    {
                        excelSheet.SetValue(startRow1, 6, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow1, 7, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow1, 8, item.NGAY_VAY);
                        excelSheet.SetValue(startRow1, 9, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow1, 10, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow1, 11, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    else
                    {
                        excelSheet.SetValue(startRow1, 12, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow1, 13, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow1, 14, item.NGAY_VAY);
                        excelSheet.SetValue(startRow1, 15, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow1, 16, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow1, 17, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    startRow1++;
                    sTT1++;
                }
                int sTT2 = 1;
                int startRow2 = startRow1 + 1;
                var vayCaNhan = clsVayNgoai.Where(p => p.LOAI_NGUON == "4426").OrderBy(p => p.NGAY_VAY);
                excelSheet.InsertRow(startRow2, vayCaNhan.Count(), startRow - 1);
                foreach (var item in vayCaNhan)
                {
                    excelSheet.SetValue(startRow2, 2, "III." + sTT2);
                    excelSheet.SetValue(startRow2, 3, item.TEN_NGUON_VON);
                    excelSheet.SetValue(startRow2, 4, FormatString(item.SO_TIEN_VAY));
                    excelSheet.SetValue(startRow2, 5, FormatString(item.DU_NO));
                    if (item.KY_HAN <= 12)
                    {
                        excelSheet.SetValue(startRow2, 6, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow2, 7, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow2, 8, item.NGAY_VAY);
                        excelSheet.SetValue(startRow2, 9, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow2, 10, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow2, 11, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    else
                    {
                        excelSheet.SetValue(startRow2, 12, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow2, 13, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow2, 14, item.NGAY_VAY);
                        excelSheet.SetValue(startRow2, 15, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow2, 16, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow2, 17, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    startRow2++;
                    sTT2++;
                }
                int startRow3 = startRow2;
                excelSheet.SetValue(startRow3, 4, FormatString(clsVayNgoai.Sum(p => p.SO_TIEN_VAY)));
                excelSheet.SetValue(startRow3, 5, FormatString(clsVayNgoai.Sum(p => p.DU_NO)));
                excelSheet.SetValue(startRow3, 6, FormatString(clsVayNgoai.Where(p => p.KY_HAN <= 12).Sum(t => t.SO_TIEN_VAY)));
                excelSheet.SetValue(startRow3, 7, FormatString(clsVayNgoai.Where(p => p.KY_HAN <= 12).Sum(t => t.DU_NO)));
                excelSheet.SetValue(startRow3, 12, FormatString(clsVayNgoai.Where(p => p.KY_HAN > 12).Sum(t => t.SO_TIEN_VAY)));
                excelSheet.SetValue(startRow3, 13, FormatString(clsVayNgoai.Where(p => p.KY_HAN > 12).Sum(t => t.DU_NO)));
                exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                SplashScreenManager.CloseDefaultWaitForm();
            }

            if (EasyDialog.ShowYesNoDialog("Tạo báo cáo thành công. Bạn có muốn tiếp tục không ?") != System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
                this.Dispose();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _clsWPFDanhMucChiNhanh = new clsWPF(cbxDviPsinhDLieu);
            _clsWPFDanhMucChiNhanh.GetDanhMucChiNhanh();
            cbxDviPsinhDLieu.SelectedIndex = 0;
            dateNgayPSinhDLieu.SelectedDate = DateTime.Now;
        }

        private string FormatString(decimal? input)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", input / dviTinh);
        }
    }
}