using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using ServiceTools;
using MPRSG6Z;

namespace AmpService
{
    public partial class fTray: Form
    {
        Amp m_control;
        AmpApi.HttpServer api;

        //      Controler m_control;
        SystemTray Tray;
        bool ServiceInstalled;
        bool ServiceCanInstall;
        bool ServiceCanStartStop;
        public int State = -1;
     //   internal EnterpriseDT.Util.Debug.Logger m_Log;

        public fTray()
        {
            InitializeComponent();

            //m_Log = Logger.GetLogger("Tray");
            //m_Log.Info("Tray Initializing");
        }

        private void Start()
        {
        //    m_Log.Info("Starting Worker");
            CheckState1(); // Make sure still not Installed
            if (ServiceInstalled)
                ServiceTools.ServiceInstaller.StartService(Assembly.GetExecutingAssembly().GetName().Name);
            else
            {
                gb_SvcInstall.Visible = false;
                
                m_control = new Amp();
                m_control.StatusChanged += m_control_StatusChanged;
                m_control.PolledWait = Global.CurrentConfig.Parameters.PolledWait;
                m_control.Units = Global.CurrentConfig.Parameters.Units;
                m_control.ComPort = Global.CurrentConfig.Parameters.ComPort;
                m_control.PollMS = Global.CurrentConfig.Parameters.PollMS;
                m_control.QueueDupeElimination = Global.CurrentConfig.Parameters.RemoveDupes;
                m_control.Sources = Global.CurrentConfig.Parameters.Sources;
                m_control.Start();
                api = new AmpApi.HttpServer();
                api.Start(m_control, Global.CurrentConfig.Parameters.IPAddress, Global.CurrentConfig.Parameters.WebPort);//, Global.CurrentConfig.Parameters.ApiPort);
            }
            CheckState();
        }

        void m_control_StatusChanged(object sender, ControlerStatusChangedEventArgs e)
        {
            m_control_UpdateIcon(e);
        //    m_Log.Debug(e.Status);
            tbStatus.Text = e.Status;
            if (e.State == -1)
                m_control = null;
            CheckState();
        }





        private void bStart_Click(object sender, EventArgs e)
        {
 //           m_Log.Info("Start Button Pressed");
            Start();
        }

        void m_control_UpdateIcon(ControlerStatusChangedEventArgs e)
        {
            if (e.State == -1)
            {
                gb_SvcInstall.Visible = (ServiceInstalled || ServiceCanInstall);
                bStart.Enabled = true;
                bStop.Enabled = false;
                try
                {
                    Tray.trayIcon.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AmpService.Tray.SvcDn.ico"));
                }
                catch (Exception)
                {
                }

            }

            if (e.State == 1)
            {
                gb_SvcInstall.Visible = false;
                bStart.Enabled = false;
                bStop.Enabled = true;
                try
                {
                    Tray.trayIcon.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AmpService.Tray.SvcUp.ico"));
                }
                catch (Exception)
                {
                }
            }
        }
        




        private void bStop_Click(object sender, EventArgs e)
        {
            if (ServiceInstalled)
                ServiceTools.ServiceInstaller.StopService(Assembly.GetExecutingAssembly().GetName().Name);
            else
            {
                api.Stop();
                api = null;
     //           m_Log.Info("Stop Button Pressed");
                m_control.Stop();
        //        m_control.StatusChanged -= m_control_StatusChanged;
        //        m_control = null;
            }
            CheckState();
        }



        private void fTray_Load(object sender, EventArgs e)
        {
            Tray = new SystemTray(this, "Amp Server", this.Icon, true);
            this.Text += this.GetType().Assembly.GetName().Name + " Version " + this.GetType().Assembly.GetName().Version.ToString();

            ServiceInstalled = (ServiceInstaller.GetServiceStatus(Assembly.GetExecutingAssembly().GetName().Name) != ServiceTools.ServiceState.NotFound);
            ServiceCanInstall = ServiceInstaller.CanInstall();
            ServiceCanStartStop = ServiceInstaller.CanStartStop();

            CheckState();
            if (!ServiceInstalled)
                Start();

 
        }

