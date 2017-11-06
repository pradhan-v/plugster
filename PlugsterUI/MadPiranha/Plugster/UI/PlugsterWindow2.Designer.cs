//Copyright © MadPiranha 2012-2013

using MadPiranha.Plugster.UI.Controls;
using MadPiranha.FlatForm.Controls;
namespace MadPiranha.Plugster.UI
{
    partial class PlugsterWindow2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlugsterWindow2));
            this.panelBelow = new System.Windows.Forms.Panel();
            this.showHidePanelButtonLoad = new MadPiranha.FlatForm.Controls.ShowHidePanelButton();
            this.testsListPanel = new MadPiranha.Plugster.UI.Controls.TestsHolderListPanel();
            this.showHidePanelButtonAppDom = new MadPiranha.FlatForm.Controls.ShowHidePanelButton();
            this.showHidePanelAppDom = new MadPiranha.FlatForm.Controls.ShowHideFilterListPanel();
            this.checkBoxAlwaysOnTop = new MadPiranha.FlatForm.Controls.MyCheckBox();
            this.buttonExecute = new MadPiranha.FlatForm.Controls.MyButton();
            this.buttonShowLog = new MadPiranha.FlatForm.Controls.MyButton();
            this.buttonQuickTest = new MadPiranha.FlatForm.Controls.MyButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlTests = new MadPiranha.FlatForm.Controls.MyTabControl2();
            this.tabPageQuickTest = new System.Windows.Forms.TabPage();
            this.quickTestPanel1 = new MadPiranha.Plugster.UI.Controls.QuickTestPanel();
            this.outputWindowLog = new MadPiranha.Plugster.UI.Controls.OutputWindow2();
            this.nonClientButtons1 = new MadPiranha.FlatForm.Controls.NonClientButtons();
            this.panelBelow.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlTests.SuspendLayout();
            this.tabPageQuickTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBelow
            // 
            this.panelBelow.AutoSize = true;
            this.panelBelow.BackColor = System.Drawing.SystemColors.Control;
            this.panelBelow.Controls.Add(this.showHidePanelButtonLoad);
            this.panelBelow.Controls.Add(this.showHidePanelButtonAppDom);
            this.panelBelow.Controls.Add(this.checkBoxAlwaysOnTop);
            this.panelBelow.Controls.Add(this.buttonExecute);
            this.panelBelow.Controls.Add(this.buttonShowLog);
            this.panelBelow.Controls.Add(this.buttonQuickTest);
            this.panelBelow.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBelow.Location = new System.Drawing.Point(0, 329);
            this.panelBelow.Name = "panelBelow";
            this.panelBelow.Size = new System.Drawing.Size(784, 29);
            this.panelBelow.TabIndex = 5;
            // 
            // showHidePanelButtonLoad
            // 
            this.showHidePanelButtonLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showHidePanelButtonLoad.Location = new System.Drawing.Point(5, 3);
            this.showHidePanelButtonLoad.Name = "showHidePanelButtonLoad";
            this.showHidePanelButtonLoad.ShowHidePanel = this.testsListPanel;
            this.showHidePanelButtonLoad.Size = new System.Drawing.Size(75, 23);
            this.showHidePanelButtonLoad.TabIndex = 7;
            this.showHidePanelButtonLoad.Text = "&Load";
            this.showHidePanelButtonLoad.UseVisualStyleBackColor = true;
            // 
            // testsListPanel
            // 
            this.testsListPanel.AddFromTextBox = false;
            this.testsListPanel.AllowDrop = true;
            this.testsListPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.testsListPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.testsListPanel.DeleteAllowed = true;
            this.testsListPanel.Label = "Tests";
            this.testsListPanel.Location = new System.Drawing.Point(9, 85);
            this.testsListPanel.Name = "testsListPanel";
            this.testsListPanel.Padding = new System.Windows.Forms.Padding(3);
            this.testsListPanel.Size = new System.Drawing.Size(205, 243);
            this.testsListPanel.TabIndex = 12;
            this.testsListPanel.TestLoader = null;
            this.testsListPanel.Visible = false;
            // 
            // showHidePanelButtonAppDom
            // 
            this.showHidePanelButtonAppDom.Location = new System.Drawing.Point(365, 3);
            this.showHidePanelButtonAppDom.Name = "showHidePanelButtonAppDom";
            this.showHidePanelButtonAppDom.ShowHidePanel = this.showHidePanelAppDom;
            this.showHidePanelButtonAppDom.Size = new System.Drawing.Size(75, 23);
            this.showHidePanelButtonAppDom.TabIndex = 6;
            this.showHidePanelButtonAppDom.Text = "AppDom";
            this.showHidePanelButtonAppDom.UseVisualStyleBackColor = true;
            // 
            // showHidePanelAppDom
            // 
            this.showHidePanelAppDom.AddFromTextBox = false;
            this.showHidePanelAppDom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.showHidePanelAppDom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.showHidePanelAppDom.DeleteAllowed = true;
            this.showHidePanelAppDom.Label = "App Domains";
            this.showHidePanelAppDom.Location = new System.Drawing.Point(369, 172);
            this.showHidePanelAppDom.Name = "showHidePanelAppDom";
            this.showHidePanelAppDom.Padding = new System.Windows.Forms.Padding(3);
            this.showHidePanelAppDom.Size = new System.Drawing.Size(173, 156);
            this.showHidePanelAppDom.TabIndex = 6;
            this.showHidePanelAppDom.Visible = false;
            // 
            // checkBoxAlwaysOnTop
            // 
            this.checkBoxAlwaysOnTop.AutoSize = true;
            this.checkBoxAlwaysOnTop.Location = new System.Drawing.Point(264, 8);
            this.checkBoxAlwaysOnTop.Name = "checkBoxAlwaysOnTop";
            this.checkBoxAlwaysOnTop.Size = new System.Drawing.Size(98, 17);
            this.checkBoxAlwaysOnTop.TabIndex = 5;
            this.checkBoxAlwaysOnTop.Text = "&Always On Top";
            this.checkBoxAlwaysOnTop.CheckedChanged += new System.EventHandler(this.checkBoxAlwaysOnTop_CheckedChanged);
            // 
            // buttonExecute
            // 
            this.buttonExecute.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonExecute.Location = new System.Drawing.Point(705, 3);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 23);
            this.buttonExecute.TabIndex = 2;
            this.buttonExecute.Text = "&Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonShowLog
            // 
            this.buttonShowLog.Location = new System.Drawing.Point(182, 3);
            this.buttonShowLog.Name = "buttonShowLog";
            this.buttonShowLog.Size = new System.Drawing.Size(75, 23);
            this.buttonShowLog.TabIndex = 3;
            this.buttonShowLog.Text = "&Show Log";
            this.buttonShowLog.UseVisualStyleBackColor = true;
            this.buttonShowLog.Click += new System.EventHandler(this.buttonShowLog_Click);
            // 
            // buttonQuickTest
            // 
            this.buttonQuickTest.Location = new System.Drawing.Point(88, 3);
            this.buttonQuickTest.Name = "buttonQuickTest";
            this.buttonQuickTest.Size = new System.Drawing.Size(88, 23);
            this.buttonQuickTest.TabIndex = 4;
            this.buttonQuickTest.Text = "Add &QuickTest";
            this.buttonQuickTest.UseVisualStyleBackColor = true;
            this.buttonQuickTest.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlTests);
            this.splitContainer1.Panel1MinSize = 50;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.outputWindowLog);
            this.splitContainer1.Panel2Collapsed = true;
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(784, 325);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 11;
            // 
            // tabControlTests
            // 
            this.tabControlTests.AllowDrop = true;
            this.tabControlTests.ConfirmOnClose = true;
            this.tabControlTests.Controls.Add(this.tabPageQuickTest);
            this.tabControlTests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTests.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tabControlTests.ItemSize = new System.Drawing.Size(86, 22);
            this.tabControlTests.Location = new System.Drawing.Point(0, 0);
            this.tabControlTests.MyBackColor = System.Drawing.SystemColors.Control;
            this.tabControlTests.Name = "tabControlTests";
            this.tabControlTests.Padding = new System.Drawing.Point(9, 0);
            this.tabControlTests.SelectedIndex = 0;
            this.tabControlTests.Size = new System.Drawing.Size(784, 325);
            this.tabControlTests.TabIndex = 6;
            this.tabControlTests.TabStop = false;
            this.tabControlTests.OnClose += new MadPiranha.FlatForm.Controls.MyTabControl2.OnHeaderCloseDelegate(this.tabControlTests_OnClose);
            // 
            // tabPageQuickTest
            // 
            this.tabPageQuickTest.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageQuickTest.Controls.Add(this.quickTestPanel1);
            this.tabPageQuickTest.Location = new System.Drawing.Point(4, 26);
            this.tabPageQuickTest.Name = "tabPageQuickTest";
            this.tabPageQuickTest.Size = new System.Drawing.Size(776, 295);
            this.tabPageQuickTest.TabIndex = 0;
            this.tabPageQuickTest.Text = "Quick Test";
            // 
            // quickTestPanel1
            // 
            this.quickTestPanel1.CompilerOutput = this.outputWindowLog;
            this.quickTestPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quickTestPanel1.Location = new System.Drawing.Point(0, 0);
            this.quickTestPanel1.Name = "quickTestPanel1";
            this.quickTestPanel1.Size = new System.Drawing.Size(776, 295);
            this.quickTestPanel1.TabIndex = 5;
            // 
            // outputWindowLog
            // 
            this.outputWindowLog.AcceptsTab = true;
            this.outputWindowLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.outputWindowLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputWindowLog.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputWindowLog.Location = new System.Drawing.Point(0, 0);
            this.outputWindowLog.Name = "outputWindowLog";
            this.outputWindowLog.Size = new System.Drawing.Size(150, 46);
            this.outputWindowLog.TabIndex = 0;
            this.outputWindowLog.Text = "";
            this.outputWindowLog.WordWrap = false;
            // 
            // nonClientButtons1
            // 
            this.nonClientButtons1.AutoSize = true;
            this.nonClientButtons1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nonClientButtons1.BackColor = System.Drawing.SystemColors.Control;
            this.nonClientButtons1.Location = new System.Drawing.Point(638, 0);
            this.nonClientButtons1.Margin = new System.Windows.Forms.Padding(0);
            this.nonClientButtons1.Name = "nonClientButtons1";
            this.nonClientButtons1.Size = new System.Drawing.Size(51, 16);
            this.nonClientButtons1.TabIndex = 6;
            // 
            // PlugsterWindow2
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 358);
            this.Controls.Add(this.showHidePanelAppDom);
            this.Controls.Add(this.panelBelow);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.testsListPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlugsterWindow2";
            this.Text = "Plugster";
            this.Activated += new System.EventHandler(this.PlugsterWindow2_Activated);
            this.Deactivate += new System.EventHandler(this.PlugsterWindow2_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PlugsterWindow2_FormClosed);
            this.Shown += new System.EventHandler(this.PlugsterWindow2_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.this_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.this_DragEnter);
            this.Controls.SetChildIndex(this.testsListPanel, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.Controls.SetChildIndex(this.panelBelow, 0);
            this.Controls.SetChildIndex(this.showHidePanelAppDom, 0);
            this.panelBelow.ResumeLayout(false);
            this.panelBelow.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabControlTests.ResumeLayout(false);
            this.tabPageQuickTest.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyTabControl2 tabControlTests;
        private OutputWindow2 outputWindowLog;
        private MyButton buttonShowLog;
        private MyButton buttonQuickTest;
        private MadPiranha.Plugster.UI.Controls.QuickTestPanel quickTestPanel1;
        private System.Windows.Forms.TabPage tabPageQuickTest;
        private System.Windows.Forms.Panel panelBelow;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TestsHolderListPanel testsListPanel;
        private MyButton buttonExecute;
        private MyCheckBox checkBoxAlwaysOnTop;
        private ShowHidePanelButton showHidePanelButtonAppDom;
        private ShowHideFilterListPanel showHidePanelAppDom;
        private ShowHidePanelButton showHidePanelButtonLoad;
        private NonClientButtons nonClientButtons1;
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       