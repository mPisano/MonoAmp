using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Threading;
// using Newtonsoft.Json;


namespace MPRSG6Z
{
    public class KeyPad : INotifyPropertyChanged
    {

        public enum KEYPAD_STATE
        {
            UNKNOWN,
            NEEDSSTATUS,
            IDLE,
        }

        // private KEYPAD_STATE currentState = KEYPAD_STATE.UNKNOWN;

        internal int _Group;
        internal bool _Virtual;
        internal string _Name;
        internal int _ID;
        internal int _PA, _PR, _MU, _DT, _VO, _TR, _BS, _BL, _CH, _LS;
        //  internal int _PA_d, _PR_d, _MU_d, _DT_d, _VO_d, _TR_d, _BS_d, _BL_d, _CH_d, _LS_d;

        internal bool statuspending = false;
        //   internal bool isDirty = false;

        [NonSerialized, SoapIgnore, XmlIgnore]//, JsonIgnore]
        internal System.Threading.SynchronizationContext synchronizationContext;

        [NonSerialized, SoapIgnore, XmlIgnore]//, JsonIgnore]
        public object Tag;

        // This method is called by the Set accessor of each property. 
        // The CallerMemberName attribute that is applied to the optional propertyName 
        // parameter causes the property name of the caller to be substituted as an argument. 
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (this.ID !=0 && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                NotifyPropertyChanged("Name");
                SaveXML();
            }
        }

        public int Group
        {
            get { return _Group; }
            set
            {
                _Group = value;
                NotifyPropertyChanged("Group");
                SaveXML();
            }
        }
        //public object Tag
        //{
        //    get { return _tag; }
        //    set
        //    {
        //        _tag = value;
        //        NotifyPropertyChanged("Tag");
        //    }
        //}

        public KeyPad(int id, bool Virtual)
            : this()
        {
            _Virtual = Virtual;
            _ID = id;
            NotifyPropertyChanged("ID");
            Name = "";

        }
        public KeyPad()
        {
        }
        ~KeyPad()
        {
        }
        public class State : EventArgs
        {
            public KeyPad Keypad { get; private set; }
            public int OldValue { get; private set; }
            public int NewValue { get; private set; }
            public Command Cmd { get; private set; }
            private State() { }
            public State(KeyPad keypad, Command cmd, int oldval, int newval)
            {
                Keypad = keypad;
                Cmd = cmd;
                OldValue = oldval;
                NewValue = newval;
            }
        }

        public delegate void ValueChangedHandler(object sender, State e);
        public event ValueChangedHandler OnValueChanged;
        internal delegate void XmitKeyCmdHandler(object sender, State e);
        internal event XmitKeyCmdHandler OnXmitKeyCmd;

        private void ValueChanged(KeyPad keypad, Command cmd, int oldvalue, int newvalue)
        {
            // Make sure someone is listening to event
            if (OnValueChanged == null) return;
            State args = new State(keypad, cmd, oldvalue, newvalue);
            OnValueChanged(this, args);
        }

        private void XmitKeyCmd(KeyPad keypad, Command cmd, int oldvalue, int newvalue)
        {
            // Make sure someone is listening to event
            if (OnXmitKeyCmd == null) return;
            State args = new State(keypad, cmd, oldvalue, newvalue);
            OnXmitKeyCmd(this, args);
        }

