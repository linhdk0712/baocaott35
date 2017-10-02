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
    public class G01315Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly decimal _dviTinhs = ClsDonViTinh.DviTinhDong();
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;
        private readonly clsBaoCaoDuNoPhanTheoNganhKinhTe _baoCaoDuNoPhanTheoNganhKinhTe;
        private readonly clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan _clsBaoCaoTinhHinhHuyDongVon;
        public G01315Controller()
        {
            _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
            _baoCaoDuNoPhanTheoNganhKinhTe = new clsBaoCaoDuNoPhanTheoNganhKinhTe();
            _clsBaoCaoTinhHinhHuyDongVon = new clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan();
        }
        // GET: G01315
        
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var ngayDuLieu = (DateTime.Parse(denNgay)).ToString("yyyyMMdd");
            var dtFirstEndOfQuarter = clsDatesOfQuarter.DatesOfQuarter(denNgay);
            var endDateOfQuarter = dtFirstEndOfQuarter[2].ToString("yyyyMMdd");
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            //           
            var clsTinhHinhHuyDongTienGui = _clsBaoCaoTinhHinhHuyDongVon.GetAllData("00", endDateOfQuarter);
            //-----------------------------------------------------------------------------------------------------
            var bangCanDoiKeToan = _clsBangCanDoiTaiKhoanKeToan.GetAllData(endDateOfQuarter);            
            //-----------------------------------------------------------------------------------------------------
            var mucDich01 = new[] { "01", "02", "10", "11" };
            var mucDich06 = new[] { "09" };
            var mucDich07 = new[] { "03" };
            var mucDich16 = new[] { "05" };
            var mucDich17 = new[] { "06", "12" };
            var mucDich19 = new[] { "04", "07", "08", "13", "99", "100", "101", "MUC_DICH_VAY_SXKD", "MUC_DICH_VAY_SXNN", "MUC_DICH_VAY_TDCN", "MUC_DICH_VAY_HSSV" };
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
                var _10duNo = bangCanDoiKeToan.Where(x => x.F03 == "10" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _1113duNo = bangCanDoiKeToan.Where(x => x.F03 == "1113" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _1123duNo = bangCanDoiKeToan.Where(x => x.F03 == "1123" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _1111duNo = bangCanDoiKeToan.Where(x => x.F03 == "1111" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _1121duNo = bangCanDoiKeToan.Where(x => x.F03 == "1121" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _1311duNo = bangCanDoiKeToan.Where(x => x.F03 == "1311" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _1312duNo = bangCanDoiKeToan.Where(x => x.F03 == "1312" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _1321duNo = bangCanDoiKeToan.Where(x => x.F03 == "1321" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _1322duNo = bangCanDoiKeToan.Where(x => x.F03 == "1322" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _20duNo = bangCanDoiKeToan.Where(x => x.F03 == "21" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _2111duNo = bangCanDoiKeToan.Where(x => x.F03 == "2111" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _2121duNo = bangCanDoiKeToan.Where(x => x.F03 == "2121" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _2131duNo = bangCanDoiKeToan.Where(x => x.F03 == "2131" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _2112duNo = bangCanDoiKeToan.Where(x => x.F03 == "2112" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _2122duNo = bangCanDoiKeToan.Where(x => x.F03 == "2122" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _2132duNo = bangCanDoiKeToan.Where(x => x.F03 == "2132" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _2113duNo = bangCanDoiKeToan.Where(x => x.F03 == "2113" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _2123duNo = bangCanDoiKeToan.Where(x => x.F03 == "2123" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _2133duNo = bangCanDoiKeToan.Where(x => x.F03 == "2133" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _2114duNo = bangCanDoiKeToan.Where(x => x.F03 == "2114" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _2124duNo = bangCanDoiKeToan.Where(x => x.F03 == "2124" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _2134duNo = bangCanDoiKeToan.Where(x => x.F03 == "2134" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _2115duNo = bangCanDoiKeToan.Where(x => x.F03 == "2115" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _2125duNo = bangCanDoiKeToan.Where(x => x.F03 == "2125" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _2135duNo = bangCanDoiKeToan.Where(x => x.F03 == "2135" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _211duNo = bangCanDoiKeToan.Where(x => x.F03 == "211" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _212duNo = bangCanDoiKeToan.Where(x => x.F03 == "212" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _213duNo = bangCanDoiKeToan.Where(x => x.F03 == "213" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _2192duNo = bangCanDoiKeToan.Where(x => x.F03 == "2192" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();     
                var _2191duNo = bangCanDoiKeToan.Where(x => x.F03 == "2191" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();                

                var _301duNo = bangCanDoiKeToan.Where(x => x.F03 == "301" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _302duNo = bangCanDoiKeToan.Where(x => x.F03 == "302" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _303duNo = bangCanDoiKeToan.Where(x => x.F03 == "303" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();                

                var _31duNo = bangCanDoiKeToan.Where(x => x.F03 == "31" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _35duNo = bangCanDoiKeToan.Where(x => x.F03 == "35" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _3535duNo = bangCanDoiKeToan.Where(x => x.F03 == "3535" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _36duNo = bangCanDoiKeToan.Where(x => x.F03 == "36" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var _366duNo = bangCanDoiKeToan.Where(x => x.F03 == "366" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _38duNo = bangCanDoiKeToan.Where(x => x.F03 == "38" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _39duNo = bangCanDoiKeToan.Where(x => x.F03 == "39" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _631duNo = bangCanDoiKeToan.Where(x => x.F03 == "631" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _64duNo = bangCanDoiKeToan.Where(x => x.F03 == "64" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                // 2191             
                var _2191duCoDauKy = bangCanDoiKeToan.Where(x => x.F03 == "2191" && x.F10 == maChiNhanh).Select(x => x.F05).FirstOrDefault();
                var _2191phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "2191" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();
                var _2191phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "2191" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();
                // 2192
                var _2192duCoDauKy = bangCanDoiKeToan.Where(x => x.F03 == "2192" && x.F10 == maChiNhanh).Select(x => x.F05).FirstOrDefault();
                var _2192phatSinhCo = bangCanDoiKeToan.Where(x => x.F03 == "2192" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();
                var _2192phatSinhNo = bangCanDoiKeToan.Where(x => x.F03 == "2192" && x.F10 == maChiNhanh).Select(x => x.F06).FirstOrDefault();

                var _3051duCo = bangCanDoiKeToan.Where(x => x.F03 == "3051" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _3052duCo = bangCanDoiKeToan.Where(x => x.F03 == "3052" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _415duCo = bangCanDoiKeToan.Where(x => x.F03 == "415" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _416duCo = bangCanDoiKeToan.Where(x => x.F03 == "416" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _42321duCo = bangCanDoiKeToan.Where(x => x.F03 == "42321" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();                
                var _42322duCo = bangCanDoiKeToan.Where(x => x.F03 == "42322" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _4231duCo = bangCanDoiKeToan.Where(x => x.F03 == "4231" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _441duCo = bangCanDoiKeToan.Where(x => x.F03 == "441" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _442duCo = bangCanDoiKeToan.Where(x => x.F03 == "442" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _45duCo = bangCanDoiKeToan.Where(x => x.F03 == "45" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _4535duCo = bangCanDoiKeToan.Where(x => x.F03 == "4535" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _46duCo = bangCanDoiKeToan.Where(x => x.F03 == "46" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _466duCo = bangCanDoiKeToan.Where(x => x.F03 == "466" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _48duCo = bangCanDoiKeToan.Where(x => x.F03 == "48" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _484duCo = 0;

                var _49duCo = bangCanDoiKeToan.Where(x => x.F03 == "49" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _601duCo = bangCanDoiKeToan.Where(x => x.F03 == "601" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _602duCo = bangCanDoiKeToan.Where(x => x.F03 == "602" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _609duCo = bangCanDoiKeToan.Where(x => x.F03 == "609" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _61duCo = bangCanDoiKeToan.Where(x => x.F03 == "61" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _62duCo = bangCanDoiKeToan.Where(x => x.F03 == "62" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _631duCo = bangCanDoiKeToan.Where(x => x.F03 == "631" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _64duCo = bangCanDoiKeToan.Where(x => x.F03 == "64" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();

                var _70duCo = bangCanDoiKeToan.Where(x => x.F03 == "7" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _80duNo = bangCanDoiKeToan.Where(x => x.F03 == "8" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                var _692duCo = bangCanDoiKeToan.Where(x => x.F03 == "692" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var _692DuNo = bangCanDoiKeToan.Where(x => x.F03 == "692" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();

                //var duTienGuiTietKiemQuyDinh = clsTinhHinhHuyDongTienGui.Where(x => x.MA_NHOM_SP == "T01").Sum(x => x.SO_TIEN); ;// Thống kê số dư tiền gửi tiết kiệm quy định
                var duTienGuiKhachHangThanhVien = clsTinhHinhHuyDongTienGui.Where(x=>x.MA_KHANG_LOAI == "TVIEN").Sum(x=>x.SO_TIEN); // Thống kê số dư tiền gửi của khách hàng thành viên
                var duTienGuiKhachHangKhongPhaiThanhVien = clsTinhHinhHuyDongTienGui.Where(x => x.MA_KHANG_LOAI == "CNHAN").Sum(x => x.SO_TIEN); ; // Thống kê số dư tiền gửi của khách hàng không phải là khách hàng thành viên
               

                // Phân tích dư nợ cho vay theo ngành
                var duThangNay = _baoCaoDuNoPhanTheoNganhKinhTe.GetAllData(maChiNhanh, ngayDuLieu);

                var duNoNongLamNghiepThuySan = mucDich01.Sum(item => (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault());
                var duNoXayDung = mucDich06.Sum(item => (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault());
                var duNoBanBuonBanLe = mucDich07.Sum(item => (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault());
                var duNoGiaoDuc = mucDich16.Sum(item => (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault());
                var duNoYTe = mucDich17.Sum(item => (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault());
                var duNoKhac = mucDich19.Sum(item => (from a in duThangNay where a.MUC_DICH_VAY == item select a.NHOM1 + a.NHOM2 + a.NHOM3 + a.NHOM4 + a.NHOM5).FirstOrDefault());
                var duNoNganhCongNghiepXayDung = duNoXayDung;
                var duNoNganhThuongMaiDichVu = duNoBanBuonBanLe + duNoGiaoDuc + duNoYTe + duNoKhac;
                var tongDuNoTheoNganh = duNoNongLamNghiepThuySan + duNoNganhCongNghiepXayDung + duNoNganhThuongMaiDichVu;
                //----------------------------------------------------------------------------------------------
                var tienMatVnd = _10duNo; // Tiền mặt

                var tienGuiThanhToanTaiNhnn = _1113duNo + _1123duNo;
                var tienGuiPhongToa = _1111duNo + _1121duNo;
                var tienGuiNhnn = tienGuiThanhToanTaiNhnn + tienGuiPhongToa; // Tiền gửi tại NHNN

                var tienGuiKkhVnd = _1311duNo;
                var tienGuiKhhNgoaiHoi = _1321duNo;
                var tienGuiKkhTctdKhac = tienGuiKkhVnd + tienGuiKhhNgoaiHoi; // Tiền gửi không kỳ hạn

                var tienGuiCkhVnd = _1312duNo;
                var tienGuiCkhNgoaiHoi = _1322duNo;
                var tienGuiCkhTctdKhac = tienGuiCkhVnd + tienGuiCkhNgoaiHoi; // Tiền gửi có kỳ hạn

                var tienGuiTctd = tienGuiKkhTctdKhac + tienGuiCkhTctdKhac; // Tiền gửi các TCTD khác

                var choVayToChucCaNhanTrongNuoc = _20duNo;
                
                var noDuTieuChuan = _2111duNo + _2121duNo + _2131duNo;
                var noCanChuY = _2112duNo + _2122duNo + _2132duNo;
                var noDuoiTieuChuan = _2113duNo + _2123duNo + _2133duNo; ;
                var noNghiNgo = _2114duNo + _2124duNo + _2134duNo; ;
                var noCoKhaNangMatVon = _2115duNo + _2125duNo + _2135duNo;
                var chatLuongNoVay = noDuTieuChuan + noCanChuY + noDuoiTieuChuan + noNghiNgo + noCoKhaNangMatVon;

                var noNganHan = _211duNo;
                var noTrungHan = _212duNo;
                var noDaiHan = _213duNo;
                var duNoTheoThoiGian = noNganHan + noTrungHan + noDaiHan;

                var choVayKhachHangTcvm = _20duNo;
                var duNoTheoDoiTuongKhachHang = choVayKhachHangTcvm;

                var duPhongChungTrichLapTrongKy = -_2192duCoDauKy - _2192phatSinhCo;
                var duPhongChungSuDungTrongKy = _2192phatSinhNo;
                var duPhongChungTong = duPhongChungTrichLapTrongKy + duPhongChungSuDungTrongKy;

                var duPhongCuTheTrichLapTrongKy = -_2191duCoDauKy - _2191phatSinhCo;//-------------------------------------
                var duPhongCuTheSuDungTrongKy = _2191phatSinhNo;
                var duPhongCuTheTong = duPhongCuTheSuDungTrongKy + duPhongCuTheTrichLapTrongKy;

                var duPhongRuiRoChoVayKhachHang = duPhongChungTong + duPhongCuTheTong;

                var choVayKhachHang = choVayToChucCaNhanTrongNuoc + duPhongRuiRoChoVayKhachHang ;

                var nguyenGiaTscdHuuHinh = _301duNo;
                var haoMonTscdHuuHinh = -_3051duCo;
                var taiSanCoDinhHuuHinh = nguyenGiaTscdHuuHinh + haoMonTscdHuuHinh;

                var taiSanCoDinhThueTaiChinh = _303duNo;

                var nguyenGiaTscdVoHinh = _302duNo;
                var haoMonTscdVoHinh = -_3052duCo;
                var taSanCoDinhVoHinh = nguyenGiaTscdVoHinh + haoMonTscdVoHinh;

                var taiSanCoDinh = taiSanCoDinhHuuHinh + taiSanCoDinhThueTaiChinh + taSanCoDinhVoHinh;

                var cacKhoanPhaiThuNoiBo = _36duNo - _366duNo;
                var cacKhoanPhaiThuBenNgoai = _35duNo - _3535duNo;
                var cacKhoanPhaiThu = cacKhoanPhaiThuNoiBo + cacKhoanPhaiThuBenNgoai;

                var laiPhiPhaiThu = _39duNo;
                var taiSanThueTndnHoanLai = _3535duNo;
                var taiSanCoKhac = _31duNo + _38duNo;
                var tongTaiSanCoKhac = cacKhoanPhaiThu + laiPhiPhaiThu + taiSanThueTndnHoanLai + taiSanCoKhac;

                var tongTaiSanCo = tienMatVnd + tienGuiNhnn + tienGuiTctd + choVayKhachHang + taiSanCoDinh +
                                   tongTaiSanCoKhac;

                var tienVayTctdKhacVnd = _415duCo;
                var tienVayTctdKhacNgoaiHoi = _416duCo;
                var tienVayTctdKhac = tienVayTctdKhacVnd + tienVayTctdKhacNgoaiHoi;

                var tienGuiTietKiemBatBuoc = _42321duCo;
                var tienGuiTietKiemTuNguyen = _42322duCo + _4231duCo;

                var tienGuiCuaKhachHangTheoLoaiTienGui = tienGuiTietKiemBatBuoc + tienGuiTietKiemTuNguyen;

                var tienguiCuaKhachHangTheoDoiTuongKhachHang =
                    duTienGuiKhachHangThanhVien + duTienGuiKhachHangKhongPhaiThanhVien;

                var vonTaiTroUyThacVnd = _441duCo;
                var vonTaiTroUyThacNgoaiTe = _442duCo;
                var vonTaitroUyThac = vonTaiTroUyThacNgoaiTe + vonTaiTroUyThacVnd;

                var noKhacLaiPhiPhaiTra = _49duCo;
                var noKhacThueTndn = _4535duCo;
                var noKhacCongNoPhaiTra = _45duCo - _4535duCo + _46duCo - _466duCo + _48duCo + _62duCo;

                var quyKhenThuongPhucLoi = _484duCo;
                var cacKhoanNoKhac = noKhacLaiPhiPhaiTra + noKhacThueTndn + noKhacCongNoPhaiTra + quyKhenThuongPhucLoi;

                var tongNoPhaiTra = tienVayTctdKhac + tienGuiCuaKhachHangTheoLoaiTienGui + vonTaitroUyThac +
                                    cacKhoanNoKhac;

                var vonDieuLe = _601duCo;
                var vonDauTuXdcbTscd = _602duCo;
                var vonKhac = _609duCo;
                var vonCuaTctd = vonDieuLe + vonDauTuXdcbTscd + vonKhac;

                var quyTctd = _61duCo;

                var chenhLechTyGia = _631duCo - _631duNo;

                var chenhLechDanhGiaLaiTaiSan = _64duCo - _64duNo;

                var loiNhuanNamNay = _70duCo - _80duNo;

                var loiNhuanNamTruoc = _692duCo - _692DuNo;
                var loiNhuanChuaPhanPhoi = loiNhuanNamNay + loiNhuanNamTruoc;
                var vonChuSoHuu = vonCuaTctd + quyTctd + chenhLechTyGia + chenhLechDanhGiaLaiTaiSan +
                                  loiNhuanChuaPhanPhoi;

                var tongNoPhaiTraVonChuSoHuu = tongNoPhaiTra + vonChuSoHuu;

                excelSheet.Cells["D19"].Value = Format(tienMatVnd);

                excelSheet.Cells["D20"].Value = Format(tienGuiNhnn);
                excelSheet.Cells["D21"].Value = Format(tienGuiThanhToanTaiNhnn);
                excelSheet.Cells["D22"].Value = Format(tienGuiPhongToa);
                excelSheet.Cells["D23"].Value = Format(tienGuiTctd);
                excelSheet.Cells["D24"].Value = Format(tienGuiKkhTctdKhac);
                excelSheet.Cells["D25"].Value = Format(tienGuiKkhVnd);
                excelSheet.Cells["D26"].Value = Format(tienGuiKhhNgoaiHoi);
                excelSheet.Cells["D27"].Value = Format(tienGuiCkhTctdKhac);
                excelSheet.Cells["D28"].Value = Format(tienGuiCkhVnd);
                excelSheet.Cells["D29"].Value = Format(tienGuiCkhNgoaiHoi);

                excelSheet.Cells["D31"].Value = Format(choVayKhachHang);
                excelSheet.Cells["D32"].Value = Format(choVayToChucCaNhanTrongNuoc);
                excelSheet.Cells["D33"].Value = Format(choVayToChucCaNhanTrongNuoc);
                excelSheet.Cells["D36"].Value = Format(chatLuongNoVay);
                excelSheet.Cells["D37"].Value = Format(noDuTieuChuan);
                excelSheet.Cells["D38"].Value = Format(noCanChuY);
                excelSheet.Cells["D39"].Value = Format(noDuoiTieuChuan);

                excelSheet.Cells["D40"].Value = Format(noNghiNgo);
                excelSheet.Cells["D41"].Value = Format(noCoKhaNangMatVon);
                excelSheet.Cells["D42"].Value = Format(duNoTheoThoiGian);
                excelSheet.Cells["D43"].Value = Format(noNganHan);
                excelSheet.Cells["D44"].Value = Format(noTrungHan);
                excelSheet.Cells["D45"].Value = Format(noDaiHan);
                excelSheet.Cells["D46"].Value = Format(duNoTheoDoiTuongKhachHang);
                
                excelSheet.Cells["D50"].Value = Format(choVayKhachHangTcvm);

                excelSheet.Cells["D51"].Value = Format(tongDuNoTheoNganh);

                excelSheet.Cells["D52"].Value = Format(duNoNongLamNghiepThuySan);
                excelSheet.Cells["D53"].Value = Format(duNoXayDung);
                excelSheet.Cells["D58"].Value = Format(duNoXayDung);
                excelSheet.Cells["D59"].Value = Format(duNoNganhThuongMaiDichVu);
                excelSheet.Cells["D60"].Value = Format(duNoBanBuonBanLe);
                excelSheet.Cells["D69"].Value = Format(duNoGiaoDuc);
                excelSheet.Cells["D70"].Value = Format(duNoYTe);
                excelSheet.Cells["D72"].Value = Format(duNoKhac);

                excelSheet.Cells["D75"].Value = Format(duPhongRuiRoChoVayKhachHang);

                excelSheet.Cells["D76"].Value = Format(duPhongChungTong);
                excelSheet.Cells["D77"].Value = Format(duPhongChungTrichLapTrongKy);
                excelSheet.Cells["D78"].Value = Format(duPhongChungSuDungTrongKy);

                excelSheet.Cells["D79"].Value = Format(duPhongCuTheTong);
                excelSheet.Cells["D80"].Value = Format(duPhongCuTheTrichLapTrongKy);
                excelSheet.Cells["D81"].Value = Format(duPhongCuTheSuDungTrongKy);

                excelSheet.Cells["D95"].Value = Format(taiSanCoDinh);

                excelSheet.Cells["D96"].Value = Format(taiSanCoDinhHuuHinh);
                excelSheet.Cells["D97"].Value = Format(nguyenGiaTscdHuuHinh);
                excelSheet.Cells["D98"].Value = Format(haoMonTscdHuuHinh);
                excelSheet.Cells["D107"].Value = Format(taiSanCoDinhThueTaiChinh);
                excelSheet.Cells["D108"].Value = Format(taSanCoDinhVoHinh);
                excelSheet.Cells["D109"].Value = Format(nguyenGiaTscdVoHinh);
                excelSheet.Cells["D110"].Value = Format(haoMonTscdVoHinh);

                excelSheet.Cells["D111"].Value = Format(tongTaiSanCoKhac);
                excelSheet.Cells["D113"].Value = Format(cacKhoanPhaiThu);
                excelSheet.Cells["D114"].Value = Format(cacKhoanPhaiThuNoiBo);
                excelSheet.Cells["D115"].Value = Format(cacKhoanPhaiThuBenNgoai);

                excelSheet.Cells["D116"].Value = Format(laiPhiPhaiThu);

                excelSheet.Cells["D117"].Value = Format(taiSanThueTndnHoanLai);

                excelSheet.Cells["D118"].Value = Format(taiSanCoKhac);

                excelSheet.Cells["D132"].Value = Format(tongTaiSanCo);

                excelSheet.Cells["D134"].Value = Format(tienVayTctdKhac);
                excelSheet.Cells["D135"].Value = Format(tienVayTctdKhacVnd);
                excelSheet.Cells["D136"].Value = Format(tienVayTctdKhacNgoaiHoi);

                excelSheet.Cells["D138"].Value = Format(tienGuiCuaKhachHangTheoLoaiTienGui);
                excelSheet.Cells["D145"].Value = Format(tienGuiTietKiemBatBuoc);
                excelSheet.Cells["D146"].Value = Format(tienGuiTietKiemTuNguyen);


                excelSheet.Cells["D147"].Value = Format(tienguiCuaKhachHangTheoDoiTuongKhachHang);
                excelSheet.Cells["D150"].Value = Format(duTienGuiKhachHangThanhVien);
                excelSheet.Cells["D151"].Value = Format(duTienGuiKhachHangKhongPhaiThanhVien);

                excelSheet.Cells["D152"].Value = Format(vonTaitroUyThac);
                excelSheet.Cells["D153"].Value = Format(vonTaiTroUyThacVnd);
                excelSheet.Cells["D154"].Value = Format(vonTaiTroUyThacNgoaiTe);


                excelSheet.Cells["D155"].Value = Format(cacKhoanNoKhac);
                excelSheet.Cells["D156"].Value = Format(noKhacLaiPhiPhaiTra);
                excelSheet.Cells["D157"].Value = Format(noKhacThueTndn);
                excelSheet.Cells["D158"].Value = Format(noKhacCongNoPhaiTra);
                excelSheet.Cells["D159"].Value = Format(quyKhenThuongPhucLoi);
                excelSheet.Cells["D167"].Value = Format(quyKhenThuongPhucLoi);

                excelSheet.Cells["D168"].Value = Format(tongNoPhaiTra);

                excelSheet.Cells["D169"].Value = Format(vonChuSoHuu);

                excelSheet.Cells["D170"].Value = Format(vonCuaTctd);
                excelSheet.Cells["D171"].Value = Format(vonDieuLe);
                excelSheet.Cells["D172"].Value = Format(vonDauTuXdcbTscd);
                excelSheet.Cells["D173"].Value = Format(vonKhac);

                excelSheet.Cells["D174"].Value = Format(quyTctd);

                excelSheet.Cells["D175"].Value = Format(chenhLechTyGia);

                excelSheet.Cells["D176"].Value = Format(chenhLechDanhGiaLaiTaiSan);

                excelSheet.Cells["D177"].Value = Format(loiNhuanChuaPhanPhoi);
                excelSheet.Cells["D178"].Value = Format(loiNhuanNamNay);
                excelSheet.Cells["D179"].Value = Format(loiNhuanNamTruoc);

                excelSheet.Cells["D181"].Value = Format(tongNoPhaiTraVonChuSoHuu);


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