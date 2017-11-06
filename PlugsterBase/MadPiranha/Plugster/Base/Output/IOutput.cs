//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

namespace MadPiranha.Plugster.Base.Output
{
    public interface IOutput
    {
        void Write(Object str);

        void WriteLine(Object str);

        void WriteLine();

        void ScrollEnd();

        void Clear();

    }
}
