//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

namespace MadPiranha.Plugster.Base
{
    public class BaseUtils
    {
        public static string GetExceptionInfo(Exception e)
        {
            string message = e.ToString() + "\n->" + e.Message + "\n->" + e.StackTrace;
            if (e.InnerException != null)
            {
                message += "\nInner Exception: "
                    + e.InnerException.ToString()
                    + "\n" + e.InnerException.Message
                    + "\n" + e.InnerException.StackTrace;
            }

            return message;
        }
    }
}
