namespace RealCompare
{
    partial class frmCreateReport
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
            this.btClose = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbNullDataValidate = new System.Windows.Forms.RadioButton();
            this.rbErrorRealiz = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.BackgroundImage = global::RealCompare.Properties.Resources.exit_8633;
            this.btClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btClose.Location = new System.Drawing.Point(298, 122);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(32, 32);
            this.btClose.TabIndex = 5;
            this.btClose.UseVisualStyleBackColor = true;
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.BackgroundImage = global::RealCompare.Properties.Resources.printer;
            this.btPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btPrint.Location = new System.Drawing.Point(260, 122);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(32, 32);
            this.btPrint.TabIndex = 5;
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(93, 12);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(87, 20);
            this.dtpStart.TabIndex = 6;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Период с";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(237, 12);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(87, 20);
            this.dtpEnd.TabIndex = 6;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "по";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbNullDataValidate);
            this.groupBox1.Controls.Add(this.rbErrorRealiz);
            this.groupBox1.Location = new System.Drawing.Point(23, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 73);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип отчета";
            // 
            // rbNullDataValidate
            // 
            this.rbNullDataValidate.AutoSize = true;
            this.rbNullDataValidate.Location = new System.Drawing.Point(18, 42);
            this.rbNullDataValidate.Name = "rbNullDataValidate";
            this.rbNullDataValidate.Size = new System.Drawing.Size(207, 17);
            this.rbNullDataValidate.TabIndex = 0;
            this.rbNullDataValidate.TabStop = true;
            this.rbNullDataValidate.Text = "Отчет об отсутствии сверки данных";
            this.rbNullDataValidate.UseVisualStyleBackColor = true;
            // 
            // rbErrorRealiz
            // 
            this.rbErrorRealiz.AutoSize = true;
            this.rbErrorRealiz.Checked = true;
            this.rbErrorRealiz.Location = new System.Drawing.Point(18, 19);
            this.rbErrorRealiz.Name = "rbErrorRealiz";
            this.rbErrorRealiz.Size = new System.Drawing.Size(190, 17);
            this.rbErrorRealiz.TabIndex = 0;
            this.rbErrorRealiz.TabStop = true;
            this.rbErrorRealiz.Text = "Отчет по ошибкам в реализации";
            this.rbErrorRealiz.UseVisualStyleBackColor = true;
            // 
            // frmCreateReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 162);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreateReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отчеты";
            this.Load += new System.EventHandler(this.frmCreateReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbNullDataValidate;
        private System.Windows.Forms.RadioButton rbErrorRealiz;
    }
}