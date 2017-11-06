//Copyright © MadPiranha 2012-2013

using System;
using System.Threading;


namespace MadPiranha.Plugster.Base.Test
{
    public class TestController
    {

        public event TestStartedHandler TestStarted;
        public event ExceptionInTestHandler ExceptionInTest;
        public event TestFinishedHandler TestFinished;

        public delegate void TestStartedHandler (object o, TestLifeCycleEventArgs e);
        public delegate void ExceptionInTestHandler (object o, TestLifeCycleEventArgs e);
        public delegate void TestFinishedHandler (object o, TestLifeCycleEventArgs e);

        private Thread executionThread;

        private ITest test;

        public TestController(ITest test)
        {
            this.test = test;
        }

        public void ExecuteTest()
        {
            if (executionThread == null || (executionThread != null && !executionThread.IsAlive))
            {
                //executionThread = new Thread(new ParameterizedThreadStart(this.StartExecute));
                executionThread = new Thread(new ThreadStart(this.StartExecute));
            }

            if (!executionThread.IsAlive)
            {
                //executionThread.Start(test);
                executionThread.Start();
            }
        }

        public void CloseTest()
        {
            TestLifeCycleEventArgs tlce = new TestLifeCycleEventArgs(test);
            try
            {
                test.Closing();
            }
            catch (Exception e)
            {
                tlce.ExceptionInfo = BaseUtils.GetExceptionInfo(e);
                ExceptionInTest(this, tlce);
            }
        }

        //public void StartExecute(object testobj)
        public void StartExecute()
        {
            TestLifeCycleEventArgs tlce = new TestLifeCycleEventArgs(test);
            TestStarted(this, tlce);

            try
            {
                test.ExecuteThis();
            }
            catch (Exception e)
            {
                tlce.ExceptionInfo = BaseUtils.GetExceptionInfo(e);
                ExceptionInTest(this, tlce);
            }
            TestFinished(this, tlce);
        }

        public void Kill()
        {
            if (executionThread.IsAlive)
                executionThread.Abort();
        }

    }
}
