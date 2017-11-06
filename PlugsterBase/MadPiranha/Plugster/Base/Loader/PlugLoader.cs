//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Security.Policy;
using System.IO;
using MadPiranha.Plugster.Base.Test;
using System.Collections;
using MadPiranha.Plugster.Base;
using System.Threading;
using System.Runtime.InteropServices;
using MadPiranha.Plugster.Base.Manager;

namespace MadPiranha.Plugster.Base.Loader
{
    public delegate void LoadingAppDomainHandler(object o, PlugLoadEventArgs e);
    public delegate void LoadingPlugHandler(object o, PlugLoadEventArgs e);
    public delegate void AppDomainLoadedHandler(object o, PlugLoadEventArgs e);
    public delegate void PlugLoadedHandler(object o, PlugLoadEventArgs e);
    public delegate void UnloadingAppDomainHandler(object o, PlugLoadEventArgs e);
    public delegate void AppDomainUnloadedHandler(object o, PlugLoadEventArgs e);
    public delegate void PlugLoadExceptionHandler(object o, PlugLoadEventArgs e);

    public class PlugLoader
    {
        public event LoadingAppDomainHandler LoadingAppDomain;
        public event LoadingPlugHandler LoadingPlug;
        public event AppDomainLoadedHandler AppDomainLoaded;
        public event PlugLoadedHandler PlugLoaded;
        public event UnloadingAppDomainHandler UnloadingAppDomain;
        public event AppDomainUnloadedHandler AppDomainUnloaded;
        public event PlugLoadExceptionHandler PlugLoadException;

        private Sponsorer sponsorer;
        private Hashtable appDomainTable;

        public PlugLoader()
        {
            sponsorer = new Sponsorer();
            appDomainTable = new Hashtable();
        }

        public bool Unload(string appDomainName)
        {
            AppDomain appdomain = (AppDomain) appDomainTable[appDomainName];
            if (appdomain != null)
                return Unload(ref appdomain);
            else
                return false;
        }

        private bool Unload(ref AppDomain appDomain)
        {
            bool success = false;

            PlugLoadEventArgs plea = new PlugLoadEventArgs();
            string name = appDomain.FriendlyName;
            plea.AppDomainName = name;//TODO: check this !
            
            if(UnloadingAppDomain!=null)
                UnloadingAppDomain(this, plea);

            try
            {
                if (appDomain != null)
                {
                    AppDomain.Unload(appDomain);
                    appDomain = null;
                    appDomainTable.Remove(name);

                    if(AppDomainUnloaded!=null)
                        AppDomainUnloaded(this, plea);
                }
            }
            catch (Exception e)
            {
                plea.ExceptionInfo = BaseUtils.GetExceptionInfo(e);

                if(AppDomainUnloaded!=null)
                    AppDomainUnloaded(this, plea);

                success = false;
            }

            return success;
        }

        public AppDomain LoadAppDomain(string appDomainName)
        {
            PlugLoadEventArgs loadEvent1 = new PlugLoadEventArgs();
            loadEvent1.AppDomainName = appDomainName;
            
            if(LoadingAppDomain!=null)
                LoadingAppDomain(this, loadEvent1);

            AppDomain appDomain;
            if (appDomainName == null || string.Empty.Equals(appDomainName)
                || "default".Equals(appDomainName))
            {
                appDomain = AppDomain.CurrentDomain;
            }
            else if (appDomainTable.Contains(appDomainName))
            {
                appDomain = (AppDomain)appDomainTable[appDomainName];
            }
            else
            {
                //AppDomainSetup appSetup = new AppDomainSetup();
                AppDomainSetup appSetup = AppDomain.CurrentDomain.SetupInformation;
                appSetup.ApplicationName = appDomainName;
                appSetup.ShadowCopyFiles = "true";
                appSetup.LoaderOptimization = LoaderOptimization.MultiDomain;

                appDomain = AppDomain.CreateDomain(appDomainName, AppDomain.CurrentDomain.Evidence, appSetup);

                if (appDomain != null && !appDomainTable.Contains(appDomain.FriendlyName))
                {
                    appDomainTable.Add(appDomain.FriendlyName, appDomain);
                    if (AppDomainLoaded != null)
                        AppDomainLoaded(this, loadEvent1);
                }
            }

            return appDomain;

        }

        public void LoadAllPlugs(string assemblyFile, AppDomain appDomain, string[] testsClasses, ArrayList tests)
        {
            for (int i = 0; i < testsClasses.Length; i++)
            {
                ITest test = LoadPlug(appDomain, assemblyFile, testsClasses[i]);
                if(test!=null)
                    tests.Add(test);
            }

        }

        public ITest LoadPlug(string appDomainName, string assemblyFile, string classname)
        {
            try
            {
                AppDomain appDomain = LoadAppDomain(appDomainName);
                return LoadPlug(appDomain, assemblyFile, classname);
            }
            catch (Exception e)
            {
                PlugLoadEventArgs loadEvent = new PlugLoadEventArgs();
                loadEvent.AppDomainName = appDomainName;
                loadEvent.AssemblyName = assemblyFile;
                loadEvent.ExceptionInfo = BaseUtils.GetExceptionInfo(e);

                if(PlugLoadException!=null)
                    PlugLoadException(this, loadEvent);
            }
            return null;
        }


