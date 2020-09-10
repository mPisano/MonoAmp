using System;
using System.Collections.Generic;
using System.Text;

namespace MPRSG6Z
{
    class ShutdownState : AmpState
    {
        // This constructor will create new state taking values from old state
        public ShutdownState(AmpState state)     
            :this(state.Amp)
        {
            
        }

        // this constructor will be used by the other one
        public ShutdownState(Amp ampBeingUsed)
        {
            this.Amp = ampBeingUsed;
         //   this.DummyCashPresent = amountRemaining;
        }

        public override string GetNextScreen()
        {            
            //Console.WriteLine("Please Enter your Pin");
            //string userInput = Console.ReadLine();

            //// lets check with the dummy pin
            //if (userInput.Trim() == "1234")
            //{
            //    UpdateState();
            //    return "Enter the Amount to Withdraw";
            //}

            //// Show only message and no change in state
            //return "Invalid PIN";
            return "SHUTDOWN";
        }

        private void UpdateState()
        {
         //   Amp.currentState = new CardValidatedState(this);
        }
    }
}

