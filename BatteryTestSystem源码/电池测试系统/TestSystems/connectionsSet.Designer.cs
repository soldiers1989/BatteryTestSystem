namespace 电池测试系统.TestSystems
{
    partial class connectionsSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(connectionsSet));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btn_cancle = new DevComponents.DotNetBar.ButtonX();
            this.btn_save = new DevComponents.DotNetBar.ButtonX();
            this.labelX25 = new DevComponents.DotNetBar.LabelX();
            this.txt_DataSource = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txt_InitialCatalog = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btn_cancle);
            this.panelEx1.Controls.Add(this.btn_save);
            this.panelEx1.Controls.Add(this.labelX25);
            this.panelEx1.Controls.Add(this.txt_DataSource);
            this.panelEx1.Controls.Add(this.labelX8);
            this.panelEx1.Controls.Add(this.txt_InitialCatalog);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(505, 176);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btn_cancle
            // 
            this.btn_cancle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_cancle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_cancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancle.Location = new System.Drawing.Point(318, 119);
            this.btn_cancle.Name = "btn_cancle";
            this.btn_cancle.Size = new System.Drawing.Size(75, 23);
            this.btn_cancle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_cancle.TabIndex = 4;
            this.btn_cancle.Text = "取消";
            this.btn_cancle.Click += new System.EventHandler(this.btn_cancle_Click);
            // 
            // btn_save
            // 
            this.btn_save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_save.Location = new System.Drawing.Point(98, 119);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_save.TabIndex = 3;
            this.btn_save.Text = "保存";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // labelX25
            // 
            this.labelX25.AutoSize = true;
            // 
            // 
            // 
            this.labelX25.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX25.Location = new System.Drawing.Point(122, 34);
            this.labelX25.Name = "labelX25";
            this.labelX25.Size = new System.Drawing.Size(44, 18);
            this.labelX25.TabIndex = 70;
            this.labelX25.Text = "IP地址";
            // 
            // txt_DataSource
            // 
            // 
            // 
            // 
            this.txt_DataSource.Border.Class = "TextBoxBorder";
            this.txt_DataSource.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_DataSource.Location = new System.Drawing.Point(178, 31);
            this.txt_DataSource.Name = "txt_DataSource";
            this.txt_DataSource.PreventEnterBeep = true;
            this.txt_DataSource.Size = new System.Drawing.Size(198, 21);
            this.txt_DataSource.TabIndex = 1;
            this.txt_DataSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_IP_KeyPress);
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(98, 61);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(68, 18);
            this.labelX8.TabIndex = 62;
            this.labelX8.Text = "数据库名称";
            // 
            // txt_InitialCatalog
            // 
            // 
            // 
            // 
            this.txt_InitialCatalog.Border.Class = "TextBoxBorder";
            this.txt_InitialCatalog.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_InitialCatalog.Location = new System.Drawing.Point(178, 58);
            this.txt_InitialCatalog.Name = "txt_InitialCatalog";
            this.txt_InitialCatalog.PreventEnterBeep = true;
            this.txt_InitialCatalog.Size = new System.Drawing.Size(198, 21);
            this.txt_InitialCatalog.TabIndex = 2;
            // 
            // connectionsSet
            // 
            this.AcceptButton = this.btn_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancle;
            this.ClientSize = new System.Drawing.Size(505, 176);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "connectionsSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连接配置";
            this.Load += new System.EventHandler(this.connectionsSet_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX25;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_DataSource;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_InitialCatalog;
        private DevComponents.DotNetBar.ButtonX btn_save;
        private DevComponents.DotNetBar.ButtonX btn_cancle;
    }
}