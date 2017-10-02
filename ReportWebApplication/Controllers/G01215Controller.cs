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
    public class G01215Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly decimal _dviTinhs = ClsDonViTinh.DviTinhTrieuDong();
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;
        public G01215Controller()
        {
            _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
        }
        // GET: G01215
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            clsGetLastDayOfMonth.GetFirstDayOfMont(((DateTime.Parse(denNgay))).Month, (DateTime.Parse(denNgay)).Year);
            var daysOfQuarter = clsDatesOfQuarter.DatesOfQuarter(denNgay);
            var endDateOfThisQuarter = daysOfQuarter[2].ToString("yyyyMMdd");
            var _15DateOfThisQuarter = daysOfQuarter[1].ToString("yyyyMMdd");
            var endDateOfPreMonth = daysOfQuarter[0].AddDays(-1);
            var daysOfPrevQuater = clsDatesOfQuarter.DatesOfQuarter(endDateOfPreMonth.ToString(CultureInfo.CurrentCulture));
            var _15DateOfPreMonth = daysOfPrevQuater[1].ToString("yyyyMMdd");
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            //-------------------------------------------------------------------------------------------               
            var bangCanDoiKeToanHienTai = _clsBangCanDoiTaiKhoanKeToan.GetAllData( endDateOfThisQuarter);
            var bangCanDoiKeToanGiuaThangQuyNay = _clsBangCanDoiTaiKhoanKeToan.GetAllData( _15DateOfThisQuarter);
            var bangCanDoiKeToanQuyTruoc = _clsBangCanDoiTaiKhoanKeToan.GetAllData( endDateOfPreMonth.ToString("yyyyMMdd"));
            var bangCanDoiKeToanGiuaThangQuyTruoc = _clsBangCanDoiTaiKhoanKeToan.GetAllData( _15DateOfPreMonth);
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
                var noTaiKhoan2112QuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "2112" && x.F10 == maChiNhanh).Select(x=>x.F08).FirstOrDefault();
                var noTaiKhoan2122QuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "2122" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var noTaiKhoan2113QuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "2113" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var noTaiKhoan2123QuyTruoc = bangCanDoiKeToanQuyTruoc.Where(x => x.F03 == "2123" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                var noTaiKhoan2114QuyTruoc = (from a in bangCanDoiKeToanQuyTruoc where a.F03 == "2114" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var noTaiKhoan2124QuyTruoc = (from a in bangCanDoiKeToanQuyTruoc where a.F03 == "2124" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var noTaiKhoan2115QuyTruoc = (from a in bangCanDoiKeToanQuyTruoc where a.F03 == "2115" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var noTaiKhoan2125QuyTruoc = (from a in bangCanDoiKeToanQuyTruoc where a.F03 == "2125" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var noTaiKhoan2111HienTai = (from a in bangCanDoiKeToanHienTai where a.F03 == "2111" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var noTaiKhoan2112HienTai = (from a in bangCanDoiKeToanHienTai where a.F03 == "2112" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var noTaiKhoan2113HienTai = (from a in bangCanDoiKeToanHienTai where a.F03 == "2113" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var noTaiKhoan2114HienTai = (from a in bangCanDoiKeToanHienTai where a.F03 == "2114" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var noTaiKhoan2121HienTai = (from a in bangCanDoiKeToanHienTai where a.F03 == "2121" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var noTaiKhoan2122HienTai = (from a in bangCanDoiKeToanHienTai where a.F03 == "2122" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var noTaiKhoan2123HienTai = (from a in bangCanDoiKeToanHienTai where a.F03 == "2123" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                var noTaiKhoan2124HienTai = (from a in bangCanDoiKeToanHienTai where a.F03 == "2124" && a.F10 == maChiNhanh select a.F08).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var coTaiKhoan2191Ngay15HienTai = (from a in bangCanDoiKeToanGiuaThangQuyNay where a.F03 == "2191" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                var coTaiKhoan2192Ngay15HienTai = (from a in bangCanDoiKeToanGiuaThangQuyNay where a.F03 == "2192" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var coTaiKhoan2191Ngay15QuyTruoc = bangCanDoiKeToanGiuaThangQuyTruoc.Where(x => x.F03 == "2191" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                var coTaiKhoan2192Ngay15QuyTruoc = bangCanDoiKeToanGiuaThangQuyTruoc.Where(x => x.F03 == "2192" && x.F10 == maChiNhanh).Select(x => x.F09).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var phatSinhCoTaiKhoan971HienTai = bangCanDoiKeToanHienTai.Where(x => x.F03 == "971" && x.F10 == maChiNhanh).Select(x => x.F07).FirstOrDefault();
                var noTaiKhoan971HienTai = bangCanDoiKeToanHienTai.Where(x => x.F03 == "971" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                //var phatSinhCoTaiKhoan2115HienTai = bangCanDoiKeToanHienTai.Where(x => x.F03 == "2115").Select(x => x.F07).FirstOrDefault();
                //var phatSinhCoTaiKhoan2125HienTai = bangCanDoiKeToanHienTai.Where(x => x.F03 == "2125").Select(x => x.F07).FirstOrDefault();
                var noTaiKhoan2191HienTai = bangCanDoiKeToanHienTai.Where(x => x.F03 == "2191" && x.F10 == maChiNhanh).Select(x => x.F08).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var duPhongCuThePhaiTrich = (decimal)0.02 * (noTaiKhoan2113QuyTruoc + noTaiKhoan2122QuyTruoc) + (decimal)0.25 * (noTaiKhoan2113QuyTruoc + noTaiKhoan2123QuyTruoc)
                    + (decimal)0.5*(noTaiKhoan2114QuyTruoc + noTaiKhoan2124QuyTruoc) + noTaiKhoan2115QuyTruoc + noTaiKhoan2125QuyTruoc ;

                var duPhongChungPhaiTrich = (decimal)0.05 * (noTaiKhoan2111HienTai + noTaiKhoan2112HienTai + noTaiKhoan2113HienTai +
                    noTaiKhoan2114HienTai + noTaiKhoan2121HienTai + noTaiKhoan2122HienTai + noTaiKhoan2123HienTai + noTaiKhoan2124HienTai);

                var tongDuPhongPhaiTrich = duPhongCuThePhaiTrich + duPhongChungPhaiTrich;

                var soDuDuPhongCuTheConLai = coTaiKhoan2191Ngay15QuyTruoc;

                var soDuDuPhongChungConLai = coTaiKhoan2192Ngay15QuyTruoc;

                var tongSoDuDuPhongConLai = soDuDuPhongChungConLai + soDuDuPhongCuTheConLai;

                var duPhongCuTheBoSung = duPhongCuThePhaiTrich - soDuDuPhongCuTheConLai;

                var duPhongChungBoSung = duPhongChungPhaiTrich - soDuDuPhongChungConLai;

                var tongDuPhongBoSung = duPhongCuTheBoSung + duPhongChungBoSung;

                var duPhongCuTheThucTrich = coTaiKhoan2191Ngay15HienTai;

                var duPhongChungThucTrich = coTaiKhoan2192Ngay15HienTai;

                var tongDuPhongThucTrich = duPhongCuTheThucTrich + duPhongChungThucTrich;

                var duPhongCuTheDaSuDung = noTaiKhoan2191HienTai;

                var duPhongChungDaSuDung = 0;

                var tongDuPhongDaSuDung = duPhongCuTheDaSuDung + duPhongChungDaSuDung;

                var duPhongCuTheConLai = duPhongCuThePhaiTrich - duPhongCuTheDaSuDung;

                var duPhongChungConLai = duPhongChungPhaiTrich - duPhongChungDaSuDung;

                var soTienThuHoiDuocTrongQuy = phatSinhCoTaiKhoan971HienTai;

                var soTienChuaThuHoiDuoc = noTaiKhoan971HienTai;

                var soTienTonThatHetThoiHan = 0;
                //-------------------------------------------------------------------------------------------
                excelSheet.Cells["D18"].Value = Format(tongDuPhongPhaiTrich);
                excelSheet.Cells["D19"].Value = Format(duPhongCuThePhaiTrich);

                excelSheet.Cells["D20"].Value = Format(duPhongChungPhaiTrich);
                excelSheet.Cells["D21"].Value = Format(tongSoDuDuPhongConLai);
                excelSheet.Cells["D22"].Value = Format(soDuDuPhongCuTheConLai);
                excelSheet.Cells["D23"].Value = Format(soDuDuPhongChungConLai);
                excelSheet.Cells["D24"].Value = Format(tongDuPhongBoSung);
                excelSheet.Cells["D25"].Value = Format(duPhongCuTheBoSung);
                excelSheet.Cells["D26"].Value = Format(duPhongChungBoSung);
                excelSheet.Cells["D27"].Value = Format(tongDuPhongThucTrich);
                excelSheet.Cells["D28"].Value = Format(duPhongCuTheThucTrich);
                excelSheet.Cells["D29"].Value = Format(duPhongChungThucTrich);

                excelSheet.Cells["D30"].Value = Format(tongDuPhongDaSuDung);
                excelSheet.Cells["D31"].Value = Format(duPhongCuTheDaSuDung);
                excelSheet.Cells["D32"].Value = Format(duPhongChungDaSuDung);
                excelSheet.Cells["D33"].Value = Format(tongSoDuDuPhongConLai);
                excelSheet.Cells["D34"].Value = Format(duPhongCuTheConLai);
                excelSheet.Cells["D35"].Value = Format(duPhongChungConLai);
                excelSheet.Cells["D36"].Value = Format(soTienThuHoiDuocTrongQuy);
                excelSheet.Cells["D37"].Value = Format(soTienChuaThuHoiDuoc);
                excelSheet.Cells["D38"].Value = Format(soTienTonThatHetThoiHan);
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