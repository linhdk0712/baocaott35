using Dapper;
using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWebApplication.Commons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportWebApplication.Controllers
{
    public class G02965Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly decimal _dviTinhs = ClsDonViTinh.DviTinhDong();
        private readonly ClsBaoCaoRuiRoThanhKhoan _clsBaoCaoRuiRoThanhKhoan;
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;
        public G02965Controller()
        {
            _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
            _clsBaoCaoRuiRoThanhKhoan = new ClsBaoCaoRuiRoThanhKhoan();
        }
        // GET: G02965
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var ngayDuLieu = (DateTime.Parse(denNgay)).ToString("yyyyMMdd");
            var dtFirstEndOfQuarter = clsDatesOfQuarter.DatesOfQuarter(denNgay);
            var endDateOfQuarter = dtFirstEndOfQuarter[1].ToString("yyyyMMdd");
            clsGetLastDayOfMonth.GetFirstDayOfMont(((DateTime.Parse(denNgay))).Month, (DateTime.Parse(denNgay)).Year);
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            //-------------------------------------------------------------------------------------------
            var param = new DynamicParameters();
            param.Add("@ngayDuLieu", ngayDuLieu);
            var tienGui = _clsBaoCaoRuiRoThanhKhoan.TaiDanhSachSoTienGui<ClsRuiRoThanhKhoan>(param);
            var bangCanDoiKeToan = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfQuarter);
            //-------------------------------------------------------------------------------------------
            foreach (var machinhanh in phamViBaoCao)
            {
                var maChiNhanh = machinhanh.MA_DVI;
                string ngayBaoCao;
                string maDviPsinhDlieu;
                string fileName;
                _createFileNameReport = new clsGetFileNameReportsWeb(dinhKyBaoCao);
                _createFileNameReport.GetFileNameReportMonthWpf(out ngayBaoCao, out maDviPsinhDlieu, out fileName,
                   maChiNhanh, _maDonViGui, _reportName, denNgay);
                var newFile = new FileInfo(fileName);
                var fileTemplate = new FileInfo(HttpContext.Server.MapPath($"~/Report/{_reportName}.xlsx"));
                var exPackage = new ExcelPackage(newFile, fileTemplate);
                var _ngayBaoCao = _createFileNameReport.NgayBaoCao(denNgay);
                var excelSheet = exPackage.Workbook.Worksheets[1];
                _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                  clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), _ngayBaoCao, _user, excelSheet);                
                //-------------------------------------------------------------------------------------------
                var tk10 = (from a in bangCanDoiKeToan where a.F03 == "10" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk11 = (from a in bangCanDoiKeToan where a.F03 == "11" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk13 = (from a in bangCanDoiKeToan where a.F03 == "13" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk2112 = (from a in bangCanDoiKeToan where a.F03 == "2112" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk2113 = (from a in bangCanDoiKeToan where a.F03 == "2113" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk2122 = (from a in bangCanDoiKeToan where a.F03 == "2122" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk2123 = (from a in bangCanDoiKeToan where a.F03 == "2123" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk2114 = (from a in bangCanDoiKeToan where a.F03 == "2114" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk2115 = (from a in bangCanDoiKeToan where a.F03 == "2115" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk2124 = (from a in bangCanDoiKeToan where a.F03 == "2124" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk2125 = (from a in bangCanDoiKeToan where a.F03 == "2125" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk2111 = (from a in bangCanDoiKeToan where a.F03 == "2111" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk21111 = (from a in bangCanDoiKeToan where a.F03 == "21111" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk2121 = (from a in bangCanDoiKeToan where a.F03 == "2121" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk35 = (from a in bangCanDoiKeToan where a.F03 == "35" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk36 = (from a in bangCanDoiKeToan where a.F03 == "36" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk366 = (from a in bangCanDoiKeToan where a.F03 == "366" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk31 = (from a in bangCanDoiKeToan where a.F03 == "31" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk38 = (from a in bangCanDoiKeToan where a.F03 == "38" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tk45 = (from a in bangCanDoiKeToan where a.F03 == "45" && a.F10 == maChiNhanh select a.F09).FirstOrDefault();
                var tk46 = (from a in bangCanDoiKeToan where a.F03 == "46" && a.F10 == maChiNhanh select a.F09).FirstOrDefault();
                var tk466 = (from a in bangCanDoiKeToan where a.F03 == "466" && a.F10 == maChiNhanh select a.F09).FirstOrDefault();
                var tk49 = (from a in bangCanDoiKeToan where a.F03 == "49" && a.F10 == maChiNhanh select a.F09).FirstOrDefault();
                var tk48 = (from a in bangCanDoiKeToan where a.F03 == "48" && a.F10 == maChiNhanh select a.F09).FirstOrDefault();
                var tkNo30 = (from a in bangCanDoiKeToan where a.F03 == "30" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var tkCo30 = (from a in bangCanDoiKeToan where a.F03 == "30" && a.F10 == maChiNhanh select a.F09).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var tienMatDaQuy = tk10;

                var tienGuiNhnn = tk11;

                var tienGuiToChucTinDung = tk13;

                var choVayQuaHanDen3Thang =tk2112+tk2113+tk2122+tk2123;

                var choVayQuaHanTren3Thang = tk2114 + tk2115 + tk2124 + tk2125;

                var choVayTrongHanTu3Den12Thang = tk2111-tk21111;

                var choVayTrongHan12Den60Thang = tk2121;

                var choVayTongCong = choVayTrongHanTu3Den12Thang + choVayTrongHan12Den60Thang + choVayQuaHanDen3Thang + choVayQuaHanTren3Thang;

                var taiSanCoDinhVaBds = tkNo30 - tkCo30;

                var taiSanCoKhacDen1Thang = tk35 + tk36 - tk366;

                var taiSanCoKhacTu3Den12Thang = tk31 + tk38;

                var taiSanCoKhacTongCong = taiSanCoKhacDen1Thang + taiSanCoKhacTu3Den12Thang; 

                var tongTaiSanQuaHan3Thang = choVayQuaHanDen3Thang;

                var tongTaiSanQuaHanTren3Thang = choVayQuaHanTren3Thang;

                var tongTaiSanTrongHanDen1Thang = tienMatDaQuy  + tienGuiToChucTinDung  + taiSanCoKhacDen1Thang;

                const int tongTaiSanTrongHanTu1Den3Thang = 0;

                var tongTaiSanTrongHanTuTren3Den12Thang = choVayTrongHanTu3Den12Thang + taiSanCoKhacTu3Den12Thang;

                var tongTaiSanTrongHan12Den60Thang = choVayTrongHan12Den60Thang;

                var tongTaiSanTren60Thang = taiSanCoDinhVaBds;

                var tongTaiSanTongCong = tongTaiSanQuaHan3Thang + tongTaiSanQuaHanTren3Thang + tongTaiSanTrongHanDen1Thang + tongTaiSanTrongHanTuTren3Den12Thang + tongTaiSanTrongHan12Den60Thang + tongTaiSanTren60Thang;

                var tienGuiDen1Thang = (from a in tienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN <= 1 select a.SO_TIEN).Sum() + (from a in tienGui where a.MA_NHOM_SP == "T02" select a.SO_TIEN).Sum();
                var tienGuiTu1Den3Thang = (from a in tienGui where a.KY_HAN >1 && a.KY_HAN <=3 select a.SO_TIEN).Sum();
                var tienGuiTuTren3Den12Thang = (from a in tienGui where a.KY_HAN > 3 && a.KY_HAN <= 12 select a.SO_TIEN).Sum();
                var tienGuiTuTren12Den60Thang = (from a in tienGui where a.KY_HAN > 12 && a.KY_HAN <= 60 select a.SO_TIEN).Sum() + (from a in tienGui where a.MA_NHOM_SP == "T01" select a.SO_TIEN).Sum();
                var tienGuiTren60Thang = (from a in tienGui where a.KY_HAN > 60 select a.SO_TIEN).Sum();
                var tienGuiTongCong = tienGuiDen1Thang + tienGuiTu1Den3Thang + tienGuiTuTren3Den12Thang + tienGuiTuTren12Den60Thang + tienGuiTren60Thang;

                var cacKhoanNoKhacDen1Thang = tk45 + tk46 - tk466 + tk49;
                var cacKhoanNoKhacTren3Den13Thang = tk48;

                const int noPhaiTraQuanHanDen3Thang = 0;
                const int noPhaiTraQuaHanTren3Thang = 0;
                var noPhaiTraTrongHanDen1Thang = tienGuiDen1Thang + cacKhoanNoKhacDen1Thang;
                var noPhaiTraTrongHanTu1Den3Thang = tienGuiTu1Den3Thang;
                var noPhaiTraTrongHanTuTren3Den12Thang = tienGuiTuTren3Den12Thang + cacKhoanNoKhacTren3Den13Thang;
                var noPhaiTraTrongHanTuTren12Den60Thang = tienGuiTuTren12Den60Thang;
                var noPhaiTraTrongHanTren60Thang = tienGuiTren60Thang;
                var noPhaiTraTongCong = noPhaiTraQuaHanTren3Thang + noPhaiTraQuanHanDen3Thang + noPhaiTraTrongHanDen1Thang + noPhaiTraTrongHanTu1Den3Thang + noPhaiTraTrongHanTuTren3Den12Thang + noPhaiTraTrongHanTuTren12Den60Thang + noPhaiTraTrongHanTren60Thang;

                var thanhKhoanRongQuaHanDen3Thang = tongTaiSanQuaHan3Thang - noPhaiTraQuanHanDen3Thang;
                var thanhKhoanRongQuaHanTren3Thang = tongTaiSanQuaHanTren3Thang - noPhaiTraQuaHanTren3Thang;
                var thanhKhoanRongTrongHanDen1Thang = tongTaiSanTrongHanDen1Thang - noPhaiTraTrongHanDen1Thang;
                var thanhKhoanRongTrongHanTu1Den3Thang = tongTaiSanTrongHanTu1Den3Thang - noPhaiTraTrongHanTu1Den3Thang;
                var thanhKhoanRongTrongHanTuTren3Den12Thang = tongTaiSanTrongHanTuTren3Den12Thang - noPhaiTraTrongHanTuTren3Den12Thang;
                var thanhKhoanRongTrongHanTu12Den60Thang = tongTaiSanTrongHan12Den60Thang - noPhaiTraTrongHanTuTren12Den60Thang;
                var thanhKhoanRongTrongHanTren60Thang = tongTaiSanTren60Thang - noPhaiTraTrongHanTren60Thang;
                var thanhKhoanRong = tongTaiSanTongCong - noPhaiTraTongCong;

                excelSheet.Cells["E20"].Value =Format(tienMatDaQuy) ;

                excelSheet.Cells["E21"].Value = Format(tienGuiNhnn);

                excelSheet.Cells["E22"].Value = Format(tienGuiToChucTinDung);

                excelSheet.Cells["C23"].Value = Format(choVayQuaHanDen3Thang);
                excelSheet.Cells["D23"].Value = Format(choVayQuaHanTren3Thang);
                excelSheet.Cells["G23"].Value = Format(choVayTrongHanTu3Den12Thang);
                excelSheet.Cells["H23"].Value = Format(choVayTrongHan12Den60Thang);

                excelSheet.Cells["I25"].Value = Format(taiSanCoDinhVaBds);

                excelSheet.Cells["E26"].Value = Format(taiSanCoKhacDen1Thang);
                excelSheet.Cells["G26"].Value = Format(taiSanCoKhacTu3Den12Thang);

                excelSheet.Cells["C27"].Value = Format(tongTaiSanQuaHan3Thang);
                excelSheet.Cells["D27"].Value = Format(tongTaiSanQuaHanTren3Thang);
                excelSheet.Cells["E27"].Value = Format(tongTaiSanTrongHanDen1Thang);
                excelSheet.Cells["F27"].Value = Format(tongTaiSanTrongHanTu1Den3Thang);
                excelSheet.Cells["G27"].Value = Format(tongTaiSanTrongHanTuTren3Den12Thang);
                excelSheet.Cells["H27"].Value = Format(tongTaiSanTrongHan12Den60Thang);
                excelSheet.Cells["I27"].Value = Format(tongTaiSanTren60Thang);

                excelSheet.Cells["E30"].Value = Format(tienGuiDen1Thang);
                excelSheet.Cells["F30"].Value = Format(tienGuiTu1Den3Thang);
                excelSheet.Cells["G30"].Value = Format(tienGuiTuTren3Den12Thang);
                excelSheet.Cells["H30"].Value = Format(tienGuiTuTren12Den60Thang);
                excelSheet.Cells["I30"].Value = Format(tienGuiTren60Thang);

                excelSheet.Cells["E32"].Value = Format(cacKhoanNoKhacDen1Thang);
                excelSheet.Cells["G32"].Value = Format(cacKhoanNoKhacTren3Den13Thang);

                excelSheet.Cells["C33"].Value = Format(noPhaiTraQuanHanDen3Thang);
                excelSheet.Cells["D33"].Value = Format(noPhaiTraQuaHanTren3Thang);
                excelSheet.Cells["E33"].Value = Format(noPhaiTraTrongHanDen1Thang);
                excelSheet.Cells["F33"].Value = Format(noPhaiTraTrongHanTu1Den3Thang);
                excelSheet.Cells["G33"].Value = Format(noPhaiTraTrongHanTuTren3Den12Thang);
                excelSheet.Cells["H33"].Value = Format(noPhaiTraTrongHanTuTren12Den60Thang);
                excelSheet.Cells["I33"].Value = Format(noPhaiTraTrongHanTren60Thang);

                excelSheet.Cells["C34"].Value = Format(thanhKhoanRongQuaHanDen3Thang);
                excelSheet.Cells["D34"].Value = Format(thanhKhoanRongQuaHanTren3Thang);
                excelSheet.Cells["E34"].Value = Format(thanhKhoanRongTrongHanDen1Thang);
                excelSheet.Cells["F34"].Value = Format(thanhKhoanRongTrongHanTu1Den3Thang);
                excelSheet.Cells["G34"].Value = Format(thanhKhoanRongTrongHanTuTren3Den12Thang);
                excelSheet.Cells["H34"].Value = Format(thanhKhoanRongTrongHanTu12Den60Thang);
                excelSheet.Cells["I34"].Value = Format(thanhKhoanRongTrongHanTren60Thang);

                excelSheet.Cells["J20"].Value = Format(tienMatDaQuy);
                excelSheet.Cells["J21"].Value = Format(tienGuiNhnn);
                excelSheet.Cells["J22"].Value = Format(tienGuiToChucTinDung);
                excelSheet.Cells["J23"].Value = Format(choVayTongCong);
                excelSheet.Cells["J25"].Value = Format(taiSanCoDinhVaBds);
                excelSheet.Cells["J26"].Value = Format(taiSanCoKhacTongCong);
                excelSheet.Cells["J27"].Value = Format(tongTaiSanTongCong);
                excelSheet.Cells["J30"].Value = Format(tienGuiTongCong);
                excelSheet.Cells["J32"].Value = Format(cacKhoanNoKhacDen1Thang+cacKhoanNoKhacTren3Den13Thang);
                excelSheet.Cells["J33"].Value = Format(noPhaiTraTongCong);
                excelSheet.Cells["J34"].Value = Format(thanhKhoanRong);
                //Write it back to the client
                var fileOnServer = Server.MapPath($"~/Temp/{folderName}/{fileName}.xlsx");
                exPackage.SaveAs(new FileInfo(fileOnServer));
                reportCount++;
            }
            if (reportCount == phamViBaoCao.Count())
            {
                status = true;
            }
            return Json(new
            {
                data = reportCount,
                status
            }, JsonRequestBehavior.AllowGet);
        }

        private string Format(decimal data)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", Math.Round(data / _dviTinhs, 1));
        }
    }
}