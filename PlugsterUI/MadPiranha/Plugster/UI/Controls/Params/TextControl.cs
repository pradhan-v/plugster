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
    public partial class TextControl : AbstractParamControl
    {
        private TextParam param;

        public TextControl(TextParam param) : base (param)
        {
            InitializeComponent();
            this.param = param;

            this.label1.Text = this.param.Label;
            UpdateControlValue();
        }

        protected override void UpdateParamValue()
        {
            param.TextValue = this.textBox1.Text;
        }

        protected override void UpdateControlValue()
        {
            this.textBox1.Text = this.param.TextValue;
        }
    }
}
