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
    /// Interaction logic for G02832.xaml
    /// </summary>
    public partial class G02832 : Window
    {
        private const string _reportName = "G02832";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsWPF _clsWPFDanhMucChiNhanh;
        private clsTyLeKhaNangChiTra _clsTyLeKhaNangChiTra;
        public int iDinhKyBaoCao { get; set; }

        public G02832()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
            _createFileNameReport = new clsGetFileNameReports(iDinhKyBaoCao);
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _clsTyLeKhaNangChiTra = new clsTyLeKhaNangChiTra();
                var clsGetDateReport = new clsGetLastDayOfMonth();
                var res = new List<DateTime>();
                var ngayPhatSinhDuLieu = (DateTime)(dateNgayPSinhDLieu.SelectedDate);
                var firstDayOfMonth = new DateTime(ngayPhatSinhDuLieu.Year, ngayPhatSinhDuLieu.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                var endOfPreviousMonth = firstDayOfMonth.AddDays(-1);
                var day10OfMonth = firstDayOfMonth.AddDays(9);
                var day20OfMonth = firstDayOfMonth.AddDays(19);
                if (ngayPhatSinhDuLieu.Day >= 10 && ngayPhatSinhDuLieu.Day < 20)
                {
                    res = clsGetDateReport.GetRange(endOfPreviousMonth, day10OfMonth);
                }
                else if (ngayPhatSinhDuLieu.Day >= 20 && ngayPhatSinhDuLieu.Day < lastDayOfMonth.Day)
                {
                    res = clsGetDateReport.GetRange(firstDayOfMonth.AddDays(9), day20OfMonth);
                }
                else if (ngayPhatSinhDuLieu.Day >= lastDayOfMonth.Day)
                {
                    res = clsGetDateReport.GetRange(firstDayOfMonth.AddDays(19), lastDayOfMonth);
                }
                var sTt = 1;
                var startRow = 18;

                string tuNgay;
                string denNgay;
                decimal tienMat;
                decimal _tienGuiNHNN;
                decimal _tienGuiNHKHAC;
                decimal _tienGuiTN;
                decimal _tyLeKNCT;
                decimal _42;
                decimal _42321;
                string ngayBaoCao;
                string maDviPsinhDlieu;
                string fileName;
                _createFileNameReport.GetFileNameReportDayWpf(out ngayBaoCao, out maDviPsinhDlieu, out fileName, cbxDviPsinhDLieu, _maDonViGui, _reportName, dateNgayPSinhDLieu);
                var saveFileDialog = new SaveFileDialog() { Filter = @"Excel File (*.xlsx)|*.xlsx", FilterIndex = 1, FileName = fileName };
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    SplashScreenManager.ShowDefaultWaitForm();
                    var fs = File.OpenRead($"{AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
                    var exPackage = new ExcelPackage(fs);
                    var excelSheet = exPackage.Workbook.Worksheets[1];
                    _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui, clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), ngayBaoCao, _user, excelSheet);
                    for (var i = 0; i < res.Count - 1; i++)
                    {
                        tuNgay = res[i].ToString("yyyyMMdd");
                        denNgay = res[i + 1].ToString("yyyyMMdd");
                        var _bangCanDoiKeToan = _clsTyLeKhaNangChiTra.GetTyLeKhaNangChiTra(maDviPsinhDlieu, tuNgay, denNgay);
                        tienMat = (from a in _bangCanDoiKeToan where a.F03 == "10" select a.F08).FirstOrDefault();
                        _tienGuiNHNN = 0;
                        _tienGuiNHKHAC = (from a in _bangCanDoiKeToan where a.F03 == "13" select a.F08).FirstOrDefault();
                        _42 = (from a in _bangCanDoiKeToan where a.F03 == "42" select a.F09).FirstOrDefault();
                        _42321 = (from a in _bangCanDoiKeToan where a.F03 == "42321" select a.F09).FirstOrDefault();
                        _tienGuiTN = _42 - _42321;
                        _tyLeKNCT = Math.Round((tienMat + _tienGuiNHNN + _tienGuiNHKHAC) / _tienGuiTN * 100, 2);
                        excelSheet.Cells["B" + startRow].Value = sTt;
                        excelSheet.Cells["C" + startRow].Value = res[i + 1].ToString("yyyyMMdd");
                        excelSheet.Cells["D" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", Math.Round(tienMat / dviTinh), 1);
                        excelSheet.Cells["F" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", Math.Round(_tienGuiNHKHAC / dviTinh), 1);
                        excelSheet.Cells["G" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", Math.Round(_tienGuiTN / dviTinh), 1);
                        excelSheet.Cells["H" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", _tyLeKNCT);
                        sTt++;
                        startRow++;
                    }
                    exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                    _clsTyLeKhaNangChiTra.Dispose();
                    SplashScreenManager.CloseDefaultWaitForm();
                    if (EasyDialog.ShowYesNoDialog("Tạo báo cáo thành công. Bạn có muốn tiếp tục không ?") != System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var _dateTime = ((DateTime)(dateNgayPSinhDLieu.SelectedDate));
                string _denNgay;
                string _thang;
                if (_dateTime.Month.ToString().Length < 2)
                {
                    _thang = "0" + _dateTime.Month.ToString();
                }
                else
                {
                    _thang = _dateTime.Month.ToString();
                }
                if (cbxKyBaoCao.SelectedIndex == 0)
                {
                    _denNgay = (_dateTime.Year.ToString() + _thang + 10);
                }
                else if (cbxKyBaoCao.SelectedIndex == 1)
                {
                    _denNgay = (_dateTime.Year.ToString() + _thang + 20);
                }
                else
                {
                    _denNgay = _dateTime.Year.ToString() + _thang + clsGetLastDayOfMonth.GetLastDayOfMonth(_dateTime.Month,_dateTime.Year);
                }
                var _tuNgay = clsGetLastDayOfMonth.GetFirstDayOfMont(_dateTime.Month,_dateTime.Year);
                _clsTyLeKhaNangChiTra = new clsTyLeKhaNangChiTra();
                grdReport.ItemsSource = _clsTyLeKhaNangChiTra.GetTyLeKhaNangChiTra("00", _tuNgay, _denNgay);
                _clsTyLeKhaNangChiTra.Dispose();
            }
            catch (Exception ex)
            {
                EasyDialog.ShowErrorDialog(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _clsWPFDanhMucChiNhanh = new clsWPF(cbxDviPsinhDLieu);
            _clsWPFDanhMucChiNhanh.GetDanhMucChiNhanh();
            cbxDviPsinhDLieu.SelectedIndex = 0;
            var date = DateTime.Now;
            dateNgayPSinhDLieu.SelectedDate = date;
        }

        private void cbxDviPsinhDLieu_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

        private void dateNgayPSinhDLieu_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var date = (DateTime)dateNgayPSinhDLieu.SelectedDate;
            if (date.Day <= 10)
            {
                cbxKyBaoCao.SelectedIndex = 0;
            }
            else if (date.Day <= 20 && date.Day > 10)
            {
                cbxKyBaoCao.SelectedIndex = 1;
            }
            else
            {
                cbxKyBaoCao.SelectedIndex = 2;
            }
        }
    }
}