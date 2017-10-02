using ReportApplication.BAL;
using System.Windows.Controls;

namespace ReportWpfApplication.Common
{
    public class clsWPF
    {
        private readonly clsDanhMucChiNhanh _clsdanhMucChiNhanh;
        private ComboBox cbx;

        public clsWPF(ComboBox _cbx)
        {
            cbx = _cbx;
            _clsdanhMucChiNhanh = new clsDanhMucChiNhanh();
        }

        public void GetDanhMucChiNhanh()
        {
            cbx.ItemsSource = _clsdanhMucChiNhanh.GetDanhMucChiNhanh();
            cbx.DisplayMemberPath = "TEN_DVI";
            cbx.SelectedValuePath = "MA_DVI";
            cbx.SelectedIndex = 0;
        }
    }
}