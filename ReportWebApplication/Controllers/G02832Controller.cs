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
    public class G02832Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();

        private readonly clsTyLeKhaNangChiTra _clsTyLeKhaNangChiTra;

        public G02832Controller()
        {
            _clsTyLeKhaNangChiTra = new clsTyLeKhaNangChiTra();
        }

        // GET: G02832
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var ngayDuLieu = (DateTime.Parse(denNgay)).ToString("yyyyMMdd");
            var clsGetDateReport = new clsGetLastDayOfMonth();
            var res = new List<DateTime>();
            var firstDayOfMonth = new DateTime((DateTime.Parse(denNgay)).Year, (DateTime.Parse(denNgay)).Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var endOfPreviousMonth = firstDayOfMonth.AddDays(-1);
            var day10OfMonth = firstDayOfMonth.AddDays(9);
            var day20OfMonth = firstDayOfMonth.AddDays(19);
            if ((DateTime.Parse(denNgay)).Day >= 10 && (DateTime.Parse(denNgay)).Day < 20)
            {
                res = clsGetDateReport.GetRange(endOfPreviousMonth, day10OfMonth);
            }
            else if ((DateTime.Parse(denNgay)).Day >= 20 && (DateTime.Parse(denNgay)).Day < lastDayOfMonth.Day)
            {
                res = clsGetDateReport.GetRange(firstDayOfMonth.AddDays(9), day20OfMonth);
            }
            else if ((DateTime.Parse(denNgay)).Day >= lastDayOfMonth.Day)
            {
                res = clsGetDateReport.GetRange(firstDayOfMonth.AddDays(19), lastDayOfMonth);
            }
            var sTt = 1;
            var startRow = 18;
            string tuNgay;
            string _denNgay;
            decimal tienMat;
            decimal _tienGuiNHNN;
            decimal _tienGuiNHKHAC;
            decimal _tienGuiTN;
            decimal _tyLeKNCT;
            decimal _42;
            decimal _42321;
            var ngayDauThang = clsGetLastDayOfMonth.GetFirstDayOfMont((DateTime.Parse(denNgay)).Month, (DateTime.Parse(denNgay)).Year);
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var _phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            foreach (var machinhanh in _phamViBaoCao)
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
                for (var i = 0; i < res.Count - 1; i++)
                {
                    tuNgay = res[i].ToString("yyyyMMdd");
                    _denNgay = res[i + 1].ToString("yyyyMMdd");
                    var _bangCanDoiKeToan = _clsTyLeKhaNangChiTra.GetTyLeKhaNangChiTra(maDviPsinhDlieu, tuNgay, _denNgay);
                    tienMat = (from a in _bangCanDoiKeToan where a.F03 == "10" select a.F08).FirstOrDefault();
                    _tienGuiNHNN = 0;
                    _tienGuiNHKHAC = (from a in _bangCanDoiKeToan where a.F03 == "13" select a.F08).FirstOrDefault();
                    _42 = (from a in _bangCanDoiKeToan where a.F03 == "42" select a.F09).FirstOrDefault();
                    _42321 = (from a in _bangCanDoiKeToan where a.F03 == "42321" select a.F09).FirstOrDefault();
                    _tienGuiTN = _42 - _42321;
                    _tyLeKNCT = Math.Round((tienMat + _tienGuiNHNN + _tienGuiNHKHAC) / _tienGuiTN * 100, 2);
                    excelSheet.Cells["B" + startRow].Value = sTt;
                    excelSheet.Cells["C" + startRow].Value = res[i + 1].ToString("yyyyMMdd");
                    excelSheet.Cells["D" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                    "{0:00.0}", Math.Round(tienMat / dviTinh), 1);
                    excelSheet.Cells["F" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                    "{0:00.0}", Math.Round(_tienGuiNHKHAC / dviTinh), 1);
                    excelSheet.Cells["G" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                    "{0:00.0}", Math.Round(_tienGuiTN / dviTinh), 1);
                    excelSheet.Cells["H" + startRow].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                    "{0:00.00}", _tyLeKNCT);
                    sTt++;
                    startRow++;
                }
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