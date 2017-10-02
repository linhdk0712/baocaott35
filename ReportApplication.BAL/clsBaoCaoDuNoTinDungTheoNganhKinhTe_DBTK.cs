using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK : IDisposable
    {
        [Display(Name = "Mục đích vay vốn")]
        public string MUC_DICH_VAY { get; set; }

        [Display(Name = "Thời gian vay vốn")]
        public int TGIAN_VAY { get; set; }

        [Display(Name = "Dư nợ")]
        public decimal SO_DU { get; set; }

        [Display(Name = "Lãi dự thu")]
        public decimal LAI_DU_THU { get; set; }

        private ReportApplicationEntities _reportApplicationEntities;

        public void Dispose()
        {
            if (_reportApplicationEntities != null)
            {
                _reportApplicationEntities.Dispose();
                _reportApplicationEntities = null;
            }
        }

        public IEnumerable<clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK> GetAllData(string maChiNhanh, string denNgay)
        {
            using (_reportApplicationEntities = new ReportApplicationEntities())
            {
                object[] param =
                {
                new SqlParameter("@MaChiNhanh",maChiNhanh),
                new SqlParameter("@DenNgay",denNgay)
                };
                _reportApplicationEntities.Database.CommandTimeout = 1800;
                var result = _reportApplicationEntities.Database.SqlQuery<clsBaoCaoDuNoTinDungTheoNganhKinhTe_DBTK>("Proc_BaoCaoDuNoTinDungTheoNganhKinhTe_DBTK @MaChiNhanh,@DenNgay", param).ToList();
                return result;
            }
        }
    }
}