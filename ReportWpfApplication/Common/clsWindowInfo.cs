using ReportApplication.DAL;
using System.Linq;

namespace ReportWpfApplication.Common
{
    public class clsWindowInfo
    {
        private readonly ReportApplicationEntities _reportApplicationEntities;
        public string name { get; set; }

        public clsWindowInfo(string wb)
        {
            name = wb;
        }

        public string GetReportName()
        {
            var frmName = name;
            var reportName = (from a in _reportApplicationEntities.BAO_CAO_TT35 where a.FRM_NAME == frmName select a.FRM_NAME).FirstOrDefault();
            return reportName;
        }

        public string GetFormDes()
        {
            var frmName = name;
            var reportName = (from a in _reportApplicationEntities.BAO_CAO_TT35 where a.FRM_NAME == frmName select a.FRM_DES).FirstOrDefault();
            return reportName;
        }
    }
}