namespace 电池测试系统.InformationSystems
{
    partial class dataManagement
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_FinishedProduct = new System.Windows.Forms.RadioButton();
            this.RB_ElectricCore = new System.Windows.Forms.RadioButton();
            this.RB_All = new System.Windows.Forms.RadioButton();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.Dti_Begin = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.Dti_End = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.cbe_ProductionClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX60 = new DevComponents.DotNetBar.LabelX();
            this.txt_QRCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btni_Query = new DevComponents.DotNetBar.ButtonItem();
            this.btni_Delete = new DevComponents.DotNetBar.ButtonItem();
            this.btni_DeleteAll = new DevComponents.DotNetBar.ButtonItem();
            this.btni_DeleteSelected = new DevComponents.DotNetBar.ButtonItem();
            this.btni_Condition = new DevComponents.DotNetBar.ButtonItem();
            this.btni_DataOut = new DevComponents.DotNetBar.ButtonItem();
            this.dataGridViewX1 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除此界面所以数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除选中的数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.按条件删除数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dti_Begin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dti_End)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.comboBoxEx1);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.groupBox1);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.Dti_Begin);
            this.panelEx1.Controls.Add(this.Dti_End);
            this.panelEx1.Controls.Add(this.cbe_ProductionClass);
            this.panelEx1.Controls.Add(this.labelX60);
            this.panelEx1.Controls.Add(this.txt_QRCode);
            this.panelEx1.Controls.Add(this.labelX6);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1227, 151);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxEx1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 16;
            this.comboBoxEx1.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.comboBoxEx1.Location = new System.Drawing.Point(342, 79);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(147, 22);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 82;
            this.comboBoxEx1.SelectionChangeCommitted += new System.EventHandler(this.comboBoxEx1_SelectionChangeCommitted);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "条码测试数据";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "快速测试数据";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(280, 82);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 81;
            this.labelX1.Text = "电池类型";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_FinishedProduct);
            this.groupBox1.Controls.Add(this.RB_ElectricCore);
            this.groupBox1.Controls.Add(this.RB_All);
            this.groupBox1.Location = new System.Drawing.Point(516, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 46);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "电池规格";
            // 
            // RB_FinishedProduct
            // 
            this.RB_FinishedProduct.AutoSize = true;
            this.RB_FinishedProduct.Location = new System.Drawing.Point(133, 20);
            this.RB_FinishedProduct.Name = "RB_FinishedProduct";
            this.RB_FinishedProduct.Size = new System.Drawing.Size(47, 16);
            this.RB_FinishedProduct.TabIndex = 2;
            this.RB_FinishedProduct.TabStop = true;
            this.RB_FinishedProduct.Text = "成品";
            this.RB_FinishedProduct.UseVisualStyleBackColor = true;
            // 
            // RB_ElectricCore
            // 
            this.RB_ElectricCore.AutoSize = true;
            this.RB_ElectricCore.Location = new System.Drawing.Point(80, 20);
            this.RB_ElectricCore.Name = "RB_ElectricCore";
            this.RB_ElectricCore.Size = new System.Drawing.Size(47, 16);
            this.RB_ElectricCore.TabIndex = 1;
            this.RB_ElectricCore.TabStop = true;
            this.RB_ElectricCore.Text = "电芯";
            this.RB_ElectricCore.UseVisualStyleBackColor = true;
            // 
            // RB_All
            // 
            this.RB_All.AutoSize = true;
            this.RB_All.Checked = true;
            this.RB_All.Location = new System.Drawing.Point(27, 20);
            this.RB_All.Name = "RB_All";
            this.RB_All.Size = new System.Drawing.Size(47, 16);
            this.RB_All.TabIndex = 0;
            this.RB_All.TabStop = true;
            this.RB_All.Text = "全部";
            this.RB_All.UseVisualStyleBackColor = true;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(495, 30);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(19, 18);
            this.labelX3.TabIndex = 79;
            this.labelX3.Text = "至";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(294, 30);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(19, 18);
            this.labelX2.TabIndex = 78;
            this.labelX2.Text = "从";
            // 
            // Dti_Begin
            // 
            // 
            // 
            // 
            this.Dti_Begin.BackgroundStyle.Class = "DateTimeInputBackground";
            this.Dti_Begin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Dti_Begin.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.Dti_Begin.ButtonDropDown.Visible = true;
            this.Dti_Begin.CustomFormat = "yyyy-MM-dd HH:mm";
            this.Dti_Begin.DateTimeSelectorVisibility = DevComponents.Editors.DateTimeAdv.eDateTimeSelectorVisibility.Both;
            this.Dti_Begin.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.Dti_Begin.IsPopupCalendarOpen = false;
            this.Dti_Begin.Location = new System.Drawing.Point(319, 27);
            // 
            // 
            // 
            // 
            // 
            // 
            this.Dti_Begin.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Dti_Begin.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.Dti_Begin.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.Dti_Begin.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.Dti_Begin.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.Dti_Begin.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.Dti_Begin.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.Dti_Begin.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.Dti_Begin.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.Dti_Begin.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Dti_Begin.MonthCalendar.DayClickAutoClosePopup = false;
            this.Dti_Begin.MonthCalendar.DisplayMonth = new System.DateTime(2016, 8, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.Dti_Begin.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.Dti_Begin.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.Dti_Begin.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.Dti_Begin.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Dti_Begin.MonthCalendar.TodayButtonVisible = true;
            this.Dti_Begin.Name = "Dti_Begin";
            this.Dti_Begin.Size = new System.Drawing.Size(170, 21);
            this.Dti_Begin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Dti_Begin.TabIndex = 77;
            // 
            // Dti_End
            // 
            // 
            // 
            // 
            this.Dti_End.BackgroundStyle.Class = "DateTimeInputBackground";
            this.Dti_End.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Dti_End.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.Dti_End.ButtonDropDown.Visible = true;
            this.Dti_End.CustomFormat = "yyyy-MM-dd HH:mm";
            this.Dti_End.DateTimeSelectorVisibility = DevComponents.Editors.DateTimeAdv.eDateTimeSelectorVisibility.Both;
            this.Dti_End.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.Dti_End.IsPopupCalendarOpen = false;
            this.Dti_End.Location = new System.Drawing.Point(516, 27);
            // 
            // 
            // 
            // 
            // 
            // 
            this.Dti_End.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Dti_End.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.Dti_End.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.Dti_End.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.Dti_End.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.Dti_End.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.Dti_End.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.Dti_End.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.Dti_End.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.Dti_End.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Dti_End.MonthCalendar.DayClickAutoClosePopup = false;
            this.Dti_End.MonthCalendar.DisplayMonth = new System.DateTime(2016, 8, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.Dti_End.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.Dti_End.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.Dti_End.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.Dti_End.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Dti_End.MonthCalendar.TodayButtonVisible = true;
            this.Dti_End.Name = "Dti_End";
            this.Dti_End.Size = new System.Drawing.Size(170, 21);
            this.Dti_End.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Dti_End.TabIndex = 76;
            // 
            // cbe_ProductionClass
            // 
            this.cbe_ProductionClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbe_ProductionClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbe_ProductionClass.DisplayMember = "Text";
            this.cbe_ProductionClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbe_ProductionClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbe_ProductionClass.FormattingEnabled = true;
            this.cbe_ProductionClass.ItemHeight = 15;
            this.cbe_ProductionClass.Location = new System.Drawing.Point(92, 79);
            this.cbe_ProductionClass.Name = "cbe_ProductionClass";
            this.cbe_ProductionClass.Size = new System.Drawing.Size(168, 21);
            this.cbe_ProductionClass.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbe_ProductionClass.TabIndex = 75;
            // 
            // labelX60
            // 
            this.labelX60.AutoSize = true;
            // 
            // 
            // 
            this.labelX60.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX60.Location = new System.Drawing.Point(30, 82);
            this.labelX60.Name = "labelX60";
            this.labelX60.Size = new System.Drawing.Size(56, 18);
            this.labelX60.TabIndex = 74;
            this.labelX60.Text = "电池类型";
            // 
            // txt_QRCode
            // 
            // 
            // 
            // 
            this.txt_QRCode.Border.Class = "TextBoxBorder";
            this.txt_QRCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_QRCode.Location = new System.Drawing.Point(92, 27);
            this.txt_QRCode.Name = "txt_QRCode";
            this.txt_QRCode.PreventEnterBeep = true;
            this.txt_QRCode.Size = new System.Drawing.Size(168, 21);
            this.txt_QRCode.TabIndex = 72;
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(12, 30);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(74, 18);
            this.labelX6.TabIndex = 73;
            this.labelX6.Text = "二维码/条码";
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btni_Query,
            this.btni_Delete,
            this.btni_DataOut});
            this.bar1.Location = new System.Drawing.Point(0, 151);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1227, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 4;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btni_Query
            // 
            this.btni_Query.FontBold = true;
            this.btni_Query.Name = "btni_Query";
            this.btni_Query.Text = "查询";
            this.btni_Query.Click += new System.EventHandler(this.btni_Query_Click);
            // 
            // btni_Delete
            // 
            this.btni_Delete.FontBold = true;
            this.btni_Delete.Name = "btni_Delete";
            this.btni_Delete.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btni_DeleteAll,
            this.btni_DeleteSelected,
            this.btni_Condition});
            this.btni_Delete.Text = "删除";
            this.btni_Delete.Click += new System.EventHandler(this.btni_Delete_Click);
            // 
            // btni_DeleteAll
            // 
            this.btni_DeleteAll.FontBold = true;
            this.btni_DeleteAll.Name = "btni_DeleteAll";
            this.btni_DeleteAll.Text = "删除此界面所有数据";
            this.btni_DeleteAll.Click += new System.EventHandler(this.btni_DeleteAll_Click);
            // 
            // btni_DeleteSelected
            // 
            this.btni_DeleteSelected.FontBold = true;
            this.btni_DeleteSelected.Name = "btni_DeleteSelected";
            this.btni_DeleteSelected.Text = "删除选中的一条数据";
            this.btni_DeleteSelected.Click += new System.EventHandler(this.btni_DeleteSelected_Click);
            // 
            // btni_Condition
            // 
            this.btni_Condition.FontBold = true;
            this.btni_Condition.Name = "btni_Condition";
            this.btni_Condition.Text = "按条件删除数据";
            this.btni_Condition.Click += new System.EventHandler(this.btni_Condition_Click);
            // 
            // btni_DataOut
            // 
            this.btni_DataOut.FontBold = true;
            this.btni_DataOut.Name = "btni_DataOut";
            this.btni_DataOut.Text = "导出数据";
            this.btni_DataOut.Click += new System.EventHandler(this.btni_DataOut_Click);
            // 
            // dataGridViewX1
            // 
            this.dataGridViewX1.AllowUserToAddRows = false;
            this.dataGridViewX1.AllowUserToDeleteRows = false;
            this.dataGridViewX1.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridViewX1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewX1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewX1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dataGridViewX1.Location = new System.Drawing.Point(0, 178);
            this.dataGridViewX1.Name = "dataGridViewX1";
            this.dataGridViewX1.ReadOnly = true;
            this.dataGridViewX1.RowTemplate.Height = 23;
            this.dataGridViewX1.Size = new System.Drawing.Size(1227, 478);
            this.dataGridViewX1.TabIndex = 5;
            this.dataGridViewX1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewX1_RowPostPaint);
            this.dataGridViewX1.SelectionChanged += new System.EventHandler(this.dataGridViewX1_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除此界面所以数据ToolStripMenuItem,
            this.删除选中的数据ToolStripMenuItem,
            this.按条件删除数据ToolStripMenuItem,
            this.导出数据ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 92);
            // 
            // 删除此界面所以数据ToolStripMenuItem
            // 
            this.删除此界面所以数据ToolStripMenuItem.Name = "删除此界面所以数据ToolStripMenuItem";
            this.删除此界面所以数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.删除此界面所以数据ToolStripMenuItem.Text = "删除此界面所有数据";
            this.删除此界面所以数据ToolStripMenuItem.Click += new System.EventHandler(this.btni_DeleteAll_Click);
            // 
            // 删除选中的数据ToolStripMenuItem
            // 
            this.删除选中的数据ToolStripMenuItem.Name = "删除选中的数据ToolStripMenuItem";
            this.删除选中的数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.删除选中的数据ToolStripMenuItem.Text = "删除选中的一条数据";
            this.删除选中的数据ToolStripMenuItem.Click += new System.EventHandler(this.btni_DeleteSelected_Click);
            // 
            // 按条件删除数据ToolStripMenuItem
            // 
            this.按条件删除数据ToolStripMenuItem.Name = "按条件删除数据ToolStripMenuItem";
            this.按条件删除数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.按条件删除数据ToolStripMenuItem.Text = "按条件删除数据";
            this.按条件删除数据ToolStripMenuItem.Click += new System.EventHandler(this.btni_Condition_Click);
            // 
            // 导出数据ToolStripMenuItem
            // 
            this.导出数据ToolStripMenuItem.Name = "导出数据ToolStripMenuItem";
            this.导出数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.导出数据ToolStripMenuItem.Text = "导出数据";
            this.导出数据ToolStripMenuItem.Click += new System.EventHandler(this.btni_DataOut_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.FontBold = true;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "查询";
            // 
            // dataManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 656);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.dataGridViewX1);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Name = "dataManagement";
            this.Load += new System.EventHandler(this.dataManagement_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Dti_Begin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dti_End)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewX1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.Controls.DataGridViewX dataGridViewX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbe_ProductionClass;
        private DevComponents.DotNetBar.LabelX labelX60;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_QRCode;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput Dti_Begin;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput Dti_End;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private DevComponents.DotNetBar.ButtonItem btni_Query;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_FinishedProduct;
        private System.Windows.Forms.RadioButton RB_ElectricCore;
        private System.Windows.Forms.RadioButton RB_All;
        private DevComponents.DotNetBar.ButtonItem btni_Delete;
        private DevComponents.DotNetBar.ButtonItem btni_DeleteAll;
        private DevComponents.DotNetBar.ButtonItem btni_DeleteSelected;
        private DevComponents.DotNetBar.ButtonItem btni_Condition;
        private System.Windows.Forms.ToolStripMenuItem 删除此界面所以数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除选中的数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按条件删除数据ToolStripMenuItem;
        private DevComponents.DotNetBar.ButtonItem btni_DataOut;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private System.Windows.Forms.ToolStripMenuItem 导出数据ToolStripMenuItem;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
    }
}