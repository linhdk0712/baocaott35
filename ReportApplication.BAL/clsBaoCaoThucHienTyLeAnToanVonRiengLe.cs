using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsBaoCaoThucHienTyLeAnToanVonRiengLe : IDisposable
    {
        [Display(Name = "Tên loại tài khoản")]
        public string F10 { get; set; }

        [Display(Name = "Mã loại tài khoản")]
        public string F03 { get; set; }

        [Display(Name = "Dư nợ đầu kỳ")]
        public decimal F04 { get; set; }

        [Display(Name = "Dư có đầu kỳ")]
        public decimal F05 { get; set; }

        [Display(Name = "Phát sinh nợ trong kỳ")]
        public decimal F06 { get; set; }

        [Display(Name = "Phát sinh có trong kỳ")]
        public decimal F07 { get; set; }

        [Display(Name = "Dư nợ cuối kỳ")]
        public decimal F08 { get; set; }

        [Display(Name = "Dư có cuối kỳ")]
        public decimal F09 { get; set; }

        private readonly ReportApplicationEntities _reportApplicationEntities;

        public void Dispose()
        {
            if (_reportApplicationEntities != null)
                _reportApplicationEntities.Dispose();
        }

        public clsBaoCaoThucHienTyLeAnToanVonRiengLe()
        {
            _reportApplicationEntities = new ReportApplicationEntities();
        }

        public IEnumerable<clsBaoCaoThucHienTyLeAnToanVonRiengLe> GetAll(string tuNgay, string denNgay)
        {
            object[] param =
             {
                new SqlParameter("@TuNgay",tuNgay),
                new SqlParameter("@DenNgay",denNgay)
            };
            _reportApplicationEntities.Database.CommandTimeout = 1800;
            var result = _reportApplicationEntities.Database.SqlQuery<clsBaoCaoThucHienTyLeAnToanVonRiengLe>("Proc_BaoCaoTinhHinhThucHienTyLeAnToanRiengLe @TuNgay,@DenNgay", param).ToList();
            return result;
        }

        public decimal GetDuNoKhachHangTKQD(string denNgay)
        {
            var param = new SqlParameter("@NgayDL", denNgay);
            using (var dbContext = new ReportApplicationEntities())
            {
                dbContext.Database.CommandTimeout = 1800;
                var result = dbContext.Database.SqlQuery<decimal>("Proc_DuNoCuaKhachHangTKQD @NgayDL", param).SingleOrDefault();
                return result;
            }
        }

        public decimal GetDuNoKhachHangTKTN(string denNgay)
        {
            decimal duTKTN = 0;
            var param = new SqlParameter("@NgayDL", denNgay);
            using (var dbContext = new ReportApplicationEntities())
            {
                dbContext.Database.CommandTimeout = 1800;
                var result = dbContext.Database.SqlQuery<DuTKTN>("Proc_DuNoCuaKhachHangTKTN @NgayDL", param).ToList();
                foreach (var item in result)
                {
                    duTKTN += Math.Min(item.SO_DU, item.SO_TIEN);
                }
            }
            return duTKTN;
        }

        private class DuTKTN
        {
            public string MA_KHANG { get; set; }
            public decimal SO_DU { get; set; }
            public decimal SO_TIEN { get; set; }
        }
    }
}