using Dapper;
using ReportApplication.BAL.Common;
using ReportApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ReportApplication.BAL
{
    public class ClsBangCanDoiTaiKhoanKeToan : IDisposable
    {
        public string F02 { get; set; }
        public string F03 { get; set; }
        public decimal F04 { get; set; }
        public decimal F05 { get; set; }
        public decimal F06 { get; set; }
        public decimal F07 { get; set; }
        public decimal F08 { get; set; }
        public decimal F09 { get; set; }
        public string F10 { get; set; }

        public string F11 { get; set; }
        /// <summary>
        ///
        /// </summary>
        private readonly ReportApplicationEntities _reportApplicationEntities;

        public ClsBangCanDoiTaiKhoanKeToan()
        {
            _reportApplicationEntities = new ReportApplicationEntities();
        }

        public void Dispose()
        {
            _reportApplicationEntities?.Dispose();
        }

        public IEnumerable<ClsBangCanDoiTaiKhoanKeToan> GetAllData(string denNgay, string maChiNhanh = null,string loai = null)
        {
            using (var sqlConnection = new SqlConnection(clsConnectionString.ConnectionString()))
            {
                sqlConnection.Open();
                const string queryGetData = @"SELECT [F02],[F03],[F04],[F05],[F06],[F07],[F08],[F09],[F10],[F11]
                                            FROM [M7_ACC_BALANCE_SHEET] WHERE F02 = @denngay AND F11 = @loai";
                var param = new DynamicParameters();               
                param.Add("@denngay", denNgay);
                param.Add("@loai", loai);
                return sqlConnection.Query<ClsBangCanDoiTaiKhoanKeToan>(queryGetData,param,commandType:System.Data.CommandType.Text,commandTimeout:1800);
            } 
          
        }
        public IEnumerable<ClsBangCanDoiTaiKhoanKeToan> GetAllDataCap4(string maChiNhanh, string tuNgay, string denNgay)
        {
            object[] param =
            {
                new SqlParameter("@MaChiNhanh",maChiNhanh),
                new SqlParameter("@TuNgay",tuNgay),
                new SqlParameter("@DenNgay",denNgay),
                new SqlParameter("@CapChiTiet","CAP_4")
            };
            _reportApplicationEntities.Database.CommandTimeout = 1800;
            var result = _reportApplicationEntities.Database.SqlQuery<ClsBangCanDoiTaiKhoanKeToan>("BangCanDoiTaiKhoanKeToan @MaChiNhanh,@TuNgay,@DenNgay,@CapChiTiet", param).ToList();
            return result;
        }
        public IEnumerable<ClsBangCanDoiTaiKhoanKeToan> GetAllDataChiTiet(string maChiNhanh,string tuNgay, string denNgay)
        {
            var danhmucs = new clsDanhMucDonVi();
            var donvis = danhmucs.GetDonVis(maChiNhanh);
            var phonggiaodichs = danhmucs.GetPhongGiaoDichs(maChiNhanh);
            using (var sqlConnection = new SqlConnection(clsConnectionStringBAL.ConnectionStringBAL()))
            {
                sqlConnection.Open();
                const string queryGetData = @"BangCanDoiTaiKhoanKeToan";
                var param = new DynamicParameters();
                param.Add("@MaChiNhanh", donvis);
                param.Add("@MaPhongGiaoDich", phonggiaodichs);
                param.Add("@TuNgay",tuNgay );
                param.Add("@DenNgay", denNgay);
                return sqlConnection.Query<ClsBangCanDoiTaiKhoanKeToan>(queryGetData,param,commandType: System.Data.CommandType.StoredProcedure, commandTimeout: 1800);
            }

        }
    }
}