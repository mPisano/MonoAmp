using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmpApi;
using MPRSG6Z;

using Options;
namespace AmpLoader
{
    class Program
    {
        public static bool IsRunningMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("AMP Loader (C)2014 Mike Pisano - Mpisano1@aol.com");
            Global.CurrentConfig = new Config();

            bool show_help = false;
            Dictionary<string, string> names = new Dictionary<string, string>();
            Dictionary<string, string> sources = new Dictionary<string, string>();
            Int32 port = -1;
            string com = null;
            string ip = null;
            string lp = null;
            Int32 amps = -1;
            Int32 ms = -1;
            string polled = null;
            string webapi = null;
            string dupes = null;


            var p = new OptionSet() {
                { "com=", "The {COM Port} to transmit data on. (ie COM1 or /dev/ttyUSB0, /dev/ttyAMA0)",
                   (v) => com = v },  
                { "name=", "the {NAME} UNIT+ZONE:Name to Changee",
                   (v,w) => names.Add (v,w) },
                { "source=", "the {SOURCE} (1 to 6):Name to Change",
                   (v,w) => sources.Add (v,w) },
                { "port=", "the {PORT} number for WebApp to listen",
                   (int v) => port = v },
                { "lp=", "the {Listen IP address} (default to *)",
                   (v) => lp = v },
                { "ip=", "the {IP Address} to bind to (Typically the IP of your NIC. type IPCONFIG at a CMD prompt to find)",
                   (v) => ip = v },      
                { "amps=", "the number of Amps {Units} Connected 1-3",
                   (int v) => amps = v },      
                { "poll=", "How often to {Poll in ms} the Amp for updated values.",
                   (int v) => ms = v },  
                { "pw+|pw-", "How to check for amp updates. true is Poller Wait, false is Event Driven. Mono has problems with events",
                   (string v) => polled = v},  
                { "rd-|rd+", "If multiple same outbound commands are queued, only transmit last value (helpfull with volume ramp)",
                   (string v) => dupes = v },  
                { "wa+|wa-", "use webapi, false will use SignalR for Bidirectinal Communications between apps, but requies a more powerfull CPU. webapi is reccomended for the Raspberry PI",
                   (string v) => webapi = v },  
                { "help",  "Shows this message", 
              v => show_help = v != null },
            };

