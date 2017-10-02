using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsG01254LaiSuat : IDisposable
    {
        public int ID { get; set; }

        [Display(Name = "Ngày nhập")]
        public string NGAY_DL { get; set; }

        [Display(Name = "Mã lãi suất")]
        public string MA_LAI_SUAT { get; set; }

        [Display(Name = "Kỳ hạn")]
        public int? KY_HAN { get; set; }

        [Display(Name = "Đơn vị tính")]
        public string DV_TINH { get; set; }

        [Display(Name = "Giá trị")]
        public decimal LAI_SUAT { get; set; }

        private readonly ReportApplicationEntities _reportApplicationEntities;

        public void Dispose()
        {
            if (_reportApplicationEntities != null)
                _reportApplicationEntities.Dispose();
        }

        public clsG01254LaiSuat()
        {
            _reportApplicationEntities = new ReportApplicationEntities();
        }

        public void AddNew(clsG01254LaiSuat laiSuat)
        {
            var _laiSuat = new TT35_LAI_SUAT_G01254()
            {
                NGAY_DL = laiSuat.NGAY_DL,
                MA_LAI_SUAT = laiSuat.MA_LAI_SUAT,
                LAI_SUAT = laiSuat.LAI_SUAT
            };
            _reportApplicationEntities.TT35_LAI_SUAT_G01254.Add(_laiSuat);
            _reportApplicationEntities.SaveChanges();
        }

        public void Edit(clsG01254LaiSuat laiSuat)
        {
            var _laiSuat = _reportApplicationEntities.TT35_LAI_SUAT_G01254.Find(laiSuat.MA_LAI_SUAT);
            _laiSuat.LAI_SUAT = laiSuat.LAI_SUAT;
            _laiSuat.NGAY_DL = laiSuat.NGAY_DL;
            _reportApplicationEntities.SaveChanges();
        }

        public IEnumerable<clsG01254LaiSuat> GetAll(string ngayDuLieu)
        {
            var para = new SqlParameter("@NgayDl", ngayDuLieu);
            _reportApplicationEntities.Database.CommandTimeout = 1800;
            return _reportApplicationEntities.Database.SqlQuery<clsG01254LaiSuat>("Proc_GetAllLaiSuatG01254 @NgayDl", para).ToList();
        }

        public clsG01254LaiSuat GetData(string laiSuat)
        {
            var result = _reportApplicationEntities.TT35_LAI_SUAT_G01254.FirstOrDefault(p => p.MA_LAI_SUAT == laiSuat);
            var _laiSuat = new clsG01254LaiSuat
            {
                ID = result.ID,
                NGAY_DL = result.NGAY_DL,
                MA_LAI_SUAT = result.MA_LAI_SUAT,
                LAI_SUAT = result.LAI_SUAT
            };
            return _laiSuat;
        }
    }
}