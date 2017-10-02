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
    /// Interaction logic for A00014.xaml
    /// </summary>
    public partial class A00014 : Window
    {
        private const string _reportName = "A00014";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public A00014()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
        }

        private string GetNgayDuLieu()
        {
            string ngayDuLieu = ((DateTime)dateNgayPSinhDLieu.SelectedDate).ToString("yyyyMMdd");
            return ngayDuLieu;
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ngayDuLieu = GetNgayDuLieu();
                string ngayDauThang = clsGetLastDayOfMonth.GetFirstDayOfMont(((DateTime)dateNgayPSinhDLieu.SelectedDate).Month, ((DateTime)dateNgayPSinhDLieu.SelectedDate).Year);
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
                    //var fs = File.OpenRead($"{System.AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
                    var newFile = new FileInfo(saveFileDialog.FileName);
                    var fileTemplate = new FileInfo($"{System.AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
                    var exPackage = new ExcelPackage(newFile, fileTemplate);
                    string _ngayBaoCao = _createFileNameReport.NgayBaoCao(dateNgayPSinhDLieu);
                    var excelSheet = exPackage.Workbook.Worksheets[1];
                    _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                       clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);
                    using (_clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan())
                    {
                        var bangCanDoi = _clsBangCanDoiTaiKhoanKeToan.GetAllData(maChiNhanh,ngayDuLieu);
                        var duTienGui = (from a in bangCanDoi where a.F03 == "42" select a.F09).FirstOrDefault();
                        excelSheet.Cells["D19"].Value = excelSheet.Cells["F19"].Value = excelSheet.Cells["D40"].Value = excelSheet.Cells["F40"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                                  "{0:00.00}", Math.Round(duTienGui / dviTinh, 2));
                        exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                        SplashScreenManager.CloseDefaultWaitForm();
                    }
                }
                if (EasyDialog.ShowYesNoDialog("Tạo báo cáo thành công. Bạn có muốn tiếp tục không ?") != System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                EasyDialog.ShowUnsuccessfulDialog(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _clsWPFDanhMucChiNhanh = new clsWPF(cbxDviPsinhDLieu);
                _clsWPFDanhMucChiNhanh.GetDanhMucChiNhanh();
                cbxDviPsinhDLieu.SelectedIndex = 0;
                dateNgayPSinhDLieu.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                EasyDialog.ShowUnsuccessfulDialog(ex.Message);
            }
        }
    }
}