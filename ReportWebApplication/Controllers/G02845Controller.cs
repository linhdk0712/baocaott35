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
    public class G02845Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();
        private readonly decimal dviTinhs = ClsDonViTinh.DviTinhDong();
        private readonly ClsBangCanDoiTaiKhoanKeToan _clsBangCanDoiTaiKhoanKeToan;
        private readonly ClsBaoCaoRuiRoThanhKhoan _clsBaoCaoRuiRoThanhKhoan;
        private readonly clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan;
        public G02845Controller()
        {
            _clsBangCanDoiTaiKhoanKeToan = new ClsBangCanDoiTaiKhoanKeToan();
            _clsBaoCaoRuiRoThanhKhoan = new ClsBaoCaoRuiRoThanhKhoan();
            _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan = new clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan();
        }
        // GET: G02845
       
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var ngayDuLieu = (DateTime.Parse(denNgay)).ToString("yyyyMMdd");
            var dtFirstEndOfQuarter = clsDatesOfQuarter.DatesOfQuarter(denNgay);
            var endDateOfQuarter = dtFirstEndOfQuarter[1].ToString("yyyyMMdd");
            var ngayDauThang = clsGetLastDayOfMonth.GetFirstDayOfMont(((DateTime.Parse(denNgay))).Month, (DateTime.Parse(denNgay)).Year);
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
                //-------------------------------------------------------------------------------------------
                var param = new DynamicParameters();
                param.Add("@ngayDuLieu", ngayDuLieu);
                var tienGui = _clsBaoCaoRuiRoThanhKhoan.TaiDanhSachSoTienGui<ClsRuiRoThanhKhoan>(param);
                var bangCanDoiKeToan = _clsBangCanDoiTaiKhoanKeToan.GetAllDataCap4(maChiNhanh, ngayDauThang, endDateOfQuarter);
                var clsVayNgoai = _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan.GetAllData(maChiNhanh, ngayDuLieu);
                //-------------------------------------------------------------------------------------------
                var tk10 = (from a in bangCanDoiKeToan where a.F03 == "10" select a.F08).FirstOrDefault();
                var tk1311 = (from a in bangCanDoiKeToan where a.F03 == "1311" select a.F08).FirstOrDefault();
                var tk1321 = (from a in bangCanDoiKeToan where a.F03 == "1321" select a.F08).FirstOrDefault();
                var tk1312 = (from a in bangCanDoiKeToan where a.F03 == "1312" select a.F08).FirstOrDefault();
                var tk2112 = (from a in bangCanDoiKeToan where a.F03 == "2112" select a.F08).FirstOrDefault();
                var tk2113 = (from a in bangCanDoiKeToan where a.F03 == "2113" select a.F08).FirstOrDefault();
                var tk2114 = (from a in bangCanDoiKeToan where a.F03 == "2114" select a.F08).FirstOrDefault();
                var tk2115 = (from a in bangCanDoiKeToan where a.F03 == "2115" select a.F08).FirstOrDefault();
                var tk2122 = (from a in bangCanDoiKeToan where a.F03 == "2122" select a.F08).FirstOrDefault();
                var tk2123 = (from a in bangCanDoiKeToan where a.F03 == "2123" select a.F08).FirstOrDefault();
                var tk2124 = (from a in bangCanDoiKeToan where a.F03 == "2124" select a.F08).FirstOrDefault();
                var tk2125 = (from a in bangCanDoiKeToan where a.F03 == "2125" select a.F08).FirstOrDefault();
                var tk21111 = (from a in bangCanDoiKeToan where a.F03 == "21111" select a.F08).FirstOrDefault();
                var tk21112 = (from a in bangCanDoiKeToan where a.F03 == "21112" select a.F08).FirstOrDefault();
                var tk21113 = (from a in bangCanDoiKeToan where a.F03 == "21113" select a.F08).FirstOrDefault();
                var tk21114 = (from a in bangCanDoiKeToan where a.F03 == "21114" select a.F08).FirstOrDefault();
                var tk21115 = (from a in bangCanDoiKeToan where a.F03 == "21115" select a.F08).FirstOrDefault();
                var tk21116 = (from a in bangCanDoiKeToan where a.F03 == "21116" select a.F08).FirstOrDefault();
                var tk2121 = (from a in bangCanDoiKeToan where a.F03 == "2121" select a.F08).FirstOrDefault();
                var tk31 = (from a in bangCanDoiKeToan where a.F03 == "31" select a.F08).FirstOrDefault();
                var tk35 = (from a in bangCanDoiKeToan where a.F03 == "35" select a.F08).FirstOrDefault();
                var tk36 = (from a in bangCanDoiKeToan where a.F03 == "36" select a.F08).FirstOrDefault();
                var tk366 = (from a in bangCanDoiKeToan where a.F03 == "366" select a.F08).FirstOrDefault();               
                var tk38 = (from a in bangCanDoiKeToan where a.F03 == "38" select a.F08).FirstOrDefault();
                var tk45 = (from a in bangCanDoiKeToan where a.F03 == "45" select a.F09).FirstOrDefault();
                var tk46 = (from a in bangCanDoiKeToan where a.F03 == "46" select a.F09).FirstOrDefault();
                var tk466 = (from a in bangCanDoiKeToan where a.F03 == "466" select a.F09).FirstOrDefault();
                var tk48 = (from a in bangCanDoiKeToan where a.F03 == "48" select a.F09).FirstOrDefault();
                var tk49 = (from a in bangCanDoiKeToan where a.F03 == "49" select a.F09).FirstOrDefault();
                var tkNo30 = (from a in bangCanDoiKeToan where a.F03 == "30" select a.F08).FirstOrDefault();
                var tkCo30 = (from a in bangCanDoiKeToan where a.F03 == "30" select a.F09).FirstOrDefault();
                //-------------------------------------------------------------------------------------------
                var tienMatDaQuy = tk10;

                var tienGui_TCTDKhongChiuLai = tk1311 + tk1321;
                var tienGui_TCTDDuoi1Thang = tk1312;
                var tienGui_TCTDTongCong = tienGui_TCTDDuoi1Thang + tienGui_TCTDKhongChiuLai;

                var choVay_QuaHan= tk2112 + tk2113 + tk2114+tk2115 + tk2122 + tk2123 + tk2124 + tk2125;               
                var choVay_Tren3Den6Thang = tk21111 + tk21112 + tk21115 + tk21116;
                var choVay_Tren6Den12Thang = tk21114 + tk21113;
                var choVay_Tren12Den60Thang = tk2121;
                var choVay_Tren60Thang = 0;
                var choVay_TongCong = choVay_QuaHan + choVay_Tren3Den6Thang + choVay_Tren6Den12Thang + choVay_Tren12Den60Thang + choVay_Tren60Thang;

                var taiSanCoDinhVaBDS_KhongChiuLai = tkNo30 - tkCo30;

                var taiSanCoKhac_KhongChiuLai =  tk31 + tk35+tk36-tk366+tk38;

                var tongTaiSan_QuaHan = choVay_QuaHan;
                var tongTaiSan_KhongChiuLai = tienMatDaQuy + tienGui_TCTDKhongChiuLai + taiSanCoKhac_KhongChiuLai + taiSanCoDinhVaBDS_KhongChiuLai;
                var tongTaiSan_Duoi1Thang = tienGui_TCTDDuoi1Thang;
                var tongTaiSan_Tu1Den3Thang =  0;
                var tongTaiSan_TuTren3Den6Thang = choVay_Tren3Den6Thang;
                var tongTaiSan_TuTren6Den12Thang = choVay_Tren6Den12Thang;
                var tongTaiSan_TuTren12Den60Thang = choVay_Tren12Den60Thang;
                var tongTaiSan_TuTren60Thang = choVay_Tren60Thang;
                var tongTaiSan_TongCong = tongTaiSan_QuaHan + tongTaiSan_KhongChiuLai + tongTaiSan_Duoi1Thang+ tongTaiSan_Tu1Den3Thang+ tongTaiSan_TuTren3Den6Thang+ tongTaiSan_TuTren6Den12Thang+ tongTaiSan_TuTren12Den60Thang+ tongTaiSan_TuTren60Thang;

                var tienGui_Duoi1Thang = (from a in tienGui where a.MA_NHOM_SP == "T04" && a.KY_HAN < 1 select a.SO_TIEN).Sum() + (from a in tienGui where a.MA_NHOM_SP == "T02" select a.SO_TIEN).Sum();
                var tienGui_Tu1Den3Thang = (from a in tienGui where a.KY_HAN >= 1 && a.KY_HAN <= 3 select a.SO_TIEN).Sum();
                var tienGui_TuTren3Den6Thang = (from a in tienGui where a.KY_HAN > 3 && a.KY_HAN <= 6 select a.SO_TIEN).Sum();
                var tienGui_TuTren6Den12Thang = (from a in tienGui where a.KY_HAN > 6 && a.KY_HAN <= 12 select a.SO_TIEN).Sum();
                var tienGui_TuTren12Den60Thang = (from a in tienGui where a.KY_HAN > 12 && a.KY_HAN <= 60 select a.SO_TIEN).Sum() + (from a in tienGui where a.MA_NHOM_SP == "T01" select a.SO_TIEN).Sum();
                var tienGui_TuTren60Thang = (from a in tienGui where a.KY_HAN > 60 select a.SO_TIEN).Sum();
                var tienGui_TongCong = tienGui_Duoi1Thang + tienGui_Tu1Den3Thang + tienGui_TuTren3Den6Thang + tienGui_TuTren12Den60Thang + tienGui_TuTren60Thang + tienGui_TuTren6Den12Thang;

                var vayNgoai_Duoi1Thang = (from a in clsVayNgoai where a.KY_HAN < 1 select a.DU_NO).Sum();
                var vayNgoai_Tu1Den3Thang = (from a in clsVayNgoai where a.KY_HAN >= 1 && a.KY_HAN <= 3 select a.DU_NO).Sum();
                var vayNgoai_TuTren3Den6Thang = (from a in clsVayNgoai where a.KY_HAN >3 && a.KY_HAN <= 6 select a.DU_NO).Sum();
                var vayNgoai_TuTren6Den12Thang = (from a in clsVayNgoai where a.KY_HAN > 6 && a.KY_HAN <= 12 select a.DU_NO).Sum();
                var vayNgoai_TuTren12Den60Thang = (from a in clsVayNgoai where a.KY_HAN > 12 && a.KY_HAN <= 60 select a.DU_NO).Sum();
                var vayNgoai_TuTren60Thang = (from a in clsVayNgoai where a.KY_HAN > 60 select a.DU_NO).Sum();
                var vayNgoai_TongCong = vayNgoai_Duoi1Thang + vayNgoai_Tu1Den3Thang + vayNgoai_TuTren3Den6Thang + vayNgoai_TuTren6Den12Thang + vayNgoai_TuTren12Den60Thang + vayNgoai_TuTren60Thang;

                var cacKhoanNoKhac_KhongChiuLai= tk45 + tk46 - tk466 + tk49+tk48;
                var cacKhoanNoKhac_TongCong = cacKhoanNoKhac_KhongChiuLai;

                var noPhaiTra_QuaHan = 0;
                var noPhaiTra_KhongChiuLai = cacKhoanNoKhac_KhongChiuLai;
                var noPhaiTra_Duoi1Thang = tienGui_Duoi1Thang + vayNgoai_Duoi1Thang;
                var noPhaiTra_Tu1Den3Thang = tienGui_Tu1Den3Thang + vayNgoai_Tu1Den3Thang;
                var noPhaiTra_TuTren3Den6Thang = tienGui_TuTren3Den6Thang + vayNgoai_TuTren3Den6Thang;
                var noPhaiTra_TuTren6Den12Thang = tienGui_TuTren6Den12Thang + vayNgoai_TuTren6Den12Thang;
                var noPhaiTra_TuTren12Den60Thang = tienGui_TuTren12Den60Thang + vayNgoai_TuTren12Den60Thang;
                var noPhaiTra_TuTren60Thang = tienGui_TuTren60Thang + vayNgoai_TuTren60Thang;
                var noPhaiTra_TongCong = noPhaiTra_QuaHan + noPhaiTra_KhongChiuLai + noPhaiTra_Duoi1Thang + noPhaiTra_Tu1Den3Thang + noPhaiTra_TuTren3Den6Thang + noPhaiTra_TuTren6Den12Thang + noPhaiTra_TuTren12Den60Thang + noPhaiTra_TuTren60Thang;

                var laiSuatNoiBang_QuaHan = tongTaiSan_QuaHan - noPhaiTra_QuaHan;
                var laiSuatNoiBang_KhongChiuLai = tongTaiSan_KhongChiuLai - noPhaiTra_KhongChiuLai;
                var laiSuatNoiBang_Duoi1Thang = tongTaiSan_Duoi1Thang - noPhaiTra_Duoi1Thang;
                var laiSuatNoiBang_Tu1Den3Thang = tongTaiSan_Tu1Den3Thang - noPhaiTra_Tu1Den3Thang;
                var laiSuatNoiBang_TuTren3Den6Thang = tongTaiSan_TuTren3Den6Thang - noPhaiTra_TuTren3Den6Thang;
                var laiSuatNoiBang_TuTren6Den12Thang = tongTaiSan_TuTren6Den12Thang - noPhaiTra_TuTren6Den12Thang;
                var laiSuatNoiBang_TuTren12Den60Thang = tongTaiSan_TuTren12Den60Thang - noPhaiTra_TuTren12Den60Thang;
                var laiSuatNoiBang_TuTren60Thang = tongTaiSan_TuTren60Thang - noPhaiTra_TuTren60Thang;
                var laiSuatNoiBang_TongCong = laiSuatNoiBang_QuaHan + laiSuatNoiBang_KhongChiuLai + laiSuatNoiBang_Duoi1Thang + laiSuatNoiBang_Tu1Den3Thang + laiSuatNoiBang_TuTren3Den6Thang + laiSuatNoiBang_TuTren6Den12Thang + laiSuatNoiBang_TuTren12Den60Thang + laiSuatNoiBang_TuTren60Thang;

                excelSheet.Cells["D19"].Value = Format(tienMatDaQuy);

                excelSheet.Cells["D21"].Value = Format(tienGui_TCTDKhongChiuLai);
                excelSheet.Cells["E21"].Value = Format(tienGui_TCTDDuoi1Thang);
                excelSheet.Cells["E21"].Value = Format(tienGui_TCTDDuoi1Thang);

                excelSheet.Cells["C22"].Value = Format(choVay_QuaHan);
                excelSheet.Cells["G22"].Value = Format(choVay_Tren3Den6Thang);
                excelSheet.Cells["H22"].Value = Format(choVay_Tren6Den12Thang);
                excelSheet.Cells["I22"].Value = Format(choVay_Tren12Den60Thang);
                excelSheet.Cells["J22"].Value = Format(choVay_Tren60Thang);

                excelSheet.Cells["D24"].Value = Format(taiSanCoDinhVaBDS_KhongChiuLai);

                excelSheet.Cells["D25"].Value = Format(taiSanCoKhac_KhongChiuLai);

                excelSheet.Cells["C26"].Value = Format(tongTaiSan_QuaHan);
                excelSheet.Cells["D26"].Value = Format(tongTaiSan_KhongChiuLai);
                excelSheet.Cells["E26"].Value = Format(tongTaiSan_Duoi1Thang);
                excelSheet.Cells["F26"].Value = Format(tongTaiSan_Tu1Den3Thang);
                excelSheet.Cells["G26"].Value = Format(tongTaiSan_TuTren3Den6Thang);
                excelSheet.Cells["H26"].Value = Format(tongTaiSan_TuTren6Den12Thang);
                excelSheet.Cells["I26"].Value = Format(tongTaiSan_TuTren12Den60Thang);
                excelSheet.Cells["J26"].Value = Format(tongTaiSan_TuTren60Thang);

                excelSheet.Cells["E29"].Value = Format(tienGui_Duoi1Thang);
                excelSheet.Cells["F29"].Value = Format(tienGui_Tu1Den3Thang);
                excelSheet.Cells["G29"].Value = Format(tienGui_TuTren3Den6Thang);
                excelSheet.Cells["H29"].Value = Format(tienGui_TuTren6Den12Thang);
                excelSheet.Cells["I29"].Value = Format(tienGui_TuTren12Den60Thang);
                excelSheet.Cells["J29"].Value = Format(tienGui_TuTren60Thang);

                excelSheet.Cells["E30"].Value = Format(vayNgoai_Duoi1Thang);
                excelSheet.Cells["F30"].Value = Format(vayNgoai_Tu1Den3Thang);
                excelSheet.Cells["G30"].Value = Format(vayNgoai_TuTren3Den6Thang);
                excelSheet.Cells["H30"].Value = Format(vayNgoai_TuTren6Den12Thang);
                excelSheet.Cells["I30"].Value = Format(vayNgoai_TuTren12Den60Thang);
                excelSheet.Cells["J30"].Value = Format(vayNgoai_TuTren60Thang);

                excelSheet.Cells["D31"].Value = Format(cacKhoanNoKhac_KhongChiuLai);
                excelSheet.Cells["K31"].Value = Format(cacKhoanNoKhac_TongCong);

                excelSheet.Cells["C32"].Value = Format(noPhaiTra_QuaHan);
                excelSheet.Cells["D32"].Value = Format(noPhaiTra_KhongChiuLai);
                excelSheet.Cells["E32"].Value = Format(noPhaiTra_Duoi1Thang);
                excelSheet.Cells["F32"].Value = Format(noPhaiTra_Tu1Den3Thang);
                excelSheet.Cells["G32"].Value = Format(noPhaiTra_TuTren3Den6Thang);
                excelSheet.Cells["H32"].Value = Format(noPhaiTra_TuTren6Den12Thang);
                excelSheet.Cells["I32"].Value = Format(noPhaiTra_TuTren12Den60Thang);
                excelSheet.Cells["J32"].Value = Format(noPhaiTra_TuTren60Thang);

                excelSheet.Cells["C33"].Value = Format(laiSuatNoiBang_QuaHan);
                excelSheet.Cells["D33"].Value = Format(laiSuatNoiBang_KhongChiuLai);
                excelSheet.Cells["E33"].Value = Format(laiSuatNoiBang_Duoi1Thang);
                excelSheet.Cells["F33"].Value = Format(laiSuatNoiBang_Tu1Den3Thang);
                excelSheet.Cells["G33"].Value = Format(laiSuatNoiBang_TuTren3Den6Thang);
                excelSheet.Cells["H33"].Value = Format(laiSuatNoiBang_TuTren6Den12Thang);
                excelSheet.Cells["I33"].Value = Format(laiSuatNoiBang_TuTren12Den60Thang);
                excelSheet.Cells["J33"].Value = Format(laiSuatNoiBang_TuTren60Thang);

                excelSheet.Cells["C35"].Value = Format(laiSuatNoiBang_QuaHan);
                excelSheet.Cells["D35"].Value = Format(laiSuatNoiBang_KhongChiuLai);
                excelSheet.Cells["E35"].Value = Format(laiSuatNoiBang_Duoi1Thang);
                excelSheet.Cells["F35"].Value = Format(laiSuatNoiBang_Tu1Den3Thang);
                excelSheet.Cells["G35"].Value = Format(laiSuatNoiBang_TuTren3Den6Thang);
                excelSheet.Cells["H35"].Value = Format(laiSuatNoiBang_TuTren6Den12Thang);
                excelSheet.Cells["I35"].Value = Format(laiSuatNoiBang_TuTren12Den60Thang);
                excelSheet.Cells["J35"].Value = Format(laiSuatNoiBang_TuTren60Thang);

                excelSheet.Cells["K19"].Value = Format(tienMatDaQuy);
                excelSheet.Cells["K21"].Value = Format(tienGui_TCTDTongCong);
                excelSheet.Cells["K22"].Value = Format(choVay_TongCong);
                excelSheet.Cells["K24"].Value = Format(taiSanCoDinhVaBDS_KhongChiuLai);
                excelSheet.Cells["K25"].Value = Format(taiSanCoKhac_KhongChiuLai);
                excelSheet.Cells["K26"].Value = Format(tongTaiSan_TongCong);
                excelSheet.Cells["K29"].Value = Format(tienGui_TongCong);
                excelSheet.Cells["K30"].Value = Format(vayNgoai_TongCong);
                excelSheet.Cells["K32"].Value = Format(noPhaiTra_TongCong);
                excelSheet.Cells["K33"].Value = Format(laiSuatNoiBang_TongCong);
                excelSheet.Cells["K35"].Value = Format(laiSuatNoiBang_TongCong);

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
                        "{0:00.0}", Math.Round(data / dviTinhs, 1));
        }
    }
}