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
    public partial class LabelControl : UserControl, IParamControl
    {
        public LabelControl(LabelParam param)
        {
            InitializeComponent();
            this.label1.Text = param.Label;

            if (this.label1.PreferredWidth > this.label1.Width)
                this.label1.Size = new System.Drawing.Size(this.label1.PreferredWidth, this.label1.Height);
        }

        public Control GetControl()
        {
            return this;
        }
    }
}
