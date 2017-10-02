using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWebApplication.Commons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ReportWebApplication.Controllers
{
    public class A00024Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan _clsBaoCaoTinhHinhHuyDongVon;

        public A00024Controller()
        {
            _clsBaoCaoTinhHinhHuyDongVon = new clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan();
        }

        // GET: A00024
        private IEnumerable<clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan> GetDataPrevMonth(string maChiNhanh, string ngayDuLieu)
        {
            return _clsBaoCaoTinhHinhHuyDongVon.GetAllData(maChiNhanh, ngayDuLieu);
        }

        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var ngayDuLieu = (DateTime.Parse(denNgay)).ToString("yyyyMMdd");
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var _phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
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
                var clsTinhHinhHuyDongTienGui = GetDataPrevMonth(maChiNhanh, ngayDuLieu);
                var tienGuiTuNguyen = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T02" select a.SO_TIEN).Sum();
                var tienGuiQuyDinh = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T01" select a.SO_TIEN).Sum();
                var tienGuiDuoi6Thang = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN < 6 select a.SO_TIEN).Sum();
                var tienGui6Den12Thang = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN >= 6 && a.KY_HAN <= 12 select a.SO_TIEN).Sum();
                var tienGui12Den24Thang = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN > 12 && a.KY_HAN <= 24 select a.SO_TIEN).Sum();
                var tienGui24Den60Thang = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN > 24 && a.KY_HAN <= 60 select a.SO_TIEN).Sum();
                var tienGuiTu60Thang = (from a in clsTinhHinhHuyDongTienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN > 60 select a.SO_TIEN).Sum();
                var tienGuiTietKiem = tienGuiTuNguyen + tienGuiQuyDinh + tienGuiDuoi6Thang + tienGui6Den12Thang + tienGui12Den24Thang + tienGui24Den60Thang + tienGuiTu60Thang;
                var tienGuiKhachHang = tienGuiTietKiem;
                excelSheet.Cells["F21"].Value = excelSheet.Cells["J21"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tienGuiKhachHang / dviTinh, 2));
                excelSheet.Cells["F26"].Value = excelSheet.Cells["J26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tienGuiTietKiem / dviTinh, 2));
                excelSheet.Cells["F27"].Value = excelSheet.Cells["J27"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((tienGuiTuNguyen + tienGuiDuoi6Thang) / dviTinh, 2));
                excelSheet.Cells["F28"].Value = excelSheet.Cells["J28"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tienGui6Den12Thang / dviTinh, 2));
                excelSheet.Cells["F29"].Value = excelSheet.Cells["J29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tienGui12Den24Thang / dviTinh, 2));
                excelSheet.Cells["F30"].Value = excelSheet.Cells["J30"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(tienGui24Den60Thang / dviTinh, 2));
                excelSheet.Cells["F31"].Value = excelSheet.Cells["J31"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((tienGuiTu60Thang + tienGuiQuyDinh) / dviTinh, 2));
                excelSheet.Cells["F56"].Value = excelSheet.Cells["J56"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", Math.Round((tienGuiKhachHang) / dviTinh, 2));
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
                status
            }, JsonRequestBehavior.AllowGet);
        }
    }
}