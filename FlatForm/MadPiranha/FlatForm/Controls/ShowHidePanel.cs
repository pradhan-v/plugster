//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MadPiranha.FlatForm.Controls
{
    public partial class ShowHidePanel : Panel
    {
        new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = UITheme.BackColor; }
        }

        public ShowHidePanel()
        {
            InitializeComponent();
        }

        protected void EscapeKeyPress(object sender, KeyEventArgs a)
        {
            if (Keys.Escape == a.KeyCode)
            {
                this.Visible = false;
            }
        }

        protected void TestsListPanel_Leave(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Visible = false;
            }
        }

    }
}
