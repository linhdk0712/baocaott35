using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using System;
using System.IO;
using System.Windows.Forms;

namespace ReportApplication
{
    public partial class G01264 : Form
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
        /// Hàm khởi tạo
        /// </summary>
        public G01264()
        {
            InitializeComponent();
            _clsFormInfo = new clsFormInfo(this);
            Text = _clsFormInfo.GetFormDes();
            _setFileName = new clsSetFileNameReport();
            _reportName = _clsFormInfo.GetReportName();
        }

        private void GetDanhMucChiNhanh()
        {
            _danhMucChiNhanh = new clsGetDanhMucChiNhanh();
            _danhMucChiNhanh.GetDanMucChiNhanh(cbxDviPsinhDLieu);
        }

        private void G01264_Load(object sender, EventArgs e)
        {
            GetDanhMucChiNhanh();
            btnCreateReport.Enabled = false;
            cbxDviPsinhDLieu.Enabled = false;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            EasyDialog.ShowInfoDialog("Báo cáo này không phát sinh dữ liệu. Bạn có thể tạo file báo cáo ngay");
            btnCreateReport.Enabled = true;
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
            exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
            SplashScreenManager.CloseDefaultWaitForm();
            this.Close();
            this.Dispose();
        }
    }
}