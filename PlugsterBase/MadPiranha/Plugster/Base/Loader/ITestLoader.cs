//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Base.Test;

namespace MadPiranha.Plugster.Base.Loader
{
    public interface ITestLoader
    {
        void LoadTest(TestLoadInfo testHolder);
        void UnLoadTest(TestLoadInfo testHolder);
    }
}
