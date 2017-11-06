//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.UI;
using System.Windows.Forms;
using System.Threading;
using MadPiranha.Plugster.UI.Controls;
using MadPiranha.Plugster.UI.Base;


class PlugsterMain
{
    [STAThread]
    public static void Main(String[] args)
    {
        try
        {
            Application.EnableVisualStyles();
            PlugsterWindow2 pw = PlugsterWindow2.TryGetInstance();
            if (pw != null)
                Application.Run(pw);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        //DoStuffs();

    }

    private static void DoStuffs()
    {
        Thread t = new Thread(new ThreadStart(delegate()
        {
            //Console.WriteLine("start");
            MadPiranha.Plugster.Base.Loader.PlugLoader loader = MadPiranha.Plugster.Base.TempFactory.GetPlugLoader();
            MadPiranha.Plugster.Base.Test.ITest test = loader.LoadPlug("default", "PlugsterTests", "MadPiranha.PlugsterTests.Test.TestWindows");
            //Console.WriteLine("test : " + test.Name);
        }));
        //Console.WriteLine("thread created");
        t.Start();
        //Console.WriteLine("thread started");
        t.Join();
        //Console.WriteLine("thread joined");
    }
}

