
using System;
using Microsoft.AspNet.SignalR;
//using Microsoft.AspNet.SignalR.Owin;
using Microsoft.Owin.Hosting;
//using Microsoft.Owin.Host.HttpListener;
//using Microsoft.AspNet.SignalR.Hosting;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet;
//using System.Web.Http.Owin;
//using System.Web.Http.Properties;

using System.Web.Http;   //for webapi
//using System.Web.Http.SelfHost;  //for webapi
using System.Net.Http; //for webapi

//using Microsoft.Owin.Cors
// Microsoft.Owin.Host.HttpListener.dll  deploy

// This will *ONLY* bind to localhost, if you want to bind to all addresses
// use http://*:8080 to bind to all addresses. 
// See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
// for more information.
using MPRSG6Z;
using Microsoft.Owin.StaticFiles;
using System.IO;

//[assembly: OwinStartup(typeof(SignalRSelfHost.Startup))]

namespace AmpApi
{
    public class AmpState
    {
        public Int32 Units;
        public Int32 KeypadCount;
        public Int32 ResetCount;
        public bool AmpIsRunning;
        public bool AmpIsResponding;
        public String[] Sources;
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Amp amp = new Amp(2, "COM2");
    //        amp.Start();
    //        Control ampr = new Control();
    //        ampr.Start(amp, "localhost", 8080,0);
    //        Console.WriteLine("Server running on");
    //        Console.ReadLine();
    //        ampr.Stop();
    //        ampr = null;
    //        amp.Stop();
    //        amp = null;
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        // This will *ONLY* bind to localhost, if you want to bind to all addresses
    //        // use http://*:8080 to bind to all addresses. 
    //        // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
    //        // for more information.

    //        string url = "http://localhost:8080";

    //        using (WebApp.Start(url))
    //        {
    //            Console.WriteLine("Server running on {0}", url);
    //            Console.ReadLine();
    //        }
    //    }
    //}

    //class Startup
    //{
    //    public void Configuration(IAppBuilder app)
    //    {
    //        app.MapHubs(new HubConfiguration()
    //        { 
    //            EnableCrossDomain = true
    //        });

    //        //app.UseCors(CorsOptions.AllowAll);
    //        //app.MapSignalR();
    //    }
    //}



    public static class MyAmp
    {
        //public delegate void ValueChangedHandlerl(object sender, Amp.State e);
        //public event ValueChangedHandlerl OnValueChangedl;

        // private readonly static Lazy<Amp> _instance ;
        public static Amp _instance;
        public static IDisposable webapp;
        //  public static HttpSelfHostServer server = null;
        public static void OnValueChangedr(object sender, Amp.State e)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver _amp_OnValueChanged " + e.Property + " " + e.NewValue.ToString() + " was " + e.NewValue.ToString());
            GlobalHost.ConnectionManager.GetHubContext<MyHub>().Clients.All.valueChanged(e);
        }
        //ValueChangedHandlerl 
        //        public static void Set(Amp amp)
        //        {
        //             _instance = amp;
        //        }
        ////        private readonly static Lazy<Amp> _instance = new Lazy<Amp>(() => new Amp(2, "COM2"));
        //        public static Amp Instance
        //        {
        //            get { return _instance;} //.Value
        //        }


    }

    public class MyHub : Hub
    {

        private readonly Amp _amp;

        public MyHub() : this(MyAmp._instance)
        {
            System.Diagnostics.Debug.WriteLine("HUB Creating ");
         }

        public MyHub(Amp amp)
        {
            if (amp == null)
                System.Diagnostics.Debug.WriteLine("SIGNALR HUB AMP is NULL!!!!");

            System.Diagnostics.Debug.WriteLine("HUB Created ");
            _amp = amp;
            //    _amp.OnValueChanged += _amp_OnValueChanged;
        }

        //void _amp_OnValueChanged(object sender, Amp.State e)
        //{
        //    System.Diagnostics.Debug.WriteLine("Httpserver _amp_OnValueChanged " + e.Property + " " + e.NewValue.ToString() + " was " + e.NewValue.ToString());
        //    Clients.All.valueChanged(e);
        //}

        public int getkeypadCount()
        {
            System.Diagnostics.Debug.WriteLine("Httpserver HUB getkeypadCount");
            return _amp.Keypads.Count;
        }
        public int getUnitCount()
        {
            System.Diagnostics.Debug.WriteLine("Httpserver HUB getUnitCount");
            return _amp.Units;
        }

        public String[] getSources()
        {
            System.Diagnostics.Debug.WriteLine("Httpserver HUB getSources");
            return _amp.Sources;
        }

