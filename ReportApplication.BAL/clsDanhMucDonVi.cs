using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class clsDanhMucDonVi
    {
        public string MA_DVI { get; set; }
        public string MA_DVI_CHA { get; set; }

        public string GetDonVis(string machinhanh)
        {
            List<clsDanhMucDonVi> danhmucs = new List<clsDanhMucDonVi>();
            List<string> arrayString = new List<string>();
            using (var sqlConnection = new SqlConnection(clsConnectionStringBAL.ConnectionStringBAL()))
            {
                sqlConnection.Open();
                if (machinhanh == "00")
                {
                    const string queryGetData = @"SELECT MA_DVI,MA_DVI_CHA FROM DM_DON_VI WHERE MA_DVI_CHA =@madonvi";
                    var param = new DynamicParameters();
                    param.Add("@madonvi", machinhanh);
                    danhmucs = sqlConnection.Query<clsDanhMucDonVi>(queryGetData, param, commandType: System.Data.CommandType.Text, commandTimeout: 1800).ToList();                  
                    foreach (var item in danhmucs)
                    {
                        arrayString.Add(item.MA_DVI);
                       
                    }
                }
                else
                {
                    arrayString.Add(machinhanh);
                }
                string result = string.Join(",",arrayString);
                return result;
            }
        }

        public string GetPhongGiaoDichs(string machinhanh)
        {
            List<clsDanhMucDonVi> danhmucs = new List<clsDanhMucDonVi>();
            List<string> arrayString = new List<string>();
            using (var sqlConnection = new SqlConnection(clsConnectionStringBAL.ConnectionStringBAL()))
            {
                sqlConnection.Open();
                if (machinhanh == "00")
                {
                    const string queryGetData = @"SELECT MA_DVI,MA_DVI_CHA FROM dbo.DM_DON_VI WHERE LOAI_DVI IN ('VPGD','PGD')";                    
                    danhmucs = sqlConnection.Query<clsDanhMucDonVi>(queryGetData,commandType: System.Data.CommandType.Text, commandTimeout: 1800).ToList();
                    foreach (var item in danhmucs)
                    {
                        arrayString.Add(item.MA_DVI);

                    }
                }
                else
                {
                    const string queryGetData = @"SELECT MA_DVI,MA_DVI_CHA FROM dbo.DM_DON_VI WHERE LOAI_DVI IN ('VPGD','PGD') AND MA_DVI_CHA = @madonvi";
                    var param = new DynamicParameters();
                    param.Add("@madonvi", machinhanh);
                    danhmucs = sqlConnection.Query<clsDanhMucDonVi>(queryGetData, param, commandType: System.Data.CommandType.Text, commandTimeout: 1800).ToList();
                    foreach (var item in danhmucs)
                    {
                        arrayString.Add(item.MA_DVI);

                    }
                }
                string result = string.Join(",", arrayString);
                return result;
            }
        }
    }
}