using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.ComponentModel;
using System.Collections.Concurrent;
using System.Linq;

namespace MPRSG6Z
{

    public class ControlerStatusChangedEventArgs : System.EventArgs
    {
        public readonly int State;
        public readonly string Status;
        public ControlerStatusChangedEventArgs(int state, string status)
        {
            State = state;
            Status = status;
        }
    }


    public class Amp
    {

        public class State : EventArgs
        {
            public int Channel { get; private set; }
            public String Property { get; private set; }
            public KeyPad Keypad { get; private set; }
            public int OldValue { get; private set; }
            public int NewValue { get; private set; }
            public Command Cmd { get; private set; }
            public String Command { get; private set; }
            public String Source { get; private set; }
            private State() { }

            public State(KeyPad keypad, Command cmd, int oldval, int newval)
            {
                Channel = getKeypadPtr(keypad.ID.ToString());
                Command = cmd.ToString();
                Property = StringEnum.GetCodeValue(cmd);
                Keypad = keypad;
                Cmd = cmd;
                OldValue = oldval;
                NewValue = newval;
            }
        }
        public AmpState currentState = null;
        public bool AmpIsRunning = false;
        public bool AmpIsResponding = false;
        public delegate void ValueChangedHandler(object sender, State e);
        public event ValueChangedHandler OnValueChanged;
        private void ValueChanged(KeyPad keypad, Command cmd, int oldvalue, int newvalue)
        {
            System.Diagnostics.Debug.WriteLine(keypad.ID.ToString() + " cmd" + cmd.ToString() + " old:" + oldvalue.ToString() + " new" + newvalue.ToString());
            // Make sure someone is listening to event
            if (OnValueChanged == null) return;

            State args = new State(keypad, cmd, oldvalue, newvalue);
            OnValueChanged(this, args);
            if (_AutoSaveState) //!(_port.IsOpen) &&
                keypad.SaveXML(_StateDir);
        }

        public event EventHandler<ControlerStatusChangedEventArgs> StatusChanged;
        protected virtual void OnStatusChanged(ControlerStatusChangedEventArgs e)
        {
            if (StatusChanged != null) StatusChanged(this, e);
        }




        //private void XmitTimerCallback(Object o)
        //{
        //   System.Diagnostics.Debug.WriteLine("XmitTimerCallback:" + _bc.Count.ToString());
        //    _tmXmit.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

        //    try
        //    {
        //        if (_port != null && _port.IsOpen)
        //        {
        //            if (!string.IsNullOrWhiteSpace(LastFailedCommand))
        //            {

        //                System.Diagnostics.Debug.WriteLine("Amp Sending Commnad " + LastFailedCommand);
        //                try
        //                {
        //                    _port.WriteLine(LastFailedCommand);
        //                    LastFailedCommand = "";
        //                    System.Diagnostics.Debug.WriteLine("Command Resent:" + LastFailedCommand);// (e.Argument);  

        //                }
        //                catch (Exception ex)
        //                {
        //                    System.Diagnostics.Debug.WriteLine("Retry Port EXception!!! " + ex.Message + "Still DID NOT SEND" + LastFailedCommand);// (e.Argument);  
        //                    _tmXmit.Change(0, System.Threading.Timeout.Infinite);
        //                }

        //            }
        //            string c;
        //            while (_port != null && _port.IsOpen && _bc.TryDequeue(out c))
        //            {
        //                bool Found = false;
        //                if (QueueDupeElimination)
        //                {
        //                    string[] cmds = _bc.ToArray();
        //                    int cnt = cmds.Length;
        //                    if (cnt > 0)
        //                    {
        //                        for (int i = 0; i < cnt; i++)
        //                        {
        //                            if (cmds[i] == c || ((c.Length >= 5 && cmds[i].Length >= 5) && cmds[i].Substring(0, 5) == c.Substring(0, 5)))
        //                            {
        //                                Found = true;
        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //                if (!Found)
        //                {
        //                    System.Diagnostics.Debug.WriteLine("Amp Sending Commnad " + c);
        //                    try
        //                    {
        //                        _port.WriteLine(c);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        System.Diagnostics.Debug.WriteLine("Port EXception!!! "+ex.Message+" DID NOT SEND" + c);// (e.Argument);  
        //                        LastFailedCommand = c; //Hack Yeild thread and retry
        //                        _tmXmit.Change(0, System.Threading.Timeout.Infinite);
        //                    }

