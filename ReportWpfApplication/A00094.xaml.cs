using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWpfApplication.Common;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace ReportWpfApplication
{
    /// <summary>
    /// Interaction logic for A00094.xaml
    /// </summary>
    public partial class A00094 : Window
    {
        private const string _reportName = "A00094";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private ClsBaoCaoDoanhSoCapThuNoTinDung _clsBaoCaoDoanhSoCapThuNoTinDung;
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public A00094()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
            _clsBaoCaoDoanhSoCapThuNoTinDung = new ClsBaoCaoDoanhSoCapThuNoTinDung();
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            var ngayDuLieu = ((DateTime)(dateNgayPSinhDLieu.SelectedDate)).ToString("yyyyMMdd");
            using (_clsBaoCaoDoanhSoCapThuNoTinDung = new ClsBaoCaoDoanhSoCapThuNoTinDung())
            {
                grdReport.ItemsSource = _clsBaoCaoDoanhSoCapThuNoTinDung.GetAllData(ngayDuLieu);
            }
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            var ngayDuLieu = ((DateTime)(dateNgayPSinhDLieu.SelectedDate)).ToString("yyyyMMdd");
            clsSoLieuToanHeThongVaDonVi _clsSoLieuToanHeThongVaDonVi = new clsSoLieuToanHeThongVaDonVi();
            var _phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            foreach (var item in _phamViBaoCao)
            {
                cbxDviPsinhDLieu.SelectedValue = item.MA_DVI;
                string maChiNhanh = cbxDviPsinhDLieu.SelectedValue.ToString();
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
                var newFile = new FileInfo(saveFileDialog.FileName);
                var fileTemplate = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
                var exPackage = new ExcelPackage(newFile, fileTemplate);
                string _ngayBaoCao = _createFileNameReport.NgayBaoCao(dateNgayPSinhDLieu);
                var excelSheet = exPackage.Workbook.Worksheets[1];
                _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                   clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);
                var doanhSo = _clsBaoCaoDoanhSoCapThuNoTinDung.GetAllData(ngayDuLieu);
                var doanhSoCapTinDungNganHan = ((from a in doanhSo where a.TGIAN_VAY <= 12 select a.SO_TIEN_GIAI_NGAN).Sum() / dviTinh);
                var doanhSoCapTinDungTDH = ((from a in doanhSo where a.TGIAN_VAY > 12 select a.SO_TIEN_GIAI_NGAN).Sum() / dviTinh);
                var doanhSoThuNoNganHan = ((from a in doanhSo where a.TGIAN_VAY <= 12 select a.TT_TRA_GOC).Sum() / dviTinh);
                var doanhSoThuNoTDH = ((from a in doanhSo where a.TGIAN_VAY > 12 select a.TT_TRA_GOC).Sum() / dviTinh);
                var doanhSoCapTinDung = doanhSoCapTinDungNganHan + doanhSoCapTinDungTDH;
                var doanhSoThuNo = doanhSoThuNoNganHan + doanhSoThuNoTDH;
                excelSheet.Cells["D19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", doanhSoCapTinDungNganHan);
                excelSheet.Cells["D20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                         "{0:00.00}", doanhSoCapTinDungTDH);
                excelSheet.Cells["D21"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                         "{0:00.00}", doanhSoCapTinDung);

                excelSheet.Cells["H19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                         "{0:00.00}", doanhSoThuNoNganHan);
                excelSheet.Cells["H20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                         "{0:00.00}", doanhSoThuNoTDH);
                excelSheet.Cells["H21"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                         "{0:00.00}", doanhSoThuNo);
                exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                SplashScreenManager.CloseDefaultWaitForm();
            }

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