using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWebApplication.Commons;
using ReportWpfApplication.Common;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ReportWebApplication.Controllers
{
    public class G02824Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();

        private readonly clsBaoCaoThucHienTyLeAnToanVonRiengLe _clsBaoCaoThucHienTyLeAnToanVonRiengLe;
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;
        public G02824Controller()
        {
            _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
            _clsBaoCaoThucHienTyLeAnToanVonRiengLe = new clsBaoCaoThucHienTyLeAnToanVonRiengLe();
        }

        // GET: G02824
        

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
            var clsTyLeAnToanVonRiengLe = _clsBangCanDoiTaiKhoanKeToan.GetAllData(ngayDuLieu);
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

                var tk60 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "60" && x.F10 == maChiNhanh).Select(x => x.F09).SingleOrDefault();
                var tk61 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "61" && x.F10 == maChiNhanh).Select(x => x.F09).SingleOrDefault();

                var vonCap1 = tk60 + tk61;

                var vonCap2 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "2192" && x.F10 == maChiNhanh).Select(x => x.F09).SingleOrDefault();

                var vonTuCo = vonCap1 + vonCap2;

                var tienMat = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "10" && x.F10 == maChiNhanh).Select(x => x.F08).SingleOrDefault();

                var tienGuiNHTM = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "13" && x.F10 == maChiNhanh).Select(x => x.F08).SingleOrDefault();

                var tk21 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "21" && x.F10 == maChiNhanh).Select(x => x.F08).SingleOrDefault();
                var tk219 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "219" && x.F10 == maChiNhanh).Select(x => x.F09).SingleOrDefault();

                var tkNo30 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "30" && x.F10 == maChiNhanh).Select(x => x.F08).SingleOrDefault();
                var tkCo30 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "30" && x.F10 == maChiNhanh).Select(x => x.F09).SingleOrDefault();

                var tk31 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "31" && x.F10 == maChiNhanh).Select(x => x.F08).SingleOrDefault();

                var tk35 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "35" && x.F10 == maChiNhanh).Select(x => x.F08).SingleOrDefault();

                var tk36 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "36" && x.F10 == maChiNhanh).Select(x => x.F08).SingleOrDefault();
                var tk366 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "366" && x.F10 == maChiNhanh).Select(x => x.F08).SingleOrDefault();

                var tk38 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "38" && x.F10 == maChiNhanh).Select(x => x.F08).SingleOrDefault();

                var tk39 = clsTyLeAnToanVonRiengLe.Where(x => x.F03 == "39" && x.F10 == maChiNhanh).Select(x => x.F08).SingleOrDefault();

                var duNoKhachHangTkqd = _clsBaoCaoThucHienTyLeAnToanVonRiengLe.GetDuNoKhachHangTKQD(ngayDuLieu);
                var dunoKhachHangTktn = _clsBaoCaoThucHienTyLeAnToanVonRiengLe.GetDuNoKhachHangTKTN(ngayDuLieu);

                var duNoDamBaoBangTienGui = 2 * duNoKhachHangTkqd + dunoKhachHangTktn;

                var duNoChoVayKhachHang = tk21 - tk219 - duNoDamBaoBangTienGui;

                var taiSanCoKhac = tkNo30 - tkCo30 + tk31 + tk35 + tk36 - tk366 + tk38 + tk39;

                var taiSanRuiRo0 = tienMat + duNoDamBaoBangTienGui;
                var taiSanRuiRo20 = tienGuiNHTM;
                var taiSanRuiRo50 = 0;
                var taiSanRuiRo100 = duNoChoVayKhachHang + taiSanCoKhac;

                var tyle0 = taiSanRuiRo0 * 0;
                var tyle20 = taiSanRuiRo20 * (decimal)0.2;
                var tyle50 = taiSanRuiRo50 * (decimal)0.5;
                var tyle100 = taiSanRuiRo100;

                var giaTriTaiSanCoRuiRoNoiBangQuyDoi = tyle0 + tyle20 + tyle50 + tyle100;
                var giaTriTaiSanCoRuiRoNoiBang = taiSanRuiRo0 + taiSanRuiRo20 + taiSanRuiRo100 + taiSanRuiRo50;

                var tyLeAnToanVonToiThieu = Math.Round(vonTuCo / giaTriTaiSanCoRuiRoNoiBangQuyDoi * 100, 1);

                excelSheet.Cells["D18"].Value = clsFormatString.FormatStringTyLePhanTram(tyLeAnToanVonToiThieu);
                excelSheet.Cells["D19"].Value = clsFormatString.FormatStringDviTinhTrieuDong(vonTuCo);
                excelSheet.Cells["D20"].Value = clsFormatString.FormatStringDviTinhTrieuDong(vonCap1);
                excelSheet.Cells["D21"].Value = clsFormatString.FormatStringDviTinhTrieuDong(vonCap2);
                excelSheet.Cells["D23"].Value = clsFormatString.FormatStringDviTinhTrieuDong(giaTriTaiSanCoRuiRoNoiBang);
                excelSheet.Cells["D24"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanRuiRo0);
                excelSheet.Cells["D25"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tienMat);
                excelSheet.Cells["D27"].Value = clsFormatString.FormatStringDviTinhTrieuDong(duNoDamBaoBangTienGui);
                excelSheet.Cells["D30"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanRuiRo20);
                excelSheet.Cells["D31"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tienGuiNHTM);
                excelSheet.Cells["D34"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanRuiRo50);
                excelSheet.Cells["D37"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanRuiRo100);
                excelSheet.Cells["D38"].Value = clsFormatString.FormatStringDviTinhTrieuDong(duNoChoVayKhachHang);
                excelSheet.Cells["D39"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanCoKhac);
                excelSheet.Cells["E23"].Value = clsFormatString.FormatStringDviTinhTrieuDong(giaTriTaiSanCoRuiRoNoiBangQuyDoi);
                excelSheet.Cells["E24"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanRuiRo0 * 0);
                excelSheet.Cells["E25"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tienMat * 0);
                excelSheet.Cells["E27"].Value = clsFormatString.FormatStringDviTinhTrieuDong(duNoDamBaoBangTienGui * 0);
                excelSheet.Cells["E30"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tyle20);
                excelSheet.Cells["E31"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tienGuiNHTM * (decimal)0.2);
                excelSheet.Cells["E34"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tyle50);
                excelSheet.Cells["E37"].Value = clsFormatString.FormatStringDviTinhTrieuDong(tyle100);
                excelSheet.Cells["E38"].Value = clsFormatString.FormatStringDviTinhTrieuDong(duNoChoVayKhachHang * 1);
                excelSheet.Cells["E39"].Value = clsFormatString.FormatStringDviTinhTrieuDong(taiSanCoKhac * 1);
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