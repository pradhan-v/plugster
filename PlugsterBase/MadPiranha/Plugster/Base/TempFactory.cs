//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Base.Loader;

namespace MadPiranha.Plugster.Base
{
    public class TempFactory
    {
        public static PlugLoader GetPlugLoader()
        {
            return new PlugLoader();
        }

        public static IPlugLoadInfo GetLoadTestsInfo()
        {
            return new PlugLoadInfo();
        }
    }


}
