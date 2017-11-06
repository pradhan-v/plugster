//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MadPiranha.Plugster.UI.Controls.Params
{
    public partial class KeyValueControl : UserControl, IParamControl
    {
        public KeyValueControl()
        {
            InitializeComponent();
        }

        public Control GetControl()
        {
            return this;
        }
    }
}
