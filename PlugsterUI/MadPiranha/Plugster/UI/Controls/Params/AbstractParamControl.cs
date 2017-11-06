//Copyright © MadPiranha 2012-2013

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MadPiranha.Plugster.Base.Param;

namespace MadPiranha.Plugster.UI.Controls.Params
{
    //Commented and made virtual because the extending controls dont open in editor.
    public /*abstract*/ class AbstractParamControl : UserControl, IParamControl
    {
        private Boolean dontChange = false;

        protected AbstractParamControl()
        {
            
        }

        protected AbstractParamControl(IParam param)
        {
            param.ValueChanged += new ValueChangedDelegate(this.UpdateValue);
        }

        public Control GetControl()
        {
            return this;
        }

        protected void control_ValueChanged(object sender, EventArgs e)
        {
            if (!dontChange)
            {
                dontChange = true;
                UpdateParamValue();
                dontChange = false;
            }
        }

        private void UpdateValue(object oldValue)
        {
            if (dontChange) return;

            MethodInvoker mi = new MethodInvoker(delegate()
            {
                dontChange = true;
                UpdateControlValue();
                dontChange = false;
            });

            if (this.InvokeRequired)
                this.Invoke(mi);
            else
                mi.Invoke();
        }

        //Commented and made virtual because the extending controls dont open in editor.
        //protected abstract void UpdateParamValue();
        //protected abstract void UpdateControlValue();

        protected virtual void UpdateParamValue() { }
        protected virtual void UpdateControlValue() { }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AbstractParamControl
            // 
            this.Name = "AbstractParamControl";
            this.ResumeLayout(false);

        }
    }
}
