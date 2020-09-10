using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MPR_Mixer
{
    static class Program
    {
        static Form myForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            myForm = new frmMain();
            Application.Run(myForm);
 
        }
    }
}