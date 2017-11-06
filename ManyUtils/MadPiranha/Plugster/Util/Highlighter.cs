//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Windows;
using System.Drawing;

namespace MadPiranha.Plugster.Util
{
    public class Highlighter
    {
        Form hWindow;
        Panel panel;

        private int flashCount;
        public int FlashCount
        {
            get { return flashCount; }
            set { flashCount = value; }
        }

        private int flashInterval;
        public int FlashInterval
        {
            get { return flashInterval; }
            set { flashInterval = value; }
        }

        public Color FlashColor
        {
            get { return hWindow.BackColor; }
            set 
            { 
                hWindow.BackColor = value; 
                if(!Hollow)
                    panel.BackColor = hWindow.BackColor; 
            }
        }

        public bool Hollow
        {
            get { return panel.BackColor == hWindow.TransparencyKey; }
            set 
            {
                if (value && !Hollow)
                    panel.BackColor = hWindow.TransparencyKey;
                else if (!value && Hollow)
                    panel.BackColor = hWindow.BackColor;                    
            }
        }

        public double FlashOpacity
        {
            get { return hWindow.Opacity; }
            set { hWindow.Opacity = value; }
        }

        public int FlashBorder
        {
            get { return hWindow.Padding.All; }
            set { hWindow.Padding = new Padding(value); }
        }

        public Highlighter()
        {
            InitWindow();

            this.FlashCount = 3;
            this.FlashInterval = 200;
            this.FlashColor = Color.Red;
            this.Hollow = true;
            this.FlashOpacity = 1.0;
            this.FlashBorder = 2;
        }

        public Highlighter(Color color, bool hollow, int flashCount, int flashInterval, double flashOpacity, int border)
        {
            InitWindow();

            this.FlashCount = flashCount;
            this.FlashInterval = flashInterval;
            this.FlashColor = color;
            this.Hollow = hollow;
            this.FlashOpacity = flashOpacity;
            this.FlashBorder = border;
        }

        private void InitWindow()
        {
            hWindow = new Form();
            //hWindow.Text = "";
            hWindow.ControlBox = false;
            hWindow.TopMost = true;
            hWindow.ShowInTaskbar = false;
            hWindow.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            hWindow.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            hWindow.TransparencyKey = Color.White;

            panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BackColor = hWindow.TransparencyKey;

            hWindow.Controls.Add(panel);
        }


        public void SetBounds(Rectangle rect)
        {
            rect.Inflate(FlashBorder, FlashBorder);
            hWindow.Bounds = rect;
            //hWindow.Location = new System.Drawing.Point(rect.X, rect.Y);
            //hWindow.Size = new System.Drawing.Size(rect.Width, rect.Height);
        }

        public void Show()
        {
            hWindow.Show();
        }

        public void Hide()
        {
            hWindow.Hide();
        }

        Thread flashThread;
        //public void FlashThread(Rect rect)
        //{
        //    FlashThread(new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));
        //}
        public void FlashThread(Rectangle rect)
        {
            if (flashThread!=null && ThreadState.Running == flashThread.ThreadState)
            {
                throw new Exception("Flashing in progress");
            }

            if (flashThread == null || (flashThread != null && ThreadState.Running != flashThread.ThreadState))
                flashThread = new Thread(this.FlashWindow);

            if (ThreadState.Unstarted == flashThread.ThreadState)
            {
                flashThread.Start(rect);
            }

        }

        public bool Flash(Rectangle rect)
        {
            if (rect.IsEmpty || rect.Width == 0 || rect.Height == 0) return false;
            FlashWindow(rect);
            return true;
        }

        //public void Flash(Rect rect)
        //{
        //    Rectangle r = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        //    Flash(r);
        //}

        private void FlashWindow(object rect)
        {
            SetBounds((Rectangle)rect);
            for(int i=0; i<FlashCount; i++)
            {
                Show();
                Thread.Sleep(FlashInterval);
                Hide();
                if(i!=FlashCount-1)
                    Thread.Sleep(FlashInterval);
            }
        }

    }
}
