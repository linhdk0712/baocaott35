using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan : IDisposable
    {
        public string NGAY_DL { get; set; }
        public string LOAI_NGUON { get; set; }
        public string MA_NGUON { get; set; }
        public string MA_NGUON_VON { get; set; }
        public string TEN_NGUON_VON { get; set; }
        public string NGAY_VAY { get; set; }
        public string NGAY_DAO_HAN { get; set; }
        public string NGAY_CO_CAU { get; set; }
        public decimal SO_TIEN_VAY { get; set; }
        public decimal DU_NO { get; set; }
        public int? KY_HAN { get; set; }
        public decimal? LAI_SUAT { get; set; }
        public string MA_DVI_QLY { get; set; }
        private readonly ReportApplicationEntities _reportApplicationEntities;

        public void Dispose()
        {
            if (_reportApplicationEntities != null)
                _reportApplicationEntities.Dispose();
        }

        public clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan()
        {
            _reportApplicationEntities = new ReportApplicationEntities();
        }

        public IEnumerable<clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan> GetAllData(string maChiNhanh, string denNgay)
        {
            object[] param =
            {
                new SqlParameter("@MaChiNhanh",maChiNhanh),
                new SqlParameter("@NgayDL",denNgay)
            };
            _reportApplicationEntities.Database.CommandTimeout = 1800;
            var result = _reportApplicationEntities.Database.SqlQuery<clsBaoCaoTinhHinhVayVonTuCacToChucCaNhan>("Proc_BaoCaoTinhHinhVayVonTuCacToChucCaNhan @MaChiNhanh,@NgayDL", param).ToList();
            return result;
        }
    }
}