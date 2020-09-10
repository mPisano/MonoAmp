using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmpService
{
        public enum CloseResult : int 
        {
        Yes = 1,
        No = 2,
        Background = 3
        }

    public partial class fCloseForm : Form
    {
        CloseResult result = CloseResult.No;
        
        public fCloseForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            result = CloseResult.Background;
            this.Hide();
        }

        private void bYes_Click(object sender, EventArgs e)
        {
            result = CloseResult.Yes;
            this.Hide();
        }

        private void bNo_Click(object sender, EventArgs e)
        {
            result = CloseResult.No;
            this.Hide();
        }

        private void bBack_Click(object sender, EventArgs e)
        {
            result = CloseResult.Background;
            this.Hide();
        }

    public CloseResult ASK()
        {
        Timer1.Interval = 1000 * 15;
        Timer1.Enabled = true;
        ShowDialog();
        Timer1.Enabled = false;
        return result;
        }

 
     
    }
}
