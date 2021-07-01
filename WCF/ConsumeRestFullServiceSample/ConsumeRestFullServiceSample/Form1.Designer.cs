namespace ConsumeRestFullServiceSample
{
    partial class Form1
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
            this.btnBind = new System.Windows.Forms.Button();
            this.lblStudentIdText = new System.Windows.Forms.Label();
            this.lblStudentNameText = new System.Windows.Forms.Label();
            this.lblStudentIdValue = new System.Windows.Forms.Label();
            this.lblStudentNameValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBind
            // 
            this.btnBind.Location = new System.Drawing.Point(142, 82);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(396, 23);
            this.btnBind.TabIndex = 0;
            this.btnBind.Text = "Bind Student Details From RestFul Service";
            this.btnBind.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBind.UseVisualStyleBackColor = true;
            this.btnBind.Click += new System.EventHandler(this.btnBind_Click);
            // 
            // lblStudentIdText
            // 
            this.lblStudentIdText.AutoSize = true;
            this.lblStudentIdText.Location = new System.Drawing.Point(142, 130);
            this.lblStudentIdText.Name = "lblStudentIdText";
            this.lblStudentIdText.Size = new System.Drawing.Size(62, 13);
            this.lblStudentIdText.TabIndex = 1;
            this.lblStudentIdText.Text = "Student Id :";
            // 
            // lblStudentNameText
            // 
            this.lblStudentNameText.AutoSize = true;
            this.lblStudentNameText.Location = new System.Drawing.Point(142, 170);
            this.lblStudentNameText.Name = "lblStudentNameText";
            this.lblStudentNameText.Size = new System.Drawing.Size(41, 13);
            this.lblStudentNameText.TabIndex = 2;
            this.lblStudentNameText.Text = "Name :";
            // 
            // lblStudentIdValue
            // 
            this.lblStudentIdValue.AutoSize = true;
            this.lblStudentIdValue.Location = new System.Drawing.Point(220, 130);
            this.lblStudentIdValue.Name = "lblStudentIdValue";
            this.lblStudentIdValue.Size = new System.Drawing.Size(0, 13);
            this.lblStudentIdValue.TabIndex = 3;
            // 
            // lblStudentNameValue
            // 
            this.lblStudentNameValue.AutoSize = true;
            this.lblStudentNameValue.Location = new System.Drawing.Point(220, 170);
            this.lblStudentNameValue.Name = "lblStudentNameValue";
            this.lblStudentNameValue.Size = new System.Drawing.Size(0, 13);
            this.lblStudentNameValue.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblStudentNameValue);
            this.Controls.Add(this.lblStudentIdValue);
            this.Controls.Add(this.lblStudentNameText);
            this.Controls.Add(this.lblStudentIdText);
            this.Controls.Add(this.btnBind);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBind;
        private System.Windows.Forms.Label lblStudentIdText;
        private System.Windows.Forms.Label lblStudentNameText;
        private System.Windows.Forms.Label lblStudentIdValue;
        private System.Windows.Forms.Label lblStudentNameValue;
    }
}

