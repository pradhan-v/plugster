//Copyright © MadPiranha 2012-2013

using System.Windows.Forms;
using MadPiranha.Plugster.Base.Loader;
using System.Collections.Generic;
using MadPiranha.Plugster.UI.Base;
using System;
using System.Reflection;
using System.Threading;
using MadPiranha.Plugster.UI.Controls;
using MadPiranha.Plugster.Base.Output;
using MadPiranha.Plugster.Base.Test;
using System.Drawing;
using MadPiranha.Plugster.Base;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MadPiranha.Plugster.Util;
using MadPiranha.FlatForm.Controls;

namespace MadPiranha.Plugster.UI
{

    public partial class PlugsterWindow2 : TheFlatForm, ITestLoader, IHotKeyConsumer
    {
        private PlugLoader plugLoader;
        private ITestLoader testLoader;
        private List<ITestsHolderControl> testsHolderControls;

        private PlugsterWindow2()
        {           
            InitExceptionHandler();

            InitializeComponent();

            InitLAF();

            //TODO: create a separate test loader
            testLoader = this;

            this.Load += new System.EventHandler(this.PlugsterWindow2_Load);
            //this.Disposed += new System.EventHandler(this.PlugsterWindow2_Disposed);

            InitHotKey();

            InitPlugLoader();

            testsListPanel.TestLoader = testLoader;

            testsHolderControls = new List<ITestsHolderControl>();
            testsHolderControls.Add(testsListPanel);

            //TODO:Handle this in a diff place.
            showHidePanelAppDom.FilterListBox.ItemDeleted+=new FilterListControl.DeleteHandler(this.DeleteAppDom);
        }

        private void InitLAF()
        {
            //this.FormBorderStyle = UITheme.MainWindowFormBorderStyle;
            if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.None)
            {
                this.Padding = new System.Windows.Forms.Padding(0);
                this.Controls.Remove(this.nonClientButtons1);
            }
            else
            {
                this.Padding = new System.Windows.Forms.Padding(4);
            }

            //this.BackColor = UITheme.MainWindowBackColor;
            //this.panelBelow.BackColor = UITheme.BackColor;
            //this.tabControlTests.MyBackColor = UITheme.BackColor;
            //this.testsListPanel.BackColor = UITheme.BackColor;
            //this.tabPageQuickTest.BackColor = UITheme.BackColor;
            //this.showHidePanelAppDom.BackColor = UITheme.BackColor;
            //this.nonClientButtons1.BackColor = UITheme.MainWindowBackColor;

            ////this.TransparencyKey = UITheme.TransparentColor;

            //this.buttonExecute.FlatStyle = UITheme.FlatStyle;
            //this.buttonShowLog.FlatStyle = UITheme.FlatStyle;
            //this.buttonQuickTest.FlatStyle = UITheme.FlatStyle;
            //this.showHidePanelButtonLoad.FlatStyle = UITheme.FlatStyle;
            //this.showHidePanelButtonAppDom.FlatStyle = UITheme.FlatStyle;

            //this.testsListPanel.BorderStyle = UITheme.BorderStyle;
            //this.showHidePanelAppDom.BorderStyle = UITheme.BorderStyle;
            //this.outputWindowLog.BorderStyle = UITheme.BorderStyle;

            //this.BackColor = UITheme.BackColor;
        }

        private void DeleteAppDom(object deletedItem)
        {
            plugLoader.Unload(deletedItem.ToString());
        }


        #region Singleton Window methods

        private static Mutex mutex = null;
        private static PlugsterWindow2 mainWindow;

        static PlugsterWindow2()
        {
            if (!IsThisTheOne())
            {
                ShowTheOne();
            }
            else
            {
                mainWindow = new PlugsterWindow2();
            }
        }

        private static bool IsThisTheOne()
        {
            bool createdNew = false;
            mutex = new Mutex(false, typeof(PlugsterWindow2).Namespace, out createdNew);
            return createdNew;
        }

        private static void ShowTheOne()
        {
            WindowAction.PostMessage((IntPtr)HWND_BROADCAST, WM_SHOW_PLUGSTER, IntPtr.Zero, IntPtr.Zero);

            //Process current = Process.GetCurrentProcess();
            //foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            //{
            //    if (process.Id != current.Id)
            //    {
            //        SetForegroundWindow(process.MainWindowHandle);
            //        break;
            //    }
            //}
        }

