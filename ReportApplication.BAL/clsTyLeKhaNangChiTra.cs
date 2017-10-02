using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsTyLeKhaNangChiTra : IDisposable
    {
        public int Id { get; set; }
        public string F02 { get; set; }
        public string F10 { get; set; }
        public string F03 { get; set; }
        public decimal F04 { get; set; }
        public decimal F05 { get; set; }
        public decimal F06 { get; set; }
        public decimal F07 { get; set; }
        public decimal F08 { get; set; }
        public decimal F09 { get; set; }
        private ReportApplicationEntities _reportDbContext;

        public void Dispose()
        {
            if (_reportDbContext == null) return;
            _reportDbContext.Dispose();
            _reportDbContext = null;
        }

        public clsTyLeKhaNangChiTra()
        {
            _reportDbContext = new ReportApplicationEntities();
        }

        public decimal GetTyLeKhaNangChiTra_TaiKhoanNo(string maDonVi, string tuNgay, string denNgay, string chiTieu)
        {
            object[] param =
           {
                new SqlParameter("@MaChiNhanh",maDonVi),
                new SqlParameter("@TuNgay",tuNgay),
                new SqlParameter("@DenNgay",denNgay)
            };
            _reportDbContext.Database.CommandTimeout = 1800;
            var result = _reportDbContext.Database.SqlQuery<clsTyLeKhaNangChiTra>("Proc_BaoCaoTyLeKhaNangChiTra @MaChiNhanh,@TuNgay,@DenNgay", param).ToList();
            var data = (from a in result where a.F03 == chiTieu select a.F08).FirstOrDefault();
            return data;
        }

        public decimal GetTyLeKhaNangChiTra_TaiKhoanCo(string maDonVi, string tuNgay, string denNgay, string chiTieu)
        {
            object[] param =
           {
                new SqlParameter("@MaChiNhanh",maDonVi),
                new SqlParameter("@TuNgay",tuNgay),
                new SqlParameter("@DenNgay",denNgay)
            };
            _reportDbContext.Database.CommandTimeout = 1800;
            var result = _reportDbContext.Database.SqlQuery<clsTyLeKhaNangChiTra>("Proc_BaoCaoTyLeKhaNangChiTra @MaChiNhanh,@TuNgay,@DenNgay", param).ToList();
            var data = (from a in result where a.F03 == chiTieu select a.F09).FirstOrDefault();
            return data;
        }

        public IEnumerable<clsTyLeKhaNangChiTra> GetTyLeKhaNangChiTra(string maDonVi, string tuNgay, string denNgay)
        {
            object[] param =
           {
                new SqlParameter("@MaChiNhanh",maDonVi),
                new SqlParameter("@TuNgay",tuNgay),
                new SqlParameter("@DenNgay",denNgay)
            };
            _reportDbContext.Database.CommandTimeout = 1800;
            var result = _reportDbContext.Database.SqlQuery<clsTyLeKhaNangChiTra>("Proc_BaoCaoTyLeKhaNangChiTra @MaChiNhanh,@TuNgay,@DenNgay", param).ToList();
            return result;
        }
    }
}