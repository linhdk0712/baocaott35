using ReportApplication.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReportWpfApplication
{
    /// <summary>
    /// Interaction logic for G01254LaiSuat.xaml
    /// </summary>
    public partial class G01254LaiSuat : Window
    {
        public string NgayDL { get; set; }
        public G01254LaiSuat()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            clsG01254LaiSuat _clsG01254LaiSuat = new clsG01254LaiSuat();
            grdListLaiSuat.ItemsSource = _clsG01254LaiSuat.GetAll(NgayDL);
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            clsG01254LaiSuat _clsG01254LaiSuat = new clsG01254LaiSuat();
            var maLaiSuat = txtMaLoai.Text.Trim();
            if (maLaiSuat != null)
            {
                _clsG01254LaiSuat.NGAY_DL = NgayDL;
                _clsG01254LaiSuat.MA_LAI_SUAT = maLaiSuat;
                _clsG01254LaiSuat.LAI_SUAT = Convert.ToDecimal(txtGiaTri.Text.Trim());
                _clsG01254LaiSuat.KY_HAN = Convert.ToInt32(txtKyHan.Text.Trim());
                _clsG01254LaiSuat.DV_TINH = "THANG";
                _clsG01254LaiSuat.AddNew(_clsG01254LaiSuat);
            }
            LoadData();
        }

        private void grdListLaiSuat_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdListLaiSuat.SelectedItem != null)
            {
                clsG01254LaiSuat laiSuat = grdListLaiSuat.SelectedItem as clsG01254LaiSuat;
                string maLaiSuat = laiSuat.MA_LAI_SUAT;
                decimal giaTrilaiSuat = laiSuat.LAI_SUAT;
                int? kyHan = laiSuat.KY_HAN;
                txtMaLoai.Text = maLaiSuat;
                txtGiaTri.Text = giaTrilaiSuat.ToString();
                txtKyHan.Text = kyHan.ToString();
            }
            else
            {
                return;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
