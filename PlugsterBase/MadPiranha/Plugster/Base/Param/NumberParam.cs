//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;


namespace MadPiranha.Plugster.Base.Param
{
    [Serializable]
    public class NumberParam : TextParam
    {
        public int Number
        {
            get
            {
                try
                {
                    return Convert.ToInt32(Value);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set { Value = value; }
        }

        public NumberParam(string label, int number)
            : base(label, "" + number)
        {

        }

    }
}
