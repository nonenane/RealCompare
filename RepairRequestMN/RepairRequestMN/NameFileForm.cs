using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RepairRequestMN
{
  public partial class NameFileForm : Form
  {
    public string getComment { private set; get; }
    public NameFileForm()
    {
      InitializeComponent();
    }

    private void btClose_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
    }

    private void btSelect_Click(object sender, EventArgs e)
    {
      if (tbName.Text.Trim().Length == 0)
      {
        MessageBox.Show("Необходимо ввести имя файла!", "Информирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      getComment = tbName.Text.Trim();
      this.DialogResult = DialogResult.OK;
    }
  }
}
