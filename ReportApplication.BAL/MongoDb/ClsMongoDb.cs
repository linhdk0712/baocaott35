using Dapper;
using ReportApplication.BAL.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace ReportApplication.BAL.MongoDb
{
    public class ClsMongoDb : IDisposable
    {
        
        private readonly SqlConnection _sqlConnection;

        public void Dispose()
        {
            if (_sqlConnection != null)
                _sqlConnection.Dispose();
        }
        public ClsMongoDb()
        {
            _sqlConnection = new SqlConnection(clsConnectionString.ConnectionString());
            _sqlConnection.Open();
        }
        // Thực hiện việc cập nhật dữ liệu vào ngày cuối cùng của tháng hoặc ngày 15 của tháng đầu tiên trong quý
        public int BangCanDoiKeToan(string machinhanh,string tungay, string denngay,string loai = null)
        {
            var iCheck = 0;
            var param = new DynamicParameters();            
            var date = (DateTime.ParseExact(denngay, "yyyyMMdd", CultureInfo.InvariantCulture));
            var daysInMonth = new List<string>()
            {
                string.Format("{0}0115", date.Year),
                string.Format("{0}0415", date.Year),
                string.Format("{0}0715", date.Year),
                string.Format("{0}1015", date.Year),
                string.Format("{0}0131", date.Year),
                string.Format("{0}0228", date.Year),
                string.Format("{0}0229", date.Year),
                string.Format("{0}0331", date.Year),
                string.Format("{0}0430", date.Year),
                string.Format("{0}0531", date.Year),
                string.Format("{0}0630", date.Year),
                string.Format("{0}0731", date.Year),
                string.Format("{0}0831", date.Year),
                string.Format("{0}0930", date.Year),
                string.Format("{0}1031", date.Year),
                string.Format("{0}1130", date.Year),
                string.Format("{0}1231", date.Year)
            };
            if (!daysInMonth.Contains(denngay)) return iCheck;
            string queryCount = "";
            if (string.IsNullOrEmpty(loai))
            {
                queryCount = @"DELETE FROM dbo.M7_ACC_BALANCE_SHEET WHERE F10=@f10 AND F02 =@denNgay";
                param.Add("@f10", machinhanh);
                param.Add("@denNgay", denngay);
            }
            else
            {
                queryCount = @"DELETE FROM dbo.M7_ACC_BALANCE_SHEET WHERE F10=@f10 AND F02 =@denNgay AND F11 = @loai";
                param.Add("@f10", machinhanh);
                param.Add("@denNgay", denngay);
                param.Add("@loai", loai);
            }        
            var result = _sqlConnection.Execute(queryCount, param, commandType: System.Data.CommandType.Text);            
            var source = new ClsBangCanDoiTaiKhoanKeToan().GetAllDataChiTiet(machinhanh,tungay, denngay);
            const string queryInsert = @"INSERT INTO [dbo].[M7_ACC_BALANCE_SHEET] ([F01],[F02],[F03],[F04],[F05],[F06],[F07],[F08],[F09],[F10],[F11])
                                                                          VALUES( @f01,@f02,@f03,@f04,@f05,@f06,@f07,@f08,@f09,@f10,@f11)";
            foreach (var item in source)
            {
                param.Add("@f01", Guid.NewGuid());
                param.Add("@f02", denngay);
                param.Add("@f03", item.F03);
                param.Add("@f04", item.F04);
                param.Add("@f05", item.F05);
                param.Add("@f06", item.F06);
                param.Add("@f07", item.F07);
                param.Add("@f08", item.F08);
                param.Add("@f09", item.F09);
                param.Add("@f10", machinhanh);
                param.Add("@f11", loai);
                _sqlConnection.Execute(queryInsert, param, commandType: System.Data.CommandType.Text);
                iCheck++;
            }
            return iCheck;
        }
    }
}