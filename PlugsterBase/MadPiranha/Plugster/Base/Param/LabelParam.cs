//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;


namespace MadPiranha.Plugster.Base.Param
{
    [Serializable]
    public class LabelParam : MarshalByRefObject, IParam
    {
        //TODO: implement this.
        public event ValueChangedDelegate ValueChanged;

        private string label;
        private object val;

        public object Value
        {
            get { return val; }
            set { val = value; }
        }

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public LabelParam(string str)
        {
            this.Label = str;
        }

        public LabelParam(string label, object o)
            : this(label)
        {
            this.Value = o;
        }

        public override string ToString()
        {
            return Label + " : " + Value;
        }
    }
}
