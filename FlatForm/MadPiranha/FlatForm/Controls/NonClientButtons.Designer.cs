//Copyright © MadPiranha 2012-2013

using System.Windows.Forms;
namespace MadPiranha.FlatForm.Controls
{
    partial class NonClientButtons
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
            this.myButton3 = new Button();
            this.myButton2 = new Button();
            this.myButton1 = new Button();
            this.SuspendLayout();
            // 
            // myButton3
            // 
            this.myButton3.BackColor = System.Drawing.SystemColors.Control;
            this.myButton3.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.myButton3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.myButton3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.myButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.myButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myButton3.Location = new System.Drawing.Point(35, 0);
            this.myButton3.Margin = new System.Windows.Forms.Padding(0);
            this.myButton3.Name = "myButton3";
            this.myButton3.Size = new System.Drawing.Size(16, 16);
            this.myButton3.TabIndex = 2;
            this.myButton3.UseVisualStyleBackColor = true;
            this.myButton3.Click += new System.EventHandler(this.myButton3_Click);
            // 
            // myButton2
            // 
            this.myButton2.BackColor = System.Drawing.SystemColors.Control;
            this.myButton2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.myButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.myButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.myButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.myButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myButton2.Location = new System.Drawing.Point(17, 0);
            this.myButton2.Margin = new System.Windows.Forms.Padding(0);
            this.myButton2.Name = "myButton2";
            this.myButton2.Size = new System.Drawing.Size(16, 16);
            this.myButton2.TabIndex = 1;
            this.myButton2.UseVisualStyleBackColor = true;
            this.myButton2.Click += new System.EventHandler(this.myButton2_Click);
            // 
            // myButton1
            // 
            this.myButton1.BackColor = System.Drawing.SystemColors.Control;
            this.myButton1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.myButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.myButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.myButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.myButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myButton1.Location = new System.Drawing.Point(0, 0);
            this.myButton1.Margin = new System.Windows.Forms.Padding(0);
            this.myButton1.Name = "myButton1";
            this.myButton1.Size = new System.Drawing.Size(16, 16);
            this.myButton1.TabIndex = 0;
            this.myButton1.UseVisualStyleBackColor = true;
            this.myButton1.Click += new System.EventHandler(this.myButton1_Click);
            // 
            // NonClientButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.myButton3);
            this.Controls.Add(this.myButton2);
            this.Controls.Add(this.myButton1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "NonClientButtons";
            this.Size = new System.Drawing.Size(52, 17);
            this.Load += new System.EventHandler(this.ncb_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button myButton1;
        private Button myButton2;
        private Button myButton3;
    }
}
