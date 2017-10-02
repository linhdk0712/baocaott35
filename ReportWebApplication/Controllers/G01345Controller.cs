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
    public class G01345Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly decimal _dviTinhs = ClsDonViTinh.DviTinhDong();
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;

        // GET: G01345
        public G01345Controller()
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
            var endDateOfQuarter = dtFirstEndOfQuarter[2].ToString("yyyyMMdd");
            var endDateOfPrevQuater = dtFirstEndOfQuarter[0].AddDays(-1);
            var prevYear = dtFirstEndOfQuarter[0].AddYears(-1).Year;
            var endDateOfPrevYear = new DateTime(prevYear, 12, 31);
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            //-----------------------------------------------------------------------------------------------------
            var bangCanDoiKeToan = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfQuarter);
            var bangCanDoiKeToanQuyTruoc = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfPrevQuater.ToString("yyyyMMdd"));
            var bangCanDoiKeToanNamTruoc = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfPrevYear.ToString("yyyyMMdd"));
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
                // Số liệu quý báo cáo
                var _701DuCoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "701" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _702DuCoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "702" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _7022DuCoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "7022" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _709DuCoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "709" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _801DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "801" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _802DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "802" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _8022DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "8022" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _805DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "805" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _809DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "809" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _71DuCoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "71" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _81DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "81" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _72DuCoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "72" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _82DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "82" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _79DuCoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "79" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _89DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _83DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "83" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _851DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _853DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _854DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _855DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "855" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _856DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _87DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _871DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _86DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _862DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _868DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _883DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _882DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _8822DuNoQuyNay = bangCanDoiKeToan.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                // Số liệu quý trước
                var _701DuCoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "701" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _702DuCoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "702" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _709DuCoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "709" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _801DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "801" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _802DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "802" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _805DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "805" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _809DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "809" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _71DuCoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "71" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _81DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "81" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _72DuCoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "72" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _82DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "82" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _79DuCoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "79" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _89DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _83DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "83" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _851DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _853DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _854DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _855DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "855" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _856DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _87DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _871DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _86DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _862DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _868DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _883DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _882DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _8822DuNoQuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                // Số liệu năm trước
                var _701DuCoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "701" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _702DuCoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "702" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _709DuCoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "709" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _801DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "801" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _802DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "802" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _805DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "805" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _809DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "809" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _71DuCoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "71" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _81DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "81" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _72DuCoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "72" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _82DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "82" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _79DuCoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "79" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _89DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _83DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "83" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _851DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _853DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _854DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _855DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "855" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _856DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _87DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _871DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _86DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _862DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _868DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _883DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _882DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _8822DuNoNamTruoc = bangCanDoiKeToanNamTruoc.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();


                #region Phát sinh trong kỳ

                var thuNhapLaiTienGui = _701DuCoQuyNay - _701DuCoQuyTruoc;
                var thuNhapLaiChoVay = _702DuCoQuyNay - _702DuCoQuyTruoc - _7022DuCoQuyNay;
                var thuNhapKhacTuHoatDongTinDung = _709DuCoQuyNay - _709DuCoQuyTruoc;
                var thuNhapLaiVaCacKhoanThuTuongTu =
                    thuNhapLaiTienGui + thuNhapLaiChoVay + thuNhapKhacTuHoatDongTinDung;

                var traLaiTienGui =  - _801DuNoQuyTruoc + _801DuNoQuyNay;
                var traLaiTienVay = _802DuNoQuyNay - _802DuNoQuyTruoc - _8022DuNoQuyNay;
                var traLaiThueTaiChinh = _805DuNoQuyNay - _805DuNoQuyTruoc;
                var chiPhiHoatDongTinDungKhac = _809DuNoQuyNay - _809DuNoQuyTruoc;
                var chiPhiLaiVaCacChiPhiTuongTu =
                    traLaiTienGui + traLaiTienVay + traLaiThueTaiChinh + chiPhiHoatDongTinDungKhac;

                var thuNhapLaiThuan = thuNhapLaiVaCacKhoanThuTuongTu - chiPhiLaiVaCacChiPhiTuongTu;

                var thuNhapTuHoatDongDichVu = _71DuCoQuyNay - _71DuCoQuyTruoc;
                var chiPhiTuHoatDongDichVu = _81DuNoQuyNay - _81DuNoQuyTruoc;

                var laiLoThuanTuHoatDongDichVu = thuNhapTuHoatDongDichVu - chiPhiTuHoatDongDichVu;

                var thuNhapTuKinhDoanhNgoaiHoi = _72DuCoQuyNay - _72DuCoQuyTruoc;
                var chiPhiKinhDoanhNgoaiHoi = _82DuNoQuyNay - _82DuNoQuyTruoc;

                var laiLoThuanTuKinhDoanhNgoaiHoi = thuNhapTuKinhDoanhNgoaiHoi - chiPhiKinhDoanhNgoaiHoi;

                var thuNhapKhac = _79DuCoQuyNay - _79DuCoQuyTruoc;
                var chiPhiKhac = _89DuNoQuyNay - _89DuNoQuyTruoc;

                var laiLoThuanTuHoatDongKhac = thuNhapKhac - chiPhiKhac;

                var chiNopThueVaCacKhoanPhi = _83DuNoQuyNay - _83DuNoQuyTruoc;
                var chiLuongVaPhucap = _851DuNoQuyNay - _851DuNoQuyTruoc;
                var cacKhoanChiDongGopTheoLuong = _853DuNoQuyNay - _853DuNoQuyTruoc;
                var chiTroCap = _854DuNoQuyNay - _854DuNoQuyTruoc;
                var chiKhacChoNhanVien = _855DuNoQuyNay + _856DuNoQuyNay - _855DuNoQuyTruoc - _856DuNoQuyTruoc;
                var chiVeTaiSan = _87DuNoQuyNay - _87DuNoQuyTruoc;
                var khauHaoTscd = _871DuNoQuyNay - _871DuNoQuyTruoc;
                var chiHoatDongQuanLyCongVu = _86DuNoQuyNay - _86DuNoQuyTruoc;
                var congTacPhi = _862DuNoQuyNay - _862DuNoQuyTruoc;
                var chiCacHoatDongDoanThe = _868DuNoQuyNay - _868DuNoQuyTruoc;
                var chiNopPhiBaoHiemTienGuiCuaKhachHang = _883DuNoQuyNay - _883DuNoQuyTruoc;
                var chiPhiDuPhong = _882DuNoQuyNay - _882DuNoQuyTruoc - _8822DuNoQuyNay + _8822DuNoQuyTruoc;
                const decimal chiPhiHoatDongKhac = 0;

                var chiPhiHoatDong = chiNopThueVaCacKhoanPhi + chiLuongVaPhucap + cacKhoanChiDongGopTheoLuong +
                                         chiTroCap + chiKhacChoNhanVien + chiVeTaiSan + chiHoatDongQuanLyCongVu +
                                         chiNopPhiBaoHiemTienGuiCuaKhachHang + chiPhiDuPhong + chiPhiHoatDongKhac;

                var loiNhuanThuanTruocChiPhiDuPhongRuiRo =
                    thuNhapLaiThuan + laiLoThuanTuHoatDongDichVu + laiLoThuanTuKinhDoanhNgoaiHoi + laiLoThuanTuHoatDongKhac + chiPhiHoatDong;

                var chiPhiDuPhongRuiRoTinDungChovayKhachHang = _8822DuNoQuyNay - _8822DuNoQuyNay;

                var chiPhiDuPhongRuiRoTinDung = chiPhiDuPhongRuiRoTinDungChovayKhachHang;

                var loiNhuanTruocThue = loiNhuanThuanTruocChiPhiDuPhongRuiRo + chiPhiDuPhongRuiRoTinDung;

                var thuNhapChiuThue = loiNhuanTruocThue;

                var chiPhiThueThuNhapDoanhNghiep = thuNhapChiuThue * (decimal)0.1;

                var tongChiPhiThueThuNhapDoanhNghiep = chiPhiThueThuNhapDoanhNghiep;

                var loiNhuanSauThue = loiNhuanTruocThue - tongChiPhiThueThuNhapDoanhNghiep;

                #endregion Phát sinh trong kỳ

                #region Lũy kế từ đầu năm đến thời điểm kết thúc kỳ báo cáo

                var thuNhapLaiTienGuiLuyKe = _701DuCoQuyNay - _701DuCoNamTruoc;
                var thuNhapLaiChoVayLuyKe = _702DuCoQuyNay - _702DuCoNamTruoc - _7022DuCoQuyNay;
                var thuNhapKhacTuHoatDongTinDungLuyKe = _709DuCoQuyNay - _709DuCoNamTruoc;
                var thuNhapLaiVaCacKhoanThuTuongTuLuyKe =
                    thuNhapLaiTienGuiLuyKe + thuNhapLaiChoVayLuyKe + thuNhapKhacTuHoatDongTinDungLuyKe;

                var traLaiTienGuiLuyKe = _801DuNoQuyNay - _801DuNoNamTruoc;
                var traLaiTienVayLuyKe = _802DuNoQuyNay - _802DuNoNamTruoc - _8022DuNoQuyNay;
                var traLaiThueTaiChinhLuyKe = _805DuNoQuyNay - _805DuNoNamTruoc;
                var chiPhiHoatDongTinDungKhacLuyKe = _809DuNoQuyNay - _809DuNoNamTruoc;
                var chiPhiLaiVaCacChiPhiTuongTuLuyKe =
                    traLaiTienGuiLuyKe + traLaiTienVayLuyKe + traLaiThueTaiChinhLuyKe + chiPhiHoatDongTinDungKhacLuyKe;

                var thuNhapLaiThuanLuyKe = thuNhapLaiVaCacKhoanThuTuongTuLuyKe - chiPhiLaiVaCacChiPhiTuongTuLuyKe;

                var thuNhapTuHoatDongDichVuLuyKe = _71DuCoQuyNay - _71DuCoNamTruoc;
                var chiPhiTuHoatDongDichVuLuyKe = _81DuNoQuyNay - _81DuNoNamTruoc;

                var laiLoThuanTuHoatDongDichVuLuyKe = thuNhapTuHoatDongDichVuLuyKe - chiPhiTuHoatDongDichVuLuyKe;

                var thuNhapTuKinhDoanhNgoaiHoiLuyKe = _72DuCoQuyNay - _72DuCoNamTruoc;
                var chiPhiKinhDoanhNgoaiHoiLuyKe = _82DuNoQuyNay - _82DuNoNamTruoc;

                var laiLoThuanTuKinhDoanhNgoaiHoiLuyKe = thuNhapTuKinhDoanhNgoaiHoi - chiPhiKinhDoanhNgoaiHoi;

                var thuNhapKhacLuyKe = _79DuCoQuyNay - _79DuCoNamTruoc;
                var chiPhiKhacLuyKe = _89DuNoQuyNay - _89DuNoNamTruoc;

                var laiLoThuanTuHoatDongKhacLuyKe = thuNhapKhacLuyKe - chiPhiKhacLuyKe;

                var chiNopThueVaCacKhoanPhiLuyKe = _83DuNoQuyNay - _83DuNoNamTruoc;
                var chiLuongVaPhucapLuyKe = _851DuNoQuyNay - _851DuNoNamTruoc;
                var cacKhoanChiDongGopTheoLuongLuyKe = _853DuNoQuyNay - _853DuNoNamTruoc;
                var chiTroCapLuyKe = _854DuNoQuyNay - _854DuNoNamTruoc;
                var chiKhacChoNhanVienLuyKe = _855DuNoQuyNay + _856DuNoQuyNay - _855DuNoNamTruoc - _856DuNoNamTruoc;
                var chiVeTaiSanLuyKe = _87DuNoQuyNay - _87DuNoNamTruoc;
                var khauHaoTscdLuyKe = _871DuNoQuyNay - _871DuNoNamTruoc;
                var chiHoatDongQuanLyCongVuLuyKe = _86DuNoQuyNay - _86DuNoNamTruoc;
                var congTacPhiLuyKe = _862DuNoQuyNay - _862DuNoNamTruoc;
                var chiCacHoatDongDoanTheLuyKe = _868DuNoQuyNay - _868DuNoNamTruoc;
                var chiNopPhiBaoHiemTienGuiCuaKhachHangLuyKe = _883DuNoQuyNay - _883DuNoNamTruoc;
                var chiPhiDuPhongLuyKe = _882DuNoQuyNay - _8822DuNoQuyNay - _882DuNoNamTruoc - _8822DuNoNamTruoc;
                const decimal chiPhiHoatDongKhacLuyKe = 0;

                var chiPhiHoatDongLuyKe = chiNopThueVaCacKhoanPhiLuyKe + chiLuongVaPhucapLuyKe + cacKhoanChiDongGopTheoLuongLuyKe +
                                         chiTroCapLuyKe + chiKhacChoNhanVienLuyKe + chiVeTaiSanLuyKe + chiHoatDongQuanLyCongVuLuyKe +
                                         chiNopPhiBaoHiemTienGuiCuaKhachHangLuyKe + chiPhiDuPhongLuyKe + chiPhiHoatDongKhacLuyKe;

                var loiNhuanThuanTruocChiPhiDuPhongRuiRoLuyKe =
                    thuNhapLaiThuanLuyKe + laiLoThuanTuHoatDongDichVuLuyKe + laiLoThuanTuKinhDoanhNgoaiHoiLuyKe + laiLoThuanTuHoatDongKhacLuyKe + chiPhiHoatDongLuyKe;

                var chiPhiDuPhongRuiRoTinDungChovayKhachHangLuyKe = _8822DuNoQuyNay - _8822DuNoNamTruoc;

                var chiPhiDuPhongRuiRoTinDungLuyKe = chiPhiDuPhongRuiRoTinDungChovayKhachHangLuyKe;

                var loiNhuanTruocThueLuyKe = loiNhuanThuanTruocChiPhiDuPhongRuiRoLuyKe + chiPhiDuPhongRuiRoTinDungLuyKe;

                var thuNhapChiuThueLuyKe = loiNhuanTruocThueLuyKe;

                var chiPhiThueThuNhapDoanhNghiepLuyKe = thuNhapChiuThueLuyKe * (decimal)0.1;

                var tongChiPhiThueThuNhapDoanhNghiepLuyKe = chiPhiThueThuNhapDoanhNghiepLuyKe;

                var loiNhuanSauThueLuyKe = loiNhuanTruocThueLuyKe - tongChiPhiThueThuNhapDoanhNghiepLuyKe;

                #endregion Lũy kế từ đầu năm đến thời điểm kết thúc kỳ báo cáo

                excelSheet.Cells["D18"].Value = Format(thuNhapLaiVaCacKhoanThuTuongTu);
                excelSheet.Cells["D19"].Value = Format(thuNhapLaiTienGui);
                excelSheet.Cells["D20"].Value = Format(thuNhapLaiChoVay);
                excelSheet.Cells["D21"].Value = Format(thuNhapKhacTuHoatDongTinDung);

                excelSheet.Cells["D22"].Value = Format(chiPhiLaiVaCacChiPhiTuongTu);
                excelSheet.Cells["D23"].Value = Format(traLaiTienGui);
                excelSheet.Cells["D24"].Value = Format(traLaiTienVay);
                excelSheet.Cells["D25"].Value = Format(traLaiThueTaiChinh);
                excelSheet.Cells["D26"].Value = Format(chiPhiHoatDongTinDungKhac);

                excelSheet.Cells["D27"].Value = Format(thuNhapLaiThuan);
                excelSheet.Cells["D28"].Value = Format(thuNhapTuHoatDongDichVu);
                excelSheet.Cells["D29"].Value = Format(chiPhiTuHoatDongDichVu);
                excelSheet.Cells["D30"].Value = Format(laiLoThuanTuHoatDongDichVu);

                excelSheet.Cells["D31"].Value = Format(thuNhapTuKinhDoanhNgoaiHoi);
                excelSheet.Cells["D32"].Value = Format(chiPhiKinhDoanhNgoaiHoi);
                excelSheet.Cells["D33"].Value = Format(laiLoThuanTuKinhDoanhNgoaiHoi);

                excelSheet.Cells["D34"].Value = Format(thuNhapKhac);
                excelSheet.Cells["D35"].Value = Format(chiPhiKhac);
                excelSheet.Cells["D36"].Value = Format(laiLoThuanTuHoatDongKhac);

                excelSheet.Cells["D37"].Value = Format(chiNopThueVaCacKhoanPhi);
                excelSheet.Cells["D39"].Value = Format(chiLuongVaPhucap);
                excelSheet.Cells["D40"].Value = Format(cacKhoanChiDongGopTheoLuong);
                excelSheet.Cells["D41"].Value = Format(chiTroCap);
                excelSheet.Cells["D42"].Value = Format(chiKhacChoNhanVien);
                excelSheet.Cells["D43"].Value = Format(chiVeTaiSan);
                excelSheet.Cells["D44"].Value = Format(khauHaoTscd);
                excelSheet.Cells["D45"].Value = Format(chiHoatDongQuanLyCongVu);
                excelSheet.Cells["D46"].Value = Format(congTacPhi);
                excelSheet.Cells["D47"].Value = Format(chiCacHoatDongDoanThe);
                excelSheet.Cells["D48"].Value = Format(chiNopPhiBaoHiemTienGuiCuaKhachHang);
                excelSheet.Cells["D49"].Value = Format(chiPhiDuPhong);
                excelSheet.Cells["D50"].Value = Format(chiPhiHoatDongKhac);
                excelSheet.Cells["D51"].Value = Format(chiPhiHoatDong);

                excelSheet.Cells["D52"].Value = Format(loiNhuanThuanTruocChiPhiDuPhongRuiRo);

                excelSheet.Cells["D53"].Value = Format(chiPhiDuPhongRuiRoTinDungChovayKhachHang);

                excelSheet.Cells["D56"].Value = Format(chiPhiDuPhongRuiRoTinDung);

                excelSheet.Cells["D57"].Value = Format(loiNhuanTruocThue);
                excelSheet.Cells["D58"].Value = Format(thuNhapChiuThue);
                excelSheet.Cells["D59"].Value = Format(chiPhiThueThuNhapDoanhNghiep);

                excelSheet.Cells["D61"].Value = Format(tongChiPhiThueThuNhapDoanhNghiep);

                excelSheet.Cells["D81"].Value = Format(tongChiPhiThueThuNhapDoanhNghiep);
                excelSheet.Cells["D82"].Value = Format(loiNhuanSauThue);

                //------------------------------------------------------------------------------------------------------
                excelSheet.Cells["E18"].Value = Format(thuNhapLaiVaCacKhoanThuTuongTuLuyKe);
                excelSheet.Cells["E19"].Value = Format(thuNhapLaiTienGuiLuyKe);
                excelSheet.Cells["E20"].Value = Format(thuNhapLaiChoVayLuyKe);
                excelSheet.Cells["E21"].Value = Format(thuNhapKhacTuHoatDongTinDungLuyKe);

                excelSheet.Cells["E22"].Value = Format(chiPhiLaiVaCacChiPhiTuongTuLuyKe);
                excelSheet.Cells["E23"].Value = Format(traLaiTienGuiLuyKe);
                excelSheet.Cells["E24"].Value = Format(traLaiTienVayLuyKe);
                excelSheet.Cells["E25"].Value = Format(traLaiThueTaiChinhLuyKe);
                excelSheet.Cells["E26"].Value = Format(chiPhiHoatDongTinDungKhacLuyKe);

                excelSheet.Cells["E27"].Value = Format(thuNhapLaiThuanLuyKe);
                excelSheet.Cells["E28"].Value = Format(thuNhapTuHoatDongDichVuLuyKe);
                excelSheet.Cells["E29"].Value = Format(chiPhiTuHoatDongDichVuLuyKe);
                excelSheet.Cells["E30"].Value = Format(laiLoThuanTuHoatDongDichVuLuyKe);

                excelSheet.Cells["E31"].Value = Format(thuNhapTuKinhDoanhNgoaiHoiLuyKe);
                excelSheet.Cells["E32"].Value = Format(chiPhiKinhDoanhNgoaiHoiLuyKe);
                excelSheet.Cells["E33"].Value = Format(laiLoThuanTuKinhDoanhNgoaiHoiLuyKe);

                excelSheet.Cells["E34"].Value = Format(thuNhapKhacLuyKe);
                excelSheet.Cells["E35"].Value = Format(chiPhiKhacLuyKe);
                excelSheet.Cells["E36"].Value = Format(laiLoThuanTuHoatDongKhacLuyKe);

                excelSheet.Cells["E37"].Value = Format(chiNopThueVaCacKhoanPhiLuyKe);
                excelSheet.Cells["E39"].Value = Format(chiLuongVaPhucapLuyKe);
                excelSheet.Cells["E40"].Value = Format(cacKhoanChiDongGopTheoLuongLuyKe);
                excelSheet.Cells["E41"].Value = Format(chiTroCapLuyKe);
                excelSheet.Cells["E42"].Value = Format(chiKhacChoNhanVienLuyKe);
                excelSheet.Cells["E43"].Value = Format(chiVeTaiSanLuyKe);
                excelSheet.Cells["E44"].Value = Format(khauHaoTscdLuyKe);
                excelSheet.Cells["E45"].Value = Format(chiHoatDongQuanLyCongVuLuyKe);
                excelSheet.Cells["E46"].Value = Format(congTacPhiLuyKe);
                excelSheet.Cells["E47"].Value = Format(chiCacHoatDongDoanTheLuyKe);
                excelSheet.Cells["E48"].Value = Format(chiNopPhiBaoHiemTienGuiCuaKhachHangLuyKe);
                excelSheet.Cells["E49"].Value = Format(chiPhiDuPhongLuyKe);
                excelSheet.Cells["E50"].Value = Format(chiPhiHoatDongKhacLuyKe);
                excelSheet.Cells["E51"].Value = Format(chiPhiHoatDongLuyKe);

                excelSheet.Cells["E52"].Value = Format(loiNhuanThuanTruocChiPhiDuPhongRuiRoLuyKe);

                excelSheet.Cells["E53"].Value = Format(chiPhiDuPhongRuiRoTinDungChovayKhachHangLuyKe);

                excelSheet.Cells["E56"].Value = Format(chiPhiDuPhongRuiRoTinDungLuyKe);

                excelSheet.Cells["E57"].Value = Format(loiNhuanTruocThueLuyKe);
                excelSheet.Cells["E58"].Value = Format(thuNhapChiuThueLuyKe);
                excelSheet.Cells["E59"].Value = Format(chiPhiThueThuNhapDoanhNghiepLuyKe);

                excelSheet.Cells["E61"].Value = Format(tongChiPhiThueThuNhapDoanhNghiepLuyKe);

                excelSheet.Cells["E81"].Value = Format(tongChiPhiThueThuNhapDoanhNghiepLuyKe);
                excelSheet.Cells["E82"].Value = Format(loiNhuanSauThueLuyKe);

                //-------------------------------------------------------------------------------------------

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