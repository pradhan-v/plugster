//Copyright © MadPiranha 2012-2013

using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MadPiranha.Plugster.UI
{

    public partial class TheFlatForm : Form
    {
        public TheFlatForm()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.FlatForm_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FlatForm_MoveWindow);

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static private extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        #region Shadow

        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_DROPSHADOW;
                return cp;
            }
        }

        #endregion

        #region UI methods

        private void InvokeOnControl(Control control, MethodInvoker del)
        {
            if (control.InvokeRequired)
                control.Invoke(del);
            else
                del.Invoke();
        }


        private void ShowApp()
        {
            ////woraround to bring window to focus
            //FormWindowState state = this.WindowState;
            //this.WindowState = FormWindowState.Minimized;
            //this.WindowState = state == FormWindowState.Minimized ? FormWindowState.Normal : state;

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            bool top = TopMost;
            TopMost = true;
            TopMost = top;
        }

        #endregion

        #region Window Move and Resize

        private static int SIZE_INCREASE = 50;
        //private static int RESIZE_CORNER = 20;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private enum WindowCursorPostion : uint
        {
            HTTOPLEFT = 0x0D,
            HTTOP = 0x0C,
            HTTOPRIGHT = 0x0E,
            HTRIGHT = 0x0B,
            HTBOTTOMRIGHT = 0x11,
            HTBOTTOM = 0x0F,
            HTBOTTOMLEFT = 0x10,
            HTLEFT = 0x0A,
            NOWHERE = 0x0
        }

        private WindowCursorPostion IsResizeCursorSet(int x, int y)
        {
            int CORNERS = 20;

            if ( (x < this.Padding.Left && y < CORNERS) || (x<CORNERS && y<this.Padding.Top) )
            {
                this.Cursor = Cursors.SizeNWSE;
                return WindowCursorPostion.HTTOPLEFT;
            }
            else if ((x > CORNERS && x < this.Bounds.Width - CORNERS) && (y < this.Padding.Top || y > this.Height-this.Padding.Bottom))
            {
                this.Cursor = Cursors.SizeNS;
                if (y >= this.Bounds.Height - this.Padding.Bottom)
                    return WindowCursorPostion.HTBOTTOM;
                else
                    return WindowCursorPostion.HTTOP;
            }
            else if ( (x > this.Bounds.Width - this.Padding.Right && y < CORNERS) || (x > this.Width-CORNERS && y<this.Padding.Top) )
            {
                this.Cursor = Cursors.SizeNESW;
                return WindowCursorPostion.HTTOPRIGHT;
            }
            else if ((y > CORNERS && y < this.Bounds.Height - CORNERS) && (x < this.Padding.Left || x > this.Width - this.Padding.Right))
            {
                this.Cursor = Cursors.SizeWE;
                if (x >= this.Bounds.Width - this.Padding.Right - 2)//2 px buffer, sometimes mouse goes crazy
                    return WindowCursorPostion.HTRIGHT;
                else
                    return WindowCursorPostion.HTLEFT;
            }
            else if ((x > this.Bounds.Width - this.Padding.Right && y > this.Bounds.Height - CORNERS) || (x>this.Width-CORNERS && y<this.Height-this.Padding.Bottom))
            {
                this.Cursor = Cursors.SizeNWSE;
                return WindowCursorPostion.HTBOTTOMRIGHT;
            }
            else if ((x < this.Padding.Left && y > this.Bounds.Height - CORNERS) || (x<CORNERS && y > this.Height-this.Padding.Bottom))
            {
                this.Cursor = Cursors.SizeNESW;
                return WindowCursorPostion.HTBOTTOMLEFT;
            }

            this.Cursor = Cursors.Default;
            return WindowCursorPostion.NOWHERE;

        }

        private void FlatForm_MoveWindow(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, new IntPtr(HT_CAPTION), IntPtr.Zero);
            }
        }

        private void FlatForm_AutoResizeWindow(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Size newSize = new Size(this.Size.Width + SIZE_INCREASE * 2, this.Size.Height + SIZE_INCREASE * 2); ;
                Rectangle screen = Screen.GetBounds(this.Bounds);
                if (screen.Width > newSize.Width && screen.Height > newSize.Height)
                {
                    this.Location = new Point(this.Location.X - SIZE_INCREASE, this.Location.Y - SIZE_INCREASE);
                    this.Size = newSize;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Size newSize = new Size(this.Size.Width - SIZE_INCREASE * 2, this.Size.Height - SIZE_INCREASE * 2); ;
                if (this.MinimumSize.Width < newSize.Width && this.MinimumSize.Height < newSize.Height)
                {
                    this.Location = new Point(this.Bounds.X + SIZE_INCREASE, this.Bounds.Y + SIZE_INCREASE);
                    if (WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }

                    this.Size = newSize;
                }
            }

        }

        private void FlatForm_MouseEnter(object sender, EventArgs e)
        {
            Point scpoint = System.Windows.Forms.Cursor.Position;
            IsResizeCursorSet(scpoint.X - this.Bounds.X, scpoint.Y - this.Bounds.Y);
        }

        private void FlatForm_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void FlatForm_MouseMove(object sender, MouseEventArgs e)
        {
            WindowCursorPostion pos = IsResizeCursorSet(e.X, e.Y);
            if (pos != WindowCursorPostion.NOWHERE && e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, new IntPtr((int)pos), IntPtr.Zero);
            }
        }

        private void FlatForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal) this.WindowState = FormWindowState.Maximized;
            else if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
        }

        #endregion

        #region UI events

        private void FlatForm_Load(object sender, EventArgs e)
        {
            
        }

        private void FlatForm_Activated(object sender, EventArgs e)
        {
            //this.AllowTransparency = false;
            //this.Opacity = 1.0;
        }

        private void FlatForm_Deactivate(object sender, EventArgs e)
        {
            //if (!this.TopMost)
            //{
            //    this.AllowTransparency = true;
            //    this.Opacity = .8;
            //}
        }

        private void FlatForm_ControlAdded(object sender, ControlEventArgs e)
        {
            if (sender is Control)
            {
                Control control = ((Control)sender);
                AddMoveListeners(control);
            }

        }

        private void AddMoveListeners(Control control)
        {
            if (!(control is SplitContainer))
                control.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FlatForm_MoveWindow);
            control.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.FlatForm_ControlAdded);

            for (int i = 0; i < control.Controls.Count; i++)
            {
                if (control.Controls[i] is ScrollableControl)
                    AddMoveListeners(control.Controls[i]);
            }
        }

        #endregion

    }
}
