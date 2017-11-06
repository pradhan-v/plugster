//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using MadPiranha.Plugster.Base.Output;
using System.Windows.Forms;
using MadPiranha.FlatForm.Controls;

namespace MadPiranha.Plugster.UI.Controls
{
    class OutputWindow2 : MyRichTextBox, IOutput
    {
        private delegate void PrintCallback(string text);

        #region IOutput Members

        public void Write(Object str)
        {
            if (str == null) str = "null";
            Print("" + str.ToString());
        }

        public void WriteLine(Object str)
        {
            if (str == null) str = "null";
            Print("" + str.ToString() + Environment.NewLine);
        }

        public void WriteLine()
        {
            Print(Environment.NewLine);
        }

        public void ScrollEnd()
        {
            base.ScrollToCaret();
        }

        public void Clear()
        {
            if (base.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate() { Clear(); }));
            }
            else
            {
                base.Clear();
            }
        }


        private void Print(String str)
        {
            if (IsDisposed)
                Console.Write(str);
            else
            {
                if (base.InvokeRequired)
                {
                    PrintCallback d = new PrintCallback(Print);
                    this.Invoke(d, new object[] { str });
                }
                else
                {
                    base.AppendText(str);
                }
            }
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OutputWindow2
            // 
            this.Name = "OutputWindow2";
            this.Size = new System.Drawing.Size(412, 238);
            this.ResumeLayout(false);

        }
    }
}
