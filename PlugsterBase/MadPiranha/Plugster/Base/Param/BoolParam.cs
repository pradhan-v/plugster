//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;


namespace MadPiranha.Plugster.Base.Param
{
    public class BoolParam : LabelParam, IParam
    {
        public event ValueChangedDelegate ValueChanged;

        public bool BoolValue
        {
            get { return Convert.ToBoolean(Value); }
            set
            {
                Value = value;
                if (ValueChanged != null) ValueChanged(!Convert.ToBoolean(Value));
            }
        }

        public BoolParam(string label, bool b) : base(label, b)
        {

        }

    }
}
