//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MadPiranha.FlatForm.Controls
{
    public partial class MySplitterContainer : SplitContainer
    {

        private MadPiranha.FlatForm.Controls.MyButton buttonForPanel1;
        private MadPiranha.FlatForm.Controls.MyButton buttonForPanel2;

        private int prevSplitterDist = 0;
        private int maxSplitterDist = 0;

        public MySplitterContainer()
        {
            InitializeComponent();
        }

        private void SplitterButtons_MouseLeave(object sender, EventArgs a)
        {
            ((Control)sender).Visible = false;
        }

        private void Splitter_MouseEnter(object e, EventArgs a)
        {
            Rectangle panel1buttonParentRect = buttonForPanel1.Parent.RectangleToScreen(new Rectangle());
            Rectangle panel2buttonParentRect = buttonForPanel2.Parent.RectangleToScreen(new Rectangle());

            if (this.Orientation == Orientation.Vertical)
            {
                buttonForPanel1.Width = 12;
                buttonForPanel1.Height = 48;
                buttonForPanel2.Width = 12;
                buttonForPanel2.Height = 48;

                buttonForPanel1.Location = new Point((Cursor.Position.X - panel1buttonParentRect.X) - buttonForPanel1.Bounds.Width - buttonForPanel1.Margin.Right, (Cursor.Position.Y - panel1buttonParentRect.Y) - (buttonForPanel1.Bounds.Height / 2));
                buttonForPanel2.Location = new Point(0, (Cursor.Position.Y - panel2buttonParentRect.Y) - (buttonForPanel2.Bounds.Height / 2));
                buttonForPanel1.Text = "<";
                buttonForPanel2.Text = ">";
            }
            else
            {
                buttonForPanel1.Width = 48;
                buttonForPanel1.Height = 12;
                buttonForPanel2.Width = 48;
                buttonForPanel2.Height = 12;

                buttonForPanel1.Location = new Point((Cursor.Position.X - panel1buttonParentRect.X) - (buttonForPanel1.Bounds.Width/2), (Cursor.Position.Y - panel1buttonParentRect.Y) - buttonForPanel1.Bounds.Height-buttonForPanel1.Margin.Bottom);
                buttonForPanel2.Location = new Point((Cursor.Position.X - panel2buttonParentRect.X) - (buttonForPanel2.Bounds.Width / 2), 0);
                buttonForPanel1.Text = "^";
                buttonForPanel2.Text = "v";

            }

            buttonForPanel1.Visible = true;
            buttonForPanel2.Visible = true;

            Splitter_MouseLeave(e, a);
        }

        private void Splitter_MouseLeave(object e, EventArgs a)
        {
            new Thread(new ThreadStart(delegate()
            {
                Thread.Sleep(1000);
                //TODO: Exception Invoke or BeginInvoke cannot be called on a control until the window handle has been created.
                //Happens when exit
                if (!buttonForPanel1.IsDisposed && !!buttonForPanel2.IsDisposed)
                {
                    buttonForPanel1.Invoke(new MethodInvoker(delegate()
                    {
                        if (!buttonForPanel1.RectangleToScreen(new Rectangle(0, 0, buttonForPanel1.Width, buttonForPanel1.Height)).Contains(Cursor.Position))
                            buttonForPanel1.Visible = false;
                    }));
                    buttonForPanel2.Invoke(new MethodInvoker(delegate()
                    {
                        if (!buttonForPanel2.RectangleToScreen(new Rectangle(0, 0, buttonForPanel2.Width, buttonForPanel2.Height)).Contains(Cursor.Position))
                            buttonForPanel2.Visible = false;
                    }));
                }
            })).Start();
        }

        private void buttonForPanel1_Click(object sender, EventArgs e)
        {
            if (this.SplitterDistance == maxSplitterDist)
            {
                this.SplitterDistance = prevSplitterDist;
            }
            else
            {
                prevSplitterDist = this.SplitterDistance;
                this.SplitterDistance = 0;
            }
        }

        private void buttonForPanel2_Click(object sender, EventArgs e)
        {
            if (this.SplitterDistance == 0)
            {
                this.SplitterDistance = prevSplitterDist;
            }
            else
            {
                prevSplitterDist = this.SplitterDistance;
                //TODO:
                this.SplitterDistance = 10000;
                maxSplitterDist = this.SplitterDistance;
            }
        }

        private void Splitter_DoubleClick(object sender, MouseEventArgs e)
        {
            Control child;
            if (e.Button == MouseButtons.Left && this.Panel1.Controls.Count > 0 && (child = this.Panel1.Controls[0]) != null && child is Panel)
            {
                
                if (this.Orientation == Orientation.Horizontal)
                {
                    this.SplitterDistance = child.Bounds.Height;
                }
                else if (this.Orientation == Orientation.Vertical)
                {
                    this.SplitterDistance = child.Bounds.Width;
                }

            }
        }


        private void InitializeComponent()
        {
            this.buttonForPanel1 = new MadPiranha.FlatForm.Controls.MyButton();
            this.buttonForPanel2 = new MadPiranha.FlatForm.Controls.MyButton();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            this.SuspendLayout();


            this.Panel1.Controls.Add(this.buttonForPanel1);
            this.Panel2.Controls.Add(this.buttonForPanel2);
            
            this.buttonForPanel1.Name = "buttonPanel1";
            this.buttonForPanel1.Size = new System.Drawing.Size(24, 24);
            this.buttonForPanel1.UseVisualStyleBackColor = true;
            this.buttonForPanel1.Visible = false;
            this.buttonForPanel1.Click += new System.EventHandler(this.buttonForPanel1_Click);
            // 
            // buttonPanel2
            // 
            this.buttonForPanel2.Name = "buttonPanel2";
            this.buttonForPanel2.Size = new System.Drawing.Size(24, 24);
            this.buttonForPanel2.UseVisualStyleBackColor = true;
            this.buttonForPanel2.Visible = false;
            this.buttonForPanel2.Click += new System.EventHandler(this.buttonForPanel2_Click);

            this.FixedPanel = FixedPanel.Panel1;

//            this.MouseEnter += new EventHandler(this.Splitter_MouseEnter);
//            this.MouseLeave += new EventHandler(this.Splitter_MouseLeave);
            this.MouseDoubleClick += new MouseEventHandler(this.Splitter_DoubleClick);

            buttonForPanel1.MouseLeave += new EventHandler(this.SplitterButtons_MouseLeave);
            buttonForPanel2.MouseLeave += new EventHandler(this.SplitterButtons_MouseLeave);

            // 
            // MySplitterContainer
            // 
            this.Panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.ResumeLayout(false);

        }

    }
}
