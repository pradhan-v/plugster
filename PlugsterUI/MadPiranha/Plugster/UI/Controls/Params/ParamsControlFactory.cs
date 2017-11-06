//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Base.Param;

namespace MadPiranha.Plugster.UI.Controls.Params
{
    public class ParamsControlFactory
    {
        public static IParamControl GetParamControl(IParam param)
        {
            IParamControl con = null;
            if (param is CoOrdinatesParam)
            {
                con = new CoOrdinatesControl((CoOrdinatesParam)param);
            }
            else if (param is NumberParam)
            {
                con = new NumberControl((NumberParam)param);
            }
            else if (param is TextParam)
            {
                con = new TextControl((TextParam)param);
            }
            else if (param is BoolParam)
            {
                con = new BoolControl((BoolParam)param);
            }
            else if (param is KeyValueParam)
            {
                KeyValueParam kvp = (KeyValueParam)param;
                con = new KeyValueControl();// (GetComboBox(key, kvp.KeyValuePairs, kvp.KeyString, kvp.ValueString));
            }

            return con;
        }
    }
}
