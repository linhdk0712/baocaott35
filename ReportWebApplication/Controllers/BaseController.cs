using ReportApplication.BAL;
using ReportApplication.Common;
using ReportWebApplication.Commons;
using System;
using System.Web.Mvc;

namespace ReportWebApplication.Controllers
{
    public class BaseController : Controller
    {
        protected readonly clsSetFileNameReport _setFileName;
        protected readonly string _maDonViGui = clsMaDonViGui.MaDonViGui();
        protected clsGetFileNameReportsWeb _createFileNameReport;
        protected readonly string _user = clsUserCheckReport.IdUser;
        protected readonly decimal dviTinh = ClsDonViTinh.DviTinhTrieuDong();
        protected clsDeleteFileTemp _clsDeleteFileTemp;
        protected clsCreateNewFolder _clsCreateNewFolder;
        protected clsSoLieuToanHeThongVaDonVi _clsSoLieuToanHeThongVaDonVi;
        protected clsDinhKyBaoCao _clsDinhKyBaoCao;

        public BaseController()
        {
            _setFileName = new clsSetFileNameReport();
            _clsDeleteFileTemp = new clsDeleteFileTemp();
            _clsSoLieuToanHeThongVaDonVi = new clsSoLieuToanHeThongVaDonVi();
            _clsDinhKyBaoCao = new clsDinhKyBaoCao();
        }

        protected string GetFolderName(string denNgay, string _reportName)
        {
            _clsCreateNewFolder = new clsCreateNewFolder();
            var yyyyMM = (DateTime.Parse(denNgay)).ToString("yyyyMM");
            var folderName = _clsCreateNewFolder.CreatNewFolder(Server.MapPath("~/Temp/" + yyyyMM));
            var path = Server.MapPath("~/Temp/" + folderName);
            _clsDeleteFileTemp.DeleteFileOnTemp(_reportName, path);
            return folderName;
        }
    }
}