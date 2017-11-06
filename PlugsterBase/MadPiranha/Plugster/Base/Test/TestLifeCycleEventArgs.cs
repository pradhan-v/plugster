//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

namespace MadPiranha.Plugster.Base.Test
{
    public class TestLifeCycleEventArgs
    {
        public TestLifeCycleEventArgs(ITest test)
        {
            this.test = test;
        }

        private ITest test;

        public ITest Test
        {
            get { return test; }
        }

        public string TestName
        {
            get { return test.Name; }
        }

        private string exceptionInfo;

        public string ExceptionInfo
        {
            get { return exceptionInfo; }
            set { exceptionInfo = value; }
        }
    }
}
