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
    /// Interaction logic for G01304.xaml
    /// </summary>
    public partial class G01304 : Window, IDisposable
    {
        private const string _reportName = "G01304";
        private readonly clsSetFileNameReport _setFileName;
        private readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        private clsGetFileNameReports _createFileNameReport;
        private readonly string _user = clsUserCheckReport.IdUser;
        private readonly decimal dviTinh = ClsDonViTinh.DviTinhDong();
        private clsWPF _clsWPFDanhMucChiNhanh;
        private ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;
        public int iDinhKyBaoCao { get; set; }

        public void Dispose()
        {
            if (_clsBangCanDoiTaiKhoanKeToan != null)
            {
                _clsBangCanDoiTaiKhoanKeToan.Dispose();
                _clsBangCanDoiTaiKhoanKeToan = null;
            }
        }

        public G01304()
        {
            InitializeComponent();
            _setFileName = new clsSetFileNameReport();
            _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            string ngayDuLieu = GetNgayDuLieu();
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
                var excelSheet = exPackage.Workbook.Worksheets[1];
                string _ngayBaoCao = _createFileNameReport.NgayBaoCao(dateNgayPSinhDLieu);
                _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);
                ExcelRange ranger = excelSheet.Cells["D19:D324"];
                int startRow = 19;
                var clsG01304 = _clsBangCanDoiTaiKhoanKeToan.GetAllData(maChiNhanh,ngayDuLieu);
                for (int i = 0; i < ranger.Count(); i++)
                {
                    var f04 = (from a in clsG01304 where a.F03 == excelSheet.Cells["D" + startRow].Value.ToString().Trim() select a.F04).FirstOrDefault();
                    var f05 = (from a in clsG01304 where a.F03 == excelSheet.Cells["D" + startRow].Value.ToString().Trim() select a.F05).FirstOrDefault();
                    var f06 = (from a in clsG01304 where a.F03 == excelSheet.Cells["D" + startRow].Value.ToString().Trim() select a.F06).FirstOrDefault();
                    var f07 = (from a in clsG01304 where a.F03 == excelSheet.Cells["D" + startRow].Value.ToString().Trim() select a.F07).FirstOrDefault();
                    var f08 = (from a in clsG01304 where a.F03 == excelSheet.Cells["D" + startRow].Value.ToString().Trim() select a.F08).FirstOrDefault();
                    var f09 = (from a in clsG01304 where a.F03 == excelSheet.Cells["D" + startRow].Value.ToString().Trim() select a.F09).FirstOrDefault();
                    excelSheet.Cells["E" + startRow].Value = Format(f04);
                    excelSheet.Cells["F" + startRow].Value = Format(f05);
                    excelSheet.Cells["G" + startRow].Value = Format(f06);
                    excelSheet.Cells["H" + startRow].Value = Format(f07);
                    excelSheet.Cells["I" + startRow].Value = Format(f08);
                    excelSheet.Cells["J" + startRow].Value = Format(f09);
                    startRow++;
                }
                // Chỉnh sửa cột tổng cộng
                var tkTongCongf04 = (from a in clsG01304 where a.F03 == "Không" select a.F04).FirstOrDefault();
                var tkTongCongf05 = (from a in clsG01304 where a.F03 == "Không" select a.F05).FirstOrDefault();
                var tkTongCongf06 = (from a in clsG01304 where a.F03 == "Không" select a.F06).FirstOrDefault();
                var tkTongCongf07 = (from a in clsG01304 where a.F03 == "Không" select a.F07).FirstOrDefault();
                var tkTongCongf08 = (from a in clsG01304 where a.F03 == "Không" select a.F08).FirstOrDefault();
                var tkTongCongf09 = (from a in clsG01304 where a.F03 == "Không" select a.F09).FirstOrDefault();
                //---------
                var tk5f04 = (from a in clsG01304 where a.F03 == "5" select a.F04).FirstOrDefault();
                var tk5f05 = (from a in clsG01304 where a.F03 == "5" select a.F05).FirstOrDefault();
                var tk5f06 = (from a in clsG01304 where a.F03 == "5" select a.F06).FirstOrDefault();
                var tk5f07 = (from a in clsG01304 where a.F03 == "5" select a.F07).FirstOrDefault();
                var tk5f08 = (from a in clsG01304 where a.F03 == "5" select a.F08).FirstOrDefault();
                var tk5f09 = (from a in clsG01304 where a.F03 == "5" select a.F09).FirstOrDefault();
                //---------
                excelSheet.Cells["E324"].Value = Format(tkTongCongf04 - tk5f04);
                excelSheet.Cells["F324"].Value = Format(tkTongCongf05 - tk5f05);
                excelSheet.Cells["G324"].Value = Format(tkTongCongf06 - tk5f06);
                excelSheet.Cells["H324"].Value = Format(tkTongCongf07 - tk5f07);
                excelSheet.Cells["I324"].Value = Format(tkTongCongf08 - tk5f08);
                excelSheet.Cells["J324"].Value = Format(tkTongCongf09 - tk5f09);
                exPackage.SaveAs(new FileInfo(saveFileDialog.FileName));
                SplashScreenManager.CloseDefaultWaitForm();
            }

            if (EasyDialog.ShowYesNoDialog("Tạo báo cáo thành công. Bạn có muốn tiếp tục không ?") != System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
                this.Dispose();
            }
        }

        private string Format(decimal data)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", Math.Round(data / dviTinh, 1));
        }

        private string GetNgayDuLieu()
        {
            string ngayDuLieu = ((DateTime)dateNgayPSinhDLieu.SelectedDate).ToString("yyyyMMdd");
            return ngayDuLieu;
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            EasyDialog.ShowInfoDialog("Chức năng này đã được sửa đổi. Hãy chọn chức năng Tạo báo cáo");
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