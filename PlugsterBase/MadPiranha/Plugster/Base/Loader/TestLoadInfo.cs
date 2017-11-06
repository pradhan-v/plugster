//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Base.Test;

namespace MadPiranha.Plugster.Base.Loader
{
    public class TestLoadInfo
    {
        public TestLoadInfo(string appdomainname, string assemblyname, string classname)
        {
            this.AppDomainName = appdomainname;
            this.AssemblyName = assemblyname;
            this.ClassName = classname;
        }

        public TestLoadInfo(string appdomainname, string assemblyname, string classname, bool autoLoad)
        {
            this.AppDomainName = appdomainname;
            this.AssemblyName = assemblyname;
            this.ClassName = classname;
            this.AutoLoad = autoLoad;
        }

        public TestLoadInfo()
        {
        }

        private ITest test;
        public ITest Test
        {
            get { return test; }
            set 
            { 
                test = value;
                name = (test!=null?test.Name:"");
            }
        }

        private string name = "";
        public string Name
        {
            get { return name; }
        }


        private string appDomainName;
        public string AppDomainName
        {
            get { return appDomainName; }
            set { appDomainName = value; }
        }

        private string assemblyName;
        public string AssemblyName
        {
            get { return assemblyName; }
            set { assemblyName = value; }
        }

        private string className;
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        private bool autoLoad;
        public bool AutoLoad
        {
            get { return autoLoad; }
            set { autoLoad = value; }
        }

        public override string ToString()
        {
            return className;
            //string tostring=className + " (" + assemblyName + ")";;
            //if(className.IndexOf(".")>-1)
            //    tostring = className.Substring(className.LastIndexOf(".")+1) +  " <" + tostring + ">";
            //return tostring;
        }
    }
}
