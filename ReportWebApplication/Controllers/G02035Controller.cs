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
    public class G02035Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly decimal _dviTinhs = ClsDonViTinh.DviTinhTrieuDong();
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;

        public G02035Controller()
        {
            _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
        }

        // GET: G02035
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            clsGetLastDayOfMonth.GetFirstDayOfMont(((DateTime.Parse(denNgay))).Month, (DateTime.Parse(denNgay)).Year);
            var daysOfQuarter = clsDatesOfQuarter.DatesOfQuarter(denNgay);
            var endDateOfThisQuarter = daysOfQuarter[2].ToString("yyyyMMdd");
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            //-------------------------------------------------------------------------------------------
            var bangCanDoiKeToanHienTai = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfThisQuarter);
            var clsSoLieuToanHeThongVaDonVis = phamViBaoCao as IList<clsSoLieuToanHeThongVaDonVi> ?? phamViBaoCao.ToList();
            foreach (var machinhanh in clsSoLieuToanHeThongVaDonVis)
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
                var clsBangCanDoiTaiKhoanKeToans = bangCanDoiKeToanHienTai as IList<ClsBangCanDoiTaiKhoanKeToan> ?? bangCanDoiKeToanHienTai.ToList();
                var duCoTaiKhoan4531 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4531" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var phatSinhCoTaiKhoan4531 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4531" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();
                var phatSinhNoTaiKhoan4531 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4531" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var duCoTaiKhoan4534 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4534" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var phatSinhCoTaiKhoan4534 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4534" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();
                var phatSinhNoTaiKhoan4534 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4534" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var duCoTaiKhoan4538 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4538" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var phatSinhCoTaiKhoan4538 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4538" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();
                var phatSinhNoTaiKhoan4538 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4538" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var duCoTaiKhoan4539 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4539" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var phatSinhCoTaiKhoan4539 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4539" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();
                var phatSinhNoTaiKhoan4539 = clsBangCanDoiTaiKhoanKeToans.Where(x => x.F03 == "4539" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var duDauKyThueGtgt = duCoTaiKhoan4531;
                var soPhaiNopThueGtgt = phatSinhCoTaiKhoan4531;
                var soDaNopThueGtgt = phatSinhNoTaiKhoan4531;
                var duCuoiKyThueGtgt = duDauKyThueGtgt + soPhaiNopThueGtgt - soDaNopThueGtgt;

                var duDauKyThueTndn = duCoTaiKhoan4534;
                var soPhaiNopThueTndn = phatSinhCoTaiKhoan4534;
                var soDaNopThueTndn = phatSinhNoTaiKhoan4534;
                var duCuoiKyThueTndn = duDauKyThueTndn + soPhaiNopThueTndn - soDaNopThueTndn;

                var duDauKyThueKhac = duCoTaiKhoan4538;
                var soPhaiNopThueKhac = phatSinhCoTaiKhoan4538;
                var soDaNopThueKhac = phatSinhNoTaiKhoan4538;
                var duCuoiKyThueKhac = duDauKyThueKhac + soPhaiNopThueKhac - soDaNopThueKhac;

                var duDauKyPhi = duCoTaiKhoan4539;
                var soPhaiNopPhi = phatSinhCoTaiKhoan4539;
                var soDaNopPhi = phatSinhNoTaiKhoan4539;
                var duCuoiKyPhi = duDauKyPhi + soPhaiNopPhi - soDaNopPhi;

                var tongDuDauKy = duDauKyThueGtgt + duDauKyThueTndn + duDauKyThueKhac + duDauKyPhi;
                var tongSoPhaiNop = soPhaiNopThueGtgt + soPhaiNopThueTndn + soPhaiNopThueKhac + soPhaiNopPhi;
                var tongSoDaNop = soDaNopThueGtgt + soDaNopThueTndn + soDaNopThueKhac + soDaNopPhi;
                var tongDuCuoiKy = tongDuDauKy + tongSoPhaiNop - tongSoDaNop;
                //-------------------------------------------------------------------------------------------
                excelSheet.Cells["C19"].Value = Format(duDauKyThueGtgt);
                excelSheet.Cells["C21"].Value = Format(duDauKyThueTndn);
                excelSheet.Cells["C27"].Value = Format(duDauKyThueKhac);
                excelSheet.Cells["C28"].Value = Format(duDauKyPhi);
                excelSheet.Cells["C29"].Value = Format(tongDuDauKy);

                excelSheet.Cells["D19"].Value = Format(soPhaiNopThueGtgt);
                excelSheet.Cells["D21"].Value = Format(soPhaiNopThueTndn);
                excelSheet.Cells["D27"].Value = Format(soPhaiNopThueKhac);
                excelSheet.Cells["D28"].Value = Format(soPhaiNopPhi);
                excelSheet.Cells["D29"].Value = Format(tongSoPhaiNop);

                excelSheet.Cells["E19"].Value = Format(soDaNopThueGtgt);
                excelSheet.Cells["E21"].Value = Format(soDaNopThueTndn);
                excelSheet.Cells["E27"].Value = Format(soDaNopThueKhac);
                excelSheet.Cells["E28"].Value = Format(soDaNopPhi);
                excelSheet.Cells["E29"].Value = Format(tongSoDaNop);

                excelSheet.Cells["F19"].Value = Format(duCuoiKyThueGtgt);
                excelSheet.Cells["F21"].Value = Format(duCuoiKyThueTndn);
                excelSheet.Cells["F27"].Value = Format(duCuoiKyThueKhac);
                excelSheet.Cells["F28"].Value = Format(duCuoiKyPhi);
                excelSheet.Cells["F29"].Value = Format(tongDuCuoiKy);
                //-------------------------------------------------------------------------------------------
                //Write it back to the client
                var fileOnServer = Server.MapPath($"~/Temp/{folderName}/{fileName}.xlsx");
                exPackage.SaveAs(new FileInfo(fileOnServer));
                reportCount++;
            }
            if (reportCount == clsSoLieuToanHeThongVaDonVis.Count())
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