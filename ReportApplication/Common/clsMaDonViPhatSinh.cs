namespace ReportApplication.Common
{
    /// <summary>
    /// Class trả về đơn vị phát sinh dữ liệu
    /// </summary>
    public class clsMaDonViPhatSinh
    {
        /// <summary>
        /// Mã ngân hàng của đơn vị phát sinh dữ liệu
        /// </summary>
        /// <param name="maDonVi"></param>
        /// <returns></returns>
        public static string GetMaNganHang(string maDonVi)
        {
            string ma = null;
            switch (maDonVi)
            {
                case "00":
                    return ma = "01912001";

                case "0000":
                    return ma = "01912001";

                case "0001":
                    return ma = "22912002";

                case "0002":
                    return ma = "22912001";

                case "0003":
                    return ma = "14912001";
            }
            return ma;
        }
    }
}