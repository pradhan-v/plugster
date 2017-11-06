//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;

using MadPiranha.Plugster.Util;
using MadPiranha.Plugster.Base.Param;
using MadPiranha.Plugster.Base.Test;
using MadPiranha.Plugster.Base.Output;



namespace MadPiranha.PlugsterTests.Test
{
    public class TestWindows : AbstractTest
    {
        protected TextParam filter;
        protected BoolParam allTopWins;
        protected CoOrdinatesParam xyParam;
        protected BoolParam printParent;
        protected BoolParam printChildren;

        private List<WindowInfo> windows;
        protected List<WindowInfo> ListedWindows
        {
            get { return windows; }
        }

        private bool beSilent;
        protected bool BeSilent
        {
            get { return beSilent; }
            set 
            { 
                beSilent = value;
                base.silent = value;
            }
        }

        public TestWindows()
        {
            windows = new List<WindowInfo>();

            xyParam = new CoOrdinatesParam();
            printParent = new BoolParam("Print Parent", false);
            printChildren = new BoolParam("Print Children", false);
            filter = new TextParam("Filter", "");
            allTopWins = new BoolParam("-Only Print TopWindows", false);
            parameters = new IParam[] { xyParam, printParent, printChildren, allTopWins, filter };
        }

        public TestWindows(IOutput output)
        {
            SetOutput(output);
        }

        #region ITest Members

        public override string GetDescription()
        {
            return "Some stuffs to get window info.";
        }

        public override void ExecuteThis()
        {
            windows.Clear();

            WriteLine("PID <WINDOW PROCESS ID>"
                + " : TID <WINDOW THREAD ID>"
                + " : PNAME <WINDOW PROCESS NAME>" 
                + " : CLSNAME <CLASSNAME>"
                + " : HWNDI <WINDOW HANDLE INT>"
                + " : HWNDX <WINDOW HANDLE HEX>"
                + "\n");
            
            if(allTopWins.BoolValue)
            {
                PrintTopLevelWindows();
            }
            else 
            {
                System.Drawing.Point pt = new System.Drawing.Point(xyParam.X, xyParam.Y);
                WriteLine("Window at " + pt);
                IntPtr hWnd = WindowAction.WindowFromPoint(pt);

                if(!BeSilent)
                    Utils.HighlightWindow(hWnd);
                
                PrintWindow(hWnd, IntPtr.Zero);

                if (printParent.BoolValue)
                {
                    WriteLine("\nPrinting Parent hierarchy...");
                    IntPtr parent = hWnd;
                    while (parent != null && parent != IntPtr.Zero )
                    {
                        PrintWindow(parent, IntPtr.Zero);
                        parent = WindowAction.GetAncestor(parent, WindowAction.GA_PARENT);
                    }
                }

                if (printChildren.BoolValue)
                {
                    WriteLine("\nPrinting Children hierarchy...");
                    PrintChildren(hWnd);
                }
            }
           
        }

        #endregion

        private void PrintTopLevelWindows()
        {
            WriteLine("Printing top windows...(FILTER <...>)");

            GCHandle gch = GCHandle.Alloc(windows);

            WindowAction.EnumWindowsProc callback = new WindowAction.EnumWindowsProc(PrintWindow);
            WindowAction.EnumWindows(callback, (IntPtr)gch);
        }

        int c = 0;
        public void PrintChildren(IntPtr hWnd)
        {
            for (int s = 0; s < c; s++) output.Write("  ");
            PrintWindow(hWnd, IntPtr.Zero);

            IntPtr child = WindowAction.GetWindow(hWnd, WindowAction.GW_CHILD);
            
            if (child != null && child != IntPtr.Zero)
            {
                c++;
                for (IntPtr chWnd = WindowAction.GetWindow(child, WindowAction.GW_HWNDFIRST);
                        chWnd != IntPtr.Zero;
                        chWnd = WindowAction.GetWindow(chWnd, WindowAction.GW_HWNDNEXT))
                {
                    if (!WindowAction.IsValidWindow(chWnd))
                    { continue; }

                    PrintChildren(chWnd);
                }
                c--;
            }
        }


        private bool PrintWindow(IntPtr hWnd, IntPtr lParam)
        {
            WindowInfo winfo = new WindowInfo(hWnd);

            String filtertext = filter.TextValue.Trim();

            if (filtertext.Length == 0 || winfo.GetString().ToLower().Contains(filtertext.ToLower()))
            {
                WriteLine(winfo.ToString());

                //GCHandle gch = GCHandle.FromIntPtr(lParam);
                //ArrayList winfos = (ArrayList)gch.Target;
                windows.Add(winfo);
            }

            return true;
        }
 
    }
}
