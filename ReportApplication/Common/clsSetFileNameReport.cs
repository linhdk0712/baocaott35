using OfficeOpenXml;

namespace ReportApplication.Common
{
    /// <summary>
    /// Class tạo tên file báo cáo
    /// </summary>
    public class clsSetFileNameReport
    {
        /// <summary>
        /// Tạo tên file báo cáo ở template
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="code"></param>
        /// <param name="codeData"></param>
        /// <param name="date"></param>
        /// <param name="user"></param>
        /// <param name="workSheet"></param>
        public void SetFileNameReport(string fileName, string code, string codeData, string date, string user, ExcelWorksheet workSheet)
        {
            workSheet.Cells["C2"].Value = fileName;
            workSheet.Cells["C3"].Value = code;
            workSheet.Cells["C4"].Value = codeData;
            workSheet.Cells["C5"].Value = date;
            workSheet.Cells["C6"].Value = "Do Khanh Linh";
            workSheet.Cells["C7"].Value = user;
        }
    }
}