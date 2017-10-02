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
    public class G01254Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();

        private readonly clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan _clsBaoCaoTinhHinhHuyDongVon;
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;
        public G01254Controller()
        {
            _clsBangCanDoiTaiKhoanKeToan= new ClsBangCanDoiTaiKhoanKeToan();
            _clsBaoCaoTinhHinhHuyDongVon = new clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan();
        }
        
        private IEnumerable<clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan> GetDataPrevMonth(string maChiNhanh, string ngayDuLieu)
        {
            return _clsBaoCaoTinhHinhHuyDongVon.GetAllData(maChiNhanh, ngayDuLieu);
        }

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
            //----------------------------------------------------------------------------
            var laiSuatList = new clsG01254LaiSuat();
            var laiSuatTienGui = laiSuatList.GetAll(ngayDuLieu);
            var bangCanDoi = _clsBangCanDoiTaiKhoanKeToan.GetAllData(ngayDuLieu);
            foreach (var item in phamViBaoCao)
            {
                var maChiNhanh = item.MA_DVI;
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

                var clsTinhHinhHuyDongTienGui = GetDataPrevMonth(maChiNhanh, ngayDuLieu);                

                var noTK801 = (from a in bangCanDoi where a.F03 == "801" && a.F10 == maChiNhanh select a.F06).FirstOrDefault();
                var coTK801 = (from a in bangCanDoi where a.F03 == "801" && a.F10 == maChiNhanh select a.F07).FirstOrDefault();

                var coDauKyTK423 = (from a in bangCanDoi where a.F03 == "423" && a.F10 == maChiNhanh select a.F05).FirstOrDefault();
                var coCuoiKyTK423 = (from a in bangCanDoi where a.F03 == "423" && a.F10 == maChiNhanh select a.F09).FirstOrDefault();

                var tong423 = coDauKyTK423 + coCuoiKyTK423;

                decimal laiSuatBinhQuan = 0;
                if (tong423 > 0)
                {
                    laiSuatBinhQuan = Math.Round(((noTK801 - coTK801) / (tong423 / 2)) * 100 * 12, 2);
                }
                var laiSuatTKQD = laiSuatTienGui.Where(p => p.MA_LAI_SUAT == "TKQD" && p.KY_HAN == null).FirstOrDefault();
                var laiSuatTKTN = laiSuatTienGui.Where(p => p.MA_LAI_SUAT == "TKTN" && p.KY_HAN == null).FirstOrDefault();
                var laiSuatDuoi1Thang = laiSuatTienGui.Where(p => p.MA_LAI_SUAT == "DUOI1THANG" && p.KY_HAN == 1).FirstOrDefault();
                var laiSuat1Thang = laiSuatTienGui.Where(p => p.KY_HAN == 1 && p.MA_LAI_SUAT == "TK1THANG").FirstOrDefault();
                var laiSuat2Thang = laiSuatTienGui.Where(p => p.KY_HAN == 2).FirstOrDefault();
                var laiSuat3Thang = laiSuatTienGui.Where(p => p.KY_HAN == 3).FirstOrDefault();
                var laiSuat4Thang = laiSuatTienGui.Where(p => p.KY_HAN == 4).FirstOrDefault();
                var laiSuat5Thang = laiSuatTienGui.Where(p => p.KY_HAN == 5).FirstOrDefault();
                var laiSuat6Thang = laiSuatTienGui.Where(p => p.KY_HAN == 6).FirstOrDefault();
                var laiSuat7Thang = laiSuatTienGui.Where(p => p.KY_HAN == 7).FirstOrDefault();
                var laiSuat8Thang = laiSuatTienGui.Where(p => p.KY_HAN == 8).FirstOrDefault();
                var laiSuat9Thang = laiSuatTienGui.Where(p => p.KY_HAN == 9).FirstOrDefault();
                var laiSuat12Thang = laiSuatTienGui.Where(p => p.KY_HAN == 12).FirstOrDefault();
                var laiSuat15Thang = laiSuatTienGui.Where(p => p.KY_HAN == 15).FirstOrDefault();
                var laiSuat18Thang = laiSuatTienGui.Where(p => p.KY_HAN == 18).FirstOrDefault();
                var laiSuat24Thang = laiSuatTienGui.Where(p => p.KY_HAN == 24).FirstOrDefault();
                var laiSuat36Thang = laiSuatTienGui.Where(p => p.KY_HAN == 36).FirstOrDefault();
                var duTienGuiKhongKyHanTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T02" select a.SO_TIEN).Sum();
                var duTienGuiTu60ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T01" select a.SO_TIEN).Sum();
                var duTienGuiDuoi1ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN < 1 select a.SO_TIEN).Sum();
                var duTienGuiTu1Den3ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 1 && a.KY_HAN < 3 select a.SO_TIEN).Sum();
                var duTienGuiTu3Den6ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 3 && a.KY_HAN < 6 select a.SO_TIEN).Sum();
                var duTienGuiTu6Den9ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 6 && a.KY_HAN < 9 select a.SO_TIEN).Sum();
                var duTienGuiTu9Den12ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 9 && a.KY_HAN < 12 select a.SO_TIEN).Sum();
                var duTienGuiTu12Den24ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 12 && a.KY_HAN < 24 select a.SO_TIEN).Sum();
                var duTienGuiTu24Den60ThangTVien = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "TVIEN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 24 && a.KY_HAN < 60 select a.SO_TIEN).Sum();
                var tongTienGuiTVien = (duTienGuiKhongKyHanTVien + duTienGuiDuoi1ThangTVien + duTienGuiTu1Den3ThangTVien +
                    duTienGuiTu3Den6ThangTVien + duTienGuiTu6Den9ThangTVien + duTienGuiTu9Den12ThangTVien +
                    duTienGuiTu12Den24ThangTVien + duTienGuiTu24Den60ThangTVien + duTienGuiTu60ThangTVien);
                var duTienGuiKhongKyHanCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T02" select a.SO_TIEN).Sum();
                var duTienGuiTu60ThangCNhan1 = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T01" select a.SO_TIEN).Sum();
                var duTienGuiTu60ThangCNhan2 = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 60 select a.SO_TIEN).Sum();
                var duTienGuiDuoi1ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN < 1 select a.SO_TIEN).Sum();
                var duTienGuiTu1Den3ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 1 && a.KY_HAN < 3 select a.SO_TIEN).Sum();
                var duTienGuiTu3Den6ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 3 && a.KY_HAN < 6 select a.SO_TIEN).Sum();
                var duTienGuiTu6Den9ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 6 && a.KY_HAN < 9 select a.SO_TIEN).Sum();
                var duTienGuiTu9Den12ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 9 && a.KY_HAN < 12 select a.SO_TIEN).Sum();
                var duTienGuiTu12Den24ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 12 && a.KY_HAN < 24 select a.SO_TIEN).Sum();
                var duTienGuiTu24Den60ThangCNhan = (from a in clsTinhHinhHuyDongTienGui where a.MA_KHANG_LOAI == "CNHAN" && a.MA_NHOM_SP == "T04" && a.KY_HAN >= 24 && a.KY_HAN < 60 select a.SO_TIEN).Sum();

                var tongTienGuiCNhan = (duTienGuiKhongKyHanCNhan + duTienGuiTu60ThangCNhan1 + duTienGuiTu60ThangCNhan2 + duTienGuiDuoi1ThangCNhan
                    + duTienGuiTu1Den3ThangCNhan + duTienGuiTu3Den6ThangCNhan + duTienGuiTu6Den9ThangCNhan + duTienGuiTu9Den12ThangCNhan +
                    duTienGuiTu12Den24ThangCNhan + duTienGuiTu24Den60ThangCNhan);
                excelSheet.Cells["D18"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(tongTienGuiCNhan / dviTinh, 1));
                excelSheet.Cells["D19"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiKhongKyHanCNhan / dviTinh, 1));
                excelSheet.Cells["D20"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiDuoi1ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D21"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu1Den3ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D22"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu3Den6ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D23"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu6Den9ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D24"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu9Den12ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D25"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu12Den24ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D26"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiTu24Den60ThangCNhan / dviTinh, 1));
                excelSheet.Cells["D27"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round((duTienGuiTu60ThangCNhan1 + duTienGuiTu60ThangCNhan2) / dviTinh, 1));
                excelSheet.Cells["D28"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round((tongTienGuiTVien) / dviTinh, 1));
                excelSheet.Cells["D29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round((duTienGuiKhongKyHanTVien) / dviTinh, 1));
                excelSheet.Cells["D30"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                           "{0:00.0}", Math.Round(duTienGuiDuoi1ThangTVien / dviTinh, 1));
                excelSheet.Cells["D31"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu1Den3ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D32"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu3Den6ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu6Den9ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu9Den12ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu12Den24ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu24Den60ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D37"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                          "{0:00.0}", Math.Round((duTienGuiTu60ThangTVien) / dviTinh, 1));
                excelSheet.Cells["D58"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                         "{0:00.0}", Math.Round((tongTienGuiTVien + tongTienGuiCNhan) / dviTinh, 1));
                excelSheet.Cells["E18"].Value = excelSheet.Cells["E28"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", laiSuatBinhQuan);
                excelSheet.Cells["E19"].Value = excelSheet.Cells["E29"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.00}", laiSuatTKTN.LAI_SUAT);
                excelSheet.Cells["E20"].Value = excelSheet.Cells["E30"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                       "{0:00.00}", laiSuatDuoi1Thang.LAI_SUAT);
                excelSheet.Cells["E21"].Value = excelSheet.Cells["E31"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                      "{0:00.00}", laiSuat3Thang.LAI_SUAT);
                excelSheet.Cells["E22"].Value = excelSheet.Cells["E32"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                      "{0:00.00}", laiSuat5Thang.LAI_SUAT);
                excelSheet.Cells["E23"].Value = excelSheet.Cells["E33"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                      "{0:00.00}", laiSuat9Thang.LAI_SUAT);
                excelSheet.Cells["E24"].Value = excelSheet.Cells["E34"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                      "{0:00.00}", laiSuat12Thang.LAI_SUAT);
                excelSheet.Cells["E25"].Value = excelSheet.Cells["E35"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                  "{0:00.00}", laiSuat24Thang.LAI_SUAT);
                excelSheet.Cells["E26"].Value = excelSheet.Cells["E36"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuat36Thang.LAI_SUAT);
                excelSheet.Cells["E27"].Value = excelSheet.Cells["E37"].Value = string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                 "{0:00.00}", laiSuatTKQD.LAI_SUAT);
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