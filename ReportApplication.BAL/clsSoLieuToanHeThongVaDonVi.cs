using ReportApplication.DAL;
using System.Collections.Generic;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsSoLieuToanHeThongVaDonVi
    {
        public string MA_DVI { get; set; }

        public IEnumerable<clsSoLieuToanHeThongVaDonVi> BaoCaoToanHeThongOrTruSoChinh(string mabaocao)
        {
            var donvi = new[] { "DVI", "HSO", "CNH" };
            List<clsSoLieuToanHeThongVaDonVi> cls;
            using (var reportApplicationEntities = new ReportApplicationEntities())
            {
                if ((bool)reportApplicationEntities.BAO_CAO_TT35.Where(x => x.FRM_NAME == mabaocao).Select(x => x.BC_ALL).FirstOrDefault())
                {
                    cls = (from a in reportApplicationEntities.DM_DON_VI
                           where donvi.Contains(a.LOAI_DVI)
                           select new clsSoLieuToanHeThongVaDonVi()
                           {
                               MA_DVI = a.MA_DVI
                           }).ToList();
                }
                else
                {
                    cls = (from a in reportApplicationEntities.DM_DON_VI
                           where a.LOAI_DVI == "DVI"
                           select new clsSoLieuToanHeThongVaDonVi()
                           {
                               MA_DVI = a.MA_DVI
                           }).ToList();
                }
            }
            return cls;
        }
    }
}