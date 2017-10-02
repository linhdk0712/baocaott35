using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class ClsBaoCaoPhanLoaiNoVaTrichLap : IDisposable
    {
        public string MA_KHACH_HANG { get; set; }
        public string TEN_KHACH_HANG { get; set; }
        public string SO_KHE_UOC { get; set; }
        public int TGIAN_VAY { get; set; }
        public string TGIAN_VAY_DVI_TINH { get; set; }
        public decimal SO_DU { get; set; }
        public decimal DU_NO_NHOM_1 { get; set; }
        public decimal DU_NO_NHOM_2 { get; set; }
        public decimal DU_NO_NHOM_3 { get; set; }
        public decimal DU_NO_NHOM_4 { get; set; }
        public decimal DU_NO_NHOM_5 { get; set; }
        private readonly ReportApplicationEntities _reportApplicationEntities;

        public void Dispose()
        {
            _reportApplicationEntities?.Dispose();
        }

        public ClsBaoCaoPhanLoaiNoVaTrichLap()
        {
            _reportApplicationEntities = new ReportApplicationEntities();
        }

        public IEnumerable<ClsBaoCaoPhanLoaiNoVaTrichLap> GetAllData(string maChiNhanh, string denNgay)
        {
            object[] param =
          {
                new SqlParameter("@MaChiNhanh",maChiNhanh),
                new SqlParameter("@DenNgay",denNgay)
            };
            _reportApplicationEntities.Database.CommandTimeout = 1800;
            var result = _reportApplicationEntities.Database.SqlQuery<ClsBaoCaoPhanLoaiNoVaTrichLap>("Proc_BaoCaoPLoaiNoTrichLapDPhong @MaChiNhanh,@DenNgay", param).ToList();
            return result;
        }
    }
}