using System;
using System.Collections.Generic;
using System.Text;

namespace MPRSG6Z
{
    class ConnectedState : AmpState
    {
        // This constructor will create new state taking values from old state
        public ConnectedState(AmpState state)     
            :this(state.DummyCashPresent, state.Amp)
        {
            
        }

        // this constructor will be used by the other one
        public ConnectedState(int amountRemaining, Amp atmBeingUsed)
        {
            this.Amp = atmBeingUsed;
            this.DummyCashPresent = amountRemaining;
        }

        public override string GetNextScreen()
        {
            foreach (KeyPad kp in Amp.Keypads)
            {
                Amp.GetStatus(kp._ID);
            }

            return "";
        }

        private void UpdateState()
        {
            Amp.currentState = new XmitStatusState(this);
        }
    }
}

