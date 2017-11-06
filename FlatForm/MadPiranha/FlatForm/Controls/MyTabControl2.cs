//Copyright © MadPiranha 2012-2013

//http://www.dotnetthoughts.net/2009/11/08/implementing-close-button-in-tab-pages/
//http://www.codeproject.com/Articles/20050/FireFox-like-Tab-Control
//http://dotnetrix.co.uk/tabcontrol.htm#tip2
//http://www.codeproject.com/Articles/12185/A-NET-Flat-TabControl-CustomDraw
//http://www.codeproject.com/Articles/13305/Painting-Your-Own-Tabs

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace MadPiranha.FlatForm.Controls
{
    /// <summary>
    /// Summary description for FlatTabControl.
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.TabControl))]
    public class MyTabControl2 : System.Windows.Forms.TabControl
    {

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private const int nMargin = 0;
        private Color mBackColor = UITheme.BackColor;
        private Color mBorderColor = UITheme.FlatAppearance_BorderColor;

        public delegate void OnHeaderCloseDelegate(object sender, CloseEventArgs e);
        public event OnHeaderCloseDelegate OnClose;

        public MyTabControl2()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            if (!this.tablook.Equals(TabControlLook.Default))
            {
                this.SetStyle(ControlStyles.UserPaint, true);
            }

            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tabControlTests_ControlAdded);
            // double buffering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        }

        private void tabControlTests_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.BackColor = this.mBackColor;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawControl(e.Graphics);
        }

        internal void DrawControl(Graphics g)
        {
            if (!Visible)
                return;

            Rectangle TabControlArea = this.ClientRectangle;
            Rectangle TabArea = this.DisplayRectangle;

            //DONT DELETE THIS, CODE NEEDED TO FILL COLOR TO TAB EMPTY AREA
            //----------------------------
            // fill client area
            //Brush br = new SolidBrush(mBackColor);
            //Brush br = new SolidBrush(UITheme.MainWindowBackColor);
            Rectangle tabrect = TabControlArea;
            tabrect.Height = this.GetTabRect(0).Height+5;
            Brush br = new LinearGradientBrush(
                                tabrect, 
                                UITheme.MainWindowBackColor, 
                                UITheme.GetLighterColor(UITheme.MainWindowBackColor, 20), 
                                LinearGradientMode.Vertical);
            g.FillRectangle(br, TabControlArea);
            br.Dispose();
            //----------------------------

            //----------------------------
            // draw border
            int nDelta = SystemInformation.Border3DSize.Width;

            Pen border = new Pen(mBorderColor) ;
            TabArea.Inflate(nDelta, nDelta);
            g.DrawRectangle(border, TabArea);
            border.Dispose();

            //Pen pshadow = new Pen(UITheme.GetLighterColor(mBorderColor, 100));
            //Rectangle rshadow = TabArea;
            //rshadow.Inflate(1, 1);
            //g.DrawRectangle(pshadow, rshadow);
            //pshadow.Dispose();
            //----------------------------


            //----------------------------
            // clip region for drawing tabs
            Region rsaved = g.Clip;
            Rectangle rreg;

            int nWidth = TabArea.Width + nMargin;

            rreg = new Rectangle(TabArea.Left, TabControlArea.Top, nWidth - nMargin, TabControlArea.Height);

            g.SetClip(rreg);

            // draw tabs
            for (int i = 0; i < this.TabCount; i++)
                DrawTab(g, this.TabPages[i], i);

            g.Clip = rsaved;
            //----------------------------


            //----------------------------
            // draw background to cover flat border areas
            if (this.SelectedTab != null)
            {
                TabPage tabPage = this.SelectedTab;
                Color color = tabPage.BackColor;
                border = new Pen(color);

                TabArea.Offset(1, 1);
                TabArea.Width -= 2;
                TabArea.Height -= 2;

                g.DrawRectangle(border, TabArea);
                TabArea.Width -= 1;
                TabArea.Height -= 1;
                g.DrawRectangle(border, TabArea);

                border.Dispose();
            }
            //----------------------------
        }

        internal void DrawTab(Graphics g, TabPage tabPage, int nIndex)
        {
            Rectangle recBounds = this.GetTabRect(nIndex);
            RectangleF tabTextArea = (RectangleF)this.GetTabRect(nIndex);

            bool bSelected = (this.SelectedIndex == nIndex);

            Point[] borderPts = GetTabHeaderBorderPoints(recBounds);

            //----------------------------
            // fill this tab with background color
            Brush br = new SolidBrush(tabPage.BackColor);
            g.FillPolygon(br, borderPts);
            br.Dispose();
            //----------------------------

            //----------------------------
            // draw border
            Pen borderPen = new Pen(mBorderColor);
            g.DrawPolygon(borderPen, borderPts);
            borderPen.Dispose();

            if (bSelected)
            {
                //----------------------------
                // clear bottom lines
                Pen pen = new Pen(tabPage.BackColor);

                switch (this.Alignment)
                {
                    case TabAlignment.Top:
                        g.DrawLine(pen, recBounds.Left + 1, recBounds.Bottom, recBounds.Right - 1, recBounds.Bottom);
                        g.DrawLine(pen, recBounds.Left + 1, recBounds.Bottom + 1, recBounds.Right - 1, recBounds.Bottom + 1);
                        break;

                    case TabAlignment.Bottom:
                        g.DrawLine(pen, recBounds.Left + 1, recBounds.Top, recBounds.Right - 1, recBounds.Top);
                        g.DrawLine(pen, recBounds.Left + 1, recBounds.Top - 1, recBounds.Right - 1, recBounds.Top - 1);
                        g.DrawLine(pen, recBounds.Left + 1, recBounds.Top - 2, recBounds.Right - 1, recBounds.Top - 2);
                        break;
                }

                pen.Dispose();
                //----------------------------
            }
            //----------------------------

            //----------------------------
            // draw string
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            br = new SolidBrush(tabPage.ForeColor);

            g.DrawString(tabPage.Text, Font, br, tabTextArea, stringFormat);
            //----------------------------

            if (nIndex != this.SelectedIndex)
            {
                //DrawCloseButton(tabTextArea, g, Brushes.Gray, Pens.White);
            }
            else
            {
                DrawCloseButton(nIndex, g, Color.Black);
                //DrawCloseButton(tabTextArea, g, System.Drawing.Brushes.Red, Pens.White);
            }

        }

        private Point[] GetTabHeaderBorderPoints(Rectangle recBounds)
        {
            Point[] pt = new Point[5];
            if (this.Alignment == TabAlignment.Top)
            {
                pt[0] = new Point(recBounds.Left, recBounds.Bottom);
                pt[1] = new Point(recBounds.Left, recBounds.Top);
                pt[2] = new Point(recBounds.Right, recBounds.Top);
                pt[3] = new Point(recBounds.Right, recBounds.Bottom);
                pt[4] = new Point(recBounds.Left, recBounds.Bottom);
            }
            else
            {
                pt[0] = new Point(recBounds.Left, recBounds.Top);
                pt[1] = new Point(recBounds.Right, recBounds.Top);
                pt[2] = new Point(recBounds.Right, recBounds.Bottom);
                pt[3] = new Point(recBounds.Left, recBounds.Bottom);
                pt[4] = new Point(recBounds.Left, recBounds.Top);
            }

            ////Rounded top border
            //Point[] pt = new Point[7];
            //if (this.Alignment == TabAlignment.Top)
            //{
            //    pt[0] = new Point(recBounds.Left, recBounds.Bottom);
            //    pt[1] = new Point(recBounds.Left, recBounds.Top + 3);
            //    pt[2] = new Point(recBounds.Left + 3, recBounds.Top);
            //    pt[3] = new Point(recBounds.Right - 3, recBounds.Top);
            //    pt[4] = new Point(recBounds.Right, recBounds.Top + 3);
            //    pt[5] = new Point(recBounds.Right, recBounds.Bottom);
            //    pt[6] = new Point(recBounds.Left, recBounds.Bottom);
            //}
            //else
            //{
            //    pt[0] = new Point(recBounds.Left, recBounds.Top);
            //    pt[1] = new Point(recBounds.Right, recBounds.Top);
            //    pt[2] = new Point(recBounds.Right, recBounds.Bottom - 3);
            //    pt[3] = new Point(recBounds.Right - 3, recBounds.Bottom);
            //    pt[4] = new Point(recBounds.Left + 3, recBounds.Bottom);
            //    pt[5] = new Point(recBounds.Left, recBounds.Bottom - 3);
            //    pt[6] = new Point(recBounds.Left, recBounds.Top);
            //}
            return pt;
        }

        private void DrawCloseButton(int tabIndex, Graphics g, Color cross)
        {
            //TODO:create a new tabpage type and get the 'close enabled' flag
            if (tabIndex == 0) return;
            RectangleF tabTextArea = (RectangleF)this.GetTabRect(tabIndex);
            RectangleF rect = GetCloseButtonRect(tabTextArea);
            using (Pen pen = new Pen(cross, 2))
            {
                g.DrawLine(pen, rect.X + 3, rect.Y + 3, rect.X + rect.Width - 3, rect.Y + rect.Width - 3);
                g.DrawLine(pen, rect.X + rect.Width - 3, rect.Y + 3, rect.X + 3, rect.Y + rect.Width - 3);
            }
        }

        private RectangleF GetCloseButtonRect(RectangleF tabTextArea)
        {
            int w = (int)(tabTextArea.Height / 2);
            int x = (int)(tabTextArea.X + tabTextArea.Width - w); int y = 2;
            return new Rectangle(x, y, w, w);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            //TODO:create a new tabpage type and get the 'close enabled' flag
            if (SelectedIndex == 0) return;

            if (!DesignMode)
            {
                RectangleF tabTextArea = (RectangleF)this.GetTabRect(SelectedIndex);
                RectangleF closeButtonArea = GetCloseButtonRect(tabTextArea);
                Point pt = new Point(e.X, e.Y);
                if (closeButtonArea.Contains(pt))
                {
                    if (confirmOnClose)
                    {
                        DialogResult res = MessageBox.Show(this.TopLevelControl, "Do you really want to close " + this.TabPages[SelectedIndex].Text.TrimEnd() + " ?", "Close", MessageBoxButtons.YesNo);
                        if (res == DialogResult.No)
                            return;
                    }
                    //Fire Event to Client
                    if (OnClose != null)
                    {
                        OnClose(this, new CloseEventArgs(SelectedIndex));
                    }
                }
              
            }
        }


        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }


        #endregion

        #region Properties

        private bool confirmOnClose = true;
        public bool ConfirmOnClose
        {
            get
            {
                return this.confirmOnClose;
            }
            set
            {
                this.confirmOnClose = value;
            }
        }

        public new TabPageCollection TabPages
        {
            get
            {
                return base.TabPages;
            }
        }

        new public TabAlignment Alignment
        {
            get { return base.Alignment; }
            set
            {
                TabAlignment ta = value;
                if ((ta != TabAlignment.Top) && (ta != TabAlignment.Bottom))
                    ta = TabAlignment.Top;

                base.Alignment = ta;
            }
        }

        [Browsable(false)]
        new public bool Multiline
        {
            get { return base.Multiline; }
            set { base.Multiline = false; }
        }

        [Browsable(true)]
        public Color MyBackColor
        {
            get { return mBackColor; }
            set { mBackColor = value; this.Invalidate(); }
        }

        public enum TabControlLook
        {
            Default,
            Flat
        }

        TabControlLook tablook = TabControlLook.Flat;// UITheme.TabLook;

        [System.ComponentModel.DefaultValue(typeof(TabControlLook), "Flat")]
        public TabControlLook TabLook
        {
            get
            {
                return this.tablook;
            }
            set
            {
                if (this.tablook != value)
                {
                    if (!this.tablook.Equals(TabControlLook.Default))
                    {
                        this.SetStyle(ControlStyles.UserPaint, true);
                    }
                    else
                    {
                        this.SetStyle(ControlStyles.UserPaint, false);
                    }
                }
            }
        }

        #endregion
    
    }

    public class CloseEventArgs : EventArgs
    {
        private int nTabIndex = -1;
        public CloseEventArgs(int nTabIndex)
        {
            this.nTabIndex = nTabIndex;
        }

        public int TabIndex
        {
            get
            {
                return this.nTabIndex;
            }
            set
            {
                this.nTabIndex = value;
            }
        }

    }

}
