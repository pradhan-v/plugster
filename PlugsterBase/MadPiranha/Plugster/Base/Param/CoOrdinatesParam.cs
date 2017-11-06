//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;


namespace MadPiranha.Plugster.Base.Param
{
    public class CoOrdinatesParam : CompositeParam
    {
        public int X
        {
            get { return XParam.Number; }
            set { XParam.Number = value; }
        }
        public int Y
        {
            get { return YParam.Number; }
            set { YParam.Number = value; }
        }

        private NumberParam xParam;
        public NumberParam XParam
        {
            get { return xParam; }
        }

        private NumberParam yParam;
        public NumberParam YParam
        {
            get { return yParam; }
        }

        private IParam[] paramss;
        public override IParam[] Values
        {
            get
            {
                return paramss;
            }
        }

        

        public CoOrdinatesParam(int x, int y)
        {
            xParam = new NumberParam("X", x);
            yParam = new NumberParam("Y", y);

            paramss = new IParam[] { XParam, YParam };
        }

        public CoOrdinatesParam() : this (0,0)
        {

        }

        public override string ToString()
        {
            return XParam.ToString() + ", " + YParam.ToString();
        }

    }
}
