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
    /// Interaction logic for A00034.xaml
    /// </summary>
    public partial class A00034 : Window
    {
        private const string _reportName = "A00034";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK _clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK;
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public A00034()
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
                var fileTemplate = new FileInfo($"{System.AppDomain.CurrentDomain.BaseDirectory}\\temp\\reports\\{_reportName}.xlsx");
                var exPackage = new ExcelPackage(newFile, fileTemplate);
                string _ngayBaoCao = _createFileNameReport.NgayBaoCao(dateNgayPSinhDLieu);
                var excelSheet = exPackage.Workbook.Worksheets[1];
                _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                   clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);
                var duNoTinDung = _clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK.GetAllData(maChiNhanh, denNgay);
                var mucDich01 = new[] { "01", "02", "10", "11" };
                var mucDich06 = new[] { "09" };
                var mucDich07 = new[] { "03" };
                var mucDich16 = new[] { "05" };
                var mucDich17 = new[] { "06", "12" };
                var mucDich19 = new[] { "04", "07", "08", "13", "99", "100", "101", "MUC_DICH_VAY_SXKD", "MUC_DICH_VAY_SXNN", "MUC_DICH_VAY_TDCN", "MUC_DICH_VAY_HSSV" };
                var duNoNganHan01 = (from a in duNoTinDung where mucDich01.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY <= 12 select a.SO_DU).Sum();
                var duNoNganHan06 = (from a in duNoTinDung where mucDich06.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY <= 12 select a.SO_DU).Sum();
                var duNoNganHan07 = (from a in duNoTinDung where mucDich07.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY <= 12 select a.SO_DU).Sum();
                var duNoNganHan16 = (from a in duNoTinDung where mucDich16.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY <= 12 select a.SO_DU).Sum();
                var duNoNganHan17 = (from a in duNoTinDung where mucDich17.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY <= 12 select a.SO_DU).Sum();
                var duNoNganHan19 = (from a in duNoTinDung where mucDich19.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY <= 12 select a.SO_DU).Sum();

                var duNoTDH01 = (from a in duNoTinDung where mucDich01.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY > 12 select a.SO_DU).Sum();
                var duNoTDH06 = (from a in duNoTinDung where mucDich06.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY > 12 select a.SO_DU).Sum();
                var duNoTDH07 = (from a in duNoTinDung where mucDich07.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY > 12 select a.SO_DU).Sum();
                var duNoTDH16 = (from a in duNoTinDung where mucDich16.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY > 12 select a.SO_DU).Sum();
                var duNoTDH17 = (from a in duNoTinDung where mucDich17.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY > 12 select a.SO_DU).Sum();
                var duNoTDH19 = (from a in duNoTinDung where mucDich19.Contains(a.MUC_DICH_VAY) && a.TGIAN_VAY > 12 select a.SO_DU).Sum();

                var laiDuThu01 = (from a in duNoTinDung where mucDich01.Contains(a.MUC_DICH_VAY) select a.LAI_DU_THU).Sum();
                var laiDuThu06 = (from a in duNoTinDung where mucDich06.Contains(a.MUC_DICH_VAY) select a.LAI_DU_THU).Sum();
                var laiDuThu07 = (from a in duNoTinDung where mucDich07.Contains(a.MUC_DICH_VAY) select a.LAI_DU_THU).Sum();
                var laiDuThu16 = (from a in duNoTinDung where mucDich16.Contains(a.MUC_DICH_VAY) select a.LAI_DU_THU).Sum();
                var laiDuThu17 = (from a in duNoTinDung where mucDich17.Contains(a.MUC_DICH_VAY) select a.LAI_DU_THU).Sum();
                var laiDuThu19 = (from a in duNoTinDung where mucDich19.Contains(a.MUC_DICH_VAY) select a.LAI_DU_THU).Sum();

                var tongLaiDuThu = laiDuThu01 + laiDuThu06 + laiDuThu07 + laiDuThu16 + laiDuThu17 + laiDuThu19;
                var tongDuNoNganHan = duNoNganHan01 + duNoNganHan06 + duNoNganHan07 + duNoNganHan16 + duNoNganHan17 + duNoNganHan19;
                var tongDuNoTDH = duNoTDH01 + duNoTDH06 + duNoTDH07 + duNoTDH16 + duNoTDH17 + duNoTDH19;
                excelSheet.Cells["D20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", Math.Round(duNoNganHan01 / dviTinh, 2));
                excelSheet.Cells["D25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", Math.Round(duNoNganHan06 / dviTinh, 2));
                excelSheet.Cells["D26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(duNoNganHan07 / dviTinh, 2));
                excelSheet.Cells["D35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(duNoNganHan16 / dviTinh, 2));
                excelSheet.Cells["D36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(duNoNganHan17 / dviTinh, 2));
                excelSheet.Cells["D38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(duNoNganHan19 / dviTinh, 2));
                excelSheet.Cells["D41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tongDuNoNganHan / dviTinh, 2));

                excelSheet.Cells["H20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(duNoTDH01 / dviTinh, 2));
                excelSheet.Cells["H25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", Math.Round(duNoTDH06 / dviTinh, 2));
                excelSheet.Cells["H26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(duNoTDH07 / dviTinh, 2));
                excelSheet.Cells["H35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(duNoTDH16 / dviTinh, 2));
                excelSheet.Cells["H36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(duNoTDH17 / dviTinh, 2));
                excelSheet.Cells["H38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(duNoTDH19 / dviTinh, 2));
                excelSheet.Cells["H41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tongDuNoTDH / dviTinh, 2));

                excelSheet.Cells["L20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", Math.Round((duNoTDH01 + duNoNganHan01) / dviTinh, 2));
                excelSheet.Cells["L25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", Math.Round((duNoTDH06 + duNoNganHan06) / dviTinh, 2));
                excelSheet.Cells["L26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((duNoTDH07 + duNoNganHan07) / dviTinh, 2));
                excelSheet.Cells["L35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((duNoTDH16 + duNoNganHan16) / dviTinh, 2));
                excelSheet.Cells["L36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((duNoTDH17 + duNoNganHan17) / dviTinh, 2));
                excelSheet.Cells["L38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((duNoTDH19 + duNoNganHan19) / dviTinh, 2));
                excelSheet.Cells["L41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((tongDuNoNganHan + tongDuNoTDH) / dviTinh, 2));

                excelSheet.Cells["M20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                         "{0:00.00}", Math.Round(laiDuThu01 / dviTinh, 2));
                excelSheet.Cells["M25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", Math.Round(laiDuThu06 / dviTinh, 2));
                excelSheet.Cells["M26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(laiDuThu07 / dviTinh, 2));
                excelSheet.Cells["M35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(laiDuThu16 / dviTinh, 2));
                excelSheet.Cells["M36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(laiDuThu17 / dviTinh, 2));
                excelSheet.Cells["M38"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(laiDuThu19 / dviTinh, 2));
                excelSheet.Cells["M41"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tongLaiDuThu / dviTinh, 2));
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