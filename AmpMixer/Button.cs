using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace ButtonControl
{
    public class ButtonControl : Label, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName, int Value)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool ToggleMode = true;
        private int _Value = 0;
        private Color _OnColor = Color.Red;
        private Color _OffColor = Color.DimGray;
        private Color _WarnColor = Color.Yellow;

        public int Value
        {
            get { return _Value; }
            set
            {
                if (value != _Value)
                {
                    _Value = value;
                    PaintBox();
                    NotifyPropertyChanged("Value", _Value);
                }
            }
        }

        bool _Warn;
        public bool Warn
        {
            get { return _Warn; }
            set
            {
                if (value != _Warn)
                {
                    _Warn = value;
                    PaintBox();
                    NotifyPropertyChanged("Warn", _Value);
                }
            }
        }

        public Color OnColor
        {
            get { return _OnColor; }
            set
            {
                if (value != _OnColor)
                {
                    _OnColor = value;
                    PaintBox();
                    NotifyPropertyChanged("OnColor", _Value);
                }
            }
        }

        public Color OffColor
        {
            get { return _OffColor; }
            set
            {
                if (value != _OffColor)
                {
                    _OffColor = value;
                    PaintBox();
                    NotifyPropertyChanged("OffColor", _Value);
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (ToggleMode)
                if (_Value != 0)
                    Value = 0;
                else
                    Value = 1;
            else
                Value = 1;
        }

        private void PaintBox()
        {
            if (_Warn)
                this.BackColor = _WarnColor;
            else
                if (_Value == 0)
                    this.BackColor = _OffColor;
                else
                    this.BackColor = _OnColor;
             Refresh();
        }

        protected override void OnCreateControl()
        {
            PaintBox();
            base.OnCreateControl();
        }
    }
}