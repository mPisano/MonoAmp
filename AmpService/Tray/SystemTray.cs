using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AmpService
{
    class SystemTray
    {
        //Var for context menu to add to icon in system tray
        //Placed here to be accessible from calling form (to add menu items)
        public ContextMenu trayMenu = new ContextMenu();
        //Var for optional property of window state on restore (default normal)
        public FormWindowState restoreWindowState = FormWindowState.Normal;

        public FormWindowState HideWindowState = FormWindowState.Minimized;
        //Var for icon to be in system tray
        public NotifyIcon trayIcon = new NotifyIcon();
        //Var to hold calling app's main form
        private Form mainForm = new Form();
        //Var for determining if app is visible (whether in tray or not)
        private bool visible = true;
        private bool m_IconAllwaysVisible;
        //Constructor
        //Takes three parameters
        //1)The calling main form of application (me)            --mandatory
        //2)The text to display when you mouse over icon in tray --optional
        //3)A custom icon for the tray (defaults to apps icon)   --optional


        public SystemTray(Form theForm, string iconText = "", Icon icon = null, bool IconAllwaysVisible = false)
        {
            //assign passed form to global var
            mainForm = theForm;
            //add two main menu items to traymenu and event handlers
            trayMenu.MenuItems.Add("Restore", restore);
            trayMenu.MenuItems.Add("Close", close);
            //add event handler for passed main form to execute
            mainForm.SizeChanged += execute;
            //add event handler for double clicking the icon in the tray (same as menu item) 
            trayIcon.DoubleClick += restore;
            //properties for trayicon
            //-hide
            //-if icon passed, assign - otherwise assign default icon
            //-assign passed text - if none then will be blank
            //-assign context menu
            {
                if ((icon == null))
                {
                    trayIcon.Icon = mainForm.Icon;
                }
                else
                {
                    trayIcon.Icon = icon;
                }
                m_IconAllwaysVisible = IconAllwaysVisible;
                if (m_IconAllwaysVisible)
                {
                    trayIcon.Visible = true;
                }
                else
                {
                    trayIcon.Visible = false;
                }

                trayIcon.Text = iconText;
                trayIcon.ContextMenu = trayMenu;
            }
        }

        private void restore(System.Object sender, System.EventArgs e)
        {
            //on restore
            //show show in taskbar (this can be buggy in .NET 2.0 - not sure about others
            mainForm.ShowInTaskbar = true;
            //application is now visible
            visible = true;
            //hide the icon in the system tray 
            if (m_IconAllwaysVisible)
            {
                trayIcon.Visible = true;
            }
            else
            {
                trayIcon.Visible = false;
            }
            //restore window to user's defined state (or default normal)
            mainForm.WindowState = restoreWindowState;
            mainForm.Activate();

        }

        private void close(System.Object sender, System.EventArgs e)
        {
            //Hide icon in tray
           // trayIcon.Visible = false; (mpp commented out - Close can be aboted)
            //Close passed main form (should trigger main forms closing event)
            mainForm.Close();
        }

        public bool Visible
        {
            get
            {
                return trayIcon.Visible;
            }
            set
            {
                trayIcon.Visible = value;
            }
        }

        public void ShutOff()
        {
            trayIcon.Visible = false;
            trayIcon.Dispose();

        }
        private void execute(System.Object sender, System.EventArgs e)
        {
            //This fires on form size changine so...
            //If form is visible and is being minimized, then 
            //-hide app in taskbar
            //-show icon in system tray
            if (visible & mainForm.WindowState == FormWindowState.Minimized)
            {
                visible = false;
                mainForm.ShowInTaskbar = false;
                trayIcon.Visible = true;
            }
        }
        public void HideForm()
        {
            mainForm.WindowState = FormWindowState.Minimized;
            visible = false;
            mainForm.ShowInTaskbar = false;
            trayIcon.Visible = true;
        }
        public void FormChanged()
        {
            if (mainForm.WindowState == FormWindowState.Minimized)
            {
                visible = false;
                mainForm.ShowInTaskbar = false;
                trayIcon.Visible = true;
            }
            else
            {
                visible = true;
                mainForm.ShowInTaskbar = true;
                trayIcon.Visible = true;
            }
        }


    }
}
