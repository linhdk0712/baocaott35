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
    
    public class G01304Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly decimal _dviTinhs = ClsDonViTinh.DviTinhDong();

        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;

        public G01304Controller()
        {
            _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
        }

        // GET: G01304
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var ngayDuLieu = (DateTime.Parse(denNgay)).ToString("yyyyMMdd");
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            var clsG01304 = _clsBangCanDoiTaiKhoanKeToan.GetAllData(ngayDuLieu);
            foreach (var machinhanh in phamViBaoCao)
            {
                #region Create báo cáo
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
                var ranger = excelSheet.Cells["D19:D332"];
                foreach (var item in ranger)
                {
                    var f04 = (from a in clsG01304 where a.F03 == item.Text && a.F10 == maChiNhanh select a.F04).FirstOrDefault();
                    var f05 = (from a in clsG01304 where a.F03 == item.Text && a.F10 == maChiNhanh select a.F05).FirstOrDefault();
                    var f06 = (from a in clsG01304 where a.F03 == item.Text && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                    var f07 = (from a in clsG01304 where a.F03 == item.Text && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                    var f08 = (from a in clsG01304 where a.F03 == item.Text && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                    var f09 = (from a in clsG01304 where a.F03 == item.Text && a.F10 == maChiNhanh select a.F09).FirstOrDefault();
                    var address = item.Address;
                    var row = Convert.ToInt32(address.Substring(1, address.Length - 1)) ;
                    excelSheet.Cells["E" + row].Value = Format(f04);
                    excelSheet.Cells["F" + row].Value = Format(f05);
                    excelSheet.Cells["G" + row].Value = Format(f06);
                    excelSheet.Cells["H" + row].Value = Format(f07);
                    excelSheet.Cells["I" + row].Value = Format(f08);
                    excelSheet.Cells["J" + row].Value = Format(f09);
                    
                }
                //cập nhật dòng tổng cộng
                var f04t = (from a in clsG01304 where a.F03 == "XXX" && a.F10 == maChiNhanh select a.F04).FirstOrDefault();
                var f05t = (from a in clsG01304 where a.F03 == "XXX" && a.F10 == maChiNhanh select a.F05).FirstOrDefault();
                var f06t = (from a in clsG01304 where a.F03 == "XXX" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var f07t = (from a in clsG01304 where a.F03 == "XXX" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var f08t = (from a in clsG01304 where a.F03 == "XXX" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var f09t = (from a in clsG01304 where a.F03 == "XXX" && a.F10 == maChiNhanh select a.F09).FirstOrDefault();
                var rowt = 332;
                excelSheet.Cells["E" + rowt].Value = Format(f04t);
                excelSheet.Cells["F" + rowt].Value = Format(f05t);
                excelSheet.Cells["G" + rowt].Value = Format(f06t);
                excelSheet.Cells["H" + rowt].Value = Format(f07t);
                excelSheet.Cells["I" + rowt].Value = Format(f08t);
                excelSheet.Cells["J" + rowt].Value = Format(f09t);
                //Write it back to the client
                var fileOnServer = Server.MapPath($"~/Temp/{folderName}/{fileName}.xlsx");
                exPackage.SaveAs(new FileInfo(fileOnServer));
                reportCount++;

                #endregion
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