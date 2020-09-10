using System;
using System.Collections.Generic;
using System.Text;

namespace MPRSG6Z
{
    public abstract class AmpState
    {

       private Amp amp;

        public Amp Amp
        {
            get { return amp; }
            set { amp = value; }
        }

        //private int dummyCashPresent = 1000;

        //public int DummyCashPresent
        //{
        //    get { return dummyCashPresent; }
        //    set { dummyCashPresent = value; }
        //}

        public abstract string GetNextScreen();
    }
}


