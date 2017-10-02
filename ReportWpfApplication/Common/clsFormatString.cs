using ReportApplication.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportWpfApplication.Common
{
    public class clsFormatString
    {
        public static string FormatStringDviTinhTrieuDong(decimal input)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", input / ClsDonViTinh.DviTinhTrieuDong());
        }

        public static string FormatStringDviTinhNghinDong(decimal input)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", input / ClsDonViTinh.DviTinhNghinDong());
        }

        public static string FormatStringDviTinhDong(decimal input)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", input / ClsDonViTinh.DviTinhDong());
        }

        public static string FormatStringTyLePhanTram(decimal input)
        {
            return string.Format(CultureInfo.CreateSpecificCulture("da-DK"),
                        "{0:00.0}", input);
        }
    }
}