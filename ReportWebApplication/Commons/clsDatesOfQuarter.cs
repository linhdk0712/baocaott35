using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebApplication.Commons
{
    public static class clsDatesOfQuarter
    {
        public static DateTime[] DatesOfQuarter(string date)
        {
            var dtReturn = new DateTime[3];
            var dtNow = DateTime.Parse(date);
            if (dtNow.Month >= 1 && dtNow.Month < 4)
            {
                dtReturn[0] = new DateTime(dtNow.Year, 1, 1);
                dtReturn[1] = new DateTime(dtNow.Year, 1, 15);
                dtReturn[2] = new DateTime(dtNow.Year, 3, 31);
            }
            else if (dtNow.Month >= 4 && dtNow.Month < 7)
            {
                dtReturn[0] = new DateTime(dtNow.Year, 4, 1);
                dtReturn[1] = new DateTime(dtNow.Year, 4, 15);
                dtReturn[2] = new DateTime(dtNow.Year, 6, 30);
            }
            else if (dtNow.Month >= 7 && dtNow.Month < 10)
            {
                dtReturn[0] = new DateTime(dtNow.Year, 7, 1);
                dtReturn[1] = new DateTime(dtNow.Year, 7, 15);
                dtReturn[2] = new DateTime(dtNow.Year, 9, 30);
            }
            else if (dtNow.Month >= 10 && dtNow.Month <= 12)
            {
                dtReturn[0] = new DateTime(dtNow.Year, 10, 1);
                dtReturn[1] = new DateTime(dtNow.Year, 10, 15);
                dtReturn[2] = new DateTime(dtNow.Year, 12, 31);
            }
            return dtReturn;
        }        
    }
}