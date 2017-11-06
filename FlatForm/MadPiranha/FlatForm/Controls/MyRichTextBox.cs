//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MadPiranha.FlatForm.Controls
{
    public class MyRichTextBox : RichTextBox
    {

        public MyRichTextBox()
        {
            InitializeComponent();
            //if (UITheme.BorderStyle == UITheme.TextBorderStyle)
            //{
            //    this.BorderStyle = BorderStyle.None;
            //}            
        }

        private void InitializeComponent()
        {
            // 
            // richTextBox1
            // 
            this.AcceptsTab = true;
            base.BackColor = UITheme.TextBackColor;
            base.BorderStyle = UITheme.TextBorderStyle; 
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "richTextBox1";
            this.Size = new System.Drawing.Size(410, 236);
            this.TabIndex = 1;
            this.Text = "";
            this.WordWrap = false;
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OutputWindow_DoubleClick);
        }

        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { }
        }

        public new Color BackColor
        {
            get { return base.BackColor; }
            set { }
        }



        #region UI events

        private void OutputWindow_DoubleClick(object sender, MouseEventArgs args)
        {
            //Rectangle xyLoc = this.RectangleToScreen(new System.Drawing.Rectangle());
            if (args.Button == MouseButtons.Left && (
                (Control.ModifierKeys & Keys.Control) == Keys.Control ||
                (args.X > this.Bounds.Width - 30 && args.Y > this.Bounds.Height - 30))
                )
            {
                this.Clear();
            }
        }

        private void YourRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control && e.KeyCode == Keys.I)
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        private void DrawBorder()
        {
            if (UITheme.TextBorderStyle == BorderStyle.FixedSingle)
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
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    {
                        //base.WndProc(ref m);
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
