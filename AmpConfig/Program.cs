using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HttpNamespaceManager.UI;
using System.Diagnostics;

namespace AmpConfig
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static bool IsRunningMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }
        [STAThread]
        static void Main()
        {
            if (!IsRunningMono() && !Util.IsUserAnAdmin())
            {
                ProcessStartInfo procInfo = new ProcessStartInfo(Application.ExecutablePath); //, String.Format("-{0} {1}", action.ToString(), url));
                procInfo.UseShellExecute = true;
                procInfo.Verb = "runas";
                procInfo.WindowStyle = ProcessWindowStyle.Normal;
                Process.Start(procInfo);
                Application.Exit();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new fConfig());
            }
        }
    }
}
