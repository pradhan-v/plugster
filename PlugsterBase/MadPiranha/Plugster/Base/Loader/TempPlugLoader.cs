//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Security.Policy;
using MadPiranha.Plugster.Base.Test;
using System.Collections;


namespace MadPiranha.Plugster.Base.Loader
{
    public class TempPlugLoader //: IPlugLoader
    {
        string message;
        public string Message
        {
            get { return message; }
        }
        
        string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
        }

        string errorStack;
        public string ErrorStack
        {
            get { return errorStack; }
        }

        private static TempPlugLoader loader;
        static TempPlugLoader()
        {
            loader = new TempPlugLoader();
        }
        private TempPlugLoader()
        {
            resetMessages();
        }
        //public static IPlugLoader GetPlugLoader()
        //{
        //    return loader;
        //}

        private void resetMessages()
        {
            message = string.Empty;
            errorMessage = string.Empty;
            errorStack = string.Empty;
        }


        private AppDomain appDomain;
        //private String appDomainName = "PlugsterTestsDomain";
        //private String appName = "PlugsterTests";
        //private string assemblyName = "PlugsterTests.dll";
        //private string[] teststr = new string[] { 
        //    "MadPiranha.PlugsterTests.TestScroll", 
        //    "MadPiranha.PlugsterTests.TestFindElement",
        //    "MadPiranha.PlugsterTests.TestHierarchy",
        //    "MadPiranha.PlugsterTests.TestProgram", 
        //    "MadPiranha.PlugsterTests.TestSilverWindows"
        //};

        private void Unload()
        {
            resetMessages();

            try
            {
                if (appDomain != null)
                {
                    AppDomain.Unload(appDomain);
                    appDomain = null;
                    message += "\nUnloaded Tests.";
                }
            }
            catch (Exception e)
            {
                addException(e);
            }
        }

        private void LoadAppDomain()
        {
            if (appDomain == null)
            {
                AppDomainSetup appSetup = new AppDomainSetup();
                appSetup.ShadowCopyFiles = "true";

                message += "\nLoading AppDomain : default";
                appDomain = AppDomain.CurrentDomain;// CreateDomain(appDomainName, AppDomain.CurrentDomain.Evidence, appSetup);

            }
        }

        private ArrayList LoadAllPlugs(string[] teststr)
        {
            
            message += "\nLoading tests...";
            ArrayList tests = new ArrayList();
            for (int i = 0; i < teststr.Length; i++)
            {
                try
                {
                    message += "\n\tLoading : " + teststr[i];
                    //ITest test = (ITest)assembly.CreateInstance(teststr[i]);
                    ITest test = (ITest)appDomain.CreateInstanceFromAndUnwrap("PlugsterTests.dll", teststr[i]);
                    if (test != null)
                    {
                        tests.Add(test);
                        message += "\n\tLoaded : " + test.Name;
                    }
                }
                catch (Exception ex)
                {
                    addException(ex);
                }
            }
            return tests;
        }

        private ITest[] LoadTests(string[] teststrs)
        {
            resetMessages();

            //System.Collections.Generic.Dictionary<string, ITest> testsTable = new System.Collections.Generic.Dictionary<string, ITest>();
            ITest[] tests = new ITest[0];
            try
            {
                LoadAppDomain();
                tests = (ITest[])LoadAllPlugs(teststrs).ToArray(typeof(ITest));
            }
            catch (Exception e)
            {
                addException(e);
            }

            message += "\nDone !";

            return tests;
        }

        private void addException(Exception e)
        {
            errorMessage += "\n\n" + e.Message ;
            errorStack += "\n\n" + e.Message + "\n" + e.StackTrace;
            message += "\n\n" + e.Message + "\n" + e.StackTrace + "\n";
            if (e.InnerException != null)
            {
                errorMessage += "\n\nInner Exception: " + e.InnerException.Message;
                errorStack += "\n\nInner Exception Stack : \n" + e.InnerException.Message + "\n" + e.InnerException.StackTrace;
                message += "\n\nInner Exception Stack : \n" + e.InnerException.Message + "\n" + e.InnerException.StackTrace + "\n";
            }
            
        }


        public string GetMessage()
        {
            return Message;
        }

        public string GetErrorMessage()
        {
            return ErrorMessage;
        }

        public string GetErrorStack()
        {
            return ErrorStack;
        }

        public void ResetMessages()
        {
            resetMessages();
        }

        public ITest[] LoadAll(string[] tests)
        {
            return LoadTests(tests);
        }
    }
}
