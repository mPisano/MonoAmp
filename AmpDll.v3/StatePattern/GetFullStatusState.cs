using System;
using System.Collections.Generic;
using System.Text;

namespace MPRSG6Z
{
    class GetFullStatusState : AmpState
    {
        // This constructor will create new state taking values from old state
        public GetFullStatusState(AmpState state)
            : this(state.Amp)
        {

        }

        // this constructor will be used by the other one
        public GetFullStatusState(Amp ampBeingUsed)
        {
            this.Amp = ampBeingUsed;
       //     this.DummyCashPresent = amountRemaining;
        }

        public override string GetNextScreen()
        {

                try
            {
                if ( Amp._txq.Count != 0)
                {
                    UpdateState();
                    return "";
                }

                for (int u = 1; u <= Amp._units; u++)
                {
                    
                System.Diagnostics.Debug.WriteLine("GetFullStatusState");
                 this.Amp.SendCommand("?"+u.ToString()+"0",true);
              //  this.Amp.WaitCommand("?10", 1000);
                 if (this.Amp.waitForMessage(1000, 1))
                 {
                     string ack1 = this.Amp.GetNextCmd();
                     if (ack1 == "?" + u.ToString() + "0")
                     {
                         if (this.Amp.waitForMessage(1000, 6))

                             for (int i = 0; i < 6; i++)
                             {


                                 string cmd = this.Amp.GetNextCmd();
                                 if (cmd.Substring(0, 1) == ">" && cmd.Length == 24)
                                 {

                                     int unit = Int32.Parse(cmd.Substring(1, 1));
                                     int chan = Int32.Parse(cmd.Substring(2, 1));
                                     {
                                         int id = ((unit - 1) * 6) + (chan - 1);

                                         KeyPad kp = this.Amp.Keypads[id];
                                         if (!kp.statuspending)
                                             kp.ProcessStatusPacket(cmd);
                                         else
                                             System.Diagnostics.Debug.WriteLine("GetFullStatusState - IGNORING STATUS PENDING " + kp.ID);
                                     }
                                 }


                                 else
                                 {
                                     System.Diagnostics.Debug.WriteLine("GetFullStatusState - Incorrect cmd recieved " + cmd);
                                     this.Amp.Reset();
                                 }
                             }
                     }
                     else
                     {
                         System.Diagnostics.Debug.WriteLine("GetFullStatusState - Incorrect ACK  for ?10 :" + ack1);
                         this.Amp.Reset();
                     }
                 }
                 else
                 {
                     System.Diagnostics.Debug.WriteLine("GetFullStatusState - IGNORING Sendmessage Timeout");
                     this.Amp.Reset();

                 }
                }

                    UpdateState();
                
                }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION - " + ex.Message );
            }
            return "";

        }

        private void UpdateState()
        {
         
            Amp.currentState = new XmitCmdState(this);
        }
    }
}

