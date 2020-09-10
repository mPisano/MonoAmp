namespace MPRSG6Z_Test
{
    partial class AmpTest
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
            this.label3 = new System.Windows.Forms.Label();
            this.bOpen = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.nudPollms = new System.Windows.Forms.NumericUpDown();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nudAmps = new System.Windows.Forms.NumericUpDown();
            this.bStatus = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudPollms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbComSelect
            // 
            this.cmbComSelect.FormattingEnabled = true;
            this.cmbComSelect.Location = new System.Drawing.Point(74, 8);
            this.cmbComSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbComSelect.Name = "cmbComSelect";
            this.cmbComSelect.Size = new System.Drawing.Size(69, 24);
            this.cmbComSelect.Sorted = true;
            this.cmbComSelect.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "COM Port";
            // 
            // bOpen
            // 
            this.bOpen.Location = new System.Drawing.Point(281, 5);
            this.bOpen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(59, 29);
            this.bOpen.TabIndex = 12;
            this.bOpen.Text = "Open port";
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(344, 5);
            this.bClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(59, 29);
            this.bClose.TabIndex = 25;
            this.bClose.Text = "Close ";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // nudPollms
            // 
            this.nudPollms.Increment = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.nudPollms.Location = new System.Drawing.Point(470, 9);
            this.nudPollms.Margin = new System.Windows.Forms.Padding(2);
            this.nudPollms.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudPollms.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudPollms.Name = "nudPollms";
            this.nudPollms.Size = new System.Drawing.Size(70, 22);
            this.nudPollms.TabIndex = 29;
            this.nudPollms.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPollms.ValueChanged += new System.EventHandler(this.nudPollms_ValueChanged);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(8, 37);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(629, 196);
            this.listBox1.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(411, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 32;
            this.label1.Text = "Poll ms";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(145, 10);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 17);
            this.label9.TabIndex = 34;
            this.label9.Text = "Amps (1-3)";
            // 
            // nudAmps
            // 
            this.nudAmps.Location = new System.Drawing.Point(219, 8);
            this.nudAmps.Margin = new System.Windows.Forms.Padding(2);
            this.nudAmps.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudAmps.Name = "nudAmps";
            this.nudAmps.Size = new System.Drawing.Size(56, 22);
            this.nudAmps.TabIndex = 33;
            this.nudAmps.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bStatus
            // 
            this.bStatus.Location = new System.Drawing.Point(546, 5);
            this.bStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bStatus.Name = "bStatus";
            this.bStatus.Size = new System.Drawing.Size(92, 29);
            this.bStatus.TabIndex = 35;
            this.bStatus.Text = "Get Status";
            this.bStatus.UseVisualStyleBackColor = true;
            this.bStatus.Click += new System.EventHandler(this.bStatus_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 244);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(629, 286);
            this.dataGridView1.TabIndex = 39;
            // 
            // AmpTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(651, 545);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.bStatus);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nudAmps);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.nudPollms);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bOpen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbComSelect);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(243, 231);
            this.Name = "AmpTest";
            this.Text = "Amp Test";
            this.Load += new System.EventHandler(this.AmpTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudPollms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbComSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.NumericUpDown nudPollms;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudAmps;
        private System.Windows.Forms.Button bStatus;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

