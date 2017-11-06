//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

namespace MadPiranha.Plugster.Base
{
    public interface IPlugLoadInfo
    {
        string GetAppDomainName(string assemblyFile);

        string[] GetTestsFromAssembly(string assemblyFile);

        string[] GetAssemblyFiles();

        string GetAutoLoadTests(string assemblyFile);

        bool IsAutoLoad(string plug, string assemblyFile);

        //string[] GetAllTestClasses();

        //string GetAppDomainName(string testclass);

        //string GetAssemblyName(string testclass);

        //string[] GetTestsFromAppDomain(string appDomainName);
    }
}
