using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan : IDisposable
    {
        public string MA_KHANG { get; set; }
        public string MA_KHANG_LOAI { get; set; }
        public string MA_NHOM_SP { get; set; }
        public decimal SO_TIEN { get; set; }
        public int KY_HAN { get; set; }
        private readonly ReportApplicationEntities _reportApplicationEntities;

        public void Dispose()
        {
            if (_reportApplicationEntities != null)
                _reportApplicationEntities.Dispose();
        }

        public clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan()
        {
            _reportApplicationEntities = new ReportApplicationEntities();
        }

        public IEnumerable<clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan> GetAllData(string maChiNhanh, string denNgay)
        {
            object[] param =
            {
                new SqlParameter("@MaChiNhanh",maChiNhanh),
                new SqlParameter("@DenNgay",denNgay)
            };
            _reportApplicationEntities.Database.CommandTimeout = 1800;
            var result = _reportApplicationEntities.Database.SqlQuery<clsBaoCaoTinhHinhHuyDongTienGuiToChucCaNhan>("Proc_BaoCaoTinhHinhHuyDongVonToChucCaNhan @MaChiNhanh,@DenNgay", param).ToList();
            return result;
        }
    }
}