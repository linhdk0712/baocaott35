using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsChildMenu : IDisposable
    {
        [Display(Name = "STT")]
        public int ID { get; set; }

        [Display(Name = "Mã định kỳ báo cáo")]
        public int? ID_MENU_CHA { get; set; }

        [Display(Name = "Loại định kỳ báo cáo")]
        public string MENU_CHA { get; set; }

        [Display(Name = "Mã báo cáo")]
        public string FRM_CODE { get; set; }

        [Display(Name = "Mã định danh báo cáo")]
        public string FRM_NAME { get; set; }

        [Display(Name = "Tên báo cáo")]
        public string FRM_DES { get; set; }

        [Display(Name = "Trạng thái")]
        public bool STATUS { get; set; }

        public DateTime DATECREATE { get; set; }

        private ReportApplicationEntities _reportApplicationEntities;

        public void Dispose()
        {
            if (_reportApplicationEntities == null) return;
            _reportApplicationEntities.Dispose();
            _reportApplicationEntities = null;
        }

        public clsChildMenu()
        {
            _reportApplicationEntities = new ReportApplicationEntities();
        }

        public IEnumerable<clsChildMenu> GetAllChildMenu()
        {
            var result = (from a in _reportApplicationEntities.BAO_CAO_TT35
                          join b in _reportApplicationEntities.BAO_CAO_TT35_MENU
                          on a.MENU_CHA equals b.ID
                          select new clsChildMenu
                          {
                              ID = a.ID,
                              ID_MENU_CHA = a.MENU_CHA,
                              MENU_CHA = b.MENU_NAME,
                              FRM_CODE = a.FRM_CODE,
                              FRM_NAME = a.FRM_NAME,
                              FRM_DES = a.FRM_DES,
                              STATUS = a.STATUS,
                              DATECREATE = DateTime.Now
                          });
            return result.ToList();
        }

        public IEnumerable<clsChildMenu> GetAllChildMenuWithCondition(string condition)
        {
            var result = (from a in _reportApplicationEntities.BAO_CAO_TT35
                          join b in _reportApplicationEntities.BAO_CAO_TT35_MENU
                          on a.MENU_CHA equals b.ID
                          where a.FRM_DES.Contains(condition)
                          select new clsChildMenu
                          {
                              ID = a.ID,
                              ID_MENU_CHA = a.MENU_CHA,
                              MENU_CHA = b.MENU_NAME,
                              FRM_CODE = a.FRM_CODE,
                              FRM_NAME = a.FRM_NAME,
                              FRM_DES = a.FRM_DES,
                              STATUS = a.STATUS,
                              DATECREATE = DateTime.Now
                          });
            return result.ToList();
        }
    }
}