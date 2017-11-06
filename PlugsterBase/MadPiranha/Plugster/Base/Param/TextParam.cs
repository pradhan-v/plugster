//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;


namespace MadPiranha.Plugster.Base.Param
{
    [Serializable]
    public class TextParam : LabelParam, IParam
    {
        public event ValueChangedDelegate ValueChanged;

        public string TextValue
        {
            get { return Value.ToString(); }
            set
            {
                string temp = Value.ToString();
                Value = value;
                if (ValueChanged != null) ValueChanged(temp);
            }
        }

        public TextParam(string label, string text) : base(label, text)
        {
            
        }

    }
}
