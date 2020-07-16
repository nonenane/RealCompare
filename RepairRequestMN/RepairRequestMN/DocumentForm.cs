using System;
using System.Data;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace RepairRequestMN
{
  public partial class DocumentForm : Form
  {
    private readonly Sql _sql = new Sql(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(),
      ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
    private int ZoomValue = 10;
    public int id_Req { private get; set; }
    private DataTable dtScan;
    private bool isView = false;
    public DocumentForm(bool isView)
    {
      InitializeComponent();
      Sql.bufferDataTable = null;
      this.isView = isView;
      dgvScan.AutoGenerateColumns = false;

      btSaveDoc.Visible = !id_Req.Equals(0);
      btDel.Visible = btEditName.Visible = btAddFile.Visible = btScan.Visible = btSave.Visible = !isView;
    }

    private void DocumentForm_Load(object sender, EventArgs e)
    {
      getData();
    }

    private void getData()
    {
      dtScan = _sql.getScan(id_Req, -1);
      if (id_Req == 0)
        Sql.bufferDataTable = dtScan.Clone();
      else
        Sql.bufferDataTable = dtScan.Copy();
      dgvScan.DataSource = Sql.bufferDataTable;
    }

    private void btZoomOut_Click(object sender, EventArgs e)
    {
      try
      {
        ZoomValue -= 1;
        if (ZoomValue == 0)
          ZoomValue = 1;
        imagePanel1.Zoom = ZoomValue * 0.02f;
      }
      catch (Exception)
      {
      }
    }

    private void btZoomIn_Click(object sender, EventArgs e)
    {
      try
      {
        ZoomValue += 1;
        imagePanel1.Zoom = ZoomValue * 0.02f;
      }
      catch (Exception)
      {

      }
    }

    private void Scan()
    {
      try
      {
        Nwuram.Framework.scan.scanImg fImg = new Nwuram.Framework.scan.scanImg();
        fImg.ShowDialog();
        this.TopMost = true;
        byte[] img_array = fImg.img_array;
        this.TopMost = false;
        if (img_array != null)
        {
          NameFileForm frmNF = new NameFileForm();
          if (DialogResult.OK == frmNF.ShowDialog())
          {
            string fileName = frmNF.getComment;
            byte[] byteFile = img_array;
            saveFileToDataBase(byteFile, fileName, ".jpg");
          }
        }
      }
      catch
      {
        MessageBox.Show("Ошибка при работе со сканером!");
      }
    }

    private void addFile()
    {
      OpenFileDialog fileDialog = new OpenFileDialog();
      fileDialog.Filter = @"(All Image Files)|*.BMP;*.bmp;*.JPG;*.JPEG*.jpg;*.jpeg;*.PNG;*.png;*.GIF;*.gif;*.tif;*.tiff;*.ico;*.ICO" +
        "|(Microsoft Word)|*.doc;*.docx" +
        "|(Microsoft Excel)|*.xls;*.xlsx" +
        "|(Portable Document Format)|*.pdf";
      DialogResult dr = new DialogResult();
      Thread thread = new Thread(() => dr = fileDialog.ShowDialog());
      thread.SetApartmentState(ApartmentState.STA);
      thread.Start();
      thread.Join();
      if (dr == DialogResult.OK)
      {
        string fileName = Path.GetFileNameWithoutExtension(fileDialog.FileName);
        byte[] byteFile = File.ReadAllBytes(fileDialog.FileName);
        string @Extension = Path.GetExtension(fileDialog.FileName);

        saveFileToDataBase(byteFile, fileName, @Extension);
      }
    }

    private void btScan_Click(object sender, EventArgs e)
    {
      try
      {
        Scan();
      }
      catch (Exception)
      { }
    }

    private void btClose_Click(object sender, EventArgs e)
    {
      try
      {
        bool changed = false;
        DataTable dtOldScan = _sql.getScan(id_Req, -1);
        if (dtScan.Rows.Count == dtOldScan.Rows.Count)
        {
          if (dtOldScan.Rows.Count != 0)
          {
            for (int i = 0; i < dtOldScan.Rows.Count; i++)
            {
              if (dtScan.Rows[i]["id"].ToString() != dtOldScan.Rows[i]["id"].ToString() ||
                dtScan.Rows[i]["cName"].ToString() != dtOldScan.Rows[i]["cName"].ToString())
              {
                changed = true;
                break;
              }
            }
          }
        }
        else
          changed = true;
        if (changed)
        {
          if (MessageBox.Show("На форме были внесены изменения. \nВыйти без сохранения?", "Запрос на выход",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
          { this.DialogResult = DialogResult.Cancel; }
        }
        else
          this.DialogResult = DialogResult.Cancel;
      }
      catch (Exception)
      {

      }
    }

    private void btView_Click(object sender, EventArgs e)
    {
      try
      {
        if (Sql.bufferDataTable != null && Sql.bufferDataTable.DefaultView.Count > 0 && dgvScan.CurrentRow != null && id_Req != 0)
        {
          int indexRow = dgvScan.CurrentRow.Index;
          int id = int.Parse(Sql.bufferDataTable.DefaultView[indexRow]["id"].ToString());
          DataTable dtFile = _sql.getScan(id_Req, id);
          if (dtFile != null && dtFile.Rows.Count > 0 && dtFile.Rows[0]["Scan"] != DBNull.Value)
          {
            byte[] img = (byte[])dtFile.Rows[0]["Scan"];
            string @Extension = (string)Sql.bufferDataTable.DefaultView[indexRow]["Extension"];
            try
            {
              using (var fs = new FileStream("tmpFile" + @Extension, FileMode.Create, FileAccess.Write))
              {
                fs.Write(img, 0, img.Length);
                Process.Start("tmpFile" + @Extension);
                return;
              }
            }
            catch (Exception ex)
            {
              Console.WriteLine("Exception caught in process: {0}", ex);
              return;
            }
          }
        }
        else
          if (id_Req == 0 && Sql.bufferDataTable != null && Sql.bufferDataTable.Rows.Count > 0 && dgvScan.CurrentRow != null)
          {
            int indexRow = dgvScan.CurrentRow.Index;
            byte[] img = (byte[])Sql.bufferDataTable.DefaultView[indexRow]["Scan"];
            string @Extension = (string)Sql.bufferDataTable.DefaultView[indexRow]["Extension"];

            try
            {
              using (var fs = new FileStream("tmpFile" + @Extension, FileMode.Create, FileAccess.Write))
              {
                fs.Write(img, 0, img.Length);
                Process.Start("tmpFile" + @Extension);
                return;
              }
            }
            catch (Exception ex)
            {
              Console.WriteLine("Exception caught in process: {0}", ex);
              return;
            }
          }
      }
      catch (Exception)
      { }
    }

    private void btAddFile_Click(object sender, EventArgs e)
    {
      try
      {
        addFile();
      }
      catch (Exception)
      {
      }
    }

    List<int> lRename = new List<int>();
    private void btEditName_Click(object sender, EventArgs e)
    {
      try
      {
        int indexRow = dgvScan.CurrentRow.Index;
        NameFileForm frmNF = new NameFileForm();
        if (DialogResult.OK == frmNF.ShowDialog())
        {
          if (id_Req != 0 && Sql.bufferDataTable != null && Sql.bufferDataTable.DefaultView.Count > 0 && dgvScan.CurrentRow != null)
          {
            int ind = dgvScan.CurrentRow.Index;
            int id = int.Parse(Sql.bufferDataTable.DefaultView[ind]["id"].ToString());
            lRename.Add(id);
          }
          string fileName = frmNF.getComment;
          Sql.bufferDataTable.DefaultView[indexRow]["cName"] = fileName;
          Sql.bufferDataTable.AcceptChanges();
        }
      }
      catch (Exception)
      {
      }
    }

    private void saveFileToDataBase(byte[] byteFile, string nameFile, string Extension)
    {
      DataRow row = Sql.bufferDataTable.Rows.Add();
      row["id"] = -1;
      row["cName"] = nameFile;
      row["Scan"] = byteFile;
      row["Extension"] = @Extension;
      dgvScan_CurrentCellChanged(null, null);
    }

    private void dgvScan_CurrentCellChanged(object sender, EventArgs e)
    {
      if (Sql.bufferDataTable != null && Sql.bufferDataTable.Rows.Count > 0 &&
        dgvScan.CurrentRow != null)
      {
        int indexRow = dgvScan.CurrentRow.Index;
        if (Sql.bufferDataTable.DefaultView[indexRow]["id"] == DBNull.Value) return;

        int id = int.Parse(Sql.bufferDataTable.DefaultView[indexRow]["id"].ToString());

        byte[] img = (byte[])Sql.bufferDataTable.DefaultView[indexRow]["Scan"];
        string @Extension = (string)Sql.bufferDataTable.DefaultView[indexRow]["Extension"];
        try
        {
          MemoryStream ms = new MemoryStream(img);
          Bitmap b = new Bitmap(ms);
          imagePanel1.Image = (Bitmap)b;
          ZoomValue = 10;
          imagePanel1.Zoom = ZoomValue * 0.02f;
        }
        catch (Exception ex)
        {
          Console.WriteLine("Exception caught in process: {0}", ex);
          imagePanel1.Image = null;
          return;
        }
      }
      else
      {
        imagePanel1.Image = null;
      }
    }

    private void tbNameImg_TextChanged(object sender, EventArgs e)
    {
      setFilter();
    }
    private void setFilter()
    {
      string filter = "";

      filter += (tbNameImg.Text.Length != 0 ?
        (filter.Length == 0 ? "" : " AND ") + "CONVERT(cName, 'System.String') LIKE '%" + tbNameImg.Text + "%'" : "");

      try
      {
        Sql.bufferDataTable.DefaultView.RowFilter = filter;
      }
      catch (Exception)
      {
        MessageBox.Show("Некорректное значение фильтра!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    List<int> lDelete = new List<int>();
    private void btDel_Click(object sender, EventArgs e)
    {
      try
      {
        if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo,
          MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
        {
          if (id_Req != 0 && Sql.bufferDataTable != null && Sql.bufferDataTable.DefaultView.Count > 0 &&
            dgvScan.CurrentRow != null)
          {
            int ind = dgvScan.CurrentRow.Index;
            int id = int.Parse(Sql.bufferDataTable.DefaultView[ind]["id"].ToString());
            lDelete.Add(id);
            if (lRename != null && lRename.Count != 0 && lRename.Contains(id))
              lRename.Remove(id);
          }

          int indexRow = dgvScan.CurrentRow.Index;
          dgvScan.Rows.RemoveAt(indexRow);
          Sql.bufferDataTable.AcceptChanges();
        }
      }
      catch (Exception)
      {

      }
    }

    private void DocumentForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }

    private void btSaveDoc_Click(object sender, EventArgs e)
    {
      try
      {
        if (id_Req != 0 && Sql.bufferDataTable != null && Sql.bufferDataTable.DefaultView.Count > 0 && dgvScan.CurrentRow != null)
        {
          int indexRow = dgvScan.CurrentRow.Index;
          int id = int.Parse(Sql.bufferDataTable.DefaultView[indexRow]["id"].ToString());
          string name = Sql.bufferDataTable.DefaultView[indexRow]["cName"].ToString();
          string extension = Sql.bufferDataTable.DefaultView[indexRow]["Extension"].ToString();
          byte[] file = new byte[0];
          DataTable dtFile = _sql.getScan(id_Req, id);
          if (dtFile != null && dtFile.Rows.Count > 0 && dtFile.Rows[0]["Scan"] != DBNull.Value)
            file = (byte[])dtFile.Rows[0]["Scan"];

          SaveFileDialog fileDialog = new SaveFileDialog();
          fileDialog.Filter = @"(All Image Files)|*.BMP;*.bmp;*.JPG;*.JPEG*.jpg;*.jpeg;*.PNG;*.png;*.GIF;*.gif;*.tif;*.tiff;*.ico;*.ICO" +
            "|(Microsoft Word)|*.doc;*.docx" +
            "|(Microsoft Excel)|*.xls;*.xlsx" +
            "|(Portable Document Format)|*.pdf";
          fileDialog.FileName = name + extension;
          if (fileDialog.ShowDialog() == DialogResult.OK)
          {
            File.WriteAllBytes(fileDialog.FileName, file);
          }
        }
      }
      catch (Exception)
      {

      }
    }

    private void btSave_Click(object sender, EventArgs e)
    {
      try
      {
        if (id_Req != 0)
        {
          if (lDelete != null && lDelete.Count != 0)
            foreach (int del in lDelete)
              _sql.delScan(del);

          if (lRename != null && lRename.Count != 0)
            foreach (int rename in lRename)
            {
              foreach (DataRow r in Sql.bufferDataTable.Rows)
                if (r["id"].ToString() == rename.ToString())
                {
                  _sql.updateScanName(rename, r["cName"].ToString());
                  break;
                }
            }
          foreach (DataRow r in Sql.bufferDataTable.Rows)
          {
            if ((int)r["id"] == -1)
              _sql.setScan(id_Req, (byte[])r["Scan"], r["cName"].ToString(), r["Extension"].ToString());
          }
        }
        this.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.Close();
      }
      catch (Exception)
      {
      }
    }

    private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
    {
      DataGridView dgv = (DataGridView)sender;
      if (dgv.CurrentCell != null)
      {
        Color RowColor = Color.White;

        dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor =
          dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = RowColor;

        dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
      }
    }

    private void dgw_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
    {
      DataGridView dgv = (DataGridView)sender;
      //Рисуем рамку для выделеной строки
      if (e.RowIndex != -1 && dgv.Rows[e.RowIndex].Selected)
      {
        int width = dgv.Rows[e.RowIndex].Cells[0].Size.Width;
        Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
        Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

        ControlPaint.DrawBorder(e.Graphics, rect,
          Color.FromKnownColor(KnownColor.Black), 2, ButtonBorderStyle.Solid,
          Color.FromKnownColor(KnownColor.Black), 2, ButtonBorderStyle.Solid,
          Color.FromKnownColor(KnownColor.Black), 2, ButtonBorderStyle.Solid,
          Color.FromKnownColor(KnownColor.Black), 2, ButtonBorderStyle.Solid);
      }
    }
  }
}
