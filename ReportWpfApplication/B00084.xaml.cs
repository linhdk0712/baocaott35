using ReportApplication.Common;
using ReportWpfApplication.Common;
using System;
using System.Windows;

namespace ReportWpfApplication
{
    /// <summary>
    /// Interaction logic for B00084.xaml
    /// </summary>
    public partial class B00084 : Window
    {
        private const string _reportName = "B00084";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();

        //private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;

        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public B00084()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _clsWPFDanhMucChiNhanh = new clsWPF(cbxDviPsinhDLieu);
            _clsWPFDanhMucChiNhanh.GetDanhMucChiNhanh();
            cbxDviPsinhDLieu.SelectedIndex = 0;
            dateNgayPSinhDLieu.SelectedDate = DateTime.Now;
        }
    }
}