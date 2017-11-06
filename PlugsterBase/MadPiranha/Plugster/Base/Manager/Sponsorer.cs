//Copyright © MadPiranha 2012-2013

//http://msdn.microsoft.com/en-us/magazine/cc300474.aspx#S2

using System;
using System.Diagnostics;
using System.Collections;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting;

namespace MadPiranha.Plugster.Base.Manager
{
    //TODO: Use this in plug loader... !
    public class Sponsorer : MarshalByRefObject, ISponsor
    {
        IList leaseObjects;
        public Sponsorer()
        {
            leaseObjects = new ArrayList();
        }

        ~Sponsorer()
        {
            UnregisterAll();
        }

        public void OnExit(object sender, EventArgs e)
        {
            UnregisterAll();
        }

        TimeSpan ISponsor.Renewal(ILease lease)
        {
            Debug.Assert(lease.CurrentState == LeaseState.Active);
            return lease.InitialLeaseTime;
        }

        public void Register(MarshalByRefObject obj)
        {
            ILease lease = (ILease)RemotingServices.GetLifetimeService(obj);
            Debug.Assert(lease.CurrentState == LeaseState.Active);

            lease.Register(this);
            lock (this)
            {
                leaseObjects.Add(lease);
            }
        }

        public void UnregisterAll()
        {
            lock (this)
            {
                int index = 0;
                while (leaseObjects.Count > 0)
                {
                    ILease lease = (ILease)leaseObjects[index];
                    try
                    {
                        lease.Unregister(this);
                    }
                    catch (Exception e)
                    {
                        //InvalidOperationException and RemotingException
                        //Happens while close of app.
                        //Domain gets unloaded first and
                        //we get this exception...  "Handle is not initialized".
                    }
                    leaseObjects.RemoveAt(index);
                    //index++;
                }
            }
        }

        public void Unregister(MarshalByRefObject obj)
        {
            ILease lease = (ILease)RemotingServices.GetLifetimeService(obj);
            Debug.Assert(lease.CurrentState == LeaseState.Active);

            lease.Unregister(this);
            lock (this)
            {
                leaseObjects.Remove(lease);
            }
        }
    }
}

