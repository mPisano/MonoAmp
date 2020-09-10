using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;

static class GlobalClass
{
	public static Config CurrentConfig = new Config();


	private static string m_globalVar = "";

	public static string GlobalVar
	{
		get { return m_globalVar; }
		set { m_globalVar = value; }
	}
}

namespace AmpService
{

	static class Program
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool SetForegroundWindow(IntPtr hWnd);


        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
               
            if (args.Count() == 0)
                if (System.Environment.UserInteractive)
                {
                    WinMain(args);
                }
                else
                {
                    System.IO.Directory.SetCurrentDirectory(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));
                    SvcMain(args);
                }
            else
            {
                for (int i = 0; i < args.Length; i++) // Loop through array
                {
                    string argument = args[i].ToUpper();
                    switch (argument.ToUpper())
                    {
                        case "INSTALL":
                            ServiceTools.ServiceInstaller.InstallAndStart(Assembly.GetExecutingAssembly().GetName().Name, Application.ProductName, Assembly.GetExecutingAssembly().Location);

                            var attribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).Cast<AssemblyDescriptionAttribute>().FirstOrDefault();
                            String Description;
                            if (attribute != null)
                            {
                                Description = attribute.Description;
                                ServiceTools.ServiceInstaller.SetServiceDescription(Assembly.GetExecutingAssembly().GetName().Name, Description);
                            }

                            break;
                        case "UNINSTALL":
                            ServiceTools.ServiceInstaller.Uninstall(Assembly.GetExecutingAssembly().GetName().Name);
                            break;

                        case "CONFIG":
                            AmpConfig.fConfig f = new AmpConfig.fConfig();
                           f.ShowDialog();
                           break;

                        default:
                            {

                            }
                            break;
                    }
                }
            }
        }

		[STAThread]
        private static void WinMain(string[] args)
		{
            string processName = Application.ProductName;
			bool singleInstance = true; //Is application single instance (Mutex = mutual exclusion)

			using (Mutex mutex = new Mutex(true, processName, out singleInstance))
			{
				if (!singleInstance)      //If not single instance
				{
					GC.KeepAlive(mutex);  //Tell "Garbage Collector" to keep hold of Mutex resource 

					NativeMethods.PostMessage(
						(IntPtr)NativeMethods.HWND_BROADCAST,
						NativeMethods.WM_SHOWME,
						IntPtr.Zero,
						IntPtr.Zero);  // Make Original Instance Form Come forward
					Environment.Exit(0); //Close Application
				}
				else
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new fTray());
				}
			}
		}

        private static void SvcMain(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new  AmpSvc() 
			};
            ServiceBase.Run(ServicesToRun);
        }

        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
       //     System.IO.File.AppendAllText(@"error.txt", ((Exception)e.ExceptionObject).Message + ((Exception)e.ExceptionObject).InnerException.Message);
        }

	}

}
