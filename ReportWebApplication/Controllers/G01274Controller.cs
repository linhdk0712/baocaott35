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
    public class G01274Controller : BaseController
    {
        private readonly string _reportName = clsControllerName.ControllerName();

        private readonly clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan;

        public G01274Controller()
        {
            _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan = new clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan();
        }

        // GET: G01274
        [HttpGet]
        public JsonResult GetXLSXReport(string denNgay)
        {
            var dinhKyBaoCao = _clsDinhKyBaoCao.DinhKyBaoCao(_reportName);
            var status = false;
            var reportCount = 0;
            var ngayDuLieu = (DateTime.Parse(denNgay)).ToString("yyyyMMdd");
            var ngayDauThang = clsGetLastDayOfMonth.GetFirstDayOfMont((DateTime.Parse(denNgay)).Month, (DateTime.Parse(denNgay)).Year);
            // tạo folder theo tháng
            var folderName = GetFolderName(denNgay, _reportName);
            var _phamViBaoCao = _clsSoLieuToanHeThongVaDonVi.BaoCaoToanHeThongOrTruSoChinh(_reportName);
            foreach (var machinhanh in _phamViBaoCao)
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
                var clsVayNgoai = _clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan.GetAllData(maChiNhanh, ngayDuLieu);
                var vayTCTD = clsVayNgoai.Where(p => p.LOAI_NGUON == "4424").OrderBy(p => p.NGAY_VAY);
                var startRow = 21;
                var sTT = 1;
                excelSheet.InsertRow(startRow, vayTCTD.Count(), startRow - 1);
                foreach (var item in vayTCTD)
                {
                    excelSheet.SetValue(startRow, 2, "I." + sTT);
                    excelSheet.SetValue(startRow, 3, item.TEN_NGUON_VON);
                    excelSheet.SetValue(startRow, 4, FormatString(item.SO_TIEN_VAY));
                    excelSheet.SetValue(startRow, 5, FormatString(item.DU_NO));
                    if (item.KY_HAN <= 12)
                    {
                        excelSheet.SetValue(startRow, 6, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow, 7, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow, 8, item.NGAY_VAY);
                        excelSheet.SetValue(startRow, 9, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow, 10, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow, 11, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    else
                    {
                        excelSheet.SetValue(startRow, 12, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow, 13, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow, 14, item.NGAY_VAY);
                        excelSheet.SetValue(startRow, 15, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow, 16, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow, 17, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    startRow++;
                    sTT++;
                }
                var sTT1 = 1;
                var startRow1 = startRow + 1;
                var vayTCKHAC = clsVayNgoai.Where(p => p.LOAI_NGUON == "4425").OrderBy(p => p.NGAY_VAY);
                excelSheet.InsertRow(startRow1, vayTCKHAC.Count(), startRow - 1);
                foreach (var item in vayTCKHAC)
                {
                    excelSheet.SetValue(startRow1, 2, "II." + sTT1);
                    excelSheet.SetValue(startRow1, 3, item.TEN_NGUON_VON);
                    excelSheet.SetValue(startRow1, 4, FormatString(item.SO_TIEN_VAY));
                    excelSheet.SetValue(startRow1, 5, FormatString(item.DU_NO));
                    if (item.KY_HAN <= 12)
                    {
                        excelSheet.SetValue(startRow1, 6, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow1, 7, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow1, 8, item.NGAY_VAY);
                        excelSheet.SetValue(startRow1, 9, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow1, 10, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow1, 11, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    else
                    {
                        excelSheet.SetValue(startRow1, 12, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow1, 13, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow1, 14, item.NGAY_VAY);
                        excelSheet.SetValue(startRow1, 15, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow1, 16, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow1, 17, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    startRow1++;
                    sTT1++;
                }
                var sTT2 = 1;
                var startRow2 = startRow1 + 1;
                var vayCaNhan = clsVayNgoai.Where(p => p.LOAI_NGUON == "4426").OrderBy(p => p.NGAY_VAY);
                excelSheet.InsertRow(startRow2, vayCaNhan.Count(), startRow - 1);
                foreach (var item in vayCaNhan)
                {
                    excelSheet.SetValue(startRow2, 2, "III." + sTT2);
                    excelSheet.SetValue(startRow2, 3, item.TEN_NGUON_VON);
                    excelSheet.SetValue(startRow2, 4, FormatString(item.SO_TIEN_VAY));
                    excelSheet.SetValue(startRow2, 5, FormatString(item.DU_NO));
                    if (item.KY_HAN <= 12)
                    {
                        excelSheet.SetValue(startRow2, 6, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow2, 7, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow2, 8, item.NGAY_VAY);
                        excelSheet.SetValue(startRow2, 9, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow2, 10, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow2, 11, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    else
                    {
                        excelSheet.SetValue(startRow2, 12, FormatString(item.SO_TIEN_VAY));
                        excelSheet.SetValue(startRow2, 13, FormatString(item.DU_NO));
                        excelSheet.SetValue(startRow2, 14, item.NGAY_VAY);
                        excelSheet.SetValue(startRow2, 15, item.NGAY_DAO_HAN);
                        excelSheet.SetValue(startRow2, 16, item.NGAY_CO_CAU);
                        excelSheet.SetValue(startRow2, 17, string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                            "{0:00.00}", item.LAI_SUAT));
                    }
                    startRow2++;
                    sTT2++;
                }
                var startRow3 = startRow2;
                excelSheet.SetValue(startRow3, 4, FormatString(clsVayNgoai.Sum(p => p.SO_TIEN_VAY)));
                excelSheet.SetValue(startRow3, 5, FormatString(clsVayNgoai.Sum(p => p.DU_NO)));
                excelSheet.SetValue(startRow3, 6, FormatString(clsVayNgoai.Where(p => p.KY_HAN <= 12).Sum(t => t.SO_TIEN_VAY)));
                excelSheet.SetValue(startRow3, 7, FormatString(clsVayNgoai.Where(p => p.KY_HAN <= 12).Sum(t => t.DU_NO)));
                excelSheet.SetValue(startRow3, 12, FormatString(clsVayNgoai.Where(p => p.KY_HAN > 12).Sum(t => t.SO_TIEN_VAY)));
                excelSheet.SetValue(startRow3, 13, FormatString(clsVayNgoai.Where(p => p.KY_HAN > 12).Sum(t => t.DU_NO)));
                //Write it back to the client
                var fileOnServer = Server.MapPath($"~/Temp/{folderName}/{fileName}.xlsx");
                exPackage.SaveAs(new FileInfo(fileOnServer));
                reportCount++;
            }
            if (reportCount == _phamViBaoCao.Count())
            {
                status = true;
            }
            return Json(new
            {
                data = reportCount,
                status = status
            }, JsonRequestBehavior.AllowGet);
        }

        private string FormatString(decimal? input)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", input / dviTinh);
        }
    }
}