using Dapper;
using ReportApplication.BAL;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ReportWebApplication.Commons
{
    public class ClsKiemTrNgayLamViecDapper
    {
        public static IEnumerable<ClsKiemTraNgayLamViecViewModel> CheckWorkingDate<ClsKiemTraNgayLamViecViewModel>(DynamicParameters param = null)
        {
            using (var conn = new SqlConnection(clsConnectionStringBAL.ConnectionStringBAL()))
            {
                conn.Open();
                const string query = @"SELECT dbo.DM_DON_VI.TEN_GDICH TEN_GDICH,
                                    CONVERT(VARCHAR(20), CONVERT(DATETIME, dbo.HT_NGAY_LVIEC.NGAY_LVIEC), 103) NGAY_LVIEC
                                    FROM dbo.HT_NGAY_LVIEC
                                    INNER JOIN dbo.DM_DON_VI
                                    ON HT_NGAY_LVIEC.MA_DVI = DM_DON_VI.MA_DVI;";
                return conn.Query<ClsKiemTraNgayLamViecViewModel>(query, param, commandType: System.Data.CommandType.Text, commandTimeout: 1800);
            }
        }
    }
}