using System;
using System.Collections.Generic;
using System.Text;

namespace MPRSG6Z
{
    class GetOneStatusState : AmpState
    {
        // This constructor will create new state taking values from old state
        public GetOneStatusState(AmpState state)
            : this(state.Amp)
        {

        }
        int chan;

        // this constructor will be used by the other one
        public GetOneStatusState(Amp ampBeingUsed)
        {
            this.Amp = ampBeingUsed;
        //    this.chan = chan;
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
                    for (int i = 1; i <= 6; i++)
                    {
                        System.Diagnostics.Debug.WriteLine("GetOneStatusState");
                        this.Amp.SendCommand("?" + u.ToString() + i.ToString(), true);

                        if (this.Amp.waitForMessage(1000, 1))
                        {
                            string ack1 = this.Amp.GetNextCmd();
                            if (ack1 == "?" + u.ToString() + i.ToString())
                            {
                                if (this.Amp.waitForMessage(1000, 1))
                                {
                                    string cmd = this.Amp.GetNextCmd();
                                    if (cmd.Substring(0, 1) == ">" && cmd.Length == 24)
                                    {

                                        int unit = Int32.Parse(cmd.Substring(1, 1));
                                        int chan = Int32.Parse(cmd.Substring(2, 1));
                                
                                            int id = ((unit - 1) * 6) + (chan - 1);

                                            KeyPad kp = this.Amp.Keypads[id];
                                            if (!kp.statuspending)
                                                kp.ProcessStatusPacket(cmd);
                                            else
                                                System.Diagnostics.Debug.WriteLine("GetOneStatusState - IGNORING STATUS PENDING " + kp.ID);
                                    
                                    }


                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine("GetOneStatusState - Incorrect cmd recieved " + cmd);
                                        this.Amp.Reset();
                                    }
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("GetOneStatusState - Incorrect ACK  for ?10 :" + ack1);
                                this.Amp.Reset();
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("GetOneStatusState - IGNORING Sendmessage Timeout");
                            this.Amp.Reset();

                        }
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

