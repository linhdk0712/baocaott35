using DevExpress.XtraEditors;
using System;

namespace ReportApplication.Common
{
    /// <summary>
    /// Tạo tên file báo cáo
    /// </summary>
    public class clsCreateFileNameReport
    {
        /// <summary>
        /// Tạo tên file báo cáo theo ngày
        /// </summary>
        /// <param name="ngayBaoCao"></param>
        /// <param name="maDviPsinhDlieu"></param>
        /// <param name="fileName"></param>
        /// <param name="radioGroup1"></param>
        /// <param name="maDonViGui"></param>
        /// <param name="reportName"></param>
        public void GetFileNameReportDay(out string ngayBaoCao, out string maDviPsinhDlieu, out string fileName, RadioGroup radioGroup1, string maDonViGui, string reportName, DateEdit denNgay)
        {
            var ngayPsinhDLieu = ((DateTime)(denNgay.EditValue)).ToString("yyyyMMdd");
            var dateTime = DateTime.Now;
            ngayBaoCao = dateTime.ToString("yyyyMMdd");
            var firstDayOfMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var day10OfMonth = firstDayOfMonth.AddDays(9);
            var day20OfMonth = firstDayOfMonth.AddDays(19);
            var compare1 = dateTime - day10OfMonth;
            var compare2 = dateTime - day20OfMonth;
            var compare3 = dateTime - lastDayOfMonth;
            string thoiGianGui;
            const string loaiDuLieu = "T";
            if (compare1.TotalDays > 2)
                thoiGianGui = "B";
            else if (compare2.TotalDays > 2)
                thoiGianGui = "B";
            else if (compare3.TotalDays > 2)
                thoiGianGui = "B";
            else
                thoiGianGui = "S";
            var loaiBaoCao = radioGroup1.EditValue.ToString();
            maDviPsinhDlieu = "00";
            fileName =
                $"{reportName}-{clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu)}-{maDonViGui}-{ngayPsinhDLieu}-{thoiGianGui}{loaiDuLieu}-{loaiBaoCao}-01";
        }

        /// <summary>
        /// Tạo tên file báo cáo theo tháng
        /// </summary>
        /// <param name="ngayBaoCao"></param>
        /// <param name="maDviPsinhDlieu"></param>
        /// <param name="fileName"></param>
        /// <param name="cbxDviPsinhDlieu"></param>
        /// <param name="radioGroup1"></param>
        /// <param name="maDonViGui"></param>
        /// <param name="reportName"></param>
        public void GetFileNameReportMonth(out string ngayBaoCao, out string maDviPsinhDlieu, out string fileName, LookUpEdit cbxDviPsinhDlieu, RadioGroup radioGroup1, string maDonViGui, string reportName, DateEdit denNgay)
        {
            var dateTime = DateTime.Now;
            ngayBaoCao = dateTime.ToString("yyyyMMdd");
            var ngayPsinhDLieu = (DateTime)(denNgay.EditValue);
            var compare1 = dateTime - ngayPsinhDLieu;
            var thoiGianGui = compare1.TotalDays > 12 ? "B" : "S";
            var loaiBaoCao = radioGroup1.EditValue.ToString();
            maDviPsinhDlieu = cbxDviPsinhDlieu.EditValue.ToString();
            var loaiDuLieu = maDviPsinhDlieu == "00" ? "T" : "I";
            fileName =
                $"{reportName}-{clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu)}-{maDonViGui}-{ngayPsinhDLieu.ToString("yyyyMMdd")}-{thoiGianGui}{loaiDuLieu}-{loaiBaoCao}-01";
        }
    }
}