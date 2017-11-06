//Copyright © MadPiranha 2012-2013

using MadPiranha.FlatForm.Controls;
namespace MadPiranha.Plugster.UI.Controls
{
    partial class TheTestWindow
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerParamOut = new MySplitterContainer();
            this.flowLayoutPanelParameters = new System.Windows.Forms.FlowLayoutPanel();
            this.labelDescription = new System.Windows.Forms.Label();
            this.outputWindow21 = new MadPiranha.Plugster.UI.Controls.OutputWindow2();
            this.splitContainerParamOut.Panel1.SuspendLayout();
            this.splitContainerParamOut.Panel2.SuspendLayout();
            this.splitContainerParamOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerParamOut
            // 
            this.splitContainerParamOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerParamOut.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerParamOut.Location = new System.Drawing.Point(0, 0);
            this.splitContainerParamOut.Name = "splitContainerParamOut";
            this.splitContainerParamOut.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerParamOut.Panel1
            // 
            this.splitContainerParamOut.Panel1.Controls.Add(this.flowLayoutPanelParameters);
            this.splitContainerParamOut.Panel1.Controls.Add(this.labelDescription);
            this.splitContainerParamOut.Panel1MinSize = 0;
            // 
            // splitContainerParamOut.Panel2
            // 
            this.splitContainerParamOut.Panel2.Controls.Add(this.outputWindow21);
            this.splitContainerParamOut.Size = new System.Drawing.Size(666, 317);
            this.splitContainerParamOut.SplitterDistance = 68;
            this.splitContainerParamOut.TabIndex = 0;
            this.splitContainerParamOut.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.splitContainerParamOut_DoubleClick);
            // 
            // flowLayoutPanelParameters
            // 
            this.flowLayoutPanelParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelParameters.Location = new System.Drawing.Point(3, 4);
            this.flowLayoutPanelParameters.Name = "flowLayoutPanelParameters";
            this.flowLayoutPanelParameters.Size = new System.Drawing.Size(660, 48);
            this.flowLayoutPanelParameters.TabIndex = 2;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelDescription.Location = new System.Drawing.Point(0, 55);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(60, 13);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "Description";
            // 
            // outputWindow21
            // 
            this.outputWindow21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputWindow21.Location = new System.Drawing.Point(0, 0);
            this.outputWindow21.Name = "outputWindow21";
            this.outputWindow21.Size = new System.Drawing.Size(666, 245);
            this.outputWindow21.TabIndex = 0;
            // 
            // TheTestWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerParamOut);
            this.Name = "TheTestWindow";
            this.Size = new System.Drawing.Size(666, 317);
            this.splitContainerParamOut.Panel1.ResumeLayout(false);
            this.splitContainerParamOut.Panel1.PerformLayout();
            this.splitContainerParamOut.Panel2.ResumeLayout(false);
            this.splitContainerParamOut.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OutputWindow2 outputWindow21;
        private MySplitterContainer splitContainerParamOut;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelParameters;
    }
}
