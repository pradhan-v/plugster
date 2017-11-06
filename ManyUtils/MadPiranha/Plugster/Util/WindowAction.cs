//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections;
using System.Drawing;


namespace MadPiranha.Plugster.Util
{
    public class WindowAction
    {
        
        public WindowAction()
        {
            
        }

        //Pulled from winuser.h
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;
        public const int GA_PARENT = 1;
        public const int GA_ROOT = 2;
        public const int GA_ROOTOWNER = 3;

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        public delegate bool EnumChildWindowsProc(IntPtr hWnd, IntPtr lParam);
        public static IntPtr statusbar;

        //[DllImport("oleacc.dll", EntryPoint = "AccessibleChildren", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //static public extern int AccessibleChildren(Accessibility.IAccessible acc, int start, int numChildren, [In, Out] object[] vals, out int numObtained);

        //[DllImport("oleacc.dll", EntryPoint = "AccessibleObjectFromWindow", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //static public extern int AccessibleObjectFromWindow(IntPtr hwnd, uint objectID, ref System.Guid guid, out Accessibility.IAccessible pAcc);

        //[DllImport("oleacc.dll", EntryPoint = "WindowFromAccessibleObject", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //static private extern uint WindowFromAccessibleObject(Accessibility.IAccessible pAcc, out IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "GetProcessDefaultLayout", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        static public extern bool GetProcessDefaultLayout(ref int layout);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        static public extern int GetWindowLongPtr(IntPtr hWnd, int index);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int RealGetWindowClass(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowsProc ewp, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindow", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint command);
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumChildWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point lpPoint);

        [DllImport("user32.dll", EntryPoint = "GetAncestor", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        static public extern IntPtr GetAncestor(IntPtr hWnd, uint flag);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public sealed class ClassLongIndex
        {
            static public int GCL_STYLE = -26;
        }

        public sealed class LayoutOrientation
        {
            static public int LAYOUT_RTL = 0x00000001;
        }

        public sealed class ExStyleBits
        {
            public static int WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
            public static int WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
        }

        public sealed class GetWindowLong
        {
            static public int GWL_WNDPROC = -4;
            static public int GWL_HINSTANCE = -6;
            static public int GWL_HWNDPARENT = -8;
            static public int GWL_STYLE = -16;
            static public int GWL_EXSTYLE = -20;
            static public int GWL_USERDATA = -21;
            static public int GWL_ID = -12;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public Int32 Left;
            public Int32 Top;
            public Int32 Right;
            public Int32 Bottom;

            public static RECT FromRectangle(Rectangle rectangle)
            {
                RECT win32rect = new RECT();
                win32rect.Top = rectangle.Top;
                win32rect.Bottom = rectangle.Bottom;
                win32rect.Left = rectangle.Left;
                win32rect.Right = rectangle.Right;
                return win32rect;
            }
            public static Rectangle ToRectangle(RECT rect)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.X = rect.Left;
                rectangle.Y = rect.Top;
                rectangle.Height = rect.Bottom - rect.Top; ;
                rectangle.Width = rect.Right - rect.Left;
                return rectangle;
            }
            public override string ToString()
            {
                return "{ " + Left + ", " + Top + ", " + Right + ", " + Bottom + " }";
            }
        }
        [DllImport("user32.dll")]
        public static extern long GetWindowRect(IntPtr hWnd, out RECT lpRect);//System.Windows.Rectangle does not work properly !

        public  List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumChildWindowsProc childProc = new EnumChildWindowsProc(EnumChildWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }


        private  bool EnumChildWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            // You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        public static string GetWindowText(IntPtr hWnd)
        {
            StringBuilder sb = new StringBuilder(255);
            WindowAction.GetWindowText(hWnd, sb, 255);
            return sb.ToString();
        }

        public static string GetWindowClassName(IntPtr hWnd)
        {
            StringBuilder sb = new StringBuilder(255);
            WindowAction.GetClassName(hWnd, sb, 255);
            return sb.ToString();
        }

        public static string GetWindowClassReal(IntPtr hWnd)
        {
            StringBuilder sb = new StringBuilder(255);
            WindowAction.RealGetWindowClass(hWnd, sb, 255);
            return sb.ToString();
        }

        private class ProcessHWinds
        {
            public ArrayList topWinds;
            public int processId;
        }
        public static IntPtr[] GetTopWindowsForProcess(int processId)
        {
            ProcessHWinds phw = new ProcessHWinds();
            ArrayList topwinds = new ArrayList();
            phw.processId = processId;
            phw.topWinds = topwinds;

            GCHandle gch = GCHandle.Alloc(phw);

            EnumWindowsProc callback = new EnumWindowsProc(EnumTopWindows);
            EnumWindows(callback, (IntPtr)gch);

            return (IntPtr[])topwinds.ToArray(typeof(IntPtr));
        }

        private static bool EnumTopWindows(IntPtr hWnd, IntPtr lParam)
        {
            int winProcessId = 0;
            WindowAction.GetWindowThreadProcessId(hWnd, out winProcessId);

            if (!IsWindow(hWnd) ||
                !WindowAction.IsWindowVisible(hWnd) ||
                hWnd == WindowAction.statusbar
               )
            {
                return true;
            }

            GCHandle gch = (GCHandle)lParam;
            ProcessHWinds phw = (ProcessHWinds)(gch.Target);
            if (!DoesWindowBelongToProcess(hWnd, phw.processId))
                return true;

            phw.topWinds.Add(hWnd);

            return true;
        }

        public static bool DoesWindowBelongToProcess(IntPtr hWnd, int processId)
        {
            int winProcessId = 0;
            WindowAction.GetWindowThreadProcessId(hWnd, out winProcessId);
            return (winProcessId == processId);
        }

        public static bool IsValidWindow(IntPtr hWnd)
        {
            if (!WindowAction.IsWindowVisible(hWnd) || hWnd == WindowAction.statusbar || !WindowAction.IsWindow(hWnd))
            {
                return false;
            }
            return true;
        }

        static public bool IsWindowMirrored(IntPtr hwnd)
        {
            int layout = 0;

            if (GetProcessDefaultLayout(ref layout))
            {
                int exstyle = GetWindowLongPtr(hwnd, GetWindowLong.GWL_EXSTYLE);

                if ((layout == LayoutOrientation.LAYOUT_RTL) ||
                    ((exstyle & ExStyleBits.WS_EX_LAYOUTRTL) == ExStyleBits.WS_EX_LAYOUTRTL))
                {
                    return (true);
                }
            }

            return (false);
        }


    }
}
