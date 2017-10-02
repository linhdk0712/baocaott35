using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.Common;
using ReportWpfApplication.Common;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace ReportWpfApplication
{
    /// <summary>
    /// Interaction logic for G01927.xaml
    /// </summary>
    public partial class G01927 : Window
    {
        private const string _reportName = "G01927";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public G01927()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            EasyDialog.ShowWarningDialog("Phần mềm chỉ hỗ trợ tạo tên file báo cáo. Nội dung vui lòng tự hoàn thiện");
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
            //var fs = File.OpenRead($"{System.AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
            var newFile = new FileInfo(saveFileDialog.FileName);
            var fileTemplate = new FileInfo($"{System.AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
            var exPackage = new ExcelPackage(newFile, fileTemplate);
            var excelSheet = exPackage.Workbook.Worksheets[1];
            string _ngayBaoCao = _createFileNameReport.NgayBaoCao(dateNgayPSinhDLieu);
            _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
            clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);
            exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
            SplashScreenManager.CloseDefaultWaitForm();
            if (EasyDialog.ShowYesNoDialog("Tạo báo cáo thành công. Bạn có muốn tiếp tục không ?") != System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();            
            }
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