using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebApplication.Commons
{
    public class clsDinhKyBaoCao
    {
        private readonly ReportApplicationEntities _reportApplicationEntities;

        public clsDinhKyBaoCao()
        {
            _reportApplicationEntities = new ReportApplicationEntities();
        }

        public int DinhKyBaoCao(string reportName)
        {
            return Convert.ToInt32(_reportApplicationEntities.BAO_CAO_TT35.Where(x => x.FRM_NAME == reportName).Select(x => x.MENU_CHA).FirstOrDefault());
        }
    }
}