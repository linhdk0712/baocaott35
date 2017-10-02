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
    /// Interaction logic for B00094.xaml
    /// </summary>
    public partial class B00094 : Window
    {
        private const string _reportName = "B00094";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public B00094()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            EasyDialog.ShowInfoDialog("Chức năng này đã được sửa đổi. Hãy chọn chức năng Tạo báo cáo");
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            string ngayDuLieu = ((DateTime)dateNgayPSinhDLieu.SelectedDate).ToString("yyyyMMdd");
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
                var newFile = new FileInfo(saveFileDialog.FileName);
                var fileTemplate = new FileInfo($"{AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
                var exPackage = new ExcelPackage(newFile, fileTemplate);
                var excelSheet = exPackage.Workbook.Worksheets[1];
                string _ngayBaoCao = _createFileNameReport.NgayBaoCao(dateNgayPSinhDLieu);
                _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                   clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);
                var _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
                var bangCanDoi = _clsBangCanDoiTaiKhoanKeToan.GetAllData(maChiNhanh,ngayDuLieu);
                var noTK801 = (from a in bangCanDoi where a.F03 == "801" select a.F06).FirstOrDefault();
                var coTK801 = (from a in bangCanDoi where a.F03 == "801" select a.F07).FirstOrDefault();
                var coDauKyTK423 = (from a in bangCanDoi where a.F03 == "423" select a.F05).FirstOrDefault();
                var coCuoiKyTK423 = (from a in bangCanDoi where a.F03 == "423" select a.F09).FirstOrDefault();
                var laiSuatTienGuiBinhQuan = Math.Round(((noTK801 - coTK801) / ((coDauKyTK423 + coCuoiKyTK423) / 2)) * 100 * 12, 2);

                var coTK702 = (from a in bangCanDoi where a.F03 == "702" select a.F07).FirstOrDefault();
                var noTK702 = (from a in bangCanDoi where a.F03 == "702" select a.F06).FirstOrDefault();
                var noDauKy21 = (from a in bangCanDoi where a.F03 == "21" select a.F04).FirstOrDefault();
                var noCuoiKy21 = (from a in bangCanDoi where a.F03 == "21" select a.F08).FirstOrDefault();
                var coTK7022 = (from a in bangCanDoi where a.F03 == "7022" select a.F07).FirstOrDefault();
                var noTK7022 = (from a in bangCanDoi where a.F03 == "7022" select a.F06).FirstOrDefault();

                var laiSuatChoVayBinhQuan = Math.Round(((coTK702 - noTK702) - (coTK7022 - noTK7022)) / ((noDauKy21 + noCuoiKy21) / 2) * 100 * 12, 2);
                excelSheet.Cells["D19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", laiSuatTienGuiBinhQuan);
                excelSheet.Cells["E19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", laiSuatChoVayBinhQuan);
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