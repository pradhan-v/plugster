//Copyright © MadPiranha 2012-2013


using MadPiranha.Plugster.Base.Test;
using MadPiranha.Plugster.Base.Param;
using System;
using System.Threading;

namespace MadPiranha.PlugsterTests.Test
{
    public class ClockTest : AbstractTest
    {
        
        private BoolParam start;
        
        public ClockTest()
        {
            start = new BoolParam("Run Clock", true);
            parameters = new IParam[] { start };
        }

        public override string GetDescription()
        {
            return "Simple Clock. Uncheck to stop clock.";
        }

        public override void ParamUpdated()
        {
            //startClock();
            WriteLine("param updated");
        }

        public override void ExecuteThis()
        {
            startClock();
        }

        private void startClock()
        {
            while (start.BoolValue)
            {
                output.Clear();
                WriteLine(DateTime.Now);
                Thread.Sleep(500);
            }
        }
    }
}
