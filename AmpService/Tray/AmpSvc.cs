using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
//using EnterpriseDT.Util;
//using EnterpriseDT.Util.Debug;
using System.Reflection;
using MPRSG6Z;

namespace AmpService
{
    public partial class AmpSvc : ServiceBase
    {
         private Amp  m_svcController;
 //       Logger m_Log;
         private AmpApi.HttpServer api;
         public AmpSvc()
        {
            
            InitializeComponent();
        //    Directory.SetCurrentDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
            this.ServiceName = Assembly.GetExecutingAssembly().GetName().Name;
            m_svcController = new Amp();

        }

        protected override void OnStart(string[] args)
        {
       //     amp = new Amp(System.Windows.Forms.WindowsFormsSynchronizationContext.Current);
            m_svcController.Units = Global.CurrentConfig.Parameters.Units;
            m_svcController.ComPort = Global.CurrentConfig.Parameters.ComPort;
            m_svcController.PollMS = Global.CurrentConfig.Parameters.PollMS;
            m_svcController.QueueDupeElimination = Global.CurrentConfig.Parameters.RemoveDupes;
            m_svcController.Sources = Global.CurrentConfig.Parameters.Sources;
            m_svcController.PolledWait = Global.CurrentConfig.Parameters.PolledWait;

            m_svcController.Start();
            api = new AmpApi.HttpServer();
            api.Start(m_svcController, Global.CurrentConfig.Parameters.IPAddress, Global.CurrentConfig.Parameters.WebPort);//, Global.CurrentConfig.Parameters.ApiPort);
         //  Directory.SetCurrentDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
         //   File.AppendAllText(@"C:\Temp\sss.txt", "Start " + Assembly.GetExecutingAssembly().Location);
            m_svcController.Start();// RunWorkerAsync(args);
       //     WinEventLog.WriteEntry(ServiceName + " Started");
         //   m_Log.Info("Service" + ServiceName + " Started"); 
        }

        protected override void OnStop()
        {
            api.Stop();
         //   Directory.SetCurrentDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
       //     File.AppendAllText(@"C:\Temp\sss.txt", "Stop");
            m_svcController.Stop(); // CancelAsync();
           // while (BW.IsBusy) Thread.Sleep(1000);
     //       WinEventLog.WriteEntry(ServiceName + " Stopped");
         //   m_Log.Info("Service" + ServiceName + " Stopped");  
        }
 
    }
}


 