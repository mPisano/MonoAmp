namespace Track
{
    partial class Track
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPanVal = new System.Windows.Forms.Label();
            this.lblPan = new System.Windows.Forms.Label();
            this.panelMute = new System.Windows.Forms.Panel();
            this.tbKpName = new System.Windows.Forms.TextBox();
            this.bcMute = new ButtonControl.ButtonControl();
            this.lblMuteText = new System.Windows.Forms.Label();
            this.lblTrackNum2 = new System.Windows.Forms.Label();
            this.volMain = new System.Windows.Forms.TrackBar();
            this.lblVolMain = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.volBass = new System.Windows.Forms.TrackBar();
            this.volTreb = new System.Windows.Forms.TrackBar();
            this.panelPanSolo = new System.Windows.Forms.Panel();
            this.volPAN = new KnobControl.KnobControl();
            this.lbPanVal = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelTrack = new System.Windows.Forms.Panel();
            this.LED = new LxControl.LxLedControl();
            this.bsChan = new ButtonSource.ButtonSource();
            this.bcPower = new ButtonControl.ButtonControl();
            this.panelMute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volBass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volTreb)).BeginInit();
            this.panelPanSolo.SuspendLayout();
            this.panelTrack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LED)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPanVal
            // 
            this.lblPanVal.AutoSize = true;
            this.lblPanVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPanVal.Location = new System.Drawing.Point(55, 37);
            this.lblPanVal.Name = "lblPanVal";
            this.lblPanVal.Size = new System.Drawing.Size(10, 12);
            this.lblPanVal.TabIndex = 25;
            this.lblPanVal.Text = "0";
            // 
            // lblPan
            // 
            this.lblPan.AutoSize = true;
            this.lblPan.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPan.Location = new System.Drawing.Point(52, 17);
            this.lblPan.Name = "lblPan";
            this.lblPan.Size = new System.Drawing.Size(21, 12);
            this.lblPan.TabIndex = 24;
            this.lblPan.Text = "Pan";
            // 
            // panelMute
            // 
            this.panelMute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(208)))), ((int)(((byte)(227)))));
            this.panelMute.Controls.Add(this.tbKpName);
            this.panelMute.Controls.Add(this.bcMute);
            this.panelMute.Controls.Add(this.lblMuteText);
            this.panelMute.Controls.Add(this.lblTrackNum2);
            this.panelMute.Location = new System.Drawing.Point(0, 319);
            this.panelMute.Name = "panelMute";
            this.panelMute.Size = new System.Drawing.Size(64, 66);
            this.panelMute.TabIndex = 10;
            this.panelMute.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMute_Paint);
            // 
            // tbKpName
            // 
            this.tbKpName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(208)))), ((int)(((byte)(227)))));
            this.tbKpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbKpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbKpName.ForeColor = System.Drawing.Color.Black;
            this.tbKpName.Location = new System.Drawing.Point(-1, 26);
            this.tbKpName.Name = "tbKpName";
            this.tbKpName.Size = new System.Drawing.Size(66, 20);
            this.tbKpName.TabIndex = 28;
            this.tbKpName.Text = "Keypad X";
            this.tbKpName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bcMute
            // 
            this.bcMute.BackColor = System.Drawing.Color.Red;
            this.bcMute.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bcMute.Location = new System.Drawing.Point(41, 49);
            this.bcMute.Name = "bcMute";
            this.bcMute.OffColor = System.Drawing.Color.DimGray;
            this.bcMute.OnColor = System.Drawing.Color.Red;
            this.bcMute.Size = new System.Drawing.Size(14, 12);
            this.bcMute.TabIndex = 27;
            this.bcMute.Value = 1;
            this.bcMute.Warn = false;
            // 
            // lblMuteText
            // 
            this.lblMuteText.AutoSize = true;
            this.lblMuteText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMuteText.Location = new System.Drawing.Point(7, 48);
            this.lblMuteText.Name = "lblMuteText";
            this.lblMuteText.Size = new System.Drawing.Size(27, 12);
            this.lblMuteText.TabIndex = 10;
            this.lblMuteText.Text = "Mute";
            // 
            // lblTrackNum2
            // 
            this.lblTrackNum2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackNum2.Location = new System.Drawing.Point(7, 1);
            this.lblTrackNum2.Name = "lblTrackNum2";
            this.lblTrackNum2.Size = new System.Drawing.Size(51, 24);
            this.lblTrackNum2.TabIndex = 9;
            this.lblTrackNum2.Text = "X";
            this.lblTrackNum2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // volMain
            // 
            this.volMain.Location = new System.Drawing.Point(12, 385);
            this.volMain.Maximum = 38;
            this.volMain.Name = "volMain";
            this.volMain.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.volMain.Size = new System.Drawing.Size(45, 152);
            this.volMain.TabIndex = 0;
            this.volMain.TickFrequency = 3;
            this.volMain.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.volMain.Value = 5;
            // 
            // lblVolMain
            // 
            this.lblVolMain.AutoSize = true;
            this.lblVolMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVolMain.ForeColor = System.Drawing.SystemColors.Control;
            this.lblVolMain.Location = new System.Drawing.Point(3, 523);
            this.lblVolMain.Name = "lblVolMain";
            this.lblVolMain.Size = new System.Drawing.Size(10, 12);
            this.lblVolMain.TabIndex = 29;
            this.lblVolMain.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 12);
            this.label1.TabIndex = 44;
            this.label1.Text = "Bass";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 12);
            this.label2.TabIndex = 45;
            this.label2.Text = "Treble";
            // 
            // volBass
            // 
            this.volBass.LargeChange = 3;
            this.volBass.Location = new System.Drawing.Point(-1, 155);
            this.volBass.Maximum = 14;
            this.volBass.Name = "volBass";
            this.volBass.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.volBass.Size = new System.Drawing.Size(45, 94);
            this.volBass.TabIndex = 43;
            this.volBass.TickFrequency = 5;
            this.volBass.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // volTreb
            // 
            this.volTreb.LargeChange = 3;
            this.volTreb.Location = new System.Drawing.Point(31, 155);
            this.volTreb.Maximum = 14;
            this.volTreb.Name = "volTreb";
            this.volTreb.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.volTreb.Size = new System.Drawing.Size(45, 94);
            this.volTreb.TabIndex = 46;
            this.volTreb.TickFrequency = 5;
            // 
            // panelPanSolo
            // 
            this.panelPanSolo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPanSolo.Controls.Add(this.volPAN);
            this.panelPanSolo.Controls.Add(this.lbPanVal);
            this.panelPanSolo.Controls.Add(this.label4);
            this.panelPanSolo.Location = new System.Drawing.Point(-1, 249);
            this.panelPanSolo.Name = "panelPanSolo";
            this.panelPanSolo.Size = new System.Drawing.Size(64, 64);
            this.panelPanSolo.TabIndex = 47;
            // 
            // volPAN
            // 
            this.volPAN.ImeMode = System.Windows.Forms.ImeMode.On;
            this.volPAN.LargeChange = 2;
            this.volPAN.Location = new System.Drawing.Point(8, 19);
            this.volPAN.Maximum = 20;
            this.volPAN.Minimum = 0;
            this.volPAN.Name = "volPAN";
            this.volPAN.ShowLargeScale = false;
            this.volPAN.ShowSmallScale = true;
            this.volPAN.Size = new System.Drawing.Size(44, 45);
            this.volPAN.SmallChange = 1;
            this.volPAN.TabIndex = 26;
            this.volPAN.TabStop = false;
            this.volPAN.Value = 10;
            // 
            // lbPanVal
            // 
            this.lbPanVal.AutoSize = true;
            this.lbPanVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPanVal.Location = new System.Drawing.Point(44, 3);
            this.lbPanVal.Name = "lbPanVal";
            this.lbPanVal.Size = new System.Drawing.Size(10, 12);
            this.lbPanVal.TabIndex = 25;
            this.lbPanVal.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "Pan";
            // 
            // panelTrack
            // 
            this.panelTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(128)))), ((int)(((byte)(134)))));
            this.panelTrack.Controls.Add(this.LED);
            this.panelTrack.Controls.Add(this.panelPanSolo);
            this.panelTrack.Controls.Add(this.bsChan);
            this.panelTrack.Controls.Add(this.bcPower);
            this.panelTrack.Controls.Add(this.volTreb);
            this.panelTrack.Controls.Add(this.volBass);
            this.panelTrack.Controls.Add(this.label2);
            this.panelTrack.Controls.Add(this.label1);
            this.panelTrack.Controls.Add(this.lblVolMain);
            this.panelTrack.Controls.Add(this.volMain);
            this.panelTrack.Controls.Add(this.panelMute);
            this.panelTrack.ForeColor = System.Drawing.Color.Black;
            this.panelTrack.Location = new System.Drawing.Point(0, 0);
            this.panelTrack.Name = "panelTrack";
            this.panelTrack.Size = new System.Drawing.Size(64, 539);
            this.panelTrack.TabIndex = 0;
            this.panelTrack.Paint += new System.Windows.Forms.PaintEventHandler(this.panelTrack_Paint);
            // 
            // LED
            // 
            this.LED.BackColor = System.Drawing.Color.Transparent;
            this.LED.BackColor_1 = System.Drawing.Color.Black;
            this.LED.BackColor_2 = System.Drawing.Color.DimGray;
            this.LED.BevelRate = 0.25F;
            this.LED.Enabled = false;
            this.LED.FadedColor = System.Drawing.Color.DimGray;
            this.LED.FocusedBorderColor = System.Drawing.Color.Crimson;
            this.LED.ForeColor = System.Drawing.Color.Cyan;
            this.LED.HighlightOpaque = ((byte)(50));
            this.LED.Location = new System.Drawing.Point(0, 31);
            this.LED.Name = "LED";
            this.LED.SegmentIntervalRatio = 60;
            this.LED.SegmentWidthRatio = 30;
            this.LED.Size = new System.Drawing.Size(62, 46);
            this.LED.TabIndex = 48;
            this.LED.Text = "";
            this.LED.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.LED.TotalCharCount = 2;
            // 
            // bsChan
            // 
            this.bsChan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(128)))), ((int)(((byte)(134)))));
            this.bsChan.Location = new System.Drawing.Point(-1, 77);
            this.bsChan.Name = "bsChan";
            this.bsChan.OnColor = System.Drawing.Color.Lime;
            this.bsChan.Size = new System.Drawing.Size(64, 67);
            this.bsChan.TabIndex = 1;
            this.bsChan.Value = 1;
            // 
            // bcPower
            // 
            this.bcPower.BackColor = System.Drawing.Color.Green;
            this.bcPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bcPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F);
            this.bcPower.Location = new System.Drawing.Point(1, 3);
            this.bcPower.Name = "bcPower";
            this.bcPower.OffColor = System.Drawing.Color.Gray;
            this.bcPower.OnColor = System.Drawing.Color.Green;
            this.bcPower.Size = new System.Drawing.Size(60, 22);
            this.bcPower.TabIndex = 28;
            this.bcPower.Text = "Power";
            this.bcPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bcPower.Value = 1;
            this.bcPower.Warn = false;
            // 
            // Track
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panelTrack);
            this.Name = "Track";
            this.Size = new System.Drawing.Size(64, 538);
            this.panelMute.ResumeLayout(false);
            this.panelMute.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volBass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volTreb)).EndInit();
            this.panelPanSolo.ResumeLayout(false);
            this.panelPanSolo.PerformLayout();
            this.panelTrack.ResumeLayout(false);
            this.panelTrack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LED)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPan;
        private System.Windows.Forms.Label lblPanVal;
        private ButtonSource.ButtonSource bsChan;
        private ButtonControl.ButtonControl bcPower;
        private LxControl.LxLedControl LED;
        private System.Windows.Forms.Panel panelMute;
        private ButtonControl.ButtonControl bcMute;
        private System.Windows.Forms.Label lblMuteText;
        public System.Windows.Forms.Label lblTrackNum2;
        private System.Windows.Forms.TrackBar volMain;
        private System.Windows.Forms.Label lblVolMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar volBass;
        private System.Windows.Forms.TrackBar volTreb;
        private System.Windows.Forms.Panel panelPanSolo;
        private KnobControl.KnobControl volPAN;
        private System.Windows.Forms.Label lbPanVal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelTrack;
        private System.Windows.Forms.TextBox tbKpName;
    }
}
