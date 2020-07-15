namespace RealCompare
{
    partial class frmAddRealizMainKass
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
            this.btSave = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbVVO = new System.Windows.Forms.RadioButton();
            this.rbAllWithOutVVO = new System.Windows.Forms.RadioButton();
            this.tbRealiz = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSave.BackgroundImage = global::RealCompare.Properties.Resources.filesave_2175;
            this.btSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btSave.Location = new System.Drawing.Point(239, 130);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(32, 32);
            this.btSave.TabIndex = 3;
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.BackgroundImage = global::RealCompare.Properties.Resources.exit_8633;
            this.btClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btClose.Location = new System.Drawing.Point(277, 130);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 4;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата реализации";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(114, 12);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(81, 20);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.CloseUp += new System.EventHandler(this.dtpDate_CloseUp);
            this.dtpDate.Leave += new System.EventHandler(this.dtpDate_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbVVO);
            this.groupBox1.Controls.Add(this.rbAllWithOutVVO);
            this.groupBox1.Location = new System.Drawing.Point(15, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 47);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Признак реализации";
            // 
            // rbVVO
            // 
            this.rbVVO.AutoSize = true;
            this.rbVVO.Location = new System.Drawing.Point(188, 19);
            this.rbVVO.Name = "rbVVO";
            this.rbVVO.Size = new System.Drawing.Size(79, 17);
            this.rbVVO.TabIndex = 1;
            this.rbVVO.Text = "отдел ВВО";
            this.rbVVO.UseVisualStyleBackColor = true;
            this.rbVVO.Click += new System.EventHandler(this.rbAllWithOutVVO_Click);
            // 
            // rbAllWithOutVVO
            // 
            this.rbAllWithOutVVO.AutoSize = true;
            this.rbAllWithOutVVO.Checked = true;
            this.rbAllWithOutVVO.Location = new System.Drawing.Point(8, 19);
            this.rbAllWithOutVVO.Name = "rbAllWithOutVVO";
            this.rbAllWithOutVVO.Size = new System.Drawing.Size(146, 17);
            this.rbAllWithOutVVO.TabIndex = 0;
            this.rbAllWithOutVVO.TabStop = true;
            this.rbAllWithOutVVO.Text = "все отделы, кроме ВВО";
            this.rbAllWithOutVVO.UseVisualStyleBackColor = true;
            this.rbAllWithOutVVO.Click += new System.EventHandler(this.rbAllWithOutVVO_Click);
            // 
            // tbRealiz
            // 
            this.tbRealiz.Location = new System.Drawing.Point(86, 91);
            this.tbRealiz.MaxLength = 20;
            this.tbRealiz.Name = "tbRealiz";
            this.tbRealiz.Size = new System.Drawing.Size(221, 20);
            this.tbRealiz.TabIndex = 2;
            this.tbRealiz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRealiz.TextChanged += new System.EventHandler(this.tbRealiz_TextChanged);
            this.tbRealiz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRealiz_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Реализация";
            // 
            // frmAddRealizMainKass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 170);
            this.ControlBox = false;
            this.Controls.Add(this.tbRealiz);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmAddRealizMainKass";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddRealizMainKass";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddRealizMainKass_FormClosing);
            this.Load += new System.EventHandler(this.frmAddRealizMainKass_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbVVO;
        private System.Windows.Forms.RadioButton rbAllWithOutVVO;
        private System.Windows.Forms.TextBox tbRealiz;
        private System.Windows.Forms.Label label2;
    }
}