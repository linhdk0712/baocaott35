using DevExpress.XtraEditors;
using ReportApplication.BAL;
using System;

namespace ReportApplication.Common
{
    /// <summary>
    /// Class lấy danh sách các chi nhánh
    /// </summary>
    public class clsGetDanhMucChiNhanh : IDisposable
    {
        private clsDanhMucChiNhanh _danhMucChiNhanh;

        public void Dispose()
        {
            if (_danhMucChiNhanh == null) return;
            _danhMucChiNhanh.Dispose();
            _danhMucChiNhanh = null;
        }

        /// <summary>
        /// Tải danh sách các chi nhánh
        /// </summary>
        /// <param name="cbxDviPsinhDlieu"></param>
        public void GetDanMucChiNhanh(LookUpEdit cbxDviPsinhDlieu)
        {
            _danhMucChiNhanh = new clsDanhMucChiNhanh();
            cbxDviPsinhDlieu.Properties.DataSource = _danhMucChiNhanh.GetDanhMucChiNhanh();
            cbxDviPsinhDlieu.Properties.DisplayMember = "TEN_DVI";
            cbxDviPsinhDlieu.Properties.ValueMember = "MA_DVI";
            cbxDviPsinhDlieu.ItemIndex = 0;
        }
    }
}