        internal bool Set_Val(int NewValue, Command Cmd, ref int Var, string Property, bool xmit)
        {

  //          if (!_Virtual)
            if (Var != NewValue)
            {
                int OldVal = Var;
                System.Diagnostics.Debug.WriteLine("setval update " + _ID + " " + Cmd + " " + NewValue.ToString() + " was " + OldVal.ToString() + xmit.ToString());
                if (xmit) //ui Changed
                {
                    statuspending = true;
                    Var = NewValue;
                    System.Diagnostics.Debug.WriteLine("Status SET pending");
                    XmitKeyCmd(this, Cmd, OldVal, Var);

                }

                System.Diagnostics.Debug.WriteLine(this._ID + "Change Found - raise Events");
                Var = NewValue;
                ValueChanged(this, Cmd, OldVal, Var);



                if (synchronizationContext == null)
                    NotifyPropertyChanged(Property);
                else
                    synchronizationContext.Post((o) => NotifyPropertyChanged(Property), null);



                if (!xmit)
                {
                    statuspending = false;
                    //if (AnyChanged && _AutoSaveState)  // We have already changed our values, anychange represents a failed change, or a user keypad change
                    //{
                    //    kp.SaveXML(_StateDir);
                    //}
                    //   System.Diagnostics.Debug.WriteLine("Status NOT pending");                     
                }
                return true;
            }
            //       else
            //     {
            //System.Diagnostics.Debug.WriteLine("cLEAR NOT pending");    
            statuspending = false;

            //   }
            return false;
        }
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int PA
        {
            get { return _PA; }
            set { Set_PA(value, true); }
        }
        internal bool Set_PA(int value, bool xmit)
        {
            return Set_Val(value, Command.Public_Address, ref _PA, "PA", xmit);
        }
        public int PR
        {
            get { return _PR; }
            set { Set_PR(value, true); }
        }
        internal bool Set_PR(int value, bool xmit)
        {
            return Set_Val(value, Command.Power, ref _PR, "PR", xmit);
        }
        public int MU
        {
            get { return _MU; }
            set { Set_MU(value, true); }
        }
        internal bool Set_MU(int value, bool xmit)
        {
            return Set_Val(value, Command.Mute, ref _MU, "MU", xmit);
        }
        public int DT
        {
            get { return _DT; }
            set { Set_DT(value, true); }
        }
        internal bool Set_DT(int value, bool xmit)
        {
            return Set_Val(value, Command.Do_Not_Disturb, ref _DT, "DT", xmit);
        }
        public int VO
        {
            get { return _VO; }
            set { Set_VO(value, true); }
        }
        internal bool Set_VO(int value, bool xmit)
        {
            return Set_Val(value, Command.Volume, ref _VO, "VO", xmit);
        }
        public int TR
        {
            get { return _TR; }
            set { Set_TR(value, true); }
        }
        internal bool Set_TR(int value, bool xmit)
        {
            return Set_Val(value, Command.Treble, ref _TR, "TR", xmit);
        }
        public int BS
        {
            get { return _BS; }
            set { Set_BS(value, true); }
        }
        internal bool Set_BS(int value, bool xmit)
        {
            return Set_Val(value, Command.Bass, ref _BS, "BS", xmit);
        }
        public int BL
        {
            get { return _BL; }
            set { Set_BL(value, true); }
        }
        internal bool Set_BL(int value, bool xmit)
        {
            return Set_Val(value, Command.Balance, ref _BL, "BL", xmit);
        }
        public int CH
        {
            get { return _CH; }
            set { Set_CH(value, true); }
        }
        internal bool Set_CH(int value, bool xmit)
        {
            return Set_Val(value, Command.Source, ref _CH, "CH", xmit);
        }
        public int LS
        {
            get { return _LS; }
            set { Set_LS(value, true); }
        }
        internal bool Set_LS(int value, bool xmit)
        {
            return Set_Val(value, Command.Connected, ref _LS, "LS", xmit);
        }

        internal void ParseCmdPacket(int value, string cmd)
        {
            //statuspending = false;
            if (synchronizationContext == null)
                ParseCmdPacketOnUI(value, cmd);
            else
                synchronizationContext.Post((o) => ParseCmdPacketOnUI(value, cmd), null);
        }

        private void ParseCmdPacketOnUI(int value, string cmd)
        {
            System.Diagnostics.Debug.WriteLine("Parse cmd id=" + ID + " cmd=" + cmd + " value=" + value.ToString());
            Set_Value(value, cmd, false);
        }
        public bool Set_Value(int value, string Property)
        {
            return Set_Value(value, Property, true);
        }

        internal bool Set_Value(int value, string Property, bool xmit)
        {
            switch (Property)
            {
                case "PA":
                    return Set_Val(value, Command.Public_Address, ref _PA, "PA", xmit);
                case "PR":
                    return Set_Val(value, Command.Power, ref _PR, "PR", xmit);
                case "MU":
                    return Set_Val(value, Command.Mute, ref _MU, "MU", xmit);
                case "DT":
                    return Set_Val(value, Command.Do_Not_Disturb, ref _DT, "DT", xmit);
                case "VO":
                    return Set_Val(value, Command.Volume, ref _VO, "VO", xmit);
                case "TR":
                    return Set_Val(value, Command.Treble, ref _TR, "TR", xmit);
                case "BS":
                    return Set_Val(value, Command.Bass, ref _BS, "BS", xmit);
                case "BL":
                    return Set_Val(value, Command.Balance, ref _BL, "BL", xmit);
                case "CH":
                    return Set_Val(value, Command.Source, ref _CH, "CH", xmit);
                case "LS":
                    return Set_Val(value, Command.Connected, ref _LS, "LS", xmit);
            }
            return false;
        }


        public bool Set_ValueUp(string Property)
        {
            Int32 min = CommandValues.Min(Property);
            Int32 max = CommandValues.Max(Property);

            switch (Property)
            {
                case "PA":
                    return Set_Val(_PA < max ? _PA + 1 : max, Command.Public_Address, ref _PA, "PA", true);
                case "PR":
                    return Set_Val(_PR < max ? _PR + 1 : min, Command.Power, ref _PR, "PR", true);
                case "MU":
                    return Set_Val(_MU < max ? _MU + 1 : min, Command.Mute, ref _MU, "MU", true);
                case "DT":
                    return Set_Val(_DT < max ? _DT + 1 : min, Command.Do_Not_Disturb, ref _DT, "DT", true);
                case "VO":
                    return Set_Val(_VO < max ? _VO + 1 : max, Command.Volume, ref _VO, "VO", true);
                case "TR":
                    return Set_Val(_TR < max ? _TR + 1 : max, Command.Treble, ref _TR, "TR", true);
                case "BS":
                    return Set_Val(_BS < max ? _BS + 1 : max, Command.Bass, ref _BS, "BS", true);
                case "BL":
                    return Set_Val(_BL < max ? _BL + 1 : max, Command.Balance, ref _BL, "BL", true);
                case "CH":
                    return Set_Val(_CH < max ? _CH + 1 : max, Command.Source, ref _CH, "CH", true);
                case "LS":
                    return Set_Val(_LS < max ? _LS + 1 : max, Command.Connected, ref _LS, "LS", true);
            }
            return false;
        }

