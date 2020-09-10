namespace AmpService
{
    partial class fCloseForm
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
            this.bBack = new System.Windows.Forms.Button();
            this.bNo = new System.Windows.Forms.Button();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.bYes = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bBack
            // 
            this.bBack.BackColor = System.Drawing.SystemColors.Control;
            this.bBack.Cursor = System.Windows.Forms.Cursors.Default;
            this.bBack.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bBack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bBack.Location = new System.Drawing.Point(12, 44);
            this.bBack.Name = "bBack";
            this.bBack.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bBack.Size = new System.Drawing.Size(140, 32);
            this.bBack.TabIndex = 7;
            this.bBack.Text = "Run in Background";
            this.bBack.UseVisualStyleBackColor = false;
            this.bBack.Click += new System.EventHandler(this.bBack_Click);
            // 
            // bNo
            // 
            this.bNo.BackColor = System.Drawing.SystemColors.Control;
            this.bNo.Cursor = System.Windows.Forms.Cursors.Default;
            this.bNo.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bNo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bNo.Location = new System.Drawing.Point(297, 44);
            this.bNo.Name = "bNo";
            this.bNo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bNo.Size = new System.Drawing.Size(88, 32);
            this.bNo.TabIndex = 5;
            this.bNo.Text = "No";
            this.bNo.UseVisualStyleBackColor = false;
            this.bNo.Click += new System.EventHandler(this.bNo_Click);
            // 
            // Timer1
            // 
            this.Timer1.Interval = 1;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // bYes
            // 
            this.bYes.BackColor = System.Drawing.SystemColors.Control;
            this.bYes.Cursor = System.Windows.Forms.Cursors.Default;
            this.bYes.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bYes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bYes.Location = new System.Drawing.Point(207, 44);
            this.bYes.Name = "bYes";
            this.bYes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bYes.Size = new System.Drawing.Size(84, 32);
            this.bYes.TabIndex = 4;
            this.bYes.Text = "Yes";
            this.bYes.UseVisualStyleBackColor = false;
            this.bYes.Click += new System.EventHandler(this.bYes_Click);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.SystemColors.Control;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(373, 24);
            this.Label1.TabIndex = 6;
            this.Label1.Text = "Exiting will Stop Amp Communications. Do you really want to Exit?";
            // 
            // fCloseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(402, 85);
            this.Controls.Add(this.bBack);
            this.Controls.Add(this.bNo);
            this.Controls.Add(this.bYes);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fCloseForm";
            this.ShowIcon = false;
            this.Text = "WARNING";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button bBack;
        public System.Windows.Forms.Button bNo;
        public System.Windows.Forms.Timer Timer1;
        public System.Windows.Forms.Button bYes;
        public System.Windows.Forms.ToolTip ToolTip1;
        public System.Windows.Forms.Label Label1;
    }
}