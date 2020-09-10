using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using MPRSG6Z;

namespace MPRSG6Z_Test
{
    public partial class AmpTest : Form
    {
        Amp amp;
        delegate void SetKeyCallback(object sender, KeyPad.State e);
        delegate void SetAmpCallback(object sender, Amp.State e);
        public AmpTest()
        {
            InitializeComponent();
        }

        private void AmpTest_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cmbComSelect.Items.Add(port);
            }
        }

        void amp_OnValueChanged(object sender, Amp.State e)
        {
             this.listBox1.Items.Add(DateTime.Now.ToShortTimeString() + " ID: "+e.Keypad.ID+" "+e.Cmd.ToString() + " Changed From " + e.OldValue.ToString() + " To "+e.NewValue.ToString()) ;
        }

         private void bStatus_Click(object sender, EventArgs e)
        {
            if (amp != null)
            {
                amp.GetStatus(0);
            }
        }
        private void bOpen_Click(object sender, EventArgs e)
        {
            Open(null);
        }

        void Open(string Path)
        {
            if (amp == null)
            {
                string port = "";
                try
                {
                    port = cmbComSelect.Text;//.SelectedItem.ToString();
                }
                catch (Exception)
                {
                    port = "";
                    if (string.IsNullOrWhiteSpace(Path))
                        Path = ""; // try Xml\Demo mode
                }

                amp = new Amp((int)nudAmps.Value, port, System.Windows.Forms.WindowsFormsSynchronizationContext.Current);
                amp.PollMS  = (int)nudPollms.Value;
                amp.OnValueChanged += amp_OnValueChanged;

                dataGridView1.RowHeadersVisible = false;
                dataGridView1.DataSource = amp.Keypads;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
                amp.Start();
            }
        }
        private void bClose_Click(object sender, EventArgs e)
        {
            if (amp != null)
            {
                amp.Stop();
                amp = null;
            }
        }

         private void button1_Click(object sender, EventArgs e)
        {
            if (amp != null)
            {
                amp.SaveState("");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Open("");
        }

        private void nudPollms_ValueChanged(object sender, EventArgs e)
        {
            if (amp != null)
            {
                amp.PollMS = (int)nudPollms.Value;
            }
        }
   }
}