        public bool Set_ValueDn(string Property)
        {
            Int32 min = CommandValues.Min(Property);
            Int32 max = CommandValues.Max(Property);

            switch (Property)
            {
                case "PA":
                    return Set_Val(_PA > min ? _PA - 1 : min, Command.Public_Address, ref _PA, "PA", true);
                case "PR":
                    return Set_Val(_PR > min ? _PR - 1 : max, Command.Power, ref _PR, "PR", true);
                case "MU":
                    return Set_Val(_MU > min ? _MU - 1 : max, Command.Mute, ref _MU, "MU", true);
                case "DT":
                    return Set_Val(_DT > min ? _DT - 1 : max, Command.Do_Not_Disturb, ref _DT, "DT", true);
                case "VO":
                    return Set_Val(_VO > min ? _VO - 1 : min, Command.Volume, ref _VO, "VO", true);
                case "TR":
                    return Set_Val(_TR > min ? _TR - 1 : min, Command.Treble, ref _TR, "TR", true);
                case "BS":
                    return Set_Val(_BS > min ? _BS - 1 : min, Command.Bass, ref _BS, "BS", true);
                case "BL":
                    return Set_Val(_BL > min ? _BL - 1 : min, Command.Balance, ref _BL, "BL", true);
                case "CH":
                    return Set_Val(_CH > min ? _CH - 1 : min, Command.Source, ref _CH, "CH", true);
                case "LS":
                    return Set_Val(_LS > min ? _LS - 1 : min, Command.Connected, ref _LS, "LS", true);
            }
            return false;
        }


        internal void ProcessStatusPacket(string Packet)
        {
            //    bool AnyChanged = false;
            statuspending = false;
            if (synchronizationContext == null)
                ParseStatusPacketOnUI(Packet);
            else
                synchronizationContext.Post((o) => ParseStatusPacketOnUI(Packet), null);
        }

        private void ParseStatusPacketOnUI(string Packet)
        {
            bool AnyChanged = false;
            try
            {
                //>IDPAPRMUDTVOTRBSBLCHLS
                //>1100010001060909080101\r
                System.Diagnostics.Debug.WriteLine("Parse Status id=" + ID + " cmd=" + Packet);
                //    statuspending = false;

                AnyChanged = (
                Set_PR(Int32.Parse(Packet.Substring(5, 2)), false) |
                Set_PA(Int32.Parse(Packet.Substring(3, 2)), false) |
                Set_MU(Int32.Parse(Packet.Substring(7, 2)), false) |
                Set_DT(Int32.Parse(Packet.Substring(9, 2)), false) |
                Set_VO(Int32.Parse(Packet.Substring(11, 2)), false) |
                Set_TR(Int32.Parse(Packet.Substring(13, 2)), false) |
                Set_BS(Int32.Parse(Packet.Substring(15, 2)), false) |
                Set_BL(Int32.Parse(Packet.Substring(17, 2)), false) |
                Set_CH(Int32.Parse(Packet.Substring(19, 2)), false) |
                Set_LS(Int32.Parse(Packet.Substring(21, 2)), false));
                //if (AnyChanged && _AutoSaveState)  // We have already changed our values, anychange represents a failed change, or a user keypad change
                //{
                //    kp.SaveXML(_StateDir);
                //}
                Thread.Sleep(0);
            }
            catch (Exception EX)
            {
                System.Diagnostics.Debug.WriteLine("CANT Parse Status id=" + ID + " cmd=" + Packet + " ERROR:" + EX.Message);
                //    throw;
            }
        }



        public void SaveXML()
        {
            try
            {

                // needed to update zone names // hack
                string StateDir = "Current" + System.IO.Path.DirectorySeparatorChar;
                SaveXML(StateDir);
            }
            catch (Exception)
            {

                System.Diagnostics.Debug.WriteLine("filed to fave keypad");
            }

        }



        public void SaveXML(string XmlFile)
        {
            if (this.ID != 0)
            {
                try
                {
                    FileStream s = new FileStream(XmlFile + "Keypad" + this._ID + ".xml", FileMode.Create);
                    XmlSerializer f = new XmlSerializer(this.GetType());
                    f.Serialize(s, this);
                    s.Close();
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Failed to Write XML for Keypad " + this._ID);
                }
            }
        }
    }
}
