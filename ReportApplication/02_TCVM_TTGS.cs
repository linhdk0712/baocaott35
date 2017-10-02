using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ReportApplication
{
    /// <summary>
    /// Báo cáo phân loại nợ và trích lập dự phòng rủi ro
    /// </summary>
    public partial class G01204 : Form
    {
        private readonly string _reportName;
        private readonly clsSetFileNameReport _setFileName;
        private clsGetDanhMucChiNhanh _danhMucChiNhanh;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsCreateFileNameReport _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly ClsBaoCaoPhanLoaiNoVaTrichLap _clsBaoCaoPhanLoaiNo;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();

        /// <summary>
        ///
        /// </summary>
        public int ChuKyBaoCao { get; set; }

        /// <summary>
        ///
        /// </summary>
        public G01204()
        {
            InitializeComponent();
            var clsFormInfo = new clsFormInfo(this);
            Text = clsFormInfo.GetFormDes();
            _reportName = clsFormInfo.GetReportName();
            _setFileName = new clsSetFileNameReport();
            _clsBaoCaoPhanLoaiNo = new ClsBaoCaoPhanLoaiNoVaTrichLap();
        }

        private void GetDanhMucChiNhanh()
        {
            _danhMucChiNhanh = new clsGetDanhMucChiNhanh();
            _danhMucChiNhanh.GetDanMucChiNhanh(cbxDviPsinhDlieu);
        }

        private void G01204_Load(object sender, EventArgs e)
        {
            GetDanhMucChiNhanh();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                var dateTime = (DateTime)(dateNgayPsinhDlieu.EditValue);
                var denNgay = dateTime.ToString("yyyyMMdd");
                gridControl1.DataSource = _clsBaoCaoPhanLoaiNo.GetAllData(cbxDviPsinhDlieu.EditValue.ToString(), denNgay);
            }
            catch (Exception ex)
            {
                EasyDialog.ShowErrorDialog(ex.Message);
            }
        }

        private void btnCreatReport_Click(object sender, EventArgs e)
        {
            string ngayBaoCao;
            string maDviPsinhDlieu;
            string fileName;
            _createFileNameReport = new clsCreateFileNameReport();
            _createFileNameReport.GetFileNameReportMonth(out ngayBaoCao, out maDviPsinhDlieu, out fileName,
                cbxDviPsinhDlieu, radioGroup1, _maDonViGui, _reportName, dateNgayPsinhDlieu);
            var saveFileDialog = new SaveFileDialog()
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
            //-----------------------------------------------------
            var duNoNhom1 = (decimal)(gridView1.Columns["DU_NO_NHOM_1"].SummaryItem.SummaryValue);
            var duNoNhom2 = (decimal)(gridView1.Columns["DU_NO_NHOM_2"].SummaryItem.SummaryValue);
            var duNoNhom3 = (decimal)(gridView1.Columns["DU_NO_NHOM_3"].SummaryItem.SummaryValue);
            var duNoNhom4 = (decimal)(gridView1.Columns["DU_NO_NHOM_4"].SummaryItem.SummaryValue);
            var duNoNhom5 = (decimal)(gridView1.Columns["DU_NO_NHOM_5"].SummaryItem.SummaryValue);
            //-----------------------------------------------------
            var duPhongCuThe2 = duNoNhom2 * (decimal)0.02;
            var duPhongCuThe3 = duNoNhom3 * (decimal)0.25;
            var duPhongCuThe4 = duNoNhom4 * (decimal)0.5;
            var duPhongCuThe5 = duNoNhom5 * 1;
            //-----------------------------------------------------
            var duPhongChung1 = duNoNhom1 * (decimal)0.005;
            var duPhongChung2 = duNoNhom2 * (decimal)0.005;
            var duPhongChung3 = duNoNhom3 * (decimal)0.005;
            var duPhongChung4 = duNoNhom4 * (decimal)0.005;
            //-----------------------------------------------------
            var tongDuNo = duNoNhom1 + duNoNhom2 + duNoNhom3 + duNoNhom4 + duNoNhom5;
            var tongNoXau = duNoNhom3 + duNoNhom4 + duNoNhom5;
            var tyLeNoXau = tongNoXau / tongDuNo * 100;
            //-----------------------------------------------------
            excelSheet.Cells["D18"].Value = excelSheet.Cells["D19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duNoNhom1 / dviTinh);
            excelSheet.Cells["D23"].Value = excelSheet.Cells["D24"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duNoNhom2 / dviTinh);
            excelSheet.Cells["D28"].Value = excelSheet.Cells["D29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duNoNhom3 / dviTinh);
            excelSheet.Cells["D33"].Value = excelSheet.Cells["D34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duNoNhom4 / dviTinh);
            excelSheet.Cells["D38"].Value = excelSheet.Cells["D39"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duNoNhom5 / dviTinh);
            excelSheet.Cells["D43"].Value = excelSheet.Cells["D44"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", tongDuNo / dviTinh);
            //-----------------------------------------------------
            excelSheet.Cells["D49"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", tongNoXau / dviTinh);
            excelSheet.Cells["D50"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", tyLeNoXau);
            //-----------------------------------------------------
            excelSheet.Cells["F18"].Value = excelSheet.Cells["F19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duPhongChung1 / dviTinh);
            excelSheet.Cells["F23"].Value = excelSheet.Cells["F24"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duPhongChung2 / dviTinh);
            excelSheet.Cells["F28"].Value = excelSheet.Cells["F29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duPhongChung3 / dviTinh);
            excelSheet.Cells["F33"].Value = excelSheet.Cells["F34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duPhongChung4 / dviTinh);
            //-----------------------------------------------------
            excelSheet.Cells["E23"].Value = excelSheet.Cells["E24"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duPhongCuThe2 / dviTinh);
            excelSheet.Cells["E28"].Value = excelSheet.Cells["E29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duPhongCuThe3 / dviTinh);
            excelSheet.Cells["E33"].Value = excelSheet.Cells["E34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duPhongCuThe4 / dviTinh);
            excelSheet.Cells["E38"].Value = excelSheet.Cells["E39"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", duPhongCuThe5 / dviTinh);
            exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
            SplashScreenManager.CloseDefaultWaitForm();
            this.Close();
            this.Dispose();
        }
    }
}