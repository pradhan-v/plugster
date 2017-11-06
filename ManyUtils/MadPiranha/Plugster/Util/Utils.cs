//Copyright © MadPiranha 2012-2013




using System;
namespace MadPiranha.Plugster.Util
{
    public class Utils
    {

        public static void HighlightWindow(IntPtr hWnd)
        {
            if (hWnd.ToInt32() == 0) return;
            Highlighter h = new Highlighter();
            HighlightWindow(hWnd, h);
        }

        public static void HighlightWindow(IntPtr hWnd, Highlighter h)
        {
            if (hWnd.ToInt32() == 0) return;
            WindowAction.RECT rect = new WindowAction.RECT();
            WindowAction.GetWindowRect(hWnd, out rect);
            h.Flash(WindowAction.RECT.ToRectangle(rect));
        }
    }
}
