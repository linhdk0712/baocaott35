using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data.SqlClient;

namespace ReportWebApplication.Commons
{
    public class clsCicReportDapper
    {
       // const string CONNECTION_STRING = "Data Source=118.70.185.190,1433;Network Library=DBMSSOCN;Initial Catalog = NG-mFINA; User ID = linhdk0712; Password=213456789;";
        public static IEnumerable<clsCicReport> ExecuteReturnList<clsCicReport>(DynamicParameters param, string query)
        {
            using (var conn = new SqlConnection(clsConnectionString.ConnectionString()))
            {
                conn.Open();
                return conn.Query<clsCicReport>(query, param, commandType: System.Data.CommandType.StoredProcedure,commandTimeout:1800);
            }
        }
    }
}