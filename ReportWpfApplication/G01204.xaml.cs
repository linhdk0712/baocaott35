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
    /// Interaction logic for G01204.xaml
    /// </summary>
    public partial class G01204 : Window, IDisposable
    {
        private const string _reportName = "G01204";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly ClsBaoCaoPhanLoaiNoVaTrichLap _clsPhanLoaiNoVaTrichLap;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        private clsWPF _clsWPFDanhMucChiNhanh;
        public int iDinhKyBaoCao { get; set; }

        public void Dispose()
        {
            if (_clsPhanLoaiNoVaTrichLap != null)
                _clsPhanLoaiNoVaTrichLap.Dispose();
        }

        public G01204()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
            _clsPhanLoaiNoVaTrichLap = new ClsBaoCaoPhanLoaiNoVaTrichLap();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _clsWPFDanhMucChiNhanh = new clsWPF(cbxDviPsinhDLieu);
            _clsWPFDanhMucChiNhanh.GetDanhMucChiNhanh();
            dateNgayPSinhDLieu.SelectedDate = DateTime.Now;
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            EasyDialog.ShowInfoDialog("Chức năng này đã được sửa đổi. Hãy chọn chức năng Tạo báo cáo");
        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            var dateTime = (DateTime)(dateNgayPSinhDLieu.SelectedDate);
            var denNgay = dateTime.ToString("yyyyMMdd");
            clsSoLieuToanHeThongVaDonVi _clsSoLieuToanHeThongVaDonVi = new clsSoLieuToanHeThongVaDonVi();
            var _phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            foreach (var item in _phamViBaoCao)
            {
                cbxDviPsinhDLieu.SelectedValue = item.MA_DVI;
                string maChiNhanh = cbxDviPsinhDLieu.SelectedValue.ToString();
                //---------------------------------------------------
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
                var result = _clsPhanLoaiNoVaTrichLap.GetAllData(cbxDviPsinhDLieu.SelectedValue.ToString(), denNgay);
                var duNoNhom1 = result.Sum(p => p.DU_NO_NHOM_1);
                var duNoNhom2 = result.Sum(p => p.DU_NO_NHOM_2);
                var duNoNhom3 = result.Sum(p => p.DU_NO_NHOM_3);
                var duNoNhom4 = result.Sum(p => p.DU_NO_NHOM_4);
                var duNoNhom5 = result.Sum(p => p.DU_NO_NHOM_5);
                //-----------------------------------------------------
                var duPhongCuThe2 = duNoNhom2 * (decimal)0.02;
                var duPhongCuThe3 = duNoNhom3 * (decimal)0.25;
                var duPhongCuThe4 = duNoNhom4 * (decimal)0.5;
                var duPhongCuThe5 = duNoNhom5 * 1;
                var tongDuPhongCuThe = duPhongCuThe2 + duPhongCuThe3 + duPhongCuThe4 + duPhongCuThe5;
                //-----------------------------------------------------
                var duPhongChung1 = Math.Round(duNoNhom1 * (decimal)0.005, 1);
                var duPhongChung2 = Math.Round(duNoNhom2 * (decimal)0.005, 1);
                var duPhongChung3 = Math.Round(duNoNhom3 * (decimal)0.005, 1);
                var duPhongChung4 = Math.Round(duNoNhom4 * (decimal)0.005, 1);
                var tongDuPhongChung = duPhongChung1 + duPhongChung2 + duPhongChung3 + duPhongChung4;
                //-----------------------------------------------------
                var tongDuNo = duNoNhom1 + duNoNhom2 + duNoNhom3 + duNoNhom4 + duNoNhom5;
                var tongNoXau = duNoNhom3 + duNoNhom4 + duNoNhom5;
                decimal tyLeNoXau = 0;
                if (tongDuNo > 0)
                {
                    tyLeNoXau = Math.Round((tongNoXau / tongDuNo) * 100, 1);
                }
                //-----------------------------------------------------
                excelSheet.Cells["D18"].Value = excelSheet.Cells["D19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", (duNoNhom1 / dviTinh));
                excelSheet.Cells["D23"].Value = excelSheet.Cells["D24"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", (duNoNhom2 / dviTinh));
                excelSheet.Cells["D28"].Value = excelSheet.Cells["D29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duNoNhom3 / dviTinh);
                excelSheet.Cells["D33"].Value = excelSheet.Cells["D34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duNoNhom4 / dviTinh);
                excelSheet.Cells["D38"].Value = excelSheet.Cells["D39"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duNoNhom5 / dviTinh);
                excelSheet.Cells["D43"].Value = excelSheet.Cells["D44"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", tongDuNo / dviTinh);
                //-----------------------------------------------------
                excelSheet.Cells["D49"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", tongNoXau / dviTinh);
                excelSheet.Cells["D50"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", tyLeNoXau);
                //-----------------------------------------------------
                excelSheet.Cells["F18"].Value = excelSheet.Cells["F19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duPhongChung1 / dviTinh);
                excelSheet.Cells["F23"].Value = excelSheet.Cells["F24"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duPhongChung2 / dviTinh);
                excelSheet.Cells["F28"].Value = excelSheet.Cells["F29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duPhongChung3 / dviTinh);
                excelSheet.Cells["F33"].Value = excelSheet.Cells["F34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duPhongChung4 / dviTinh);
                excelSheet.Cells["F43"].Value = excelSheet.Cells["F44"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", tongDuPhongChung / dviTinh);
                //-----------------------------------------------------
                excelSheet.Cells["E23"].Value = excelSheet.Cells["E24"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duPhongCuThe2 / dviTinh);
                excelSheet.Cells["E28"].Value = excelSheet.Cells["E29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duPhongCuThe3 / dviTinh);
                excelSheet.Cells["E33"].Value = excelSheet.Cells["E34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duPhongCuThe4 / dviTinh);
                excelSheet.Cells["E38"].Value = excelSheet.Cells["E39"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", duPhongCuThe5 / dviTinh);
                excelSheet.Cells["E43"].Value = excelSheet.Cells["E44"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", tongDuPhongCuThe / dviTinh);
                excelSheet.Cells["E49"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.0}", (duPhongCuThe3 + duPhongCuThe4 + duPhongCuThe5) / dviTinh);
                exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                SplashScreenManager.CloseDefaultWaitForm();
            }
            if (EasyDialog.ShowYesNoDialog("Tạo báo cáo thành công. Bạn có muốn tiếp tục không ?") != System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
                this.Dispose();
            }
        }
    }
}