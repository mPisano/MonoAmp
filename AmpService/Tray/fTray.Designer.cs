namespace AmpService
{
    partial class fTray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fTray));
            this.bStart = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bConfig = new System.Windows.Forms.Button();
            this.bInstall = new System.Windows.Forms.Button();
            this.bUninstall = new System.Windows.Forms.Button();
            this.gb_SvcInstall = new System.Windows.Forms.GroupBox();
            this.gb_StartStop = new System.Windows.Forms.GroupBox();
            this.TmrChkSvc = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.bBrowse = new System.Windows.Forms.Button();
            this.gb_SvcInstall.SuspendLayout();
            this.gb_StartStop.SuspendLayout();
            this.SuspendLayout();
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(6, 18);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(61, 25);
            this.bStart.TabIndex = 0;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bStop
            // 
            this.bStop.Enabled = false;
            this.bStop.Location = new System.Drawing.Point(73, 18);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(61, 25);
            this.bStop.TabIndex = 1;
            this.bStop.Text = "Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // bConfig
            // 
            this.bConfig.Location = new System.Drawing.Point(42, 2);
            this.bConfig.Name = "bConfig";
            this.bConfig.Size = new System.Drawing.Size(61, 25);
            this.bConfig.TabIndex = 4;
            this.bConfig.Text = "Config";
            this.bConfig.UseVisualStyleBackColor = true;
            this.bConfig.Click += new System.EventHandler(this.bConfigEdi_Click);
            // 
            // bInstall
            // 
            this.bInstall.Location = new System.Drawing.Point(7, 18);
            this.bInstall.Name = "bInstall";
            this.bInstall.Size = new System.Drawing.Size(61, 25);
            this.bInstall.TabIndex = 5;
            this.bInstall.Text = "Install";
            this.bInstall.UseVisualStyleBackColor = true;
            this.bInstall.Click += new System.EventHandler(this.bInstall_Click);
            // 
            // bUninstall
            // 
            this.bUninstall.Location = new System.Drawing.Point(74, 18);
            this.bUninstall.Name = "bUninstall";
            this.bUninstall.Size = new System.Drawing.Size(61, 25);
            this.bUninstall.TabIndex = 6;
            this.bUninstall.Text = "Uninstall";
            this.bUninstall.UseVisualStyleBackColor = true;
            this.bUninstall.Click += new System.EventHandler(this.bUninstall_Click);
            // 
            // gb_SvcInstall
            // 
            this.gb_SvcInstall.Controls.Add(this.bInstall);
            this.gb_SvcInstall.Controls.Add(this.bUninstall);
            this.gb_SvcInstall.Location = new System.Drawing.Point(35, 53);
            this.gb_SvcInstall.Name = "gb_SvcInstall";
            this.gb_SvcInstall.Size = new System.Drawing.Size(141, 49);
            this.gb_SvcInstall.TabIndex = 7;
            this.gb_SvcInstall.TabStop = false;
            this.gb_SvcInstall.Text = "Service";
            // 
            // gb_StartStop
            // 
            this.gb_StartStop.Controls.Add(this.bStart);
            this.gb_StartStop.Controls.Add(this.bStop);
            this.gb_StartStop.Location = new System.Drawing.Point(179, 53);
            this.gb_StartStop.Name = "gb_StartStop";
            this.gb_StartStop.Size = new System.Drawing.Size(144, 49);
            this.gb_StartStop.TabIndex = 8;
            this.gb_StartStop.TabStop = false;
            // 
            // TmrChkSvc
            // 
            this.TmrChkSvc.Interval = 1000;
            this.TmrChkSvc.Tick += new System.EventHandler(this.TmrChkSvc_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "State";
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(35, 30);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Size = new System.Drawing.Size(282, 20);
            this.tbStatus.TabIndex = 11;
            // 
            // bBrowse
            // 
            this.bBrowse.Location = new System.Drawing.Point(252, 2);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(61, 25);
            this.bBrowse.TabIndex = 13;
            this.bBrowse.Text = "App";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // fTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(328, 106);
            this.Controls.Add(this.bBrowse);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.gb_StartStop);
            this.Controls.Add(this.gb_SvcInstall);
            this.Controls.Add(this.bConfig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "fTray";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fTray_FormClosing);
            this.Load += new System.EventHandler(this.fTray_Load);
            this.Resize += new System.EventHandler(this.fTray_Resize);
            this.gb_SvcInstall.ResumeLayout(false);
            this.gb_StartStop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bConfig;
        private System.Windows.Forms.Button bInstall;
        private System.Windows.Forms.Button bUninstall;
        private System.Windows.Forms.GroupBox gb_SvcInstall;
        private System.Windows.Forms.GroupBox gb_StartStop;
        private System.Windows.Forms.Timer TmrChkSvc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.Button bBrowse;
    }
}