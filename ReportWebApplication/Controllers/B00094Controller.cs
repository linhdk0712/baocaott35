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
    public class B00094Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();

        public B00094Controller()
        {
        }

        // GET: B00094
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
                
                #region Write to template

                var noTk801 = (from a in bangCanDoi where a.F03 == "801" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var coTk801 = (from a in bangCanDoi where a.F03 == "801" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var coDauKyTk423 = (from a in bangCanDoi where a.F03 == "423" && a.F10 == maChiNhanh select a.F05).FirstOrDefault();
                var coCuoiKyTk423 = (from a in bangCanDoi where a.F03 == "423" && a.F10 == maChiNhanh select a.F09).FirstOrDefault();
                var laiSuatTienGuiBinhQuan = Math.Round(((noTk801 - coTk801) / ((coDauKyTk423 + coCuoiKyTk423) / 2)) * 100 * 12, 2);

                var coTk702 = (from a in bangCanDoi where a.F03 == "702" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var noTk702 = (from a in bangCanDoi where a.F03 == "702" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var noDauKy21 = (from a in bangCanDoi where a.F03 == "21" && a.F10 == maChiNhanh select a.F04).FirstOrDefault();
                var noCuoiKy21 = (from a in bangCanDoi where a.F03 == "21" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var coTk7022 = (from a in bangCanDoi where a.F03 == "7022" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var noTk7022 = (from a in bangCanDoi where a.F03 == "7022" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();

                var laiSuatChoVayBinhQuan = Math.Round(((coTk702 - noTk702) - (coTk7022 - noTk7022)) / ((noDauKy21 + noCuoiKy21) / 2) * 100 * 12, 2);
                excelSheet.Cells["D19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", laiSuatTienGuiBinhQuan);
                excelSheet.Cells["E19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", laiSuatChoVayBinhQuan);

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
                status
            }, JsonRequestBehavior.AllowGet);
        }
    }
}