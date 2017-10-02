using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ReportApplication.BAL
{
    public class ClsBaoCaoMangLuoiHoatDong
    {
        public  int NumBerOfBranch()
        {
            using (var conn = new SqlConnection(clsConnectionStringBAL.ConnectionStringBAL()))
            {
                conn.Open();
                var query = @"SELECT COUNT(ID) FROM dbo.DM_DON_VI WHERE LOAI_DVI ='CNH' 
                                    AND TTHAI_BGHI='SDU' AND TTHAI_NVU='DDU'";
                return Convert.ToInt32(conn.ExecuteScalar(query, commandType: System.Data.CommandType.Text, commandTimeout: 1800));
            }
        }
        public  int NumBerOfTrans()
        {
            using (var conn = new SqlConnection(clsConnectionStringBAL.ConnectionStringBAL()))
            {
                conn.Open();
                var query = @"SELECT COUNT(ID) FROM dbo.DM_DON_VI WHERE LOAI_DVI ='PGD' 
                                AND TTHAI_BGHI='SDU' AND TTHAI_NVU='DDU'";
                return Convert.ToInt32(conn.ExecuteScalar(query, commandType: System.Data.CommandType.Text, commandTimeout: 1800));
            }
        }
        public  int NumBerOfCustomers(string ngayCuoiQuy)
        {
            using (var conn = new SqlConnection(clsConnectionStringBAL.ConnectionStringBAL()))
            {
                conn.Open();
                var query = @"SELECT COUNT(id) FROM dbo.KH_KHANG_HSO_LSU WHERE id 
                                IN (SELECT MAX(ID) FROM dbo.KH_KHANG_HSO_LSU WHERE NGAY_LSU <='" + ngayCuoiQuy+@"' 
                                GROUP BY MA_KHANG) AND MA_KHANG_LOAI ='TVIEN' AND NGAY_HET_HLUC IS NULL";
                return Convert.ToInt32(conn.ExecuteScalar(query, commandType: System.Data.CommandType.Text, commandTimeout: 1800));
            }
        }
        public  int NumBerOfLoans(string ngayCuoiQuy)
        {
            using (var conn = new SqlConnection(clsConnectionStringBAL.ConnectionStringBAL()))
            {
                conn.Open();
                var query = @"SELECT COUNT(DISTINCT ID_KHANG) FROM dbo.TD_KUOCVM_LSU WHERE id IN (SELECT MAX(ID) 
                                FROM dbo.TD_KUOCVM_LSU WHERE NGAY_LSU <='"+ngayCuoiQuy+"' GROUP BY ID_KUOCVM) AND SO_DU > 0 ";
                return Convert.ToInt32(conn.ExecuteScalar(query, commandType: System.Data.CommandType.Text, commandTimeout: 1800));
            }
        }
    }
}
