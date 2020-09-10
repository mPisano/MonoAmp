using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MPRSG6Z;
 

namespace MPR_Mixer
{
     

    public partial class frmMain : Form
    {
        Amp amp;
        AmpApi.HttpServer api;

        ArrayList _Tracks = new ArrayList();
       
        public frmMain()
        {
            InitializeComponent();
            this.buttonSource1.PropertyChanged += buttonSource1_PropertyChanged;
            this.buttonSource1.ToggleMode = true;          
        }
        void buttonSource1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (buttonSource1.Value != 0)
            {
                amp.SendCommand(0,Command.Source, buttonSource1.Value);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            amp = new Amp(System.Windows.Forms.WindowsFormsSynchronizationContext.Current);
            amp.PolledWait = Global.CurrentConfig.Parameters.PolledWait;
            amp.Units =  Global.CurrentConfig.Parameters.Units;
            amp.ComPort = Global.CurrentConfig.Parameters.ComPort;
            amp.PollMS = Global.CurrentConfig.Parameters.PollMS;
            amp.Sources = Global.CurrentConfig.Parameters.Sources;
            amp.QueueDupeElimination = Global.CurrentConfig.Parameters.RemoveDupes;
            amp.PolledWait = Global.CurrentConfig.Parameters.PolledWait;
            Global.Sources = amp.Sources;
            buttonSource1.Sources = amp.Sources;
            amp.Start();

            try
            {
                api = new AmpApi.HttpServer();
                api.Start(amp, Global.CurrentConfig.Parameters.IPAddress, Global.CurrentConfig.Parameters.WebPort);//, Global.CurrentConfig.Parameters.ApiPort);

            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.InnerException.Message, "Cannot Start Web API");
                throw;
            }

            amp.AutoSaveState = true;
            amp.OnValueChanged += amp_OnValueChanged;


    //        amp = new Amp(System.Windows.Forms.WindowsFormsSynchronizationContext.Current);
   //         api = new AmpApi.HttpServer();
            if (amp.Keypads.Count ==0 )
            {
                AmpConfig.fConfig f = new AmpConfig.fConfig();
                f.ShowDialog();
            }

            //Monoprice.fConfig f = new Monoprice.fConfig();
            //f.ShowDialog();
            
            this.SuspendLayout();
            this.buttonSource1.OnColor = Color.Orange;
            int chancount =   amp.Keypads.Count;//.Amps * 6;
            
            this.panel1.Left = (chancount * 70)+5;
            this.panel1.Top = 10;
            this.Width = ((chancount + 1) * 70) + 25;
            this.ResumeLayout(false);
            this.PerformLayout();

            //amp = new Amp(f.Amps, f.Port,"", System.Windows.Forms.WindowsFormsSynchronizationContext.Current);
      //      amp.Pollms = (int)nudPollms.Value;
  //          amp.AfterSendSleepMs = (int)nudWait.Value;
  //          amp.PollWaitms = (int)nudStatusLag.Value;
        //    amp.QueueDupeElimination = cbDupes.Checked;
   //         amp.AfterQueueSleep = (int)nudXmit.Value;

             for (int t = 0; t < chancount; t++)
            {
                Track.Track  track = new Track.Track();
                track.Left = (t * 70)+10;
                track.Top=10;
                _Tracks.Add(track);
            
               this.Controls.Add(track);
            }
            int i = 0;
            foreach (Track.Track _ArrayTracks in _Tracks)
            {
                _ArrayTracks.Keypad = amp.Keypads[i];
                i++;
                _ArrayTracks.TNUM = i;
            }
 
 //           try
 //           {
 ////               amp.PortName = f.Port;
 //               amp.Start();
 //           }
 //           catch (Exception)
 //           {
 //           }
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Tracks.Clear();
            amp.Stop();
            amp = null;
            this.Dispose();
        }
        void amp_OnValueChanged(object sender, Amp.State e)
        {
            System.Diagnostics.Debug.WriteLine("ID:" + e.Keypad.ID + " New:" + e.NewValue.ToString() + " Old:" + e.OldValue.ToString() + " Cmd:" + e.Cmd.ToString());
        }   
        private void LblMaOn_Click(object sender, EventArgs e)
        {
            amp.SendCommand(0, Command.Power, 1);
        }
        private void LblMaOff_Click(object sender, EventArgs e)
        {
            amp.SendCommand(0, Command.Power, 0);
        }
        private void lbMuOn_Click(object sender, EventArgs e)
        {
            amp.SendCommand(0, Command.Mute, 1);
        }
        private void lbMuOFF_Click(object sender, EventArgs e)
        {
            amp.SendCommand(0, Command.Mute, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            string url = "http://" + Global.CurrentConfig.Parameters.WebAddress  + ":" + Global.CurrentConfig.Parameters.WebPort + "/Default.html";
                  //     string url = "http://localhost:40000/Web/Default.html";
                        System.Diagnostics.Process.Start(url);
        }
    }

    public class StringValue
    {
        public StringValue(string s)
        {
            _value = s;
        }
        public string Value { get { return _value; } set { _value = value; } }
        string _value;
    }

}
