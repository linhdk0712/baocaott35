using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWebApplication.Commons;
using System.IO;
using OfficeOpenXml;

namespace ReportWebApplication.Controllers
{
    public class G02005Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly decimal _dviTinhs = ClsDonViTinh.DviTinhTrieuDong();
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;
        public G02005Controller()
        {
         _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();       
        }
         [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var dtFirstEndOfQuarter = clsDatesOfQuarter.DatesOfQuarter(denNgay);
            var startDateOfQuater = dtFirstEndOfQuarter[0];
            var endDateOfPrevQuater = startDateOfQuater.AddDays(-1).ToString("yyyyMMdd");
            var endDateOfQuarter = dtFirstEndOfQuarter[2].ToString("yyyyMMdd");
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            //-----------------------------------------------------------------------------------------------------
            var bangCanDoiKeToan = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfQuarter);  
            var bangCanDoiKeToanKyTruoc = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfPrevQuater);
            //-----------------------------------------------------------------------------------------------------
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
                var setNgayBaoCao = _createFileNameReport.NgayBaoCao(denNgay);
                var excelSheet = exPackage.Workbook.Worksheets[1];
                _setFileName.SetFileNameReport(fileName + ".xlsx", _maDonViGui,
                  clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu), setNgayBaoCao, _user, excelSheet);               
                //-------------------------------------------------------------------------------------------                

                #region Số liệu kỳ này

                var _851phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _851phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _853phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _853phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _854phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _854phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _852phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "852" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _852phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "852" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _856phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _856phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _87phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _87phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _871phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _871phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _86phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _86phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _862phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _862phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _868phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _868phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _883phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _883phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _882phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _882phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _8822phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _8822phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _89phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _89phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var tongSoCanBoNVien = 108;
               
                var _8512duCo = bangCanDoiKeToan.Where(x => x.F03 == "8512" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var luongVaPhuCap = _851phatSinhNo - _851phatSinhCo;
                var chiDongGop = _853phatSinhNo - _853phatSinhCo;
                var chiTroCap = _854phatSinhNo - _854phatSinhCo;
                var chiKhacChoNhanVien = _852phatSinhNo + _856phatSinhNo - _852phatSinhCo - _856phatSinhCo;
                var chiPhiChoNhanVien = luongVaPhuCap + chiDongGop + chiTroCap + chiKhacChoNhanVien;

                var chiVeTaiSan = _87phatSinhNo - _87phatSinhCo;

                var khauHaoTscd = _871phatSinhNo - _871phatSinhCo;
                var chiChoHoatDongQuanLyCongVu = _86phatSinhNo - _86phatSinhCo;

                var congTacPhi = _862phatSinhNo - _862phatSinhCo;
                var chiHoatDongDoanThe = _868phatSinhNo - _868phatSinhCo;
                var chiNopPhiBaoHiem = _883phatSinhNo - _883phatSinhCo;

                var chiPhiDuPhong = _882phatSinhNo - _882phatSinhCo - _8822phatSinhNo + _8822phatSinhCo;

                var chiPhiHoatDongKhac = _89phatSinhNo - _89phatSinhCo;

                var tongChiPhi = chiPhiChoNhanVien + chiVeTaiSan + chiChoHoatDongQuanLyCongVu + chiNopPhiBaoHiem +
                                 chiPhiDuPhong + chiPhiHoatDongKhac;

                var tongQuyLuong = luongVaPhuCap - _8512duCo;

                var tienThuong = 0;
                var thuNhapKhac = 0;

                var tongThuNhap = tongQuyLuong + tienThuong + thuNhapKhac;

                var tienLuongBinhQuan = tongQuyLuong / tongSoCanBoNVien;

                var thuNhapBinhQuan = tongThuNhap / tongSoCanBoNVien;

                #endregion

                #region Số liệu kỳ trước

                var _851phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _851phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _853phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _853phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _854phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _854phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _852phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "852" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _852phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "852" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _856phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _856phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _87phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _87phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _871phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _871phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _86phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _86phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _862phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _862phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _868phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _868phatSinhCoKyTruoc = bangCanDoiKeToan.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _883phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _883phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _882phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _882phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _8822phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _8822phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                var _89phatSinhNoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                var _89phatSinhCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();

                const int tongSoCanBoNVienKyTruoc = 108;

                var _8512duCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "8512" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var luongVaPhuCapKyTruoc = _851phatSinhNoKyTruoc - _851phatSinhCoKyTruoc;
                var chiDongGopKyTruoc = _853phatSinhNoKyTruoc - _853phatSinhCoKyTruoc;
                var chiTroCapKyTruoc = _854phatSinhNoKyTruoc - _854phatSinhCoKyTruoc;
                var chiKhacChoNhanVienKyTruoc = _852phatSinhNoKyTruoc + _856phatSinhNoKyTruoc - _852phatSinhCoKyTruoc - _856phatSinhCoKyTruoc;
                var chiPhiChoNhanVienKyTruoc = luongVaPhuCapKyTruoc + chiDongGopKyTruoc + chiTroCapKyTruoc + chiKhacChoNhanVienKyTruoc;

                var chiVeTaiSanKyTruoc = _87phatSinhNoKyTruoc - _87phatSinhCoKyTruoc;
                var khauHaoTscdKyTruoc = _871phatSinhNoKyTruoc - _871phatSinhCoKyTruoc;
                var chiChoHoatDongQuanLyCongVuKyTruoc = _86phatSinhNoKyTruoc - _86phatSinhCoKyTruoc;

                var congTacPhiKyTruoc = _862phatSinhNoKyTruoc - _862phatSinhCoKyTruoc;
                var chiHoatDongDoanTheKyTruoc = _868phatSinhNoKyTruoc - _868phatSinhCoKyTruoc;
                var chiNopPhiBaoHiemKyTruoc = _883phatSinhNoKyTruoc - _883phatSinhCoKyTruoc;

                var chiPhiDuPhongKyTruoc = _882phatSinhNoKyTruoc - _882phatSinhCoKyTruoc - _8822phatSinhNoKyTruoc + _8822phatSinhCoKyTruoc;

                var chiPhiHoatDongKhacKyTruoc = _89phatSinhNoKyTruoc - _89phatSinhCoKyTruoc;

                var tongChiPhiKyTruoc = chiPhiChoNhanVienKyTruoc + chiVeTaiSanKyTruoc + chiChoHoatDongQuanLyCongVuKyTruoc + chiNopPhiBaoHiemKyTruoc +
                                 chiPhiDuPhongKyTruoc + chiPhiHoatDongKhacKyTruoc;

                var tongQuyLuongKyTruoc = luongVaPhuCapKyTruoc - _8512duCoKyTruoc;

                const int tienThuongKyTruoc = 0;
                const int thuNhapKhacKyTruoc = 0;

                var tongThuNhapKyTruoc = tongQuyLuongKyTruoc + tienThuongKyTruoc + thuNhapKhacKyTruoc;

                var tienLuongBinhQuanKyTruoc = tongQuyLuongKyTruoc / tongSoCanBoNVienKyTruoc;

                var thuNhapBinhQuanKyTruoc = tongThuNhapKyTruoc / tongSoCanBoNVienKyTruoc;

                #endregion

                excelSheet.Cells["C20"].Value = Format(chiPhiChoNhanVien);
                excelSheet.Cells["C21"].Value = Format(luongVaPhuCap);
                excelSheet.Cells["C22"].Value = Format(chiDongGop);
                excelSheet.Cells["C23"].Value = Format(chiTroCap);
                excelSheet.Cells["C24"].Value = Format(chiKhacChoNhanVien);
                excelSheet.Cells["C25"].Value = Format(chiVeTaiSan);
                excelSheet.Cells["C26"].Value = Format(khauHaoTscd);
                excelSheet.Cells["C27"].Value = Format(chiChoHoatDongQuanLyCongVu);
                excelSheet.Cells["C29"].Value = Format(congTacPhi);

                excelSheet.Cells["C30"].Value = Format(chiHoatDongDoanThe);
                excelSheet.Cells["C31"].Value = Format(chiNopPhiBaoHiem);
                excelSheet.Cells["C32"].Value = Format(chiPhiDuPhong);
                excelSheet.Cells["C33"].Value = Format(chiPhiHoatDongKhac);
                excelSheet.Cells["C34"].Value = Format(tongChiPhi);
                excelSheet.Cells["C36"].Value = tongSoCanBoNVien;
                excelSheet.Cells["C38"].Value = Format(tongQuyLuong);
                excelSheet.Cells["C39"].Value = Format(tienThuong);

                excelSheet.Cells["C40"].Value = Format(thuNhapKhac);
                excelSheet.Cells["C41"].Value = Format(tongThuNhap);
                excelSheet.Cells["C42"].Value = Format(tienLuongBinhQuan);
                excelSheet.Cells["C43"].Value = Format(thuNhapBinhQuan);

                //------------------------------------------------------------------------------------
                excelSheet.Cells["D20"].Value = Format(chiPhiChoNhanVienKyTruoc);
                excelSheet.Cells["D21"].Value = Format(luongVaPhuCapKyTruoc);
                excelSheet.Cells["D22"].Value = Format(chiDongGopKyTruoc);
                excelSheet.Cells["D23"].Value = Format(chiTroCapKyTruoc);
                excelSheet.Cells["D24"].Value = Format(chiKhacChoNhanVienKyTruoc);
                excelSheet.Cells["D25"].Value = Format(chiVeTaiSanKyTruoc);
                excelSheet.Cells["D26"].Value = Format(khauHaoTscdKyTruoc);
                excelSheet.Cells["D27"].Value = Format(chiChoHoatDongQuanLyCongVuKyTruoc);
                excelSheet.Cells["D29"].Value = Format(congTacPhiKyTruoc);

                excelSheet.Cells["D30"].Value = Format(chiHoatDongDoanTheKyTruoc);
                excelSheet.Cells["D31"].Value = Format(chiNopPhiBaoHiemKyTruoc);
                excelSheet.Cells["D32"].Value = Format(chiPhiDuPhongKyTruoc);
                excelSheet.Cells["D33"].Value = Format(chiPhiHoatDongKhacKyTruoc);
                excelSheet.Cells["D34"].Value = Format(tongChiPhiKyTruoc);
                excelSheet.Cells["D36"].Value = tongSoCanBoNVienKyTruoc;
                excelSheet.Cells["D38"].Value = Format(tongQuyLuongKyTruoc);
                excelSheet.Cells["D39"].Value = Format(tienThuongKyTruoc);

                excelSheet.Cells["D40"].Value = Format(thuNhapKhacKyTruoc);
                excelSheet.Cells["D41"].Value = Format(tongThuNhapKyTruoc);
                excelSheet.Cells["D42"].Value = Format(tienLuongBinhQuanKyTruoc);
                excelSheet.Cells["D43"].Value = Format(thuNhapBinhQuanKyTruoc);
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

        private string Format(decimal data)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", Math.Round(data / _dviTinhs, 1));
        }
       
    }
}