        public static PlugsterWindow2 TryGetInstance()
        {
            return mainWindow;
        }

        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_SHOW_PLUGSTER = WindowAction.RegisterWindowMessage("WM_SHOW_PLUGSTER");

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SHOW_PLUGSTER)
            {
                ShowApp();
            }
            base.WndProc(ref m);
        }

        #endregion

        #region Init Methods

        private void InitExceptionHandler()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(ApplicationThreadException);
        }
        private void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            outputWindowLog.WriteLine("Exception in Plugster");
            outputWindowLog.WriteLine(BaseUtils.GetExceptionInfo(e.Exception));
            MessageBox.Show(e.Exception.Message, "An exception occurred(See logs for more details):", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void InitPlugLoader()
        {
            plugLoader = TempFactory.GetPlugLoader();
            plugLoader.AppDomainLoaded += new AppDomainLoadedHandler(this.AppDomainLoaded);
            plugLoader.AppDomainUnloaded += new AppDomainUnloadedHandler(this.AppDomainUnloaded);
            plugLoader.LoadingAppDomain += new LoadingAppDomainHandler(this.LoadingAppDomain);
            plugLoader.LoadingPlug += new LoadingPlugHandler(this.LoadingPlug);
            plugLoader.PlugLoaded += new PlugLoadedHandler(this.PlugLoaded);
            plugLoader.PlugLoadException += new PlugLoadExceptionHandler(this.PlugLoadException);
            plugLoader.UnloadingAppDomain += new UnloadingAppDomainHandler(this.UnloadingAppDomainHandler);
        }

        #endregion

        #region Control Get Methods

        public TabControl GetTestWindowsTab()
        {
            return tabControlTests;
        }

        public string GetCurrentTestName()
        {
            //TODO: trim is required for the mytabpage having spaces in text.
            return tabControlTests.SelectedTab.Text.Trim();
        }

        public IOutput GetOutputWindow()
        {
            return outputWindowLog;
        }

        public TheTestWindow GetTestWindow()
        {
            if (tabControlTests.TabCount > 0)
            {
                Control[] pw = tabControlTests.SelectedTab.Controls.Find(GetCurrentTestName(), false);
                if (pw != null && pw.Length > 0)
                {
                    if (pw[0] is TheTestWindow)
                    {
                        return (TheTestWindow)pw[0];
                    }
                }
            }
            return null;
        }

        #endregion

        #region Plug Loader Events

        public void AppDomainLoaded(object o, PlugLoadEventArgs a)
        {
            this.GetOutputWindow().WriteLine("App Domain Loaded : " + a.AppDomainName);
            this.showHidePanelAppDom.FilterListBox.AddListItem(a.AppDomainName);
        }

        public void AppDomainUnloaded(object o, PlugLoadEventArgs a)
        {
            this.GetOutputWindow().WriteLine("App Domain Unloaded : " + a.AppDomainName);
        }

        public void LoadingAppDomain(object o, PlugLoadEventArgs a)
        {
            this.GetOutputWindow().WriteLine("\nLoading assembly : " + a.AssemblyName + " to App Domain " + a.AppDomainName);
        }

        public void LoadingPlug(object o, PlugLoadEventArgs a)
        {
            this.GetOutputWindow().WriteLine("Loading Plug : " + a.PlugName);
        }

        public void PlugLoaded(object o, PlugLoadEventArgs a)
        {
            this.GetOutputWindow().WriteLine("Plug Loaded : " + a.PlugName);
        }

        public void PlugLoadException(object o, PlugLoadEventArgs a)
        {
            this.GetOutputWindow().WriteLine("Exception Loading Plug... \n" + a.ExceptionInfo);
        }

        public void UnloadingAppDomainHandler(object o, PlugLoadEventArgs a)
        {

        }

        #endregion

        #region ITestLoader Methods

        public void LoadTest(TestLoadInfo holder)
        {
            if (holder == null)
                return;

            //Tabpage was closed
            if (holder.Test != null)
            {
                TabPage tabPage1 = GetTestWindowsTab().TabPages[holder.Name];
                if (tabPage1 == null)
                {
                    holder.Test = null;
                }
                else
                {
                    this.GetTestWindowsTab().SelectedTab = tabPage1;
                    return;
                }
            }

            //TODO:what are you doing here ???
            TabPage tabPage = (TabPage)this.GetTestWindowsTab().Controls[holder.ClassName];
            AddTestPage(holder, tabPage);

            new Thread(new ThreadStart(delegate()
            {
                
                if (holder.Test == null)
                {
                    ITest test = plugLoader.LoadPlug(holder.AppDomainName, holder.AssemblyName, holder.ClassName);
                    holder.Test = test;
                    //AddTestPage(test);
                    tabPage = (TabPage)this.GetTestWindowsTab().Controls[holder.ClassName];
                    InvokeOnControl(this, new MethodInvoker(delegate() { AddTestPage(holder, tabPage); }));
                }
                else
                {
                    //this.GetTestWindowsTab().SelectTab(holder.Test.Name);
                    InvokeOnControl(this, new MethodInvoker(delegate() { this.GetTestWindowsTab().SelectTab(holder.Name); }));
                }

            })).Start();
        }

        //TODO:this method
        public void UnLoadTest(TestLoadInfo holder)
        {
            //TODO: Unload should stop the test if its executing !!

            //TODO: null check and stuff
            TabControl tab = this.GetTestWindowsTab();
            Control[] pages = tab.Controls.Find(holder.Name , false);
            tab.Controls.Remove(pages[0]);

            holder.Test = null;
        }

        #endregion

        #region UI methods

        private void AddTestFromAssembly(String appDomainName, String assemblyStr)
        {
            this.tabControlTests.SelectedIndex = 0;

            TestLoadInfo[] plugs = plugLoader.GetAllPlugs(appDomainName, assemblyStr);
            AddPlugsToTestHolderControls(plugs);
        }

        private void InvokeOnControl(Control control, MethodInvoker del)
        {
            if (control.InvokeRequired)
                control.Invoke(del);
            else
                del.Invoke();
        }

        //TODO:again... what are you doing here ???
        private void AddTestPage(TestLoadInfo testLoadInfo, TabPage tabPage)
        {
            if(tabPage==null)
            {
                tabPage = new TabPage();
                tabPage.Text = testLoadInfo.ClassName;
                tabPage.Name = testLoadInfo.ClassName;

                Label l = new Label();
                l.Text = "Loading... \n"+testLoadInfo.ClassName;
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Dock = System.Windows.Forms.DockStyle.Fill;

                tabPage.Controls.Add(l);

                this.GetTestWindowsTab().Controls.Add(tabPage);
                this.GetTestWindowsTab().SelectedTab = tabPage;
            }
            else if (testLoadInfo.Test != null)
            {
                //MyTabPage tabPage = new MyTabPage();
                tabPage.Text = testLoadInfo.Name;
                tabPage.Name = testLoadInfo.Name;

                TheTestWindow tw = new TheTestWindow(testLoadInfo);
                tw.Name = testLoadInfo.Name;
                tw.Dock = System.Windows.Forms.DockStyle.Fill;

                tabPage.Controls.Clear();
                tabPage.Controls.Add(tw);

                //this.GetTestWindowsTab().Controls.Add(tabPage);
                //this.GetTestWindowsTab().SelectedTab = tabPage;
            }

        }

        private void AddPlugsToTestHolderControls(TestLoadInfo[] testholders)
        {
            MethodInvoker mi = new MethodInvoker(delegate()
            {
                foreach (ITestsHolderControl testHolderControl in testsHolderControls)
                {
                    testHolderControl.AddTestHolders(testholders);
                }
            });

            InvokeOnControl(this, mi);
        }

        private void LoadForm()
        {
            //Thread t = new Thread(new ThreadStart(this.StartPlugster));
            //t.Name = "PlugsterStartThreddu";
            //t.Start();

            StartPlugster();
        }

        private void Execute()
        {
            if (this.GetTestWindowsTab().TabCount > 1)
            {
                this.GetTestWindow().Execute();
            }
        }

        private void StartPlugster()
        {
            //TODO: test load life cycle for each test.
            //TODO: remove all temp factories.

            //TestLoadInfo[] testholders = plugLoader.GetAllPlugs(TempFactory.GetLoadTestsInfo());
            TestLoadInfo[] testholders = plugLoader.GetAllPlugs(TempFactory.GetLoadTestsInfo());
            if (testholders != null)
            {
                AddPlugsToTestHolderControls(testholders);
            }

        }

        private void ShowApp()
        {
            ////woraround to bring window to focus
            //FormWindowState state = this.WindowState;
            //this.WindowState = FormWindowState.Minimized;
            //this.WindowState = state == FormWindowState.Minimized ? FormWindowState.Normal : state;

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            bool top = TopMost;
            TopMost = true;
            TopMost = top;
        }

        private void ShowAppAndLoad()
        {
            ShowApp();
            showHidePanelButtonLoad.ShowPanel();
        }

        #endregion

        #region HotKey stuffs

        private void InitHotKey()
        {
            HotKeyRegister.GetHotKeyRegister().RegisterHotKey(Util.ModifierKeys.MOD_WIN, HOTKEY_EXEC, this);
            HotKeyRegister.GetHotKeyRegister().RegisterHotKey(Util.ModifierKeys.MOD_WIN, HOTKEY_SHOW, this);
        }

        public void HotKeyPressed(KeyPressedEventArgs args)
        {
            if (Util.ModifierKeys.MOD_WIN == args.Modifier && HOTKEY_EXEC == args.Key)
            {
                Execute();
            }
            else if (Util.ModifierKeys.MOD_WIN == args.Modifier && HOTKEY_SHOW == args.Key)
            {
                ShowAppAndLoad();
            }
        }

        private const Keys HOTKEY_EXEC = Keys.Z;
        private const Keys HOTKEY_SHOW = Keys.A;


        #endregion

        #region UI events

        private void PlugsterWindow2_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        //private void PlugsterWindow2_Disposed(object sender, EventArgs e)
        //{
            //hotkey.Dispose();
            //mutex.ReleaseMutex();
        //}

        private void PlugsterWindow2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(mutex.WaitOne(TimeSpan.Zero, true))
            {
                mutex.ReleaseMutex();
            }
        }

        private void buttonShowLog_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
            {
                splitContainer1.Panel2Collapsed = false;
                buttonShowLog.Text = "&Hide Log";
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                buttonShowLog.Text = "&Show Log";
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddTestFromAssembly("QuickTestDomain", quickTestPanel1.GetCurrentQuickTestAssembly());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GetTestWindowsTab().SelectedIndex > 0)
                GetTestWindow().Execute();
        }

        private void PlugsterWindow2_Shown(object sender, EventArgs e)
        {
            bool show = true;
            TestLoadInfo[] plugs = testsListPanel.GetAllTests();
            foreach (TestLoadInfo plug in plugs)
            {
                if (plug.AutoLoad)
                {
                    show = false;
                    LoadTest(plug);
                }
            }
            if(show)
                showHidePanelButtonLoad.ShowPanel();
        }

        private void tabControlTests_OnClose(object sender, CloseEventArgs e)
        {
            //TODO: Neatly close the tests
            TabControl tab = this.GetTestWindowsTab();
            TabPage tabPage = tab.TabPages[e.TabIndex];
            if (tabPage.Controls[0] is TheTestWindow)
            {
                ((TheTestWindow)tabPage.Controls[0]).CloseTest();
            }
            tab.Controls.Remove(tabPage);
        }

        private void checkBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBoxAlwaysOnTop.Checked;
        }


        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(UITheme.TransparentColor);
        //    System.Drawing.Graphics g = this.CreateGraphics();
        //    g.FillPolygon(myBrush, new Point[] { new Point(0, 3), new Point(0, 0), new Point(3, 0)});
        //    g.FillPolygon(myBrush, new Point[] { new Point(this.Width-3, 0), new Point(this.Width, 0), new Point(this.Width, 3) });
        //    myBrush.Dispose();
        //    g.Dispose();
        //}

        private void PlugsterWindow2_Activated(object sender, EventArgs e)
        {
            //this.AllowTransparency = false;
            //this.Opacity = 1.0;
        }

        private void PlugsterWindow2_Deactivate(object sender, EventArgs e)
        {
            //if (!this.TopMost)
            //{
            //    this.AllowTransparency = true;
            //    this.Opacity = .8;
            //}
        }

        private void this_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array data = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (data != null)
                {
                    string filename = data.GetValue(0).ToString();
                    if (filename != null && !string.Empty.Equals(filename) && (filename.EndsWith(".dll")||filename.EndsWith(".exe")))
                    {
                        AddTestFromAssembly(null, filename);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void this_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        #endregion


    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               