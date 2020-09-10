using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Xml.Serialization;
using System.Security.AccessControl;
using Microsoft.Win32;


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
//    public Int32 ApiPort = 50231;
    public String IPAddress = "127.0.0.1";
    public String[] Sources = new String[6] { "Source 1", "Source 2", "Source 3", "Source 4", "Source 5", "Source 6" };
    public bool PolledWait = false;
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
            XmlSerializer xs = new XmlSerializer(Parameters.GetType());
            FileStream fs = new FileStream(XmlFile, FileMode.Open);
            try
            {
                Parameters = (ConfigParameters)xs.Deserialize(fs);
                fs.Close();
            }

            catch (Exception ex)
            {
                throw;
                //MessageBox.Show("FATAL ERROR" + ex.Message + ex.InnerException.Message);
                //Application.Exit();
                //Application.ExitThread();
            }
        }
        catch (Exception)
        {
            Parameters = new ConfigParameters();
        }
        return Parameters;
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

