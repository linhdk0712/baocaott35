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
    /// Interaction logic for A00054.xaml
    /// </summary>
    public partial class A00054 : Window
    {
        private const string _reportName = "A00054";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK _clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK;
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public A00054()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
            _clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK = new clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK();
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            EasyDialog.ShowInfoDialog("Chức năng này đã được sửa đổi. Hãy chọn chức năng Tạo báo cáo");
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            string denNgay = ((DateTime)dateNgayPSinhDLieu.SelectedDate).ToString("yyyyMMdd");
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
                var duNoTinDung = _clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK.GetAllData(maChiNhanh, denNgay);
                var duNoNganHan01 = (from a in duNoTinDung where a.TGIAN_VAY <= 12 select a.SO_DU).Sum();
                var duNoTDH01 = (from a in duNoTinDung where a.TGIAN_VAY > 12 select a.SO_DU).Sum();
                var laiDuThu01 = (from a in duNoTinDung select a.LAI_DU_THU).Sum();
                excelSheet.Cells["D30"].Value = excelSheet.Cells["D33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", Math.Round(duNoNganHan01 / dviTinh, 2));
                excelSheet.Cells["H30"].Value = excelSheet.Cells["H33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", Math.Round(duNoTDH01 / dviTinh, 2));
                excelSheet.Cells["L30"].Value = excelSheet.Cells["L33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((duNoTDH01 + duNoNganHan01) / dviTinh, 2));
                excelSheet.Cells["M30"].Value = excelSheet.Cells["M33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(laiDuThu01 / dviTinh, 2));
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