//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Base.Test;
using MadPiranha.Plugster.Base.Loader;

namespace MadPiranha.Plugster.UI.Base
{
    public interface ITestsHolderControl
    {
        TestLoadInfo GetSelectedTest();
        void AddTestHolders(TestLoadInfo[] tests);
        void AddTestHolder(TestLoadInfo test);
        TestLoadInfo[] GetAllTests();
    }
}
