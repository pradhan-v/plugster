//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using MadPiranha.Plugster.Base.Reader;

namespace MadPiranha.Plugster.Base.Loader
{
    public class PlugLoadInfo : IPlugLoadInfo
    {

        private INIReader testsReader;
        public PlugLoadInfo()
        {
            testsReader = new INIReader(AppDomain.CurrentDomain.BaseDirectory + "AllTests.ini");
        }

        public string GetAppDomainName(string assemblyFile)
        {
            return testsReader.ReadValue(assemblyFile, "AppDomainName");
        }

        public string GetAutoLoadTests(string assemblyFile)
        {
            return testsReader.ReadValue(assemblyFile, "AutoLoad");
        }
        //TODO:move to xml, this is getting ugly.
        public bool IsAutoLoad(string plug, string assemblyFile)
        {
            bool isauto = false;
            string galt = GetAutoLoadTests(assemblyFile);
            if (galt != null && galt.IndexOf(plug)>-1)
            {
                isauto = true;
            }
            return isauto;
        }

        public string[] GetTestsFromAssembly(string assemblyFile)
        {
            ArrayList teststr = new ArrayList();

            bool read = true;
            for (int i = 1; read && i < 100; i++)
            {
                try
                {
                    string testName = testsReader.ReadValue(assemblyFile, "Test" + i);
                    if (testName == null || string.Empty.Equals(testName))
                    {
                        read = false;
                        continue;
                    }

                    teststr.Add(testName);
                }
                catch (Exception)
                {
                    
                }
            }
            return (string[])teststr.ToArray(typeof(string));
        }

        public string[] GetAssemblyFiles()
        {
            return (string[])testsReader.GetSections().ToArray();
        }
    }
}
