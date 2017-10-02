using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportWebApplication.Commons
{
    public class ExcelContentType
    {
        public static string ContentType()
        {
            return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }
    }
}