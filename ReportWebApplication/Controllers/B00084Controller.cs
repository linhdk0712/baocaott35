using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWebApplication.Commons;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ReportWebApplication.Controllers
{
    public class B00084Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly clsBaoCaoLaiSuatVoiNenKinhTe _clsBaoCaoLaiSuatVoiNenKinhTe;
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;

        // GET: B00084
        public B00084Controller()
        {
            _clsBaoCaoLaiSuatVoiNenKinhTe = new clsBaoCaoLaiSuatVoiNenKinhTe();
            _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
        }

        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var ngayDuLieu = (DateTime.Parse(denNgay)).ToString("yyyyMMdd");
            var ngayDauThang = clsGetLastDayOfMonth.GetFirstDayOfMont(((DateTime.Parse(denNgay))).Month, (DateTime.Parse(denNgay)).Year);
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var _phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            //-------------------------------
            var laiSuatTietKiem = _clsBaoCaoLaiSuatVoiNenKinhTe.GetLaiSuatTietKiem(ngayDauThang, ngayDuLieu);
            var bangCanDoi = _clsBangCanDoiTaiKhoanKeToan.GetAllData(ngayDuLieu);
            foreach (var item in _phamViBaoCao)
            {
                var maChiNhanh = item.MA_DVI;
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
                
                //-------------------------------
                var laiSuatKhongKyHan = (from a in laiSuatTietKiem where a.KY_HAN == 0 && a.MA_NHOM_SP == "T02"  select a.LAI_SUAT).FirstOrDefault();
                var laiSuatDuoi1Thang = (from a in laiSuatTietKiem where a.KY_HAN < 1 && a.MA_NHOM_SP == "T04" select a.LAI_SUAT).FirstOrDefault();
                var laiSuatKyHan1Thang = (from a in laiSuatTietKiem where a.KY_HAN == 1 && a.MA_NHOM_SP == "T04" orderby a.SO_SO_TG descending, a.TY_TRONG descending select a.LAI_SUAT).FirstOrDefault();
                var laiSuatKyHan2Thang = (from a in laiSuatTietKiem where a.KY_HAN == 2 && a.MA_NHOM_SP == "T04" orderby a.SO_SO_TG descending, a.TY_TRONG descending select a.LAI_SUAT).FirstOrDefault();
                var laiSuatKyHan3Thang = (from a in laiSuatTietKiem where a.KY_HAN == 3 && a.MA_NHOM_SP == "T04" orderby a.SO_SO_TG descending, a.TY_TRONG descending select a.LAI_SUAT).FirstOrDefault();
                var laiSuatKyHan4Thang = (from a in laiSuatTietKiem where a.KY_HAN == 4 && a.MA_NHOM_SP == "T04" orderby a.SO_SO_TG descending, a.TY_TRONG descending select a.LAI_SUAT).FirstOrDefault();
                var laiSuatKyHan5Thang = (from a in laiSuatTietKiem where a.KY_HAN == 5 && a.MA_NHOM_SP == "T04" orderby a.SO_SO_TG descending, a.TY_TRONG descending select a.LAI_SUAT).FirstOrDefault();
                var laiSuatKyHan6Thang = (from a in laiSuatTietKiem where a.KY_HAN == 6 && a.MA_NHOM_SP == "T04" orderby a.SO_SO_TG descending, a.TY_TRONG descending select a.LAI_SUAT).FirstOrDefault();
                var laiSuatKyHan9Thang = (from a in laiSuatTietKiem where a.KY_HAN == 9 && a.MA_NHOM_SP == "T04" orderby a.SO_SO_TG descending, a.TY_TRONG descending select a.LAI_SUAT).FirstOrDefault();
                var laiSuatKyHan12Thang = (from a in laiSuatTietKiem where a.KY_HAN == 12 && a.MA_NHOM_SP == "T04" orderby a.SO_SO_TG descending, a.TY_TRONG descending select a.LAI_SUAT).FirstOrDefault();
                var laiSuatKyHan1224Thang = (from a in laiSuatTietKiem where a.KY_HAN > 12 && a.KY_HAN <= 24 && a.MA_NHOM_SP == "T04" orderby a.SO_SO_TG descending, a.TY_TRONG descending select a.LAI_SUAT).FirstOrDefault();
                var laiSuatKyHan24Thang = (from a in laiSuatTietKiem where a.KY_HAN == 0 && a.MA_NHOM_SP == "T01" select a.LAI_SUAT).FirstOrDefault();
                //---------------------------------
                var tk702010 = (from a in bangCanDoi where a.F03 == "702010" && a.F10 == maChiNhanh select a.F07).FirstOrDefault() 
                    - (from a in bangCanDoi where a.F03 == "702010" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();

                var tk70202 = (from a in bangCanDoi where a.F03 == "70202" && a.F10 == maChiNhanh select a.F07).FirstOrDefault() 
                    - (from a in bangCanDoi where a.F03 == "70202" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();

                var tk70203 = (from a in bangCanDoi where a.F03 == "70203" && a.F10 == maChiNhanh select a.F07).FirstOrDefault() 
                    - (from a in bangCanDoi where a.F03 == "70203" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();

                var tk70204 = (from a in bangCanDoi where a.F03 == "70204" && a.F10 == maChiNhanh select a.F07).FirstOrDefault() 
                    - (from a in bangCanDoi where a.F03 == "70204" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();

                var tk70213 = (from a in bangCanDoi where a.F03 == "70213" && a.F10 == maChiNhanh select a.F07).FirstOrDefault() 
                    - (from a in bangCanDoi where a.F03 == "70213" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();

                var tk70205 = (from a in bangCanDoi where a.F03 == "70205" && a.F10 == maChiNhanh select a.F07).FirstOrDefault() 
                    - (from a in bangCanDoi where a.F03 == "70205" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();

                var tk70206 = (from a in bangCanDoi where a.F03 == "70206" && a.F10 == maChiNhanh select a.F07).FirstOrDefault() 
                    - (from a in bangCanDoi where a.F03 == "70206" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();

                var tk70208 = (from a in bangCanDoi where a.F03 == "70208" && a.F10 == maChiNhanh select a.F07).FirstOrDefault() 
                    - (from a in bangCanDoi where a.F03 == "70208" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();

                var duNganHan = ((from a in bangCanDoi where a.F03 == "211" && a.F10 == maChiNhanh select a.F04).FirstOrDefault() 
                    + (from a in bangCanDoi where a.F03 == "211" && a.F10 == maChiNhanh select a.F08).FirstOrDefault()) / 2;

                var duTrungDaiHan = ((from a in bangCanDoi where a.F03 == "212" && a.F10 == maChiNhanh select a.F04).FirstOrDefault() 
                    + (from a in bangCanDoi where a.F03 == "212" && a.F10 == maChiNhanh select a.F08).FirstOrDefault()) / 2;

                var laiSuatChoVayThongThuongNganHan = (tk702010 + tk70202 + tk70204 + tk70213 + tk70203) / duNganHan * 100 * 12;

                var laiSuatChoVayThongThuongTrungDaiHan = (tk70205 + tk70206 + tk70208) / duTrungDaiHan * 100 * 12;

                var laiSuatChoVayDoiSongNganHan = (tk702010 + tk70202 + tk70204 + tk70213 + tk70203) / duNganHan * 100 * 12;

                var laiSuatChoVayDoiSongTrungDaiHan = (tk70205 + tk70206 + tk70208) / duTrungDaiHan * 100 * 12;

                excelSheet.Cells["D19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                    "{0:00.00}", laiSuatKhongKyHan);
                excelSheet.Cells["D20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", laiSuatDuoi1Thang);
                excelSheet.Cells["D21"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                   "{0:00.00}", laiSuatKyHan1Thang);
                excelSheet.Cells["D22"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatKyHan2Thang);
                excelSheet.Cells["D23"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatKyHan3Thang);
                excelSheet.Cells["D24"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatKyHan4Thang);
                excelSheet.Cells["D25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatKyHan5Thang);
                excelSheet.Cells["D26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatKyHan6Thang);
                excelSheet.Cells["D27"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatKyHan9Thang);
                excelSheet.Cells["D28"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatKyHan12Thang);
                excelSheet.Cells["D29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatKyHan1224Thang);
                excelSheet.Cells["D30"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatKyHan24Thang);
                excelSheet.Cells["D33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                "{0:00.00}", laiSuatChoVayThongThuongNganHan);
                excelSheet.Cells["D34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
              "{0:00.00}", laiSuatChoVayThongThuongTrungDaiHan);
                excelSheet.Cells["D39"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
            "{0:00.00}", laiSuatChoVayDoiSongNganHan);
                excelSheet.Cells["D40"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
           "{0:00.00}", laiSuatChoVayDoiSongTrungDaiHan);
                //Write it back to the client
                var fileOnServer = Server.MapPath($"~/Temp/{folderName}/{fileName}.xlsx");
                exPackage.SaveAs(new FileInfo(fileOnServer));
                reportCount++;
            }
            if (reportCount == _phamViBaoCao.Count())
            {
                status = true;
            }
            return Json(new
            {
                data = reportCount,
                status = status
            }, JsonRequestBehavior.AllowGet);
        }
    }
}