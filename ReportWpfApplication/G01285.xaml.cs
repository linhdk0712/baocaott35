using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.Common;
using ReportWpfApplication.Common;
using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace ReportWpfApplication
{
    /// <summary>
    /// Interaction logic for G01285.xaml
    /// </summary>
    public partial class G01285 : Window
    {
        private const string _reportName = "G01285";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public G01285()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
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
            excelSheet.Cells["D18"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                       "{0:00.00}", txtName.Text.Trim());
            excelSheet.Cells["D19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                       "{0:00.00}", txtMa.Text.Trim());
            excelSheet.Cells["D20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                     "{0:00.00}", txtDiaChi.Text.Trim());
            excelSheet.Cells["D21"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                     "{0:00.00}", txtDienThoai.Text.Trim());
            excelSheet.Cells["D22"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                     "{0:00.00}", txtGiayPhep.Text.Trim());
            excelSheet.Cells["D23"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                     "{0:00.00}", dateCapPhep.Text.Trim());
            excelSheet.Cells["D24"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", dateHoatDong.Text.Trim());
            excelSheet.Cells["D25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", txtVonDieuLe.Text.Trim());
            excelSheet.Cells["D26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", txtTangVonDieuLe.Text.Trim());
            excelSheet.Cells["D27"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", dateTangVonDieuLe.Text.Trim());
            excelSheet.Cells["D28"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", txtChiNhanh.Text.Trim());
            excelSheet.Cells["D29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", txtTangGiamChiNhanh.Text.Trim());
            excelSheet.Cells["D30"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", txtPGD.Text.Trim());
            excelSheet.Cells["D31"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", txtTangGiamPGD.Text.Trim());
            excelSheet.Cells["D32"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", txtThanhVien.Text.Trim());
            excelSheet.Cells["D33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", txtTangGiamTV.Text.Trim());
            excelSheet.Cells["D34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", txtKhachHang.Text.Trim());
            excelSheet.Cells["D35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", txtTangGiamKhachHang.Text.Trim());
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
            txtName.Text = "Tổ chức tài chính vi mô TNHH M7";
            txtMa.Text = "01912001";
            txtDiaChi.Text = "Tầng 2 Căn nhà lô A9/D5 KĐT Cầu Giấy, Dịch Vọng Hậu, Cầu Giấy, Hà Nội ";
            txtDienThoai.Text = "0473036688";
        }
    }
}