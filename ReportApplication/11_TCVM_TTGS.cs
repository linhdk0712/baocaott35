using OfficeOpenXml;

// XSSFWorkbook, XSSFSheet
using ReportApplication.BAL;
using ReportApplication.Common;
using System;
using System.IO; // File.Exists()
using System.Windows.Forms;

namespace ReportApplication
{
    public partial class _11_TCVM_TTGS : Form
    {
        private clsFormInfo _clsFormInfo;
        private string _reportName;
        private clsSetFileNameReport _setFileName;
        private string _maDonViGui = clsMaDonViGui.MaDonViGui();

        public _11_TCVM_TTGS()
        {
            InitializeComponent();
            _clsFormInfo = new clsFormInfo(this);
            this.Text = _clsFormInfo.GetFormDes();
            _reportName = _clsFormInfo.GetReportName();
            _setFileName = new clsSetFileNameReport();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string _fileName = _reportName + "-" + clsMaDonViPhatSinh.GetMaNganHang("0001") + "-" + _maDonViGui;
            var _tempReport = string.Format("{0}\\temp\\reports\\{1}{2}", Application.StartupPath, _reportName, ".xlsx");
            var saveFileDialog = new SaveFileDialog() { Filter = "Excel File (*.xlsx)|*.xlsx", FilterIndex = 1 };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = File.OpenRead(_tempReport);
                ExcelPackage exPackage = new ExcelPackage(fs);
                ExcelWorksheet excelSheet = exPackage.Workbook.Worksheets[1];
                _setFileName.SetFileNameReport(_reportName, _maDonViGui, clsMaDonViPhatSinh.GetMaNganHang("0001"), "201703", "LINHDK", excelSheet);
                exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
            }
        }

        private void _11_TCVM_TTGS_Load(object sender, EventArgs e)
        {
        }
    }
}