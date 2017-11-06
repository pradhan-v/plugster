//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

namespace MadPiranha.Plugster.Base.Output
{
    public class ConsoleOutput : IOutput
    {
        public void Write(object str)
        {
            Console.Write(str);
        }

        public void WriteLine(object str)
        {
            Console.WriteLine(str);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void ScrollEnd()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}
