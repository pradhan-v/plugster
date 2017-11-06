//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.UI.Base;
using MadPiranha.Plugster.Base.Test;
using MadPiranha.Plugster.UI.Controls;
using System.Windows.Forms;
using MadPiranha.Plugster.Base.Loader;
using MadPiranha.FlatForm.Controls;

namespace MadPiranha.Plugster.UI.Controls
{
    public class TestsHolderListPanel : ShowHideFilterListPanel, ITestsHolderControl
    {
        private ITestLoader testLoader;
        public ITestLoader TestLoader
        {
            set
            {
                testLoader = value;
            }
            get
            {
                return testLoader;
            }
        }

        public TestsHolderListPanel()
        {
            InitializeComponent();
            FilterListBox.ActionPerformed += new FilterListControl.ActionHandler(this.ListAction);
        }

        public TestsHolderListPanel(ITestLoader loader)
            : this()
        {
            this.testLoader = loader;
        }

        public TestLoadInfo GetSelectedTest()
        {
            return (TestLoadInfo)FilterListBox.GetSelectedItem();
        }

        public void AddTestHolders(TestLoadInfo[] tests)
        {
            FilterListBox.SetListRows(tests);
        }

        public TestLoadInfo[] GetAllTests()
        {
            return (TestLoadInfo[])FilterListBox.GetAllValueItems(typeof(TestLoadInfo));
        }

        public void AddTestHolder(TestLoadInfo test)
        {
            FilterListBox.AddListItem(test);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TestsHolderListPanel
            // 
            this.Name = "TestsHolderListPanel";
            this.Size = new System.Drawing.Size(160, 160);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected virtual void ListAction(object selectedItem)
        {
            if (testLoader != null)
                testLoader.LoadTest((TestLoadInfo)selectedItem);
        }

    }
}
