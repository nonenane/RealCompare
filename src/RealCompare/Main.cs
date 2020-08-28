using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using System.Collections;
using System.Data.OleDb;
using System.Reflection;
using System.Text.RegularExpressions;
using Nwuram.Framework.ToExcel;
using System.Threading.Tasks;
using Nwuram.Framework.Logging;

namespace RealCompare
{

    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btAdd, "Добавить");
            tp.SetToolTip(btEdit, "Редактировать");
            tp.SetToolTip(btDel, "Удалить");
            tp.SetToolTip(btViewRepair, "Просмотр заявки на ремонт");
        }

        //private BindingSource bsGrdMain = new BindingSource();
        private DataTable //resultTable, 
            dtResult, Departments;
        //ArrayList alDataColumns = new ArrayList();

        private void Main_Load(object sender, EventArgs e)
        {                       
            dgvMain.AutoGenerateColumns = false;
            dgvRepaireRequest.AutoGenerateColumns = false;
            Parameters.hConnect = new SqlWorker(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            Parameters.hConnectVVO = new SqlWorker(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            Parameters.hConnectKass = new SqlWorker(ConnectionSettings.GetServer("3"), ConnectionSettings.GetDatabase("3"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            Parameters.hConnectVVOKass = new SqlWorker(ConnectionSettings.GetServer("4"), ConnectionSettings.GetDatabase("4"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (ConnectionSettings.GetServer("5").Length > 0)
                Parameters.hConnectX14 = new SqlWorker(ConnectionSettings.GetServer("5"), ConnectionSettings.GetDatabase("5"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            toolStripStatusLabel1.Text = $"Основной Сервер:{ConnectionSettings.GetServer()}:{ConnectionSettings.GetDatabase()}";


            if (ConnectionSettings.GetServer("2").Length != 0 || ConnectionSettings.GetServer("3").Length != 0 || ConnectionSettings.GetServer("4").Length != 0)
                toolStripStatusLabel1.Text += " | Дополнительные сервера:" +
               (ConnectionSettings.GetServer("2").Length != 0 ? $" {ConnectionSettings.GetServer("2")}:{ConnectionSettings.GetDatabase("2")} | " : $"") +
                (ConnectionSettings.GetServer("3").Length != 0 ? $" {ConnectionSettings.GetServer("3")}:{ConnectionSettings.GetDatabase("3")} | " : $"") +
                (ConnectionSettings.GetServer("4").Length != 0 ? $" {ConnectionSettings.GetServer("4")}:{ConnectionSettings.GetDatabase("4")}" : $"");

            //Console.WriteLine(ConnectionSettings.GetServer("5")+"  :  "+ ConnectionSettings.GetDatabase("5"));

            btAdd.Visible = btEdit.Visible = btDel.Visible = !new List<string> { "ПР" }.Contains(UserSettings.User.StatusCode);

            this.Text = ConnectionSettings.ProgramName + " " + UserSettings.User.FullUsername;
            DateTime curDate = Parameters.hConnect.GetDate().AddDays(-1);

            dtpEnd.Value = curDate.Date;
            dtpStart.Value = curDate.Date;
            GetDepartments();
            EnableByGroups();


            if (checkedCount() != 2)
                cDelta.Visible = false;
            else
            {

                cDelta.Visible = true;
                tbTotalcDelta.Visible = true;
                cDelta.Width = 65;
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из программы?", "Запрос на выход.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();

                Regex filePattern = new Regex(@"\d+.dbf");
                DirectoryInfo dInf = new DirectoryInfo(Application.StartupPath);
                FileInfo[] files = dInf.GetFiles("*.dbf");

                foreach (FileInfo fi in files)
                {
                    if (filePattern.Match(fi.Name).Success)
                    {
                        try
                        {
                            fi.Delete();
                        }
                        catch (IOException) { }
                        catch (System.Security.SecurityException) { }
                        catch (UnauthorizedAccessException) { }
                    }
                }
            }
        }

        /// <summary>
        /// Получение списка отделов
        /// </summary>
        private void GetDepartments()
        {
            Departments = Parameters.hConnect.GetDepartments();
            cbDeps.DataSource = Departments.Copy();
            cbDeps.ValueMember = "id";
            cbDeps.DisplayMember = "name";
        }

        private void cbDeps_SelectedValueChanged(object sender, EventArgs e)
        {
            int curId;
            if (int.TryParse(cbDeps.SelectedValue.ToString(), out curId))
            {
                GetTUGroups(curId);
                SetFilter();
            }
        }

        #region "Работа с гридом"

        private void dgwMain_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            setTotalTextBoxPosition();
        }    
     
        private void setTotalTextBoxPosition()
        {
            foreach (DataGridViewColumn Col in dgvMain.Columns)
            {
                //if (Col.Index >= 4 && Col.Visible)
                if(Col.Visible)
                {
                    foreach (Control con in this.Controls)
                    {
                        if (con.Name == "tbTotal" + Col.Name)
                        {
                            con.Location = new Point(dgvMain.GetCellDisplayRectangle(Col.HeaderCell.ColumnIndex, Col.HeaderCell.RowIndex, false).Left + dgvMain.Left, dgvMain.Top + dgvMain.Height + 5);
                            con.Width = Col.Width;
                            break;
                        }
                    }
                }

                string columnName = "";
                for (int i = 2; i >= 0; i--)
                {
                    if (dgvMain.Columns[i].Visible)
                    {
                        columnName = dgvMain.Columns[i].Name;
                        break;
                    }
                }

                if (columnName != "")
                    lbTotal.Location = new Point(dgvMain.GetCellDisplayRectangle(dgvMain.Columns[columnName].Index, -1, false).Right - lbTotal.Width, tbTotalKsSql.Location.Y + 3);
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            //setTotalTextBoxPosition();
            //foreach (DataGridViewColumn col in dgvMain.Columns)
            //{
            //    setTotalTextBoxPosition();
            //}
        }

        private void dgvMain_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            Color rColor = Color.White;
            rColor = (dgvMain.Rows[e.RowIndex].Cells["isRealEquals"].Value is bool && (bool)dgvMain.Rows[e.RowIndex].Cells["isRealEquals"].Value) ? Color.White : panel1.BackColor;

            dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvMain.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;




            if (chbMainKass.Checked && rbDateAndVVO.Checked)
            {
                if (dtResult.DefaultView[e.RowIndex]["ChessBoard"] != DBNull.Value
                    && dtResult.DefaultView[e.RowIndex]["RealSQL_vvo"] != DBNull.Value)
                {
                    dgvMain.Rows[e.RowIndex].Cells["DateReal"].Style.BackColor =
                    dgvMain.Rows[e.RowIndex].Cells["DateReal"].Style.SelectionBackColor = panel3.BackColor;

                    if ((decimal)dtResult.DefaultView[e.RowIndex]["RealSql"] != (decimal)dtResult.DefaultView[e.RowIndex]["RealSQL_vvo"]
                        || (decimal)dtResult.DefaultView[e.RowIndex]["KsSql"] != (decimal)dtResult.DefaultView[e.RowIndex]["ChessBoard"])
                    {
                        dgvMain.Rows[e.RowIndex].Cells["cMainKass"].Style.BackColor =
                            dgvMain.Rows[e.RowIndex].Cells["cMainKass"].Style.SelectionBackColor = panel2.BackColor;
                    }
                }
                else
                {
                    dgvMain.Rows[e.RowIndex].Cells["DateReal"].Style.BackColor =
                                dgvMain.Rows[e.RowIndex].Cells["DateReal"].Style.SelectionBackColor = Color.White;
                }
            }
            else
            {
                dgvMain.Rows[e.RowIndex].Cells["DateReal"].Style.BackColor =
                              dgvMain.Rows[e.RowIndex].Cells["DateReal"].Style.SelectionBackColor = Color.White;
            }

        }


        private void dgvMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1 && chbMainKass.Checked && rbDateAndVVO.Checked && !new List<string> { "ПР" }.Contains(UserSettings.User.StatusCode))
            {
                dgvMain.CurrentCell = dgvMain[e.ColumnIndex, e.RowIndex];
                cmsMainGridContext.Show(MousePosition);
            }
        }

        private void dgvMain_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            //Рисуем рамку для выделеной строки
            if (dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
                Rectangle r = dgv.GetRowDisplayRectangle(e.RowIndex, false);
                Rectangle rect = new Rectangle(r.X, r.Y, width - 1, r.Height - 1);

                ControlPaint.DrawBorder(e.Graphics, rect,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid,
                    SystemColors.Highlight, 2, ButtonBorderStyle.Solid);
            }
        }

        private void dgvMain_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtResult == null || dtResult.Rows.Count == 0 || dtResult.DefaultView.Count == 0 || dgvMain.Rows.Count == 0)
                {
                    tbDateAdd.Text = "";
                    tbFio.Text = "";
                    btDel.Enabled = btEdit.Enabled = false;
                    dgvRepaireRequest.DataSource = null;
                    btViewRepair.Enabled = false;
                    return;
                }

                if (rbDateAndVVO.Checked && chbMainKass.Checked)
                {
                    DataRowView _rowView = dtResult.DefaultView[dgvMain.CurrentRow.Index];

                    tbFio.Text = _rowView["FIO"].ToString();
                    tbDateAdd.Text = _rowView["DateEdit"].ToString();
                    btDel.Enabled = btEdit.Enabled =
                        _rowView["ChessBoard"] == DBNull.Value
                        && _rowView["RealSQL_vvo"] == DBNull.Value
                        && _rowView["id"] != DBNull.Value;

                    if (_rowView["id"] != DBNull.Value)
                    {
                        int id_MainKass = (int)_rowView["id"];
                        getListRepairRequestMainKass(id_MainKass);
                    }
                    else
                    {
                        btViewRepair.Enabled = false;
                        dgvRepaireRequest.DataSource = null;
                    }

                    return;
                }
                else
                {
                    tbDateAdd.Text = "";
                    tbFio.Text = "";
                    btDel.Enabled = btEdit.Enabled = false;
                    dgvRepaireRequest.DataSource = null;
                    return;
                }
            }
            catch
            {
                tbDateAdd.Text = "";
                tbFio.Text = "";
                btDel.Enabled = btEdit.Enabled = false;
                dgvRepaireRequest.DataSource = null;
            }
        }

        #endregion

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {

            if (dtpStart.Value.Month != dtpEnd.Value.Month || dtpStart.Value.Year != dtpEnd.Value.Year)
            {
                dtpEnd.Value = dtpEnd.Value.AddMonths((dtpStart.Value.Year - 1) * 12 + dtpStart.Value.Month - (dtpEnd.Value.Year - 1) * 12 - dtpEnd.Value.Month);
            }

            if (dtpStart.Value > dtpEnd.Value)
            {
                dtpEnd.Value = dtpStart.Value;
            }

            dgvMain.DataSource = null;
            tbDateAdd.Text = "";
            tbFio.Text = "";
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEnd.Value > DateTime.Now.AddDays(-1).Date)
            {
                dtpEnd.Value = DateTime.Now.AddDays(-1).Date;
            }

            if (dtpStart.Value.Month != dtpEnd.Value.Month || dtpStart.Value.Year != dtpEnd.Value.Year)
            {
                dtpStart.Value = dtpStart.Value.AddMonths((dtpEnd.Value.Year - 1) * 12 + dtpEnd.Value.Month - (dtpStart.Value.Year - 1) * 12 - dtpStart.Value.Month);
            }

            if (dtpStart.Value > dtpEnd.Value)
            {
                dtpStart.Value = dtpEnd.Value;
            }

            dgvMain.DataSource = null;
            reSumTotal();
            tbDateAdd.Text = "";
            tbFio.Text = "";
        }

        private void rbDate_CheckedChanged(object sender, EventArgs e)
        {
            EnableByGroups();
            visibleColumnDelta();
        }

        private void rbDateAndDep_CheckedChanged(object sender, EventArgs e)
        {
            EnableByGroups();
        }

        private void rbDateAndDepAndGood_CheckedChanged(object sender, EventArgs e)
        {
            EnableByGroups();
            visibleColumnDelta();
            if (cDelta.Visible) cDelta.Width = 65;
        }

        /// <summary>
        /// Изменение доступности элементов при смене чекбоксов в панели группировки
        /// </summary>
        private void EnableByGroups()
        {
            if (rbDate.Checked)
            {
                cbDeps.SelectedIndex = 0;
                cbDeps.Enabled = false;
                Department.Visible = false;
            }
            else if (rbDateAndVVO.Checked)
            {
                Department.Visible = true;
                cbDeps.SelectedIndex = 0;
                cbDeps.Enabled = false;
            }
            else
            {
                cbDeps.Enabled = true;
                Department.Visible = true; ;
            }

            if (rbDateAndDepAndGood.Checked)
            {
                tbEAN.Text = "";
                tbEAN.Enabled = true;
                tbName.Text = "";
                tbName.Enabled = true;
                EAN.Visible = true;
                cName.Visible = true;
                cbTUGroups.Enabled = true;
            }
            else
            {
                tbEAN.Text = "";
                tbEAN.Enabled = false;
                tbName.Text = "";
                tbName.Enabled = false;
                EAN.Visible = false;
                cName.Visible = false;
                cbTUGroups.Enabled = false;
            }

            if (chbMainKass.Checked)
            {
                //label3.Visible = tbFio.Visible = label4.Visible = tbDateAdd.Visible = rbDateAndVVO.Checked;
                //btViewRepair.Visible = dgvRepaireRequest.Visible = btAdd.Visible = btEdit.Visible = btDel.Visible = rbDateAndVVO.Checked;
                groupBox1.Visible = rbDateAndVVO.Checked;
                dgvMain_SelectionChanged(null, null);
            }

            
            dgvMain.DataSource = null;            

            cbDeps.SelectedIndex = 0;
            cbTUGroups.SelectedIndex = 0;
        }

        private void tbEAN_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back) && e.KeyChar != Convert.ToChar(Keys.Delete));
        }

        private void chkKsSql_CheckedChanged(object sender, EventArgs e)
        {
            if (chkKsSql.Checked)
            {
                KsSql.Visible = true;
                tbTotalKsSql.Visible = true;
                dgvMain.DataSource = null;
            }
            else
            {
                KsSql.Visible = false;
                tbTotalKsSql.Visible = false;
                reCountDelta();
                dgvMain.Invalidate();
            }

            dgvMain_SelectionChanged(null, null);
            visibleColumnDelta();

            //cDelta.Visible = tbTotalcDelta.Visible = tbTotalcDelta.Visible =  chkRealSql.Checked && !chkKsSql.Checked;
            //if (cDelta.Visible) cDelta.Width = 65;
        }

        private void chkRealSql_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRealSql.Checked)
            {
                RealSql.Visible = true;
                tbTotalRealSql.Visible = true;
                dgvMain.DataSource = null;
            }
            else
            {
                RealSql.Visible = false;
                tbTotalRealSql.Visible = false;
                reCountDelta();
                dgvMain.Invalidate();
                dgvMain.Refresh();
            }
            //cDelta.Visible = tbTotalcDelta.Visible = chkRealSql.Checked && !chkKsSql.Checked;
            //if (cDelta.Visible) cDelta.Width = 65;
            dgvMain_SelectionChanged(null, null);
            visibleColumnDelta();
        }

        #region Получение реализаций
        private void RefreshGrid()
        {
            if (checkedCount() < 2)
            {
                MessageBox.Show("В группе чек-боксов\n\"Источники данных\"\nдолжно быть отмечено\nминимум два источника.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }


            Logging.StartFirstLevel(1555);
            Logging.Comment("Произведена сверка реализаций со следующими параметрами:");
            Logging.Comment($"Период с {dtpStart.Value.ToShortDateString()} по {dtpEnd.Value.ToShortDateString()}");
            Logging.Comment($"Отдел ID: {cbDeps.SelectedValue}; Наименование: {cbDeps.Text}");
            Logging.Comment($"Т/У группы ID: {cbTUGroups.SelectedValue}; Наименование: {cbTUGroups.Text}");

            foreach (Control cnt in grpGroups.Controls)
            {
                if (cnt is RadioButton && (cnt as RadioButton).Checked)
                { Logging.Comment($"Группировка по \": {(cnt as RadioButton).Text}\""); break; }
            }

            foreach (Control cnt in grpSources.Controls)
            {
                if (cnt is CheckBox && (cnt as CheckBox).Checked)
                { Logging.Comment($"Источники данных: \": {(cnt as CheckBox).Text}\""); }
            }

            Logging.Comment($"Магазин: {(ConnectionSettings.GetServer().Contains("K21") ? "K21" : "X14")}");
            //if (tbTotalcMainKass.Visible)
            //    Logging.Comment($"Итого Главня касса: {tbTotalcMainKass.Text}");
            //if (tbTotalKsSql.Visible)
            //    Logging.Comment($"Итого Шахматка: {tbTotalKsSql.Text}");
            //if (tbTotalRealSql.Visible)
            //    Logging.Comment($"Итого Реал. SQL: {tbTotalRealSql.Text}");
            Logging.StopFirstLevel();


            //Определение параметров для выгрузки данных
            //Parameters.isRealDbf = chkRealDbf.Checked;
            Parameters.isKsSql = chkKsSql.Checked;
            //Parameters.isKsSql = chkKsSql.Checked;
            Parameters.isRealSql = chkRealSql.Checked;
            //Parameters.isRealSql2 = chkRealSql2.Checked;
            Parameters.dateStart = dtpStart.Value;
            Parameters.dateEnd = dtpEnd.Value;
            Parameters.idDep = int.Parse(cbDeps.SelectedValue.ToString());
            Parameters.groupType = rbDate.Checked ? 1 : rbDateAndDep.Checked ? 2 : 3;

            //Формируем список сравниваемых колонок
            //alDataColumns.Clear();

            //if (chkKsSql.Checked)
            //{
            //    alDataColumns.Add("KsSql");
            //}

            //if (chkKsSql.Checked)
            //{
            //    alDataColumns.Add("KsSql");
            //}

            //if (chkRealSql.Checked)
            //{
            //    alDataColumns.Add("RealSql");
            //}

            //if (chkRealSql2.Checked)
            //{
            //    alDataColumns.Add("RealSql2");
            //}

            //if (chkRealDbf.Checked)
            //{
            //    alDataColumns.Add("RealDbf");
            //}

            //pbData.Visible = true;
            //pbData.Value = 0;
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Value = 0;

            this.Enabled = false;
            dgvMain.DataSource = null;
            try
            {
                if (dtResult != null)
                    dtResult.Clear();
                //if (bsGrdMain.DataSource != null)
                 //   (bsGrdMain.DataSource as DataTable).Clear();
            }
            catch { }
            SetFilter();
            if (checkedCount() != 2)
                cDelta.Visible = false;
            else cDelta.Visible = true;
            tbDateAdd.Text = "";
            tbFio.Text = "";
            dgvMain_SelectionChanged(null, null);

            bgwGetCompare.RunWorkerAsync();
        }

        /// <summary>
        /// Определяет количество активных чек-боксов
        /// </summary>
        /// <returns></returns>
        private int checkedCount()
        {
            int cnt = 0;

            foreach (Control con in grpSources.Controls)
            {
                CheckBox cb = con as CheckBox;
                if (cb != null)
                {
                    if (((CheckBox)con).Checked)
                        cnt++;
                }
            }
            return cnt;
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void bgwGetCompare_DoWork(object sender, DoWorkEventArgs e)
        {
            if (rbDate.Checked) calDateData();
            else if (rbDateAndDep.Checked) calDateAndDepsData();
            else if (rbDateAndDepAndGood.Checked) calDateDepsAndGoods();
            else if (rbDateAndVVO.Checked) calDateAndVvoData();            
            /*
            return;


            DataTable dtRealDbf = new DataTable(); //Реализация из dbf
            DataTable dtGraphRealiz = null;
            //Реализация из SQL
            //  DataTable dtRealData = Parameters.hConnect.GetCompareData(Parameters.dateStart, Parameters.dateEnd, Parameters.groupType, Parameters.isShah, Parameters.isKsSql, Parameters.isRealSql, Parameters.isRealSql2);

            DataTable dtGoodsUpdates = Parameters.hConnectKass.GetGoodsData(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJRealiz = Parameters.hConnect.GetJRealizMain(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJRealizVVO = Parameters.hConnectVVO.GetJRealizVVO(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJournal = Parameters.hConnectKass.GetKassRealizJournal(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJournalVVO = Parameters.hConnectVVOKass.GetKassRealizJournalVVO(Parameters.dateStart, Parameters.dateEnd);
           
            if (chbGraphRealiz.Checked)
            {
                Task<DataTable> task = Parameters.hConnectKass.getRealizHours(Parameters.dateStart, Parameters.dateEnd, !rbDate.Checked);
                task.Wait();
                dtGraphRealiz = task.Result;
            }


            dtJRealiz.Merge(dtJRealizVVO);
            dtJournal.Merge(dtJournalVVO);

            //resultTable = (from g in dtGoodsUpdates.AsEnumerable()
            //               join jreal in dtJRealiz.AsEnumerable() on new { Q = g.Field<string>("ean"), W = g.Field<DateTime>("dreal") } equals new { Q = jreal.Field<string>("ean"), W = jreal.Field<DateTime>("dreal") }
            //               join jour in dtJournal.AsEnumerable() on new { Q = g.Field<string>("ean"), W = g.Field<DateTime>("dreal") } equals new { Q = jour.Field<string>("ean"), W = jour.Field<DateTime>("dreal") }

            //               select new
            //               {
            //                   date = g.Field<DateTime>("dreal"),
            //                   id_dep = g.Field<int>("id_dep"),
            //                   depName = jreal.Field<string>("depName"),
            //                   ean = g.Field<string>("ean"),
            //                   goodsName = g.Field<string>("name"),
            //                   KsSql = jour.Field<decimal>("KsSql"),
            //                   RealSql = jreal.Field<decimal>("RealSql"),
            //                   idTU = g.Field<Int16>("idTU")
            //               }).CopyToDataTable();

            resultTable = (from g in dtGoodsUpdates.AsEnumerable()
                           join jreal in dtJRealiz.AsEnumerable() on new { Q = g.Field<string>("ean"), W = g.Field<DateTime>("dreal") } equals new { Q = jreal.Field<string>("ean"), W = jreal.Field<DateTime>("dreal") } into t1
                           join jour in dtJournal.AsEnumerable() on new { Q = g.Field<string>("ean"), W = g.Field<DateTime>("dreal") } equals new { Q = jour.Field<string>("ean"), W = jour.Field<DateTime>("dreal") } into t2
                           //into tempJoin
                           from leftjoin1 in t1.DefaultIfEmpty()
                           from leftjoin2 in t2.DefaultIfEmpty()
                           select new
                           {
                               date = g.Field<DateTime>("dreal"),
                               id_dep = g.Field<int>("id_dep"),
                               depName = leftjoin1 == null ? "" : leftjoin1.Field<string>("depName"),
                               ean = g.Field<string>("ean"),
                               goodsName = g.Field<string>("name"),
                               KsSql = leftjoin2 == null ? 0 : leftjoin2.Field<decimal>("KsSql"),
                               RealSql = leftjoin1 == null ? 0 : leftjoin1.Field<decimal>("RealSql"),
                               idTU = g.Field<Int16>("idTU")
                           }).CopyToDataTable();            

            //EnumerableRowCollection<DataRow> rowCla = resultTable.AsEnumerable().Where(r => r.Field<string>("ean").Equals("4601653025820"));

            bgwGetCompare.ReportProgress(40);

            resultTable.Columns.Add("isRealEquals", typeof(bool));
            resultTable.Columns.Add("delta", typeof(decimal));
          
            foreach (DataRow dr in resultTable.Rows)
            {
                dr["delta"] = decimal.Parse(dr["KsSql"].ToString()) - decimal.Parse(dr["RealSql"].ToString());
            }

            foreach (DataRow dr in resultTable.Rows)
            {
                if (dr["KsSql"].ToString() == dr["RealSql"].ToString())
                    dr["isRealEquals"] = true;
                else dr["isRealEquals"] = false;
            }





            if (rbDate.Checked)
            {
                resultTable = (from table in resultTable.AsEnumerable()
                               group table by new
                               {
                                   date = table["date"],
                               }
                               into g

                               select new
                               {
                                   date = g.Key.date,
                                   KsSql = g.Sum(table => Decimal.Parse(table["KsSql"].ToString())),
                                   RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                                   delta = g.Sum(table => Decimal.Parse(table["delta"].ToString())),
                                   isRealEquals = ((g.Sum(table => (bool)table["isRealEquals"] ? 1 : 0)) % 2 == 0)
                               }).CopyToDataTable();
            }
            else if (rbDateAndDep.Checked)
            {
                resultTable = (from table in resultTable.AsEnumerable()
                               group table by new
                               {
                                   date = table["date"],
                                   id_dep = table["id_dep"],
                               }
                               into g

                               select new
                               {
                                   date = g.Key.date,
                                   id_dep = g.Key.id_dep,
                                   depName = resultTable.AsEnumerable().Where(r => r.Field<int>("id_dep") == int.Parse(g.Key.id_dep.ToString())).Select((depName) => depName).First()["depName"].ToString(),
                                   KsSql = g.Sum(table => Decimal.Parse(table["KsSql"].ToString())),
                                   RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                                   delta = g.Sum(table => Decimal.Parse(table["delta"].ToString())),
                                   isRealEquals = ((g.Sum(table => (bool)table["isRealEquals"] ? 1 : 0)) % 2 == 0)
                               }).CopyToDataTable();
            }
            else if (rbDateAndVVO.Checked && !chbMainKass.Checked)
            {
                DataTable dtTmpMain = (from table in resultTable.AsEnumerable().Where(r => r.Field<int>("id_dep") != 6)
                                       group table by new
                                       {
                                           date = table["date"],
                                       }
                       into g

                                       select new
                                       {
                                           date = g.Key.date,
                                           KsSql = g.Sum(table => Decimal.Parse(table["KsSql"].ToString())),
                                           RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                                           delta = g.Sum(table => Decimal.Parse(table["delta"].ToString())),
                                           isRealEquals = ((g.Sum(table => (bool)table["isRealEquals"] ? 1 : 0)) %2== 0),
                                           isVVO = false,
                                           depName = "Все отделы, кроме ВВО"
                                       }).CopyToDataTable();

                DataTable dtTmpVVo = (from table in resultTable.AsEnumerable().Where(r => r.Field<int>("id_dep") == 6)
                                      group table by new
                                      {
                                          date = table["date"],
                                      }
                       into g

                                      select new
                                      {
                                          date = g.Key.date,
                                          KsSql = g.Sum(table => Decimal.Parse(table["KsSql"].ToString())),
                                          RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                                          delta = g.Sum(table => Decimal.Parse(table["delta"].ToString())),
                                          isRealEquals = ((g.Sum(table => (bool)table["isRealEquals"] ? 1 : 0)) % 2 == 0),
                                          isVVO = true,
                                          depName = "Отдел ВВО"
                                      }).CopyToDataTable();


                resultTable = dtTmpMain.Clone();
                resultTable = dtTmpMain.Copy();
                resultTable.Merge(dtTmpVVo);
            }

            foreach (DataRow dr in resultTable.Rows)
            {
                dr["delta"] = decimal.Parse(dr["KsSql"].ToString()) - decimal.Parse(dr["RealSql"].ToString());
            }

            foreach (DataRow dr in resultTable.Rows)
            {
                if (dr["KsSql"].ToString() == dr["RealSql"].ToString())
                    dr["isRealEquals"] = true;
                else dr["isRealEquals"] = false;
            }

            if (chbGraphRealiz.Checked)
            {
                if (!resultTable.Columns.Contains("graphRealiz"))
                    resultTable.Columns.Add("graphRealiz", typeof(decimal));
                
                foreach (DataRow row in resultTable.Rows)
                {
                    EnumerableRowCollection<DataRow> rowCollect;
                    if (rbDate.Checked)
                    {
                        rowCollect = dtGraphRealiz.AsEnumerable().Where(r => r.Field<DateTime>("DateCount").Date == ((DateTime)row["date"]).Date);
                    }
                    else
                    {
                        rowCollect = dtGraphRealiz.AsEnumerable().Where(r => r.Field<DateTime>("DateCount").Date == ((DateTime)row["date"]).Date && r.Field<int>("id_Departments") ==(int)row["id_dep"]);

                    }
                    if (rowCollect.Count() > 0)
                            row["graphRealiz"] = rowCollect.First()["Realiz"];
                        else
                            row["graphRealiz"] = 0;                    
                }
            }


            if (chbMainKass.Checked)
            {
                Task<DataTable> task;
                #region "Формирование группировки данных для разбивки по дате и отделам"
                if (rbDateAndVVO.Checked)
                {
                    DataTable dtTmpMain = (from table in resultTable.AsEnumerable().Where(r => r.Field<int>("id_dep") != 6)
                                           group table by new
                                           {
                                               date = table["date"],
                                           }
                           into g

                                           select new
                                           {
                                               date = g.Key.date,
                                               KsSql = g.Sum(table => Decimal.Parse(table["KsSql"].ToString())),
                                               RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                                               delta = g.Sum(table => Decimal.Parse(table["delta"].ToString())),
                                               isRealEquals = ((g.Sum(table => (bool)table["isRealEquals"] ? 1 : 0)) > 0),
                                               isVVO = false,
                                               depName = "Все отделы, кроме ВВО"
                                           }).CopyToDataTable();

                    DataTable dtTmpVVo = (from table in resultTable.AsEnumerable().Where(r => r.Field<int>("id_dep") == 6)
                                          group table by new
                                          {
                                              date = table["date"],
                                          }
                           into g

                                          select new
                                          {
                                              date = g.Key.date,
                                              KsSql = g.Sum(table => Decimal.Parse(table["KsSql"].ToString())),
                                              RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                                              delta = g.Sum(table => Decimal.Parse(table["delta"].ToString())),
                                              isRealEquals = ((g.Sum(table => (bool)table["isRealEquals"] ? 1 : 0)) > 0),
                                              isVVO = true,
                                              depName = "Отдел ВВО"
                                          }).CopyToDataTable();


                    resultTable = dtTmpMain.Clone();
                    resultTable = dtTmpMain.Copy();
                    resultTable.Merge(dtTmpVVo);
                }
                #endregion

                #region "Получение данных по главной кассе"
                task = Parameters.hConnect.getMainKassForListDate(Parameters.dateStart, Parameters.dateEnd);
                task.Wait();
                DataTable dtMainKass = task.Result;
                #endregion

                #region "Формирование данных по главной кассе"
                if (dtMainKass != null && dtMainKass.Rows.Count > 0)
                {
                    #region "Формирование данных по дате"
                    if (rbDate.Checked)
                    {
                        DataTable dtTmp = new DataTable();

                        dtTmp = (from table in dtMainKass.AsEnumerable()
                                 group table by new
                                 {
                                     date = table["Data"]
                                 }
                                 into g

                                 select new
                                 {
                                     g.Key.date,
                                     MainKass = g.Sum(table => Decimal.Parse(table["MainKass"] == DBNull.Value ? "0" : table["MainKass"].ToString())),
                                     ChessBoard = g.Sum(table => Decimal.Parse(table["ChessBoard"] == DBNull.Value ? "0" : table["ChessBoard"].ToString())),
                                     RealSQL = g.Sum(table => Decimal.Parse(table["RealSQL"] == DBNull.Value ? "0" : table["RealSQL"].ToString()))
                                 }

                            ).CopyToDataTable();

                        resultTable.Columns.Add("MainKass", typeof(decimal));
                        resultTable.Columns.Add("ChessBoard", typeof(decimal));
                        resultTable.Columns.Add("RealSQL_vvo", typeof(decimal));

                        foreach (DataRow row in resultTable.Rows)
                        {
                            EnumerableRowCollection<DataRow> rowCollect = dtTmp.AsEnumerable().Where(r => r.Field<DateTime>("date").Date == ((DateTime)row["date"]).Date);
                            if (rowCollect.Count() > 0)
                            {
                                row["MainKass"] = rowCollect.First()["MainKass"];
                                row["ChessBoard"] = rowCollect.First()["ChessBoard"];
                                row["RealSQL_vvo"] = rowCollect.First()["RealSQL"];
                                rowCollect.First().Delete();
                                dtTmp.AcceptChanges();
                            }
                            else
                            {
                                row["MainKass"] = 0;
                                row["ChessBoard"] = DBNull.Value;
                                row["RealSQL_vvo"] = DBNull.Value;
                            }
                        }

                        foreach (DataRow row in dtTmp.Rows)
                        {
                            DataRow newRow = resultTable.NewRow();

                            newRow["date"] = row["date"];
                            newRow["KsSql"] = 0;
                            newRow["RealSql"] = 0;
                            newRow["delta"] = 0;
                            newRow["isRealEquals"] = false;

                            newRow["MainKass"] = row["MainKass"];
                            newRow["ChessBoard"] = row["ChessBoard"];
                            newRow["RealSQL_vvo"] = row["RealSQL"];

                            resultTable.Rows.Add(newRow);
                        }

                        foreach (DataRow row in resultTable.Rows)
                        {
                            if (chkKsSql.Checked && chkRealSql.Checked)
                            {
                                row["isRealEquals"] = ((decimal)row["KsSql"] == (decimal)row["MainKass"] && (decimal)row["RealSql"] == (decimal)row["MainKass"]);
                                row["delta"] = 0;
                            }
                            else if (chkKsSql.Checked)
                            {
                                row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"];
                                row["delta"] = (decimal)row["KsSql"] - (decimal)row["MainKass"];
                            }
                            else if (chkRealSql.Checked)
                            {
                                row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"];
                                row["delta"] = (decimal)row["RealSql"] - (decimal)row["MainKass"];
                            }
                        }
                    }
                    #endregion

                    #region "Формирование данных с разбивкой по отделу ВВО"
                    if (rbDateAndVVO.Checked)
                    {

                        resultTable.Columns.Add("MainKass", typeof(decimal));
                        resultTable.Columns.Add("ChessBoard", typeof(decimal));
                        resultTable.Columns.Add("RealSQL_vvo", typeof(decimal));
                        resultTable.Columns.Add("DateEdit", typeof(DateTime));
                        resultTable.Columns.Add("FIO", typeof(string));
                        resultTable.Columns.Add("id", typeof(int));



                        foreach (DataRow row in resultTable.Rows)
                        {
                            EnumerableRowCollection<DataRow> rowCollect = dtMainKass.AsEnumerable().Where(r => r.Field<DateTime>("Data").Date == ((DateTime)row["date"]).Date && r.Field<bool>("isVVO") == (bool)row["isVVO"]);
                            if (rowCollect.Count() > 0)
                            {
                                row["MainKass"] = rowCollect.First()["MainKass"];
                                row["ChessBoard"] = rowCollect.First()["ChessBoard"];
                                row["RealSQL_vvo"] = rowCollect.First()["RealSQL"];
                                row["DateEdit"] = rowCollect.First()["DateEdit"];
                                row["FIO"] = rowCollect.First()["FIO"];
                                row["id"] = rowCollect.First()["id"];

                                rowCollect.First().Delete();
                            }
                            else
                            {
                                row["MainKass"] = 0;
                                row["ChessBoard"] = DBNull.Value;
                                row["RealSQL_vvo"] = DBNull.Value;
                                row["DateEdit"] = DBNull.Value;
                                row["FIO"] = DBNull.Value;
                                row["id"] = DBNull.Value;
                            }
                        }

                        foreach (DataRow row in dtMainKass.Rows)
                        {
                            DataRow newRow = resultTable.NewRow();

                            newRow["date"] = row["Data"];
                            newRow["KsSql"] = 0;
                            newRow["RealSql"] = 0;
                            newRow["delta"] = 0;
                            newRow["isRealEquals"] = false;
                            newRow["isVVO"] = row["isVVO"];

                            newRow["MainKass"] = row["MainKass"];
                            newRow["ChessBoard"] = row["ChessBoard"];
                            newRow["RealSQL_vvo"] = row["RealSQL"];
                            newRow["DateEdit"] = row["DateEdit"];
                            newRow["FIO"] = row["FIO"];
                            newRow["id"] = row["id"];
                            newRow["depName"] = (bool)row["isVVO"] ? "Отдел ВВО" : "Все отделы, кроме ВВО";

                            resultTable.Rows.Add(newRow);
                        }

                        foreach (DataRow row in resultTable.Rows)
                        {
                            if (chkKsSql.Checked && chkRealSql.Checked)
                            {
                                row["isRealEquals"] = ((decimal)row["KsSql"] == (decimal)row["MainKass"] && (decimal)row["RealSql"] == (decimal)row["MainKass"]);
                                row["delta"] = 0;
                            }
                            else if (chkKsSql.Checked)
                            {
                                row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"];
                                row["delta"] = (decimal)row["KsSql"] - (decimal)row["MainKass"];
                            }
                            else if (chkRealSql.Checked)
                            {
                                row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"];
                                row["delta"] = (decimal)row["RealSql"] - (decimal)row["MainKass"];
                            }
                            //else if (chkKsSql.Checked)
                            //    row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"];
                            //else if (chkRealSql.Checked)
                            //    row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"];
                        }
                    }
                    #endregion
                }
                else
                {
                    if (rbDate.Checked)
                    {
                        resultTable.Columns.Add("MainKass", typeof(decimal));
                        resultTable.Columns.Add("ChessBoard", typeof(decimal));
                        resultTable.Columns.Add("RealSQL_vvo", typeof(decimal));


                        foreach (DataRow row in resultTable.Rows)
                        {
                            row["MainKass"] = 0;
                            row["ChessBoard"] = 0;
                            row["RealSQL_vvo"] = 0;

                            if (chkKsSql.Checked && chkRealSql.Checked)
                            {
                                row["isRealEquals"] = ((decimal)row["KsSql"] == (decimal)row["MainKass"] && (decimal)row["RealSql"] == (decimal)row["MainKass"]);
                                row["delta"] = 0;
                            }
                            else if (chkKsSql.Checked)
                            {
                                row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"];
                                row["delta"] = (decimal)row["KsSql"] - (decimal)row["MainKass"];
                            }
                            else if (chkRealSql.Checked)
                            {
                                row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"];
                                row["delta"] = (decimal)row["RealSql"] - (decimal)row["MainKass"];
                            }
                        }

                    }
                    else if (rbDateAndVVO.Checked)
                    {
                        resultTable.Columns.Add("MainKass", typeof(decimal));
                        resultTable.Columns.Add("ChessBoard", typeof(decimal));
                        resultTable.Columns.Add("RealSQL_vvo", typeof(decimal));
                        resultTable.Columns.Add("DateEdit", typeof(DateTime));
                        resultTable.Columns.Add("FIO", typeof(string));
                        resultTable.Columns.Add("id", typeof(int));


                        foreach (DataRow row in resultTable.Rows)
                        {
                            row["MainKass"] = 0;
                            row["ChessBoard"] = DBNull.Value;
                            row["RealSQL_vvo"] = DBNull.Value;
                            row["DateEdit"] = DBNull.Value;
                            row["FIO"] = DBNull.Value;
                            row["id"] = DBNull.Value;

                            if (chkKsSql.Checked && chkRealSql.Checked)
                            {
                                row["isRealEquals"] = ((decimal)row["KsSql"] == (decimal)row["MainKass"] && (decimal)row["RealSql"] == (decimal)row["MainKass"]);
                                row["delta"] = 0;
                            }
                            else if (chkKsSql.Checked)
                            {
                                row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"];
                                row["delta"] = (decimal)row["KsSql"] - (decimal)row["MainKass"];
                            }
                            else if (chkRealSql.Checked)
                            {
                                row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"];
                                row["delta"] = (decimal)row["RealSql"] - (decimal)row["MainKass"];
                            }
                        }

                    }
                }
                #endregion

                #region "Расчёт скидки"
                DataTable dtDiscount;
                task = Parameters.hConnectKass.getDiscount(Parameters.dateStart, Parameters.dateEnd,false);
                task.Wait();
                dtDiscount = task.Result;

                task = Parameters.hConnectVVOKass.getDiscount(Parameters.dateStart, Parameters.dateEnd, true);
                task.Wait();
                if (task.Result != null)
                    dtDiscount.Merge(task.Result);

                if (!resultTable.Columns.Contains("discount"))
                    resultTable.Columns.Add("discount", typeof(decimal));

                foreach (DataRow row in resultTable.Rows)
                {
                    EnumerableRowCollection<DataRow> rowCollect;
                    if (rbDate.Checked)
                        rowCollect = dtDiscount.AsEnumerable().Where(r => r.Field<DateTime>("conDate").Date == ((DateTime)row["date"]).Date);
                    else
                        rowCollect = dtDiscount.AsEnumerable().Where(r => r.Field<DateTime>("conDate").Date == ((DateTime)row["date"]).Date && r.Field<bool>("isVVO") == (bool)row["isVVO"]);
                    if (rowCollect.Count() > 0)
                    {
                        row["discount"] = rowCollect.Sum(r => r.Field<decimal>("cash_val"));
                    }
                    else
                    {
                        row["discount"] = 0;
                    }
                }


                #endregion
            }


            // bsGrdMain.DataSource = dtRealData;

            //bsGrdMain.DataSource = resultTable;


            bgwGetCompare.ReportProgress(100);
            */
        }

        #region "Новые расчёты по типам выборки"
        /// <summary>
        /// Расчёт данных группировка по датам
        /// </summary>
        private void calDateData()
        {         
            dtResult = null;
            DataTable dtJRealiz = null, dtJournal = null, dtMainKass = null, dtGraphRealiz = null, dtDiscount = null;

            Task<DataTable> task;

            if (chkRealSql.Checked)
            {
                task = Parameters.hConnect.getRealizForDate(Parameters.dateStart, Parameters.dateEnd, false, false);
                task.Wait();
                dtJRealiz = task.Result.Copy();
                task = Parameters.hConnectVVO.getRealizForDate(Parameters.dateStart, Parameters.dateEnd, true, false);
                task.Wait();
                if (task.Result != null) dtJRealiz.Merge(task.Result);

                dtJRealiz = (from table in dtJRealiz.AsEnumerable()
                             group table by new
                             {
                                 date = table["dreal"],
                             }
                               into g
                             select new
                             {
                                 g.Key.date,
                                 RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                                 KsSql = (decimal)0,
                                 graphRealiz = (decimal)0,
                                 MainKass = (decimal)0,
                                 discount = (decimal)0,
                             }).CopyToDataTable();
                if (dtResult == null) dtResult = dtJRealiz.Clone();
                dtResult.Merge(dtJRealiz);
            }

            if (chkKsSql.Checked)
            {
                task = Parameters.hConnectKass.getJournalForDate(Parameters.dateStart, Parameters.dateEnd, false, true);
                task.Wait();
                dtJournal = task.Result.Copy();
                task = Parameters.hConnectVVOKass.getJournalForDate(Parameters.dateStart, Parameters.dateEnd, true, false);
                task.Wait();
                if (task.Result != null) dtJournal.Merge(task.Result);


                dtJournal = (from table in dtJournal.AsEnumerable()
                             group table by new
                             {
                                 date = table["conDate"],
                             }
                               into g
                             select new
                             {
                                 g.Key.date,
                                 RealSql = (decimal)0,
                                 KsSql = g.Sum(table => Decimal.Parse(table["cash_val"].ToString())),
                                 graphRealiz = (decimal)0,
                                 MainKass = (decimal)0,
                                 discount = (decimal)0,
                             }).CopyToDataTable();

                if (dtResult == null) dtResult = dtJournal.Clone();
                dtResult.Merge(dtJournal);
            }

            if (chbGraphRealiz.Checked)
            {
                task = Parameters.hConnectKass.getRealizHours(Parameters.dateStart, Parameters.dateEnd, !rbDate.Checked);
                task.Wait();
                dtGraphRealiz = task.Result;

                dtGraphRealiz = (from table in dtGraphRealiz.AsEnumerable()
                                 group table by new
                                 {
                                     date = table["DateCount"],
                                 }
                             into g
                                 select new
                                 {
                                     g.Key.date,
                                     RealSql = (decimal)0,
                                     KsSql = (decimal)0,
                                     graphRealiz = g.Sum(table => Decimal.Parse(table["Realiz"].ToString())),
                                     MainKass = (decimal)0,
                                     discount = (decimal)0,
                                 }).CopyToDataTable();

                if (dtResult == null) dtResult = dtGraphRealiz.Clone();

                dtResult.Merge(dtGraphRealiz);
            }

            if (chbMainKass.Checked)
            {
                task = Parameters.hConnect.getMainKassForListDate(Parameters.dateStart, Parameters.dateEnd);
                task.Wait();
                dtMainKass = task.Result;

                dtMainKass = (from table in dtMainKass.AsEnumerable()
                              group table by new
                              {
                                  date = table["Data"],
                              }
                           into g
                              select new
                              {
                                  g.Key.date,
                                  RealSql = (decimal)0,
                                  KsSql = (decimal)0,
                                  graphRealiz = (decimal)0,
                                  MainKass = g.Sum(table => Decimal.Parse(table["MainKass"].ToString())),
                                  discount = (decimal)0,
                              }).CopyToDataTable();

                if (dtResult == null) dtResult = dtMainKass.Clone();
                dtResult.Merge(dtMainKass);

                task = Parameters.hConnectKass.getDiscount(Parameters.dateStart, Parameters.dateEnd, false);
                task.Wait();
                dtDiscount = task.Result;

                task = Parameters.hConnectVVOKass.getDiscount(Parameters.dateStart, Parameters.dateEnd, true);
                task.Wait();
                if (task.Result != null)
                    dtDiscount.Merge(task.Result);


                dtDiscount = (from table in dtDiscount.AsEnumerable()
                              group table by new
                              {
                                  date = table["conDate"],
                              }
                           into g
                              select new
                              {
                                  g.Key.date,
                                  RealSql = (decimal)0,
                                  KsSql = (decimal)0,
                                  graphRealiz = (decimal)0,
                                  MainKass = (decimal)0,
                                  discount = g.Sum(table => Decimal.Parse(table["cash_val"].ToString())),
                              }).CopyToDataTable();

                if (dtResult == null) dtResult = dtDiscount.Clone();
                dtResult.Merge(dtDiscount);
            }

            #region "группировка дат и расчёт сумм по колонкам"
            dtResult = (from table in dtResult.AsEnumerable()
                        group table by new
                        {
                            date = table["date"],
                        }
                           into g
                        select new
                        {
                            g.Key.date,
                            RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                            KsSql = g.Sum(table => Decimal.Parse(table["KsSql"].ToString())),
                            graphRealiz = g.Sum(table => Decimal.Parse(table["graphRealiz"].ToString())),
                            MainKass = g.Sum(table => Decimal.Parse(table["MainKass"].ToString())),
                            discount = g.Sum(table => Decimal.Parse(table["discount"].ToString())),
                        }).CopyToDataTable();

            #endregion

            #region "Расчёт дельты и расхождения"
            dtResult.Columns.Add("delta", typeof(decimal));
            dtResult.Columns.Add("isRealEquals", typeof(bool));

            foreach (DataRow row in dtResult.Rows)
            {

                if (chkKsSql.Checked && chkRealSql.Checked && chbMainKass.Checked && chbGraphRealiz.Checked)//Все флаги
                {
                    row["isRealEquals"] =
                        (decimal)row["KsSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
                        && (decimal)row["RealSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
                        && (decimal)row["graphRealiz"] == (decimal)row["MainKass"] + (decimal)row["discount"]

                        && (decimal)row["RealSql"] == (decimal)row["graphRealiz"]
                        && (decimal)row["KsSql"] == (decimal)row["graphRealiz"]

                        && (decimal)row["KsSql"] == (decimal)row["RealSql"];
                    row["delta"] = 0;
                }
                else
                if (chkKsSql.Checked && chkRealSql.Checked && (chbMainKass.Checked || chbGraphRealiz.Checked))//Шахматка и реал SQL с главной кассой или графиком реализации
                {
                    if (chbMainKass.Checked)
                    {
                        row["isRealEquals"] =
                            (decimal)row["KsSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
                            && (decimal)row["RealSql"] == (decimal)row["MainKass"] + (decimal)row["discount"];
                    }
                    else if (chbGraphRealiz.Checked)
                    {
                        row["isRealEquals"] =
                            (decimal)row["RealSql"] == (decimal)row["graphRealiz"]
                            && (decimal)row["KsSql"] == (decimal)row["graphRealiz"];
                    }

                    row["delta"] = 0;
                }
                else if (chkKsSql.Checked && (chkRealSql.Checked || chbMainKass.Checked || chbGraphRealiz.Checked))//Шахматка c реал SQL или главной кассой или графиком реализации
                {
                    if (chbMainKass.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"] + (decimal)row["discount"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["MainKass"] + (decimal)row["discount"];
                    }
                    else if (chbGraphRealiz.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["graphRealiz"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["graphRealiz"];
                    }
                    else if (chkRealSql.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["RealSql"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["RealSql"];
                    }
                }
                else if (chkRealSql.Checked && (chkKsSql.Checked || chbMainKass.Checked || chbGraphRealiz.Checked))
                {
                    if (chbMainKass.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"] + (decimal)row["discount"];
                        row["delta"] = (decimal)row["RealSql"] - (decimal)row["MainKass"] + (decimal)row["discount"];
                    }
                    else if (chbGraphRealiz.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["graphRealiz"];
                        row["delta"] = (decimal)row["RealSql"] - (decimal)row["graphRealiz"];
                    }
                    else if (chkRealSql.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["RealSql"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["RealSql"];
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Расчёт данных группировка по датам и отделу
        /// </summary>
        private void calDateAndDepsData()
        {            
            dtResult = null;
            DataTable dtJRealiz = null, dtJournal = null, dtMainKass = null, dtGraphRealiz = null, dtDiscount = null;

            Task<DataTable> task;

            if (chkRealSql.Checked)
            {
                task = Parameters.hConnect.getRealizForDate(Parameters.dateStart, Parameters.dateEnd, false, false);
                task.Wait();
                dtJRealiz = task.Result.Copy();
                task = Parameters.hConnectVVO.getRealizForDate(Parameters.dateStart, Parameters.dateEnd, true, false);
                task.Wait();
                if (task.Result != null) dtJRealiz.Merge(task.Result);

                dtJRealiz = (from table in dtJRealiz.AsEnumerable()
                             group table by new
                             {
                                 date = table["dreal"],
                                 id_dep = table["id_dep"]
                             }
                               into g
                             select new
                             {
                                 g.Key.date,
                                 id_dep = Convert.ToInt32(g.Key.id_dep.ToString()),
                                 RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                                 KsSql = (decimal)0,
                                 graphRealiz = (decimal)0,
                                 MainKass = (decimal)0,
                                 discount = (decimal)0,
                             }).CopyToDataTable();
                if (dtResult == null) dtResult = dtJRealiz.Clone();
                dtResult.Merge(dtJRealiz);
            }

            if (chkKsSql.Checked)
            {
                task = Parameters.hConnectKass.getJournalForDate(Parameters.dateStart, Parameters.dateEnd, false,true);
                task.Wait();
                dtJournal = task.Result.Copy();
                task = Parameters.hConnectVVOKass.getJournalForDate(Parameters.dateStart, Parameters.dateEnd, true, false);
                task.Wait();
                if (task.Result != null) dtJournal.Merge(task.Result);


                dtJournal = (from table in dtJournal.AsEnumerable()
                             group table by new
                             {
                                 date = table["conDate"],
                                 id_dep = table["dpt_no"]
                             }
                               into g
                             select new
                             {
                                 g.Key.date,
                                 id_dep = Convert.ToInt32(g.Key.id_dep.ToString()),
                                 RealSql = (decimal)0,
                                 KsSql = g.Sum(table => Decimal.Parse(table["cash_val"].ToString())),
                                 graphRealiz = (decimal)0,
                                 MainKass = (decimal)0,
                                 discount = (decimal)0,
                             }).CopyToDataTable();

                if (dtResult == null) dtResult = dtJournal.Clone();
                dtResult.Merge(dtJournal);
            }

            if (chbGraphRealiz.Checked)
            {
                task = Parameters.hConnectKass.getRealizHours(Parameters.dateStart, Parameters.dateEnd, !rbDate.Checked);
                task.Wait();
                dtGraphRealiz = task.Result;

                dtGraphRealiz = (from table in dtGraphRealiz.AsEnumerable()
                                 group table by new
                                 {
                                     date = table["DateCount"],
                                     id_dep = table["id_Departments"]
                                 }
                             into g
                                 select new
                                 {
                                     g.Key.date,
                                     id_dep = Convert.ToInt32(g.Key.id_dep.ToString()),
                                     RealSql = (decimal)0,
                                     KsSql = (decimal)0,
                                     graphRealiz = g.Sum(table => Decimal.Parse(table["Realiz"].ToString())),
                                     MainKass = (decimal)0,
                                     discount = (decimal)0,
                                 }).CopyToDataTable();

                if (dtResult == null) dtResult = dtGraphRealiz.Clone();

                dtResult.Merge(dtGraphRealiz);
            }

            /*if (chbMainKass.Checked)
            {
                task = Parameters.hConnect.getMainKassForListDate(Parameters.dateStart, Parameters.dateEnd);
                task.Wait();
                dtMainKass = task.Result;

                dtMainKass = (from table in dtMainKass.AsEnumerable()
                              group table by new
                              {
                                  date = table["Data"],
                              }
                           into g
                              select new
                              {
                                  g.Key.date,
                                  RealSql = (decimal)0,
                                  KsSql = (decimal)0,
                                  graphRealiz = (decimal)0,
                                  MainKass = g.Sum(table => Decimal.Parse(table["MainKass"].ToString())),
                                  discount = (decimal)0,
                              }).CopyToDataTable();

                if (dtResult == null) dtResult = dtMainKass.Clone();
                dtResult.Merge(dtMainKass);

                task = Parameters.hConnectKass.getDiscount(Parameters.dateStart, Parameters.dateEnd, false);
                task.Wait();
                dtDiscount = task.Result;

                task = Parameters.hConnectVVOKass.getDiscount(Parameters.dateStart, Parameters.dateEnd, true);
                task.Wait();
                if (task.Result != null)
                    dtDiscount.Merge(task.Result);


                dtDiscount = (from table in dtDiscount.AsEnumerable()
                              group table by new
                              {
                                  date = table["conDate"],
                              }
                           into g
                              select new
                              {
                                  g.Key.date,
                                  RealSql = (decimal)0,
                                  KsSql = (decimal)0,
                                  graphRealiz = (decimal)0,
                                  MainKass = (decimal)0,
                                  discount = g.Sum(table => Decimal.Parse(table["cash_val"].ToString())),
                              }).CopyToDataTable();

                if (dtResult == null) dtResult = dtDiscount.Clone();
                dtResult.Merge(dtDiscount);
            }*/

            #region "группировка дат и расчёт сумм по колонкам"
            dtResult = (from table in dtResult.AsEnumerable()
                        group table by new
                        {
                            date = table["date"],
                            id_dep = table["id_dep"]
                        }
                           into g
                        select new
                        {
                            g.Key.date,
                            g.Key.id_dep,
                            depName = Departments.AsEnumerable().Where(r=>r.Field<int>("id")==(int)g.Key.id_dep).First()["name"],
                            RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                            KsSql = g.Sum(table => Decimal.Parse(table["KsSql"].ToString())),
                            graphRealiz = g.Sum(table => Decimal.Parse(table["graphRealiz"].ToString())),
                            MainKass = g.Sum(table => Decimal.Parse(table["MainKass"].ToString())),
                            discount = g.Sum(table => Decimal.Parse(table["discount"].ToString())),
                        }).CopyToDataTable();

            #endregion

            #region "Расчёт дельты и расхождения"
            dtResult.Columns.Add("delta", typeof(decimal));
            dtResult.Columns.Add("isRealEquals", typeof(bool));

            foreach (DataRow row in dtResult.Rows)
            {

                if (chkKsSql.Checked && chkRealSql.Checked && chbMainKass.Checked && chbGraphRealiz.Checked)//Все флаги
                {
                    row["isRealEquals"] =
                        (decimal)row["KsSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
                        && (decimal)row["RealSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
                        && (decimal)row["graphRealiz"] == (decimal)row["MainKass"] + (decimal)row["discount"]

                        && (decimal)row["RealSql"] == (decimal)row["graphRealiz"]
                        && (decimal)row["KsSql"] == (decimal)row["graphRealiz"]

                        && (decimal)row["KsSql"] == (decimal)row["RealSql"];
                    row["delta"] = 0;
                }
                else
                if (chkKsSql.Checked && chkRealSql.Checked && (chbMainKass.Checked || chbGraphRealiz.Checked))//Шахматка и реал SQL с главной кассой или графиком реализации
                {
                    if (chbMainKass.Checked)
                    {
                        row["isRealEquals"] =
                            (decimal)row["KsSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
                            && (decimal)row["RealSql"] == (decimal)row["MainKass"] + (decimal)row["discount"];
                    }
                    else if (chbGraphRealiz.Checked)
                    {
                        row["isRealEquals"] =
                            (decimal)row["RealSql"] == (decimal)row["graphRealiz"]
                            && (decimal)row["KsSql"] == (decimal)row["graphRealiz"];
                    }

                    row["delta"] = 0;
                }
                else if (chkKsSql.Checked && (chkRealSql.Checked || chbMainKass.Checked || chbGraphRealiz.Checked))//Шахматка c реал SQL или главной кассой или графиком реализации
                {
                    if (chbMainKass.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"] + (decimal)row["discount"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["MainKass"] + (decimal)row["discount"];
                    }
                    else if (chbGraphRealiz.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["graphRealiz"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["graphRealiz"];
                    }
                    else if (chkRealSql.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["RealSql"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["RealSql"];
                    }
                }
                else if (chkRealSql.Checked && (chkKsSql.Checked || chbMainKass.Checked || chbGraphRealiz.Checked))
                {
                    if (chbMainKass.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"] + (decimal)row["discount"];
                        row["delta"] = (decimal)row["RealSql"] - (decimal)row["MainKass"] + (decimal)row["discount"];
                    }
                    else if (chbGraphRealiz.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["graphRealiz"];
                        row["delta"] = (decimal)row["RealSql"] - (decimal)row["graphRealiz"];
                    }
                    else if (chkRealSql.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["RealSql"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["RealSql"];
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Расчёт данных группировка по датам и на тип ВВО
        /// </summary>
        private void calDateAndVvoData()
        {
          
            dtResult = null;
            DataTable dtJRealiz = null, dtJournal = null, dtMainKass = null, dtGraphRealiz = null, dtDiscount = null, dtMainKassBuff = null;

            Task<DataTable> task;

            if (chkRealSql.Checked)
            {
                task = Parameters.hConnect.getRealizForDate(Parameters.dateStart, Parameters.dateEnd, false, false);
                task.Wait();
                dtJRealiz = task.Result.Copy();
                task = Parameters.hConnectVVO.getRealizForDate(Parameters.dateStart, Parameters.dateEnd, true, false);
                task.Wait();
                if (task.Result != null) dtJRealiz.Merge(task.Result);

                dtJRealiz = (from table in dtJRealiz.AsEnumerable()
                             group table by new
                             {
                                 date = table["dreal"],
                                 id_dep = table["id_dep"]
                             }
                               into g
                             select new
                             {
                                 g.Key.date,
                                 //id_dep = Convert.ToInt32(g.Key.id_dep.ToString()),
                                 isVVO = Convert.ToInt32(g.Key.id_dep.ToString()) == 6,
                                 RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                                 KsSql = (decimal)0,
                                 graphRealiz = (decimal)0,
                                 MainKass = (decimal)0,
                                 discount = (decimal)0,
                             }).CopyToDataTable();
                if (dtResult == null) dtResult = dtJRealiz.Clone();
                dtResult.Merge(dtJRealiz);
                dtJRealiz = null;
            }

            if (chkKsSql.Checked)
            {
                task = Parameters.hConnectKass.getJournalForDate(Parameters.dateStart, Parameters.dateEnd, false, true);
                task.Wait();
                dtJournal = task.Result.Copy();
                task = Parameters.hConnectVVOKass.getJournalForDate(Parameters.dateStart, Parameters.dateEnd, true, false);
                task.Wait();
                if (task.Result != null) dtJournal.Merge(task.Result);


                dtJournal = (from table in dtJournal.AsEnumerable()
                             group table by new
                             {
                                 date = table["conDate"],
                                 id_dep = table["dpt_no"]
                             }
                               into g
                             select new
                             {
                                 g.Key.date,
                                 //id_dep = Convert.ToInt32(g.Key.id_dep.ToString()),
                                 isVVO = Convert.ToInt32(g.Key.id_dep.ToString()) == 6,
                                 RealSql = (decimal)0,
                                 KsSql = g.Sum(table => Decimal.Parse(table["cash_val"].ToString())),
                                 graphRealiz = (decimal)0,
                                 MainKass = (decimal)0,
                                 discount = (decimal)0,
                             }).CopyToDataTable();

                if (dtResult == null) dtResult = dtJournal.Clone();
                dtResult.Merge(dtJournal);
                dtJournal = null;
            }

            if (chbGraphRealiz.Checked)
            {
                task = Parameters.hConnectKass.getRealizHours(Parameters.dateStart, Parameters.dateEnd, !rbDate.Checked);
                task.Wait();
                dtGraphRealiz = task.Result;

                dtGraphRealiz = (from table in dtGraphRealiz.AsEnumerable()
                                 group table by new
                                 {
                                     date = table["DateCount"],
                                     id_dep = table["id_Departments"]
                                 }
                             into g
                                 select new
                                 {
                                     g.Key.date,
                                     //id_dep = Convert.ToInt32(g.Key.id_dep.ToString()),
                                     isVVO = Convert.ToInt32(g.Key.id_dep.ToString()) == 6,
                                     RealSql = (decimal)0,
                                     KsSql = (decimal)0,
                                     graphRealiz = g.Sum(table => Decimal.Parse(table["Realiz"].ToString())),
                                     MainKass = (decimal)0,
                                     discount = (decimal)0,
                                 }).CopyToDataTable();

                if (dtResult == null) dtResult = dtGraphRealiz.Clone();

                dtResult.Merge(dtGraphRealiz);
                dtGraphRealiz = null;
            }

            if (chbMainKass.Checked)
            {
                task = Parameters.hConnect.getMainKassForListDate(Parameters.dateStart, Parameters.dateEnd);
                task.Wait();
                dtMainKass = task.Result;
                dtMainKassBuff = dtMainKass.Copy();

                dtMainKass = (from table in dtMainKass.AsEnumerable()
                              group table by new
                              {
                                  date = table["Data"],
                                  isVVO = table["isVVO"]
                              }
                           into g
                              select new
                              {
                                  g.Key.date,
                                  //id_dep = (bool)g.Key.isVVO ? 6 : 1,
                                  isVVO = (bool)g.Key.isVVO,
                                  RealSql = (decimal)0,
                                  KsSql = (decimal)0,
                                  graphRealiz = (decimal)0,
                                  MainKass = g.Sum(table => Decimal.Parse(table["MainKass"].ToString())),
                                  discount = (decimal)0,
                              }).CopyToDataTable();

                if (dtResult == null) dtResult = dtMainKass.Clone();
                dtResult.Merge(dtMainKass);
                dtMainKass = null;

                task = Parameters.hConnectKass.getDiscount(Parameters.dateStart, Parameters.dateEnd, false);
                task.Wait();
                dtDiscount = task.Result;

                task = Parameters.hConnectVVOKass.getDiscount(Parameters.dateStart, Parameters.dateEnd, true);
                task.Wait();
                if (task.Result != null)
                    dtDiscount.Merge(task.Result);


                dtDiscount = (from table in dtDiscount.AsEnumerable()
                              group table by new
                              {
                                  date = table["conDate"],
                                  isVVO = table["isVVO"]
                              }
                           into g
                              select new
                              {
                                  g.Key.date,
                                  //id_dep = (bool)g.Key.isVVO ? 6 : 1,
                                  isVVO = (bool)g.Key.isVVO,
                                  RealSql = (decimal)0,
                                  KsSql = (decimal)0,
                                  graphRealiz = (decimal)0,
                                  MainKass = (decimal)0,
                                  discount = g.Sum(table => Decimal.Parse(table["cash_val"].ToString())),
                              }).CopyToDataTable();

                if (dtResult == null) dtResult = dtDiscount.Clone();
                dtResult.Merge(dtDiscount);
                dtDiscount = null;
            }

            #region "группировка дат и расчёт сумм по колонкам"
            dtResult = (from table in dtResult.AsEnumerable()
                        group table by new
                        {
                            date = table["date"],
                            isVVO = table["isVVO"]
                        }
                           into g
                        select new
                        {
                            g.Key.date,
                            g.Key.isVVO,
                            depName = (bool)g.Key.isVVO? "Отдел ВВО" : "Все отделы, кроме ВВО",
                            RealSql = g.Sum(table => Decimal.Parse(table["RealSql"].ToString())),
                            KsSql = g.Sum(table => Decimal.Parse(table["KsSql"].ToString())),
                            graphRealiz = g.Sum(table => Decimal.Parse(table["graphRealiz"].ToString())),
                            MainKass = g.Sum(table => Decimal.Parse(table["MainKass"].ToString())),
                            discount = g.Sum(table => Decimal.Parse(table["discount"].ToString())),
                        }).CopyToDataTable();

            #endregion

            #region "Расчёт дельты и расхождения"
            dtResult.Columns.Add("delta", typeof(decimal));
            dtResult.Columns.Add("isRealEquals", typeof(bool));
            dtResult.Columns.Add("ChessBoard", typeof(decimal));
            dtResult.Columns.Add("RealSQL_vvo", typeof(decimal));
            dtResult.Columns.Add("DateEdit", typeof(DateTime));
            dtResult.Columns.Add("FIO", typeof(string));
            dtResult.Columns.Add("id", typeof(int));

            foreach (DataRow row in dtResult.Rows)
            {
                if (dtMainKassBuff != null)
                {
                    EnumerableRowCollection<DataRow> rowCollect = dtMainKassBuff.AsEnumerable()
                        .Where(r => r.Field<DateTime>("Data").Date == ((DateTime)row["date"]).Date && r.Field<bool>("isVVO") == (bool)row["isVVO"]);
                    if (rowCollect.Count() > 0)
                    {
                        row["ChessBoard"] = rowCollect.First()["ChessBoard"];
                        row["RealSQL_vvo"] = rowCollect.First()["RealSQL"];
                        row["DateEdit"] = rowCollect.First()["DateEdit"];
                        row["FIO"] = rowCollect.First()["FIO"];
                        row["id"] = rowCollect.First()["id"];
                    }
                    else
                    {
                        row["ChessBoard"] = DBNull.Value;
                        row["RealSQL_vvo"] = DBNull.Value;
                        row["DateEdit"] = DBNull.Value;
                        row["FIO"] = DBNull.Value;
                        row["id"] = DBNull.Value;
                    }
                }

                if (chkKsSql.Checked && chkRealSql.Checked && chbMainKass.Checked && chbGraphRealiz.Checked)//Все флаги
                {
                    row["isRealEquals"] =
                        (decimal)row["KsSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
                        && (decimal)row["RealSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
                        && (decimal)row["graphRealiz"] == (decimal)row["MainKass"] + (decimal)row["discount"]

                        && (decimal)row["RealSql"] == (decimal)row["graphRealiz"]
                        && (decimal)row["KsSql"] == (decimal)row["graphRealiz"]

                        && (decimal)row["KsSql"] == (decimal)row["RealSql"];
                    row["delta"] = 0;
                }
                else
                if (chkKsSql.Checked && chkRealSql.Checked && (chbMainKass.Checked || chbGraphRealiz.Checked))//Шахматка и реал SQL с главной кассой или графиком реализации
                {
                    if (chbMainKass.Checked)
                    {
                        row["isRealEquals"] =
                            (decimal)row["KsSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
                            && (decimal)row["RealSql"] == (decimal)row["MainKass"] + (decimal)row["discount"];
                    }
                    else if (chbGraphRealiz.Checked)
                    {
                        row["isRealEquals"] =
                            (decimal)row["RealSql"] == (decimal)row["graphRealiz"]
                            && (decimal)row["KsSql"] == (decimal)row["graphRealiz"];
                    }

                    row["delta"] = 0;
                }
                else if (chkKsSql.Checked && (chkRealSql.Checked || chbMainKass.Checked || chbGraphRealiz.Checked))//Шахматка c реал SQL или главной кассой или графиком реализации
                {
                    if (chbMainKass.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"] + (decimal)row["discount"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["MainKass"] + (decimal)row["discount"];
                    }
                    else if (chbGraphRealiz.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["graphRealiz"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["graphRealiz"];
                    }
                    else if (chkRealSql.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["RealSql"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["RealSql"];
                    }
                }
                else if (chkRealSql.Checked && (chkKsSql.Checked || chbMainKass.Checked || chbGraphRealiz.Checked))
                {
                    if (chbMainKass.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"] + (decimal)row["discount"];
                        row["delta"] = (decimal)row["RealSql"] - (decimal)row["MainKass"] + (decimal)row["discount"];
                    }
                    else if (chbGraphRealiz.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["graphRealiz"];
                        row["delta"] = (decimal)row["RealSql"] - (decimal)row["graphRealiz"];
                    }
                    else if (chkRealSql.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["RealSql"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["RealSql"];
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Расчёт данных группировка по датам, отделам и товарам
        /// </summary>
        private void calDateDepsAndGoods()
        {

            DataTable dtGoodsUpdates = Parameters.hConnectKass.GetGoodsData(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJRealiz = Parameters.hConnect.GetJRealizMain(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJRealizVVO = Parameters.hConnectVVO.GetJRealizVVO(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJournal = Parameters.hConnectKass.GetKassRealizJournal(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJournalVVO = Parameters.hConnectVVOKass.GetKassRealizJournalVVO(Parameters.dateStart, Parameters.dateEnd);

            dtJRealiz.Merge(dtJRealizVVO);
            dtJournal.Merge(dtJournalVVO);

            dtResult = (from g in dtGoodsUpdates.AsEnumerable()
                        join jreal in dtJRealiz.AsEnumerable() on new { Q = g.Field<string>("ean"), W = g.Field<DateTime>("dreal") } equals new { Q = jreal.Field<string>("ean"), W = jreal.Field<DateTime>("dreal") } into t1
                        join jour in dtJournal.AsEnumerable() on new { Q = g.Field<string>("ean"), W = g.Field<DateTime>("dreal") } equals new { Q = jour.Field<string>("ean"), W = jour.Field<DateTime>("dreal") } into t2
                        //into tempJoin
                        from leftjoin1 in t1.DefaultIfEmpty()
                        from leftjoin2 in t2.DefaultIfEmpty()
                        select new
                        {
                            date = g.Field<DateTime>("dreal"),
                            id_dep = g.Field<int>("id_dep"),
                            depName = leftjoin1 == null ? "" : leftjoin1.Field<string>("depName"),
                            ean = g.Field<string>("ean"),
                            goodsName = g.Field<string>("name"),
                            KsSql = leftjoin2 == null ? 0 : leftjoin2.Field<decimal>("KsSql"),
                            RealSql = leftjoin1 == null ? 0 : leftjoin1.Field<decimal>("RealSql"),
                            idTU = g.Field<Int16>("idTU")
                        }).Where(r => r.KsSql != 0 || r.RealSql != 0).CopyToDataTable();



            dtResult.Columns.Add("isRealEquals", typeof(bool));
            dtResult.Columns.Add("delta", typeof(decimal));

            foreach (DataRow dr in dtResult.Rows)
            {
                dr["delta"] = decimal.Parse(dr["KsSql"].ToString()) - decimal.Parse(dr["RealSql"].ToString());
            }

            foreach (DataRow dr in dtResult.Rows)
            {
                if (dr["KsSql"].ToString() == dr["RealSql"].ToString())
                    dr["isRealEquals"] = true;
                else dr["isRealEquals"] = false;
            }

        }

        #endregion

        /// <summary>
        /// Получение ТУ групп
        /// </summary>
        /// <param name="idDep">id отдела</param>
        private void GetTUGroups(int idDep)
        {
            DataTable groups;
            if (idDep == 0)
            {
                var comparer = new CustomComparer();
                DataTable tmpGroupsKom;
                DataTable tmpGroupsVVO;
                tmpGroupsKom = Parameters.hConnect.GetGroups(idDep);
                tmpGroupsVVO = Parameters.hConnectVVO.GetGroups(6);

                groups = DataTableExtensions.CopyToDataTable<DataRow>(tmpGroupsKom.AsEnumerable()
                      .Union(tmpGroupsVVO.AsEnumerable(), comparer));
            }
            else
            {
                if (idDep != 6)
                {
                    groups = Parameters.hConnect.GetGroups(idDep);
                }
                else
                {
                    groups = Parameters.hConnectVVO.GetGroups(idDep);
                }
            }
            cbTUGroups.DataSource = groups;
            cbTUGroups.ValueMember = "id";
            cbTUGroups.DisplayMember = "cname";
        }

        /// <summary>
        /// Сообщаем о ходе выполнения загрузки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwGetCompare_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbData.Value = e.ProgressPercentage;
        }

        private void bgwGetCompare_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            if (rbDateAndDep.Checked)
                dtResult.DefaultView.Sort = "date asc,id_dep asc";
            //bsGrdMain.Sort = "date, id_dep asc";
            else
            if (rbDate.Checked)
                //bsGrdMain.Sort = "date";
                dtResult.DefaultView.Sort = "date";
            else
                //bsGrdMain.Sort = "date";
                dtResult.DefaultView.Sort = "date";

            SetFilter();
            try
            {
                //dgvMain.DataSource = bsGrdMain;
                dgvMain.DataSource = dtResult;
                reSumTotal();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            btPrint.Enabled = (dgvMain != null && dgvMain.Rows.Count != 0);

            //dgvMain.DataSource = resultTable;

            this.Enabled = true;


            //pbData.Visible = false;
            toolStripProgressBar1.Visible = false;

        }

        #endregion 
                                 
        #region Фильтрация
        private void SetFilter()
        {

            if (dtResult == null)
            {
                btPrint.Enabled = false;
                return;
            }

            string filter = "";
            int val;

            if (int.TryParse(cbDeps.SelectedValue.ToString(), out val))
            {
                if (val != 0)
                {
                    filter += "id_dep = " + cbDeps.SelectedValue.ToString();
                }
            }

            if (int.TryParse(cbTUGroups.SelectedValue.ToString(), out val))
            {
                if (val != 0)
                {
                    filter += (filter.Length > 0 ? " AND " : "") + "idTU = " + cbTUGroups.SelectedValue.ToString();
                }
            }

            if (tbEAN.Text.Length > 0)
            {
                filter += (filter.Length > 0 ? " AND " : "") + "ean like '%" + tbEAN.Text + "%'";
            }

            if (tbName.Text.Length > 0)
            {
                filter += (filter.Length > 0 ? " AND " : "") + "goodsName like '%" + tbName.Text.ToUpper() + "%'";
            }

            if (chkShowOnlyRash.Checked)
            {
                filter += (filter.Length > 0 ? " AND " : "") + "isRealEquals = false";
            }

            try
            {
                //bsGrdMain.Filter = filter;

                dtResult.DefaultView.RowFilter = filter;
            }
            catch
            {
                btPrint.Enabled = false;
            }
            btPrint.Enabled = (dgvMain != null && dgvMain.Rows.Count != 0);

            reSumTotal();
        }

        private void reSumTotal()
        {
            //if ((bsGrdMain.DataSource as DataTable) != null && (bsGrdMain.DataSource as DataTable).Rows.Count > 0 && dgvMain.Rows.Count>0)
            if(dtResult!=null && dtResult.Rows.Count>0 && dgvMain.Rows.Count>0)
            {
                if (chbMainKass.Checked)
                {
                    decimal SumMainKass = dtResult.DefaultView.ToTable().AsEnumerable()
                         .Sum(r => r.Field<decimal?>("MainKass") ?? 0);
                    tbTotalcMainKass.Text = SumMainKass.ToString("0.00");

                    SumMainKass = dtResult.DefaultView.ToTable().AsEnumerable()
                       .Sum(r => r.Field<decimal?>("discount") ?? 0);
                    tbTotalcDiscount.Text = SumMainKass.ToString("0.00");
                }

                if (chkKsSql.Checked)
                {
                    decimal SumKsSql = dtResult.DefaultView.ToTable().AsEnumerable()
                            .Sum(r => r.Field<decimal?>("KsSql") ?? 0);
                    tbTotalKsSql.Text = SumKsSql.ToString("0.00");
                }

                if (chkRealSql.Checked)
                {
                    decimal SumRealSql = dtResult.DefaultView.ToTable().AsEnumerable()
                            .Sum(r => r.Field<decimal?>("RealSql") ?? 0);
                    tbTotalRealSql.Text = SumRealSql.ToString("0.00");
                }

                if (chbGraphRealiz.Checked)
                {
                    decimal SumGraphRealiz = dtResult.DefaultView.ToTable().AsEnumerable()
                         .Sum(r => r.Field<decimal?>("graphRealiz") ?? 0);
                    tbTotalcGraphRealiz.Text = SumGraphRealiz.ToString("0.00");
                }

                decimal sumDelta = dtResult.DefaultView.ToTable().AsEnumerable()
                            .Sum(r => r.Field<decimal?>("delta") ?? 0);
                tbTotalcDelta.Text = sumDelta.ToString("0.00");
            }
            else
            {
                tbTotalcMainKass.Text=
                tbTotalKsSql.Text =
                tbTotalRealSql.Text =
                tbTotalcDelta.Text =
                tbTotalcDiscount.Text =
                tbTotalcGraphRealiz.Text = "0.00";
            }
        }

        private void tbEAN_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void cbTUGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        private void chkShowOnlyRash_CheckedChanged(object sender, EventArgs e)
        {
            SetFilter();
        }

        #endregion

        #region Выгрузка в Excel\OpenCalc
        private void setWidthColumn(int indexRow, int indexCol, int width, Nwuram.Framework.ToExcelNew.ExcelUnLoad report)
        {
            report.SetColumnWidth(indexRow, indexCol, indexRow, indexCol, width);
        }

        private void newReport()
        {
            Logging.StartFirstLevel(79);
            Logging.Comment($"Период с {dtpStart.Value.ToShortDateString()} по {dtpEnd.Value.ToShortDateString()}");
            Logging.Comment($"Отдел ID: {cbDeps.SelectedValue}; Наименование: {cbDeps.Text}");
            Logging.Comment($"Т/У группы ID: {cbTUGroups.SelectedValue}; Наименование: {cbTUGroups.Text}");
            
            foreach (Control cnt in grpGroups.Controls)
            {
                if (cnt is RadioButton && (cnt as RadioButton).Checked)
                { Logging.Comment($"Группировка по \": {(cnt as RadioButton).Text}\""); break; }
            }
            
            foreach (Control cnt in grpGroups.Controls)
            {
                if (cnt is CheckBox && (cnt as CheckBox).Checked)
                { Logging.Comment($"Источники данных: \": {(cnt as CheckBox).Text}\""); }
            }

            Logging.Comment($"Магазин: {(ConnectionSettings.GetServer().Contains("K21") ? "K21" : "X14")}");


            if (tbTotalcMainKass.Visible)
                Logging.Comment($"Итого Главня касса: {tbTotalcMainKass.Text}");
            if (tbTotalKsSql.Visible)
                Logging.Comment($"Итого Шахматка: {tbTotalKsSql.Text}");
            if (tbTotalRealSql.Visible)
                Logging.Comment($"Итого Реал. SQL: {tbTotalRealSql.Text}");
            Logging.StopFirstLevel();


            Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();

            int indexRow = 1;

            int maxColumns = 0;

            foreach (DataGridViewColumn col in dgvMain.Columns)
                if (col.Visible)
                {
                    maxColumns++;
                    if (col.Name.Equals("DateReal")) setWidthColumn(indexRow, maxColumns, 12, report);
                    if (col.Name.Equals("Department")) setWidthColumn(indexRow, maxColumns, 8, report);
                    if (col.Name.Equals("EAN")) setWidthColumn(indexRow, maxColumns, 17, report);
                    if (col.Name.Equals("cName")) setWidthColumn(indexRow, maxColumns, 28, report);
                    if (col.Name.Equals("cMainKass")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("KsSql")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("RealSql")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cDelta")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cDiscount")) setWidthColumn(indexRow, maxColumns, 15, report);
                    if (col.Name.Equals("cGraphRealiz")) setWidthColumn(indexRow, maxColumns, 15, report);
                }

            #region "Head"
            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue("Сверка реализации", indexRow, 1);
            report.SetFontBold(indexRow, 1, indexRow, 1);
            report.SetFontSize(indexRow, 1, indexRow, 1, 16);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
            indexRow++;
            indexRow++;


            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Период с {dtpStart.Value.ToShortDateString() } по {dtpEnd.Value.ToShortDateString() }", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Отдел: {cbDeps.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"ТУ группа: {cbTUGroups.Text}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Группировка: " +
                $"{(rbDate.Checked ? rbDate.Text : (rbDateAndDep.Checked ? rbDateAndDep.Text : (rbDateAndDepAndGood.Checked ? rbDateAndDepAndGood.Text : (rbDateAndVVO.Checked ? rbDateAndVVO.Text : ""))))}", indexRow, 1);
            indexRow++;


            string srcs = "";
            foreach (Control con in grpSources.Controls.Cast<Control>().OrderBy(c => c.TabIndex))
            {
                CheckBox box = con as CheckBox;
                if (box != null)
                {
                    if (box.Checked)
                    {
                        srcs += (srcs.Length > 0 ? ", " : "") + box.Text;
                    }
                }
            }

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Источники данных: {srcs}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, maxColumns);
            report.AddSingleValue($"Магазин: {(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetServer().Contains("K21") ? "K21" : "X14")}", indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, 6);
            report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
            indexRow++;

            report.Merge(indexRow, 1, indexRow, 6);
            report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
            indexRow++;
            indexRow++;
            #endregion

            int indexCol = 0;
            foreach (DataGridViewColumn col in dgvMain.Columns)
                if (col.Visible)
                {
                    indexCol++;
                    report.AddSingleValue(col.HeaderText, indexRow, indexCol);
                }
            report.SetFontBold(indexRow, 1, indexRow, maxColumns);
            report.SetBorders(indexRow, 1, indexRow, maxColumns);
            report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
            report.SetWrapText(indexRow, 1, indexRow, maxColumns);
            indexRow++;


            foreach (DataGridViewRow row in dgvMain.Rows)
            {
                Color rColor = Color.White;
                if (row.Cells["isRealEquals"].Value is bool && !(bool)row.Cells["isRealEquals"].Value)
                    report.SetCellColor(indexRow, 1, indexRow, maxColumns, rColor);

                indexCol = 0;
                foreach (DataGridViewColumn col in dgvMain.Columns)
                {
                    if (col.Visible)
                    {
                        indexCol++;
                        if (row.Cells[col.Index].Value is DateTime)
                            report.AddSingleValue(((DateTime)row.Cells[col.Index].Value).ToShortDateString(), indexRow, indexCol);
                        if (row.Cells[col.Index].Value is decimal)
                        {
                            report.SetFormat(indexRow, indexCol, indexRow, indexCol, "0.00");
                            report.AddSingleValueObject(row.Cells[col.Index].Value, indexRow, indexCol);
                        }
                        else
                            report.AddSingleValue(row.Cells[col.Index].Value.ToString(), indexRow, indexCol);
                        report.SetWrapText(indexRow, indexCol, indexRow, indexCol);
                    }
                }

                report.SetBorders(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, maxColumns);
                report.SetCellAlignmentToJustify(indexRow, 1, indexRow, maxColumns);
                indexRow++;
            }

            report.Show();
        }
    
        private void btPrint_Click(object sender, EventArgs e)
        {
            newReport();
            //Print();
        }
        #endregion

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("!@#$%^&*\"№;:?+=_-*".Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btCheckExisting_Click(object sender, EventArgs e)
        {
            Form frmCheckEx = new CheckExisting();
            frmCheckEx.ShowDialog();
            if (((CheckExisting)frmCheckEx).isDataUpdated)
            {
                RefreshGrid();
            }
        }

        private void tbTotalcDelta_TextChanged(object sender, EventArgs e)
        {

        }

        private void chbMainKass_CheckedChanged(object sender, EventArgs e)
        {
            if (chbMainKass.Checked)
            {
                rbDateAndVVO.Enabled = true;
                rbDate.Enabled = true;
                rbDateAndDep.Enabled = false;
                rbDateAndDepAndGood.Enabled = false;
                if (rbDateAndDep.Checked || rbDateAndDepAndGood.Checked) rbDate.Checked = true;
                cMainKass.Visible = true;
                cDiscount.Visible = true;
                tbTotalcMainKass.Visible = true;
                tbTotalcDiscount.Visible = true;
                //label3.Visible = tbFio.Visible = label4.Visible = tbDateAdd.Visible = rbDateAndVVO.Checked;
                //btViewRepair.Visible = dgvRepaireRequest.Visible = btAdd.Visible = btEdit.Visible = btDel.Visible = rbDateAndVVO.Checked;
                groupBox1.Visible = rbDateAndVVO.Checked;
                dgvMain.DataSource = null;
            }
            else
            {
                //rbDateAndVVO.Enabled = false;
                rbDateAndDep.Enabled = true;
                rbDateAndDepAndGood.Enabled = true;
                cMainKass.Visible = false;
                cDiscount.Visible = false;
                tbTotalcMainKass.Visible = false;
                tbTotalcDiscount.Visible = false;
                //label3.Visible = tbFio.Visible = label4.Visible = tbDateAdd.Visible = false;
                //btViewRepair.Visible = dgvRepaireRequest.Visible = btAdd.Visible = btEdit.Visible = btDel.Visible = false;
                groupBox1.Visible = false;
                if (rbDateAndVVO.Checked) rbDate.Checked = true;
                reCountDelta();
                dgvMain.Invalidate();
            }

            dgvMain_SelectionChanged(null, null);
            visibleColumnDelta();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {

            DateTime? date = null;

            if (dgvMain.CurrentRow != null && dgvMain.CurrentRow.Index != -1)
                date = (DateTime)dtResult.DefaultView[dgvMain.CurrentRow.Index]["date"];

            frmAddRealizMainKass frmAddRK = new frmAddRealizMainKass();
            frmAddRK.date = date;
            if (DialogResult.OK == frmAddRK.ShowDialog())
            {
                addDataToTable(frmAddRK);
                /* EnumerableRowCollection<DataRow> rowCollect = (bsGrdMain.DataSource as DataTable)
                         .AsEnumerable().Where(r => r.Field<DateTime>("date").Date == frmAddRK.dateRealiz.Date && r.Field<bool>("isVVO") == frmAddRK.isVVO);

                 if (rowCollect.Count() > 0)
                 {
                     rowCollect.First()["MainKass"] = frmAddRK.Summa;
                     rowCollect.First()["ChessBoard"] = DBNull.Value;
                     rowCollect.First()["RealSQL_vvo"] = DBNull.Value;
                     rowCollect.First()["DateEdit"] = DBNull.Value;
                     rowCollect.First()["FIO"] = DBNull.Value;
                     rowCollect.First()["id"] = frmAddRK.id;
                     rowCollect.First()["depName"] = frmAddRK.isVVO ? "Отдел ВВО" : "Все отделы, кроме ВВО";
                 }
                 else
                 {
                     DataRow newRow = (bsGrdMain.DataSource as DataTable).NewRow();
                     newRow["date"] = frmAddRK.dateRealiz;
                     newRow["KsSql"] = 0;
                     newRow["RealSql"] = 0;
                     newRow["delta"] = 0;
                     newRow["isRealEquals"] = false;
                     newRow["isVVO"] = frmAddRK.isVVO;

                     newRow["MainKass"] = frmAddRK.Summa;
                     newRow["ChessBoard"] = DBNull.Value;
                     newRow["RealSQL_vvo"] = DBNull.Value;
                     newRow["DateEdit"] = DBNull.Value;
                     newRow["FIO"] = DBNull.Value;
                     newRow["id"] = frmAddRK.id;
                     newRow["depName"] = frmAddRK.isVVO ? "Отдел ВВО" : "Все отделы, кроме ВВО";
                     (bsGrdMain.DataSource as DataTable).Rows.Add(newRow);
                 }
                 dgvMain_SelectionChanged(null, null);*/
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null) return;
            if (dgvMain.CurrentRow.Index == -1) return;
            DataRowView row = dtResult.DefaultView[dgvMain.CurrentRow.Index];

            if (row["ChessBoard"] == DBNull.Value
                && row["RealSQL_vvo"] == DBNull.Value
                && row["id"] != DBNull.Value
                )
            {
                DateTime date = (DateTime)row["date"];
                bool isVVO = (bool)row["isVVO"];
                frmAddRealizMainKass frmAddRK = new frmAddRealizMainKass() { date = date, isVVO = isVVO, isEdit = true };//.ShowDialog();
                if (DialogResult.OK == frmAddRK.ShowDialog())
                {
                    addDataToTable(frmAddRK);
                }
            }
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null) return;
            if (dgvMain.CurrentRow.Index == -1) return;
            DataRowView row = dtResult.DefaultView[dgvMain.CurrentRow.Index];

            if (row["ChessBoard"] == DBNull.Value
                && row["RealSQL_vvo"] == DBNull.Value
                && row["id"] != DBNull.Value
                )
            {
                DateTime date = (DateTime)row["date"];
                bool isVVO = (bool)row["isVVO"];
                int id = (int)row["id"];
                decimal MainKass = (decimal)row["MainKass"];

                Task<DataTable> task = Parameters.hConnect.setMainKass(id, date, MainKass, isVVO, true, 0);
                task.Wait();

                if (task.Result == null)
                {
                    MessageBox.Show(Parameters.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int result = (int)task.Result.Rows[0]["id"];

                if (result == -1)
                {
                    MessageBox.Show(Parameters.centralText("Запись уже удалена другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //RefreshGrid();
                    delDataInTable(date, isVVO);
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Удалить выбранную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    //setLog(id, 1541);
                    task = Parameters.hConnect.setMainKass(id, date, MainKass, isVVO, true, 1);
                    task.Wait();
                    if (task.Result == null)
                    {
                        MessageBox.Show(Parameters.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Logging.StartFirstLevel(171);
                    Logging.Comment($"ID: {id}");
                    Logging.Comment($"Дата реализации: {date.ToShortDateString()}");
                    Logging.Comment($"Признак реализации: {(isVVO ? "отдел ВВО" : "все отделы, кроме ВВО")}");
                    Logging.Comment($"Реализация: {MainKass.ToString("0.00")}");
                    Logging.StopFirstLevel();

                    //RefreshGrid();
                    delDataInTable(date, isVVO);
                    return;
                }

            }
        }

        private void addDataToTable(frmAddRealizMainKass frmAddRK)
        {

            if (!(dtpStart.Value <= frmAddRK.dateRealiz.Date && frmAddRK.dateRealiz.Date <= dtpEnd.Value.Date)) return;
            if (dtResult == null) return;
            if (dtResult.Rows.Count == 0) return;
            
            EnumerableRowCollection<DataRow> rowCollect = dtResult
                            .AsEnumerable().Where(r => r.Field<DateTime>("date").Date == frmAddRK.dateRealiz.Date && r.Field<bool>("isVVO") == frmAddRK.isVVO);

            if (rowCollect.Count() > 0)
            {
                rowCollect.First()["MainKass"] = frmAddRK.Summa;
                rowCollect.First()["ChessBoard"] = DBNull.Value;
                rowCollect.First()["RealSQL_vvo"] = DBNull.Value;
                rowCollect.First()["DateEdit"] = DBNull.Value;
                rowCollect.First()["FIO"] = DBNull.Value;
                rowCollect.First()["id"] = frmAddRK.id;
                rowCollect.First()["depName"] = frmAddRK.isVVO ? "Отдел ВВО" : "Все отделы, кроме ВВО";

                if (chkKsSql.Checked && chkRealSql.Checked)
                {
                    rowCollect.First()["isRealEquals"] = ((decimal)rowCollect.First()["KsSql"] == (decimal)rowCollect.First()["MainKass"] && (decimal)rowCollect.First()["RealSql"] == (decimal)rowCollect.First()["MainKass"]);
                    rowCollect.First()["delta"] = 0;
                }
                else if (chkKsSql.Checked)
                {
                    rowCollect.First()["isRealEquals"] = (decimal)rowCollect.First()["KsSql"] == (decimal)rowCollect.First()["MainKass"];
                    rowCollect.First()["delta"] = (decimal)rowCollect.First()["KsSql"] - (decimal)rowCollect.First()["MainKass"];
                }
                else if (chkRealSql.Checked)
                {
                    rowCollect.First()["isRealEquals"] = (decimal)rowCollect.First()["RealSql"] == (decimal)rowCollect.First()["MainKass"];
                    rowCollect.First()["delta"] = (decimal)rowCollect.First()["RealSql"] - (decimal)rowCollect.First()["MainKass"];
                }

            }
            else
            {
                DataRow newRow = dtResult.NewRow();
                newRow["date"] = frmAddRK.dateRealiz;
                newRow["KsSql"] = 0;
                newRow["RealSql"] = 0;
                newRow["delta"] = 0;
                newRow["isRealEquals"] = false;
                newRow["isVVO"] = frmAddRK.isVVO;

                newRow["MainKass"] = frmAddRK.Summa;
                newRow["ChessBoard"] = DBNull.Value;
                newRow["RealSQL_vvo"] = DBNull.Value;
                newRow["DateEdit"] = DBNull.Value;
                newRow["FIO"] = DBNull.Value;
                newRow["id"] = frmAddRK.id;
                newRow["depName"] = frmAddRK.isVVO ? "Отдел ВВО" : "Все отделы, кроме ВВО";
                dtResult.Rows.Add(newRow);
            }
            //(bsGrdMain.DataSource as DataTable).AcceptChanges();
            dgvMain_SelectionChanged(null, null);
        }

        private void delDataInTable(DateTime date,bool isVVO)
        {
            EnumerableRowCollection<DataRow> rowCollect = dtResult
                               .AsEnumerable().Where(r => r.Field<DateTime>("date").Date == date.Date && r.Field<bool>("isVVO") == isVVO);

            if (rowCollect.Count() > 0)
            {
                if ((decimal)rowCollect.First()["KsSql"] == 0 && (decimal)rowCollect.First()["RealSql"] == 0)
                {
                    rowCollect.First().Delete();
                    return;
                }

                rowCollect.First()["MainKass"] = 0;
                rowCollect.First()["ChessBoard"] = DBNull.Value;
                rowCollect.First()["RealSQL_vvo"] = DBNull.Value;
                rowCollect.First()["DateEdit"] = DBNull.Value;
                rowCollect.First()["FIO"] = DBNull.Value;
                rowCollect.First()["id"] = DBNull.Value;
                rowCollect.First()["depName"] = isVVO ? "Отдел ВВО" : "Все отделы, кроме ВВО";

                if (chkKsSql.Checked && chkRealSql.Checked)
                {
                    rowCollect.First()["isRealEquals"] = ((decimal)rowCollect.First()["KsSql"] == (decimal)rowCollect.First()["MainKass"] && (decimal)rowCollect.First()["RealSql"] == (decimal)rowCollect.First()["MainKass"]);
                    rowCollect.First()["delta"] = 0;
                }
                else if (chkKsSql.Checked)
                {
                    rowCollect.First()["isRealEquals"] = (decimal)rowCollect.First()["KsSql"] == (decimal)rowCollect.First()["MainKass"];
                    rowCollect.First()["delta"] = (decimal)rowCollect.First()["KsSql"] - (decimal)rowCollect.First()["MainKass"];
                }
                else if (chkRealSql.Checked)
                {
                    rowCollect.First()["isRealEquals"] = (decimal)rowCollect.First()["RealSql"] == (decimal)rowCollect.First()["MainKass"];
                    rowCollect.First()["delta"] = (decimal)rowCollect.First()["RealSql"] - (decimal)rowCollect.First()["MainKass"];
                }

                //(bsGrdMain.DataSource as DataTable).AcceptChanges();
            }
            dgvMain_SelectionChanged(null, null);
        }

        private void addAndDropSverka(DateTime date, bool isVVO,bool isDel)
        {
            EnumerableRowCollection<DataRow> rowCollect = dtResult
                                   .AsEnumerable().Where(r => r.Field<DateTime>("date").Date == date.Date && r.Field<bool>("isVVO") == isVVO);

            if (rowCollect.Count() > 0)
            {

                if (isDel)
                {
                    rowCollect.First()["ChessBoard"] = DBNull.Value;
                    rowCollect.First()["RealSQL_vvo"] = DBNull.Value;
                    rowCollect.First()["DateEdit"] = DateTime.Now;
                    rowCollect.First()["FIO"] = UserSettings.User.FullUsername;
                }
                else
                {
                    rowCollect.First()["ChessBoard"] = rowCollect.First()["KsSql"];
                    rowCollect.First()["RealSQL_vvo"] = rowCollect.First()["RealSQL"];
                    rowCollect.First()["DateEdit"] = DateTime.Now;
                    rowCollect.First()["FIO"] = UserSettings.User.FullUsername;
                }




                //if (chkKsSql.Checked && chkRealSql.Checked)
                //{
                //    rowCollect.First()["isRealEquals"] = ((decimal)rowCollect.First()["KsSql"] == (decimal)rowCollect.First()["MainKass"] && (decimal)rowCollect.First()["RealSql"] == (decimal)rowCollect.First()["MainKass"]);
                //    rowCollect.First()["delta"] = 0;
                //}
                //else if (chkKsSql.Checked)
                //{
                //    rowCollect.First()["isRealEquals"] = (decimal)rowCollect.First()["KsSql"] == (decimal)rowCollect.First()["MainKass"];
                //    rowCollect.First()["delta"] = (decimal)rowCollect.First()["KsSql"] - (decimal)rowCollect.First()["MainKass"];
                //}
                //else if (chkRealSql.Checked)
                //{
                //    rowCollect.First()["isRealEquals"] = (decimal)rowCollect.First()["RealSql"] == (decimal)rowCollect.First()["MainKass"];
                //    rowCollect.First()["delta"] = (decimal)rowCollect.First()["RealSql"] - (decimal)rowCollect.First()["MainKass"];
                //}
                //(bsGrdMain.DataSource as DataTable).AcceptChanges();
            }

            //dgvMain.Update();
            //dgvMain.Refresh();
            dgvMain.Invalidate();
            dgvMain_SelectionChanged(null, null);
        }

        private void reCountDelta()
        {
            if (dtResult == null || dtResult.Rows.Count == 0 || dgvMain.Rows.Count == 0) return;

            foreach (DataRow row in dtResult.Rows)
            {
                if (chbMainKass.Checked)
                {
                    if (chkKsSql.Checked && chkRealSql.Checked)
                    {
                        row["isRealEquals"] = ((decimal)row["KsSql"] == (decimal)row["MainKass"] && (decimal)row["RealSql"] == (decimal)row["MainKass"]);
                        row["delta"] = 0;
                    }
                    else if (chkKsSql.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"];
                        row["delta"] = (decimal)row["KsSql"] - (decimal)row["MainKass"];
                    }
                    else if (chkRealSql.Checked)
                    {
                        row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"];
                        row["delta"] = (decimal)row["RealSql"] - (decimal)row["MainKass"];
                    }
                }
                else
                {
                    row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["RealSql"];
                    row["delta"] = (decimal)row["KsSql"] - (decimal)row["RealSql"];
                }
            }
        }
        
        private void btViewRepair_Click(object sender, EventArgs e)
        {
            if (dgvRepaireRequest.DataSource == null) return;
            if ((dgvRepaireRequest.DataSource as DataTable).Rows.Count == 0) return;

            int id_repair = (int)(dgvRepaireRequest.DataSource as DataTable).Rows[dgvRepaireRequest.CurrentRow.Index]["id_RequestRepair"];

            new RepairRequestMN.ViewRequestInWork(id_repair) { Text = "Просмотр заявки на ремонт" }.ShowDialog();
        }

        private void установитьСверкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Установить сверку?", "Сверка", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {

                DataRowView row = dtResult.DefaultView[dgvMain.CurrentRow.Index];

                int id = (int)row["id"];
                decimal KsSql = (decimal)row["KsSql"];
                decimal RealSql = (decimal)row["RealSql"];
                decimal Discount = (decimal)row["discount"];
                DateTime date = (DateTime)row["date"];
                bool isVVO = (bool)row["isVVO"];

                Task<DataTable> task = Parameters.hConnect.setValidateMainKass(id, RealSql, KsSql, Discount);
                task.Wait();

                if (task.Result == null)
                {
                    MessageBox.Show(Parameters.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int result = (int)task.Result.Rows[0]["id"];

                if (result == -1)
                {
                    MessageBox.Show(Parameters.centralText("Запись удалена другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    addAndDropSverka(date, isVVO, false);
                    return;
                }

                Logging.StartFirstLevel(1553);
                Logging.Comment($"ID: {id}");
                Logging.Comment($"Дата реализации: {date.ToShortDateString()}");
                Logging.Comment($"Признак реализации: {(isVVO ? "отдел ВВО" : "все отделы, кроме ВВО")}");
                Logging.Comment($"Сумма сверки Шахматка: {KsSql.ToString("0.00")}");
                Logging.Comment($"Сумма сверки Реал.SQL.: {RealSql.ToString("0.00")}");
                Logging.Comment($"Сумма скидки : {Discount.ToString("0.00")}");
                Logging.StopFirstLevel();

                addAndDropSverka(date, isVVO, false);

            }
        }

        private void снятьСверкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Снять сверку?", "Сверка", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                DataRowView row = dtResult.DefaultView[dgvMain.CurrentRow.Index];

                int id = (int)row["id"];
                decimal MainKass = (decimal)row["MainKass"];
                decimal Discount = (decimal)row["discount"];
                DateTime date = (DateTime)row["date"];
                bool isVVO = (bool)row["isVVO"];

                Task<DataTable> task = Parameters.hConnect.setValidateMainKass(id, null, null, null);
                task.Wait();

                if (task.Result == null)
                {
                    MessageBox.Show(Parameters.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int result = (int)task.Result.Rows[0]["id"];

                if (result == -1)
                {
                    MessageBox.Show(Parameters.centralText("Запись уже удалена другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshGrid();
                    //addAndDropSverka(date, isVVO, true);
                    return;
                }

                Logging.StartFirstLevel(1554);
                Logging.Comment($"ID: {id}");
                Logging.Comment($"Дата реализации: {date.ToShortDateString()}");
                Logging.Comment($"Признак реализации: {(isVVO ? "отдел ВВО" : "все отделы, кроме ВВО")}");
                Logging.Comment($"Сумма сверки : {MainKass.ToString("0.00")}");
                Logging.Comment($"Сумма скидки : {Discount.ToString("0.00")}");
                Logging.StopFirstLevel();

                RefreshGrid();
                //addAndDropSverka(date, isVVO, true);
            }
        }

        private void создатьЗаявкуНаРемонтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRowView row = dtResult.DefaultView[dgvMain.CurrentRow.Index];

            int id = (int)row["id"];
            bool isVVO = (bool)row["isVVO"];
            decimal MainKass = (decimal)row["MainKass"];
            DateTime date = (DateTime)row["date"];
            bool isRealEquals = (bool)row["isRealEquals"];

            string stringComment = "";
            string nameShop = Nwuram.Framework.Settings.Connection.ConnectionSettings.GetServer().Contains("K21") ? "K21" : "X14";
            int sourceDifference = 0;

            if (row["ChessBoard"] != DBNull.Value
                && row["RealSQL_vvo"] != DBNull.Value)
            {
                if ((decimal)row["RealSql"] != (decimal)row["RealSQL_vvo"]
                    || (decimal)row["KsSql"] != (decimal)row["ChessBoard"])
                {
                    stringComment += (stringComment.Trim().Length == 0 ? "" : Environment.NewLine) + "Расхождение в данных после СВЕРКИ!!!";
                }
            }



            if ((decimal)row["RealSql"] != (decimal)row["MainKass"] + (decimal)row["Discount"])
            {
                //Расхождение в данных после СВЕРКИ!!!
                if (isVVO)
                    stringComment += (stringComment.Trim().Length == 0 ? "" : Environment.NewLine) + $"Имеются расхождения в данных реализации по ВВО за {date.ToShortDateString()} по магазину {nameShop}";
                else
                    stringComment += (stringComment.Trim().Length == 0 ? "" : Environment.NewLine) + $"Имеются расхождения в данных реализации за {date.ToShortDateString()} по магазину {nameShop}";
                sourceDifference += 1;
            }


            if ((decimal)row["KsSql"] != (decimal)row["MainKass"] + (decimal)row["Discount"])
            {
                if (isVVO)
                    stringComment += (stringComment.Trim().Length == 0 ? "" : Environment.NewLine) + $"Имеются расхождения в данных шахматки по ВВО за {date.ToShortDateString()} по магазину {nameShop}";
                else
                    stringComment += (stringComment.Trim().Length == 0 ? "" : Environment.NewLine) + $"Имеются расхождения в данных шахматки за {date.ToShortDateString()} по магазину {nameShop}";
                sourceDifference += 2;
            }




            RepairRequestMN.CreateRequest frmCreateRepair = new RepairRequestMN.CreateRequest();

            if (ConnectionSettings.GetServer("5").Trim().Length > 0)
                frmCreateRepair.setNextConnect(5);

            frmCreateRepair.setComment(stringComment);
            if (DialogResult.OK == frmCreateRepair.ShowDialog())
            {
                int IdRepairRequest = frmCreateRepair.getIdRepairRequest();


                Task<DataTable> task = Parameters.hConnect.setRequestOfDifference(id, IdRepairRequest, isVVO, sourceDifference);
                task.Wait();

                if (task.Result == null)
                {
                    MessageBox.Show(Parameters.centralText("При сохранение данных возникли ошибки записи.\nОбратитесь в ОЭЭС\n"), "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logging.StopFirstLevel();
                    return;
                }

                int result = (int)task.Result.Rows[0]["id"];

                if (result == -1)
                {
                    MessageBox.Show(Parameters.centralText("Запись уже удалена другим пользователем\n"), "Удаление записи", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logging.StopFirstLevel();
                    RefreshGrid();
                    return;
                }


                //Logging.StartFirstLevel(1203);
                //Logging.Comment($"ID: {id}");
                Logging.Comment($"Дата реализации: {date.ToShortDateString()}");
                Logging.Comment($"Признак реализации: {(isVVO ? "отдел ВВО" : "все отделы, кроме ВВО")}");
                Logging.Comment($"Сумма сверки : {MainKass.ToString("0.00")}");
                Logging.StopFirstLevel();


                getListRepairRequestMainKass(id);
            }
        }

        private void rbDateAndVVO_CheckedChanged(object sender, EventArgs e)
        {
            EnableByGroups();
            visibleColumnDelta();
        }

      

        private void visibleColumnDelta()
        {
            //int count = 0;
            //foreach (Control cnt in grpSources.Controls)
            //{
            //    if (cnt is CheckBox) if (((CheckBox)cnt).Checked) count++;
            //}

            ////cDelta.Visible = tbTotalcDelta.Visible = tbTotalcDelta.Visible = chkRealSql.Checked && !chkKsSql.Checked;
            //cDelta.Visible = count == 2;
            //if (cDelta.Visible) cDelta.Width = 65;

            int count = checkedCount();

            if (count != 2)
            {
                cDelta.Visible = false;
                tbTotalcDelta.Visible = false;
            }
            else
            {

                cDelta.Visible = true;
                tbTotalcDelta.Visible = true;
                cDelta.Width = 65;
            }

            lbTotal.Visible = count != 0;
            btPrint.Enabled = (dgvMain != null && dgvMain.Rows.Count != 0);

            reSumTotal();
        }

        private void btReportMainKass_Click(object sender, EventArgs e)
        {
            new frmCreateReport().ShowDialog();
        }

        private void cmsMainGridContext_Opening(object sender, CancelEventArgs e)
        {
            установитьСверкуToolStripMenuItem.Enabled = снятьСверкуToolStripMenuItem.Enabled = создатьЗаявкуНаРемонтToolStripMenuItem.Enabled = false;

            DataRowView row = dtResult.DefaultView[dgvMain.CurrentRow.Index];

            установитьСверкуToolStripMenuItem.Enabled = (row["ChessBoard"] == DBNull.Value
               && row["RealSQL_vvo"] == DBNull.Value
               && row["id"] != DBNull.Value
               && (decimal)row["RealSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
               && (decimal)row["KsSql"] == (decimal)row["MainKass"] + (decimal)row["discount"]
               );

            снятьСверкуToolStripMenuItem.Enabled = (row["ChessBoard"] != DBNull.Value
               && row["RealSQL_vvo"] != DBNull.Value
            //   && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["id"] != DBNull.Value
               );

            создатьЗаявкуНаРемонтToolStripMenuItem.Enabled = (
                   //(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["ChessBoard"] == DBNull.Value
                   //&& (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["RealSQL_vvo"] == DBNull.Value
                   row["id"] != DBNull.Value
                && ((decimal)row["RealSql"] != (decimal)row["MainKass"] + (decimal)row["discount"]
                || (decimal)row["KsSql"] != (decimal)row["MainKass"] + (decimal)row["discount"])
              );
        }

        private void getListRepairRequestMainKass(int id_mainKass)
        {
            Task<DataTable> task;
            //if (Parameters.hConnectX14 != null)
            //    task = Parameters.hConnectX14.getListRepairRequestForMainKass(id_mainKass);
            //else
                task = Parameters.hConnect.getListRepairRequestForMainKass(id_mainKass);

            task.Wait();
            dgvRepaireRequest.DataSource = task.Result;
            btViewRepair.Enabled = dgvRepaireRequest.Rows.Count != 0;
            btDel.Enabled = dgvRepaireRequest.Rows.Count == 0;
        }

        private void chbGraphRealiz_CheckedChanged(object sender, EventArgs e)
        {
            cGraphRealiz.Visible = chbGraphRealiz.Checked;
            tbTotalcGraphRealiz.Visible = chbGraphRealiz.Checked;
            rbDateAndDepAndGood.Enabled = !chbGraphRealiz.Checked;
            visibleColumnDelta();
        }

        #region "Объединение"

        private void dgvMain_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;

            if (e.RowIndex == dgvMain.Rows.Count - 1)
            {
                e.AdvancedBorderStyle.Bottom = dgvMain.AdvancedCellBorderStyle.Bottom;
            }

            if (e.ColumnIndex != DateReal.Index)
            {
                e.AdvancedBorderStyle.Top = dgvMain.AdvancedCellBorderStyle.Top;
                return;
            }

            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = dgvMain.AdvancedCellBorderStyle.Top;
                //e.AdvancedBorderStyle.Bottom = dgvMain.AdvancedCellBorderStyle.Bottom;
            }

           
        }

        private void dgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;

            if (e.ColumnIndex != DateReal.Index) return;
            
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = dgvMain[column, row];
            DataGridViewCell cell2 = dgvMain[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }

            //int id = (int)(bsGrdMain.DataSource as DataTable).DefaultView[row]["id"];
            //int id_pre = (int)(bsGrdMain.DataSource as DataTable).DefaultView[row - 1]["id"];

            return cell1.Value.ToString() == cell2.Value.ToString() && column== DateReal.Index;// && id == id_pre;
        }
        #endregion      
    }

    class CustomComparer : IEqualityComparer<DataRow>
    {        
        public bool Equals(DataRow x, DataRow y)
        {
            return ((string)x["cname"]).Equals((string)y["cname"]);
        }

        public int GetHashCode(DataRow obj)
        {
            return ((string)obj["cname"]).GetHashCode();
        }
    }

}
