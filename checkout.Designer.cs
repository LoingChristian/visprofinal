﻿namespace visprofinalproject
{
    partial class checkout
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
            this.Pay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Pay
            // 
            this.Pay.Location = new System.Drawing.Point(724, 405);
            this.Pay.Name = "Pay";
            this.Pay.Size = new System.Drawing.Size(75, 22);
            this.Pay.TabIndex = 0;
            this.Pay.Text = "Pay";
            this.Pay.UseVisualStyleBackColor = true;
            this.Pay.Click += new System.EventHandler(this.Pay_Click);
            // 
            // checkout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.Pay);
            this.Name = "checkout";
            this.Text = "checkout";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Pay;
    }
}