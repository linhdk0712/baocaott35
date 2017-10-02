using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class ClsBaoCaoDoanhSoCapThuNoTinDung : IDisposable
    {
        [Display(Name = "Kỳ hạn")]
        public int TGIAN_VAY { get; set; }

        [Display(Name = "Doanh số cấp tín dụng")]
        public decimal SO_TIEN_GIAI_NGAN { get; set; }

        [Display(Name = "Doanh số thu nợ tín dụng")]
        public decimal TT_TRA_GOC { get; set; }

        private ReportApplicationEntities _reportApplicationEntities;

        public void Dispose()
        {
            if (_reportApplicationEntities != null)
            {
                _reportApplicationEntities.Dispose();
                _reportApplicationEntities = null;
            }
        }

        public IEnumerable<ClsBaoCaoDoanhSoCapThuNoTinDung> GetAllData(string ngayDuLieu)
        {
            const string storeProc = "Proc_BaoCaoDoanhSoCapThuNoTinDung @NgayDl";
            var param = new SqlParameter("@NgayDl", ngayDuLieu);
            using (_reportApplicationEntities = new ReportApplicationEntities())
            {
                _reportApplicationEntities.Database.CommandTimeout = 1800;
                return _reportApplicationEntities.Database.SqlQuery<ClsBaoCaoDoanhSoCapThuNoTinDung>(storeProc, param).ToList();
            }
        }
    }
}