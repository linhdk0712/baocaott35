using DevExpress.XtraSplashScreen;
using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWpfApplication.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace ReportWpfApplication
{
    /// <summary>
    /// Interaction logic for G02824.xaml
    /// </summary>
    public partial class G02824 : Window, IDisposable
    {
        private const string _reportName = "G02824";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private clsBaoCaoThucHienTyLeAnToanVonRiengLe _clsBaoCaoThucHienTyLeAnToanVonRiengLe;
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public void Dispose()
        {
            if (_clsBaoCaoThucHienTyLeAnToanVonRiengLe != null)
            {
                _clsBaoCaoThucHienTyLeAnToanVonRiengLe.Dispose();
                _clsBaoCaoThucHienTyLeAnToanVonRiengLe = null;
            }
        }

        public G02824()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
            _clsBaoCaoThucHienTyLeAnToanVonRiengLe = new clsBaoCaoThucHienTyLeAnToanVonRiengLe();
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
                string maChiNhanh = cbxDviPsinhDLieu.SelectedValue.ToString();
                string ngayDuLieu = GetNgayDuLieu();
                string ngayDauThang = clsGetLastDayOfMonth.GetFirstDayOfMont(((DateTime)dateNgayPSinhDLieu.SelectedDate).Month, ((DateTime)dateNgayPSinhDLieu.SelectedDate).Year);
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
                var clsTyLeAnToanVonRiengLe = _clsBaoCaoThucHienTyLeAnToanVonRiengLe.GetAll(ngayDauThang, ngayDuLieu);
                var tk60 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "60").Select(x => x.F09).SingleOrDefault();
                var tk61 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "61").Select(x => x.F09).SingleOrDefault();
                var vonCap1 = tk60 + tk61;
                var vonCap2 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "2192").Select(x => x.F09).SingleOrDefault();
                var vonTuCo = vonCap1 + vonCap2;
                var tienMat = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "10").Select(x => x.F08).SingleOrDefault();
                var tienGuiNHTM = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "13").Select(x => x.F08).SingleOrDefault();
                var tk21 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "21").Select(x => x.F08).SingleOrDefault();
                var tk219 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "219").Select(x => x.F09).SingleOrDefault();
                var tkNo30 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "30").Select(x => x.F08).SingleOrDefault();
                var tkCo30 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "30").Select(x => x.F09).SingleOrDefault();
                var tk31 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "31").Select(x => x.F08).SingleOrDefault();
                var tk35 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "35").Select(x => x.F08).SingleOrDefault();
                var tk36 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "36").Select(x => x.F08).SingleOrDefault();
                var tk366 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "366").Select(x => x.F08).SingleOrDefault();
                var tk38 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "38").Select(x => x.F08).SingleOrDefault();
                var tk39 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "39").Select(x => x.F08).SingleOrDefault();
                var duNoKhachHangTKQD = _clsBaoCaoThucHienTyLeAnToanVonRiengLe.GetDuNoKhachHangTKQD(GetNgayDuLieu());
                var dunoKhachHangTKTN = _clsBaoCaoThucHienTyLeAnToanVonRiengLe.GetDuNoKhachHangTKTN(GetNgayDuLieu());
                var duNoDamBaoBangTienGui = 2 * duNoKhachHangTKQD + dunoKhachHangTKTN;
                var duNoChoVayKhachHang = tk21 - tk219 - duNoDamBaoBangTienGui;
                var taiSanCoKhac = tkNo30 - tkCo30 + tk31 + tk35 + tk36 - tk366 + tk38 + tk39;
                var taiSanRuiRo0 = tienMat + duNoDamBaoBangTienGui;
                var taiSanRuiRo20 = tienGuiNHTM;
                var taiSanRuiRo50 = 0;
                var taiSanRuiRo100 = duNoChoVayKhachHang + taiSanCoKhac;
                var tyle0 = taiSanRuiRo0 * 0;
                var tyle20 = taiSanRuiRo20 * (decimal)0.2;
                var tyle50 = taiSanRuiRo50 * (decimal)0.5;
                var tyle100 = taiSanRuiRo100;
                var giaTriTaiSanCoRuiRoNoiBangQuyDoi = tyle0 + tyle20 + tyle50 + tyle100;
                var giaTriTaiSanCoRuiRoNoiBang = taiSanRuiRo0 + taiSanRuiRo20 + taiSanRuiRo100 + taiSanRuiRo50;
                var tyLeAnToanVonToiThieu = Math.Round(vonTuCo / giaTriTaiSanCoRuiRoNoiBangQuyDoi * 100, 1);
                excelSheet.Cells["D18"].Value = clsFormatString.FormatStringTyLePhanTram(tyLeAnToanVonToiThieu);
                excelSheet.Cells["D19"].Value = clsFormatString.FormatStringDviTinhTrieuDong(vonTuCo);
                excelSheet.Cells["D20"].Value = clsFormatString.FormatStringDviTinhTrieuDong(vonCap1);
                excelSheet.Cells["D21"].Value = clsFormatString.FormatStringDviTinhTrieuDong(vonCap2);
                excelSheet.Cells["D23"].Value = clsFormatString.FormatStringDviTinhTrieuDong(giaTriTaiSanCoRuiRoNoiBang);
                excelSheet.Cells["D24"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanRuiRo0);
                excelSheet.Cells["D25"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tienMat);
                excelSheet.Cells["D27"].Value = clsFormatString.FormatStringDviTinhTrieuDong(duNoDamBaoBangTienGui);
                excelSheet.Cells["D30"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanRuiRo20);
                excelSheet.Cells["D31"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tienGuiNHTM);
                excelSheet.Cells["D34"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanRuiRo50);
                excelSheet.Cells["D37"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanRuiRo100);
                excelSheet.Cells["D38"].Value = clsFormatString.FormatStringDviTinhTrieuDong(duNoChoVayKhachHang);
                excelSheet.Cells["D39"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanCoKhac);
                excelSheet.Cells["E23"].Value = clsFormatString.FormatStringDviTinhTrieuDong(giaTriTaiSanCoRuiRoNoiBangQuyDoi);
                excelSheet.Cells["E24"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanRuiRo0 * 0);
                excelSheet.Cells["E25"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tienMat * 0);
                excelSheet.Cells["E27"].Value = clsFormatString.FormatStringDviTinhTrieuDong(duNoDamBaoBangTienGui * 0);
                excelSheet.Cells["E30"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tyle20);
                excelSheet.Cells["E31"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tienGuiNHTM * (decimal)0.2);
                excelSheet.Cells["E34"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tyle50);
                excelSheet.Cells["E37"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tyle100);
                excelSheet.Cells["E38"].Value = clsFormatString.FormatStringDviTinhTrieuDong(duNoChoVayKhachHang * 1);
                excelSheet.Cells["E39"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanCoKhac * 1);
                exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                SplashScreenManager.CloseDefaultWaitForm();
                if (EasyDialog.ShowYesNoDialog("Tạo báo cáo thành công. Bạn có muốn tiếp tục không ?") != System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                EasyDialog.ShowErrorDialog(ex.Message);
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