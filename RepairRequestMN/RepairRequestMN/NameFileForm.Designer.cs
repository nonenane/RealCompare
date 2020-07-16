namespace RepairRequestMN
{
  partial class NameFileForm
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
      this.tbName = new System.Windows.Forms.TextBox();
      this.btSelect = new System.Windows.Forms.Button();
      this.btClose = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // tbName
      // 
      this.tbName.Location = new System.Drawing.Point(12, 12);
      this.tbName.MaxLength = 150;
      this.tbName.Name = "tbName";
      this.tbName.Size = new System.Drawing.Size(240, 20);
      this.tbName.TabIndex = 15;
      // 
      // btSelect
      // 
      this.btSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btSelect.Image = global::RepairRequestMN.Properties.Resources.Select;
      this.btSelect.Location = new System.Drawing.Point(182, 38);
      this.btSelect.Name = "btSelect";
      this.btSelect.Size = new System.Drawing.Size(32, 32);
      this.btSelect.TabIndex = 13;
      this.toolTip1.SetToolTip(this.btSelect, "Подтвердить");
      this.btSelect.UseVisualStyleBackColor = true;
      this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
      // 
      // btClose
      // 
      this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btClose.Image = global::RepairRequestMN.Properties.Resources.exit_8633;
      this.btClose.Location = new System.Drawing.Point(220, 38);
      this.btClose.Name = "btClose";
      this.btClose.Size = new System.Drawing.Size(32, 32);
      this.btClose.TabIndex = 14;
      this.toolTip1.SetToolTip(this.btClose, "Выход");
      this.btClose.UseVisualStyleBackColor = true;
      this.btClose.Click += new System.EventHandler(this.btClose_Click);
      // 
      // NameFileForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(264, 82);
      this.ControlBox = false;
      this.Controls.Add(this.tbName);
      this.Controls.Add(this.btSelect);
      this.Controls.Add(this.btClose);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "NameFileForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Ввод имени файла";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbName;
    private System.Windows.Forms.Button btSelect;
    private System.Windows.Forms.Button btClose;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}