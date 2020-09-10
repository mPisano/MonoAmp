using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using ButtonControl;

namespace ButtonSource
{
    public class ButtonSource : System.Windows.Forms.UserControl, INotifyPropertyChanged
    {
        private ButtonControl.ButtonControl lblIn6;
        private ButtonControl.ButtonControl lblIn2;
        private ButtonControl.ButtonControl lblIn5;
        private ButtonControl.ButtonControl lblIn4;
        private ButtonControl.ButtonControl lblIn3;
        private ButtonControl.ButtonControl lblIn1;
        private System.Windows.Forms.TextBox lblIn;


        private void InitializeComponent()
        {
            this.lblIn = new System.Windows.Forms.TextBox();
            this.lblIn6 = new ButtonControl.ButtonControl();
            this.lblIn2 = new ButtonControl.ButtonControl();
            this.lblIn5 = new ButtonControl.ButtonControl();
            this.lblIn4 = new ButtonControl.ButtonControl();
            this.lblIn3 = new ButtonControl.ButtonControl();
            this.lblIn1 = new ButtonControl.ButtonControl();
            this.SuspendLayout();
            // 
            // lblIn
            // 
            this.lblIn.BackColor = System.Drawing.Color.Silver;
            this.lblIn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIn.Enabled = false;
            this.lblIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIn.ForeColor = System.Drawing.Color.Black;
            this.lblIn.Location = new System.Drawing.Point(0, 3);
            this.lblIn.Name = "lblIn";
            this.lblIn.Size = new System.Drawing.Size(63, 20);
            this.lblIn.TabIndex = 0;
            this.lblIn.Text = "Source";
            this.lblIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblIn6
            // 
            this.lblIn6.BackColor = System.Drawing.Color.DimGray;
            this.lblIn6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIn6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIn6.Location = new System.Drawing.Point(43, 46);
            this.lblIn6.Name = "lblIn6";
            this.lblIn6.OffColor = System.Drawing.Color.DimGray;
            this.lblIn6.OnColor = System.Drawing.Color.Red;
            this.lblIn6.Size = new System.Drawing.Size(18, 18);
            this.lblIn6.TabIndex = 6;
            this.lblIn6.Text = "6";
            this.lblIn6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIn6.Value = 0;
            this.lblIn6.Warn = false;
            // 
            // lblIn2
            // 
            this.lblIn2.BackColor = System.Drawing.Color.DimGray;
            this.lblIn2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIn2.Location = new System.Drawing.Point(23, 26);
            this.lblIn2.Name = "lblIn2";
            this.lblIn2.OffColor = System.Drawing.Color.DimGray;
            this.lblIn2.OnColor = System.Drawing.Color.Red;
            this.lblIn2.Size = new System.Drawing.Size(18, 18);
            this.lblIn2.TabIndex = 2;
            this.lblIn2.Text = "2";
            this.lblIn2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIn2.Value = 0;
            this.lblIn2.Warn = false;
            // 
            // lblIn5
            // 
            this.lblIn5.BackColor = System.Drawing.Color.DimGray;
            this.lblIn5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIn5.Location = new System.Drawing.Point(23, 46);
            this.lblIn5.Name = "lblIn5";
            this.lblIn5.OffColor = System.Drawing.Color.DimGray;
            this.lblIn5.OnColor = System.Drawing.Color.Red;
            this.lblIn5.Size = new System.Drawing.Size(18, 18);
            this.lblIn5.TabIndex = 5;
            this.lblIn5.Text = "5";
            this.lblIn5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIn5.Value = 0;
            this.lblIn5.Warn = false;
            // 
            // lblIn4
            // 
            this.lblIn4.BackColor = System.Drawing.Color.DimGray;
            this.lblIn4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIn4.Location = new System.Drawing.Point(3, 46);
            this.lblIn4.Name = "lblIn4";
            this.lblIn4.OffColor = System.Drawing.Color.DimGray;
            this.lblIn4.OnColor = System.Drawing.Color.Red;
            this.lblIn4.Size = new System.Drawing.Size(18, 18);
            this.lblIn4.TabIndex = 4;
            this.lblIn4.Text = "4";
            this.lblIn4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIn4.Value = 0;
            this.lblIn4.Warn = false;
            // 
            // lblIn3
            // 
            this.lblIn3.BackColor = System.Drawing.Color.DimGray;
            this.lblIn3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIn3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIn3.Location = new System.Drawing.Point(43, 26);
            this.lblIn3.Name = "lblIn3";
            this.lblIn3.OffColor = System.Drawing.Color.DimGray;
            this.lblIn3.OnColor = System.Drawing.Color.Red;
            this.lblIn3.Size = new System.Drawing.Size(18, 18);
            this.lblIn3.TabIndex = 3;
            this.lblIn3.Text = "3";
            this.lblIn3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIn3.Value = 0;
            this.lblIn3.Warn = false;
            // 
            // lblIn1
            // 
            this.lblIn1.BackColor = System.Drawing.Color.DimGray;
            this.lblIn1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIn1.Location = new System.Drawing.Point(3, 26);
            this.lblIn1.Name = "lblIn1";
            this.lblIn1.OffColor = System.Drawing.Color.DimGray;
            this.lblIn1.OnColor = System.Drawing.Color.Red;
            this.lblIn1.Size = new System.Drawing.Size(18, 18);
            this.lblIn1.TabIndex = 7;
            this.lblIn1.Text = "1";
            this.lblIn1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblIn1.Value = 0;
            this.lblIn1.Warn = false;
            // 
            // ButtonSource
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(128)))), ((int)(((byte)(134)))));
            this.Controls.Add(this.lblIn6);
            this.Controls.Add(this.lblIn2);
            this.Controls.Add(this.lblIn5);
            this.Controls.Add(this.lblIn4);
            this.Controls.Add(this.lblIn3);
            this.Controls.Add(this.lblIn1);
            this.Controls.Add(this.lblIn);
            this.Name = "ButtonSource";
            this.Size = new System.Drawing.Size(64, 67);
            this.Load += new System.EventHandler(this.ButtonSource_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public Color _OnColor = Color.Blue;
        public Color OnColor
        {
            get { return _OnColor; }
            set
            {
                if (value != _OnColor)
                {
                    _OnColor = value;
                    this.lblIn1.OnColor = this.lblIn2.OnColor = this.lblIn3.OnColor = this.lblIn4.OnColor = this.lblIn5.OnColor = this.lblIn6.OnColor = _OnColor;
                    NotifyPropertyChanged("OnColor");
                }
            }
        }

        public  String[] Sources = new String[6] { "Source 1", "Source 2", "Source 3", "Source 4", "Source 5", "Source 6" };
        public bool ToggleMode = false;
        void lblIn_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ButtonControl.ButtonControl b = (ButtonControl.ButtonControl)sender;
            int v = Int32.Parse(b.Text);
            if (b.Value==1) //Selected
            {
                 if (v != Value)
                {
                //   SetButton(v);
                   Value = v;
         //          NotifyPropertyChanged("Value");  //refire
                 }
   
             }

            //if (b.Value == 0 && v == Value && !ToggleMode)  //Selected
            // {
            //     b.Value = 1; //reselct
            // }
 
        }
        public ButtonSource()
        {
            InitializeComponent();
            Sources = Global.Sources;
            this.lblIn1.ToggleMode = false;
            this.lblIn2.ToggleMode = false;
            this.lblIn3.ToggleMode = false;
            this.lblIn4.ToggleMode = false;
            this.lblIn5.ToggleMode = false;
            this.lblIn6.ToggleMode = false;
            this.lblIn1.PropertyChanged += lblIn_PropertyChanged;
            this.lblIn2.PropertyChanged += lblIn_PropertyChanged;
            this.lblIn3.PropertyChanged += lblIn_PropertyChanged;
            this.lblIn4.PropertyChanged += lblIn_PropertyChanged;
            this.lblIn5.PropertyChanged += lblIn_PropertyChanged;
            this.lblIn6.PropertyChanged += lblIn_PropertyChanged;      
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int _Value = 0;
        public int Value
        {
            get { return _Value; }
            set
            {
                if (value != _Value)
                {
                    _Value = value;
                    SetButton(_Value);
                    NotifyPropertyChanged("Value");
                }
            } 
        }
        private void ButtonSource_Load(object sender, EventArgs e)
        {
            Refresh();
        }
 
        private int _InputSource = 0;
        // Input Signal
        private int InputSurce
        {
            get
            {
                return _InputSource;
            }
            set
            {
                _InputSource = value;
                //LoadSongToTrack();
                //  lblIn1.BackColor = lblIn1.BackColor = lblIn2.BackColor = lblIn3.BackColor = lblIn4.BackColor = lblIn5.BackColor = lblIn6.BackColor = lblMute.BackColor;
            }
        }


        private void SetButton(int InputChan)
        {
            if (InputChan <= 0 || InputChan > 6)
                lblIn.Text = "Unknown";
            else
                lblIn.Text = Sources[InputChan - 1];
            lblIn1.Value = (1 == InputChan) ? 1 : 0;
            lblIn2.Value = (2 == InputChan) ? 1 : 0;
            lblIn3.Value = (3 == InputChan) ? 1 : 0;
            lblIn4.Value = (4 == InputChan) ? 1 : 0;
            lblIn5.Value = (5 == InputChan) ? 1 : 0;
            lblIn6.Value = (6 == InputChan) ? 1 : 0;
            Refresh();
        }
      //  private string _TrackTitle = "";
        private void txtIn_KeyPress(object sender, KeyPressEventArgs e)
        {
          //  if (e.KeyChar == 13)
          //  {
          ////      _TrackTitle = txtIn.Text;
          //      lblIn.Text = _TrackTitle;

          //      if (_TrackTitle == "")
          //      {
          //          if (_InputSurce == 0)
          //          {
          //              lblIn.Text = "Signal OFF";
          //          }
          //          else
          //          {
          //              lblIn.Text = "Signal " + Convert.ToString(_InputSurce);
          //          }
          //      }

          //      lblIn.Visible = true;
          //  }
        }

        private void ButtonSource_Load_1(object sender, EventArgs e)
        {
      //      this.lblIn1.OnColor = this.lblIn2.OnColor = this.lblIn3.OnColor = this.lblIn4.OnColor = this.lblIn5.OnColor = this.lblIn6.OnColor = Color.Blue;
        }
    }
}