        private void fTray_FormClosing(object sender, FormClosingEventArgs e)
        {
       //     m_Log.Info("CIFCEDI Form Closing - " + e.CloseReason.ToString());
            Tray.Visible = false;
            if (m_control != null) {
                if (m_control.ServiceState != -1)
                {
                    if (e.CloseReason == CloseReason.UserClosing)
                    {
                        fCloseForm fAsk = new fCloseForm();
                        CloseResult result = fAsk.ASK();
                        if (result != CloseResult.Yes)
                        {
                            Tray.Visible = true;
                            e.Cancel = true;
                            if (result == CloseResult.Background)
                            {
                                this.WindowState = FormWindowState.Minimized;
                                Tray.FormChanged();
                            }
                        }
                        else
                        {
                            Tray.ShutOff();
                            m_control.Stop(); // Signal Stop

                            int Wait = 0;
                            while (m_control.ServiceState != -1 && Wait < 50)
                            {
                                Wait++;
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                    }
                    else
                    {
                        Tray.ShutOff();
                        m_control.Stop(); // Signal Stop

                        int Wait = 0;
                        while (m_control.ServiceState != -1 && Wait < 50)
                        {
                            Wait++;
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }


        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
        }

        private void fTray_Resize(object sender, EventArgs e)
        {
            if (Tray != null)
            {
                Tray.FormChanged();
            }
        }

        private void bInstall_Click(object sender, EventArgs e)
        {
            CheckState1(); // Make sure still not Installed
            if (!ServiceInstalled)
            {
                bInstall.Enabled = false;
                ServiceTools.ServiceInstaller.InstallAndStart(Assembly.GetExecutingAssembly().GetName().Name, Application.ProductName, Assembly.GetExecutingAssembly().Location);

                var attribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).Cast<AssemblyDescriptionAttribute>().FirstOrDefault();
                String Description;
                if (attribute != null)
                {
                    Description = attribute.Description;
                    ServiceTools.ServiceInstaller.SetServiceDescription(Assembly.GetExecutingAssembly().GetName().Name, Description);
                }
                CheckState1();
            }
        }

        private void bUninstall_Click(object sender, EventArgs e)
        {
            bUninstall.Enabled = false;
            bStop.Enabled = false;
            bStart.Enabled = false;
            TmrChkSvc.Enabled = false;

            if (ServiceTools.ServiceInstaller.Uninstall(Assembly.GetExecutingAssembly().GetName().Name))
            {
                CheckState1();
                bStart.Enabled = true;
                tbStatus.Text = "Service Uninstalled";
            }
            else
                tbStatus.Text = "Service Uninstall Failed";
        }

        private void TmrChkSvc_Tick(object sender, EventArgs e)
        {
            CheckState1();
        }

        private void CheckState()
        {
            if (TmrChkSvc.Enabled != true)
            {
                CheckState1();
            }
        }

        private void CheckState1()
        {
            ServiceState State = ServiceInstaller.GetServiceStatus(Assembly.GetExecutingAssembly().GetName().Name);

            ServiceInstalled = (State != ServiceTools.ServiceState.NotFound);
            gb_SvcInstall.Visible = (ServiceInstalled || ServiceCanInstall);

            if (ServiceInstalled)
            {
                bInstall.Enabled = false;
                bUninstall.Enabled = ServiceCanInstall; // Windows 2003 server seems to let us uninstall, but we have no rights to put it back unless admin?
                tbStatus.Text = State.ToString();
                gb_StartStop.Text = "Service";
                bStop.Enabled =  (State == ServiceState.Starting || State == ServiceState.Running);
                bStart.Enabled = (State == ServiceState.Stopping || State == ServiceState.Stopped);
                if (bStop.Enabled)
                    Tray.trayIcon.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AmpService.Tray.SvcUp.ico"));
                else
                    Tray.trayIcon.Icon = new Icon(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AmpService.Tray.SvcDn.ico"));

                if (!ServiceCanStartStop)
                {
                    bStart.Enabled = false;
                    bStop.Enabled = false;
                }
            }
            else
            {
                gb_StartStop.Text = "Process";

                if (m_control == null)
                {
                    bStop.Enabled = false;
                    bStart.Enabled = true;
                    if (ServiceCanInstall)
                    {
                        bInstall.Enabled = true;
                        bUninstall.Enabled = false;
                    }
                    else
                    {
                        bInstall.Enabled = false;
                        bUninstall.Enabled = false;
                    }
                }
                else
                {
                    bStop.Enabled = true;
                    bStart.Enabled = false;
                    bInstall.Enabled = false;
                    bUninstall.Enabled = false;
                }
            }

            TmrChkSvc.Enabled = ServiceInstalled;
        }

        private void bConfigEdi_Click(object sender, EventArgs e)
        {
         //   m_Log.Info("EDI Config Button Pressed");
            AmpConfig.fConfig f = new AmpConfig.fConfig();
            f.ShowDialog();
        }

        private void bBrowse_Click(object sender, EventArgs e)
        {

            string URL = "http://" + Global.CurrentConfig.Parameters.WebAddress  + ":"+ Global.CurrentConfig.Parameters.WebPort + "/Default.html";

            System.Diagnostics.Process.Start(URL);
        }


             
     }
}
