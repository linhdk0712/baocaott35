using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsBaoCaoDuNoPhanTheoNganhKinhTe : IDisposable
    {
        public string MUC_DICH_VAY { get; set; }
        public decimal NHOM1 { get; set; }
        public decimal NHOM2 { get; set; }
        public decimal NHOM3 { get; set; }
        public decimal NHOM4 { get; set; }
        public decimal NHOM5 { get; set; }
        private readonly ReportApplicationEntities _reportApplicationEntities;

        public void Dispose()
        {
            _reportApplicationEntities?.Dispose();
        }

        public clsBaoCaoDuNoPhanTheoNganhKinhTe()
        {
            _reportApplicationEntities = new ReportApplicationEntities();
        }

        public IEnumerable<clsBaoCaoDuNoPhanTheoNganhKinhTe> GetAllData(string maChiNhanh, string denNgay)
        {
            object[] param =
            {
                new SqlParameter("@MaChiNhanh",maChiNhanh),
                new SqlParameter("@DenNgay",denNgay)
            };
            _reportApplicationEntities.Database.CommandTimeout = 1800;
            var result = _reportApplicationEntities.Database.SqlQuery<clsBaoCaoDuNoPhanTheoNganhKinhTe>("Proc_BaoCaoDuNoTinDungPhanTheoNganhKinhTe @MaChiNhanh,@DenNgay", param).ToList();
            return result;
        }
    }
}