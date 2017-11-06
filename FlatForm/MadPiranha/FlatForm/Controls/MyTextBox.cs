//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MadPiranha.Plugster.UI;
using System.Runtime.InteropServices;

namespace MadPiranha.FlatForm.Controls
{
    public partial class MyTextBox : TextBox
    {
        public TextBox TheTextBox
        {
            get 
            { 
                return this;
            }
        }

        public MyTextBox()
        {
            base.BorderStyle = UITheme.TextBorderStyle;
            base.BackColor = UITheme.TextBackColor;
        }

        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set {  }
        }

        public new Color BackColor
        {
            get { return base.BackColor; }
            set { }
        }

        private void DrawBorder()
        {
            if(UITheme.TextBorderStyle == BorderStyle.FixedSingle)
            {
                IntPtr hWnd = this.Handle;
                IntPtr hDC = GetWindowDC(hWnd);
                if (hDC.ToInt32() != 0)
                {
                    Graphics g = Graphics.FromHdc(hDC);

                    using (Pen p = new Pen(UITheme.FlatAppearance_BorderColor, 2))
                    //using (Pen p = new Pen(Color.Red, 2))
                    {
                        Rectangle r = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
                        g.DrawRectangle(p, r);
                    }
                
                    ReleaseDC(hWnd, hDC);

                }
            }
        }


        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        const int WM_NCPAINT = 0x0085;
        const int WM_PAINT = 0x00F;
       
        protected override void WndProc(ref Message m)
        {
            switch(m.Msg)
            {
                case WM_NCPAINT:
                {
                   
                    if (UITheme.TextBorderStyle == BorderStyle.FixedSingle)
                    {
                        DrawBorder();
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    break;
                }
                case WM_PAINT:
                {
                    //ControlPaint.DrawBorder(this.Parent.CreateGraphics(), this.Bounds, Color.Red, ButtonBorderStyle.Solid);
                    break;
                }
            }
            base.WndProc(ref m);
        }

    }
}
