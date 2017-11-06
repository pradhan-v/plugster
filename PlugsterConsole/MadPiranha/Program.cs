//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Base.Loader;
using MadPiranha.Plugster.Base;
using MadPiranha.Plugster.Base.Output;
using MadPiranha.Plugster.Base.Test;
using MadPiranha.Plugster.Base.Param;
using MadPiranha.Plugster.Base.Reader;

namespace MadPiranha.PlugsterConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            new Program().Start();
        }

        Program()
        {

        }

        public void Start()
        {
            PlugLoader plugLoader = TempFactory.GetPlugLoader();
            plugLoader.AppDomainLoaded += new AppDomainLoadedHandler(this.AppDomainLoaded);
            plugLoader.AppDomainUnloaded += new AppDomainUnloadedHandler(this.AppDomainUnloaded);
            plugLoader.LoadingAppDomain += new LoadingAppDomainHandler(this.LoadingAppDomain);
            plugLoader.LoadingPlug += new LoadingPlugHandler(this.LoadingPlug);
            plugLoader.PlugLoaded += new PlugLoadedHandler(this.PlugLoaded);
            plugLoader.PlugLoadException += new PlugLoadExceptionHandler(this.PlugLoadException);
            plugLoader.UnloadingAppDomain += new UnloadingAppDomainHandler(this.UnloadingAppDomainHandler);


            TestLoadInfo[] testholders = plugLoader.GetAllPlugs(TempFactory.GetLoadTestsInfo());
            for (int i=0; i<testholders.Length; i++)
                Console.WriteLine(i + " : " + testholders[i]);

            int inpi = 0;
            if (testholders.Length > 1)
            {
                Console.Write("Test#:");
                string inps = Console.ReadLine();
                inpi = Convert.ToInt32(inps);
            }

            TestLoadInfo tl = testholders[inpi];

            ITest test = plugLoader.LoadPlug(tl.AppDomainName, tl.AssemblyName, tl.ClassName);
            ConsoleOutput cout = new ConsoleOutput();

            test.SetOutput(cout);
            cout.WriteLine("Parameters...");

            IParam[] tparams = test.GetParameters();

           INIReader r = new INIReader(AppDomain.CurrentDomain.BaseDirectory + "AllTests.ini");

                for (int y = 0; y < tparams.Length; y++)
                {
                    IParam param = (tparams[y]);

                    cout.WriteLine("Enter New Value for : " + param);
                    if (param is NumberParam)
                    {
                        if (testholders.Length == 1)
                        {
                            ((NumberParam)param).Number = Convert.ToInt32(r.ReadValue(testholders[inpi].AssemblyName, "Param" + y));
                        }
                        else
                        {
                            cout.Write("Number:");
                            ((NumberParam)param).Number = Convert.ToInt32(Get(param));
                        }
                    }
                    else if (param is TextParam)
                    {
                        if (testholders.Length == 1)
                        {
                            ((TextParam)param).TextValue = r.ReadValue(testholders[inpi].AssemblyName, "Param" + y);
                        }
                        else
                        {
                            cout.Write("Text:");
                            ((TextParam)param).TextValue = Get(param);
                        }
                    }
                    else if (param is BoolParam)
                    {
                        if (testholders.Length == 1)
                        {
                            ((BoolParam)param).Value = Convert.ToBoolean(r.ReadValue(testholders[inpi].AssemblyName, "Param" + y));
                        }
                        else
                        {
                            cout.Write("Bool:");
                            ((BoolParam)param).Value = Convert.ToBoolean(Get(param));
                        }
                    }
                    else if (param is KeyValueParam)
                    {
                        cout.Write("KeyVal:");
                        //((KeyValueParam)param).SelectedText = ((ComboBox)control).SelectedValue.ToString();
                    }
                    else if (param is CoOrdinatesParam)
                    {
                        if (testholders.Length == 1)
                        {
                            ((CoOrdinatesParam)param).XParam.Number = Convert.ToInt32(r.ReadValue(testholders[inpi].AssemblyName, "ParamX" + y));
                            ((CoOrdinatesParam)param).XParam.Number = Convert.ToInt32(r.ReadValue(testholders[inpi].AssemblyName, "ParamY" + y));
                        }
                        else
                        {
                            cout.Write("NumberX:");
                            ((CoOrdinatesParam)param).XParam.Number = Convert.ToInt32(Get(((CoOrdinatesParam)param).XParam));
                            cout.Write("NumberY:");
                            ((CoOrdinatesParam)param).YParam.Number = Convert.ToInt32(Get(((CoOrdinatesParam)param).YParam));
                        }
                    }

                }


                cout.WriteLine("Enter to exec...");
                    Console.ReadLine();
            
            test.ExecuteThis();
            cout.WriteLine("Done.");
            Console.ReadLine();
        }

        public string Get(IParam param)
        {
            string s = Console.ReadLine();
            if (string.Empty.Equals(s))
            {
                return param.Value.ToString();
            }
            return s;
        }

        #region Plug Loader Events

        public void AppDomainLoaded(object o, PlugLoadEventArgs a)
        {
            Console.WriteLine("App Domain Loaded : " + a.AppDomainName);
        }

        public void AppDomainUnloaded(object o, PlugLoadEventArgs a)
        {
            Console.WriteLine("App Domain Unloaded : " + a.AppDomainName);
        }

        public void LoadingAppDomain(object o, PlugLoadEventArgs a)
        {
            Console.WriteLine("\nLoading assembly : " + a.AssemblyName + " to App Domain " + a.AppDomainName);
        }

        public void LoadingPlug(object o, PlugLoadEventArgs a)
        {
            Console.WriteLine("Loading Plug : " + a.PlugName);
        }

        public void PlugLoaded(object o, PlugLoadEventArgs a)
        {
            Console.WriteLine("Plug Loaded : " + a.PlugName);
        }

        public void PlugLoadException(object o, PlugLoadEventArgs a)
        {
            Console.WriteLine("Exception Loading Plug... \n" + a.ExceptionInfo);
        }

        public void UnloadingAppDomainHandler(object o, PlugLoadEventArgs a)
        {

        }

        #endregion

    }
}
