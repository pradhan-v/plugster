//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

namespace MadPiranha.Plugster.Base.Param
{
    public delegate void ValueChangedDelegate(object oldvalue);

    public interface IParam
    {
        event ValueChangedDelegate ValueChanged;
        object Value
        {
            get;
            set;
        }
    }
}
