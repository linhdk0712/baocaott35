using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsBaoCaoLaiSuatVoiNenKinhTe : IDisposable
    {
        private ReportApplicationEntities _reportDbContext;
        public int KY_HAN { get; set; }
        public string MA_NHOM_SP { get; set; }
        public decimal LAI_SUAT { get; set; }
        public decimal SO_TIEN_MO_SO { get; set; }
        public int SO_SO_TG { get; set; }
        public decimal TY_TRONG { get; set; }

        public void Dispose()
        {
            if (_reportDbContext != null)
            {
                _reportDbContext.Dispose();
                _reportDbContext = null;
            }
        }

        public clsBaoCaoLaiSuatVoiNenKinhTe()
        {
            _reportDbContext = new ReportApplicationEntities();
        }

        public IEnumerable<clsBaoCaoLaiSuatVoiNenKinhTe> GetLaiSuatTietKiem(string tuNgay, string denNgay)
        {
            object[] param =
            {
                new SqlParameter("@NgayDauThang",tuNgay),
                new SqlParameter("@NgayCuoiThang",denNgay)
            };
            _reportDbContext.Database.CommandTimeout = 1800;
            var result = _reportDbContext.Database.SqlQuery<clsBaoCaoLaiSuatVoiNenKinhTe>("Proc_BaoCaoLaiSuatTietKiemVoiNenKinhTe @NgayDauThang,@NgayCuoiThang", param).ToList();
            return result;
        }
    }
}