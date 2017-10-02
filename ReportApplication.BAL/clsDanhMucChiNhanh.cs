using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsDanhMucChiNhanh : IDisposable
    {
        private ReportApplicationEntities _reportDbContext;
        public string TEN_DVI { get; set; }
        public string MA_DVI { get; set; }

        public void Dispose()
        {
            if (_reportDbContext == null) return;
            _reportDbContext.Dispose();
            _reportDbContext = null;
        }

        public clsDanhMucChiNhanh()
        {
            _reportDbContext = new ReportApplicationEntities();
        }

        public IEnumerable<clsDanhMucChiNhanh> GetDanhMucChiNhanh()
        {
            var loaiDonVi = new[] { "DVI", "HSO", "CNH" };
            var result = (from a in _reportDbContext.DM_DON_VI
                          where loaiDonVi.Contains(a.LOAI_DVI)
                          select new clsDanhMucChiNhanh
                          {
                              MA_DVI = a.MA_DVI,
                              TEN_DVI = a.TEN_GDICH
                          });
            return result.ToList();
        }
    }
}