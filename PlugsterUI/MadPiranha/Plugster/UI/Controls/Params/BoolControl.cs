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
    public partial class BoolControl : AbstractParamControl
    {
 
        private BoolParam param;
        public BoolControl(BoolParam param) : base(param)
        {
            this.param = param;
            InitializeComponent();
            this.checkBox1.Text = this.param.Label;
            UpdateControlValue();
        }

        protected override void UpdateParamValue()
        {
            this.param.BoolValue = this.checkBox1.Checked;
        }

        protected override void UpdateControlValue()
        {
            this.checkBox1.Checked = this.param.BoolValue;
        }
    }
}