        //                    // This is called on the worker thread
        //                    System.Diagnostics.Debug.WriteLine(Found ? "Skipped " : "Sent " + c);// (e.Argument);       )
        //                   // Thread.Sleep(AfterSendSleepMs);
        //                }
        //             }
        //        }
        //        else
        //            System.Diagnostics.Debug.WriteLine("Port Closed:" + _bc.Count.ToString() + " Pending");// (e.Argument);   
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Port Exception:" + ex.Message);
        //    }
        //    GC.Collect();
        //}

        static int getKeypadPtr(string ID)
        {
            int unit = Int32.Parse(ID.Substring(0, 1));
            int kindex = Int32.Parse(ID.Substring(1, 1));
            return ((unit - 1) * 6) + kindex - 1;
        }

        static int getKeypadPtr(Int32 unit, Int32 chan)
        {
            return ((unit - 1) * 6) + chan - 1;
        }

        [NonSerialized]
        private System.Threading.SynchronizationContext _synchronizationContext;
        public BindingList<KeyPad> Keypads = new BindingList<KeyPad>();

        private string _StateDir = "Current" + System.IO.Path.DirectorySeparatorChar;
        internal int _units;
        //    internal string _Directory;

        internal ConcurrentQueue<string> _txq = new ConcurrentQueue<string>();
        private ConcurrentQueue<string> _rxq = new ConcurrentQueue<string>();

        internal System.IO.Ports.SerialPort _port;
        private string _buffer;
        public bool PolledWait = false;
        public bool QueueDupeElimination = true;
        public int ServiceState = -1;
        private Int32 _pollms = 1000;
        private String _ComPort = "";
        private static String[] _Sources = new String[6] { "Source 1", "Source 2", "Source 3", "Source 4", "Source 5", "Source 6" };
        private BackgroundWorker bw;
        public Int32 ResetCount = 0;

        public KeyPad KeyPadID(string ID)
        {
            return Keypads[getKeypadPtr(ID)];
        }
        public KeyPad KeyPadID(Int32 Unit, Int32 Chan)
        {
            return Keypads[getKeypadPtr(Unit, Chan)];
        }

        public void SetConfig(ConfigParameters Config)
        {
            this.PolledWait = Config.PolledWait;
            this.Units = Config.Units;
            this.ComPort = Config.ComPort;
            this.PollMS = Config.PollMS;
            this.QueueDupeElimination = Config.RemoveDupes;
            this.Sources = Config.Sources;
        }

        public void Start(ConfigParameters Config)
        {
            SetConfig(Config);
            Start();
        }
        public void Start()
        {
            if (bw == null)
            {

                if (!System.IO.Directory.Exists(_StateDir))
                    System.IO.Directory.CreateDirectory(_StateDir);

                OnStatusChanged(new ControlerStatusChangedEventArgs(1, "Starting"));
                LoadKeypads();

                bw = new BackgroundWorker();
                bw.WorkerSupportsCancellation = true;
                bw.DoWork += bw_DoWork;
                bw.RunWorkerCompleted += bw_RunWorkerCompleted;
                AmpIsRunning = true;
                bw.RunWorkerAsync();
                ServiceState = 1;
                OnStatusChanged(new ControlerStatusChangedEventArgs(1, "Running"));
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnStatusChanged(new ControlerStatusChangedEventArgs(1, "Stopping"));
            foreach (KeyPad kp in Keypads)
            {
                kp.OnValueChanged -= kp_OnValueChanged;
                kp.OnXmitKeyCmd -= kp_XmitKeyCmd;
                //kp.OnRequestAmpChanState -= kp_OnRequestAmpChanState;
            }
            _synchronizationContext = null;
            Keypads.Clear();

            AmpIsRunning = false;
            AmpIsResponding = false;
            bw = null;
            ServiceState = -1;
            OnStatusChanged(new ControlerStatusChangedEventArgs(-1, "Stopped"));
            Close();
        }
        public void Stop(bool Wait = true)
        {
            if (bw != null)
                bw.CancelAsync();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_port == null)
            {
                _port = new SerialPort();
                _port.DtrEnable = true;
                _port.RtsEnable = true;
                _port.BaudRate = 9600;
                _port.StopBits = StopBits.One;
                _port.Parity = Parity.None;
                _port.DataBits = 8;

                _port.Handshake = Handshake.None;//.none;  RequestToSend;
                _port.NewLine = "\r\n";
                if (!PolledWait)
                {
                    _port.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.port_DataReceived);
                    //      _port.PinChanged += new SerialPinChangedEventHandler(this.port_PinChanged);
                    _port.ErrorReceived += _port_ErrorReceived;
                }
            }

