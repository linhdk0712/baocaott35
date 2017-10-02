using OfficeOpenXml;
using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWebApplication.Commons;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ReportWebApplication.Controllers
{
    public class G01285Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly ClsBaoCaoMangLuoiHoatDong _clsBaoCaoMangLuoiHoatDong;
        public G01285Controller()
        {
            _clsBaoCaoMangLuoiHoatDong = new ClsBaoCaoMangLuoiHoatDong();
        }

        // GET: G01285
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var ngayDuLieu = (DateTime.Parse(denNgay)).ToString("yyyyMMdd");
            var dtFirstEndOfQuarter = clsDatesOfQuarter.DatesOfQuarter(denNgay);
            var endDateOfPreQuarter = (dtFirstEndOfQuarter[0].AddDays(-1)).ToString("yyyyMMdd");
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
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
                var soLuongChiNhanh = _clsBaoCaoMangLuoiHoatDong.NumBerOfBranch();
                var soLuongPhongGd = _clsBaoCaoMangLuoiHoatDong.NumBerOfTrans();
                var soLuongKhachHangQuyHienTai = _clsBaoCaoMangLuoiHoatDong.NumBerOfCustomers(ngayDuLieu);
                var soLuongKhachHangQuyTruoc = _clsBaoCaoMangLuoiHoatDong.NumBerOfCustomers(endDateOfPreQuarter);
                var tangGiamKhachHang = soLuongKhachHangQuyHienTai - soLuongKhachHangQuyTruoc;
                var soLuongKhachHangVayVonQuyHienTai = _clsBaoCaoMangLuoiHoatDong.NumBerOfLoans(ngayDuLieu);
                var soLuongKhachHangVayVonQuyTruoc = _clsBaoCaoMangLuoiHoatDong.NumBerOfLoans(endDateOfPreQuarter);
                var tangGiamKhVayVon = soLuongKhachHangVayVonQuyHienTai - soLuongKhachHangVayVonQuyTruoc;
                excelSheet.Cells["D18"].Value = "Tổ chức tài chính vi mô TNHH M7";
                excelSheet.Cells["D19"].Value = "01912001";
                excelSheet.Cells["D20"].Value = "Tầng 2-Căn nhà lô A9/D5-Khu đô thị mới Cầu Giấy- Phường Dịch Vọng Hậu- Quận Cầu Giấy-Hà Nội ";
                excelSheet.Cells["D21"].Value = "024.7303.6688";
                excelSheet.Cells["D22"].Value = "16a/GP-NHNN";
                excelSheet.Cells["D23"].Value = "20120113";
                excelSheet.Cells["D24"].Value = "20130201";
                excelSheet.Cells["D25"].Value = "15,545,230,691";
                excelSheet.Cells["D28"].Value = soLuongChiNhanh;
                excelSheet.Cells["D30"].Value = soLuongPhongGd;
                excelSheet.Cells["D32"].Value = soLuongKhachHangQuyHienTai;
                excelSheet.Cells["D33"].Value = tangGiamKhachHang;
                excelSheet.Cells["D34"].Value = soLuongKhachHangVayVonQuyHienTai;
                excelSheet.Cells["D35"].Value = tangGiamKhVayVon;

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