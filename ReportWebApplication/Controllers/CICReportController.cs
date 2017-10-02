using Dapper;
using OfficeOpenXml;
using ReportWebApplication.Commons;
using System;
using System.IO;
using System.Web.Mvc;

namespace ReportWebApplication.Controllers
{
    public class CICReportController : Controller
    {
        // GET: CICReport
        public ActionResult Index()
        {
            return View();
        }

        public void CreatReport(string date)
        {
            var ngayDuLieu = (DateTime.Parse(date)).ToString("yyyyMMdd");

            var param = new DynamicParameters();
            param.Add("@MaChiNhanh", "%");
            param.Add("@MaPhongGD", "%");
            param.Add("@DenNgay", ngayDuLieu);
            var cicList = clsCicReportDapper.ExecuteReturnList<clsCicReport>(param, "sp_BC_TDVM_BAO_CAO_CIC");
            var fileName = "PL0101912001" + ngayDuLieu;
            var newFile = new FileInfo(fileName);
            var fileTemplate = new FileInfo(HttpContext.Server.MapPath("~/Report/MAU01.xlsx"));
            var exPackage = new ExcelPackage(newFile, fileTemplate);
            var excelSheet = exPackage.Workbook.Worksheets[1];
            var iRowCount = 2;
            foreach (var row in cicList)
            {
                excelSheet.SetValue(iRowCount, 1, "01912001");
                excelSheet.SetValue(iRowCount, 2, row.MTV);
                excelSheet.SetValue(iRowCount, 3, row.HOTEN);
                excelSheet.SetValue(iRowCount, 4, row.LOAIKH);
                excelSheet.SetValue(iRowCount, 5, row.GT);
                excelSheet.SetValue(iRowCount, 6, row.NG_SINH);
                excelSheet.SetValue(iRowCount, 7, row.DIACHI);
                excelSheet.SetValue(iRowCount, 8, row.CMND);
                excelSheet.SetValue(iRowCount, 9, row.MST);
                excelSheet.SetValue(iRowCount, 10, row.NG_CAP);
                excelSheet.SetValue(iRowCount, 11, row.HD_SO);
                excelSheet.SetValue(iRowCount, 12, row.HD_NGKY);
                excelSheet.SetValue(iRowCount, 13, row.HD_NGDH);
                excelSheet.SetValue(iRowCount, 14, row.DUNO);
                excelSheet.SetValue(iRowCount, 15, row.KU_SO);
                excelSheet.SetValue(iRowCount, 16, row.KU_NGKY);
                excelSheet.SetValue(iRowCount, 17, row.KU_NGDH);
                excelSheet.SetValue(iRowCount, 18, row.KU_NGPS);
                excelSheet.SetValue(iRowCount, 19, row.LAISUAT);
                excelSheet.SetValue(iRowCount, 20, row.LOAIVAY);
                excelSheet.SetValue(iRowCount, 21, row.MUCDICH);
                excelSheet.SetValue(iRowCount, 22, row.DUNO);
                excelSheet.SetValue(iRowCount, 23, row.NHOMNO);
                iRowCount++;
            }
            var fileOnServer = Server.MapPath($"~/Temp/{fileName}.xlsx");
            exPackage.SaveAs(new FileInfo(fileOnServer));
            var stream = exPackage.Stream;
            // Tạo buffer memory strean để hứng file excel
            var buffer = stream as MemoryStream;
            // Đây là content Type dành cho file excel, còn rất nhiều content-type khác nhưng cái này mình thấy okay nhất
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // Dòng này rất quan trọng, vì chạy trên firefox hay IE thì dòng này sẽ hiện Save As dialog cho người dùng chọn thư mục để lưu
            // File name của Excel này là ExcelDemo
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xlsx");
            // Lưu file excel của chúng ta như 1 mảng byte để trả về response
            if (buffer != null) Response.BinaryWrite(buffer.ToArray());
            // Send tất cả ouput bytes về phía clients
            Response.Flush();
            Response.End();
        }
    }
}