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
using DevExpress.Utils;

namespace ReportWpfApplication
{
    /// <summary>
    /// Interaction logic for G01934.xaml
    /// </summary>
    public partial class G01934 : Window, IDisposable
    {
        private const string _reportName = "G01934";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly clsBaoCaoDuNoPhanTheoNganhKinhTe _baoCaoDuNoPhanTheoNganhKinhTe;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public void Dispose()
        {
            if (_baoCaoDuNoPhanTheoNganhKinhTe != null)
                _baoCaoDuNoPhanTheoNganhKinhTe.Dispose();
        }

        public G01934()
        {
            InitializeComponent();
            _baoCaoDuNoPhanTheoNganhKinhTe = new clsBaoCaoDuNoPhanTheoNganhKinhTe();
            _setFileName = new clsSetFileNameReport();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _clsWPFDanhMucChiNhanh = new clsWPF(cbxDviPsinhDLieu);
            _clsWPFDanhMucChiNhanh.GetDanhMucChiNhanh();
            dateNgayPSinhDLieu.SelectedDate = DateTime.Now;
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            EasyDialog.ShowInfoDialog("Chức năng này đã được sửa đổi. Hãy chọn chức năng Tạo báo cáo");
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var date = (DateTime)(dateNgayPSinhDLieu.SelectedDate);
                var lastDayOfPreMont = clsGetLastDayOfMonth.GetLastDayOfPreMont(date.Month,date.Year);
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
                    var fileTemplate = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
                    var exPackage = new ExcelPackage(newFile, fileTemplate);
                    string _ngayBaoCao = _createFileNameReport.NgayBaoCao(dateNgayPSinhDLieu);
                    var excelSheet = exPackage.Workbook.Worksheets[1];
                    _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                       clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);
                    var duThangTruoc = _baoCaoDuNoPhanTheoNganhKinhTe.GetAllData(maChiNhanh, lastDayOfPreMont);
                    var duThangNay = _baoCaoDuNoPhanTheoNganhKinhTe.GetAllData(maChiNhanh, date.ToString("yyyyMMdd"));
                    var mucDich01 = new[] { "01", "02", "10", "11" };
                    var mucDich06 = new[] { "09" };
                    var mucDich07 = new[] { "03" };
                    var mucDich16 = new[] { "05" };
                    var mucDich17 = new[] { "06", "12" };
                    var mucDich19 = new[] { "04", "07", "08", "13", "99", "100", "101", "MUC_DICH_VAY_SXKD", "MUC_DICH_VAY_SXNN", "MUC_DICH_VAY_TDCN", "MUC_DICH_VAY_HSSV" };
                    // Dư nợ tháng báo cáo
                    //------------------------------------------------------------------------------------
                    decimal duNo01CurMonth = 0;
                    decimal duNo06CurMonth = 0;
                    decimal duNo07CurMonth = 0;
                    decimal duNo16CurMonth = 0;
                    decimal duNo17CurMonth = 0;
                    decimal duNo19CurMonth = 0;
                    decimal tongDuNoCurMonth = 0;
                    foreach (var item in mucDich01)
                    {
                        duNo01CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich06)
                    {
                        duNo06CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich07)
                    {
                        duNo07CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich16)
                    {
                        duNo16CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich17)
                    {
                        duNo17CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich19)
                    {
                        duNo19CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    tongDuNoCurMonth = duNo01CurMonth + duNo06CurMonth + duNo07CurMonth + duNo16CurMonth + duNo17CurMonth + duNo19CurMonth;
                    //----------------------------------------------------------------------------------------------------------------------
                    // Nợ xấu
                    decimal noXau01CurMonth = 0;
                    decimal noXau06CurMonth = 0;
                    decimal noXau07CurMonth = 0;
                    decimal noXau16CurMonth = 0;
                    decimal noXau17CurMonth = 0;
                    decimal noXau19CurMonth = 0;
                    decimal tongNoXauCurMonth = 0;
                    foreach (var item in mucDich01)
                    {
                        noXau01CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich06)
                    {
                        noXau06CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich07)
                    {
                        noXau07CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich16)
                    {
                        noXau16CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich17)
                    {
                        noXau17CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich19)
                    {
                        noXau19CurMonth += (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    tongNoXauCurMonth = noXau01CurMonth + noXau06CurMonth + noXau07CurMonth + noXau16CurMonth + noXau17CurMonth + noXau19CurMonth;
                    //--------------------------------------------------------------------------------------------------
                    // Tỷ lệ nợ xấu
                    decimal tyLeNoXau01CurMonth = 0;
                    decimal tyLeNoXau06CurMonth = 0;
                    decimal tyLeNoXau07CurMonth = 0;
                    decimal tyLeNoXau16CurMonth = 0;
                    decimal tyLeNoXau17CurMonth = 0;
                    decimal tyLeNoXau19CurMonth = 0;
                    decimal tyLeNoXauCurMonth = 0;
                    if (duNo01CurMonth > 0)
                    {
                        tyLeNoXau01CurMonth = Math.Round(noXau01CurMonth / duNo01CurMonth * 100, 2);
                    }
                    if (duNo06CurMonth > 0)
                    {
                        tyLeNoXau06CurMonth = Math.Round(noXau06CurMonth / duNo06CurMonth * 100, 2);
                    }
                    if (duNo07CurMonth > 0)
                    {
                        tyLeNoXau07CurMonth = Math.Round(noXau07CurMonth / duNo07CurMonth * 100, 2);
                    }
                    if (duNo16CurMonth > 0)
                    {
                        tyLeNoXau16CurMonth = Math.Round(noXau16CurMonth / duNo16CurMonth * 100, 2);
                    }
                    if (duNo17CurMonth > 0)
                    {
                        tyLeNoXau17CurMonth = Math.Round(noXau17CurMonth / duNo17CurMonth * 100, 2);
                    }
                    if (duNo19CurMonth > 0)
                    {
                        tyLeNoXau19CurMonth = Math.Round(noXau19CurMonth / duNo19CurMonth * 100, 2);
                    }
                    if (tongDuNoCurMonth > 0)
                    {
                        tyLeNoXauCurMonth = Math.Round(tongNoXauCurMonth / tongDuNoCurMonth * 100, 2);
                    }

                    //------------------------------------------------------------------------------------------
                    // Dư nợ tháng trước
                    decimal duNo01PrevMonth = 0;
                    decimal duNo06PrevMonth = 0;
                    decimal duNo07PrevMonth = 0;
                    decimal duNo16PrevMonth = 0;
                    decimal duNo17PrevMonth = 0;
                    decimal duNo19PrevMonth = 0;
                    decimal tongDuNoPrevMonth = 0;
                    foreach (var item in mucDich01)
                    {
                        duNo01PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich06)
                    {
                        duNo06PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich07)
                    {
                        duNo07PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich16)
                    {
                        duNo16PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich17)
                    {
                        duNo17PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich19)
                    {
                        duNo19PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    tongDuNoPrevMonth = duNo01PrevMonth + duNo06PrevMonth + duNo07PrevMonth + duNo16PrevMonth + duNo17PrevMonth + duNo19PrevMonth;
                    //----------------------------------------------------------------------------------------------------------------------
                    // Nợ xấu
                    decimal noXau01PrevMonth = 0;
                    decimal noXau06PrevMonth = 0;
                    decimal noXau07PrevMonth = 0;
                    decimal noXau16PrevMonth = 0;
                    decimal noXau17PrevMonth = 0;
                    decimal noXau19PrevMonth = 0;
                    decimal tongNoXauPrevMonth = 0;
                    foreach (var item in mucDich01)
                    {
                        noXau01PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich06)
                    {
                        noXau06PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich07)
                    {
                        noXau07PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich16)
                    {
                        noXau16PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich17)
                    {
                        noXau17PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    foreach (var item in mucDich19)
                    {
                        noXau19PrevMonth += (from a in duThangTruoc where a.MUC_DICH_VAY == item select a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault();
                    }
                    tongNoXauPrevMonth = noXau01PrevMonth + noXau06PrevMonth + noXau07PrevMonth + noXau16PrevMonth + noXau17PrevMonth + noXau19PrevMonth;
                    //--------------------------------------------------------------------------------------------------
                    // Tỷ lệ dư nợ
                    decimal tyLeDuNo01 = 0;
                    decimal tyLeDuNo06 = 0;
                    decimal tyLeDuNo07 = 0;
                    decimal tyLeDuNo16 = 0;
                    decimal tyLeDuNo17 = 0;
                    decimal tyLeDuNo19 = 0;
                    decimal tyLeDuNo = 0;
                    if (duNo01PrevMonth > 0)
                    {
                        tyLeDuNo01 = Math.Round((duNo01CurMonth - duNo01PrevMonth) / duNo01PrevMonth * 100, 2);
                    }

                    if (duNo06PrevMonth > 0)
                    {
                        tyLeDuNo06 = Math.Round((duNo06CurMonth - duNo06PrevMonth) / duNo06PrevMonth * 100, 2);
                    }

                    if (duNo07PrevMonth > 0)
                    {
                        tyLeDuNo07 = Math.Round((duNo07CurMonth - duNo07PrevMonth) / duNo07PrevMonth * 100, 2);
                    }

                    if (duNo16PrevMonth > 0)
                    {
                        tyLeDuNo16 = Math.Round((duNo16CurMonth - duNo16PrevMonth) / duNo16PrevMonth * 100, 2);
                    }

                    if (duNo17PrevMonth > 0)
                    {
                        tyLeDuNo17 = Math.Round((duNo17CurMonth - duNo17PrevMonth) / duNo17PrevMonth * 100, 2);
                    }

                    if (duNo19PrevMonth > 0)
                    {
                        tyLeDuNo19 = Math.Round((duNo19CurMonth - duNo19PrevMonth) / duNo19PrevMonth * 100, 2);
                    }

                    if (tongDuNoPrevMonth > 0)
                    {
                        tyLeDuNo = Math.Round((tongDuNoCurMonth - tongDuNoPrevMonth) / tongDuNoPrevMonth * 100, 2);
                    }

                    // Tỷ lệ nợ xấu
                    decimal tyleNoXau01 = 0;
                    decimal tyleNoXau06 = 0;
                    decimal tyleNoXau07 = 0;
                    decimal tyleNoXau16 = 0;
                    decimal tyleNoXau17 = 0;
                    decimal tyleNoXau19 = 0;
                    decimal tyleNoXau = 0;
                    if (noXau01PrevMonth > 0)
                    { tyleNoXau01 = Math.Round((noXau01CurMonth - noXau01PrevMonth) / noXau01PrevMonth * 100, 2); }

                    if (noXau06PrevMonth > 0)
                    { tyleNoXau06 = Math.Round((noXau06CurMonth - noXau06PrevMonth) / noXau06PrevMonth * 100, 2); }

                    if (noXau07PrevMonth > 0)
                    { tyleNoXau07 = Math.Round((noXau07CurMonth - noXau07PrevMonth) / noXau07PrevMonth * 100, 2); }

                    if (noXau16PrevMonth > 0)
                    { tyleNoXau16 = Math.Round((noXau16CurMonth - noXau16PrevMonth) / noXau16PrevMonth * 100, 2); }

                    if (noXau17PrevMonth > 0)
                    { tyleNoXau17 = Math.Round((noXau17CurMonth - noXau17PrevMonth) / noXau17PrevMonth * 100, 2); }

                    if (noXau19PrevMonth > 0)
                    { tyleNoXau19 = Math.Round((noXau19CurMonth - noXau19PrevMonth) / noXau19PrevMonth * 100, 2); }
                    tyleNoXau = Math.Round(tyleNoXau01 + tyleNoXau06 + tyleNoXau07 + tyleNoXau16 + tyleNoXau17 + tyleNoXau19, 2);
                    // Bổ sung ngày 20/06/2017 theo ý kiến góp ý của chị Thìn
                    var tangGiamDuNo01 = duNo01CurMonth - duNo01PrevMonth;
                    var tangGiamDuNo06 = duNo06CurMonth - duNo06PrevMonth;
                    var tangGiamDuNo07 = duNo07CurMonth - duNo07PrevMonth;
                    var tangGiamDuNo16 = duNo16CurMonth - duNo16PrevMonth;
                    var tangGiamDuNo17 = duNo17CurMonth - duNo17PrevMonth;
                    var tangGiamDuNo19 = duNo19CurMonth - duNo19PrevMonth;
                    var tongTangGiamDuNo = tangGiamDuNo01 + tangGiamDuNo06 + tangGiamDuNo07 + tangGiamDuNo16 + tangGiamDuNo17 + tangGiamDuNo19;
                    var tangGiamNoXau01 = noXau01CurMonth - noXau01PrevMonth;
                    var tangGiamNoXau06 = noXau06CurMonth - noXau06PrevMonth;
                    var tangGiamNoXau07 = noXau07CurMonth - noXau07PrevMonth;
                    var tangGiamNoXau16 = noXau16CurMonth - noXau16PrevMonth;
                    var tangGiamNoXau17 = noXau17CurMonth - noXau17PrevMonth;
                    var tangGiamNoXau19 = noXau19CurMonth - noXau19PrevMonth;
                    var tongTangGiamNoXau = tangGiamNoXau01 + tangGiamNoXau06 + tangGiamNoXau07 + tangGiamNoXau16 + tangGiamNoXau17 + tangGiamNoXau19;
                    // Insert to excel file
                    excelSheet.Cells["D20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                                "{0:00.0}", Math.Round(duNo01CurMonth / dviTinh, 1));
                    excelSheet.Cells["D25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(duNo06CurMonth / dviTinh, 1));
                    excelSheet.Cells["D26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(duNo07CurMonth / dviTinh, 1));
                    excelSheet.Cells["D35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(duNo16CurMonth / dviTinh, 1));
                    excelSheet.Cells["D36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(duNo17CurMonth / dviTinh, 1));
                    excelSheet.Cells["D38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(duNo19CurMonth / dviTinh, 1));
                    excelSheet.Cells["D41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tongDuNoCurMonth / dviTinh, 1));
                    //-----------------------------------------------------------------------------------
                    excelSheet.Cells["E20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(noXau01CurMonth / dviTinh, 1));
                    excelSheet.Cells["E25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(noXau06CurMonth / dviTinh, 1));
                    excelSheet.Cells["E26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(noXau07CurMonth / dviTinh, 1));
                    excelSheet.Cells["E35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(noXau16CurMonth / dviTinh, 1));
                    excelSheet.Cells["E36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(noXau17CurMonth / dviTinh, 1));
                    excelSheet.Cells["E38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(noXau19CurMonth / dviTinh, 1));
                    excelSheet.Cells["E41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tongNoXauCurMonth / dviTinh, 1));
                    //----------------------------------------------------------------------------
                    excelSheet.Cells["F20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                              "{0:00.00}", Math.Round(tyLeNoXau01CurMonth, 2));
                    excelSheet.Cells["F25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeNoXau06CurMonth, 2));
                    excelSheet.Cells["F26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeNoXau07CurMonth, 2));
                    excelSheet.Cells["F35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeNoXau16CurMonth, 2));
                    excelSheet.Cells["F36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeNoXau17CurMonth, 2));
                    excelSheet.Cells["F38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeNoXau19CurMonth, 2));
                    excelSheet.Cells["F41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeNoXauCurMonth, 2));
                    //----------------------------------------------------------------------------
                    excelSheet.Cells["G20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                              "{0:00.0}", Math.Round(tangGiamDuNo01 / dviTinh, 1));
                    excelSheet.Cells["G25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tangGiamDuNo06 / dviTinh, 1));
                    excelSheet.Cells["G26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tangGiamDuNo07 / dviTinh, 1));
                    excelSheet.Cells["G35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tangGiamDuNo16 / dviTinh, 1));
                    excelSheet.Cells["G36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tangGiamDuNo17 / dviTinh, 1));
                    excelSheet.Cells["G38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tangGiamDuNo19 / dviTinh, 1));
                    excelSheet.Cells["G41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tongTangGiamDuNo / dviTinh, 1));
                    //----------------------------------------------------------------------------
                    excelSheet.Cells["I20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                              "{0:00.0}", Math.Round(tangGiamNoXau01 / dviTinh, 1));
                    excelSheet.Cells["I25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tangGiamNoXau06 / dviTinh, 1));
                    excelSheet.Cells["I26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tangGiamNoXau07 / dviTinh, 1));
                    excelSheet.Cells["I35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tangGiamNoXau16 / dviTinh, 1));
                    excelSheet.Cells["I36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tangGiamNoXau17 / dviTinh, 1));
                    excelSheet.Cells["I38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tangGiamNoXau19 / dviTinh, 1));
                    excelSheet.Cells["I41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.0}", Math.Round(tongTangGiamNoXau / dviTinh, 1));
                    //----------------------------------------------------------------------------
                    excelSheet.Cells["J20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                              "{0:00.00}", Math.Round(tyleNoXau01, 2));
                    excelSheet.Cells["J25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyleNoXau06, 2));
                    excelSheet.Cells["J26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyleNoXau07, 2));
                    excelSheet.Cells["J35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyleNoXau16, 2));
                    excelSheet.Cells["J36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyleNoXau17, 2));
                    excelSheet.Cells["J38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyleNoXau19, 2));
                    excelSheet.Cells["J41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyleNoXau, 2));
                    //----------------------------------------------------------------------------
                    excelSheet.Cells["H20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                              "{0:00.00}", Math.Round(tyLeDuNo01, 2));
                    excelSheet.Cells["H25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeDuNo06, 2));
                    excelSheet.Cells["H26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeDuNo07, 2));
                    excelSheet.Cells["H35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeDuNo16, 2));
                    excelSheet.Cells["H36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeDuNo17, 2));
                    excelSheet.Cells["H38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeDuNo19, 2));
                    excelSheet.Cells["H41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                               "{0:00.00}", Math.Round(tyLeDuNo, 2));
                    exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                }
                SplashScreenManager.CloseDefaultWaitForm();
                if (EasyDialog.ShowYesNoDialog("Tạo báo cáo thành công. Bạn có muốn tiếp tục không ?") != System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                EasyDialog.ShowUnsuccessfulDialog(ex.Message);
            }
        }
    }
}