//Copyright © MadPiranha 2012-2013

namespace MadPiranha.Plugster.UI.Controls.Params
{
    partial class CoOrdinatesControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numberControl1 = new MadPiranha.Plugster.UI.Controls.Params.NumberControl();
            this.numberControl2 = new MadPiranha.Plugster.UI.Controls.Params.NumberControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.numberControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numberControl2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(184, 28);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // numberControl1
            // 
            this.numberControl1.AutoSize = true;
            this.numberControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numberControl1.Label = "Y";
            this.numberControl1.Location = new System.Drawing.Point(3, 3);
            this.numberControl1.Name = "numberControl1";
            this.numberControl1.Size = new System.Drawing.Size(86, 22);
            this.numberControl1.TabIndex = 1;
            // 
            // numberControl2
            // 
            this.numberControl2.AutoSize = true;
            this.numberControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numberControl2.Label = "Y";
            this.numberControl2.Location = new System.Drawing.Point(95, 3);
            this.numberControl2.Name = "numberControl2";
            this.numberControl2.Size = new System.Drawing.Size(86, 22);
            this.numberControl2.TabIndex = 2;
            // 
            // CoOrdinatesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CoOrdinatesControl";
            this.Size = new System.Drawing.Size(184, 28);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumberControl numberControl1;
        private NumberControl numberControl2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}
