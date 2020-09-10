using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PF_Consola
{
    public partial class frmColors : Form
    {
        public frmColors()
        {
            InitializeComponent();
        }

        private void butBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lblTrackColor1_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTrackColor1.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTrackColor1.BackColor = colorDialog.Color;
            }
        }

        private void lblTrackColor2_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTrackColor2.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTrackColor2.BackColor = colorDialog.Color;
            }
        }

        private void lblTrackColor3_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTrackColor3.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTrackColor3.BackColor = colorDialog.Color;
            }
        }

        private void lblTrackColor4_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTrackColor4.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTrackColor4.BackColor = colorDialog.Color;
            }
        }

        private void lblTrackColor5_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTrackColor5.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTrackColor5.BackColor = colorDialog.Color;
            }
        }

        private void lblTrackColor6_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTrackColor6.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTrackColor6.BackColor = colorDialog.Color;
            }
        }

        private void lblTrackColor7_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTrackColor7.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTrackColor7.BackColor = colorDialog.Color;
            }
        }

        private void lblTrackColor8_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTrackColor8.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTrackColor8.BackColor = colorDialog.Color;
            }
        }

        private void lblTrackColor9_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTrackColor9.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTrackColor9.BackColor = colorDialog.Color;
            }
        }

        private void lblTrackColor10_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTrackColor10.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTrackColor10.BackColor = colorDialog.Color;
            }
        }
    }
}