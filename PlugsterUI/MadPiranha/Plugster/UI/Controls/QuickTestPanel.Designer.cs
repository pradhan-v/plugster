//Copyright © MadPiranha 2012-2013

using MadPiranha.FlatForm.Controls;
namespace  MadPiranha.Plugster.UI.Controls
{
    partial class QuickTestPanel
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonGenerateCode = new MadPiranha.FlatForm.Controls.MyButton();
            this.textBoxOutputName = new MadPiranha.FlatForm.Controls.MyTextBox();
            this.groupBoxExeDll = new System.Windows.Forms.GroupBox();
            this.radioButtonDll = new System.Windows.Forms.RadioButton();
            this.radioButtonExe = new System.Windows.Forms.RadioButton();
            this.labelOptions = new System.Windows.Forms.Label();
            this.textBoxCompilerOptions = new MadPiranha.FlatForm.Controls.MyTextBox();
            this.checkBoxInMemory = new MadPiranha.FlatForm.Controls.MyCheckBox();
            this.showHidePanelButton1AddRef = new MadPiranha.FlatForm.Controls.ShowHidePanelButton();
            this.showHideFilterListPanel1RefAssemblies = new MadPiranha.FlatForm.Controls.ShowHideFilterListPanel();
            this.buttonAddRefAssemblies = new MadPiranha.FlatForm.Controls.MyButton();
            this.buttonCompile = new MadPiranha.FlatForm.Controls.MyButton();
            this.buttonRun = new MadPiranha.FlatForm.Controls.MyButton();
            this.checkBox1Capture = new System.Windows.Forms.CheckBox();
            this.myRichTextBoxCode = new MadPiranha.FlatForm.Controls.CodeTextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxExeDll.SuspendLayout();
            this.showHideFilterListPanel1RefAssemblies.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.comboBox1.Enabled = false;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "CSharp",
            "Visual Basic",
            "JScript"});
            this.comboBox1.Location = new System.Drawing.Point(3, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(98, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel1.Controls.Add(this.comboBox1);
            this.flowLayoutPanel1.Controls.Add(this.buttonGenerateCode);
            this.flowLayoutPanel1.Controls.Add(this.textBoxOutputName);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxExeDll);
            this.flowLayoutPanel1.Controls.Add(this.labelOptions);
            this.flowLayoutPanel1.Controls.Add(this.textBoxCompilerOptions);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxInMemory);
            this.flowLayoutPanel1.Controls.Add(this.showHidePanelButton1AddRef);
            this.flowLayoutPanel1.Controls.Add(this.buttonCompile);
            this.flowLayoutPanel1.Controls.Add(this.buttonRun);
            this.flowLayoutPanel1.Controls.Add(this.checkBox1Capture);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 345);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(707, 67);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // buttonGenerateCode
            // 
            this.buttonGenerateCode.Location = new System.Drawing.Point(3, 30);
            this.buttonGenerateCode.Name = "buttonGenerateCode";
            this.buttonGenerateCode.Size = new System.Drawing.Size(98, 23);
            this.buttonGenerateCode.TabIndex = 2;
            this.buttonGenerateCode.Text = "&Generate Code";
            this.buttonGenerateCode.UseVisualStyleBackColor = true;
            this.buttonGenerateCode.Click += new System.EventHandler(this.helloWorldMainToolStripMenuItem_Click);
            // 
            // textBoxOutputName
            // 
            this.textBoxOutputName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxOutputName.Location = new System.Drawing.Point(107, 3);
            this.textBoxOutputName.Name = "textBoxOutputName";
            this.textBoxOutputName.Size = new System.Drawing.Size(100, 20);
            this.textBoxOutputName.TabIndex = 4;
            this.textBoxOutputName.Text = "QuickText.exe";
            this.textBoxOutputName.Leave += new System.EventHandler(this.textBoxOutputName_Leave);
            // 
            // groupBoxExeDll
            // 
            this.groupBoxExeDll.Controls.Add(this.radioButtonDll);
            this.groupBoxExeDll.Controls.Add(this.radioButtonExe);
            this.groupBoxExeDll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBoxExeDll.Location = new System.Drawing.Point(107, 29);
            this.groupBoxExeDll.Name = "groupBoxExeDll";
            this.groupBoxExeDll.Size = new System.Drawing.Size(100, 32);
            this.groupBoxExeDll.TabIndex = 12;
            this.groupBoxExeDll.TabStop = false;
            // 
            // radioButtonDll
            // 
            this.radioButtonDll.AutoSize = true;
            this.radioButtonDll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonDll.Location = new System.Drawing.Point(53, 13);
            this.radioButtonDll.Name = "radioButtonDll";
            this.radioButtonDll.Size = new System.Drawing.Size(44, 17);
            this.radioButtonDll.TabIndex = 7;
            this.radioButtonDll.Text = "&DLL";
            this.radioButtonDll.UseVisualStyleBackColor = true;
            this.radioButtonDll.CheckedChanged += new System.EventHandler(this.radioButtonExeDll_CheckedChanged);
            // 
            // radioButtonExe
            // 
            this.radioButtonExe.AutoSize = true;
            this.radioButtonExe.Checked = true;
            this.radioButtonExe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioButtonExe.Location = new System.Drawing.Point(6, 12);
            this.radioButtonExe.Name = "radioButtonExe";
            this.radioButtonExe.Size = new System.Drawing.Size(45, 17);
            this.radioButtonExe.TabIndex = 6;
            this.radioButtonExe.TabStop = true;
            this.radioButtonExe.Text = "E&XE";
            this.radioButtonExe.UseVisualStyleBackColor = true;
            this.radioButtonExe.CheckedChanged += new System.EventHandler(this.radioButtonExeDll_CheckedChanged);
            // 
            // labelOptions
            // 
            this.labelOptions.AutoSize = true;
            this.labelOptions.Location = new System.Drawing.Point(213, 0);
            this.labelOptions.Name = "labelOptions";
            this.labelOptions.Size = new System.Drawing.Size(43, 13);
            this.labelOptions.TabIndex = 15;
            this.labelOptions.Text = "Options";
            // 
            // textBoxCompilerOptions
            // 
            this.textBoxCompilerOptions.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxCompilerOptions.Location = new System.Drawing.Point(213, 16);
            this.textBoxCompilerOptions.Name = "textBoxCompilerOptions";
            this.textBoxCompilerOptions.Size = new System.Drawing.Size(100, 20);
            this.textBoxCompilerOptions.TabIndex = 14;
            this.textBoxCompilerOptions.Text = "/platform:x86";
            // 
            // checkBoxInMemory
            // 
            this.checkBoxInMemory.AutoSize = true;
            this.checkBoxInMemory.Location = new System.Drawing.Point(213, 42);
            this.checkBoxInMemory.Name = "checkBoxInMemory";
            this.checkBoxInMemory.Size = new System.Drawing.Size(75, 17);
            this.checkBoxInMemory.TabIndex = 5;
            this.checkBoxInMemory.Text = "&In Memory";
            this.checkBoxInMemory.UseVisualStyleBackColor = true;
            // 
            // showHidePanelButton1AddRef
            // 
            this.showHidePanelButton1AddRef.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.showHidePanelButton1AddRef.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.showHidePanelButton1AddRef.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.showHidePanelButton1AddRef.Location = new System.Drawing.Point(319, 3);
            this.showHidePanelButton1AddRef.Name = "showHidePanelButton1AddRef";
            this.showHidePanelButton1AddRef.ShowHidePanel = this.showHideFilterListPanel1RefAssemblies;
            this.showHidePanelButton1AddRef.Size = new System.Drawing.Size(75, 23);
            this.showHidePanelButton1AddRef.TabIndex = 17;
            this.showHidePanelButton1AddRef.Text = "References";
            this.showHidePanelButton1AddRef.UseVisualStyleBackColor = true;
            // 
            // showHideFilterListPanel1RefAssemblies
            // 
            this.showHideFilterListPanel1RefAssemblies.AddFromTextBox = true;
            this.showHideFilterListPanel1RefAssemblies.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.showHideFilterListPanel1RefAssemblies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.showHideFilterListPanel1RefAssemblies.Controls.Add(this.buttonAddRefAssemblies);
            this.showHideFilterListPanel1RefAssemblies.DeleteAllowed = true;
            this.showHideFilterListPanel1RefAssemblies.Label = "References";
            this.showHideFilterListPanel1RefAssemblies.Location = new System.Drawing.Point(322, 188);
            this.showHideFilterListPanel1RefAssemblies.Name = "showHideFilterListPanel1RefAssemblies";
            this.showHideFilterListPanel1RefAssemblies.Padding = new System.Windows.Forms.Padding(3);
            this.showHideFilterListPanel1RefAssemblies.Size = new System.Drawing.Size(160, 160);
            this.showHideFilterListPanel1RefAssemblies.TabIndex = 9;
            // 
            // buttonAddRefAssemblies
            // 
            this.buttonAddRefAssemblies.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.buttonAddRefAssemblies.Location = new System.Drawing.Point(71, 2);
            this.buttonAddRefAssemblies.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAddRefAssemblies.Name = "buttonAddRefAssemblies";
            this.buttonAddRefAssemblies.Size = new System.Drawing.Size(55, 24);
            this.buttonAddRefAssemblies.TabIndex = 8;
            this.buttonAddRefAssemblies.Text = "&Reset";
            this.buttonAddRefAssemblies.UseVisualStyleBackColor = true;
            this.buttonAddRefAssemblies.Click += new System.EventHandler(this.buttonAddRefAssemblies_Click);
            // 
            // buttonCompile
            // 
            this.buttonCompile.Location = new System.Drawing.Point(319, 32);
            this.buttonCompile.Name = "buttonCompile";
            this.buttonCompile.Size = new System.Drawing.Size(75, 23);
            this.buttonCompile.TabIndex = 10;
            this.buttonCompile.Text = "&Compile";
            this.buttonCompile.UseVisualStyleBackColor = true;
            this.buttonCompile.Click += new System.EventHandler(this.buttonCompile_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Enabled = false;
            this.buttonRun.Location = new System.Drawing.Point(400, 3);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(75, 23);
            this.buttonRun.TabIndex = 11;
            this.buttonRun.Text = "&Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // checkBox1Capture
            // 
            this.checkBox1Capture.AutoSize = true;
            this.checkBox1Capture.Location = new System.Drawing.Point(400, 32);
            this.checkBox1Capture.Name = "checkBox1Capture";
            this.checkBox1Capture.Size = new System.Drawing.Size(63, 17);
            this.checkBox1Capture.TabIndex = 16;
            this.checkBox1Capture.Text = "Capture";
            this.checkBox1Capture.UseVisualStyleBackColor = true;
            // 
            // myRichTextBoxCode
            // 
            this.myRichTextBoxCode.AllowDrop = true;
            this.myRichTextBoxCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.myRichTextBoxCode.AutoScrollMinSize = new System.Drawing.Size(771, 28);
            this.myRichTextBoxCode.BackBrush = null;
            this.myRichTextBoxCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.myRichTextBoxCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.myRichTextBoxCode.DelayedEventsInterval = 200;
            this.myRichTextBoxCode.DelayedTextChangedInterval = 500;
            this.myRichTextBoxCode.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.myRichTextBoxCode.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myRichTextBoxCode.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.myRichTextBoxCode.IndentBackColor = System.Drawing.Color.WhiteSmoke;
            this.myRichTextBoxCode.LeftBracket = '(';
            this.myRichTextBoxCode.Location = new System.Drawing.Point(3, 3);
            this.myRichTextBoxCode.Name = "myRichTextBoxCode";
            this.myRichTextBoxCode.Paddings = new System.Windows.Forms.Padding(0);
            this.myRichTextBoxCode.RightBracket = ')';
            this.myRichTextBoxCode.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.myRichTextBoxCode.Size = new System.Drawing.Size(707, 339);
            this.myRichTextBoxCode.TabIndex = 3;
            this.myRichTextBoxCode.Text = "//MadPiranha: This text area is extended from... Fast Colored TextBox for Syntax " +
                "Highlighting\r\n//https://github.com/PavelTorgashov/FastColoredTextBox";
            // 
            // QuickTestPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.showHideFilterListPanel1RefAssemblies);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.myRichTextBoxCode);
            this.Name = "QuickTestPanel";
            this.Size = new System.Drawing.Size(713, 412);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBoxExeDll.ResumeLayout(false);
            this.groupBoxExeDll.PerformLayout();
            this.showHideFilterListPanel1RefAssemblies.ResumeLayout(false);
            this.showHideFilterListPanel1RefAssemblies.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private CodeTextBox myRichTextBoxCode;
        private MyButton buttonCompile;
        private MyButton buttonRun;
        private System.Windows.Forms.ComboBox comboBox1;
        private MyButton buttonGenerateCode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyCheckBox checkBoxInMemory;
        private MyTextBox textBoxOutputName;
        private System.Windows.Forms.GroupBox groupBoxExeDll;
        private System.Windows.Forms.RadioButton radioButtonDll;
        private System.Windows.Forms.RadioButton radioButtonExe;
        private MyButton buttonAddRefAssemblies;
        private MyTextBox textBoxCompilerOptions;
        private System.Windows.Forms.Label labelOptions;
        private System.Windows.Forms.CheckBox checkBox1Capture;
        private ShowHidePanelButton showHidePanelButton1AddRef;
        private ShowHideFilterListPanel showHideFilterListPanel1RefAssemblies;

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    