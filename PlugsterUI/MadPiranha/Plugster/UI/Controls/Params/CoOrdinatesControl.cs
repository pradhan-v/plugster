//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MadPiranha.Plugster.Base.Param;

namespace MadPiranha.Plugster.UI.Controls.Params
{
    public partial class CoOrdinatesControl : UserControl, IParamControl
    {
        //TODO: implement value change event, chceck TextControl

        private CoOrdinatesParam param;
        public CoOrdinatesControl(CoOrdinatesParam param)
        {
            InitializeComponent();
            this.Name = "XYPARAM";

            this.param = param;

            NumberParam x = this.param.XParam;
            this.numberControl1.SetNumberParam(x);
            NumberParam y = this.param.YParam;
            this.numberControl2.SetNumberParam(y);
        }

        public Control GetControl()
        {
            return this;
        }

        public void SetXY()
        {
            this.numberControl1.SetNumber(System.Windows.Forms.Cursor.Position.X);
            this.numberControl2.SetNumber(System.Windows.Forms.Cursor.Position.Y);
        }
    }
}
