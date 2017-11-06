//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MadPiranha.FlatForm
{
    public class UITheme
    {
        ////flat
        //public static readonly Color TransparentColor = Color.Red;
        //public static readonly Color BackColor = SystemColors.Control;
        //public static readonly Color TextBackColor = GetLighterColor(BackColor, 20);
        //public static readonly Color MainWindowBackColor = GetDarkerColor(BackColor, 20);
        //public static readonly FormBorderStyle MainWindowFormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        //public static readonly BorderStyle BorderStyle = BorderStyle.FixedSingle;
        //public static readonly BorderStyle TextBorderStyle = BorderStyle.FixedSingle;
        //public static readonly FlatStyle FlatStyle = FlatStyle.Flat;
        //public static readonly Color FlatAppearance_BorderColor = Color.Gray;
        //public static readonly int FlatAppearance_BorderSize = 1;
        //public static readonly Color FlatAppearance_MouseDownBackColor = Color.Gray;
        //public static readonly Color FlatAppearance_MouseOverBackColor = Color.WhiteSmoke;
        ////public static readonly MyTabControl2.TabControlLook TabLook = MyTabControl2.TabControlLook.Flat;


        //default fixed3d.
        public static readonly Color TransparentColor = Color.Red;
        public static readonly Color BackColor = SystemColors.Control;
        public static readonly Color TextBackColor = SystemColors.ControlLightLight;
        public static readonly Color MainWindowBackColor = GetDarkerColor(BackColor, 20);
        public static readonly FormBorderStyle MainWindowFormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        public static readonly BorderStyle BorderStyle = BorderStyle.Fixed3D;
        public static readonly BorderStyle TextBorderStyle = BorderStyle.Fixed3D;
        public static readonly FlatStyle FlatStyle = FlatStyle.Standard;
        public static readonly Color FlatAppearance_BorderColor = Color.Gray;
        public static readonly int FlatAppearance_BorderSize = 1;
        public static readonly Color FlatAppearance_MouseDownBackColor = Color.Gray;
        public static readonly Color FlatAppearance_MouseOverBackColor = Color.WhiteSmoke;
        //public static readonly MyTabControl2.TabControlLook TabLook = MyTabControl2.TabControlLook.Default;


        //public static readonly Color TransparentColor = Color.Red;
        //public static readonly Color BackColor = Color.White;// SystemColors.Control;
        //public static readonly Color TextBackColor = GetDarkerColor(BackColor, 20);//SystemColors.ControlLightLight;
        //public static readonly Color MainWindowBackColor = GetDarkerColor(BackColor, 40);
        //public static readonly FormBorderStyle MainWindowFormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        //public static readonly BorderStyle BorderStyle = BorderStyle.None;
        //public static readonly BorderStyle TextBorderStyle = BorderStyle.None;
        //public static readonly FlatStyle FlatStyle = FlatStyle.Flat;
        //public static readonly Color FlatAppearance_BorderColor = GetDarkerColor(MainWindowBackColor, 40);
        //public static readonly int FlatAppearance_BorderSize = 1;
        //public static readonly Color FlatAppearance_MouseDownBackColor = Color.Gray;
        //public static readonly Color FlatAppearance_MouseOverBackColor = MainWindowBackColor;
        //public static readonly MyTabControl2.TabControlLook TabLook = MyTabControl2.TabControlLook.FlatRounded;


        public static Color GetLighterColor(Color color)
        {
            return GetLighterColor(color, 10);
        }

        public static Color GetLighterColor(Color color, int light)
        {
            return Color.FromArgb(
                (color.R + light > 255 ? 255 : color.R + light),
                (color.G + light > 255 ? 255 : color.G + light),
                (color.B + light > 255 ? 255 : color.B + light)
                );
        }

        public static Color GetDarkerColor(Color color, int light)
        {
            return Color.FromArgb(
                (color.R - light > 0 ? color.R - light : 0),
                (color.G - light > 0 ? color.G - light : 0),
                (color.B - light > 0 ? color.B - light : 0)
                );
        }


    }

 
}
