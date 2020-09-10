using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MPRSG6Z;
using System.IO;

namespace Track
{
    public partial class Track : UserControl
    {
        public Track()
        {
            InitializeComponent();
        }
        public KeyPad _keypad; 
        public int _TNUM = 0;
        public KeyPad Keypad
        {
            get
            {
                return _keypad;
            }
            set
            {
                if (!(value == null))
                {
                
                    _keypad = value;
                 //   _keypad..synchronizationContext = System.Windows.Forms.WindowsFormsSynchronizationContext.Current;
                    lblVolMain.DataBindings.Add("Text", volMain, "Value", false, DataSourceUpdateMode.OnPropertyChanged).Format += delegate(object sentFrom, ConvertEventArgs convertEventArgs)
                    {
                        convertEventArgs.Value = GetPercent(0, 38, (int)convertEventArgs.Value).ToString();
                    };
                    lbPanVal.DataBindings.Add("Text", volPAN, "Value", false, DataSourceUpdateMode.OnPropertyChanged).Format += delegate(object sentFrom, ConvertEventArgs convertEventArgs)
                    {
                        convertEventArgs.Value = (Int32)convertEventArgs.Value - 10;
                    };
                    volMain.DataBindings.Add("Value",Keypad,"VO",false,DataSourceUpdateMode.OnPropertyChanged);
                    volPAN.DataBindings.Add("Value", Keypad, "BL",false,DataSourceUpdateMode.OnPropertyChanged);
                    bcMute.DataBindings.Add("Value", Keypad, "MU",false,DataSourceUpdateMode.OnPropertyChanged);
                    volBass.DataBindings.Add("Value", Keypad, "BS",false,DataSourceUpdateMode.OnPropertyChanged);
                    volTreb.DataBindings.Add("Value", Keypad, "TR", false, DataSourceUpdateMode.OnPropertyChanged);
                    bcPower.DataBindings.Add("Value", Keypad, "PR", false, DataSourceUpdateMode.OnPropertyChanged);
                    bsChan.DataBindings.Add("Value", Keypad, "CH", false, DataSourceUpdateMode.OnPropertyChanged);
                    bcPower.DataBindings.Add("Warn", Keypad, "PA", false, DataSourceUpdateMode.OnPropertyChanged);
                    LED.DataBindings.Add("Text", Keypad, "VO", false, DataSourceUpdateMode.OnPropertyChanged);
                    tbKpName.DataBindings.Add("Text", Keypad, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
       //             Binding bind = new Binding("ForeColor", Keypad, "LS", true, DataSourceUpdateMode.OnPropertyChanged);
                    //bind.Format += (s, e) =>
                    //{
                    //    e.Value = (int)e.Value == 1;
                    //    lblTrackNum2.ForeColor = ((bool)e.Value) ? Color.Black  : Color.Gray ;
                    //}; 
                    //lblTrackNum2.DataBindings.Add(bind);

                }
            }
        }
        private int GetPercent(int low, int Hi, int val)
        {
            return (int)(((float)(val - low) / (float)(Hi - low)) * 100f);
        }
        //private void DecimalToCurrencyString(object sender, ConvertEventArgs e)
        //{
        //    if (e.DesiredType == typeof(string))
        //    {
        //        // Use the ToString method to format the value as currency ("c").
        //        e.Value = ((decimal)e.Value).ToString("c");
        //    }
        //}

        //private void CurrencyStringToDecimal(object sender, ConvertEventArgs e)
        //{
        //    if (e.DesiredType == typeof(decimal))
        //    {
        //        // Convert the string back to decimal using the shared Parse method.
        //        e.Value = Decimal.Parse(e.Value.ToString(),
        //          System.Globalization.NumberStyles.Currency, null);
        //    }
        //}
         public int TNUM
        {
            get
            {
                return _TNUM;
            }
            set
            {
                _TNUM = value;
                  lblTrackNum2.Text = _TNUM.ToString();
            }
        }
 
        Color _TotalColor = Color.Black;
        public void TrackColor(Color _Color)
        {
           // lblIn.BackColor = _Color;

           /*
            System.Drawing.Color.Green;
            System.Drawing.Color.Blue;
            System.Drawing.Color.Purple;
            */
        }

        public Color TotalColor
        {
            get { return _TotalColor; }
            set { _TotalColor = value; }
        }

        protected override void OnPaint(PaintEventArgs paintEvnt)
        {
            CalculateAndDraw();
         }

        public void CalculateAndDraw()
        {

            int MaxR = 128;// lblIn.BackColor.R;
            int MaxG = 128;// lblIn.BackColor.G;
            int MaxB = 128;// lblIn.BackColor.B;

            int R = 0;
            int G = 0;
            int B = 0;

            R = Math.Max(((Convert.ToInt32(lblVolMain.Text) * MaxR) / 125),MaxR);
            G = Math.Max(((Convert.ToInt32(lblVolMain.Text) * MaxG) / 125),MaxG);
            B = Math.Max(((Convert.ToInt32(lblVolMain.Text) * MaxB) / 125), MaxB);

            DrawTrack(Color.FromArgb(R, G, B));
            TotalColor = Color.FromArgb(R, G, B);
        }

        private void DrawTrack(Color _color)
        {
            Pen myPen;
            myPen = new Pen(_color, 3);
            Graphics formGraphics = this.CreateGraphics();
            formGraphics.DrawLine(myPen, 1, 100, 1, 640);
            formGraphics.DrawLine(myPen, 81, 100, 81, 640);
            myPen.Dispose();
            formGraphics.Dispose();
        }

        private void panelTrack_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMute_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}