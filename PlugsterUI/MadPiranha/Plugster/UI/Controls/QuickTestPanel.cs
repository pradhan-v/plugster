//Copyright © MadPiranha 2012-2013

using System;
using System.Windows.Forms;
using MadPiranha.Plugster.Base.QuickTest;
using MadPiranha.Plugster.Base.Output;
using System.Diagnostics;
using System.IO;


namespace MadPiranha.Plugster.UI.Controls
{
    public partial class QuickTestPanel : UserControl, IQuickTestInput
    {
        private IOutput compileroutput;
        public IOutput CompilerOutput
        {
            get
            {
                return compileroutput;
            }
            set
            {
                this.compileroutput = value;
            }
        }

        private QuickTest quickTest;
       
        public QuickTestPanel(IOutput output) : this()
        {
            CompilerOutput = output;
        }

        public QuickTestPanel()
        {
            quickTest = new QuickTest(this);
            InitializeComponent();
            InitializeColors();
            comboBox1.SelectedIndex = 0;
            quickTest.LoadAssembliesFromCurrentDomain();
        }

        private void InitializeColors()
        {
            //this.buttonCompile.FlatStyle = UITheme.FlatStyle;
            //this.buttonRun.FlatStyle = UITheme.FlatStyle;
            //this.comboBox1.BackColor = UITheme.TextBackColor;
            //this.comboBox1.FlatStyle = UITheme.FlatStyle;
            //this.buttonGenerateCode.FlatStyle = UITheme.FlatStyle;
            //this.textBoxOutputName.BackColor = UITheme.TextBackColor;
            //this.textBoxOutputName.BorderStyle = UITheme.BorderStyle;
            //this.textBoxCompilerOptions.BackColor = UITheme.TextBackColor;
            //this.textBoxCompilerOptions.BorderStyle = UITheme.BorderStyle;
            //this.groupBoxExeDll.FlatStyle = UITheme.FlatStyle;
            //this.radioButtonDll.FlatStyle = UITheme.FlatStyle;
            //this.radioButtonExe.FlatStyle = UITheme.FlatStyle;
            //this.checkBoxInMemory.FlatStyle = UITheme.FlatStyle;
            //this.buttonAddRefAssemblies.FlatStyle = UITheme.FlatStyle;
            //this.buttonConvert.FlatStyle = UITheme.FlatStyle;
        }



        #region IQuickTestInput methods

        public void AddAssembly(string assemblyName)
        {
            //myListBoxReferencedAssemblies.AddListItem(assemblyName);
            showHideFilterListPanel1RefAssemblies.AddListItem(assemblyName);
        }

        public string GetCodeProvider()
        {
            return comboBox1.SelectedItem.ToString();
        }

        public string GetCompilerOptions()
        {
            return textBoxCompilerOptions.Text;
        }

        public string GetOutputName()
        {
            return textBoxOutputName.Text;
        }

        public string[] GetReferencedAssemblies()
        {
            string[] rass = new String[showHideFilterListPanel1RefAssemblies.ListBoxControl.Items.Count];
            for (int i = 0; i < showHideFilterListPanel1RefAssemblies.ListBoxControl.Items.Count; i++)
                rass[i] = showHideFilterListPanel1RefAssemblies.GetItemAt(i).ToString();
            return rass;
        }

        public string GetSource()
        {
            return myRichTextBoxCode.Text;
        }

        public bool ShouldGenerateExecutable()
        {
            return radioButtonExe.Checked;
        }

        public bool ShouldGenerateInMemory()
        {
            return checkBoxInMemory.Checked;
        }

        public IOutput GetMessageOut()
        {
            return compileroutput;
        }

        #endregion

        public string GetCurrentQuickTestAssembly()
        {
            return quickTest.GetCurrentQuickTestAssembly();
        }

        private void UpdateOutputName()
        {
            string outtext = textBoxOutputName.Text;
            if ("" != outtext)
            {
                if (outtext.EndsWith(".dll") || outtext.EndsWith(".exe"))
                {
                    outtext = outtext.Substring(0, outtext.LastIndexOf("."));
                }
            }
            else outtext = "QuickText";

            if (radioButtonDll.Checked)
            {
                textBoxOutputName.Text = outtext + ".dll";
            }
            else
            {
                textBoxOutputName.Text = outtext + ".exe";
            }
        }

        private string GetSelectedProvider()
        {
            return (string)this.comboBox1.SelectedItem;
        }

        private void buttonCompile_Click(object sender, EventArgs e)
        {
            buttonRun.Enabled = quickTest.Compile();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            quickTest.Execute(textBoxOutputName.Text, checkBox1Capture.Checked);
        }

        private void helloWorldMainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //myRichTextBoxCode.Text = quickTest.GenerateCode(CompileUnitCreator.BuildPlugsterTestExecuteGraph());
            myRichTextBoxCode.Text = CompileUnitCreator.CSharpCode;
        }

        private void radioButtonExeDll_CheckedChanged(object sender, EventArgs e)
        {
            UpdateOutputName();
        }

        private void textBoxOutputName_Leave(object sender, EventArgs e)
        {
            UpdateOutputName();
        }

        private void buttonAddRefAssemblies_Click(object sender, EventArgs e)
        {
            quickTest.LoadAssembliesFromCurrentDomain();
        }

    }
}
