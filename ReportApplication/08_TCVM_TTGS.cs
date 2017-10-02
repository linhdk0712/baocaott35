using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ReportApplication
{
    /// <summary>
    ///
    /// </summary>
    public partial class G02832 : Form
    {
        private clsFormInfo _clsFormInfo;
        private string _reportName;
        private clsSetFileNameReport _setFileName;
        private clsGetDanhMucChiNhanh _danhMucChiNhanh;
        private string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsCreateFileNameReport _createFileNameReport;
        private clsTyLeKhaNangChiTra _clsTyLeKhaNangChiTra;
        private string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();

        /// <summary>
        ///
        /// </summary>
        public int ChuKyBaoCao { get; set; }

        /// <summary>
        ///
        /// </summary>
        public G02832()
        {
            InitializeComponent();
            _clsFormInfo = new clsFormInfo(this);
            this.Text = _clsFormInfo.GetFormDes();
            _reportName = _clsFormInfo.GetReportName();
            _setFileName = new clsSetFileNameReport();
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                _clsTyLeKhaNangChiTra = new clsTyLeKhaNangChiTra();
                var clsGetDateReport = new clsGetLastDayOfMonth();
                var res = new List<DateTime>();
                var ngayPhatSinhDuLieu = (DateTime)(dateNgayPsinhDLieu.EditValue);
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
                string ngayBaoCao;
                string maDviPsinhDlieu;
                string fileName;
                string tuNgay;
                string denNgay;
                _createFileNameReport = new clsCreateFileNameReport();
                _createFileNameReport.GetFileNameReportDay(out ngayBaoCao, out maDviPsinhDlieu, out fileName, radioGroup1, _maDonViGui, _reportName, dateNgayPsinhDLieu);
                var saveFileDialog = new SaveFileDialog() { Filter = @"Excel File (*.xlsx)|*.xlsx", FilterIndex = 1, FileName = fileName };
                decimal tienMat;
                decimal _tienGuiNHNN;
                decimal _tienGuiNHKHAC;
                decimal _tienGuiTN;
                decimal _tyLeKNCT;
                decimal _42;
                decimal _42321;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SplashScreenManager.ShowDefaultWaitForm();
                    var fs = File.OpenRead($"{Application.StartupPath}\\temp\\reports\\{_reportName}.xlsx");
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
                        _tyLeKNCT = (tienMat + _tienGuiNHNN + _tienGuiNHKHAC) / _tienGuiTN * 100;
                        excelSheet.Cells["B" + startRow].Value = sTt;
                        excelSheet.Cells["C" + startRow].Value = "Dữ liệu ngày " + res[i + 1].ToShortDateString();
                        excelSheet.Cells["D" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", tienMat / dviTinh);
                        excelSheet.Cells["F" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", _tienGuiNHKHAC / dviTinh);
                        excelSheet.Cells["G" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", _tienGuiTN / dviTinh);
                        excelSheet.Cells["H" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", _tyLeKNCT);
                        sTt++;
                        startRow++;
                    }
                    exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                    _clsTyLeKhaNangChiTra.Dispose();
                    SplashScreenManager.CloseDefaultWaitForm();
                    this.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void _08_TCVM_TTGS_LoadExtracted()
        {
            _danhMucChiNhanh = new clsGetDanhMucChiNhanh();
            _danhMucChiNhanh.GetDanMucChiNhanh(cbxDviPsinhDlieu);
            _danhMucChiNhanh.Dispose();
        }

        private void _08_TCVM_TTGS_Load(object sender, EventArgs e)
        {
            _08_TCVM_TTGS_LoadExtracted();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                var _dateTime = ((DateTime)(dateNgayPsinhDLieu.EditValue));
                var _tuNgay = clsGetLastDayOfMonth.GetFirstDayOfMont(_dateTime.Month, _dateTime.Year);
                var _denNgay = _dateTime.ToString("yyyyMMdd");
                _clsTyLeKhaNangChiTra = new clsTyLeKhaNangChiTra();
                gridControl1.DataSource = _clsTyLeKhaNangChiTra.GetTyLeKhaNangChiTra("00", _tuNgay, _denNgay);
                _clsTyLeKhaNangChiTra.Dispose();
            }
            catch (Exception ex)
            {
                EasyDialog.ShowErrorDialog(ex.Message);
            }
        }
    }
}