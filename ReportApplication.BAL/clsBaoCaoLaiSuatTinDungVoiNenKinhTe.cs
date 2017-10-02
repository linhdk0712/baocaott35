using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsBaoCaoLaiSuatTinDungVoiNenKinhTe : IDisposable
    {
        public string MUC_DICH_VAY { get; set; }
        public int TGIAN_VAY { get; set; }
        public decimal LAI_SUAT { get; set; }
        public int SL_KUOCVM { get; set; }
        public decimal SO_TIEN_GIAI_NGAN { get; set; }
        public decimal TY_TRONG { get; set; }
        private ReportApplicationEntities _reportDbContext;

        public void Dispose()
        {
            if (_reportDbContext != null)
            {
                _reportDbContext.Dispose();
                _reportDbContext = null;
            }
        }

        public clsBaoCaoLaiSuatTinDungVoiNenKinhTe()
        {
            _reportDbContext = new ReportApplicationEntities();
        }

        public IEnumerable<clsBaoCaoLaiSuatTinDungVoiNenKinhTe> GetLaiSuatTinDung(string tuNgay, string denNgay)
        {
            object[] param =
            {
                new SqlParameter("@NgayDauThang",tuNgay),
                new SqlParameter("@NgayCuoiThang",denNgay)
            };
            _reportDbContext.Database.CommandTimeout = 1800;
            var result = _reportDbContext.Database.SqlQuery<clsBaoCaoLaiSuatTinDungVoiNenKinhTe>("Proc_BaoCaoLaiSuatTinDungVoiNenKinhTe @NgayDauThang,@NgayCuoiThang", param).ToList();
            return result;
        }
    }
}