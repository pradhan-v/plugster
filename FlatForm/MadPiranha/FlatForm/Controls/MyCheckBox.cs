//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MadPiranha.FlatForm.Controls
{
    public class MyCheckBox : CheckBox
    {
        public MyCheckBox()
        {
            base.FlatStyle = UITheme.FlatStyle;
            //this.FlatAppearance.BorderColor = UITheme.FlatAppearance_BorderColor;
            //this.FlatAppearance.BorderSize = UITheme.FlatAppearance_BorderSize;
            //this.FlatAppearance.MouseDownBackColor = UITheme.FlatAppearance_MouseDownBackColor;
            //this.FlatAppearance.MouseOverBackColor = UITheme.FlatAppearance_MouseOverBackColor;
        }

        public FlatStyle FlatStyle{
            get
            {
                return base.FlatStyle;
            }
            set
            {

            }
        }



    }
}
