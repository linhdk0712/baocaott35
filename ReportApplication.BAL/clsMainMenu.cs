using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsMainMenu : IDisposable
    {
        public int ID { get; set; }
        public string MENU_CODE { get; set; }
        public string MENU_NAME { get; set; }
        public bool STATUS { get; set; }
        private ReportApplicationEntities _reportApplicationEntities;

        public void Dispose()
        {
            if (_reportApplicationEntities != null)
            {
                _reportApplicationEntities.Dispose();
                _reportApplicationEntities = null;
            }
        }

        public clsMainMenu()
        {
            _reportApplicationEntities = new ReportApplicationEntities();
        }

        public IEnumerable<BAO_CAO_TT35_MENU> GetAllMenu() => _reportApplicationEntities.BAO_CAO_TT35_MENU.Where(p => p.STATUS);
    }
}