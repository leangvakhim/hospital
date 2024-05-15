namespace hospital
{
    partial class FormMedicine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMedicine));
            this.btnReturn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LbId = new System.Windows.Forms.Label();
            this.LbName = new System.Windows.Forms.Label();
            this.LbQty = new System.Windows.Forms.Label();
            this.LbUnitPrice = new System.Windows.Forms.Label();
            this.LbExpiryDate = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.expiryDate = new System.Windows.Forms.DateTimePicker();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnRemove = new System.Windows.Forms.Button();
            this.BtnReport = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnReturn.Image")));
            this.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReturn.Location = new System.Drawing.Point(722, 2);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 29);
            this.btnReturn.TabIndex = 112;
            this.btnReturn.Text = "Return";
            this.btnReturn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft PhagsPa", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(214, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 34);
            this.label1.TabIndex = 113;
            this.label1.Text = "ប្រព័ន្ធគ្រប់គ្រងឱសថ";
            // 
            // LbId
            // 
            this.LbId.AutoSize = true;
            this.LbId.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbId.Location = new System.Drawing.Point(49, 61);
            this.LbId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LbId.Name = "LbId";
            this.LbId.Size = new System.Drawing.Size(50, 31);
            this.LbId.TabIndex = 114;
            this.LbId.Text = "ID:";
            // 
            // LbName
            // 
            this.LbName.AutoSize = true;
            this.LbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbName.Location = new System.Drawing.Point(49, 103);
            this.LbName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LbName.Name = "LbName";
            this.LbName.Size = new System.Drawing.Size(94, 31);
            this.LbName.TabIndex = 115;
            this.LbName.Text = "Name:";
            // 
            // LbQty
            // 
            this.LbQty.AutoSize = true;
            this.LbQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbQty.Location = new System.Drawing.Point(49, 146);
            this.LbQty.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LbQty.Name = "LbQty";
            this.LbQty.Size = new System.Drawing.Size(65, 31);
            this.LbQty.TabIndex = 116;
            this.LbQty.Text = "Qty:";
            // 
            // LbUnitPrice
            // 
            this.LbUnitPrice.AutoSize = true;
            this.LbUnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbUnitPrice.Location = new System.Drawing.Point(49, 184);
            this.LbUnitPrice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LbUnitPrice.Name = "LbUnitPrice";
            this.LbUnitPrice.Size = new System.Drawing.Size(140, 31);
            this.LbUnitPrice.TabIndex = 117;
            this.LbUnitPrice.Text = "Unit Price:";
            // 
            // LbExpiryDate
            // 
            this.LbExpiryDate.AutoSize = true;
            this.LbExpiryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbExpiryDate.Location = new System.Drawing.Point(49, 232);
            this.LbExpiryDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LbExpiryDate.Name = "LbExpiryDate";
            this.LbExpiryDate.Size = new System.Drawing.Size(162, 31);
            this.LbExpiryDate.TabIndex = 118;
            this.LbExpiryDate.Text = "Expiry Date:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(286, 55);
            this.txtID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtID.Multiline = true;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(233, 37);
            this.txtID.TabIndex = 119;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(286, 98);
            this.txtName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(233, 37);
            this.txtName.TabIndex = 120;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(286, 140);
            this.txtQty.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtQty.Multiline = true;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(233, 37);
            this.txtQty.TabIndex = 121;
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Location = new System.Drawing.Point(286, 179);
            this.txtUnitPrice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtUnitPrice.Multiline = true;
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Size = new System.Drawing.Size(233, 37);
            this.txtUnitPrice.TabIndex = 122;
            // 
            // expiryDate
            // 
            this.expiryDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expiryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.expiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.expiryDate.Location = new System.Drawing.Point(286, 227);
            this.expiryDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.expiryDate.Name = "expiryDate";
            this.expiryDate.Size = new System.Drawing.Size(233, 38);
            this.expiryDate.TabIndex = 115;
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.Image")));
            this.BtnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSave.Location = new System.Drawing.Point(44, 290);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(153, 49);
            this.BtnSave.TabIndex = 124;
            this.BtnSave.Text = "Save";
            this.BtnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnSearch.Image")));
            this.BtnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSearch.Location = new System.Drawing.Point(414, 290);
            this.BtnSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(153, 49);
            this.BtnSearch.TabIndex = 125;
            this.BtnSearch.Text = "Search";
            this.BtnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.Image = ((System.Drawing.Image)(resources.GetObject("BtnEdit.Image")));
            this.BtnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnEdit.Location = new System.Drawing.Point(229, 290);
            this.BtnEdit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(153, 49);
            this.BtnEdit.TabIndex = 126;
            this.BtnEdit.Text = "Edit";
            this.BtnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // BtnRemove
            // 
            this.BtnRemove.BackColor = System.Drawing.Color.White;
            this.BtnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRemove.Image = ((System.Drawing.Image)(resources.GetObject("BtnRemove.Image")));
            this.BtnRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnRemove.Location = new System.Drawing.Point(599, 290);
            this.BtnRemove.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnRemove.Name = "BtnRemove";
            this.BtnRemove.Size = new System.Drawing.Size(153, 49);
            this.BtnRemove.TabIndex = 127;
            this.BtnRemove.Text = "Remove";
            this.BtnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnRemove.UseVisualStyleBackColor = false;
            this.BtnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // BtnReport
            // 
            this.BtnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReport.Image = ((System.Drawing.Image)(resources.GetObject("BtnReport.Image")));
            this.BtnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnReport.Location = new System.Drawing.Point(599, 214);
            this.BtnReport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnReport.Name = "BtnReport";
            this.BtnReport.Size = new System.Drawing.Size(153, 49);
            this.BtnReport.TabIndex = 128;
            this.BtnReport.Text = "Report";
            this.BtnReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnReport.UseVisualStyleBackColor = true;
            this.BtnReport.Click += new System.EventHandler(this.BtnReport_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(93, 374);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(625, 162);
            this.dataGridView1.TabIndex = 129;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // FormMedicine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 547);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.BtnReport);
            this.Controls.Add(this.BtnRemove);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.expiryDate);
            this.Controls.Add(this.txtUnitPrice);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.LbExpiryDate);
            this.Controls.Add(this.LbUnitPrice);
            this.Controls.Add(this.LbQty);
            this.Controls.Add(this.LbName);
            this.Controls.Add(this.LbId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReturn);
            this.Name = "FormMedicine";
            this.Text = "FormMedicine";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMedicine_FormClosed);
            this.Load += new System.EventHandler(this.FormMedicine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LbId;
        private System.Windows.Forms.Label LbName;
        private System.Windows.Forms.Label LbQty;
        private System.Windows.Forms.Label LbUnitPrice;
        private System.Windows.Forms.Label LbExpiryDate;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.DateTimePicker expiryDate;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnRemove;
        private System.Windows.Forms.Button BtnReport;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}