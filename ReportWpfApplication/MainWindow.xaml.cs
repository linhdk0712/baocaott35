using DevExpress.Xpf.Core;
using ReportApplication.BAL;
using ReportApplication.Common;
using System;
using System.Reflection;
using System.Windows;

namespace ReportWpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private readonly clsChildMenu _clsChildMenu;

        public void Dispose()
        {
            if (_clsChildMenu != null)
                _clsChildMenu.Dispose();
        }

        public MainWindow()
        {
            InitializeComponent();
            _clsChildMenu = new clsChildMenu();
            for (int i = 0; i < 100; i++)
            {
                DXSplashScreen.Progress(i);
                DXSplashScreen.SetState(string.Format("{0} %", (i + 1)));
                System.Threading.Thread.Sleep(40);
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DXSplashScreen.Close();
            var result = _clsChildMenu.GetAllChildMenu();
            dataGrid.ItemsSource = result;
        }

        public object CreateWindow(string fullClassName)
        {
            Assembly asm = GetType().Assembly;
            object wnd = asm.CreateInstance(fullClassName);
            if (wnd == null)
            {
                EasyDialog.ShowErrorDialog("Báo cáo này đang trong quá trình xây dựng");
            }
            return wnd;
        }

        private void dataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clsChildMenu menu = dataGrid.SelectedItem as clsChildMenu;
            string name = menu.FRM_NAME;
            int? iDinhKyBaoCao = menu.ID_MENU_CHA;
            string fullClassName = "ReportWpfApplication." + name;
            Window wnd = (Window)CreateWindow(fullClassName);
            if (wnd != null)
            {
                PropertyInfo propertyInfo = wnd.GetType().GetProperty("iDinhKyBaoCao");
                propertyInfo.SetValue(wnd, iDinhKyBaoCao, null);
                wnd.ShowDialog();
            }
        }
    }
}