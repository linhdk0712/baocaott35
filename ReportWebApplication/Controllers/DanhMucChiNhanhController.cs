using ReportApplication.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportWebApplication.Controllers
{
    public class DanhMucChiNhanhController : Controller
    {
        // GET: DanhMucChiNhanh
        public PartialViewResult DanhMucChiNhanh()
        {
            var _clsDanhMucChiNhanh = new clsDanhMucChiNhanh();
            var listChiNhanh = _clsDanhMucChiNhanh.GetDanhMucChiNhanh();
            return PartialView("InputInformation", listChiNhanh);
        }
    }
}