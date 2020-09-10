using System;
using System.Collections.Generic;
using System.Text;

namespace MPRSG6Z
{
    class DisconnectedState : AmpState
    {
        // This constructor will create new state taking values from old state
        public DisconnectedState(AmpState state)     
            :this(state.Amp)
        {       
        }

        // this constructor will be used by the other one
        public DisconnectedState(Amp ampBeingUsed)
        {
            this.Amp = ampBeingUsed;
        //    this.DummyCashPresent = amountRemaining;
        }

        public override string GetNextScreen()
        {
            System.Diagnostics.Debug.WriteLine("DisconnectedState");
            try
            {
                System.Diagnostics.Debug.WriteLine("Reestting Amp");
  
                Amp.Reset(0);

                this.Amp.SendCommand("",true);
                this.Amp.waitForMessage(1000, 1);
                string cmd = this.Amp.GetNextCmd();
                if (cmd == "") // would of been #
                {
                    Amp.AmpIsResponding = true;
                    UpdateState();
                    return "Connected";
                }
            }
            catch (Exception)
            {
                System.Threading.Thread.Sleep(1000);
                Amp.AmpIsResponding = false;
                if (Amp._port.IsOpen)
                    Amp._port.Close();
                return "";
            }
            return "";
        
        }

        private void UpdateState()
        {
            Amp.currentState = new GetFullStatusState(this);
        }
    }
}

