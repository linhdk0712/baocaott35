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
    public class A00054Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();

        private readonly clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK _clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK;

        public A00054Controller()
        {
            _clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK = new clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK();
        }

        // GET: A00054
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
                var duNoTinDung = _clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK.GetAllData(maChiNhanh, ngayDuLieu);

                #region Write to template

                var duNoNganHan01 = (from a in duNoTinDung where a.TGIAN_VAY <= 12 select a.SO_DU).Sum();
                var duNoTDH01 = (from a in duNoTinDung where a.TGIAN_VAY > 12 select a.SO_DU).Sum();
                var laiDuThu01 = (from a in duNoTinDung select a.LAI_DU_THU).Sum();
                excelSheet.Cells["D30"].Value = excelSheet.Cells["D33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", Math.Round(duNoNganHan01 / dviTinh, 2));
                excelSheet.Cells["H30"].Value = excelSheet.Cells["H33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.00}", Math.Round(duNoTDH01 / dviTinh, 2));
                excelSheet.Cells["L30"].Value = excelSheet.Cells["L33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round((duNoTDH01 + duNoNganHan01) / dviTinh, 2));
                excelSheet.Cells["M30"].Value = excelSheet.Cells["M33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.00}", Math.Round(laiDuThu01 / dviTinh, 2));

                #endregion Write to template

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