using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AmpService
{
    // this class just wraps some Win32 stuffthat we're going to use
    internal class NativeMethods
    {
        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);


        [DllImport("Wininet.dll")]
        private static extern bool InternetGetConnectedState( out int Description, int ReservedValue ) ;




        //private enum Flags : int
        //{
        //    //Local system uses a LAN to connect to the Internet. 
        //    INTERNET_CONNECTION_LAN = 0X2,
        //    //Local system uses a modem to connect to the Internet. 
        //    INTERNET_CONNECTION_MODEM = 0X1,
        //    //Local system uses a proxy server to connect to the Internet. 
        //    INTERNET_CONNECTION_PROXY = 0X4,
        //    //Type Visual Basic 6 code here... 
        //    OFFLINE = 0x20,
        //    //Local system has RAS installed. 
        //    INTERNET_RAS_INSTALLED = 0X10
        //} 

        public static bool IsRasInstalled( ){
        int INTERNET_RAS_INSTALLED = 0X10;
        int flags;
        if (InternetGetConnectedState(out flags, 0))
            return (flags & INTERNET_RAS_INSTALLED) == INTERNET_RAS_INSTALLED;
        else
            return false;
        }
    }
}