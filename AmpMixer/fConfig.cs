using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Monoprice
{
    public partial class fConfig : Form
    {
        public String Port;
        public int Amps;
        public fConfig()
        {
            InitializeComponent();
        }
        private void fConfig_Load(object sender, EventArgs e)
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cmbComSelect.Items.Add(port);
            }
        }
        private void bOpen_Click(object sender, EventArgs e)
        {
            try
            {
                Port = cmbComSelect.SelectedItem.ToString();
            }
            catch (Exception)
            {
                Port = "";
            }
            Amps = (int)nudAmps.Value;
            this.Close();
        }
    }
}
