using System.Web;

namespace ReportWebApplication.Commons
{
    public class clsControllerName
    {
        public static string ControllerName()
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            if (routeValues.ContainsKey("controller"))
                return (string)routeValues["controller"];

            return string.Empty;
        }
    }
}