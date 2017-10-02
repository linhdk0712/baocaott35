namespace ReportApplication.Common
{
    /// <summary>
    /// Xác định đơn vị tính của báo cáo
    /// </summary>
    public class ClsDonViTinh
    {
        /// <summary>
        /// Đơn vị tính là triệu Việt Nam đồng
        /// </summary>
        /// <returns></returns>
        public static decimal DviTinhTrieuDong()
        {
            return 1000000;
        }

        /// <summary>
        /// Đơn vị tính là nghìn Việt Nam đồng
        /// </summary>
        /// <returns></returns>
        public static decimal DviTinhNghinDong()
        {
            return 1000;
        }

        public static decimal DviTinhDong()
        {
            return 1;
        }
    }
}