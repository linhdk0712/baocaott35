using DevExpress.XtraSplashScreen;
using ReportApplication.BAL;
using ReportApplication.Common;
using System;
using System.Windows.Forms;

namespace ReportApplication
{
    public partial class G02824 : Form
    {
        private readonly clsFormInfo _clsFormInfo;
        private readonly string _reportName;
        private readonly clsSetFileNameReport _setFileName;
        private clsGetDanhMucChiNhanh _danhMucChiNhanh;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsCreateFileNameReport _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsBaoCaoThucHienTyLeAnToanVonRiengLe _clsBaoCaoAnToanVonRiengLe;
        private Form progressForm;

        /// <summary>
        ///
        /// </summary>
        public G02824()
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
            _danhMucChiNhanh.GetDanMucChiNhanh(cbxDviPSinhDLieu);
        }

        private void G02824_Load(object sender, EventArgs e)
        {
            GetDanhMucChiNhanh();
            btnCreateReport.Enabled = false;
            cbxDviPSinhDLieu.Enabled = false;
            dateNgayPSinhDLieu.EditValue = DateTime.Now;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm();
            GetData();
            SplashScreenManager.CloseDefaultWaitForm();
        }

        private void btnCreateReport_Click(object sender, EventArgs e)
        {
        }

        private void GetData()
        {
            try
            {
                _clsBaoCaoAnToanVonRiengLe = new clsBaoCaoThucHienTyLeAnToanVonRiengLe();
                var date = (DateTime)dateNgayPSinhDLieu.EditValue;
                string _tuNgay = clsGetLastDayOfMonth.GetFirstDayOfMont(date.Month, date.Year);
                string _denNgay = date.ToString("yyyyMMdd");
                gridControl1.DataSource = _clsBaoCaoAnToanVonRiengLe.GetAll(_tuNgay, _denNgay);
                btnCreateReport.Enabled = true;
            }
            catch (Exception ex)
            {
                EasyDialog.ShowErrorDialog(ex.Message);
            }
        }
    }
}