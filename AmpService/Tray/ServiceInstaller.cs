////Installs and starts the service
//ServiceInstaller.InstallAndStart("MyServiceName", "MyServiceDisplayName", "C:\PathToServiceFile.exe");

////Removes the service
//ServiceInstaller.Uninstall("MyServiceName");

////Checks the status of the service
//ServiceInstaller.GetServiceStatus("MyServiceName");

////Starts the service
//ServiceInstaller.StartService("MyServiceName");

////Stops the service
//ServiceInstaller.StopService("MyServiceName");

////Check if service is installed
//ServiceInstaller.ServiceIsInstalled("MyServiceName");

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ServiceTools
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ServiceManagerRights
    {
        /// <summary>
        /// 
        /// </summary>
        Connect = 0x0001,
        /// <summary>
        /// 
        /// </summary>
        CreateService = 0x0002,
        /// <summary>
        /// 
        /// </summary>
        EnumerateService = 0x0004,
        /// <summary>
        /// 
        /// </summary>
        Lock = 0x0008,
        /// <summary>
        /// 
        /// </summary>
        QueryLockStatus = 0x0010,
        /// <summary>
        /// 
        /// </summary>
        ModifyBootConfig = 0x0020,
        /// <summary>
        /// 
        /// </summary>
        StandardRightsRequired = 0xF0000,
        /// <summary>
        /// 
        /// </summary>
        AllAccess = (StandardRightsRequired | Connect | CreateService |
        EnumerateService | Lock | QueryLockStatus | ModifyBootConfig)
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ServiceRights
    {
        /// <summary>
        /// 
        /// </summary>
        QueryConfig = 0x1,
        /// <summary>
        /// 
        /// </summary>
        ChangeConfig = 0x2,
        /// <summary>
        /// 
        /// </summary>
        QueryStatus = 0x4,
        /// <summary>
        /// 
        /// </summary>
        EnumerateDependants = 0x8,
        /// <summary>
        /// 
        /// </summary>
        Start = 0x10,
        /// <summary>
        /// 
        /// </summary>
        Stop = 0x20,
        /// <summary>
        /// 
        /// </summary>
        PauseContinue = 0x40,
        /// <summary>
        /// 
        /// </summary>
        Interrogate = 0x80,
        /// <summary>
        /// 
        /// </summary>
        UserDefinedControl = 0x100,
        /// <summary>
        /// 
        /// </summary>
        Delete = 0x00010000,
        /// <summary>
        /// 
        /// </summary>
        StandardRightsRequired = 0xF0000,
        /// <summary>
        /// 
        /// </summary>
        AllAccess = (StandardRightsRequired | QueryConfig | ChangeConfig |
        QueryStatus | EnumerateDependants | Start | Stop | PauseContinue |
        Interrogate | UserDefinedControl)
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceBootFlag
    {
        /// <summary>
        /// 
        /// </summary>
        Start = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        SystemStart = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        AutoStart = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        DemandStart = 0x00000003,
        /// <summary>
        /// 
        /// </summary>
        Disabled = 0x00000004
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceState
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown = -1, // The state cannot be (has not been) retrieved.
        /// <summary>
        /// 
        /// </summary>
        NotFound = 0, // The service is not known on the host server.
        /// <summary>
        /// SERVICE_STOPPED
        /// </summary>
        Stopped = 1,  
        /// <summary>
        /// SERVICE_START_PENDING
        /// </summary>
        Starting = 2,  
        /// <summary>
        /// SERVICE_STOP_PENDING
        /// </summary>
        Stopping = 3,  
        /// <summary>
        /// SERVICE_RUNNING
        /// </summary>
        Running = 4,
        /// <summary>
        ///  SERVICE_CONTINUE_PENDING
        /// </summary>
        Continuing = 5,  
        /// <summary>
        /// SERVICE_PAUSE_PENDING
        /// </summary>
        Pausing = 6,
        /// <summary>
        /// SERVICE_PAUSED
        /// </summary>
        Paused = 7 
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceControl
    {
        /// <summary>
        /// 
        /// </summary>
        Stop = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        Pause = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        Continue = 0x00000003,
        /// <summary>
        /// 
        /// </summary>
        Interrogate = 0x00000004,
        /// <summary>
        /// 
        /// </summary>
        Shutdown = 0x00000005,
        /// <summary>
        /// 
        /// </summary>
        ParamChange = 0x00000006,
        /// <summary>
        /// 
        /// </summary>
        NetBindAdd = 0x00000007,
        /// <summary>
        /// 
        /// </summary>
        NetBindRemove = 0x00000008,
        /// <summary>
        /// 
        /// </summary>
        NetBindEnable = 0x00000009,
        /// <summary>
        /// 
        /// </summary>
        NetBindDisable = 0x0000000A
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ServiceError
    {
        /// <summary>
        /// 
        /// </summary>
        Ignore = 0x00000000,
        /// <summary>
        /// 
        /// </summary>
        Normal = 0x00000001,
        /// <summary>
        /// 
        /// </summary>
        Severe = 0x00000002,
        /// <summary>
        /// 
        /// </summary>
        Critical = 0x00000003
    }


    /// <summary>
    /// 
    /// </summary>
    ///

    public enum InfoLevel : int
    {
        Config_Descriptions = 1,
        CONFIG_FAILURE_ACTIONS = 2,
        CONFIG_DELAYED_AUTO_START_INFO = 3,
        CONFIG_FAILURE_ACTIONS_FLAG = 4,
        CONFIG_SERVICE_SID_INFO = 5,
        REQUIRED_PRIVILEGES_INFO = 6,
        PRESHUTDOWN_INFO = 7
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SERVICE_DESCRIPTION
    {
        public string lpDescription;
    }
    /// <summary>
    /// Installs and provides functionality for handling windows services
    /// </summary>
    public class ServiceInstaller
    {
        private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        private const int SERVICE_WIN32_OWN_PROCESS = 0x00000010;

        [StructLayout(LayoutKind.Sequential)]
        private class SERVICE_STATUS
        {
            public int dwServiceType = 0;
            public ServiceState dwCurrentState = 0;
            public int dwControlsAccepted = 0;
            public int dwWin32ExitCode = 0;
            public int dwServiceSpecificExitCode = 0;
            public int dwCheckPoint = 0;
            public int dwWaitHint = 0;
        }

        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerA")]
        private static extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, ServiceManagerRights dwDesiredAccess);
        [DllImport("advapi32.dll", EntryPoint = "OpenServiceA",
        CharSet = CharSet.Ansi)]
        private static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceRights dwDesiredAccess);
        [DllImport("advapi32.dll", EntryPoint = "CreateServiceA")]
        private static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceRights dwDesiredAccess, int dwServiceType, ServiceBootFlag dwStartType,
            ServiceError dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lp, string lpPassword);
        [DllImport("advapi32.dll")]
        private static extern int CloseServiceHandle(IntPtr hSCObject);
        [DllImport("advapi32.dll")]
        private static extern int QueryServiceStatus(IntPtr hService, SERVICE_STATUS lpServiceStatus);
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int DeleteService(IntPtr hService);
        [DllImport("advapi32.dll")]
        private static extern int ControlService(IntPtr hService, ServiceControl dwControl, SERVICE_STATUS lpServiceStatus);
        [DllImport("advapi32.dll", EntryPoint = "StartServiceA")]
        private static extern int StartService(IntPtr hService, int dwNumServiceArgs, int lpServiceArgVectors);
        
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool ChangeServiceConfig2(IntPtr hService, InfoLevel dwInfoLevel, [MarshalAs(UnmanagedType.Struct)] ref SERVICE_DESCRIPTION lpInfo);
        /// <summary>
        /// 
        /// </summary>
        public ServiceInstaller()
        {
        }

        /// <summary>
        /// Takes a service name and tries to stop and then uninstall the windows serviceError
        /// </summary>
        /// <param name="ServiceName">The windows service name to uninstall</param>
        public static bool Uninstall(string ServiceName)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr service = OpenService(scman, ServiceName,
                ServiceRights.StandardRightsRequired | ServiceRights.Stop |
                ServiceRights.QueryStatus);
                if (service == IntPtr.Zero)
                {
                    throw new ApplicationException("Service not installed.");
                }
                try
                {
                    StopService(service);
                    int ret = DeleteService(service);
                    if (ret == 0)
                    {
                        int error = Marshal.GetLastWin32Error();
                        throw new ApplicationException("Could not delete service " + error);
                    }
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
            return  !ServiceIsInstalled(ServiceName);
        }

        /// <summary>
        /// Accepts a service name and returns true if the service with that service name exists
        /// </summary>
        /// <param name="ServiceName">The service name that we will check for existence</param>
        /// <returns>True if that service exists false otherwise</returns>
        public static bool ServiceIsInstalled(string ServiceName)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr service = OpenService(scman, ServiceName, ServiceRights.QueryStatus);
                CloseServiceHandle(scman);
                if (service == IntPtr.Zero) return false;
                CloseServiceHandle(service);
                return true;
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        public static bool CanInstall()
        {
            return HasRights(ServiceManagerRights.CreateService);
        }

        public static bool CanStartStop()
        {
            return HasRights(ServiceManagerRights.Connect);
        }

        public static bool HasRights(ServiceManagerRights Rights)
        {
            bool retval;
            IntPtr scman = OpenSCManager(Rights);
            retval = (scman != IntPtr.Zero);
            if (retval) 
                CloseServiceHandle(scman);
            return retval;
        }

        /// <summary>
        /// Takes a service name, a service display name and the path to the service executable and installs / starts the windows service.
        /// </summary>
        /// <param name="ServiceName">The service name that this service will have</param>
        /// <param name="DisplayName">The display name that this service will have</param>
        /// <param name="FileName">The path to the executable of the service</param>
        public static void InstallAndStart(string ServiceName, string DisplayName, string FileName)
        {
            Install(ServiceName, DisplayName, FileName, true);
        }

        /// <summary>
        /// Takes a service name, a service display name and the path to the service executable and installs / starts the windows service.
        /// </summary>
        /// <param name="ServiceName">The service name that this service will have</param>
        /// <param name="DisplayName">The display name that this service will have</param>
        /// <param name="FileName">The path to the executable of the service</param>
        /// <param name="Start"> Start the Service after Install</param>
        public static void Install(string ServiceName, string DisplayName, string FileName, bool Start)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect |
            ServiceManagerRights.CreateService);
            try
            {
                IntPtr service = OpenService(scman, ServiceName,
                ServiceRights.QueryStatus | ServiceRights.Start);
                if (service == IntPtr.Zero)
                {
                    service = CreateService(scman, ServiceName, DisplayName, ServiceRights.QueryStatus | ServiceRights.Start,
                        SERVICE_WIN32_OWN_PROCESS, ServiceBootFlag.AutoStart, ServiceError.Normal, FileName, null, IntPtr.Zero, null, null, null);
                }
                if (service == IntPtr.Zero)
                {
                    throw new ApplicationException("Failed to install service.");
                }
                if (Start)
                    try
                    {
                        StartService(service);
                    }
                    finally
                    {
                        CloseServiceHandle(service);
                        CloseServiceHandle(scman);
                    }
            }
            finally
            {
                CloseServiceHandle(scman);
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Takes a service name and starts it
        /// </summary>
        /// <param name="Name">The service name</param>
        public static void StartService(string Name)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr hService = OpenService(scman, Name, ServiceRights.QueryStatus |
                ServiceRights.Start);
                if (hService == IntPtr.Zero)
                {
                    throw new ApplicationException("Could not open service.");
                }
                try
                {
                    StartService(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            { 
                CloseServiceHandle(scman);
            }
         }

        /// <summary>
        /// Stops the provided windows service
        /// </summary>
        /// <param name="Name">The service name that will be stopped</param>
        public static void StopService(string Name)
        {
            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr hService = OpenService(scman, Name, ServiceRights.QueryStatus |
                ServiceRights.Stop);
                if (hService == IntPtr.Zero)
                {
                    throw new ApplicationException("Could not open service.");
                }
                try
                {
                    StopService(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
        }

        /// <summary>
        /// Stars the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the windows service</param>
        private static void StartService(IntPtr hService)
        {
            SERVICE_STATUS status = new SERVICE_STATUS();
            StartService(hService, 0, 0);
            WaitForServiceStatus(hService, ServiceState.Starting, ServiceState.Running);
        }

        /// <summary>
        /// Stops the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the windows service</param>
        private static void StopService(IntPtr hService)
        {
            SERVICE_STATUS status = new SERVICE_STATUS();
            ControlService(hService, ServiceControl.Stop, status);
            WaitForServiceStatus(hService, ServiceState.Stopping, ServiceState.Stopped);
        }

        /// <summary>
        /// Takes a service name and returns the <code>ServiceState</code> of the corresponding service
        /// </summary>
        /// <param name="ServiceName">The service name that we will check for his <code>ServiceState</code></param>
        /// <returns>The ServiceState of the service we wanted to check</returns>
        public static ServiceState GetServiceStatus(string ServiceName)
        {
            ServiceState retval;

            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr hService = OpenService(scman, ServiceName, ServiceRights.QueryStatus);
                try
                {
                    if (hService == IntPtr.Zero)
                        retval = ServiceState.NotFound;
                    else 
                        retval = GetServiceStatus(hService);
                }
                finally
                {
                    CloseServiceHandle(hService);
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
            return retval;
        }

        /// <summary>
        /// Gets the service state by using the handle of the provided windows service
        /// </summary>
        /// <param name="hService">The handle to the service</param>
        /// <returns>The <code>ServiceState</code> of the service</returns>
        private static ServiceState GetServiceStatus(IntPtr hService)
        {
            SERVICE_STATUS ssStatus = new SERVICE_STATUS();
            if (QueryServiceStatus(hService, ssStatus) == 0)
            {
                throw new ApplicationException("Failed to query service status.");
            }
            return ssStatus.dwCurrentState;
        }

        /// <summary>
        /// Returns true when the service status has been changes from wait status to desired status
        /// ,this method waits around 10 seconds for this operation.
        /// </summary>
        /// <param name="hService">The handle to the service</param>
        /// <param name="WaitStatus">The current state of the service</param>
        /// <param name="DesiredStatus">The desired state of the service</param>
        /// <returns>bool if the service has successfully changed states within the allowed timeline</returns>
        private static bool WaitForServiceStatus(IntPtr hService, ServiceState
        WaitStatus, ServiceState DesiredStatus)
        {
            SERVICE_STATUS ssStatus = new SERVICE_STATUS();
            int dwOldCheckPoint;
            int dwStartTickCount;

            QueryServiceStatus(hService, ssStatus);
            if (ssStatus.dwCurrentState == DesiredStatus) return true;
            dwStartTickCount = Environment.TickCount;
            dwOldCheckPoint = ssStatus.dwCheckPoint;

            while (ssStatus.dwCurrentState == WaitStatus)
            {
                // Do not wait longer than the wait hint. A good interval is
                // one tenth the wait hint, but no less than 1 second and no
                // more than 10 seconds.

                int dwWaitTime = ssStatus.dwWaitHint / 10;

                if (dwWaitTime < 1000) dwWaitTime = 1000;
                else if (dwWaitTime > 10000) dwWaitTime = 10000;

                System.Threading.Thread.Sleep(dwWaitTime);

                // Check the status again.

                if (QueryServiceStatus(hService, ssStatus) == 0) break;

                if (ssStatus.dwCheckPoint > dwOldCheckPoint)
                {
                    // The service is making progress.
                    dwStartTickCount = Environment.TickCount;
                    dwOldCheckPoint = ssStatus.dwCheckPoint;
                }
                else
                {
                    if (Environment.TickCount - dwStartTickCount > ssStatus.dwWaitHint)
                    {
                        // No progress made within the wait hint
                        break;
                    }
                }
            }
            return (ssStatus.dwCurrentState == DesiredStatus);
        }

        /// <summary>
        /// Opens the service manager
        /// </summary>
        /// <param name="Rights">The service manager rights</param>
        /// <returns>the handle to the service manager</returns>
        private static IntPtr OpenSCManager(ServiceManagerRights Rights)
        {
            IntPtr scman = IntPtr.Zero;
            try
            {
                scman = OpenSCManager(null, null, Rights);
            }
            catch (Exception)
            {
                
                throw new ApplicationException("Could not connect to service control manager.");
            }
            
            return scman;
        }

        public static void SetServiceDescription (string ServiceName, string Description)
        {

            IntPtr scman = OpenSCManager(ServiceManagerRights.Connect);
            try
            {
                IntPtr service = OpenService(scman, ServiceName,
                ServiceRights.ChangeConfig);
                if (service == IntPtr.Zero) 
                    return;
                else 
                {
                    SERVICE_DESCRIPTION sd;
                    sd.lpDescription = Description;
                    //IntPtr lpInfo = Marshal.AllocHGlobal(Marshal.SizeOf(sd));
                    //if (lpInfo == IntPtr.Zero)
                    //{
 
                    //    throw new Exception(String.Format("Unable to allocate memory, error was: 0x{0:X}", Marshal.GetLastWin32Error()));
                    //}

                    if (!ChangeServiceConfig2(service, InfoLevel.Config_Descriptions, ref sd))
                    {
                        //Marshal.FreeHGlobal(lpInfo);
 
                        throw new Exception(String.Format("Error setting service config, error was: 0x{0:X}", Marshal.GetLastWin32Error()));
                    }

 
                    CloseServiceHandle(service);
                return;
                }
            }
            finally
            {
                CloseServiceHandle(scman);
            }
           
        }
         
    }
}


/////<summary>

 

///// Open the SCM Manager to monitor WLan AutoConfi Status

//_hScmHandle = OpenSCManager(null, SERVICES_ACTIVE_DATABASEW, SC_MANAGER_ENUMERATE_SERVICE);


//if (_hScmHandle == IntPtr.Zero)
//throw new Win32Exception();

///// Open WLan Auto Config service handle to monitor its status

//_hAutoConfigHandle = OpenService(_hScmHandle,"Wlansvc", (uint)SERVICE_QUERY_STATUS);

// if (_hAutoConfigHandle == IntPtr.Zero)
//    throw new Win32Exception();
///// Query for WLan Auto Config serivce Status
//SERVICE_STATUS ssStatus = new SERVICE_STATUS() ;

//bool bResult = QueryServiceStatus(_hAutoConfigHandle, ref ssStatus);
//if(!bResult)
//    throw new Win32Exception();


 

///// Register for Wlan Auto Config service stopped status

//_serviceNotify.dwVersion = SERVICE_NOTIFY_STATUS_CHANGE_2;

//_serviceNotify.OnNotify +=new ServiceNotifyHandler(Form1.HandleServiceNotify);
//// _serviceNotify.OnNotify += HandleServiceNotify;


//Int32 nResult = NotifyServiceStatusChange(_hAutoConfigHandle, SERVICE_NOTIFY_STOPPED, ref _serviceNotify);

//if (nResult != ERROR_SUCCESS)
// throw new Win32Exception();

//}

//{

//CloseServiceHandle(_hAutoConfigHandle);
//}

//{

//CloseServiceHandle(_hScmHandle);

