using ReportApplication.DAL;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ReportApplication.BAL
{
    public class clsFormInfo : IDisposable
    {
        private readonly ReportApplicationEntities _reportApplicationEntities;
        private readonly Form _form;

        public void Dispose()
        {
        }

        public clsFormInfo(Form form)
        {
            _reportApplicationEntities = new ReportApplicationEntities();
            _form = form;
        }

        public string GetReportName()
        {
            var frmName = _form.Name;
            var reportName = (from a in _reportApplicationEntities.BAO_CAO_TT35 where a.FRM_NAME == frmName select a.FRM_NAME).FirstOrDefault();
            return reportName;
        }

        public string GetFormDes()
        {
            var frmName = _form.Name;
            var reportName = (from a in _reportApplicationEntities.BAO_CAO_TT35 where a.FRM_NAME == frmName select a.FRM_DES).FirstOrDefault();
            return reportName;
        }
    }
}