            try
            {
                p.Parse(args);

            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            if (names.Count() > 0)
            {

                MPRSG6Z.Amp amp1 = new MPRSG6Z.Amp(Global.CurrentConfig.Parameters);
                amp1.LoadKeypads();
                foreach (var item in names)
                {
                    string k = item.Key;
                    string n = item.Value;
                    Console.WriteLine("Setting Keypad " + k + " name to " + n);
                    amp1.KeyPadID(k).Name = n;
                }
                amp1 = null;
            }

            if (sources.Count() > 0)
            {

                foreach (var item in sources)
                {
                    int k = Int32.Parse(item.Key);
                    string n = item.Value;
                    Console.WriteLine("Setting source " + k + " name to " + n);
                    Global.CurrentConfig.Parameters.Sources[k - 1] = n;
                }
            }

            if (!String.IsNullOrEmpty(com))
            {
                Console.WriteLine("Setting COM Port to: " + com);
                Global.CurrentConfig.Parameters.ComPort = com;
            }

            if (!String.IsNullOrEmpty(lp))
            {
                Console.WriteLine("Setting Listen IP Address to: " + lp);
                Global.CurrentConfig.Parameters.IPAddress = lp;
            }

            if (!String.IsNullOrEmpty(ip))
            {
                Console.WriteLine("Setting Web IP Address to: " + ip);
                Global.CurrentConfig.Parameters.WebAddress = ip;
            }

            if (port != -1)
            {
                Console.WriteLine("Setting Web Port to: " + port.ToString());
                Global.CurrentConfig.Parameters.WebPort = port;
            }

            if (!String.IsNullOrEmpty(polled))
            {
                bool val = polled == "pw+" ? true : false;
                Console.WriteLine("Setting Polled Wait to: " + val.ToString());
                Global.CurrentConfig.Parameters.PolledWait = val;
            }

            if (!String.IsNullOrEmpty(dupes))
            {
                bool val = dupes == "rd+" ? true : false;
                Console.WriteLine("Setting Remove Dupes to: " + val.ToString());
                Global.CurrentConfig.Parameters.RemoveDupes = val;
            }

            if (!String.IsNullOrEmpty(webapi))
            {
                bool val = webapi == "wa+" ? true : false;
                Console.WriteLine("Setting WebApi to: " + val.ToString());
                Global.CurrentConfig.Parameters.UseWebApi = val;
            }

            if (amps >= 1 && amps <= 3)
            {
                Console.WriteLine("Setting AMP Units " + amps.ToString());
                Global.CurrentConfig.Parameters.Units = amps;
            }

            if (ms != -1)
            {
                Console.WriteLine("Setting MS Delay to " + ms.ToString());
                Global.CurrentConfig.Parameters.PollMS = ms;
            }

            if (show_help)
            {
                ShowHelp(p);
            }

            if (args.Count() >= 1)
            {
                Global.CurrentConfig.Save();
                return;
            }

            Global.CurrentConfig = new Config();
            Global.CurrentConfig.BuildJS();
            //  string ipline = "var ip = 'http://" + Global.CurrentConfig.Parameters.IPAddress + ":" + Global.CurrentConfig.Parameters.ApiPort.ToString() + "';";
            //string Webipline = "var webip = 'http://" + Global.CurrentConfig.Parameters.WebAddress + ":" + Global.CurrentConfig.Parameters.WebPort.ToString() + "';";

            //string[] lines = { "// This File was Auto Created By Amp Loader " + DateTime.Now, Webipline, "// Do not Modify, Your Changes will be lost" };
            //System.IO.File.WriteAllLines(@"Web" + System.IO.Path.DirectorySeparatorChar + "host.js", lines);

            Console.WriteLine("Loading AMP on " + Global.CurrentConfig.Parameters.ComPort);

            MPRSG6Z.Amp amp = new MPRSG6Z.Amp(Global.CurrentConfig.Parameters);
            ////amp.PolledWait = Global.CurrentConfig.Parameters.PolledWait;
            ////amp.Units = Global.CurrentConfig.Parameters.Units;
            ////amp.ComPort = Global.CurrentConfig.Parameters.ComPort;
            ////amp.PollMS = Global.CurrentConfig.Parameters.PollMS;
            ////amp.QueueDupeElimination = Global.CurrentConfig.Parameters.RemoveDupes;
            ////amp.Sources = Global.CurrentConfig.Parameters.Sources;
            Console.WriteLine("Starting " + Global.CurrentConfig.Parameters.Units.ToString() + " Amps");
            amp.Start();

            Console.WriteLine("Creating Webserver on " + Global.CurrentConfig.Parameters.IPAddress + ":" + Global.CurrentConfig.Parameters.WebPort.ToString());
            AmpApi.HttpServer ampserver = new AmpApi.HttpServer();
            Console.WriteLine("Starting Server");
            ampserver.Start(amp, Global.CurrentConfig.Parameters.IPAddress, Global.CurrentConfig.Parameters.WebPort);//, Global.CurrentConfig.Parameters.ApiPort);

            try
            {
                Console.WriteLine("Listening");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
                throw;
            }

            ampserver.Stop();
            ampserver = null;
            amp.Stop(true);
            amp = null;

        }
        static void ShowHelp(OptionSet p)
        {
            //    Console.WriteLine("Usage: greet [OPTIONS]+ message");
            //    Console.WriteLine("Greet a list of individuals with an optional message.");
            //     Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }

    }
}
