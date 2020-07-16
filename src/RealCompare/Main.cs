﻿using System;
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

        private BindingSource bsGrdMain = new BindingSource();
        private DataTable resultTable;
        ArrayList alDataColumns = new ArrayList();

        private void Main_Load(object sender, EventArgs e)
        {
            dgvMain.AutoGenerateColumns = false;
            Parameters.hConnect = new SqlWorker(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            Parameters.hConnectVVO = new SqlWorker(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            Parameters.hConnectKass = new SqlWorker(ConnectionSettings.GetServer("3"), ConnectionSettings.GetDatabase("3"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
            Parameters.hConnectVVOKass = new SqlWorker(ConnectionSettings.GetServer("4"), ConnectionSettings.GetDatabase("4"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);


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

        /// <summary>
        /// Получение списка отделов
        /// </summary>
        private void GetDepartments()
        {
            DataTable Departments = Parameters.hConnect.GetDepartments();
            cbDeps.DataSource = Departments;
            cbDeps.ValueMember = "id";
            cbDeps.DisplayMember = "name";
        }

        private void dgwMain_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            setTotalTextBoxPosition();
        }

        /// <summary>
        /// Задает положение и размер полей с итогами в соответствии с колонками грида
        /// </summary>
        /// <param name="ColumnName">Колонка грида</param>
        private void setTotalTextBoxPosition()
        {
            foreach (DataGridViewColumn Col in dgvMain.Columns)
            {
                if (Col.Index >= 4 && Col.Visible)
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
                for (int i = 3; i >= 0; i--)
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

        private void Main_Resize(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn col in dgvMain.Columns)
            {
                setTotalTextBoxPosition();
            }
        }

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
            tbDateAdd.Text = "";
            tbFio.Text = "";
        }

        private void rbDate_CheckedChanged(object sender, EventArgs e)
        {
            EnableByGroups();
        }

        private void rbDateAndDep_CheckedChanged(object sender, EventArgs e)
        {
            EnableByGroups();
        }

        private void rbDateAndDepAndGood_CheckedChanged(object sender, EventArgs e)
        {
            EnableByGroups();
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
                dgvMain.Invalidate();
            }

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
                dgvMain.Refresh();
            }
            //cDelta.Visible = tbTotalcDelta.Visible = chkRealSql.Checked && !chkKsSql.Checked;
            //if (cDelta.Visible) cDelta.Width = 65;
            visibleColumnDelta();
        }

        private void dgvMain_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgvMain != null && dgvMain.Rows.Count != 0)
            {
                btPrint.Enabled = true;
                CountTotal();
            }
            else
            {
                tbTotalKsSql.Text =
                tbTotalRealSql.Text =
                tbTotalcDelta.Text = "";
            }
        }

        private void dgvMain_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dgvMain.Rows.Count == 0)
            {
                btPrint.Enabled = false;
                tbTotalKsSql.Text =

                tbTotalRealSql.Text =
               tbTotalcDelta.Text = "";

            }
            else
            {
                CountTotal();
            }
        }

        #region Получение реализаций
        private void RefreshGrid()
        {
            if (checkedCount() < 2)
            {
                MessageBox.Show("В группе чек-боксов\n\"Источники данных\"\nдолжно быть отмечено\nминимум два источника.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

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
            alDataColumns.Clear();

            if (chkKsSql.Checked)
            {
                alDataColumns.Add("KsSql");
            }

            //if (chkKsSql.Checked)
            //{
            //    alDataColumns.Add("KsSql");
            //}

            if (chkRealSql.Checked)
            {
                alDataColumns.Add("RealSql");
            }

            //if (chkRealSql2.Checked)
            //{
            //    alDataColumns.Add("RealSql2");
            //}

            //if (chkRealDbf.Checked)
            //{
            //    alDataColumns.Add("RealDbf");
            //}

            pbData.Visible = true;
            pbData.Value = 0;
            this.Enabled = false;
            dgvMain.DataSource = null;
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

        private void tsRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void bgwGetCompare_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable dtRealDbf = new DataTable(); //Реализация из dbf

            //Реализация из SQL
            //  DataTable dtRealData = Parameters.hConnect.GetCompareData(Parameters.dateStart, Parameters.dateEnd, Parameters.groupType, Parameters.isShah, Parameters.isKsSql, Parameters.isRealSql, Parameters.isRealSql2);

            DataTable dtGoodsUpdates = Parameters.hConnectKass.GetGoodsData(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJRealiz = Parameters.hConnect.GetJRealizMain(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJRealizVVO = Parameters.hConnectVVO.GetJRealizVVO(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJournal = Parameters.hConnectKass.GetKassRealizJournal(Parameters.dateStart, Parameters.dateEnd);
            DataTable dtJournalVVO = Parameters.hConnectVVOKass.GetKassRealizJournalVVO(Parameters.dateStart, Parameters.dateEnd);

            dtJRealiz.Merge(dtJRealizVVO);
            dtJournal.Merge(dtJournalVVO);

            resultTable = (from g in dtGoodsUpdates.AsEnumerable()
                           join jreal in dtJRealiz.AsEnumerable() on new { Q = g.Field<string>("ean"), W = g.Field<DateTime>("dreal") } equals new { Q = jreal.Field<string>("ean"), W = jreal.Field<DateTime>("dreal") }
                           join jour in dtJournal.AsEnumerable() on new { Q = g.Field<string>("ean"), W = g.Field<DateTime>("dreal") } equals new { Q = jour.Field<string>("ean"), W = jour.Field<DateTime>("dreal") }

                           select new
                           {
                               date = g.Field<DateTime>("dreal"),
                               id_dep = g.Field<int>("id_dep"),
                               depName = jreal.Field<string>("depName"),
                               ean = g.Field<string>("ean"),
                               goodsName = g.Field<string>("name"),
                               KsSql = jour.Field<decimal>("KsSql"),
                               RealSql = jreal.Field<decimal>("RealSql"),
                               idTU = g.Field<Int16>("idTU")
                           }).CopyToDataTable();


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
                                   isRealEquals = ((g.Sum(table => (bool)table["isRealEquals"] ? 1 : 0)) > 0)
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
                                   isRealEquals = ((g.Sum(table => (bool)table["isRealEquals"] ? 1 : 0)) > 0)
                               }).CopyToDataTable();

            }


            if (chbMainKass.Checked)
            {
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
                Task<DataTable> task = Parameters.hConnect.getMainKassForListDate(Parameters.dateStart, Parameters.dateEnd);
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
                                row["isRealEquals"] = ((decimal)row["KsSql"] == (decimal)row["MainKass"] && (decimal)row["RealSql"] == (decimal)row["MainKass"]);
                            else if (chkKsSql.Checked)
                                row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"];
                            else if (chkRealSql.Checked)
                                row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"];
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
                                row["isRealEquals"] = ((decimal)row["KsSql"] == (decimal)row["MainKass"] && (decimal)row["RealSql"] == (decimal)row["MainKass"]);
                            else if (chkKsSql.Checked)
                                row["isRealEquals"] = (decimal)row["KsSql"] == (decimal)row["MainKass"];
                            else if (chkRealSql.Checked)
                                row["isRealEquals"] = (decimal)row["RealSql"] == (decimal)row["MainKass"];
                        }
                    }
                    #endregion
                }
                #endregion
            }


            // bsGrdMain.DataSource = dtRealData;

            bsGrdMain.DataSource = resultTable;


            bgwGetCompare.ReportProgress(100);
        }

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
                bsGrdMain.Sort = "date, id_dep asc";
            else
            if (rbDate.Checked)
                bsGrdMain.Sort = "date";
            else
                bsGrdMain.Sort = "date";

            try
            {
                dgvMain.DataSource = bsGrdMain;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            //dgvMain.DataSource = resultTable;

            this.Enabled = true;


            pbData.Visible = false;

        }

        #endregion 

        private void cbDeps_SelectedValueChanged(object sender, EventArgs e)
        {
            int curId;
            if (int.TryParse(cbDeps.SelectedValue.ToString(), out curId))
            {
                GetTUGroups(curId);
                SetFilter();
            }
        }

        /// <summary>
        /// Красим грид
        /// </summary>
        private void dgvMain_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            Color rColor = Color.White;


            rColor = (dgvMain.Rows[e.RowIndex].Cells["isRealEquals"].Value is bool && (bool)dgvMain.Rows[e.RowIndex].Cells["isRealEquals"].Value) ? Color.White : panel1.BackColor;

            dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvMain.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                dgvMain.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = rColor;

            if (chbMainKass.Checked && rbDateAndVVO.Checked)
            {
                if ((bsGrdMain.DataSource as DataTable).DefaultView[e.RowIndex]["ChessBoard"] != DBNull.Value && (bsGrdMain.DataSource as DataTable).DefaultView[e.RowIndex]["RealSQL"] != DBNull.Value)
                {
                    dgvMain.Rows[e.RowIndex].Cells["DateReal"].Style.BackColor =
                    dgvMain.Rows[e.RowIndex].Cells["DateReal"].Style.SelectionBackColor = panel3.BackColor;

                    if ((decimal)(bsGrdMain.DataSource as DataTable).DefaultView[e.RowIndex]["RealSql"] != (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[e.RowIndex]["RealSQL_vvo"]
                        || (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[e.RowIndex]["KsSql"] != (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[e.RowIndex]["ChessBoard"])
                    {
                        dgvMain.Rows[e.RowIndex].Cells["cMainKass"].Style.BackColor =
                            dgvMain.Rows[e.RowIndex].Cells["cMainKass"].Style.SelectionBackColor = panel2.BackColor;
                    }
                }
            }

        }

        ///// <summary>
        ///// Проверяет, совпадает ли реализация в данной строке грида
        ///// </summary>
        ///// <param name="dgvRow">Строка грида</param>
        ///// <returns></returns>
        //private bool isRealEquals(DataGridViewRow dgvRow)
        //{            
        //    for (int i = dgvMain.Columns.Count - 5; i < dgvMain.Columns.Count; i++)
        //    {
        //        for (int j = i; j < dgvMain.Columns.Count; j++)
        //        {
        //            if (dgvMain.Columns[i].Visible && dgvMain.Columns[j].Visible)
        //            {
        //                if (!dgvMain[i, dgvMain.Rows.IndexOf(dgvRow)].Value.Equals(dgvMain[j, dgvMain.Rows.IndexOf(dgvRow)].Value))
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //    }

        //    return true;
        //}     

        /// <summary>
        /// Подсчет итоговых сумм
        /// </summary>
        private void CountTotal()
        {
            //double sum;
            //for (int i = dgvMain.Columns.Count - 5; i < dgvMain.Columns.Count; i++)
            //{
            //    sum = 0.00;
            //    if (dgvMain.Columns[i].Visible)
            //    {
            //        foreach (DataGridViewRow dr in dgvMain.Rows)
            //        {
            //            double dbl;
            //            if(Double.TryParse(dr.Cells[i].Value.ToString(), out dbl))

            //            sum += dbl;
            //        }

            //        foreach (Control con in this.Controls)
            //        {
            //            TextBox tb = con as TextBox;
            //            if (tb != null)
            //            {
            //                if (con.Name == "tbTotal" + dgvMain.Columns[i].Name)
            //                {                                
            //                        tb.Text =  Math.Round(sum,2).ToString("0.00");
            //                }
            //            }
            //        }
            //    }
            //}
        }

        #region Фильтрация
        private void SetFilter()
        {
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

            bsGrdMain.Filter = filter;
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
        /// <summary>
        /// Выгрузка отчета
        /// </summary>
        private void Print()
        {
            Parameters.dates = Parameters.dateStart.ToShortDateString() + "-" + Parameters.dateEnd.ToShortDateString();
            Parameters.deps = cbDeps.Text;
            Parameters.grpBy = "Дата" + (Parameters.groupType == 2 ? ", отдел" : Parameters.groupType == 3 ? ", отдел, товар" : "");
            Parameters.tuGrps = cbTUGroups.Text;

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

            Parameters.srcs = srcs;

            Parameters.Data = GetDataTableFromDGV(dgvMain);

            //Добавляем строчку ИТОГО
            DataRow total = Parameters.Data.NewRow();
            total[0] = "Итого:";
            foreach (DataColumn dataCol in Parameters.Data.Columns)
            {
                string s = "";
                int index = 0;
                foreach (DataGridViewColumn grdCol in dgvMain.Columns)
                {
                    if (dataCol.ColumnName == grdCol.HeaderText)
                    {
                        s = grdCol.Name;
                        index = Parameters.Data.Columns.IndexOf(dataCol);
                        break;
                    }
                }

                if (index != 0)
                {
                    foreach (Control con in this.Controls)
                    {
                        if (con.Name.Contains(s))
                        {
                            TextBox tb = con as TextBox;
                            if (tb != null)
                            {
                                total[index] = tb.Text;
                            }
                        }
                    }
                }
            }
            Parameters.Data.Rows.Add(total);

            if (HandmadeReport.ExcelAvailable || HandmadeReport.OOAvailable)
            {
                HandmadeReport Rep = new HandmadeReport();
                Rep.AddSingleValue("Сверка реализации", 2, 4);
                Rep.SetFontBold(2, 4, 2, 4);
                Rep.AddSingleValue("Период: " + Parameters.dates, 4, 1);
                Rep.AddSingleValue("Отдел: " + Parameters.deps, 5, 1);
                Rep.AddSingleValue("ТУ группа: " + Parameters.tuGrps, 6, 1);
                Rep.AddSingleValue("Группировка: " + Parameters.grpBy, 7, 1);

                Rep.AddSingleValue("Источники данных: " + Parameters.srcs, 8, 1);


                Rep.AddMultiValue(Parameters.Data, 10, 1);
                foreach (DataColumn dc in Parameters.Data.Columns)
                {
                    int indx = Parameters.Data.Columns.IndexOf(dc) + 1;
                    switch (dc.ColumnName.Trim())
                    {
                        case "Дата реал.":
                            Rep.SetFormat(11, indx, dgvMain.Rows.Count + 10, indx, "ДД/ММ/ГГГГ");
                            Rep.SetColumnWidth(1, indx, 1, indx, 10);
                            break;
                        case "Отдел":
                            Rep.SetColumnWidth(1, indx, 1, indx, 15);
                            break;
                        case "EAN":
                            Rep.SetFormat(11, indx, dgvMain.Rows.Count + 10, indx, "#############");
                            Rep.SetColumnWidth(1, indx, 1, indx, 15);
                            break;
                        case "Наименование товара":
                            Rep.SetColumnWidth(1, indx, 1, indx, 30);
                            break;
                        default:
                            Rep.SetFormat(11, indx, dgvMain.Rows.Count + 10, indx, "0,00");
                            Rep.SetColumnWidth(1, indx, 1, indx, 15);
                            break;
                    }
                }

                foreach (DataGridViewRow dgvRow in dgvMain.Rows)
                {
                    if (dgvRow.Cells["isRealEquals"].Value is bool
                        && !(bool)dgvRow.Cells["isRealEquals"].Value)
                    {
                        Rep.SetCellColor(dgvRow.Index + 11, 1, dgvRow.Index + 11, Parameters.Data.Columns.Count, 22);
                    }
                }

                Rep.SetBorders(10, 1, Parameters.Data.Rows.Count + 9, Parameters.Data.Columns.Count);
                Rep.SaveToFile(Application.StartupPath + "\\Сверка реализации " + dtpStart.Value.ToString("ddMMyyyy") + "-" + dtpEnd.Value.ToString("ddMMyyyy") + " " + cbDeps.Text);
                Rep.Show();
            }
            else
            {
                Report crReport = new Report();
                //Отмечаем строки, которые нужно покрасить в Crystal report
                Parameters.Data.Columns.Add("isPrint");
                foreach (DataGridViewRow dRow in dgvMain.Rows)
                {
                    if (dRow.Cells["isRealEquals"].Value is bool
                        && (bool)dRow.Cells["isRealEquals"].Value)
                    {
                        Parameters.Data.Rows[dgvMain.Rows.IndexOf(dRow) + 1]["isPrint"] = "0";
                    }
                    else
                    {
                        Parameters.Data.Rows[dgvMain.Rows.IndexOf(dRow) + 1]["isPrint"] = "1";
                    }
                }

                crReport.ShowDialog();
            }
        }

        /// <summary>
        /// Получение DataTable из грида
        /// </summary>
        /// <param name="dgv">Грид</param>
        /// <returns></returns>
        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            // Колонки
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.Visible)
                {
                    DataColumn dc = new DataColumn(column.HeaderText.ToString());
                    dt.Columns.Add(dc);
                }
            }

            //Название колонок записываем в 1 строку
            DataRow headRow = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                headRow[dc.ColumnName] = dc.ColumnName;
            }
            dt.Rows.Add(headRow);

            // Строки
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                {
                    if (dt.Columns.Contains(dgv.Columns[dgvCell.ColumnIndex].HeaderText))
                    {
                        dr[dgv.Columns[dgvCell.ColumnIndex].HeaderText] = (dgvRow.Cells[dgv.Columns[dgvCell.ColumnIndex].Name].Value == null) ? "" : dgvRow.Cells[dgv.Columns[dgvCell.ColumnIndex].Name].Value.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            Print();
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
                tbTotalcMainKass.Visible = true;
                //label3.Visible = tbFio.Visible = label4.Visible = tbDateAdd.Visible = rbDateAndVVO.Checked;
                //btViewRepair.Visible = dgvRepaireRequest.Visible = btAdd.Visible = btEdit.Visible = btDel.Visible = rbDateAndVVO.Checked;
                groupBox1.Visible = rbDateAndVVO.Checked;
                dgvMain.DataSource = null;
            }
            else
            {
                rbDateAndVVO.Enabled = false;
                rbDateAndDep.Enabled = true;
                rbDateAndDepAndGood.Enabled = true;
                cMainKass.Visible = false;
                tbTotalcMainKass.Visible = false;
                //label3.Visible = tbFio.Visible = label4.Visible = tbDateAdd.Visible = false;
                //btViewRepair.Visible = dgvRepaireRequest.Visible = btAdd.Visible = btEdit.Visible = btDel.Visible = false;
                groupBox1.Visible = rbDateAndVVO.Checked;
                if (rbDateAndVVO.Checked) rbDate.Checked = true;
                dgvMain.Invalidate();
            }

            visibleColumnDelta();
        }

        private void dgvMain_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1 && chbMainKass.Checked && rbDateAndVVO.Checked)
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

        private void btAdd_Click(object sender, EventArgs e)
        {
            new frmAddRealizMainKass().ShowDialog();
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null) return;
            if (dgvMain.CurrentRow.Index == -1) return;
            if ((bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["ChessBoard"] == DBNull.Value
                && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["RealSQL_vvo"] == DBNull.Value
                && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["id"] != DBNull.Value
                )
            {
                DateTime date = (DateTime)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["date"];
                bool isVVO = (bool)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["isVVO"];

                new frmAddRealizMainKass() { date = date, isVVO = isVVO, isEdit = true }.ShowDialog();
            }
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null) return;
            if (dgvMain.CurrentRow.Index == -1) return;
            if ((bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["ChessBoard"] == DBNull.Value
                && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["RealSQL_vvo"] == DBNull.Value
                && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["id"] != DBNull.Value
                )
            {
                DateTime date = (DateTime)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["date"];
                bool isVVO = (bool)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["isVVO"];
                int id = (int)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["id"];
                decimal MainKass = (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["MainKass"];

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
                    RefreshGrid();
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
                    RefreshGrid();
                    return;
                }

            }
        }

        private void btViewRepair_Click(object sender, EventArgs e)
        {

        }

        private void установитьСверкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Установить сверку?", "Сверка", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                int id = (int)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["id"];
                decimal MainKass = (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["MainKass"];

                Task<DataTable> task = Parameters.hConnect.setValidateMainKass(id, MainKass, MainKass);
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
                    return;
                }

                RefreshGrid();

            }
        }

        private void снятьСверкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Снять сверку?", "Сверка", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                int id = (int)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["id"];
                //decimal MainKass = (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["MainKass"];

                Task<DataTable> task = Parameters.hConnect.setValidateMainKass(id, null, null);
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
                    return;
                }

                RefreshGrid();
            }
        }

        private void создатьЗаявкуНаРемонтToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rbDateAndVVO_CheckedChanged(object sender, EventArgs e)
        {
            EnableByGroups();
        }

        private void dgvMain_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                if ((bsGrdMain.DataSource as DataTable) == null || (bsGrdMain.DataSource as DataTable).Rows.Count == 0 || (bsGrdMain.DataSource as DataTable).DefaultView.Count == 0)
                {
                    tbDateAdd.Text = "";
                    tbFio.Text = "";
                    btDel.Enabled = btEdit.Enabled = false;
                    return;
                }

                if (rbDateAndVVO.Checked)
                {
                    tbFio.Text = (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["FIO"].ToString();
                    tbDateAdd.Text = (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["DateEdit"].ToString();
                    btDel.Enabled = btEdit.Enabled = ((bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["ChessBoard"] == DBNull.Value
                        && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["RealSQL_vvo"] == DBNull.Value
                        && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["id"] != DBNull.Value);
                    return;
                }
                else
                {
                    tbDateAdd.Text = "";
                    tbFio.Text = "";
                    btDel.Enabled = btEdit.Enabled = false;
                    return;
                }
            }
            catch
            {
                tbDateAdd.Text = "";
                tbFio.Text = "";
                btDel.Enabled = btEdit.Enabled = false;
            }
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


        }

        private void btReportMainKass_Click(object sender, EventArgs e)
        {
            new frmCreateReport().ShowDialog();
        }

        private void cmsMainGridContext_Opening(object sender, CancelEventArgs e)
        {
            установитьСверкуToolStripMenuItem.Enabled = снятьСверкуToolStripMenuItem.Enabled = создатьЗаявкуНаРемонтToolStripMenuItem.Enabled = false;


            установитьСверкуToolStripMenuItem.Enabled = ((bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["ChessBoard"] == DBNull.Value
               && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["RealSQL_vvo"] == DBNull.Value
               && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["id"] != DBNull.Value
               && (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["RealSql"] == (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["MainKass"]
               && (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["KsSql"] == (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["MainKass"]
               );

            снятьСверкуToolStripMenuItem.Enabled = ((bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["ChessBoard"] != DBNull.Value
               && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["RealSQL_vvo"] != DBNull.Value
            //   && (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["id"] != DBNull.Value
               );

            создатьЗаявкуНаРемонтToolStripMenuItem.Enabled = (
                 //(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["ChessBoard"] == DBNull.Value
              //&& (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["RealSQL_vvo"] == DBNull.Value
               (bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["id"] != DBNull.Value
              && (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["RealSql"] != (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["MainKass"]
              && (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["KsSql"] != (decimal)(bsGrdMain.DataSource as DataTable).DefaultView[dgvMain.CurrentRow.Index]["MainKass"]
              );


        }
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