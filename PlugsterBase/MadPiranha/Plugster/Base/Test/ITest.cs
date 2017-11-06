//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Base.Output;
using MadPiranha.Plugster.Base.Param;

namespace MadPiranha.Plugster.Base.Test
{
    public interface ITest
    {
        //String GetName();
        //String GetKey();

        String Name{get;}
        String Key{get;}

        void SetOutput(IOutput output);
        IOutput GetOutput();

        void InitTest();
        
        IParam[] GetParameters();

        void ParamUpdated();
        
        void ExecuteThis();

        string GetDescription();

        void Closing();
    }
}