        public ITest LoadPlug(AppDomain appDomain, string assemblyFile, string classname)
        {
            ITest test = null;
            PlugLoadEventArgs loadEvent1 = new PlugLoadEventArgs();
            loadEvent1.PlugName = classname;
            loadEvent1.AppDomainName = appDomain.FriendlyName;
            loadEvent1.AssemblyName = assemblyFile;

            if(LoadingPlug!=null)
                LoadingPlug(this, loadEvent1);

            try
            {
                //Assembly assembly = Assembly.LoadFrom(appDomain.BaseDirectory + assemblyName + ".dll");
                //test = (ITest)assembly.CreateInstance(classname);
                test = (ITest)appDomain.CreateInstanceFromAndUnwrap(assemblyFile, classname);

                if (test != null)
                {
                    if(test is MarshalByRefObject && appDomain!=AppDomain.CurrentDomain)
                        sponsorer.Register((MarshalByRefObject)test);

                    //TODO: setOutput here...
                    loadEvent1.TestName = test.Name;
                    
                    if(PlugLoaded!=null)
                        PlugLoaded(this, loadEvent1);
                    
                    test.InitTest();
                }
            }
            catch (Exception ex)
            {
                //addException(ex);
                loadEvent1.ExceptionInfo = BaseUtils.GetExceptionInfo(ex);
                
                if(PlugLoadException!=null)
                    PlugLoadException(this, loadEvent1);
            }

            return test;
        }

        public void Unload()
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        public ITest[] LoadAllPlugs(IPlugLoadInfo info)
        {
            //resetMessages();
            ArrayList tests = new ArrayList();

            string assemblyName = null;
            string appDomainName = null;

            try
            {
                string[] assemblies = info.GetAssemblyFiles();
                for (int i = 0; i < assemblies.Length; i++)
                {
                    assemblyName = assemblies[i];
                    appDomainName = info.GetAppDomainName(assemblies[i]);

                    AppDomain appDomain = LoadAppDomain(appDomainName);
                    string[] testcases = info.GetTestsFromAssembly(assemblyName);

                    LoadAllPlugs(assemblyName, appDomain, testcases, tests);
                }
            }
            catch (Exception e)
            {
                PlugLoadEventArgs loadEvent = new PlugLoadEventArgs();
                loadEvent.AppDomainName = appDomainName;
                loadEvent.AssemblyName = assemblyName;
                loadEvent.ExceptionInfo = BaseUtils.GetExceptionInfo(e);
                
                if(PlugLoadException!=null)
                    PlugLoadException(this, loadEvent);
            }

            //message += "\n\nDone !\n\n";
            ITest[] ret = new ITest[tests.Count];
            for (int i = 0; i < tests.Count; i++)
            {
                ret[i] = (ITest)tests[i];
            }
            //return (ITest[])tests.ToArray(typeof(ITest));
            return ret;
        }


        public TestLoadInfo[] GetAllPlugs(String appDomainName, String assemblyStr)
        {
            ArrayList tests = new ArrayList();
            if (assemblyStr != null)
            {
                //dont use load from... this will load the dll in the current default domain
                //so, we cannot delete/overwrite the file while recompile in quicktest...
                //Assembly assembly = Assembly.LoadFrom(assemblyStr);
                Assembly assembly = Assembly.Load(File.ReadAllBytes(assemblyStr));
                Type[] types = assembly.GetExportedTypes();
                foreach (Type type in types)
                {
                    if (type.IsClass && typeof(ITest).IsAssignableFrom(type))
                    {
                        //tests.Add(new TestLoadInfo("QuickTestDomain", assembly.ManifestModule.ScopeName, type.ToString()));
                        tests.Add(new TestLoadInfo(appDomainName, assemblyStr, type.ToString()));
                    }
                }
            }
            return (TestLoadInfo[])tests.ToArray(typeof(TestLoadInfo));
        }

        public TestLoadInfo[] GetAllPlugs(IPlugLoadInfo info)
        {
            ArrayList tests = new ArrayList();

            string[] assemblies = info.GetAssemblyFiles();
            foreach (string assemblyFile in assemblies)
            {
                string appDomainName = info.GetAppDomainName(assemblyFile);
                string[] testcases = info.GetTestsFromAssembly(assemblyFile);
                
                foreach(string testcase in testcases)
                {
                    bool auto = info.IsAutoLoad(testcase, assemblyFile);
                    tests.Add(new TestLoadInfo(appDomainName, assemblyFile, testcase, auto));
                }
            }

            return (TestLoadInfo[])tests.ToArray(typeof(TestLoadInfo));
        }


        public static IList<AppDomain> GetAppDomains()
        {
            IList<AppDomain> _IList = new List<AppDomain>();
            IntPtr enumHandle = IntPtr.Zero;

            //COM Reference to the mscoree.tlb is required for this
            //mscoree.tlb - Common Language Runtime Execution Engine 2.0 Library
            mscoree.CorRuntimeHostClass host = new mscoree.CorRuntimeHostClass();

            try
            {
                host.EnumDomains(out enumHandle);
                object domain = null;
                while (true)
                {
                    host.NextDomain(enumHandle, out domain);
                    if (domain == null) break;
                    AppDomain appDomain = (AppDomain)domain;
                    _IList.Add(appDomain);
                }
                return _IList;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                return null;
            }
            finally
            {
                host.CloseEnum(enumHandle);
                Marshal.ReleaseComObject(host);
            }

        }

    }
}
