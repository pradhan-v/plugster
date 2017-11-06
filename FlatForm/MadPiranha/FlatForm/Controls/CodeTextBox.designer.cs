//Copyright © MadPiranha 2012-2013

using FastColoredTextBoxNS;
namespace MadPiranha.FlatForm.Controls
{
    partial class CodeTextBox
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
            this.SuspendLayout();
            // 
            // CodeTextBox
            // 
            this.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DelayedEventsInterval = 200;
            this.DelayedTextChangedInterval = 500;
            this.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.IndentBackColor = System.Drawing.Color.WhiteSmoke;
            this.LeftBracket = '(';
            this.Name = "CodeTextBox";
            this.RightBracket = ')';
            this.Size = new System.Drawing.Size(946, 441);
            this.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.this_TextChanged);
            this.SelectionChangedDelayed += new System.EventHandler(this.this_SelectionChangedDelayed);
            this.AutoIndentNeeded += new System.EventHandler<FastColoredTextBoxNS.AutoIndentEventArgs>(this.this_AutoIndentNeeded);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CodeTextBox_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

    }
}

