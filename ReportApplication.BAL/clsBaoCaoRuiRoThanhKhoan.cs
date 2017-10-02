using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ReportApplication.BAL
{
    public class ClsRuiRoThanhKhoan
    {
        public int ID_TIEN_GUI { get; set; }
        public string MA_NHOM_SP { get; set; }
        public decimal SO_TIEN { get; set; }
        public int KY_HAN { get; set; }
    }

    public class ClsBaoCaoRuiRoThanhKhoan
    {
        public IEnumerable<clsRuiRoThanhKhoan> TaiDanhSachSoTienGui<clsRuiRoThanhKhoan>(DynamicParameters param)
        {
            using (var conn = new SqlConnection(clsConnectionStringBAL.ConnectionStringBAL()))
            {
                conn.Open();
                const string query = @"SELECT ID_TIEN_GUI,MA_NHOM_SP,SO_TIEN,CASE KY_HAN_DVI_TINH  WHEN 'NAM' THEN  KY_HAN * 12
                                                                                             WHEN 'TUAN' THEN KY_HAN / 4
                                                                                             WHEN 'NGAY' THEN KY_HAN * 30
                                                                                             WHEN 'THANG' THEN KY_HAN
			                                                                                 ELSE 0
                                                                                             END AS KY_HAN
                                FROM dbo.BL_TIEN_GUI_LSU
                                WHERE ID IN (
                                SELECT MAX(ID)
                                FROM dbo.BL_TIEN_GUI_LSU
                                WHERE NGAY_LSU <= @ngayDuLieu
                                GROUP BY ID_TIEN_GUI
                                )
                                AND SO_TIEN > 0;";
                return conn.Query<clsRuiRoThanhKhoan>(query, param, commandType: System.Data.CommandType.Text, commandTimeout: 1800);
            }
        }
    }
}