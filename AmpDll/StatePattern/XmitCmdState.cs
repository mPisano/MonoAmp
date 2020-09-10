using System;
using System.Collections.Generic;
using System.Text;

namespace MPRSG6Z
{
    class XmitCmdState : AmpState
    {
        // This constructor will create new state taking values from old state
        public XmitCmdState(AmpState state)     
            :this(state.Amp)
        {
            
        }

        // this constructor will be used by the other one
        public XmitCmdState(Amp ampBeingUsed)
        {
            this.Amp = ampBeingUsed;
      //      this.DummyCashPresent = amountRemaining;
        }

        public override string GetNextScreen()
        {
            try
            {
                if (this.Amp._txq.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("XMT Q Contains:" + this.Amp._txq.Count.ToString());
                try
                {

                    string cmd;
                    this.Amp._txq.TryDequeue(out cmd);


                    if (this.Amp.QueueDupeElimination && this.Amp.TXQContains(cmd)) //<ucxx##'
                        System.Diagnostics.Debug.WriteLine("PERFORMANCE - SKIPPING DUPE");
                    else
                    {


                        this.Amp.SendCommand(cmd,true);    // <ucxx##
                        if (this.Amp.waitForMessage(2000, 1))
                        {
                            string ack1 = this.Amp.GetNextCmd();
                            if (ack1 == cmd) //ack
                            {
                                int u = Int32.Parse(cmd.Substring(1, 1));
                                int c = Int32.Parse(cmd.Substring(2, 1));                       
                                string scmd = "?" + cmd.Substring(1, 4);
                                if (false) //c == 0)
                                {
                                    this.Amp.SendCommand(scmd, false);
                                    UpdateState();
                                    return "ok";
                                }

                                else
                                {
                                    this.Amp.SendCommand(scmd, true);

                                    if (this.Amp.waitForMessage(2000, 1)) //? & >
                                    {
                                        string ack2 = this.Amp.GetNextCmd();
                                        if (ack2 == scmd) //ack
                                        {
                                            int rpack =  c==0 ? 6:1;
                                            for (int i = 0; i < rpack; i++)
	                                		{
	                                       if (this.Amp.waitForMessage(2000, 1)) // ?
                                           {
                                            string stat = this.Amp.GetNextCmd(); // >
                                            try
                                            {
                                                int unit = Int32.Parse(stat.Substring(1, 1));
                                                int chan = Int32.Parse(stat.Substring(2, 1));
                                                int id = ((unit - 1) * 6) + (chan - 1);
                                                KeyPad kp = this.Amp.Keypads[id];

                                                if (cmd.Substring(3, 2) != stat.Substring(3, 2)) // MU PA... XX
                                                {
                                                    System.Diagnostics.Debug.WriteLine("FATAL " + cmd.Substring(1, 4) + " != " + stat.Substring(1, 4));
                                                    this.Amp.Reset();
                                                }
                                                else
                                                    if (this.Amp.TXQContains("<" + stat.Substring(1, 4))) //<ucxx##'
                                                        System.Diagnostics.Debug.WriteLine("Skipping " + stat.Substring(1, 5) + " Another in Q");
                                                    else
                                                    {
                                                        int kval = Int32.Parse(stat.Substring(5, 2));
                                                        string kcmd = stat.Substring(3, 2);
                                                         kp.ParseCmdPacket(kval, kcmd);
    
                                                    }
    
                                            }
                                            catch (Exception)
                                            {
                                                System.Diagnostics.Debug.WriteLine("Bad Packet");
                                                this.Amp.Reset();
                                            }
                                        }
                                           else
                                           {
                                               System.Diagnostics.Debug.WriteLine("Status timeout");
                                               this.Amp.Reset();
                                           }
                                        }
                                    }

                                        else
                                        {
                                            System.Diagnostics.Debug.WriteLine("missed status");
                                            this.Amp.Reset();
                                        }
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine("Ack timeout");
                                        this.Amp.Reset();
                                    }
                                }
                            }
                            else
                                System.Diagnostics.Debug.WriteLine("missed cmd");
                        }
                    }
               
                }

                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine("xmit amp error");
                    this.Amp.Reset();
                }

                 
                
            }

            //foreach (KeyPad K in this.Amp.Keypads)
            //{
            //    try
            //    {
            //        System.Diagnostics.Debug.WriteLine("checking " + K.ID);
            //    if (K.isDirty)
            //    {
            //        System.Diagnostics.Debug.WriteLine("Keyboard Dirty: " + K.ID);
            //    }
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
 
            //}

            }
            catch (Exception)
            {

                throw;
            }
            UpdateState();

                return "ok";
        }

        private void UpdateState()
        {
            if (Amp.PollMS > 0)
            {
          //      int count = 0;
       //          System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        //        sw.Start();
                System.Diagnostics.Debug.WriteLine("Sleeping "+Amp.PollMS.ToString());
                DateTime dt = DateTime.Now.AddMilliseconds(Amp.PollMS);
                while (this.Amp._txq.Count == 0 && dt >= DateTime.Now)    // sw.ElapsedMilliseconds < Amp.Pollms)
                {
                    System.Threading.Thread.Sleep(1);
                }
          //      sw.Stop();
            }
            if (this.Amp._txq.Count == 0 && Amp.PollMS > 0)
            {
                Amp.currentState = new GetOneStatusState(this);
            }
            else
                Amp.currentState = new XmitCmdState(this);
        }
    }
}

