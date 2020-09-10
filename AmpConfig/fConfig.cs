using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AmpConfig
{
    public partial class fConfig : Form
    {
        public fConfig()
        {
            InitializeComponent();
        }
        String OrigIpAddress;
    //    Int32 OrigApi;
        Int32 OrigWeb;

        public static bool IsRunningMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        private void fConfig_Load(object sender, EventArgs e)
        {
            Global.CurrentConfig = new Config();
            var ConfigWith = Global.CurrentConfig.Parameters;
            nudAmps.Value = ConfigWith.Units;
            cbDupes.Checked = ConfigWith.RemoveDupes;
            cbPolled.Checked = ConfigWith.PolledWait;
            cbUseWebApi.Checked = ConfigWith.UseWebApi;

            cmbComSelect.Text = ConfigWith.ComPort;
            nudPollms.Value = ConfigWith.PollMS;
          //  nudApiPort.Value = ConfigWith.ApiPort;
           
            nudWebPort.Value = ConfigWith.WebPort;
            szIP.Text = ConfigWith.IPAddress;
            szWebIP.Text = ConfigWith.WebAddress;

          //OrigApi = ConfigWith.ApiPort;
            OrigWeb = ConfigWith.WebPort;
            OrigIpAddress = ConfigWith.IPAddress;

            tbSource1.Text = ConfigWith.Sources[0];
            tbSource2.Text = ConfigWith.Sources[1];
            tbSource3.Text = ConfigWith.Sources[2];
            tbSource4.Text = ConfigWith.Sources[3];
            tbSource5.Text = ConfigWith.Sources[4];
            tbSource6.Text = ConfigWith.Sources[5];


            //List<StringValue> foo = (List<StringValuConfigWith.Sources.ToList <StringValue>;

            //dgSources.DataSource = foo;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            try
            {
                Global.CurrentConfig = new Config();
                var ConfigWith = Global.CurrentConfig.Parameters;
                ConfigWith.ComPort = cmbComSelect.Text;
                ConfigWith.Units = (int)nudAmps.Value;
                ConfigWith.PollMS = Int32.Parse(nudPollms.Text);
                ConfigWith.RemoveDupes = cbDupes.Checked;
                ConfigWith.PolledWait = cbPolled.Checked;
                ConfigWith.UseWebApi = cbUseWebApi.Checked;
             //   ConfigWith.ApiPort = Int32.Parse(nudApiPort.Text);
                ConfigWith.WebPort = Int32.Parse(nudWebPort.Text);
                ConfigWith.IPAddress = szIP.Text;
                ConfigWith.WebAddress = szWebIP.Text;
                cbDupes.Checked = ConfigWith.RemoveDupes;
                cbPolled.Checked = ConfigWith.PolledWait;
                cbUseWebApi.Checked = ConfigWith.UseWebApi;

                ConfigWith.Sources[0] = tbSource1.Text;
                ConfigWith.Sources[1] = tbSource2.Text;
                ConfigWith.Sources[2] = tbSource3.Text;
                ConfigWith.Sources[3] = tbSource4.Text;
                ConfigWith.Sources[4] = tbSource5.Text;
                ConfigWith.Sources[5] = tbSource6.Text;

                Global.CurrentConfig.Save();

                try
                {
                //    bool found = false;
                //    HttpApi nsManager = new HttpApi();

                  //  string RegApiIP = "+";// ConfigWith.IPAddress;
                    if (!string.IsNullOrWhiteSpace(ConfigWith.IPAddress))
                    {
                        string NewUrl = "http://"+ConfigWith.IPAddress +":"+ ConfigWith.WebPort.ToString() + "/";
                        string OldUrl = "http://"+OrigIpAddress+":" + OrigWeb.ToString() + "/";
                        Global.CurrentConfig.OpenPorts(OldUrl, NewUrl);

                        if (ConfigWith.WebPort != 0)
                        {
                            Global.CurrentConfig.BuildJS();
                        }
                    }
                    //    try
                //    {

                //        var x = nsManager.QueryHttpNamespaceAcls();
                //        var z = x[NewUrl];
                //        found = true;
                //    }
                //    catch (Exception)
                //    {
                //        found = false; 
                //    }


                //    if (OldUrl != NewUrl || !found)
                //    {
                //        try
                //        {
                //            var x = nsManager.QueryHttpNamespaceAcls();
                //            var z = x[OldUrl];
                //            nsManager.RemoveHttpHamespaceAcl(OldUrl);
                //        }
                //        catch (Exception)
                //        {
                //        }


                //        SecurityIdentity sid = SecurityIdentity.SecurityIdentityFromName("Everyone");

                //        SecurityDescriptor newSd = new SecurityDescriptor();
                //        newSd.DACL = new AccessControlList();
                //        List<AceRights> supportedRights = new List<AceRights>(new AceRights[] { AceRights.GenericAll, AceRights.GenericExecute, AceRights.GenericRead, AceRights.GenericWrite });
                //        AccessControlEntry newAce = new AccessControlEntry(sid);
                //        newAce.AceType = AceType.AccessAllowed;
                //        newAce.Add(AceRights.GenericExecute);
                //        newSd.DACL.Add(newAce);
                //        nsManager.SetHttpNamespaceAcl(NewUrl, newSd);

                //    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Must be an Administrator to change Http Namespaces");
                }


             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid Parameter");
            }
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbComSelect_DropDown(object sender, EventArgs e)
        {
            cmbComSelect.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cmbComSelect.Items.Add(port);
            }
        }
    }
    public class StringValue
    {
        public StringValue(string s)
        {
            _value = s;
        }
        public string Value { get { return _value; } set { _value = value; } }
        string _value;
    }
}