            //           long t = 0;
            while (!bw.CancellationPending)
            {
                try
                {
                    if (!_port.IsOpen)
                        Reset(0);

                    if (_port.IsOpen)
                        currentState.GetNextScreen();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception ERROR!!!!! " + ex.Message);
                    Thread.Sleep(1000);
                }
            }
            if (_port != null)
            {
                if (!PolledWait)
                {
                    _port.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(this.port_DataReceived);
                    //         _port.PinChanged -= new SerialPinChangedEventHandler(this.port_PinChanged);
                    _port.ErrorReceived -= _port_ErrorReceived;
                }
                _port.Close();
                _port.Dispose();
            }
            e.Cancel = true;
        }


        internal System.Threading.SynchronizationContext SynchronizationContext
        {
            get { return _synchronizationContext; }
            set { _synchronizationContext = value; }
        }


        public int Units
        {
            get { return _units; }
            set { _units = value; }
        }

        public int PollMS
        {
            get { return _pollms; }
            set { _pollms = value; }
        }


        public string ComPort
        {
            get { return _ComPort; }
            set { _ComPort = value; }
        }

        public string[] Sources
        {
            get { return _Sources; }
            set { _Sources = value; }
        }

        public Amp(System.Threading.SynchronizationContext synchronizationContext, ConfigParameters Config)
        {
            _synchronizationContext = synchronizationContext;
            SetConfig(Config);
        }


        public Amp(System.Threading.SynchronizationContext synchronizationContext)
        {
            _synchronizationContext = synchronizationContext;
        }

        public Amp(int units)
        {
            _units = units;
        }
        public Amp(int units, string PortName)
        {
            _ComPort = PortName;
            _units = units;
            //     _Directory = Directory;
        }

        public Amp(int units, string PortName, System.Threading.SynchronizationContext synchronizationContext)
        {
            _synchronizationContext = synchronizationContext;
            _units = units;
            _ComPort = PortName;
            //_Directory = Directory;
        }

        public Amp(ConfigParameters Config)
        {
            SetConfig(Config);
        }

        public Amp()
        {
        }


        //public Amp(ConfigParameters p)
        //{
        //    _ComPort = p.ComPort;
        //    _units = p.Units;
        //    _Sources = p.Sources;
        //    _pollms  = p.PollMS;
        //    QueueDupeElimination = p.RemoveDupes;
        //}
        public void LoadKeypads()
        {
            //          this.PortName = PortName;
            //   _tmXmit = new Timer(XmitTimerCallback, null, System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);





            for (int unit = 1; unit <= _units; unit++)
            {
                for (int chan = 1; chan <= 6; chan++)
                {
                    int id = (unit * 10) + chan;
                    KeyPad kp = LoadOrCreateKeypad(id, false);
                    Keypads.Add(kp);
                    kp.synchronizationContext = this._synchronizationContext;
                    kp.OnValueChanged += kp_OnValueChanged;
                    kp.OnXmitKeyCmd += kp_XmitKeyCmd;
                    //kp.OnRequestAmpChanState += kp_OnRequestAmpChanState;
                    if (kp.Group != 0)
                    {
                      if (Keypads.SingleOrDefault(p => p.ID == kp.Group + 100) == null)
                            { 
                            KeyPad vkp = LoadOrCreateKeypad(kp.Group + 100, true);
                            vkp.Group = kp.Group;
                            Keypads.Add(vkp);
                            vkp.synchronizationContext = this._synchronizationContext;
                            vkp.OnValueChanged += kp_OnValueChanged;
                            vkp.OnXmitKeyCmd += kp_XmitKeyCmd;
                        }
                    }
                }
            }

        }

