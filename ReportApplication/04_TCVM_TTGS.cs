using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ReportApplication
{
    public partial class G01254 : Form
    {
        private readonly string _reportName;
        private readonly clsSetFileNameReport _setFileName;
        private clsGetDanhMucChiNhanh _danhMucChiNhanh;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsCreateFileNameReport _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan _clsBaoCaoTinhHinhHuyDongVon;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();

        /// <summary>
        /// Giá trị thể hiện chu kỳ báo cáo theo ngày - 1, tháng - 2, quý - 3, năm - 4, năm đã kiểm toán - 5
        /// </summary>
        public int ChuKyBaoCao { get; set; }

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        public G01254()
        {
            InitializeComponent();
            var clsFormInfo = new clsFormInfo(this);
            Text = clsFormInfo.GetFormDes();
            _reportName = clsFormInfo.GetReportName();
            _setFileName = new clsSetFileNameReport();
        }

        private void GetDanhMucChiNhanh()
        {
            _danhMucChiNhanh = new clsGetDanhMucChiNhanh();
            _danhMucChiNhanh.GetDanMucChiNhanh(cbxDviPsinhDLieu);
        }

        private void G01254_Load(object sender, EventArgs e)
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
                var date = (DateTime)(dateNgayPSinhDLieu.EditValue);
                gridControl1.DataSource = GetDataPrevMonth(cbxDviPsinhDLieu.EditValue.ToString(), date.ToString("yyyyMMdd"));
                btnCreateReport.Enabled = true;
                SplashScreenManager.CloseDefaultWaitForm();
            }
            catch (Exception ex)
            {
                EasyDialog.ShowErrorDialog(ex.Message);
            }
        }

        private IEnumerable<clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan> GetDataPrevMonth(string maChiNhanh, string ngayDuLieu)
        {
            _clsBaoCaoTinhHinhHuyDongVon = new clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan();
            return _clsBaoCaoTinhHinhHuyDongVon.GetAllData(maChiNhanh, ngayDuLieu);
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
            string maChiNhanh = cbxDviPsinhDLieu.EditValue.ToString();
            string ngayDuLieu = ((DateTime)dateNgayPSinhDLieu.EditValue).ToString("yyyyMMdd");
            var clsTinhHinhHuyDongTienGui = gridControl1.DataSource as IEnumerable<clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan>;
            var listTienGui = new List<clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan>();

            //-------------------------------------------------------------------------------------------
            exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
            SplashScreenManager.CloseDefaultWaitForm();
            this.Close();
            this.Dispose();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
    }
}