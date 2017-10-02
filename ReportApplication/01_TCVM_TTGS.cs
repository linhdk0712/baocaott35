using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ReportApplication
{
    /// <summary>
    ///
    /// </summary>
    public partial class G01934 : Form
    {
        private readonly clsFormInfo _clsFormInfo;
        private readonly string _reportName;
        private readonly clsSetFileNameReport _setFileName;
        private clsGetDanhMucChiNhanh _danhMucChiNhanh;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsCreateFileNameReport _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly clsBaoCaoDuNoPhanTheoNganhKinhTe _baoCaoDuNoPhanTheoNganhKinhTe;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();

        /// <summary>
        /// Giá trị thể hiện chu kỳ báo cáo theo ngày - 1, tháng - 2, quý - 3, năm - 4, năm đã kiểm toán - 5
        /// </summary>
        public int ChuKyBaoCao { get; set; }

        /// <summary>
        ///
        /// </summary>
        public G01934()
        {
            InitializeComponent();
            _clsFormInfo = new clsFormInfo(this);
            Text = _clsFormInfo.GetFormDes();
            _setFileName = new clsSetFileNameReport();
            _reportName = _clsFormInfo.GetReportName();
            _baoCaoDuNoPhanTheoNganhKinhTe = new clsBaoCaoDuNoPhanTheoNganhKinhTe();
        }

        private void GetDanhMucChiNhanh()
        {
            _danhMucChiNhanh = new clsGetDanhMucChiNhanh();
            _danhMucChiNhanh.GetDanMucChiNhanh(cbxDviPsinhDLieu);
        }

        private void G01934_Load(object sender, EventArgs e)
        {
            GetDanhMucChiNhanh();
            btnCreateReport.Enabled = false;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowDefaultWaitForm();
                gridControl1.DataSource = null;
                gridControl2.DataSource = null;
                var date = (DateTime)(dateNgayPSinhDLieu.EditValue);
                var lastDayOfPreMont = clsGetLastDayOfMonth.GetLastDayOfPreMont(date.Month,date.Year);
                layoutControlGroup2.TextVisible = true;
                layoutControlGroup2.Text = @"Dữ liệu tính đến ngày : " + DateTime.ParseExact(lastDayOfPreMont,
                                      "yyyyMMdd",
                                       CultureInfo.InvariantCulture).ToShortDateString();
                layoutControlGroup3.TextVisible = true;
                layoutControlGroup3.Text = @"Dữ liệu tính đến ngày : " + date.ToShortDateString();
                gridControl1.DataSource = GetDataPrevMonth();
                gridControl2.DataSource = GetDataCurMonth();
                btnCreateReport.Enabled = true;
                SplashScreenManager.CloseDefaultWaitForm();
            }
            catch (Exception ex)
            {
                EasyDialog.ShowErrorDialog(ex.Message);
            }
        }

        private IEnumerable<clsBaoCaoDuNoPhanTheoNganhKinhTe> GetDataPrevMonth()
        {
            var chiNhanh = cbxDviPsinhDLieu.EditValue.ToString();
            var date = (DateTime)(dateNgayPSinhDLieu.EditValue);
            var lastDayOfPreMont = clsGetLastDayOfMonth.GetLastDayOfPreMont(date.Month,date.Year);
            return _baoCaoDuNoPhanTheoNganhKinhTe.GetAllData(chiNhanh, lastDayOfPreMont);
        }

        private IEnumerable<clsBaoCaoDuNoPhanTheoNganhKinhTe> GetDataCurMonth()
        {
            var chiNhanh = cbxDviPsinhDLieu.EditValue.ToString();
            var date = (DateTime)(dateNgayPSinhDLieu.EditValue);
            var lastDayOfPreMont = clsGetLastDayOfMonth.GetLastDayOfPreMont(date.Month, date.Year);
            return _baoCaoDuNoPhanTheoNganhKinhTe.GetAllData(chiNhanh, date.ToString("yyyyMMdd"));
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
            string ngayBaoCao;
            string maDviPsinhDlieu;
            string fileName;
            _createFileNameReport = new clsCreateFileNameReport();
            _createFileNameReport.GetFileNameReportMonth(out ngayBaoCao, out maDviPsinhDlieu, out fileName,
                cbxDviPsinhDLieu, radioGroup1, _maDonViGui, _reportName, dateNgayPSinhDLieu);
            var saveFileDialog = new SaveFileDialog
            {
                Filter = @"Excel File (*.xlsx)|*.xlsx",
                FilterIndex = 1,
                FileName = fileName
            };
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            SplashScreenManager.ShowDefaultWaitForm();
            var fs = File.OpenRead($"{Application.StartupPath}\\temp\\reports\\{_reportName}.xlsx");
            var exPackage = new ExcelPackage(fs);
            var excelSheet = exPackage.Workbook.Worksheets[1];
            _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), ngayBaoCao, _user, excelSheet);
            //------------------------------------------------------------------------------------
            var duThangTruoc = gridControl1.DataSource as IEnumerable<clsBaoCaoDuNoPhanTheoNganhKinhTe>;
            var duThangNay = gridControl2.DataSource as IEnumerable<clsBaoCaoDuNoPhanTheoNganhKinhTe>;
            var mucDich01 = new[] { "01", "02", "10", "11" };
            var mucDich06 = new[] { "09" };
            var mucDich07 = new[] { "03" };
            var mucDich16 = new[] { "05" };
            var mucDich17 = new[] { "06", "12" };
            var mucDich19 = new[] { "04", "07", "08", "13", "99" };
            // Dư nợ tháng báo cáo
            //------------------------------------------------------------------------------------
            //var duNo01CurMonth = duThangNay.Where(p => mucDich01.Contains(p.MUC_DICH_VAY)).Sum(p=>p.SO_DU);
            //var duNo06CurMonth = duThangNay.Where(p => mucDich06.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var duNo07CurMonth = duThangNay.Where(p => mucDich07.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var duNo16CurMonth = duThangNay.Where(p => mucDich16.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var duNo17CurMonth = duThangNay.Where(p => mucDich17.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var duNo19CurMonth = duThangNay.Where(p => mucDich19.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var tongDuNoCurMonth = duNo01CurMonth + duNo06CurMonth + duNo07CurMonth + duNo16CurMonth + duNo17CurMonth + duNo19CurMonth;
            //// Nợ xấu tháng báo cáo
            ////------------------------------------------------------------------------------------
            //var noXau01CurMonth = duThangNay.Where(p => mucDich01.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangNay.Where(p => mucDich01.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangNay.Where(p => mucDich01.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var noXau06CurMonth = duThangNay.Where(p => mucDich06.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangNay.Where(p => mucDich06.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangNay.Where(p => mucDich06.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var noXau07CurMonth = duThangNay.Where(p => mucDich07.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangNay.Where(p => mucDich07.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangNay.Where(p => mucDich07.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var noXau16CurMonth = duThangNay.Where(p => mucDich16.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangNay.Where(p => mucDich16.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangNay.Where(p => mucDich16.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var noXau17CurMonth = duThangNay.Where(p => mucDich17.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangNay.Where(p => mucDich17.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangNay.Where(p => mucDich17.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var noXau19CurMonth = duThangNay.Where(p => mucDich19.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangNay.Where(p => mucDich19.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangNay.Where(p => mucDich19.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var tongNoXauCurMonth = noXau01CurMonth + noXau06CurMonth + noXau07CurMonth + noXau16CurMonth + noXau17CurMonth + noXau19CurMonth;
            //// Tỷ lệ nợ xấu tháng báo cáo
            ////------------------------------------------------------------------------------------
            //var tyLeNoXau01CurMonth = noXau01CurMonth / duNo01CurMonth * 100;
            //var tyLeNoXau06CurMonth = noXau06CurMonth / duNo06CurMonth * 100;
            //var tyLeNoXau07CurMonth = noXau07CurMonth / duNo07CurMonth * 100;
            //var tyLeNoXau16CurMonth = noXau16CurMonth / duNo16CurMonth * 100;
            //var tyLeNoXau17CurMonth = noXau17CurMonth / duNo17CurMonth * 100;
            //var tyLeNoXau19CurMonth = noXau19CurMonth / duNo19CurMonth * 100;
            //var tongTyLeNoXauCurMonth = tyLeNoXau01CurMonth + tyLeNoXau06CurMonth + tyLeNoXau07CurMonth + tyLeNoXau16CurMonth + tyLeNoXau17CurMonth + tyLeNoXau19CurMonth;
            //// Dư nợ tháng trước
            ////------------------------------------------------------------------------------------
            //var duNo01PrevMonth = duThangTruoc.Where(p => mucDich01.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var duNo06PrevMonth = duThangTruoc.Where(p => mucDich06.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var duNo07PrevMonth = duThangTruoc.Where(p => mucDich07.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var duNo16PrevMonth = duThangTruoc.Where(p => mucDich16.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var duNo17PrevMonth = duThangTruoc.Where(p => mucDich17.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var duNo19PrevMonth = duThangTruoc.Where(p => mucDich19.Contains(p.MUC_DICH_VAY)).Sum(p => p.SO_DU);
            //var tongDuNoThangTruoc = duNo01PrevMonth + duNo06PrevMonth + duNo07PrevMonth + duNo16PrevMonth + duNo17PrevMonth + duNo19PrevMonth;
            //// Nợ xấu tháng trước
            ////------------------------------------------------------------------------------------
            //var noXau01PrevMonth = duThangTruoc.Where(p => mucDich01.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                       duThangTruoc.Where(p => mucDich01.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                       duThangTruoc.Where(p => mucDich01.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var noXau06PrevMonth = duThangTruoc.Where(p => mucDich06.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangTruoc.Where(p => mucDich06.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangTruoc.Where(p => mucDich06.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var noXau07PrevMonth = duThangTruoc.Where(p => mucDich07.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangTruoc.Where(p => mucDich07.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangTruoc.Where(p => mucDich07.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var noXau16PrevMonth = duThangTruoc.Where(p => mucDich16.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangTruoc.Where(p => mucDich16.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangTruoc.Where(p => mucDich16.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var noXau17PrevMonth = duThangTruoc.Where(p => mucDich17.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangTruoc.Where(p => mucDich17.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangTruoc.Where(p => mucDich17.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var noXau19PrevMonth = duThangTruoc.Where(p => mucDich19.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_3) +
            //                      duThangTruoc.Where(p => mucDich19.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_4) +
            //                      duThangTruoc.Where(p => mucDich19.Contains(p.MUC_DICH_VAY)).Sum(p => p.DU_NO_NHOM_5);
            //var tongTyLeNoXauPrevMonth = noXau01PrevMonth + noXau06PrevMonth + noXau07PrevMonth + noXau16PrevMonth + noXau17PrevMonth + noXau19PrevMonth;
            //// Tỷ lệ dư nợ giữa hai tháng
            ////------------------------------------------------------------------------------------
            //var tyLeDuNo01 = duNo01CurMonth / duNo01PrevMonth * 100;
            //var tyLeDuNo06 = duNo06CurMonth / duNo06PrevMonth * 100;
            //var tyLeDuNo07 = duNo07CurMonth / duNo07PrevMonth * 100;
            //var tyLeDuNo16 = duNo16CurMonth / duNo16PrevMonth * 100;
            //var tyLeDuNo17 = duNo17CurMonth / duNo17PrevMonth * 100;
            //var tyLeDuNo19 = duNo19CurMonth / duNo19PrevMonth * 100;
            //var tyLeDuNoTong = tongDuNoCurMonth / tongDuNoThangTruoc * 100;
            //// Tỷ lệ nợ xấu giữa hai tháng
            ////------------------------------------------------------------------------------------
            //decimal tyLeNoXau01 = 0;
            //decimal tyLeNoXau06 = 0;
            //decimal tyLeNoXau07 = 0;
            //decimal tyLeNoXau16 = 0;
            //decimal tyLeNoXau17 = 0;
            //decimal tyLeNoXau19 = 0;
            //decimal tyLeNoXauTong = 0;
            //if (noXau01PrevMonth != 0)
            //{
            //    tyLeNoXau01 = noXau01CurMonth / noXau01PrevMonth * 100;
            //}
            //if (noXau06PrevMonth != 0)
            //{
            //    tyLeNoXau06 = noXau06CurMonth / noXau06PrevMonth * 100;
            //}
            //if (noXau07PrevMonth != 0)
            //{
            //    tyLeNoXau07 = noXau07CurMonth / noXau07PrevMonth * 100;
            //}
            //if (noXau16PrevMonth != 0)
            //{
            //    tyLeNoXau16 = noXau16CurMonth / noXau16PrevMonth * 100;
            //}
            //if (noXau17PrevMonth != 0)
            //{
            //    tyLeNoXau17 = noXau17CurMonth / noXau17PrevMonth * 100;
            //}
            //if (noXau19PrevMonth != 0)
            //{
            //    tyLeNoXau19 = noXau19CurMonth / noXau19PrevMonth * 100;
            //}
            //if (tongTyLeNoXauPrevMonth != 0)
            //{
            //    tyLeNoXauTong = tongTyLeNoXauCurMonth / tongTyLeNoXauPrevMonth * 100;
            //}
            //// Fill data vào excel template
            ////------------------------------------------------------------------------------------
            //excelSheet.Cells["D20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo01CurMonth/dviTinh);
            //excelSheet.Cells["D25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo06CurMonth/dviTinh);
            //excelSheet.Cells["D26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo07CurMonth/dviTinh);
            //excelSheet.Cells["D35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo16CurMonth/dviTinh);
            //excelSheet.Cells["D36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo17CurMonth/dviTinh);
            //excelSheet.Cells["D38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo19CurMonth/dviTinh);
            //excelSheet.Cells["D41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tongDuNoCurMonth / dviTinh);
            ////------------------------------------------------------------------------------------
            //excelSheet.Cells["G20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo01PrevMonth / dviTinh);
            //excelSheet.Cells["G25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo06PrevMonth / dviTinh);
            //excelSheet.Cells["G26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo07PrevMonth / dviTinh);
            //excelSheet.Cells["G35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo16PrevMonth / dviTinh);
            //excelSheet.Cells["G36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo17PrevMonth / dviTinh);
            //excelSheet.Cells["G38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",duNo19PrevMonth / dviTinh);
            //excelSheet.Cells["G41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //           "{0:00.00}",tongDuNoThangTruoc / dviTinh);
            ////------------------------------------------------------------------------------------
            //excelSheet.Cells["H20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeDuNo01);
            //excelSheet.Cells["H25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeDuNo06);
            //excelSheet.Cells["H26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeDuNo07);
            //excelSheet.Cells["H35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeDuNo16);
            //excelSheet.Cells["H36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeDuNo17);
            //excelSheet.Cells["H38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeDuNo19);
            //excelSheet.Cells["H41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //           "{0:00.00}",tyLeDuNoTong);
            ////------------------------------------------------------------------------------------
            //excelSheet.Cells["E20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau01CurMonth / dviTinh);
            //excelSheet.Cells["E25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau06CurMonth / dviTinh);
            //excelSheet.Cells["E26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau07CurMonth / dviTinh);
            //excelSheet.Cells["E35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau16CurMonth / dviTinh);
            //excelSheet.Cells["E36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau17CurMonth / dviTinh);
            //excelSheet.Cells["E38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau19CurMonth / dviTinh);
            //excelSheet.Cells["E41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //           "{0:00.00}",tongNoXauCurMonth / dviTinh);
            ////------------------------------------------------------------------------------------
            //excelSheet.Cells["F20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau01CurMonth);
            //excelSheet.Cells["F25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau06CurMonth);
            //excelSheet.Cells["F26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau07CurMonth);
            //excelSheet.Cells["F35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau16CurMonth);
            //excelSheet.Cells["F36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau17CurMonth);
            //excelSheet.Cells["F38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau19CurMonth);
            //excelSheet.Cells["F41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tongTyLeNoXauCurMonth);
            ////------------------------------------------------------------------------------------
            //excelSheet.Cells["J20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau01);
            //excelSheet.Cells["J25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau06);
            //excelSheet.Cells["J26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau07);
            //excelSheet.Cells["J35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau16);
            //excelSheet.Cells["J36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau17);
            //excelSheet.Cells["J38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",tyLeNoXau19);
            //excelSheet.Cells["J41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //           "{0:00.00}",tyLeNoXauTong);
            ////------------------------------------------------------------------------------------
            //excelSheet.Cells["I20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau01PrevMonth / dviTinh);
            //excelSheet.Cells["I25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau06PrevMonth / dviTinh);
            //excelSheet.Cells["I26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau07PrevMonth / dviTinh);
            //excelSheet.Cells["I35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau16PrevMonth / dviTinh);
            //excelSheet.Cells["I36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau17PrevMonth / dviTinh);
            //excelSheet.Cells["I38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //            "{0:00.00}",noXau19PrevMonth / dviTinh);
            //excelSheet.Cells["I41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            //          "{0:00.00}",tongTyLeNoXauPrevMonth / dviTinh);
            //exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
            SplashScreenManager.CloseDefaultWaitForm();
            this.Close();
            this.Dispose();
        }
    }
}