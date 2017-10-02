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
    public class G01204Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        public G01204Controller()
        {
           
        }

        // GET: G01204
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
            var _phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            var _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
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
               
                #region Write to template

                
                var duNoNhom1 = (from a in bangCanDoi where a.F03 == "2111" && a.F10 == maChiNhanh select a.F08).FirstOrDefault() 
                    + (from a in bangCanDoi where a.F03 == "2121" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var duNoNhom2 = (from a in bangCanDoi where a.F03 == "2112" && a.F10 == maChiNhanh select a.F08).FirstOrDefault() 
                    + (from a in bangCanDoi where a.F03 == "2122" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var duNoNhom3 = (from a in bangCanDoi where a.F03 == "2113" && a.F10 == maChiNhanh select a.F08).FirstOrDefault() 
                    + (from a in bangCanDoi where a.F03 == "2123" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var duNoNhom4 = (from a in bangCanDoi where a.F03 == "2114" && a.F10 == maChiNhanh select a.F08).FirstOrDefault() 
                    + (from a in bangCanDoi where a.F03 == "2124" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var duNoNhom5 = (from a in bangCanDoi where a.F03 == "2115" && a.F10 == maChiNhanh select a.F08).FirstOrDefault() 
                    + (from a in bangCanDoi where a.F03 == "2125" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
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
                status
            }, JsonRequestBehavior.AllowGet);
        }
    }
}