//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MadPiranha.Plugster.Util;

namespace MadPiranha.Plugster.Util
{
    public class WindowInfo
    {
        private int pid;
        public int Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        private int tid;
        public int Tid
        {
            get { return tid; }
            set { tid = value; }
        }

        private string pname;
        public string ProcessName
        {
            get { return pname; }
            set { pname = value; }
        }

        private string clsname;
        public string ClassName
        {
            get { return clsname; }
            set { clsname = value; }
        }

        private IntPtr hwnd;
        public IntPtr Hwnd
        {
            get { return hwnd; }
            set { hwnd = value; }
        }

        private string windowText;
        public string WindowText
        {
            get { return windowText; }
            set { windowText = value; }
        }

        public WindowInfo(IntPtr hWnd)
        {
            this.Hwnd = hWnd;

            this.ClassName = WindowAction.GetWindowClassName(hWnd);

            int winProcessId = 0;
            this.Tid = WindowAction.GetWindowThreadProcessId(hWnd, out winProcessId);
            this.Pid = winProcessId;

            this.ProcessName = Process.GetProcessById(winProcessId).ProcessName;
            this.WindowText = WindowAction.GetWindowText(hWnd);

        }

        public string GetString()
        {
            return "PID " + Pid
                + " : TID " + Tid
                + " : PNAME " + ProcessName
                + " : CLSNAME " + ClassName
                + " : HWNDI " + Hwnd
                + " : HWNDX " + Hwnd.ToString("X")
                + " : TEXT " + WindowText
            ;
        }

        public override string ToString()
        {
            return Pid
                + " : " + Tid
                + " : " + ProcessName
                + " : " + ClassName
                + " : " + Hwnd
                + " : " + Hwnd.ToString("X")
                + " : " + windowText;
        }
    }
}
