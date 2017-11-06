//Copyright © MadPiranha 2012-2013

using System;
using System.Windows.Forms;
using MadPiranha.Plugster.Base.Test;
using MadPiranha.Plugster.Base.Param;
using MadPiranha.Plugster.UI.Base;
using System.Runtime.InteropServices;
using MadPiranha.Plugster.Base;
using MadPiranha.Plugster.Base.Loader;
using MadPiranha.Plugster.UI.Controls.Params;
using MadPiranha.Plugster.Util;


namespace MadPiranha.Plugster.UI.Controls
{
    public partial class TheTestWindow : UserControl, IHotKeyConsumer
    {
        private TestController testController;

        public TheTestWindow(TestLoadInfo testLoadInfo)
        {
            InitializeComponent();
            InitTestWindow(testLoadInfo.Test);          
            InitHotKey();
        }

        private void InitTestWindow(ITest test)
        {
            test.SetOutput(outputWindow21);
            this.Name = test.Name;
            this.labelDescription.Text = test.GetDescription();

            this.testController = new TestController(test);
            testController.TestStarted += new TestController.TestStartedHandler(this.TestStarted);
            testController.TestFinished += new TestController.TestFinishedHandler(this.TestFinished);
            testController.ExceptionInTest += new TestController.ExceptionInTestHandler(this.ExceptionInTest);

            AddParameterControls(test.GetParameters());
        }

        #region HotKey stuffs

        private uint hotchar=0;

        private void InitHotKey()
        {
            for (int i = 0; i < this.Name.Length; i++)
            {
                hotchar = (uint)this.Name.ToUpper()[i];
                if (hotchar == ' ') { hotchar = 0; continue; } 
                bool b = HotKeyRegister.GetHotKeyRegister().RegisterHotKey(Util.ModifierKeys.MOD_WIN, (Keys)hotchar, this);
                if (b) break;
            }
            if (hotchar != 0)
            {
                labelDescription.Text = labelDescription.Text + " ( Shortcut Win+" + (char)hotchar + " )";
            }
            else
            {
                labelDescription.Text = labelDescription.Text + " ( No Shortcut registered. )";
            }
        }

        public void HotKeyPressed(KeyPressedEventArgs args)
        {
            Execute();
        }

        #endregion

        #region PlugsterController Test Execution Events

        private void TestStarted(object obj, TestLifeCycleEventArgs args)
        {
            args.Test.GetOutput().WriteLine("\n----------" + args.TestName + " Start--------------------------------------------------");
        }

        private void ExceptionInTest(object obj, TestLifeCycleEventArgs args)
        {
            args.Test.GetOutput().WriteLine("----------" + args.TestName + " Exception--------------------------------------------------");
            args.Test.GetOutput().WriteLine(args.ExceptionInfo);
            args.Test.GetOutput().WriteLine("----------" + args.TestName + " End Exception--------------------------------------------------");

        }

        private void TestFinished(object obj, TestLifeCycleEventArgs args)
        {
            args.Test.GetOutput().WriteLine("----------" + args.TestName + " End--------------------------------------------------");
        }

        #endregion

        #region Parameter Controls

        public void PrepareParams()
        {
            //TODO:
            Control[] chxy = flowLayoutPanelParameters.Controls.Find("XYPARAM", true);
            if (chxy != null && chxy.Length > 0)
            {
                ((CoOrdinatesControl)chxy[0]).SetXY();
            }
        }

        private void AddParameterControls(IParam[] parameters)
        {
            if (parameters != null)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    IParam param = parameters[i];
                    IParamControl control = ParamsControlFactory.GetParamControl(param);
                    flowLayoutPanelParameters.Controls.Add(control.GetControl());
                }
            }
        }

        #endregion

        public void CloseTest()
        {
            try
            {
                testController.CloseTest();
            }
            catch (Exception e)
            {
                outputWindow21.WriteLine("Exception in closing test...");
                outputWindow21.WriteLine(BaseUtils.GetExceptionInfo(e));
            }
        }

        public void Execute()
        {
            try
            {
                //TODO: There should be no prepare params.
                //The param and UI should be in sync automatically.
                PrepareParams();
                testController.ExecuteTest();
            }
            catch (Exception e)
            {
                outputWindow21.WriteLine("Exception in executing test...");
                outputWindow21.WriteLine(BaseUtils.GetExceptionInfo(e));
            }
        }

        #region UI events

        //private void ParamsUpdated(object o, ParamUpdatedArgs args)
        //{
        //    test.ParamUpdated();
        //}

        private void splitContainerParamOut_DoubleClick(object sender, MouseEventArgs e)
        {
            if (splitContainerParamOut.SplitterDistance > 0)
                splitContainerParamOut.SplitterDistance = 0;
            else
                splitContainerParamOut.SplitterDistance = 170;
        }

        public void Collapse()
        {
            if (splitContainerParamOut.SplitterDistance > 0)
                splitContainerParamOut.SplitterDistance = 0;
        }

        #endregion

    }
}
