//Copyright © MadPiranha 2012-2013

namespace MadPiranha.FlatForm.Controls
{
    partial class ShowHideFilterListPanel
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
            this.labelHeader = new System.Windows.Forms.Label();
            this.filterList = new MadPiranha.FlatForm.Controls.FilterListControl();
            this.SuspendLayout();
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Location = new System.Drawing.Point(7, 9);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(33, 13);
            this.labelHeader.TabIndex = 1;
            this.labelHeader.Text = "Label";
            // 
            // filterList
            // 
            this.filterList.AddFromTextBox = false;
            this.filterList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterList.DeleteAllowed = true;
            this.filterList.Location = new System.Drawing.Point(4, 28);
            this.filterList.Name = "filterList";
            this.filterList.Size = new System.Drawing.Size(151, 132);
            this.filterList.TabIndex = 0;
            // 
            // ShowHideFilterListPanel
            // 
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.filterList);
            this.Name = "ShowHideFilterListPanel";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(160, 160);
            this.VisibleChanged += new System.EventHandler(this.TestsListPanel_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private global::MadPiranha.FlatForm.Controls.FilterListControl filterList;
        private System.Windows.Forms.Label labelHeader;
    }
}
