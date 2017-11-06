//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MadPiranha.FlatForm.Controls
{
    public class MyButton : Button
    {
        public MyButton()
        {
            this.FlatStyle = UITheme.FlatStyle;
            this.BackColor = UITheme.BackColor;
            this.FlatAppearance.BorderColor = UITheme.FlatAppearance_BorderColor;
            this.FlatAppearance.BorderSize = UITheme.FlatAppearance_BorderSize;
            this.FlatAppearance.MouseDownBackColor = UITheme.FlatAppearance_MouseDownBackColor;
            this.FlatAppearance.MouseOverBackColor = UITheme.FlatAppearance_MouseOverBackColor;
        }

        [Browsable(true)]
        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set { base.FlatStyle = UITheme.FlatStyle; }
        }

        public new Color BackColor
        {
            get { return base.BackColor; }
            set { }
        }

        //new FlatButtonAppearance FlatAppearance
        //{
        //    get { return base.FlatAppearance; }
        //    set { base.FlatAppearance = ; }
        //}
    }
}
