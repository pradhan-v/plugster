//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MadPiranha.FlatForm.Controls;

namespace MadPiranha.FlatForm.Controls
{
    public partial class NonClientButtons : UserControl
    {
        public NonClientButtons()
        {
            InitializeComponent();
            this.BorderStyle = BorderStyle.None;

            myButton1.FlatStyle = FlatStyle.Flat;
            myButton1.FlatAppearance.BorderColor = this.BackColor;
            myButton1.Paint += new PaintEventHandler(this.OnPaint1);

            myButton2.FlatStyle = FlatStyle.Flat;
            myButton2.FlatAppearance.BorderColor = this.BackColor;
            myButton2.Paint += new PaintEventHandler(this.OnPaint1);

            myButton3.FlatStyle = FlatStyle.Flat;
            myButton3.FlatAppearance.BorderColor = this.BackColor;
            myButton3.Paint += new PaintEventHandler(this.OnPaint1);
        }

        private void ncb_Load(object sender, EventArgs a)
        {
            if(this.TopLevelControl!=null)
                this.TopLevelControl.Resize += new System.EventHandler(this.PlugsterWindow2_Resize);
            SetPanelLocation();
        }

        private void SetPanelLocation()
        {
            if (this.TopLevelControl != null)
            {
                //this.Location = new Point(this.TopLevelControl.Width-this.Width-this.Parent.Padding.Right, this.Parent.Padding.Top);
                this.Location = new Point(this.TopLevelControl.Width - this.Width, 0);
            }
            this.BringToFront();
        }

        private void PlugsterWindow2_Resize(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                SetPanelLocation();
            }
        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            ((Form)this.TopLevelControl).WindowState = FormWindowState.Minimized;
        }

        private void myButton2_Click(object sender, EventArgs e)
        {
            if(((Form)this.TopLevelControl).WindowState == FormWindowState.Normal)
            ((Form)this.TopLevelControl).WindowState = FormWindowState.Maximized;
            else
                ((Form)this.TopLevelControl).WindowState = FormWindowState.Normal;
        }

        private void myButton3_Click(object sender, EventArgs e)
        {
            ((Form)this.TopLevelControl).Close();
        }

        private void OnPaint1(object o, PaintEventArgs pevent)
        {
            int lineWidth = 2;
            int borderpad = 2;
            int pad = borderpad + (lineWidth / 2);

            Rectangle rect = pevent.ClipRectangle;

            Color color = Color.Black;

            if (o == myButton1)
            {
                //Draw Minimize Line
                using (Pen pen = new Pen(color, lineWidth))
                {
                    Point left = new Point(rect.X + pad, rect.Bottom - pad);
                    Point right = new Point(rect.Right - pad, rect.Bottom - pad);
                    pevent.Graphics.DrawLine(pen, left, right);
                }
            }
            else if (o == myButton2)
            {
                //Draw Restore Square
                using (Pen pen = new Pen(color, lineWidth))
                {
                    Rectangle restore = rect;
                    restore.Inflate(0-pad, 0-pad);
                    pevent.Graphics.DrawRectangle(pen, restore);
                }
            }
            else if (o == myButton3)
            {
                //Draw Close Cross
                using (Pen pen = new Pen(color, lineWidth))
                {
                    Point left = new Point(rect.X + borderpad, rect.Top + borderpad);
                    Point right = new Point(rect.Right - borderpad, rect.Bottom - borderpad);
                    pevent.Graphics.DrawLine(pen, left, right);
                    left = new Point(rect.Right - borderpad, rect.Top + borderpad);
                    right = new Point(rect.Left + borderpad, rect.Bottom - borderpad);
                    pevent.Graphics.DrawLine(pen, left, right);
                }
            }

 
        }
    }


    //public class MinButton : MyButton
    //{
    //    public MinButton()
    //    {
    //        //this.Paint += new PaintEventHandler(this.OnPaint1);
    //    }
    //    //private void OnPaint1(object o, PaintEventArgs pea)
    //    //{

    //    //}
    //    protected override void OnPaint(PaintEventArgs pevent)
    //    {
    //        base.OnPaint(pevent);
    //        using (Pen pen = new Pen(UITheme.MainWindowBackColor, 2))
    //        {
    //            Point left = new Point(pevent.ClipRectangle.X, pevent.ClipRectangle.Y);
    //            Point right = new Point(pevent.ClipRectangle.Right, pevent.ClipRectangle.Bottom);
    //            pevent.Graphics.DrawLine(pen, left, right);
    //        }
    //    }
    //}
}
