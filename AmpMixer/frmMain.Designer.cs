namespace MPR_Mixer
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblMaOff = new System.Windows.Forms.Label();
            this.LblMaOn = new System.Windows.Forms.Label();
            this.panelMute = new System.Windows.Forms.Panel();
            this.lbMuOn = new System.Windows.Forms.Label();
            this.lbMuOFF = new System.Windows.Forms.Label();
            this.lblTrackNum2 = new System.Windows.Forms.Label();
            this.panelTrack = new System.Windows.Forms.Panel();
            this.buttonSource1 = new ButtonSource.ButtonSource();
            this.bBrowse = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelMute.SuspendLayout();
            this.panelTrack.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.LblMaOff);
            this.panel1.Controls.Add(this.LblMaOn);
            this.panel1.Controls.Add(this.buttonSource1);
            this.panel1.Controls.Add(this.panelMute);
            this.panel1.Controls.Add(this.panelTrack);
            this.panel1.Location = new System.Drawing.Point(11, 11);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(64, 538);
            this.panel1.TabIndex = 28;
            // 
            // LblMaOff
            // 
            this.LblMaOff.BackColor = System.Drawing.Color.Firebrick;
            this.LblMaOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMaOff.Location = new System.Drawing.Point(32, 0);
            this.LblMaOff.Name = "LblMaOff";
            this.LblMaOff.Size = new System.Drawing.Size(30, 74);
            this.LblMaOff.TabIndex = 33;
            this.LblMaOff.Text = "Off";
            this.LblMaOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblMaOff.Click += new System.EventHandler(this.LblMaOff_Click);
            // 
            // LblMaOn
            // 
            this.LblMaOn.BackColor = System.Drawing.Color.Green;
            this.LblMaOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblMaOn.Location = new System.Drawing.Point(0, 0);
            this.LblMaOn.Name = "LblMaOn";
            this.LblMaOn.Size = new System.Drawing.Size(30, 74);
            this.LblMaOn.TabIndex = 34;
            this.LblMaOn.Text = "On";
            this.LblMaOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblMaOn.Click += new System.EventHandler(this.LblMaOn_Click);
            // 
            // panelMute
            // 
            this.panelMute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(208)))), ((int)(((byte)(227)))));
            this.panelMute.Controls.Add(this.lbMuOn);
            this.panelMute.Controls.Add(this.lbMuOFF);
            this.panelMute.Controls.Add(this.lblTrackNum2);
            this.panelMute.Location = new System.Drawing.Point(0, 319);
            this.panelMute.Name = "panelMute";
            this.panelMute.Size = new System.Drawing.Size(64, 66);
            this.panelMute.TabIndex = 30;
            // 
            // lbMuOn
            // 
            this.lbMuOn.BackColor = System.Drawing.Color.Firebrick;
            this.lbMuOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbMuOn.Location = new System.Drawing.Point(37, 46);
            this.lbMuOn.Name = "lbMuOn";
            this.lbMuOn.Size = new System.Drawing.Size(15, 15);
            this.lbMuOn.TabIndex = 32;
            this.lbMuOn.Click += new System.EventHandler(this.lbMuOn_Click);
            // 
            // lbMuOFF
            // 
            this.lbMuOFF.BackColor = System.Drawing.Color.Green;
            this.lbMuOFF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbMuOFF.Location = new System.Drawing.Point(9, 46);
            this.lbMuOFF.Name = "lbMuOFF";
            this.lbMuOFF.Size = new System.Drawing.Size(15, 15);
            this.lbMuOFF.TabIndex = 31;
            this.lbMuOFF.Click += new System.EventHandler(this.lbMuOFF_Click);
            // 
            // lblTrackNum2
            // 
            this.lblTrackNum2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackNum2.Location = new System.Drawing.Point(-1, 2);
            this.lblTrackNum2.Name = "lblTrackNum2";
            this.lblTrackNum2.Size = new System.Drawing.Size(59, 24);
            this.lblTrackNum2.TabIndex = 9;
            this.lblTrackNum2.Text = "All";
            this.lblTrackNum2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panelTrack
            // 
            this.panelTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(128)))), ((int)(((byte)(134)))));
            this.panelTrack.Controls.Add(this.bBrowse);
            this.panelTrack.ForeColor = System.Drawing.Color.Black;
            this.panelTrack.Location = new System.Drawing.Point(-1, -1);
            this.panelTrack.Name = "panelTrack";
            this.panelTrack.Size = new System.Drawing.Size(64, 539);
            this.panelTrack.TabIndex = 36;
            // 
            // buttonSource1
            // 
            this.buttonSource1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(128)))), ((int)(((byte)(134)))));
            this.buttonSource1.Location = new System.Drawing.Point(0, 77);
            this.buttonSource1.Name = "buttonSource1";
            this.buttonSource1.OnColor = System.Drawing.Color.Blue;
            this.buttonSource1.Size = new System.Drawing.Size(64, 67);
            this.buttonSource1.TabIndex = 35;
            this.buttonSource1.Value = 0;
            // 
            // bBrowse
            // 
            this.bBrowse.Location = new System.Drawing.Point(1, 511);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(61, 25);
            this.bBrowse.TabIndex = 29;
            this.bBrowse.Text = "App";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(89, 565);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Monoprice Amp Mixer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panelMute.ResumeLayout(false);
            this.panelTrack.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelMute;
        public System.Windows.Forms.Label lblTrackNum2;
        private System.Windows.Forms.Label LblMaOn;
        private System.Windows.Forms.Label lbMuOn;
        private System.Windows.Forms.Label lbMuOFF;
        private System.Windows.Forms.Label LblMaOff;
        private ButtonSource.ButtonSource buttonSource1;
        private System.Windows.Forms.Panel panelTrack;
        private System.Windows.Forms.Button bBrowse;
 

    }
}

