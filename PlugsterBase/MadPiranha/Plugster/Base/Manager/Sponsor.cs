//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Lifetime;
using MadPiranha.Plugster.Base.Output;


namespace MadPiranha.Plugster.Base.Manager
{
    public class Sponsor : MarshalByRefObject, ISponsor 
    {
        private int renewalTimeMin;
        public int RenewalTimeMin
        {
            get { return renewalTimeMin; }
            set { renewalTimeMin = value; }
        }

        public Sponsor()
        {
            RenewalTimeMin = 10;
        }
        public Sponsor(int time)
        {
            RenewalTimeMin = time;
        }



        #region ISponsor Members

        public TimeSpan Renewal(ILease lease)
        {
            if (lease.CurrentState == LeaseState.Active)
            {
                return TimeSpan.FromMinutes(RenewalTimeMin);
            }
            else
                return TimeSpan.Zero;
        }

        #endregion

    }
}
