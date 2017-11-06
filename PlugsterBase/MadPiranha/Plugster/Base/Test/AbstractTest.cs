//Copyright © MadPiranha 2012-2013


using System;
using MadPiranha.Plugster.Base.Param;
using MadPiranha.Plugster.Base.Output;

namespace MadPiranha.Plugster.Base.Test
{
    [Serializable]
    public abstract class AbstractTest : MarshalByRefObject, ITest
    {

        protected IParam[] parameters;

        public AbstractTest()
        {

        }

        public AbstractTest(IOutput output)
        {
            SetOutput(output);
        }


        #region Lifetime

        //public override object InitializeLifetimeService()
        //{

        //    ILease lease = (ILease)base.InitializeLifetimeService();

        //    if (lease.CurrentState == LeaseState.Initial)
        //    {
        //        lease.InitialLeaseTime = TimeSpan.FromMinutes(30);
        //        lease.RenewOnCallTime = TimeSpan.FromMinutes(10);
        //        lease.SponsorshipTimeout = TimeSpan.FromMinutes(2);
        //    }
        //    return lease;
        //}

        //protected void RegisterSponsor()
        //{
        //    Sponsor s = new Sponsor();
        //    ILease lease = (ILease)RemotingServices.GetLifetimeService(this);
        //    lease.Register(s);
        //}
        
        #endregion

        #region ITest Members

        public String Name
        {
            get
            {
                return GetName();
            }
        }

        public String Key
        {
            get
            {
                return GetKey();
            }
        }

        public virtual void InitTest()
        {
            //RegisterSponsor();
        }

        public virtual string GetName()
        {
            string fullname = GetType().FullName;
            //int testindex = fullname.LastIndexOf(".Test");
            int dotindex = fullname.LastIndexOf(".");
            //if(testindex>0)
            //{
            //    return fullname.Substring(testindex+1).Insert(4, " ");
            //}
            //else 
            if(dotindex>0)
            {
                return fullname.Substring(dotindex+1);
            }
            else
                return fullname;
        }

        public virtual string GetKey()
        {
            return GetName();
        }

        public virtual IParam[] GetParameters()
        {
            return parameters;
        }

        public virtual void ParamUpdated()
        {
            
        }

        public abstract void ExecuteThis();

        public abstract string GetDescription();

        protected IOutput output;
        public virtual void SetOutput(IOutput output)
        {
            this.output = output;
        }

        public virtual IOutput GetOutput()
        {
            return output;
        }

        public virtual void Closing()
        {

        }

        #endregion

        #region output

        protected bool silent = false;
        protected void WriteLine(Object str)
        {
            if(!silent)
            output.WriteLine(str);
        }

        protected void WriteLine()
        {
            if (!silent)
            output.WriteLine();
        }

        protected void Write(Object str)
        {
            if (!silent)
            output.Write(str);
        }

        #endregion 

        #region Util methods

        public void AddParameter(IParam param)
        {
            AddParameters(new IParam[] { param });
        }

        public void AddParameters(IParam[] paramss)
        {
            if (parameters != null)
            {
                IParam[] parameters1 = new IParam[parameters.Length + paramss.Length];
                Array.Copy(parameters, parameters1, parameters.Length);
                Array.Copy(paramss, 0, parameters1, parameters.Length, paramss.Length);
                parameters = parameters1;
            }
            else
                parameters = paramss;
        }

        #endregion

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    