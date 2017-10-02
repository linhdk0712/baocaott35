using ReportApplication.BAL;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ReportApplication
{
    /// <summary>
    ///
    /// </summary>
    public partial class FrmMain : Form
    {
        private readonly clsChildMenu _clsChildMenu;

        /// <summary>
        /// Khởi tạo hàm Main
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            _clsChildMenu = new clsChildMenu();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = _clsChildMenu.GetAllChildMenu().ToList();
        }

        private bool CheckFormActive(Control frm)
        {
            foreach (var child in MdiChildren.Where(child => child.Name == frm.Name))
            {
                child.Activate();
                return true;
            }
            return false;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            var frmName = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FRM_NAME").ToString();
            var chuKyBaoCao = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ID_MENU_CHA"));
            var frmAssembly = Assembly.LoadFile(Application.ExecutablePath);
            foreach (var type in frmAssembly.GetTypes())
            {
                if (type.BaseType != typeof(Form)) continue;
                if (type.Name != frmName) continue;
                var frmShow = (Form)frmAssembly.CreateInstance(type.ToString());
                if (frmShow != null)
                {
                    var propertyInfo = frmShow.GetType().GetProperty("ChuKyBaoCao");
                    if (CheckFormActive(frmShow))
                    {
                        return;
                    }
                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(frmShow, chuKyBaoCao, null);
                    }
                }
                if (frmShow == null) continue;
                frmShow.ShowDialog();
                frmShow.Close();
                frmShow.Dispose();
            }
        }
    }
}