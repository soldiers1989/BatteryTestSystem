namespace 电池测试系统.TestSystems
{
    partial class BatteryTypeManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatteryTypeManagement));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.ck_FinishedProduct = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ck_ElectricCore = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txt_BatteryType = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txt_TimeInterval = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txt_VoltageDifference = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btni_new = new DevComponents.DotNetBar.ButtonItem();
            this.btni_save = new DevComponents.DotNetBar.ButtonItem();
            this.btni_cancle = new DevComponents.DotNetBar.ButtonItem();
            this.btni_modify = new DevComponents.DotNetBar.ButtonItem();
            this.btni_delete = new DevComponents.DotNetBar.ButtonItem();
            this.btni_lookup = new DevComponents.DotNetBar.ButtonItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.ck_FinishedProduct);
            this.panelEx1.Controls.Add(this.ck_ElectricCore);
            this.panelEx1.Controls.Add(this.txt_BatteryType);
            this.panelEx1.Controls.Add(this.txt_TimeInterval);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.txt_VoltageDifference);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(173, 27);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(374, 356);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 8;
            // 
            // ck_FinishedProduct
            // 
            this.ck_FinishedProduct.AutoSize = true;
            // 
            // 
            // 
            this.ck_FinishedProduct.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ck_FinishedProduct.Location = new System.Drawing.Point(204, 170);
            this.ck_FinishedProduct.Name = "ck_FinishedProduct";
            this.ck_FinishedProduct.Size = new System.Drawing.Size(51, 18);
            this.ck_FinishedProduct.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ck_FinishedProduct.TabIndex = 10;
            this.ck_FinishedProduct.Text = "成品";
            this.ck_FinishedProduct.CheckedChanged += new System.EventHandler(this.ck_FinishedProduct_CheckedChanged);
            // 
            // ck_ElectricCore
            // 
            this.ck_ElectricCore.AutoSize = true;
            // 
            // 
            // 
            this.ck_ElectricCore.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ck_ElectricCore.Checked = true;
            this.ck_ElectricCore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_ElectricCore.CheckValue = "Y";
            this.ck_ElectricCore.Location = new System.Drawing.Point(137, 170);
            this.ck_ElectricCore.Name = "ck_ElectricCore";
            this.ck_ElectricCore.Size = new System.Drawing.Size(51, 18);
            this.ck_ElectricCore.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ck_ElectricCore.TabIndex = 9;
            this.ck_ElectricCore.Text = "电芯";
            this.ck_ElectricCore.CheckedChanged += new System.EventHandler(this.ck_ElectricCore_CheckedChanged);
            // 
            // txt_BatteryType
            // 
            // 
            // 
            // 
            this.txt_BatteryType.Border.Class = "TextBoxBorder";
            this.txt_BatteryType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_BatteryType.DisabledBackColor = System.Drawing.Color.White;
            this.txt_BatteryType.Location = new System.Drawing.Point(113, 46);
            this.txt_BatteryType.Name = "txt_BatteryType";
            this.txt_BatteryType.PreventEnterBeep = true;
            this.txt_BatteryType.Size = new System.Drawing.Size(186, 21);
            this.txt_BatteryType.TabIndex = 8;
            // 
            // txt_TimeInterval
            // 
            // 
            // 
            // 
            this.txt_TimeInterval.Border.Class = "TextBoxBorder";
            this.txt_TimeInterval.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_TimeInterval.Location = new System.Drawing.Point(137, 130);
            this.txt_TimeInterval.Name = "txt_TimeInterval";
            this.txt_TimeInterval.PreventEnterBeep = true;
            this.txt_TimeInterval.Size = new System.Drawing.Size(127, 21);
            this.txt_TimeInterval.TabIndex = 7;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(13, 133);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(118, 18);
            this.labelX2.TabIndex = 6;
            this.labelX2.Text = "最小时间间距(小时)";
            // 
            // txt_VoltageDifference
            // 
            // 
            // 
            // 
            this.txt_VoltageDifference.Border.Class = "TextBoxBorder";
            this.txt_VoltageDifference.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_VoltageDifference.Location = new System.Drawing.Point(137, 92);
            this.txt_VoltageDifference.Name = "txt_VoltageDifference";
            this.txt_VoltageDifference.PreventEnterBeep = true;
            this.txt_VoltageDifference.Size = new System.Drawing.Size(127, 21);
            this.txt_VoltageDifference.TabIndex = 3;
            this.txt_VoltageDifference.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_BarcodeEncoding_KeyPress);
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(51, 49);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(56, 18);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "电池类型";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(26, 95);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(105, 18);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "电池压差(每小时)";
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btni_new,
            this.btni_save,
            this.btni_cancle,
            this.btni_modify,
            this.btni_delete,
            this.btni_lookup});
            this.bar1.Location = new System.Drawing.Point(173, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(374, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 7;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btni_new
            // 
            this.btni_new.Name = "btni_new";
            this.btni_new.Text = "新建";
            this.btni_new.Click += new System.EventHandler(this.btni_new_Click);
            // 
            // btni_save
            // 
            this.btni_save.Name = "btni_save";
            this.btni_save.Text = "保存";
            this.btni_save.Click += new System.EventHandler(this.btni_save_Click);
            // 
            // btni_cancle
            // 
            this.btni_cancle.Name = "btni_cancle";
            this.btni_cancle.Text = "取消";
            this.btni_cancle.Click += new System.EventHandler(this.btni_cancle_Click);
            // 
            // btni_modify
            // 
            this.btni_modify.Name = "btni_modify";
            this.btni_modify.Text = "修改";
            this.btni_modify.Click += new System.EventHandler(this.btni_modify_Click);
            // 
            // btni_delete
            // 
            this.btni_delete.Name = "btni_delete";
            this.btni_delete.Text = "删除";
            this.btni_delete.Click += new System.EventHandler(this.btni_delete_Click);
            // 
            // btni_lookup
            // 
            this.btni_lookup.Name = "btni_lookup";
            this.btni_lookup.Text = "查找";
            this.btni_lookup.Click += new System.EventHandler(this.btni_lookup_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(173, 383);
            this.treeView1.TabIndex = 6;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // BatteryTypeManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 383);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.treeView1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BatteryTypeManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "电池类型管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatteryTypeManagement_FormClosing);
            this.Load += new System.EventHandler(this.BatteryTypeManagement_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_VoltageDifference;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btni_new;
        private DevComponents.DotNetBar.ButtonItem btni_save;
        private DevComponents.DotNetBar.ButtonItem btni_cancle;
        private DevComponents.DotNetBar.ButtonItem btni_modify;
        private DevComponents.DotNetBar.ButtonItem btni_delete;
        private DevComponents.DotNetBar.ButtonItem btni_lookup;
        private System.Windows.Forms.TreeView treeView1;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_TimeInterval;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_BatteryType;
        private DevComponents.DotNetBar.Controls.CheckBoxX ck_ElectricCore;
        private DevComponents.DotNetBar.Controls.CheckBoxX ck_FinishedProduct;
    }
}