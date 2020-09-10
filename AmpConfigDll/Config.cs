using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Xml.Serialization;
using System.Security.AccessControl;
using Microsoft.Win32;

using HttpNamespaceManager.Lib.AccessControl;
using HttpNamespaceManager.Lib;

public class Global
{
    public static string[] Sources;
    public static Config CurrentConfig = new Config();

}
[Serializable()]

public class ConfigParameters
{
    public string ComPort = "";
    public bool RemoveDupes = true;
    public int PollMS = 1000;
    public int Units = 1;
    public Int32 WebPort = 50230;
    public String WebAddress = "127.0.0.1";
    public String IPAddress = "*";
    public String[] Sources = new String[6] { "Source 1", "Source 2", "Source 3", "Source 4", "Source 5", "Source 6" };
    public bool PolledWait = false;
    public bool UseWebApi = false;
}

public class Config
{
    public ConfigParameters Parameters = new ConfigParameters();

    //  public string DefaultFile = Application.ProductName + ".XML";
    public string DefaultFile = "Amp_Config.XML";
    #region "Helper"
    public Config()
    {
        Load();
    }

    public ConfigParameters Load()
    {
        return LoadXML();
    }
    public void Save()
    {
        SaveXML();
    }

    public ConfigParameters LoadXML(string XmlFile)
    {
        DefaultFile = XmlFile;
        try
        {
            if (System.IO.File.Exists(DefaultFile))
            {
                XmlSerializer xs = new XmlSerializer(Parameters.GetType());
                FileStream fs = new FileStream(XmlFile, FileMode.Open);
                try
                {
                    Parameters = (ConfigParameters)xs.Deserialize(fs);
                    fs.Close();
                }

                catch (Exception ex)
                {
                    //MessageBox.Show("FATAL ERROR" + ex.Message + ex.InnerException.Message);
                    //Application.Exit();
                    //Application.ExitThread();
                }
            }
            else
            {
                Parameters = new ConfigParameters();
                if (Type.GetType("Mono.Runtime") != null)
                {
                    Parameters.ComPort = @"/dev/tty/USB0";
                    Parameters.UseWebApi = true;
                    Parameters.PolledWait = true;
                }
                else
                {
                    Parameters.ComPort = @"COM1";
                    Parameters.UseWebApi = false;
                    Parameters.PolledWait = false;
                }
                SaveXML();
            }
        }
        catch (Exception)
        {
            Parameters = new ConfigParameters();
        }
        return Parameters;
    }


    public void BuildJS()
    {
        string WebURL = "http://" + Parameters.WebAddress + ":" + Parameters.WebPort.ToString() + "/";

        string Webipline = "var webip = '" + WebURL + "';"; //'http://" + ConfigWith.IPAddress + ":" + ConfigWith.WebPort.ToString() + "';";
        string WebApiline = "var usewebapi = " + (Parameters.UseWebApi ? "true" : "false") + ";"; //'http://" + ConfigWith.IPAddress + ":" + ConfigWith.WebPort.ToString() + "';";

        string[] lines = { "// This File was Auto Created By Amp Config " + DateTime.Now, Webipline, WebApiline, "// Do not Modify, Your Changes will be lost" };
        System.IO.File.WriteAllLines(@"Web" + System.IO.Path.DirectorySeparatorChar + "host.js", lines);

        //       this.Close();
    }
    public void OpenPorts(string OldUrl, string NewUrl)
    {
        //  string RegWebIP = ConfigWith.IPAddress;
        try
        {
            bool found = false;
            HttpApi nsManager = new HttpApi();

            //  string NewUrl = "http://" + RegWebIP +":" + ConfigWith.WebPort.ToString() + "/";
            //  string OldUrl = "http://" + RegWebIP +":" + OrigWeb.ToString() + "/";

            try
            {
                var x = nsManager.QueryHttpNamespaceAcls();
                var z = x[NewUrl];
                found = true;
            }
            catch (Exception)
            {
                found = false;
            }


            if (OldUrl != NewUrl || !found)
            {
                try
                {
                    var x = nsManager.QueryHttpNamespaceAcls();
                    var z = x[OldUrl];
                    nsManager.RemoveHttpHamespaceAcl(OldUrl);
                }
                catch (Exception)
                {
                }


                SecurityIdentity sid = SecurityIdentity.SecurityIdentityFromName("Everyone");

                SecurityDescriptor newSd = new SecurityDescriptor();
                newSd.DACL = new AccessControlList();
                List<AceRights> supportedRights = new List<AceRights>(new AceRights[] { AceRights.GenericAll, AceRights.GenericExecute, AceRights.GenericRead, AceRights.GenericWrite });
                AccessControlEntry newAce = new AccessControlEntry(sid);
                newAce.AceType = HttpNamespaceManager.Lib.AccessControl.AceType.AccessAllowed;
                newAce.Add(AceRights.GenericExecute);
                newSd.DACL.Add(newAce);
                nsManager.SetHttpNamespaceAcl(NewUrl, newSd);

            }
        }
        catch (Exception)
        {
            //        MessageBox.Show("Must be an Administrator to change Http Namespaces");
        }

    }

    public void SaveXML(string XmlFile)
    {
        DefaultFile = XmlFile;
        FileStream s = new FileStream(XmlFile, FileMode.Create);
        XmlSerializer f = new XmlSerializer(Parameters.GetType());
        f.Serialize(s, Parameters);
        s.Close();
    }

    public void SaveXML()
    {
        SaveXML(DefaultFile);
    }

    public ConfigParameters LoadXML()
    {
        return LoadXML(DefaultFile);
    }

    #endregion

}

