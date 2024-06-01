namespace hospital
{
    partial class FormAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdmin));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.cbPosition = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnPosition = new System.Windows.Forms.Button();
            this.btnaddbed = new System.Windows.Forms.Button();
            this.btnAmountAmbulance = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(172, 652);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.Size = new System.Drawing.Size(1144, 448);
            this.dataGridView1.TabIndex = 117;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(1340, 269);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(334, 92);
            this.btnEdit.TabIndex = 120;
            this.btnEdit.TabStop = false;
            this.btnEdit.Text = "Update";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnReset
            // 
            this.btnReset.AutoEllipsis = true;
            this.btnReset.BackColor = System.Drawing.Color.Transparent;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReset.Location = new System.Drawing.Point(1340, 581);
            this.btnReset.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(334, 92);
            this.btnReset.TabIndex = 123;
            this.btnReset.Text = "Reset";
            this.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cbPosition
            // 
            this.cbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPosition.FormattingEnabled = true;
            this.cbPosition.Items.AddRange(new object[] {
            "",
            "View Only",
            "Create Only",
            "Super Admin"});
            this.cbPosition.Location = new System.Drawing.Point(851, 171);
            this.cbPosition.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cbPosition.Name = "cbPosition";
            this.cbPosition.Size = new System.Drawing.Size(464, 75);
            this.cbPosition.TabIndex = 106;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(573, 171);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 63);
            this.label2.TabIndex = 116;
            this.label2.Text = "Position:";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.Location = new System.Drawing.Point(661, 535);
            this.txtConfirmPassword.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(652, 74);
            this.txtConfirmPassword.TabIndex = 109;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(1340, 373);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(334, 92);
            this.btnSearch.TabIndex = 121;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.AutoEllipsis = true;
            this.btnRemove.BackColor = System.Drawing.Color.Transparent;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.Black;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemove.Location = new System.Drawing.Point(1340, 477);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(334, 92);
            this.btnRemove.TabIndex = 122;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1340, 165);
            this.btnSave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(334, 92);
            this.btnSave.TabIndex = 119;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(661, 418);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(652, 74);
            this.txtPassword.TabIndex = 108;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(163, 542);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(483, 63);
            this.label4.TabIndex = 115;
            this.label4.Text = "Confirm Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(163, 418);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 63);
            this.label3.TabIndex = 114;
            this.label3.Text = "Password:";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(661, 289);
            this.txtName.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(652, 74);
            this.txtName.TabIndex = 107;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.Location = new System.Drawing.Point(163, 296);
            this.labelUsername.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(291, 63);
            this.labelUsername.TabIndex = 113;
            this.labelUsername.Text = "Username:";
            // 
            // txtID
            // 
            this.txtID.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(296, 164);
            this.txtID.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(228, 74);
            this.txtID.TabIndex = 118;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(163, 171);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 63);
            this.label1.TabIndex = 112;
            this.label1.Text = "ID:";
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(1584, 2);
            this.btnReturn.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(149, 56);
            this.btnReturn.TabIndex = 111;
            this.btnReturn.Text = "Return";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Cascadia Code Light", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelTitle.Image = ((System.Drawing.Image)(resources.GetObject("labelTitle.Image")));
            this.labelTitle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Location = new System.Drawing.Point(572, 42);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(427, 78);
            this.labelTitle.TabIndex = 110;
            this.labelTitle.Text = "    អ្នកគ្រប់គ្រង";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLog
            // 
            this.btnLog.AutoEllipsis = true;
            this.btnLog.BackColor = System.Drawing.Color.Transparent;
            this.btnLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLog.ForeColor = System.Drawing.Color.Black;
            this.btnLog.Image = ((System.Drawing.Image)(resources.GetObject("btnLog.Image")));
            this.btnLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLog.Location = new System.Drawing.Point(1340, 685);
            this.btnLog.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(334, 92);
            this.btnLog.TabIndex = 124;
            this.btnLog.Text = "Record";
            this.btnLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLog.UseVisualStyleBackColor = false;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnPosition
            // 
            this.btnPosition.AutoEllipsis = true;
            this.btnPosition.BackColor = System.Drawing.Color.Transparent;
            this.btnPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPosition.ForeColor = System.Drawing.Color.Black;
            this.btnPosition.Image = ((System.Drawing.Image)(resources.GetObject("btnPosition.Image")));
            this.btnPosition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPosition.Location = new System.Drawing.Point(1340, 997);
            this.btnPosition.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnPosition.Name = "btnPosition";
            this.btnPosition.Size = new System.Drawing.Size(334, 92);
            this.btnPosition.TabIndex = 125;
            this.btnPosition.Text = "Position";
            this.btnPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPosition.UseVisualStyleBackColor = false;
            this.btnPosition.Click += new System.EventHandler(this.btnPosition_Click);
            // 
            // btnaddbed
            // 
            this.btnaddbed.AutoSize = true;
            this.btnaddbed.BackColor = System.Drawing.Color.Transparent;
            this.btnaddbed.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddbed.ForeColor = System.Drawing.Color.Black;
            this.btnaddbed.Image = ((System.Drawing.Image)(resources.GetObject("btnaddbed.Image")));
            this.btnaddbed.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnaddbed.Location = new System.Drawing.Point(1340, 789);
            this.btnaddbed.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnaddbed.Name = "btnaddbed";
            this.btnaddbed.Size = new System.Drawing.Size(334, 92);
            this.btnaddbed.TabIndex = 126;
            this.btnaddbed.Text = "Add Bed";
            this.btnaddbed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnaddbed.UseVisualStyleBackColor = false;
            this.btnaddbed.Click += new System.EventHandler(this.btnaddbed_Click);
            // 
            // btnAmountAmbulance
            // 
            this.btnAmountAmbulance.AutoSize = true;
            this.btnAmountAmbulance.BackColor = System.Drawing.Color.Transparent;
            this.btnAmountAmbulance.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAmountAmbulance.ForeColor = System.Drawing.Color.Black;
            this.btnAmountAmbulance.Image = ((System.Drawing.Image)(resources.GetObject("btnAmountAmbulance.Image")));
            this.btnAmountAmbulance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAmountAmbulance.Location = new System.Drawing.Point(1340, 893);
            this.btnAmountAmbulance.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnAmountAmbulance.Name = "btnAmountAmbulance";
            this.btnAmountAmbulance.Size = new System.Drawing.Size(334, 92);
            this.btnAmountAmbulance.TabIndex = 127;
            this.btnAmountAmbulance.Text = "Ambulance";
            this.btnAmountAmbulance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAmountAmbulance.UseVisualStyleBackColor = false;
            this.btnAmountAmbulance.Click += new System.EventHandler(this.btnAmountAmbulance_Click);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1736, 1158);
            this.Controls.Add(this.btnAmountAmbulance);
            this.Controls.Add(this.btnaddbed);
            this.Controls.Add(this.btnPosition);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cbPosition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.labelTitle);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormAdmin_FormClosed);
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox cbPosition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnPosition;
        private System.Windows.Forms.Button btnaddbed;
        private System.Windows.Forms.Button btnAmountAmbulance;
    }
}