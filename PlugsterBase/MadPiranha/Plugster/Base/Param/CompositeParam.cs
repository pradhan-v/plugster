//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;


namespace MadPiranha.Plugster.Base.Param
{
    [Serializable]
    public abstract class CompositeParam : MarshalByRefObject, IParam
    {
        public event ValueChangedDelegate ValueChanged;

        public object Value
        {
            get
            {
                return Values;
            }
            set
            {
                throw new NotImplementedException();//wont implement exception
            }
        }

        public abstract IParam[] Values
        {
            get;
        }

    }
}
