using ReportApplication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebApplication.Commons
{
    public class clsGetFileNameReportsWeb
    {
        public int iDinhKyBaoCao { get; set; }

        public clsGetFileNameReportsWeb(int _iDinhKyBaoCao)
        {
            iDinhKyBaoCao = _iDinhKyBaoCao;
        }

        public string NgayBaoCao(string denNgay)
        {
            var ngayPsinhDLieu = DateTime.Parse(denNgay);
            string _ngayPsinhDLieu = null;
            var quater = (ngayPsinhDLieu.Month + 2) / 3;
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

        public void GetFileNameReportMonthWpf(out string ngayBaoCao, out string maDviPsinhDlieu, out string fileName, string cbxDviPsinhDlieu, string maDonViGui, string reportName, string denNgay)
        {
            var dateTimeNow = DateTime.Now;
            ngayBaoCao = dateTimeNow.ToString("yyyyMMdd");
            var ngayPsinhDLieu = DateTime.Parse(denNgay);
            var _ngayPsinhDLieu = NgayBaoCao(denNgay);
            var compare1 = dateTimeNow - ngayPsinhDLieu;
            var thoiGianGui = compare1.TotalDays > 12 ? "B" : "S";
            var loaiBaoCao = "M";
            maDviPsinhDlieu = cbxDviPsinhDlieu;
            var loaiDuLieu = maDviPsinhDlieu == "00" ? "T" : "I";
            fileName =
                $"{reportName}-{clsMaDonViPhatSinh.GetMaNganHang(maDviPsinhDlieu)}-{maDonViGui}-{_ngayPsinhDLieu}-{thoiGianGui}{loaiDuLieu}-{loaiBaoCao}-01";
        }
    }
}