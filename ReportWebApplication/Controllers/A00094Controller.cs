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
    public class A00094Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();

        public A00094Controller()
        {
            new ClsBaoCaoDoanhSoCapThuNoTinDung();
        }

        // GET: A00094
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var ngayDuLieu = (DateTime.Parse(denNgay)).ToString("yyyyMMdd");
            clsGetLastDayOfMonth.GetFirstDayOfMont((DateTime.Parse(denNgay)).Month, (DateTime.Parse(denNgay)).Year);
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            var clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
            var bangCanDoi = clsBangCanDoiTaiKhoanKeToan.GetAllData(ngayDuLieu);
            foreach (var item in phamViBaoCao)
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
                //var doanhSo = _clsBaoCaoDoanhSoCapThuNoTinDung.GetAllData(ngayDuLieu);              

                #region Write to template

                var No211 = (from a in bangCanDoi where a.F03 == "211" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var No2112 = (from a in bangCanDoi where a.F03 == "2112" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var No2113 = (from a in bangCanDoi where a.F03 == "2113" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var No2114 = (from a in bangCanDoi where a.F03 == "2114" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var No2115 = (from a in bangCanDoi where a.F03 == "2115" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                //---------------------------------
                var No212 = (from a in bangCanDoi where a.F03 == "212" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var No2122 = (from a in bangCanDoi where a.F03 == "2122" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var No2123 = (from a in bangCanDoi where a.F03 == "2123" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var No2124 = (from a in bangCanDoi where a.F03 == "2124" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var No2125 = (from a in bangCanDoi where a.F03 == "2125" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                //---------------------------------
                var Co211 = (from a in bangCanDoi where a.F03 == "211" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var Co2112 = (from a in bangCanDoi where a.F03 == "2112" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var Co2113 = (from a in bangCanDoi where a.F03 == "2113" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var Co2114 = (from a in bangCanDoi where a.F03 == "2114" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var Co2115 = (from a in bangCanDoi where a.F03 == "2115" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                //---------------------------------
                var Co212 = (from a in bangCanDoi where a.F03 == "212" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var Co2122 = (from a in bangCanDoi where a.F03 == "2122" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var Co2123 = (from a in bangCanDoi where a.F03 == "2123" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var Co2124 = (from a in bangCanDoi where a.F03 == "2124" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var Co2125 = (from a in bangCanDoi where a.F03 == "2125" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                //---------------------------------
                var doanhSoCapTinDungNganHan = (No211 - (No2112 + No2113 + No2114 + No2115)) / dviTinh;
                var doanhSoCapTinDungTDH = (No212 - (No2122 + No2123 + No2124 + No2125)) / dviTinh;
                var doanhSoThuNoNganHan = (Co211 - (Co2112 + Co2113 + Co2114 + Co2115)) / dviTinh;
                var doanhSoThuNoTDH = (Co212 - (Co2122 + Co2123 + Co2124 + Co2125)) / dviTinh;
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

                #endregion Write to template

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
                status = status
            }, JsonRequestBehavior.AllowGet);
        }
    }
}