        private KeyPad LoadOrCreateKeypad(int id, bool Virtual)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(new KeyPad().GetType());
            string KeyType = Virtual ? "Group" : "Keypad";
            KeyPad kp;// = new KeyPad();
            if (!(_StateDir == null))
            {
                string XmlFile = _StateDir + KeyType + id.ToString() + ".xml";
                try
                {
                    System.IO.FileStream fs = new System.IO.FileStream(XmlFile, System.IO.FileMode.Open);
                    //  kp = new KeyPad();
                    kp = (KeyPad)xs.Deserialize(fs);
                    if (string.IsNullOrWhiteSpace(kp.Name))
                        kp.Name = KeyType + kp.ID.ToString();

                    kp.statuspending = false;
                    fs.Close();
                }
                catch (Exception)
                {
                    kp = new KeyPad(id, Virtual);
                    kp.Name = KeyType + " " + kp.ID.ToString();
                }
            }
            else
            {
                kp = new KeyPad(id, Virtual);
                kp.Name = "Keypad " + kp.ID.ToString();
            }
            xs = null;
            return kp;
        }


        ~Amp()
        {

            //      _tmXmit.Dispose();
        }

        void kp_OnValueChanged(object sender, KeyPad.State e)
        {
            ValueChanged(e.Keypad, e.Cmd, e.OldValue, e.NewValue);
        }

        void kp_XmitKeyCmd(object sender, KeyPad.State e)
        {
            SendCommand(e.Keypad.ID, e.Cmd, e.NewValue, false);
            //if (_AutoSaveState) //!(_port.IsOpen) &&
            //    e.Keypad.SaveXML(_StateDir);  // GUI changes when offline
        }
        bool _AutoSaveState = false;
        public bool AutoSaveState
        {
            get { return _AutoSaveState; }
            set { _AutoSaveState = value; }
        }

        public void SaveState()
        {
            SaveState(_StateDir);
        }
        public void SaveState(string Directory)
        {
            foreach (KeyPad kp in Keypads)
            {
                kp.SaveXML(Directory);
            }
        }

        void port_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Amp port_PinChanged:" + e.EventType.ToString());
        }

        void _port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Amp _port_ErrorReceived:" + e.EventType.ToString());
        }
        private void Close()
        {
            //      _tmXmit.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            if (AutoSaveState)
                SaveState(_StateDir);
            //      _bc = null;
            Thread.Sleep(1000);
            System.Diagnostics.Debug.WriteLine("Amp Closing port");
            if (_synchronizationContext == null)
                ClosePortOnCorrectThread();
            else
                _synchronizationContext.Send((o) => ClosePortOnCorrectThread(), null);
        }
        public string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }
        public void GetStatus(int unit = 0)
        {
            if (unit != 0)
                SendCommand("?" + unit.ToString() + "0");
            else
                for (int u = 1; u <= _units; u++)
                {
                    SendCommand("?" + u.ToString() + "0");
                }
        }



        public void SendCommand(Command Command, int value, bool QueueImmidately = false)
        {
            SendCommand(0, 0, Command, value, QueueImmidately);
        }

        public void SendCommand(int ID, Command Command, int value, bool SendImmidately = false)
        {
            if (ID == 0)
                SendCommand(0, 0, Command, value, SendImmidately);
            else
            {
                int Unit = (int)(ID / 10);
                int Channel = (int)(ID % 10);
                SendCommand(Unit, Channel, Command, value, SendImmidately);
            }
        }
        public void SendCommand(int Unit, int Channel, Command Command, int value, bool SendImmidately = false)
        {
            string uid = Unit.ToString().PadLeft(1, '0');
            string chan = Channel.ToString().PadLeft(1, '0');
            string val = value.ToString().PadLeft(2, '0');
            string cmd = StringEnum.GetCodeValue(Command);

            if (Unit == 0 && Channel == 0)
            {
                for (int i = 1; i <= _units; i++)
                {
                    uid = i.ToString().PadLeft(1, '0');
                    SendCommand("<" + uid + chan + cmd + val, SendImmidately);
                }
            }
            else
                SendCommand("<" + uid + chan + cmd + val, SendImmidately);
        }
        public void SendCommand(string Unit, string Channel, string Command, string value, bool SendImmidately = false)
        {
            value = value.PadLeft(2, '0');

            if (Unit == "0" && Channel == "0")
            {
                for (int i = 1; i <= _units; i++)
                {
                    string uid = i.ToString().PadLeft(1, '0');
                    SendCommand("<" + uid + Channel + Command + value, SendImmidately);
                }
            }
            else

                SendCommand("<" + Unit + Channel + Command + value, SendImmidately);


        }
        public void WaitCommand(string cmd, int ms)
        {
        }
        public void SendCommand(string RawCommnad, bool SendImmidately = false)
        {
            if (SendImmidately)
            {
                System.Diagnostics.Debug.WriteLine("Amp Sending Commnad: " + RawCommnad);
                try
                {
                    System.Diagnostics.Debug.WriteLine("RAW SEND Message: " + RawCommnad);
                    _port.WriteLine(RawCommnad);
                    System.Diagnostics.Debug.WriteLine(RawCommnad);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Port EXception!!! " + ex.Message + " DID NOT SEND" + RawCommnad);// (e.Argument);  
                }
            }
            else
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("Queueing Message: " + RawCommnad);
                    // _port.WriteLine(RawCommnad);
                    _txq.Enqueue(RawCommnad);
                    //if (QueueImmidately)
                    //   _tmXmit.Change(0, System.Threading.Timeout.Infinite);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Port EXception!!! " + ex.Message + " DID NOT SEND" + RawCommnad);// (e.Argument);  
                }
                //_bc.Enqueue(RawCommnad);
                //System.Diagnostics.Debug.WriteLine("Amp Queuing Commnad " + RawCommnad);
                //if (QueueImmidately)
                //    _tmXmit.Change(0, System.Threading.Timeout.Infinite);



            }
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                string InputData = _port.ReadExisting();
                System.Diagnostics.Debug.WriteLine("port_DataReceived  RAW:" + e.EventType.ToString() + ":" + InputData);
                ProcessData(InputData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("port_DataReceived ERROR:" + ex.Message);
                Reset();
                //throw;
            }

        }

        public void ReadData()
        {
            System.Diagnostics.Debug.WriteLine("Reading Data");
            Thread.Sleep(50); //let port settle
            byte tmpByte;
            string rxString = "";
            try
            {
                tmpByte = (byte)_port.ReadByte();
                while (tmpByte != 255)
                {
                    // System.Diagnostics.Debug.WriteLine("recieved"+ tmpByte.ToString());
                    rxString += ((char)tmpByte);
                    tmpByte = (byte)_port.ReadByte();
                }
            }
            catch (Exception ex)
            {

            }
            System.Diagnostics.Debug.WriteLine("recieved" + rxString);


            if (!string.IsNullOrEmpty(rxString))

                ProcessData(rxString);
        }

        private void ProcessData(string InputData)
        {
            try
            {
                if (InputData != String.Empty)
                {
                    _buffer = _buffer + InputData;
                    System.Diagnostics.Debug.WriteLine(InputData);
                    while (_buffer.IndexOf("\r\n#") >= 0)
                    //    while (_buffer.IndexOf("\r\n") >= 0 && _buffer.IndexOf("#", _buffer.IndexOf("\r\n")) >= 0)
                    {
                        //      int first = _buffer.IndexOf("\r\n") + 2;
                        //      int last = _buffer.IndexOf("#", first)+1;
                        //      int first = 0;
                        int last = _buffer.IndexOf("\r\n#") + 3;
                        //  if (first !=2 ){
                        //    System.Diagnostics.Debug.WriteLine("LOST:" + _buffer.Substring(0,first));
                        // }
                        if (last >= 0)
                        {
                            //string str2 = _buffer.Substring(first, last - first);
                            string str2 = _buffer.Substring(0, last - 3);

                            System.Diagnostics.Debug.WriteLine("REC CMD:" + str2);
                            if (str2.Length == 24 || str2.Length == 0 || str2.Length == 3 || str2.Length == 7 || str2.Length == 5 || str2.Length == 8)
                                _rxq.Enqueue(str2);
                            else
                                System.Diagnostics.Debug.WriteLine("port_DataReceived bad length:" + str2);
                            string x = _buffer;
                            _buffer = _buffer.Substring(last);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ProcessData ERROR:" + ex.Message);
                Reset();
                //throw;
            }

        }


        //       if (str2.Length > 0)
        //       {
        //           char commandtype = str2[0];

        //           switch (commandtype)
        //           {
        //               case '?':
        //                   break;
        //               case '>':

        //                   if (str2.Length == 24)
        //                   {
        //                       int id = getKeypadPtr(str2.Substring(1, 2));
        //                       System.Diagnostics.Debug.WriteLine("Port parse " + str2);
        //                       try
        //                       {
        //                           KeyPad kp = Keypads[id];
        //                           if (_synchronizationContext == null)
        //                               ParsePacketOnUI(kp, str2);
        //                           else
        //                               _synchronizationContext.Send((o) => ParsePacketOnUI(kp, str2), null);

        //                       }
        //                       catch (Exception)
        //                       {
        //                           throw;
        //                       }
        //                   }
        //                   else
        //                   {
        //                       System.Diagnostics.Debug.WriteLine("UNKNOWN COMMAND " + str2);
        //                   }
        //                   break;
        //               case '<': // This is more of an ACK - doesnot mean command actually worked, ie vol cant be changed if muted, but ack will say it did
        //                        //  dont process return values to GUI / messes up sliders / assume correct until status comes back
        //                   if (str2.Length == 7)
        //                   {
        //                       int id = getKeypadPtr(str2.Substring(1, 2));
        //                       if (id == -1)
        //                       {
        //                           int unit = Int32.Parse(str2.Substring(1, 1));
        //                           for (int i = 1; i <= 6; i++)
        //                           {
        //                               id = ((unit -1) * 6) + (i - 1);
        //                               KeyPad kp = Keypads[id];
        //                    //           kp.GetStatus(0);
        //                           }

        //                       }
        //                       else
        //                       {
        //                           KeyPad kp = Keypads[id];
        ////                           kp.GetStatus(0);
        //                       }
        //                   }
        //               break;
        //               default:
        //                   System.Diagnostics.Debug.WriteLine("UNKNOWN COMMAND "+str2);
        //                   breatimek;
        //           }
        //       }
        //       else
        //           break;
        //   }
        // }
        // }


        internal void Reset(int Drainms = 2000)
        {
            ResetCount++;
            try
            {
                if (_port.IsOpen)
                {
                    System.Diagnostics.Debug.WriteLine("Resetting - Port is Open, Closing");
                    _port.Close();
                    while (waitForMessage(Drainms, 1))
                    {
                        string dis = GetNextCmd();
                        System.Diagnostics.Debug.WriteLine("Discarding " + dis);
                    }
                }
                System.Diagnostics.Debug.WriteLine("Opening Port " + ComPort);
                _buffer = "";
                _port.PortName = ComPort;
                _port.ReadTimeout = 1;// 250;  //mono
                _port.Open();
                Thread.Sleep(2000);
                currentState = new DisconnectedState(this);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Reset - Can't Open Port " + ComPort + "- " + ex.Message);
                Thread.Sleep(1000);
            }
        }

        internal bool TXQContains(string cmd)
        {
            bool Found = false;
            string[] cmds = _txq.ToArray();
            int cnt = cmds.Length;
            if (cnt > 0)
            {
                for (int i = 0; i < cnt; i++)
                {
                    if (cmds[i].Substring(0, 5) == cmd.Substring(0, 5))
                    {
                        Found = true;
                        break;
                    }
                }
            }
            return Found;
        }

        internal bool waitForMessage(int ms, int msgs)
        {
            System.Diagnostics.Debug.WriteLine("waitForMessage pending count" + _rxq.Count.ToString());

            DateTime dt = DateTime.Now.AddMilliseconds(ms);
            while (dt >= DateTime.Now)    // sw.ElapsedMilliseconds < Amp.Pollms)
            {
                if (PolledWait)
                    ReadData(); //Mono  

                if (_rxq.Count >= msgs)
                {
                    System.Diagnostics.Debug.WriteLine("waitForMessage FOUND");
                    return true;
                }
                Thread.Sleep(1);
            }
            System.Diagnostics.Debug.WriteLine("waitForMessage TIMEOUT");
            return false;
        }

        internal string GetNextCmd()
        {
            string Cmd;
            try
            {
                _rxq.TryDequeue(out Cmd);
            }
            catch (Exception ex)
            {
                Cmd = "";
                System.Diagnostics.Debug.WriteLine("GetNextCmd Failed:" + ex.Message);
            }
            return Cmd;
        }

        private void ClosePortOnCorrectThread()
        {
            if (!PolledWait)
                _port.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(this.port_DataReceived);
            _port.Close();
        }

        private void ParseCmdPacketOnUI(KeyPad kp, int value, string cmd)
        {
            System.Diagnostics.Debug.WriteLine("Parse cmd id=" + kp.ID + " cmd=" + cmd + " value=" + value.ToString());
            kp.Set_Value(value, cmd, false);
        }
        private void ParsePacketOnUI(KeyPad kp, string str2)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Parse Status id=" + kp.ID + " cmd=" + str2);
                // bool AnyChanged = 
                kp.ProcessStatusPacket(str2);
                //  if (AnyChanged && _AutoSaveState)  // We have already changed our values, anychange represents a failed change, or a user keypad change
                //  {
                //      kp.SaveXML(_StateDir);
                //  }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
