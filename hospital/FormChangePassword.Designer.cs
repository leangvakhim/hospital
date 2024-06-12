namespace hospital
{
    partial class FormChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChangePassword));
            this.labelTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.txtoldpassoword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtnewpassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtconfirmpassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Khmer OS Muol Light", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Location = new System.Drawing.Point(432, 24);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(610, 107);
            this.labelTitle.TabIndex = 113;
            this.labelTitle.Text = "ផ្លាស់ប្ដូរពាក្យសម្ងាត់";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(161, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 63);
            this.label1.TabIndex = 114;
            this.label1.Text = "Username";
            // 
            // txtusername
            // 
            this.txtusername.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusername.Location = new System.Drawing.Point(635, 178);
            this.txtusername.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(529, 74);
            this.txtusername.TabIndex = 115;
            this.txtusername.TextChanged += new System.EventHandler(this.txtusername_TextChanged);
            // 
            // txtoldpassoword
            // 
            this.txtoldpassoword.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtoldpassoword.Location = new System.Drawing.Point(635, 315);
            this.txtoldpassoword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtoldpassoword.Name = "txtoldpassoword";
            this.txtoldpassoword.Size = new System.Drawing.Size(529, 74);
            this.txtoldpassoword.TabIndex = 117;
            this.txtoldpassoword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(161, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(363, 63);
            this.label2.TabIndex = 116;
            this.label2.Text = "Old Password";
            // 
            // txtnewpassword
            // 
            this.txtnewpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnewpassword.Location = new System.Drawing.Point(635, 460);
            this.txtnewpassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtnewpassword.Name = "txtnewpassword";
            this.txtnewpassword.Size = new System.Drawing.Size(529, 74);
            this.txtnewpassword.TabIndex = 119;
            this.txtnewpassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(161, 468);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(386, 63);
            this.label3.TabIndex = 118;
            this.label3.Text = "New Password";
            // 
            // txtconfirmpassword
            // 
            this.txtconfirmpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtconfirmpassword.Location = new System.Drawing.Point(635, 610);
            this.txtconfirmpassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtconfirmpassword.Name = "txtconfirmpassword";
            this.txtconfirmpassword.Size = new System.Drawing.Size(529, 74);
            this.txtconfirmpassword.TabIndex = 121;
            this.txtconfirmpassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(161, 618);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(468, 63);
            this.label4.TabIndex = 120;
            this.label4.Text = "Confirm Password";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(915, 728);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(227, 92);
            this.btnSave.TabIndex = 122;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(1156, 728);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(219, 92);
            this.btnCancel.TabIndex = 123;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1439, 865);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtconfirmpassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtnewpassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtoldpassoword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtusername);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTitle);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FormChangePassword";
            this.Text = "FormChangePassword";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormChangePassword_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.TextBox txtoldpassoword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtnewpassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtconfirmpassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}