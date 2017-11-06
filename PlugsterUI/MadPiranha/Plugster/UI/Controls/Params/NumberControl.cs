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
    public partial class NumberControl : UserControl, IParamControl
    {
        //TODO: implement value change event, chceck TextControl
        private NumberParam param;

        public string Label
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }

        public NumberControl(NumberParam param) : this()
        {
            SetNumberParam(param);
        }

        public NumberControl()
        {
            InitializeComponent();
        }

        public Control GetControl()
        {
            return this;
        }

        public void SetNumberParam(NumberParam param)
        {
            this.param = param;
            this.label1.Text = this.param.Label;
            SetNumber(param.Number);
        }

        public void SetNumber(int n)
        {
            this.numericUpDown1.Value = n;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.param.Number = (int)this.numericUpDown1.Value;
        }
    }
}
