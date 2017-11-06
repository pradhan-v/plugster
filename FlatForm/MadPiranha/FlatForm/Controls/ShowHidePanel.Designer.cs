//Copyright © MadPiranha 2012-2013

namespace MadPiranha.FlatForm.Controls
{
    partial class ShowHidePanel
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
            this.SuspendLayout();
            // 
            // ShowHidePanel
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "ShowHidePanel";
            this.Size = new System.Drawing.Size(197, 273);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EscapeKeyPress);
            this.Leave += new System.EventHandler(this.TestsListPanel_Leave);
            this.ResumeLayout(false);

        }

        #endregion



    }
}
