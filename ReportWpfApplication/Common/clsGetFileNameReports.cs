using ReportApplication.Common;
using System;
using System.Windows.Controls;

namespace ReportWpfApplication.Common
{
    public class clsGetFileNameReports
    {
        public int iDinhKyBaoCao { get; set; }

        public clsGetFileNameReports(int _iDinhKyBaoCao)
        {
            iDinhKyBaoCao = _iDinhKyBaoCao;
        }

        public void GetFileNameReportMonthWpf(out string ngayBaoCao, out string maDviPsinhDlieu, out string fileName, ComboBox cbxDviPsinhDlieu, string maDonViGui, string reportName, DatePicker denNgay)
        {
            var dateTimeNow = DateTime.Now;
            ngayBaoCao = dateTimeNow.ToString("yyyyMMdd");
            var ngayPsinhDLieu = (DateTime)(denNgay.SelectedDate);
            var _ngayPsinhDLieu = NgayBaoCao(denNgay);
            var compare1 = dateTimeNow - ngayPsinhDLieu;
            var thoiGianGui = compare1.TotalDays > 12 ? "B" : "S";
            var loaiBaoCao = "M";
            maDviPsinhDlieu = cbxDviPsinhDlieu.SelectedValue.ToString();
            var loaiDuLieu = maDviPsinhDlieu == "00" ? "T" : "I";
            fileName =
                $"{reportName}-{clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu)}-{maDonViGui}-{_ngayPsinhDLieu}-{thoiGianGui}{loaiDuLieu}-{loaiBaoCao}-01";
        }

        /// <summary>
        /// Tạo tên file báo cáo theo ngày
        /// </summary>
        /// <param name="ngayBaoCao"></param>
        /// <param name="maDviPsinhDlieu"></param>
        /// <param name="fileName"></param>
        /// <param name="radioGroup1"></param>
        /// <param name="maDonViGui"></param>
        /// <param name="reportName"></param>
        public void GetFileNameReportDayWpf(out string ngayBaoCao, out string maDviPsinhDlieu, out string fileName, ComboBox cbxDviPsinhDlieu, string maDonViGui, string reportName, DatePicker denNgay)
        {
            var ngayPsinhDLieu = ((DateTime)(denNgay.SelectedDate)).ToString("yyyyMMdd");
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
            maDviPsinhDlieu = "00";
            var loaiBaoCao = "M";
            fileName =
                $"{reportName}-{clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu)}-{maDonViGui}-{ngayPsinhDLieu}-{thoiGianGui}{loaiDuLieu}-{loaiBaoCao}-01";
        }

        public string NgayBaoCao(DatePicker denNgay)
        {
            var ngayPsinhDLieu = (DateTime)(denNgay.SelectedDate);
            string _ngayPsinhDLieu = null;
            int quater = (ngayPsinhDLieu.Month + 2) / 3;
            if (iDinhKyBaoCao == 1)
            {
                _ngayPsinhDLieu = ngayPsinhDLieu.ToString("yyyyMMdd");
            }
            else if (iDinhKyBaoCao == 2)
            {
                _ngayPsinhDLieu = ngayPsinhDLieu.ToString("yyyyMM");
            }
            else if (iDinhKyBaoCao == 3)
            {
                if (quater == 1)
                {
                    _ngayPsinhDLieu = ngayPsinhDLieu.Year + "03";
                }
                else if (quater == 2)
                {
                    _ngayPsinhDLieu = ngayPsinhDLieu.Year + "06";
                }
                else if (quater == 3)
                {
                    _ngayPsinhDLieu = ngayPsinhDLieu.Year + "09";
                }
                else if (quater == 4)
                {
                    _ngayPsinhDLieu = ngayPsinhDLieu.Year + "12";
                }
            }
            else if (iDinhKyBaoCao == 4 || iDinhKyBaoCao == 10)
            {
                _ngayPsinhDLieu = ngayPsinhDLieu.Year.ToString();
            }
            return _ngayPsinhDLieu;
        }
    }
}