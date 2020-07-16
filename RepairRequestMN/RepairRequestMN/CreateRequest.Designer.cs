namespace RepairRequestMN
{
  partial class CreateRequest
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_Date = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_Time = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_Hardware = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Number = new System.Windows.Forms.TextBox();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.richTextBox_Description = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btDoc = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.mtbIp = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дата подачи:";
            // 
            // dateTimePicker_Date
            // 
            this.dateTimePicker_Date.Enabled = false;
            this.dateTimePicker_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_Date.Location = new System.Drawing.Point(90, 11);
            this.dateTimePicker_Date.Name = "dateTimePicker_Date";
            this.dateTimePicker_Date.Size = new System.Drawing.Size(98, 20);
            this.dateTimePicker_Date.TabIndex = 1;
            // 
            // dateTimePicker_Time
            // 
            this.dateTimePicker_Time.Enabled = false;
            this.dateTimePicker_Time.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker_Time.Location = new System.Drawing.Point(276, 11);
            this.dateTimePicker_Time.Name = "dateTimePicker_Time";
            this.dateTimePicker_Time.ShowUpDown = true;
            this.dateTimePicker_Time.Size = new System.Drawing.Size(124, 20);
            this.dateTimePicker_Time.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Время подачи:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Оборудование";
            // 
            // comboBox_Hardware
            // 
            this.comboBox_Hardware.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Hardware.FormattingEnabled = true;
            this.comboBox_Hardware.Location = new System.Drawing.Point(116, 42);
            this.comboBox_Hardware.Name = "comboBox_Hardware";
            this.comboBox_Hardware.Size = new System.Drawing.Size(284, 21);
            this.comboBox_Hardware.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "№ кабинета:";
            // 
            // textBox_Number
            // 
            this.textBox_Number.Location = new System.Drawing.Point(116, 79);
            this.textBox_Number.Name = "textBox_Number";
            this.textBox_Number.Size = new System.Drawing.Size(284, 20);
            this.textBox_Number.TabIndex = 7;
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(116, 114);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(284, 20);
            this.textBox_Name.TabIndex = 8;
            this.textBox_Name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Name_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "ФИО";
            // 
            // richTextBox_Description
            // 
            this.richTextBox_Description.Location = new System.Drawing.Point(116, 181);
            this.richTextBox_Description.Name = "richTextBox_Description";
            this.richTextBox_Description.Size = new System.Drawing.Size(282, 151);
            this.richTextBox_Description.TabIndex = 10;
            this.richTextBox_Description.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Описание неисправности:";
            // 
            // button_Save
            // 
            this.button_Save.Image = global::RepairRequestMN.Properties.Resources.pict_save;
            this.button_Save.Location = new System.Drawing.Point(290, 341);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(50, 50);
            this.button_Save.TabIndex = 12;
            this.toolTip1.SetToolTip(this.button_Save, "Сохранить");
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Close
            // 
            this.button_Close.Image = global::RepairRequestMN.Properties.Resources.pict_close;
            this.button_Close.Location = new System.Drawing.Point(346, 341);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(50, 50);
            this.button_Close.TabIndex = 13;
            this.toolTip1.SetToolTip(this.button_Close, "Выйти");
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // btDoc
            // 
            this.btDoc.Image = global::RepairRequestMN.Properties.Resources.document_library;
            this.btDoc.Location = new System.Drawing.Point(12, 341);
            this.btDoc.Name = "btDoc";
            this.btDoc.Size = new System.Drawing.Size(50, 50);
            this.btDoc.TabIndex = 11;
            this.toolTip1.SetToolTip(this.btDoc, "Работа с документами");
            this.btDoc.UseVisualStyleBackColor = true;
            this.btDoc.Click += new System.EventHandler(this.btDoc_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "ip адрес";
            // 
            // mtbIp
            // 
            this.mtbIp.Location = new System.Drawing.Point(116, 139);
            this.mtbIp.Name = "mtbIp";
            this.mtbIp.Size = new System.Drawing.Size(284, 20);
            this.mtbIp.TabIndex = 9;
            this.mtbIp.TextChanged += new System.EventHandler(this.mtbIp_TextChanged);
            this.mtbIp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtbIp_KeyPress);
            // 
            // CreateRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 399);
            this.ControlBox = false;
            this.Controls.Add(this.btDoc);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.richTextBox_Description);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.mtbIp);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_Number);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox_Hardware);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker_Time);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker_Date);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CreateRequest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подача заявки на ремонт";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateRequest_FormClosing);
            this.Load += new System.EventHandler(this.CreateRequest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.DateTimePicker dateTimePicker_Date;
    private System.Windows.Forms.DateTimePicker dateTimePicker_Time;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox comboBox_Hardware;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox textBox_Number;
    private System.Windows.Forms.TextBox textBox_Name;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.RichTextBox richTextBox_Description;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button button_Save;
    private System.Windows.Forms.Button button_Close;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox mtbIp;
    private System.Windows.Forms.Button btDoc;
  }
}