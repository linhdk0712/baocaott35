namespace ReportApplication
{
    partial class G01204
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
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MA_KHACH_HANG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TEN_KHACH_HANG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SO_KHE_UOC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TGIAN_VAY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TGIAN_VAY_DVI_TINH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SO_DU = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DU_NO_NHOM_1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DU_NO_NHOM_2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DU_NO_NHOM_3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DU_NO_NHOM_4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DU_NO_NHOM_5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCreatReport = new DevExpress.XtraEditors.SimpleButton();
            this.btnGetData = new DevExpress.XtraEditors.SimpleButton();
            this.dateNgayPsinhDlieu = new DevExpress.XtraEditors.DateEdit();
            this.cbxDviPsinhDlieu = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayPsinhDlieu.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayPsinhDlieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDviPsinhDlieu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.radioGroup1);
            this.layoutControl1.Controls.Add(this.gridControl1);
            this.layoutControl1.Controls.Add(this.btnCreatReport);
            this.layoutControl1.Controls.Add(this.btnGetData);
            this.layoutControl1.Controls.Add(this.dateNgayPsinhDlieu);
            this.layoutControl1.Controls.Add(this.cbxDviPsinhDlieu);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(984, 491);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = "M";
            this.radioGroup1.Location = new System.Drawing.Point(135, 38);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("M", "Báo cáo chính"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("N", "Báo cáo không phát sinh dữ liệu"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("R", "File thuyết minh")});
            this.radioGroup1.Size = new System.Drawing.Size(837, 25);
            this.radioGroup1.StyleController = this.layoutControl1;
            this.radioGroup1.TabIndex = 9;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 67);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(960, 412);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.MA_KHACH_HANG,
            this.TEN_KHACH_HANG,
            this.SO_KHE_UOC,
            this.TGIAN_VAY,
            this.TGIAN_VAY_DVI_TINH,
            this.SO_DU,
            this.DU_NO_NHOM_1,
            this.DU_NO_NHOM_2,
            this.DU_NO_NHOM_3,
            this.DU_NO_NHOM_4,
            this.DU_NO_NHOM_5});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // MA_KHACH_HANG
            // 
            this.MA_KHACH_HANG.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.MA_KHACH_HANG.AppearanceHeader.Options.UseFont = true;
            this.MA_KHACH_HANG.AppearanceHeader.Options.UseTextOptions = true;
            this.MA_KHACH_HANG.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.MA_KHACH_HANG.Caption = "MA_KHACH_HANG";
            this.MA_KHACH_HANG.FieldName = "MA_KHACH_HANG";
            this.MA_KHACH_HANG.Name = "MA_KHACH_HANG";
            this.MA_KHACH_HANG.Visible = true;
            this.MA_KHACH_HANG.VisibleIndex = 0;
            // 
            // TEN_KHACH_HANG
            // 
            this.TEN_KHACH_HANG.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.TEN_KHACH_HANG.AppearanceHeader.Options.UseFont = true;
            this.TEN_KHACH_HANG.AppearanceHeader.Options.UseTextOptions = true;
            this.TEN_KHACH_HANG.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TEN_KHACH_HANG.Caption = "TEN_KHACH_HANG";
            this.TEN_KHACH_HANG.FieldName = "TEN_KHACH_HANG";
            this.TEN_KHACH_HANG.Name = "TEN_KHACH_HANG";
            this.TEN_KHACH_HANG.Visible = true;
            this.TEN_KHACH_HANG.VisibleIndex = 1;
            // 
            // SO_KHE_UOC
            // 
            this.SO_KHE_UOC.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.SO_KHE_UOC.AppearanceHeader.Options.UseFont = true;
            this.SO_KHE_UOC.AppearanceHeader.Options.UseTextOptions = true;
            this.SO_KHE_UOC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SO_KHE_UOC.Caption = "SO_KHE_UOC";
            this.SO_KHE_UOC.FieldName = "SO_KHE_UOC";
            this.SO_KHE_UOC.Name = "SO_KHE_UOC";
            this.SO_KHE_UOC.Visible = true;
            this.SO_KHE_UOC.VisibleIndex = 2;
            // 
            // TGIAN_VAY
            // 
            this.TGIAN_VAY.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.TGIAN_VAY.AppearanceHeader.Options.UseFont = true;
            this.TGIAN_VAY.AppearanceHeader.Options.UseTextOptions = true;
            this.TGIAN_VAY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TGIAN_VAY.Caption = "TGIAN_VAY";
            this.TGIAN_VAY.FieldName = "TGIAN_VAY";
            this.TGIAN_VAY.Name = "TGIAN_VAY";
            this.TGIAN_VAY.Visible = true;
            this.TGIAN_VAY.VisibleIndex = 3;
            // 
            // TGIAN_VAY_DVI_TINH
            // 
            this.TGIAN_VAY_DVI_TINH.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.TGIAN_VAY_DVI_TINH.AppearanceHeader.Options.UseFont = true;
            this.TGIAN_VAY_DVI_TINH.AppearanceHeader.Options.UseTextOptions = true;
            this.TGIAN_VAY_DVI_TINH.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TGIAN_VAY_DVI_TINH.Caption = "TGIAN_VAY_DVI_TINH";
            this.TGIAN_VAY_DVI_TINH.FieldName = "TGIAN_VAY_DVI_TINH";
            this.TGIAN_VAY_DVI_TINH.Name = "TGIAN_VAY_DVI_TINH";
            this.TGIAN_VAY_DVI_TINH.Visible = true;
            this.TGIAN_VAY_DVI_TINH.VisibleIndex = 4;
            // 
            // SO_DU
            // 
            this.SO_DU.AppearanceCell.Options.UseTextOptions = true;
            this.SO_DU.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.SO_DU.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.SO_DU.AppearanceHeader.Options.UseFont = true;
            this.SO_DU.AppearanceHeader.Options.UseTextOptions = true;
            this.SO_DU.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.SO_DU.Caption = "SO_DU";
            this.SO_DU.DisplayFormat.FormatString = "n0";
            this.SO_DU.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SO_DU.FieldName = "SO_DU";
            this.SO_DU.Name = "SO_DU";
            this.SO_DU.Visible = true;
            this.SO_DU.VisibleIndex = 5;
            // 
            // DU_NO_NHOM_1
            // 
            this.DU_NO_NHOM_1.AppearanceCell.Options.UseTextOptions = true;
            this.DU_NO_NHOM_1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DU_NO_NHOM_1.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.DU_NO_NHOM_1.AppearanceHeader.Options.UseFont = true;
            this.DU_NO_NHOM_1.AppearanceHeader.Options.UseTextOptions = true;
            this.DU_NO_NHOM_1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DU_NO_NHOM_1.Caption = "DU_NO_NHOM_1";
            this.DU_NO_NHOM_1.DisplayFormat.FormatString = "n0";
            this.DU_NO_NHOM_1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.DU_NO_NHOM_1.FieldName = "DU_NO_NHOM_1";
            this.DU_NO_NHOM_1.Name = "DU_NO_NHOM_1";
            this.DU_NO_NHOM_1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DU_NO_NHOM_1", "{0:n0}")});
            this.DU_NO_NHOM_1.Visible = true;
            this.DU_NO_NHOM_1.VisibleIndex = 6;
            // 
            // DU_NO_NHOM_2
            // 
            this.DU_NO_NHOM_2.AppearanceCell.Options.UseTextOptions = true;
            this.DU_NO_NHOM_2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DU_NO_NHOM_2.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.DU_NO_NHOM_2.AppearanceHeader.Options.UseFont = true;
            this.DU_NO_NHOM_2.AppearanceHeader.Options.UseTextOptions = true;
            this.DU_NO_NHOM_2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DU_NO_NHOM_2.Caption = "DU_NO_NHOM_2";
            this.DU_NO_NHOM_2.DisplayFormat.FormatString = "n0";
            this.DU_NO_NHOM_2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.DU_NO_NHOM_2.FieldName = "DU_NO_NHOM_2";
            this.DU_NO_NHOM_2.Name = "DU_NO_NHOM_2";
            this.DU_NO_NHOM_2.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DU_NO_NHOM_2", "{0:n0}")});
            this.DU_NO_NHOM_2.Visible = true;
            this.DU_NO_NHOM_2.VisibleIndex = 7;
            // 
            // DU_NO_NHOM_3
            // 
            this.DU_NO_NHOM_3.AppearanceCell.Options.UseTextOptions = true;
            this.DU_NO_NHOM_3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DU_NO_NHOM_3.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.DU_NO_NHOM_3.AppearanceHeader.Options.UseFont = true;
            this.DU_NO_NHOM_3.AppearanceHeader.Options.UseTextOptions = true;
            this.DU_NO_NHOM_3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DU_NO_NHOM_3.Caption = "DU_NO_NHOM_3";
            this.DU_NO_NHOM_3.DisplayFormat.FormatString = "n0";
            this.DU_NO_NHOM_3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.DU_NO_NHOM_3.FieldName = "DU_NO_NHOM_3";
            this.DU_NO_NHOM_3.Name = "DU_NO_NHOM_3";
            this.DU_NO_NHOM_3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DU_NO_NHOM_3", "{0:n0}")});
            this.DU_NO_NHOM_3.Visible = true;
            this.DU_NO_NHOM_3.VisibleIndex = 8;
            // 
            // DU_NO_NHOM_4
            // 
            this.DU_NO_NHOM_4.AppearanceCell.Options.UseTextOptions = true;
            this.DU_NO_NHOM_4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DU_NO_NHOM_4.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.DU_NO_NHOM_4.AppearanceHeader.Options.UseFont = true;
            this.DU_NO_NHOM_4.AppearanceHeader.Options.UseTextOptions = true;
            this.DU_NO_NHOM_4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DU_NO_NHOM_4.Caption = "DU_NO_NHOM_4";
            this.DU_NO_NHOM_4.DisplayFormat.FormatString = "n0";
            this.DU_NO_NHOM_4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.DU_NO_NHOM_4.FieldName = "DU_NO_NHOM_4";
            this.DU_NO_NHOM_4.Name = "DU_NO_NHOM_4";
            this.DU_NO_NHOM_4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DU_NO_NHOM_4", "{0:n0}")});
            this.DU_NO_NHOM_4.Visible = true;
            this.DU_NO_NHOM_4.VisibleIndex = 9;
            // 
            // DU_NO_NHOM_5
            // 
            this.DU_NO_NHOM_5.AppearanceCell.Options.UseTextOptions = true;
            this.DU_NO_NHOM_5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.DU_NO_NHOM_5.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.DU_NO_NHOM_5.AppearanceHeader.Options.UseFont = true;
            this.DU_NO_NHOM_5.AppearanceHeader.Options.UseTextOptions = true;
            this.DU_NO_NHOM_5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.DU_NO_NHOM_5.Caption = "DU_NO_NHOM_5";
            this.DU_NO_NHOM_5.DisplayFormat.FormatString = "n0";
            this.DU_NO_NHOM_5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.DU_NO_NHOM_5.FieldName = "DU_NO_NHOM_5";
            this.DU_NO_NHOM_5.Name = "DU_NO_NHOM_5";
            this.DU_NO_NHOM_5.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DU_NO_NHOM_5", "{0:n0}")});
            this.DU_NO_NHOM_5.Visible = true;
            this.DU_NO_NHOM_5.VisibleIndex = 10;
            // 
            // btnCreatReport
            // 
            this.btnCreatReport.Location = new System.Drawing.Point(752, 12);
            this.btnCreatReport.Name = "btnCreatReport";
            this.btnCreatReport.Size = new System.Drawing.Size(76, 22);
            this.btnCreatReport.StyleController = this.layoutControl1;
            this.btnCreatReport.TabIndex = 7;
            this.btnCreatReport.Text = "Tạo báo cáo";
            this.btnCreatReport.Click += new System.EventHandler(this.btnCreatReport_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(672, 12);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(76, 22);
            this.btnGetData.StyleController = this.layoutControl1;
            this.btnGetData.TabIndex = 6;
            this.btnGetData.Text = "Lấy dữ liệu";
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // dateNgayPsinhDlieu
            // 
            this.dateNgayPsinhDlieu.EditValue = null;
            this.dateNgayPsinhDlieu.Location = new System.Drawing.Point(476, 12);
            this.dateNgayPsinhDlieu.Name = "dateNgayPsinhDlieu";
            this.dateNgayPsinhDlieu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayPsinhDlieu.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateNgayPsinhDlieu.Size = new System.Drawing.Size(192, 20);
            this.dateNgayPsinhDlieu.StyleController = this.layoutControl1;
            this.dateNgayPsinhDlieu.TabIndex = 5;
            // 
            // cbxDviPsinhDlieu
            // 
            this.cbxDviPsinhDlieu.Location = new System.Drawing.Point(135, 12);
            this.cbxDviPsinhDlieu.Name = "cbxDviPsinhDlieu";
            this.cbxDviPsinhDlieu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxDviPsinhDlieu.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MA_DVI", "Mã đơn vị"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TEN_DVI", "Tên đơn vị")});
            this.cbxDviPsinhDlieu.Properties.NullText = "";
            this.cbxDviPsinhDlieu.Size = new System.Drawing.Size(214, 20);
            this.cbxDviPsinhDlieu.StyleController = this.layoutControl1;
            this.cbxDviPsinhDlieu.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(984, 491);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(820, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(144, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cbxDviPsinhDlieu;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(341, 26);
            this.layoutControlItem1.Text = "Đơn vị phát sinh dữ liệu :";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(120, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dateNgayPsinhDlieu;
            this.layoutControlItem2.Location = new System.Drawing.Point(341, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(319, 26);
            this.layoutControlItem2.Text = "Ngày phát sinh dữ liệu :";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(120, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnGetData;
            this.layoutControlItem3.Location = new System.Drawing.Point(660, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnCreatReport;
            this.layoutControlItem4.Location = new System.Drawing.Point(740, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gridControl1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 55);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(964, 416);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.radioGroup1;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(964, 29);
            this.layoutControlItem6.Text = "Loại báo cáo";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(120, 13);
            // 
            // G01204
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 491);
            this.Controls.Add(this.layoutControl1);
            this.Name = "G01204";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.G01204_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayPsinhDlieu.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNgayPsinhDlieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxDviPsinhDlieu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.LookUpEdit cbxDviPsinhDlieu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnCreatReport;
        private DevExpress.XtraEditors.SimpleButton btnGetData;
        private DevExpress.XtraEditors.DateEdit dateNgayPsinhDlieu;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn MA_KHACH_HANG;
        private DevExpress.XtraGrid.Columns.GridColumn TEN_KHACH_HANG;
        private DevExpress.XtraGrid.Columns.GridColumn SO_KHE_UOC;
        private DevExpress.XtraGrid.Columns.GridColumn TGIAN_VAY;
        private DevExpress.XtraGrid.Columns.GridColumn TGIAN_VAY_DVI_TINH;
        private DevExpress.XtraGrid.Columns.GridColumn SO_DU;
        private DevExpress.XtraGrid.Columns.GridColumn DU_NO_NHOM_1;
        private DevExpress.XtraGrid.Columns.GridColumn DU_NO_NHOM_2;
        private DevExpress.XtraGrid.Columns.GridColumn DU_NO_NHOM_3;
        private DevExpress.XtraGrid.Columns.GridColumn DU_NO_NHOM_4;
        private DevExpress.XtraGrid.Columns.GridColumn DU_NO_NHOM_5;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}