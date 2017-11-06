//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MadPiranha.Plugster.Util
{
    public class MenuAction
    {
        [DllImport("user32")]
        public static extern bool IsMenu(IntPtr hMenu);

        [DllImport("kernel32.dll", EntryPoint = "GetLastError", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        static public extern uint GetLastError();

        [DllImport("user32.dll", EntryPoint = "GetMenu", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        static public extern IntPtr GetMenu(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "MenuItemFromPoint", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        static public extern int MenuItemFromPoint(IntPtr hWnd, IntPtr hMenu, POINT ptScreen);

        [DllImport("user32.dll", EntryPoint = "GetMenuItemInfo", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        static public extern bool GetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition, ref MENUITEMINFO mii);

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        static public extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll", EntryPoint = "GetMenuItemRect", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        static public extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint uItem, ref MadPiranha.Plugster.Util.WindowAction.RECT rect);

        [DllImport("user32.dll", EntryPoint = "GetSubMenu", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        static public extern IntPtr GetSubMenu(IntPtr hWnd, int nPos);

        [DllImport("user32.dll", EntryPoint = "GetMenuBarInfo", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        static public extern bool GetMenuBarInfo(IntPtr hWnd, long idObject, long idItem, ref MENUBARINFO pmbi);

        [System.Runtime.InteropServices.ComVisible(false)]
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        public const uint MIIM_STATE = 0x00000001;
        public const uint MIIM_ID = 0x00000002;
        public const uint MIIM_SUBMENU = 0x00000004;
        public const uint MIIM_CHECKMARKS = 0x00000008;
        public const uint MIIM_TYPE = 0x00000010;
        public const uint MIIM_DATA = 0x00000020;
        public const uint MIIM_STRING = 0x00000040;
        public const uint MIIM_BITMAP = 0x00000080;
        public const uint MIIM_FTYPE = 0x00000100;

        public const uint MN_GETHMENU = 0x01E1;

        [System.Runtime.InteropServices.ComVisible(false)]
        [StructLayout(LayoutKind.Sequential)]
        public struct MENUITEMINFO
        {
            public uint cbSize;
            public uint fMask;
            public uint fType;
            public uint fState;
            public uint wID;
            public IntPtr hSubMenu;
            public IntPtr hbmpChecked;
            public IntPtr hbmpUnchecked;
            public IntPtr dwItemData;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string dwTypeData;
            public uint cch;
            public IntPtr hbmpItem;
        }

        [System.Runtime.InteropServices.ComVisible(false)]
        [StructLayout(LayoutKind.Sequential)]
        public struct MENUBARINFO
        {
            public uint cbSize;
            public MadPiranha.Plugster.Util.WindowAction.RECT rcBar;
            public IntPtr hMenu;
            public IntPtr hwndMenu;
            public bool fBarFocused;
            public bool fFocused;
        }

        public static IntPtr GetMenuHandle(IntPtr hWnd)
        {
            IntPtr hMenu = GetMenu(hWnd);

            if (hMenu == IntPtr.Zero)
            {
                hMenu = (IntPtr)WindowAction.SendMessage(hWnd, MN_GETHMENU, IntPtr.Zero, IntPtr.Zero);
            }

            return (hMenu);
        }

        public static int MenuItemLocationFromPoint(IntPtr hWnd, IntPtr hMenu, System.Drawing.Point screenPt)
        {
            POINT ptScreen;
            ptScreen.x = screenPt.X;
            ptScreen.y = screenPt.Y;
            return (MenuItemFromPoint(hWnd, hMenu, ptScreen));
        }

        //public static int MenuItemFromPoint(IntPtr hWnd, IntPtr hMenu, System.Drawing.Point screenPt)
        //{
        //    int x = MenuAction.MenuItemLocationFromPoint(hWnd, hMenu, screenPt);
        //    object o = MenuAction.GetMenuItemText(hMenu, (uint)x, true);
        //}

        public static object GetMenuItemData(IntPtr hMenu, uint uItem, bool fByPosition)
        {
            return GetMenuItemInfo(hMenu, uItem, fByPosition, MIIM_DATA);
        }

        public static object GetMenuItemText(IntPtr hMenu, uint uItem, bool fByPosition)
        {
            return GetMenuItemInfo(hMenu, uItem, fByPosition, MIIM_STRING);
        }

        public static object GetMenuItemBitmap(IntPtr hMenu, uint uItem, bool fByPosition)
        {
            return GetMenuItemInfo(hMenu, uItem, fByPosition, MIIM_BITMAP);
        }

        private static object GetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition, uint fMask)
        {
            object obj = null;

            MENUITEMINFO mii = new MENUITEMINFO();
            mii.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(mii);
            mii.fMask = fMask;
            mii.dwTypeData = null;

            if (GetMenuItemInfo(hMenu, uItem, fByPosition, ref mii))
            {
                if (fMask == MIIM_STRING)
                {
                    string pszText = new string('\0', (int)mii.cch + 1);
                    mii.dwTypeData = pszText;
                    mii.cch = mii.cch + 1;
                    if (GetMenuItemInfo(hMenu, uItem, fByPosition, ref mii))
                    {
                        obj = mii.dwTypeData;
                    }
                }
                else if (fMask == MIIM_DATA)
                {
                    obj = mii.dwItemData;
                }
                else if (fMask == MIIM_STATE)
                {
                    obj = mii.fState;
                }
                else if (fMask == MIIM_FTYPE)
                {
                    obj = mii.fType;
                }
                else if (fMask == MIIM_BITMAP)
                {
                    obj = mii.hbmpItem.ToInt32();
                }
            }

            return (obj);
        }

        public static Rectangle GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint uItem)
        {
            Rectangle mrect = Rectangle.Empty;
            MadPiranha.Plugster.Util.WindowAction.RECT rect = new MadPiranha.Plugster.Util.WindowAction.RECT();
            if (GetMenuItemRect(hWnd, hMenu, uItem, ref rect))
            {
                mrect = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);

                MadPiranha.Plugster.Util.WindowAction.RECT winRect = new MadPiranha.Plugster.Util.WindowAction.RECT();

                if (WindowAction.IsWindowMirrored(hWnd) && WindowAction.GetWindowRect(hWnd, out winRect)!=0)
                {
                    int dist = mrect.Left - winRect.Left;
                    mrect = new Rectangle(winRect.Right - dist - mrect.Width,
                                                        mrect.Y,
                                                        mrect.Width,
                                                        mrect.Height);
                }
            }

            return mrect;
        }

        public static string[] GetChildren(IntPtr hWnd, IntPtr hMenu)
        {
            if (hMenu == IntPtr.Zero)
            {
                hMenu = GetMenu(hWnd);
            }

            System.Collections.ArrayList children = new System.Collections.ArrayList();

            int count = GetMenuItemCount(hMenu);
            for (int i = 0; i < count; i++)
            {
                object obj = GetMenuItemText(hMenu, (uint)i, true);
                if (obj is string)
                {
                    children.Add((string)obj);
                }
            }

            return (string[])children.ToArray(typeof(string));
        }

    }
}
