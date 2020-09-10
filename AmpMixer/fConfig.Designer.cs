namespace Monoprice
{
    partial class fConfig
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
            this.cmbComSelect = new System.Windows.Forms.ComboBox();
            this.bOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nudAmps = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmps)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbComSelect
            // 
            this.cmbComSelect.FormattingEnabled = true;
            this.cmbComSelect.Location = new System.Drawing.Point(72, 26);
            this.cmbComSelect.Name = "cmbComSelect";
            this.cmbComSelect.Size = new System.Drawing.Size(71, 21);
            this.cmbComSelect.Sorted = true;
            this.cmbComSelect.TabIndex = 28;
            // 
            // bOpen
            // 
            this.bOpen.Location = new System.Drawing.Point(213, 63);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(47, 23);
            this.bOpen.TabIndex = 29;
            this.bOpen.Text = "Open port";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "COM Port";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(157, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "Amps (1-3)";
            // 
            // nudAmps
            // 
            this.nudAmps.Location = new System.Drawing.Point(216, 26);
            this.nudAmps.Margin = new System.Windows.Forms.Padding(2);
            this.nudAmps.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudAmps.Name = "nudAmps";
            this.nudAmps.Size = new System.Drawing.Size(45, 20);
            this.nudAmps.TabIndex = 35;
            this.nudAmps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // fConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 105);
            this.ControlBox = false;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nudAmps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbComSelect);
            this.Controls.Add(this.bOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Amp Configuration";
            this.Load += new System.EventHandler(this.fConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudAmps)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbComSelect;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudAmps;
    }
}