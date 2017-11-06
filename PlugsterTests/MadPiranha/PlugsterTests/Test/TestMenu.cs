//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Util;
using MadPiranha.Plugster.Base.Param;

namespace MadPiranha.PlugsterTests.Test
{
    public class TestMenu : TestWindows
    {
        public TestMenu()
        {
            parameters = new IParam[] { xyParam };
            //parameters = new IParam[] { xyParam, printParent, printChildren};
        }

        public override void ExecuteThis()
        {
            base.BeSilent = true;
            base.ExecuteThis();
            base.BeSilent = false;

            WindowInfo winfo = ListedWindows[0];
            IntPtr hMenu = MenuAction.GetMenuHandle(winfo.Hwnd);

            WriteLine("Window:" + winfo);
            if (MenuAction.IsMenu(hMenu))
            {
                int x = MenuAction.MenuItemLocationFromPoint(winfo.Hwnd, hMenu, new System.Drawing.Point(xyParam.X, xyParam.Y));
                object o = MenuAction.GetMenuItemText(hMenu, (uint)x, true);

                WriteLine("Menu, hMenu : " + hMenu.ToInt64() + ", " + hMenu.ToString("X") + ", Text : " + o);

                System.Drawing.Rectangle rect = MenuAction.GetMenuItemRect(winfo.Hwnd, hMenu, (uint)x);
                Highlighter h = new Highlighter();
                h.Flash(rect);
            }
            else
            {
                WriteLine("Not a Menu");
            }
        }

        public override string GetDescription()
        {
            return "Menu Tests";
        }

    }
}

