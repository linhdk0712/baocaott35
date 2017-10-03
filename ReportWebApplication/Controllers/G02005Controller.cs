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
            // các ngày trong quý
            var dtFirstEndOfQuarter = clsDatesOfQuarter.DatesOfQuarter(denNgay);
            // Ngày đầu quý
            var startDateOfQuater = dtFirstEndOfQuarter[0];
            // Ngày cuối quý trước
            var endDateOfPrevQuater = startDateOfQuater.AddDays(-1);
            // Các ngày trong quý trước
            var daysInPrevQuater = clsDatesOfQuarter.DatesOfQuarter(endDateOfPrevQuater.ToString());
            // Ngày cuối quý trước nữa
            var endDateOfPrevPrevQuater = daysInPrevQuater[0].AddDays(-1);
            // Ngày cuối quý này
            var endDateOfQuarter = dtFirstEndOfQuarter[2].ToString("yyyyMMdd");
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            //-----------------------------------------------------------------------------------------------------
            var bangCanDoiKeToan = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfQuarter);
            var bangCanDoiKeToanKyTruoc = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfPrevQuater.ToString("yyyyMMdd"));
            var bangCanDoiKeToanKyTruocNua = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfPrevPrevQuater.ToString("yyyyMMdd"));
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

                var _851duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _851duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _853duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _853duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _854duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _854duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _852duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "852" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _852duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "852" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _856duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _856duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _87duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _87duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _871duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _871duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _86duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _86duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _862duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _862duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _868duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _868duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _883duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _883duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _882duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _882duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _8822duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _8822duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _89duNoDauKy = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _89duNoCuoiKy = bangCanDoiKeToan.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var tongSoCanBoNVien = 108;

                var _8512duCo = bangCanDoiKeToan.Where(x => x.F03 == "8512" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var luongVaPhuCap = -_851duNoDauKy + _851duNoCuoiKy;

                var chiDongGop = -_853duNoDauKy + _853duNoCuoiKy;

                var chiTroCap = -_854duNoDauKy + _854duNoCuoiKy;

                var chiKhacChoNhanVien = -_852duNoDauKy - _856duNoDauKy + _852duNoCuoiKy + _856duNoCuoiKy;

                var chiPhiChoNhanVien = luongVaPhuCap + chiDongGop + chiTroCap + chiKhacChoNhanVien;

                var chiVeTaiSan = -_87duNoDauKy + _87duNoCuoiKy;

                var khauHaoTscd = -_871duNoDauKy + _871duNoCuoiKy;

                var chiChoHoatDongQuanLyCongVu = -_86duNoDauKy + _86duNoCuoiKy;

                var congTacPhi = -_862duNoDauKy + _862duNoCuoiKy;
                var chiHoatDongDoanThe = -_868duNoDauKy + _868duNoCuoiKy;
                var chiNopPhiBaoHiem = -_883duNoDauKy + _883duNoCuoiKy;

                var chiPhiDuPhong = -_882duNoDauKy + _882duNoCuoiKy - _8822duNoDauKy + _8822duNoCuoiKy;

                var chiPhiHoatDongKhac = -_89duNoDauKy + _89duNoCuoiKy;

                var tongChiPhi = chiPhiChoNhanVien + chiVeTaiSan + chiChoHoatDongQuanLyCongVu + chiNopPhiBaoHiem +
                                 chiPhiDuPhong + chiPhiHoatDongKhac;

                var tongQuyLuong = luongVaPhuCap - _8512duCo;

                var tienThuong = 0;
                var thuNhapKhac = 0;

                var tongThuNhap = tongQuyLuong + tienThuong + thuNhapKhac;

                var tienLuongBinhQuan = tongQuyLuong / tongSoCanBoNVien;

                var thuNhapBinhQuan = tongThuNhap / tongSoCanBoNVien;

                #endregion Số liệu kỳ này

                #region Số liệu kỳ trước

                var _851duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _851duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "851" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _853duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _853duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "853" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _854duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _854duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "854" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _852duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "852" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _852duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "852" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _856duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _856duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "856" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _87duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _87duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "87" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _871duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _871duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "871" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _86duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _86duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "86" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _862duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _862duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "862" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _868duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _868duNoCuoiKyKyTruoc = bangCanDoiKeToan.Where(x => x.F03 == "868" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _883duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _883duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "883" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _882duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _882duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "882" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _8822duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _8822duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "8822" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _89duNoDauKyKyTruoc = bangCanDoiKeToanKyTruocNua.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _89duNoCuoiKyKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "89" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                const int tongSoCanBoNVienKyTruoc = 108;

                var _8512duCoKyTruoc = bangCanDoiKeToanKyTruoc.Where(x => x.F03 == "8512" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var luongVaPhuCapKyTruoc = -_851duNoDauKyKyTruoc + _851duNoCuoiKyKyTruoc;

                var chiDongGopKyTruoc = -_853duNoDauKyKyTruoc + _853duNoCuoiKyKyTruoc;

                var chiTroCapKyTruoc = -_854duNoDauKyKyTruoc + _854duNoCuoiKyKyTruoc;

                var chiKhacChoNhanVienKyTruoc = -_852duNoDauKyKyTruoc - _856duNoDauKyKyTruoc + _852duNoCuoiKyKyTruoc + _856duNoCuoiKyKyTruoc;

                var chiPhiChoNhanVienKyTruoc = luongVaPhuCapKyTruoc + chiDongGopKyTruoc + chiTroCapKyTruoc + chiKhacChoNhanVienKyTruoc;

                var chiVeTaiSanKyTruoc = -_87duNoDauKyKyTruoc + _87duNoCuoiKyKyTruoc;

                var khauHaoTscdKyTruoc = -_871duNoDauKyKyTruoc + _871duNoCuoiKyKyTruoc;

                var chiChoHoatDongQuanLyCongVuKyTruoc = -_86duNoDauKyKyTruoc + _86duNoCuoiKyKyTruoc;

                var congTacPhiKyTruoc = -_862duNoDauKyKyTruoc + _862duNoCuoiKyKyTruoc;

                var chiHoatDongDoanTheKyTruoc = -_868duNoDauKyKyTruoc + _868duNoCuoiKyKyTruoc;

                var chiNopPhiBaoHiemKyTruoc = -_883duNoDauKyKyTruoc + _883duNoCuoiKyKyTruoc;

                var chiPhiDuPhongKyTruoc = -_882duNoDauKyKyTruoc + _882duNoCuoiKyKyTruoc - _8822duNoDauKyKyTruoc + _8822duNoCuoiKyKyTruoc;

                var chiPhiHoatDongKhacKyTruoc = -_89duNoDauKyKyTruoc + _89duNoCuoiKyKyTruoc;

                var tongChiPhiKyTruoc = chiPhiChoNhanVienKyTruoc + chiVeTaiSanKyTruoc + chiChoHoatDongQuanLyCongVuKyTruoc + chiNopPhiBaoHiemKyTruoc +
                                 chiPhiDuPhongKyTruoc + chiPhiHoatDongKhacKyTruoc;

                var tongQuyLuongKyTruoc = luongVaPhuCapKyTruoc - _8512duCoKyTruoc;

                const int tienThuongKyTruoc = 0;

                const int thuNhapKhacKyTruoc = 0;

                var tongThuNhapKyTruoc = tongQuyLuongKyTruoc + tienThuongKyTruoc + thuNhapKhacKyTruoc;

                var tienLuongBinhQuanKyTruoc = tongQuyLuongKyTruoc / tongSoCanBoNVienKyTruoc;

                var thuNhapBinhQuanKyTruoc = tongThuNhapKyTruoc / tongSoCanBoNVienKyTruoc;

                #endregion Số liệu kỳ trước

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