using System;
using System.Collections.Generic;

namespace ReportApplication.Common
{
    /// <summary>
    /// Class tính toán ngày trong tháng
    /// </summary>
    public class clsGetLastDayOfMonth
    {
        /// <summary>
        /// Lấy ngày cuối cùng trong tháng
        /// </summary>
        /// <param name="iMonth"></param>
        /// <returns></returns>
        public static int GetLastDayOfMonth(int iMonth,int Years)
        {
            var dtResult = new DateTime(Years, iMonth, 1);
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult.Day;
        }

        /// <summary>
        /// Lấy ngày đầu tiên trong tháng
        /// </summary>
        /// <param name="iMonts"></param>
        /// <returns></returns>
        public static string GetFirstDayOfMont(int iMonts,int Years)
        {
            return new DateTime(Years, iMonts, 1).ToString("yyyyMMdd");
        }

        /// <summary>
        /// Lấy ngày cuối cùng của tháng trước;
        /// </summary>
        /// <param name="iMonth"></param>
        /// <returns></returns>
        public static string GetLastDayOfPreMont(int iMonth,int Years)
        {
            var firstDayOfThisMonth = new DateTime(Years, iMonth, 1);
            return firstDayOfThisMonth.AddDays(-1).ToString("yyyyMMdd");
        }

        /// <summary>
        /// Lấy về một khoảng ngày trong tháng
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<DateTime> GetRange(DateTime startDate, DateTime endDate)
        {
            var res = new List<DateTime>();
            var start = startDate;
            var end = endDate;
            for (var date = start; date <= end; date = date.AddDays(1))
                res.Add(date);
            return res;
        }
      
    }
}