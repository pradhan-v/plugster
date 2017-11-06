//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

namespace MadPiranha.Plugster.Base.Loader
{
    public class PlugLoadEventArgs : EventArgs
    {
        private string appDomainName;

        public string AppDomainName
        {
            get { return appDomainName; }
            set { appDomainName = value; }
        }

        private string testName;

        public string TestName
        {
            get { return testName; }
            set { testName = value; }
        }

        private string assemblyName;

        public string AssemblyName
        {
            get { return assemblyName; }
            set { assemblyName = value; }
        }

        private string plugName;

        public string PlugName
        {
            get { return plugName; }
            set { plugName = value; }
        }

        private string exceptionInfo;

        public string ExceptionInfo
        {
            get { return exceptionInfo; }
            set { exceptionInfo = value; }
        }

    }
}
