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
    public class G02905Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly decimal _dviTinhs = ClsDonViTinh.DviTinhDong();
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;
        public G02905Controller()
        {
            _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
        }
        // GET: G02905
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var dtFirstEndOfQuarter = clsDatesOfQuarter.DatesOfQuarter(denNgay);
            var endDateOfQuarter = dtFirstEndOfQuarter[1].ToString("yyyyMMdd");
            clsGetLastDayOfMonth.GetFirstDayOfMont(((DateTime.Parse(denNgay))).Month, (DateTime.Parse(denNgay)).Year);
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            //-------------------------------------------------------------------------------------------
            var bangCanDoiKeToan = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfQuarter);
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
                var tk132 = (from a in bangCanDoiKeToan where a.F03 == "132" && a.F10==maChiNhanh select a.F08).FirstOrDefault();
                var tk442 = (from a in bangCanDoiKeToan where a.F03 == "442" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var tongTaiSan = tk132;
                var vonTaiTroUyThacChoVay = tk442;
                var trangThaiTienTeNoiBang = tongTaiSan - vonTaiTroUyThacChoVay;

                excelSheet.Cells["D21"].Value = Format(tk132);
                excelSheet.Cells["D26"].Value = Format(tongTaiSan);
                excelSheet.Cells["D30"].Value = Format(tk442);
                excelSheet.Cells["D33"].Value = Format(vonTaiTroUyThacChoVay);
                excelSheet.Cells["D34"].Value = Format(trangThaiTienTeNoiBang);
                excelSheet.Cells["D36"].Value = Format(trangThaiTienTeNoiBang);

                excelSheet.Cells["F21"].Value = Format(tk132);
                excelSheet.Cells["F26"].Value = Format(tongTaiSan);
                excelSheet.Cells["F30"].Value = Format(tk442);
                excelSheet.Cells["F33"].Value = Format(vonTaiTroUyThacChoVay);
                excelSheet.Cells["F34"].Value = Format(trangThaiTienTeNoiBang);
                excelSheet.Cells["F36"].Value = Format(trangThaiTienTeNoiBang);
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
        private string Format(decimal data)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", Math.Round(data / _dviTinhs, 1));
        }

    }
}