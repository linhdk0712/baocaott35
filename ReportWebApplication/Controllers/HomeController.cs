using PagedList;
using ReportApplication.BAL;
using ReportApplication.BAL.MongoDb;
using ReportApplication.Common;
using ReportWebApplication.Commons;
using ReportWebApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ReportWebApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly clsChildMenu _clsChildMenu;

        public HomeController()
        {
            _clsChildMenu = new clsChildMenu();
        }

        [HttpGet]
        public ActionResult Index(int? page, string search)
        {
            ViewBag.tukhoa = search;
            var pageNumber = page ?? 1;
            List<clsChildMenu> clsChildMenu;
            var result = _clsChildMenu.GetAllChildMenu().ToList();
            if (string.IsNullOrEmpty(search))
            {
                return View(result.ToPagedList(pageNumber, 5));
            }
            else
            {
                clsChildMenu = result.Where(x => x.FRM_CODE.Contains(search) || x.FRM_NAME.Contains(search) || x.FRM_DES.Contains(search) || x.MENU_CHA.Contains(search)).ToList();
                if (clsChildMenu.Count == 0)
                {
                    return View(result.ToPagedList(pageNumber, 5));
                }
            }

            var onePageOfList = clsChildMenu.ToPagedList(pageNumber, 5);
            return View(onePageOfList);
        }

        [HttpGet]
        public JsonResult GetListFiles()
        {
            var dir = new DirectoryInfo(Server.MapPath("~/Temp"));
            var files = dir.GetDirectories();
            var list = files.Select(item => new clsFileProperties()
                {
                    FileName = item.Name,
                    FilePath = item.FullName,
                    DateCreate = item.CreationTimeUtc.ToString("dd/MM/yyyy"),
                    FileSize = item.Extension
                })
                .ToList();

            return Json(new

            {
                data = list,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FileManager()
        {
            return View();
        }
        [HttpGet]
        public JsonResult KiemTraNgayLamViec()
        {
            var result = ClsKiemTrNgayLamViecDapper.CheckWorkingDate<ClsKiemTraNgayLamViecViewModel>();
            return Json(new
            {
                data = result,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Error()
        {
            return View("Error");
        }
        [HttpPost]
        public JsonResult TongHopDuLieu(string denNgay)
        {
            var check = false;           
            string message = null;
            string error = null;
            var month = new List<int>()
            {
                1,4,7,10
            };
            var donvi = new List<string>()
            {
                "00","0001","0002","0003","0000"
            };
            var result = ClsKiemTrNgayLamViecDapper.CheckWorkingDate<ClsKiemTraNgayLamViecViewModel>();
            var minDate =  DateTime.Parse(result.Min(x => x.NGAY_LVIEC));
            if (minDate > DateTime.Parse(denNgay))
            {
                // Ngày lấy dữ liệu
                var ngayDuLieu = (DateTime.Parse(denNgay));
                var ngayDauThang = clsGetLastDayOfMonth.GetFirstDayOfMont(ngayDuLieu.Month,ngayDuLieu.Year);
                var dateInQuater = clsDatesOfQuarter.DatesOfQuarter(denNgay);
                var ngay15 = dateInQuater[1].ToString("yyyyMMdd");
                try
                {
                    var clsMongoDb = new ClsMongoDb();
                    // Bảng cân đối tài khoản toàn hàng và các chi nhánh
                    foreach (var item in donvi)
                    {

                        var bangcandoi = clsMongoDb.BangCanDoiKeToan(item, ngayDauThang, ngayDuLieu.ToString("yyyyMMdd"));
                        if (bangcandoi > 0)
                        {
                            check = true;
                        }
                    }
                    if (month.Contains((DateTime.Parse(denNgay)).Month))
                    {
                        var bangcandoi = clsMongoDb.BangCanDoiKeToan(donvi[0], ngayDauThang, ngay15);
                        if (bangcandoi > 0)
                        {
                            check = true;
                        }
                    }
                    message = check ? @"Tổng hợp dữ liệu thành công" : @"Tổng hợp dữ liệu không thành công";

                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
            }
            else
            {
                message = "Ngày tổng hợp dữ liệu không được lớn hơn ngày làm việc nhỏ nhất trong hệ thống";
            }
           
            return Json(new
            {
                status = check,
                data = message + error
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SearchResult()
        {
           var result = new clsChildMenu().GetAllChildMenu();
            var list = new List<string>();
            foreach (var str in result)
            {
                list.Add(str.FRM_CODE);
                list.Add(str.FRM_DES);
                list.Add(str.FRM_NAME);
                list.Add(str.MENU_CHA);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        
    }
}