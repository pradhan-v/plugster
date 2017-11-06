//Copyright © MadPiranha 2012-2013


using MadPiranha.FlatForm.Controls;
namespace MadPiranha.Plugster.UI
{
    partial class TheFlatForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nonClientButtons1 = new MadPiranha.FlatForm.Controls.NonClientButtons();
            this.SuspendLayout();
            // 
            // nonClientButtons1
            // 
            this.nonClientButtons1.AutoSize = true;
            this.nonClientButtons1.Location = new System.Drawing.Point(630, 0);
            this.nonClientButtons1.Margin = new System.Windows.Forms.Padding(0);
            this.nonClientButtons1.Name = "nonClientButtons1";
            this.nonClientButtons1.Size = new System.Drawing.Size(51, 16);
            this.nonClientButtons1.TabIndex = 0;
            // 
            // TheFlatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 358);
            this.Controls.Add(this.nonClientButtons1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "TheFlatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.FlatForm_Activated);
            this.Deactivate += new System.EventHandler(this.FlatForm_Deactivate);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.FlatForm_ControlAdded);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FlatForm_AutoResizeWindow);
            this.MouseEnter += new System.EventHandler(this.FlatForm_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.FlatForm_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FlatForm_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NonClientButtons nonClientButtons1;

    }
}