        public void send(string name, string message)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver HUB send ");
            try
            {
                System.Diagnostics.Debug.WriteLine("Httpserver HUB send " + name + " " + message);
                Clients.All.addMessage(name, message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Httpserver HUB getSources" + ex.Message);
            }
        }

        public AmpState getAmpState()
        {
            System.Diagnostics.Debug.WriteLine("Httpserver getAmpState");
            AmpState ampState = new AmpState();

            ampState.Units = _amp.Units;
            ampState.KeypadCount = _amp.Keypads.Count;
            ampState.AmpIsRunning = _amp.AmpIsRunning;
            ampState.AmpIsResponding = _amp.AmpIsResponding;
            ampState.ResetCount = _amp.ResetCount;
            ampState.Sources = _amp.Sources;
            return ampState;
        }

        public KeyPad getKeyPad(int chan)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver getKeyPad " + chan.ToString());

            KeyPad k = _amp.Keypads[chan];
            return k;
        }

        public bool sendUnitCommand(int Unit, int Channel, Command Command, int value)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver sendUnitCommand");
            try
            {
                System.Diagnostics.Debug.WriteLine("Httpserver sendUnitCommand Chan" + Channel.ToString() + " : " + Command.ToString() + " " + value.ToString());
                _amp.SendCommand(Unit, Channel, Command, value);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Httpserver HUB sendUnitCommand" + ex.Message);

            }
            return true;
        }


        public bool sendCommand(Command Command, int value)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver sendCommand");

            try
            {
                System.Diagnostics.Debug.WriteLine("Httpserver sendCommand " + Command.ToString() + " " + value.ToString());
                _amp.SendCommand(Command, value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Httpserver HUB sendUnitCommand" + ex.Message);

            }
            return true;
        }

        public bool setCommand(int Channel, Command Command, int value)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver setCommand");

            System.Diagnostics.Debug.WriteLine("Httpserver setCommand Chan " + Channel.ToString() + " : " + Command.ToString() + " " + value.ToString());

            string cmd = StringEnum.GetCodeValue(Command);
            return _amp.Keypads[Channel].Set_Value(value, cmd);
        }
        public bool setProperty(int Channel, string Property, int value)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver setProperty");
            System.Diagnostics.Debug.WriteLine("Httpserver setProperty Chan " + Channel.ToString() + " : " + Property + " " + value.ToString());
            return _amp.Keypads[Channel].Set_Value(value, Property);
        }

        public bool commandUp(int Channel, Command Command)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver commandUp Chan " + Channel.ToString() + " : " + Command.ToString());
            string cmd = StringEnum.GetCodeValue(Command);
            return _amp.Keypads[Channel].Set_ValueUp(cmd);
        }
        public bool propertyUp(int Channel, string Property)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver propertyUp Chan " + Channel.ToString() + " : " + Property);
            return _amp.Keypads[Channel].Set_ValueUp(Property);
        }

        public bool commandDn(int Channel, Command Command)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver commandDn Chan " + Channel.ToString() + " : " + Command.ToString());
            string cmd = StringEnum.GetCodeValue(Command);
            return _amp.Keypads[Channel].Set_ValueDn(cmd);
        }
        public bool propertyDn(int Channel, string Property)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver propertyDn Chan " + Channel.ToString() + " : " + Property);
            return _amp.Keypads[Channel].Set_ValueDn(Property);
        }

    }


    //public class CustomHeaderHandler : System.Net.Http.DelegatingHandler
    //{
    //    protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
    //    {
    //        return base.SendAsync(request, cancellationToken)
    //            .ContinueWith((task) =>
    //            {
    //                HttpResponseMessage response = task.Result;
    //                response.Headers.Add("Access-Control-Allow-Origin", "*");
    //                return response;
    //            });
    //    }
    //}

    public class HttpServer
    {

        public HttpServer()
        {
            System.Diagnostics.Debug.WriteLine("HttpServer Created");

        }

        public void Start(Amp amp, String IPAddress, Int32 WebPort)
        {
            System.Diagnostics.Debug.WriteLine("HttpServer Start on " + IPAddress + ":" + WebPort.ToString());

            if (WebPort == 0)
                System.Diagnostics.Debug.WriteLine("WEBAPI Disabled, port is 0");
            else
            {
                if (amp == null)
                    System.Diagnostics.Debug.WriteLine("WEBAPI AMP is NULL!!!!");
                else
                {
                    MyAmp._instance = amp;
                    amp.OnValueChanged += MyAmp.OnValueChangedr;
                }

                string url = "http://" + IPAddress + ":" + WebPort.ToString();
                try
                {
                    System.Diagnostics.Debug.WriteLine("WEBAPI starting on " + url);
                    //HubConfiguration h = new HubConfiguration();
                    //h.EnableCrossDomain = true;
                    //MyAmp.webapp = WebApp.Start(url, builder => builder.UseFileServer(options).MapHubs(h));

                    //var config = new HttpConfiguration();
                    //System.Diagnostics.Debug.WriteLine("WEBAPI Phaase 1");
                    //config.Routes.MapHttpRoute(
                    //    name: "DefaultApi",
                    //    routeTemplate: "api/{controller}/{id}",
                    //    defaults: new { id = RouteParameter.Optional }
                    //);
                    //System.Diagnostics.Debug.WriteLine("WEBAPI Phaase 2");
                    ////          config.Formatters.Remove(config.Formatters.XmlFormatter);
                    ////         config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
                    //config.Properties["AMP"] = amp;
                    //System.Diagnostics.Debug.WriteLine("WEBAPI Phaase 3");

                    MyAmp.webapp = WebApp.Start<Startup>(url);//, builder => builder.UseFileServer(options).MapSignalR(h).UseWebApi(config));
                    System.Diagnostics.Debug.WriteLine("WEBAPI started");
                    //   WebApp.Start(url);//, builder => builder.UseFileServer(enableDirectoryBrowsing:true));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("WEBAPI ERROR" + ex.Message);
                    throw;
                }
            }

            //  if (ApiPort != 0)
            //{
            //string ApiUrl = "http://" + IPAddress + ":" + ApiPort.ToString() + "/";
            //HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(ApiUrl);
            //config.MessageHandlers.Add(new CustomHeaderHandler());
            //config.Routes.MapHttpRoute(
            //    "API Default", "api/{controller}/{id}",
            //    new { id = RouteParameter.Optional });
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            //// Create server
            //MyAmp.server = new HttpSelfHostServer(config);
            //MyAmp.server.Configuration.Properties["AMP"] = amp;

            //// Start listening
            //MyAmp.server.OpenAsync().Wait();

            // }

        }
        public void Stop()
        {
            if (MyAmp.webapp != null)
            {
                 try
                {
                    MyAmp.webapp.Dispose();
                }
                catch (Exception)
                {
                }

                MyAmp.webapp = null;
            }
            //if (MyAmp.server != null)
            //{
            //    MyAmp.server.CloseAsync();
            //    MyAmp.server.Dispose();
            //    MyAmp.server = null;
            //}
            if (MyAmp._instance != null)
            {
                MyAmp._instance.OnValueChanged -= MyAmp.OnValueChangedr;
                MyAmp._instance = null;
            }
            System.Diagnostics.Debug.WriteLine("Httpserver Stopping");

        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            System.Diagnostics.Debug.WriteLine("WEBAPI:Configuration");

            try
            {
                System.Diagnostics.Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());

                //var config = new HttpConfiguration();
                //config.Routes.MapHttpRoute(
                //    name: "DefaultApi",
                //    routeTemplate: "api/{controller}/{id}",
                //    defaults: new { id = RouteParameter.Optional }
                //);
                //builder.UseWebApi(config);

                var config = new HttpConfiguration();
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );
                //   config.Formatters.Remove(config.Formatters.XmlFormatter);
                // config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
                config.Properties["AMP"] = MyAmp._instance;


                string exeFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                System.Diagnostics.Debug.WriteLine("WEBAPI:Configuration - exeFolder " + exeFolder);
                string webFolder = Path.Combine(exeFolder, "Web");
                System.Diagnostics.Debug.WriteLine("WEBAPI:Configuration - webFolder " + webFolder);
                var fileSystem = new Microsoft.Owin.FileSystems.PhysicalFileSystem(webFolder);
                var options = new FileServerOptions
                {
                    //   EnableDirectoryBrowsing = true,
                    FileSystem = fileSystem
                };
                System.Diagnostics.Debug.WriteLine("WEBAPI:Configuration - HubConfiguration");
                HubConfiguration h = new HubConfiguration();

                builder.UseFileServer(options);
                builder.MapSignalR(h);
                builder.UseWebApi(config);

                //HttpConfiguration httpConfiguration = new HttpConfiguration();
                //httpConfiguration.Routes.MapHttpRoute(
                //    "API Default", "api/{controller}/{id}",
                //    new { id = RouteParameter.Optional });
                //builder.UseWebApi(httpConfiguration);
                System.Diagnostics.Debug.WriteLine("WEBAPI:Configuration - Completed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("WEBAPI CONFIG ERROR" + ex.Message);
                throw;
            }
        }
    }
    public class AmpStateController : ApiController
    {
        // http://localhost:50232/api/Amp
        public AmpState Get()
        {

            System.Diagnostics.Debug.WriteLine("Httpserver AmpState.get");

            Amp amp = (Amp)this.Configuration.Properties["AMP"];
            if (amp == null)
                System.Diagnostics.Debug.WriteLine("AmpState.get AMP IS NULL!!!!");

            System.Diagnostics.Debug.WriteLine("AmpState.get: Running" + amp.AmpIsRunning.ToString() + " Reset:" + amp.ResetCount.ToString());

            AmpState ampState = new AmpState();

            ampState.Units = amp.Units;
            ampState.KeypadCount = amp.Keypads.Count;
            ampState.AmpIsRunning = amp.AmpIsRunning;
            ampState.AmpIsResponding = amp.AmpIsResponding;
            ampState.ResetCount = amp.ResetCount;
            ampState.Sources = amp.Sources;
            System.Diagnostics.Debug.WriteLine("Httpserver AmpState.get.complete");
            return ampState;

        }
    }


    public class KeyPadController : ApiController
    {
        // http://localhost:50232/api/keypad?chan=2
        public KeyPad Get(int chan)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver Keypad.get " + chan.ToString());

            Amp amp = (Amp)this.Configuration.Properties["AMP"];
            KeyPad k = amp.Keypads[chan];
            return k;

        }
    }

    public class CommandController : ApiController
    {
        // http://localhost:50232/api/Command?unit=1&channel=1&command=Volume&value=10
        public bool Get(int Unit, int Channel, Command Command, int value)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver Command.get Chan" + Channel.ToString() + " : " + Command.ToString() + " " + value.ToString());

            Amp amp = (Amp)this.Configuration.Properties["AMP"];
            amp.SendCommand(Unit, Channel, Command, value);
            return true;
        }

        public bool Get(Command Command, int value)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver Command.get " + Command.ToString() + " " + value.ToString());

            Amp amp = (Amp)this.Configuration.Properties["AMP"];
            amp.SendCommand(Command, value);
            return true;
        }
    }

    public class ValueController : ApiController
    {
        // http://localhost:50232/api/Value?Channel=1&command=Volume&value=10
        // http://localhost:50232/api/Value?Channel=1&Property=VO&value=10
        public bool Get(int Channel, Command Command, int value)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver Value.get Chan " + Channel.ToString() + " : " + Command.ToString() + " " + value.ToString());
            Amp amp = (Amp)this.Configuration.Properties["AMP"];
            string cmd = StringEnum.GetCodeValue(Command);
            return amp.Keypads[Channel].Set_Value(value, cmd);
        }
        public bool Get(int Channel, string Property, int value)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver Value.get Chan " + Channel.ToString() + " : " + Property + " " + value.ToString());

            Amp amp = (Amp)this.Configuration.Properties["AMP"];
            return amp.Keypads[Channel].Set_Value(value, Property);
        }
    }

    public class ValueUpController : ApiController
    {
        // http://localhost:50232/api/ValueUp?Channel=1&Command=Volume
        // http://localhost:50232/api/ValueUp?Channel=1&Property=VO
        public bool Get(int Channel, Command Command)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver ValueUp.get Chan " + Channel.ToString() + " : " + Command.ToString());

            Amp amp = (Amp)this.Configuration.Properties["AMP"];
            string cmd = StringEnum.GetCodeValue(Command);
            return amp.Keypads[Channel].Set_ValueUp(cmd);
        }
        public bool Get(int Channel, string Property)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver ValueUp.get Chan " + Channel.ToString() + " : " + Property);

            Amp amp = (Amp)this.Configuration.Properties["AMP"];
            return amp.Keypads[Channel].Set_ValueUp(Property);
        }
    }

    public class ValueDnController : ApiController
    {
        // http://localhost:50232/api/ValueDn?Channel=1&command=Volume
        // http://localhost:50232/api/ValueDn?Channel=1&Property=VO
        public bool Get(int Channel, Command Command)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver ValueDn.get Chan " + Channel.ToString() + " : " + Command.ToString());

            Amp amp = (Amp)this.Configuration.Properties["AMP"];
            string cmd = StringEnum.GetCodeValue(Command);
            return amp.Keypads[Channel].Set_ValueDn(cmd);
        }
        public bool Get(int Channel, string Property)
        {
            System.Diagnostics.Debug.WriteLine("Httpserver ValueDn.get Chan " + Channel.ToString() + " : " + Property);

            Amp amp = (Amp)this.Configuration.Properties["AMP"];
            return amp.Keypads[Channel].Set_ValueDn(Property);
        }
    }



}