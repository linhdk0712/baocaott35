namespace ReportApplication
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID_MENU_CHA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MENU_CHA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FRM_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FRM_NAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FRM_DES = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1035, 494);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 66);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1011, 416);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID_MENU_CHA,
            this.MENU_CHA,
            this.FRM_CODE,
            this.FRM_NAME,
            this.FRM_DES});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // ID_MENU_CHA
            // 
            this.ID_MENU_CHA.AppearanceCell.Options.UseTextOptions = true;
            this.ID_MENU_CHA.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ID_MENU_CHA.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ID_MENU_CHA.AppearanceHeader.Options.UseFont = true;
            this.ID_MENU_CHA.AppearanceHeader.Options.UseTextOptions = true;
            this.ID_MENU_CHA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ID_MENU_CHA.Caption = "Mã định kỳ báo cáo";
            this.ID_MENU_CHA.FieldName = "ID_MENU_CHA";
            this.ID_MENU_CHA.Name = "ID_MENU_CHA";
            this.ID_MENU_CHA.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.ID_MENU_CHA.Visible = true;
            this.ID_MENU_CHA.VisibleIndex = 0;
            // 
            // MENU_CHA
            // 
            this.MENU_CHA.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.MENU_CHA.AppearanceHeader.Options.UseFont = true;
            this.MENU_CHA.AppearanceHeader.Options.UseTextOptions = true;
            this.MENU_CHA.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MENU_CHA.Caption = "Định kỳ báo cáo";
            this.MENU_CHA.FieldName = "MENU_CHA";
            this.MENU_CHA.Name = "MENU_CHA";
            this.MENU_CHA.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.MENU_CHA.Visible = true;
            this.MENU_CHA.VisibleIndex = 1;
            // 
            // FRM_CODE
            // 
            this.FRM_CODE.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.FRM_CODE.AppearanceHeader.Options.UseFont = true;
            this.FRM_CODE.AppearanceHeader.Options.UseTextOptions = true;
            this.FRM_CODE.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FRM_CODE.Caption = "Mã báo cáo";
            this.FRM_CODE.FieldName = "FRM_CODE";
            this.FRM_CODE.Name = "FRM_CODE";
            this.FRM_CODE.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.FRM_CODE.Visible = true;
            this.FRM_CODE.VisibleIndex = 2;
            // 
            // FRM_NAME
            // 
            this.FRM_NAME.AppearanceCell.Options.UseTextOptions = true;
            this.FRM_NAME.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FRM_NAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.FRM_NAME.AppearanceHeader.Options.UseFont = true;
            this.FRM_NAME.AppearanceHeader.Options.UseTextOptions = true;
            this.FRM_NAME.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FRM_NAME.Caption = "Mã định danh báo cáo";
            this.FRM_NAME.FieldName = "FRM_NAME";
            this.FRM_NAME.Name = "FRM_NAME";
            this.FRM_NAME.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.FRM_NAME.Visible = true;
            this.FRM_NAME.VisibleIndex = 3;
            // 
            // FRM_DES
            // 
            this.FRM_DES.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.FRM_DES.AppearanceHeader.Options.UseFont = true;
            this.FRM_DES.AppearanceHeader.Options.UseTextOptions = true;
            this.FRM_DES.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.FRM_DES.Caption = "Tên báo cáo";
            this.FRM_DES.FieldName = "FRM_DES";
            this.FRM_DES.Name = "FRM_DES";
            this.FRM_DES.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.FRM_DES.Visible = true;
            this.FRM_DES.VisibleIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1035, 494);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 54);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1015, 420);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1015, 54);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 494);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.Text = "M7 Report Application";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn ID_MENU_CHA;
        private DevExpress.XtraGrid.Columns.GridColumn MENU_CHA;
        private DevExpress.XtraGrid.Columns.GridColumn FRM_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn FRM_NAME;
        private DevExpress.XtraGrid.Columns.GridColumn FRM_DES;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}