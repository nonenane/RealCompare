using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealCompare
{
    public partial class frmCreateReport : Form
    {
        public frmCreateReport()
        {
            InitializeComponent();
            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose,"Выход");
            tp.SetToolTip(btPrint, "Печать");

            if (Parameters.hConnect == null)
                Parameters.hConnect = new SqlWorker(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

            if (Parameters.hConnectVVO == null)
                Parameters.hConnectVVO = new SqlWorker(ConnectionSettings.GetServer("2"), ConnectionSettings.GetDatabase("2"), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        }

        private void frmCreateReport_Load(object sender, EventArgs e)
        {

        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value.Date > dtpEnd.Value.Date)
            {
                dtpEnd.Value = dtpStart.Value.Date;
            }
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStart.Value.Date > dtpEnd.Value.Date)
            {
                dtpStart.Value = dtpEnd.Value.Date;
            }
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            if (rbErrorRealiz.Checked)
            {
                Task<DataTable> task = Parameters.hConnect.GetReportMainKass(dtpStart.Value.Date, dtpEnd.Value.Date, 1);
                task.Wait();
                if (task.Result == null || task.Result.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для отчёта!", "Выгрузка отчёта", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Logging.StartFirstLevel(489);
                Logging.Comment($"Период с {dtpStart.Value.ToShortDateString()} по {dtpEnd.Value.ToShortDateString()}");
                Logging.Comment($"Отчет по ошибкам в реализации");               
                Logging.StopFirstLevel();


                DataTable dtReport = task.Result.Copy();

                Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();
                //Nwuram.Framework.ToExcel.HandmadeReport report = new Nwuram.Framework.ToExcel.HandmadeReport();                
                int indexRow = 1;

                report.Merge(indexRow, 1, indexRow, 6);
                report.AddSingleValue("Отчет по ошибкам в реализации", indexRow, 1);
                report.SetFontBold(indexRow, 1, indexRow, 1);
                report.SetFontSize(indexRow, 1, indexRow, 1, 16);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
                indexRow++;
                indexRow++;


                report.Merge(indexRow, 1, indexRow, 6);
                report.AddSingleValue($"Период с {dtpStart.Value.ToShortDateString()} по {dtpEnd.Value.ToShortDateString()}", indexRow, 1);
                indexRow++;

                report.Merge(indexRow, 1, indexRow, 6);
                report.AddSingleValue($"Магазин: {(ConnectionSettings.GetServer().Contains("K21") ? "K21" : "X14")}", indexRow, 1);
                indexRow++;

                report.Merge(indexRow, 1, indexRow, 6);
                report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
                indexRow++;

                report.Merge(indexRow, 1, indexRow, 6);
                report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
                indexRow++;
                indexRow++;


                report.SetColumnWidth(1, 1, 1, 1, 26);
                report.SetColumnWidth(1, 2, 1, 2, 20);
                report.SetColumnWidth(1, 3, 1, 3, 28);
                report.SetColumnWidth(1, 4, 1, 4, 21);
                report.SetColumnWidth(1, 5, 1, 5, 22);
                report.SetColumnWidth(1, 6, 1, 6, 22);

                report.AddSingleValue("Дата расхождения", indexRow, 1);
                report.AddSingleValue("Расхождение", indexRow, 2);
                report.AddSingleValue("Дата заявки на ремонт", indexRow, 3);
                report.AddSingleValue("Номер заявки", indexRow, 4);
                report.AddSingleValue("Комментарий к заявке", indexRow, 5);
                report.AddSingleValue("Дата подтверждения", indexRow, 6);

                report.SetFontBold(indexRow, 1, indexRow, 6);
                report.SetBorders(indexRow, 1, indexRow, 6);
                report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 6);
                indexRow++;

                foreach (DataRow row in dtReport.Rows)
                {
                    report.SetBorders(indexRow, 1, indexRow, 6);
                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 6);
                    report.SetCellAlignmentToJustify(indexRow, 1, indexRow, 6);
                    report.SetWrapText(indexRow, 1, indexRow, 6);
                    report.AddSingleValue($"{((DateTime)row["Data"]).ToShortDateString()}", indexRow, 1);
                    report.AddSingleValue($"{row["nameType"]}", indexRow, 2);
                    report.AddSingleValue($"{row["DateSubmission"]}", indexRow, 3);
                    report.AddSingleValue($"{row["Number"]}", indexRow, 4);
                    report.AddSingleValue($"{row["Comment"]}", indexRow, 5);
                    report.AddSingleValue($"{row["DateConfirm"]}", indexRow, 6);
                    indexRow++;
                }

                report.Show();
            }
            else if (rbNullDataValidate.Checked)
            {
                Task<DataTable> task = Parameters.hConnect.GetReportMainKass(dtpStart.Value.Date, dtpEnd.Value.Date, 2);
                task.Wait();
                if (task.Result == null || task.Result.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для отчёта!", "Выгрузка отчёта", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Logging.StartFirstLevel(489);
                Logging.Comment($"Период с {dtpStart.Value.ToShortDateString()} по {dtpEnd.Value.ToShortDateString()}");
                Logging.Comment($"Отчет об отсутствии сверки данных");
                Logging.StopFirstLevel();

                DataTable dtReport = task.Result.Copy();

                task = Parameters.hConnect.GetRealizForReportMainKass(dtpStart.Value.Date, dtpEnd.Value.Date);
                task.Wait();
                DataTable dtRealiz = new DataTable();
                if (task.Result != null && task.Result.Rows.Count != 0)
                    dtRealiz = task.Result.Copy();


                task = Parameters.hConnectVVO.GetRealizForReportMainKass(dtpStart.Value.Date, dtpEnd.Value.Date);
                task.Wait();
                DataTable dtRealizVVO = new DataTable();
                if (task.Result != null && task.Result.Rows.Count != 0)
                    dtRealizVVO = task.Result.Copy();

                Nwuram.Framework.ToExcelNew.ExcelUnLoad report = new Nwuram.Framework.ToExcelNew.ExcelUnLoad();                
                bool isFirstTab = true;
                for (int i = 3; i >= 1; i--)
                {

                    EnumerableRowCollection<DataRow> rowCollect = dtReport.AsEnumerable().Where(r => r.Field<int>("type") == i);
                    if (rowCollect.Count() == 0) continue;
                    if (!isFirstTab) report.GoToNextSheet();


                    int indexRow = 1;

                    report.Merge(indexRow, 1, indexRow, 3);
                    report.AddSingleValue("Отчет об отсутствии сверки данных", indexRow, 1);
                    report.SetFontBold(indexRow, 1, indexRow, 1);
                    report.SetFontSize(indexRow, 1, indexRow, 1, 16);
                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 1);
                    indexRow++;
                    indexRow++;


                    report.Merge(indexRow, 1, indexRow, 3);
                    report.AddSingleValue($"Период с {dtpStart.Value.ToShortDateString()} по {dtpEnd.Value.ToShortDateString()}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, 3);
                    report.AddSingleValue($"Магазин: {(ConnectionSettings.GetServer().Contains("K21") ? "K21" : "X14")}", indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, 3);
                    report.AddSingleValue("Выгрузил: " + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername, indexRow, 1);
                    indexRow++;

                    report.Merge(indexRow, 1, indexRow, 3);
                    report.AddSingleValue("Дата выгрузки: " + DateTime.Now.ToString(), indexRow, 1);
                    indexRow++;
                    indexRow++;


                    report.SetColumnWidth(1, 1, 1, 1, 26);
                    report.SetColumnWidth(1, 2, 1, 2, 20);
                    report.SetColumnWidth(1, 3, 1, 3, 28);

                    report.AddSingleValue("Причина", indexRow, 1);
                    report.AddSingleValue("Дата", indexRow, 2);
                    report.AddSingleValue("Отдел", indexRow, 3);

                    report.SetFontBold(indexRow, 1, indexRow, 3);
                    report.SetBorders(indexRow, 1, indexRow, 3);
                    report.SetCellAlignmentToCenter(indexRow, 1, indexRow, 3);
                    indexRow++;

                    #region "Нет данных «Главная касса»"
                    if (i == 1)
                    {
                        //EnumerableRowCollection<DataRow> rowCollect = dtReport.AsEnumerable().Where(r => r.Field<int>("type") == 1);
                        if (rowCollect.Count() > 0)
                        {
                            if (isFirstTab) { isFirstTab = false; report.changeNameTab("Нет данных «Главная касса»"); } else { report.changeNameTab("Нет данных «Главная касса»"); }

                            int headerStart = indexRow;

                            for (DateTime ii11 = dtpStart.Value; ii11 <= dtpEnd.Value; ii11 = ii11.AddDays(1))
                            {
                                int midStart = indexRow;
                                bool showData = false;


                                if (rowCollect.AsEnumerable().Where(r => !r.Field<bool>("isVVO") && r.Field<DateTime>("Data").Date == ii11.Date).Count() == 0)
                                {
                                    showData = true;
                                    report.AddSingleValue("Все отделы, кроме ВВО", indexRow, 3);
                                    indexRow++;
                                }

                                if (rowCollect.AsEnumerable().Where(r => r.Field<bool>("isVVO") && r.Field<DateTime>("Data").Date == ii11.Date).Count() == 0)
                                {
                                    showData = true;
                                    report.AddSingleValue("Отдел ВВО", indexRow, 3);
                                    indexRow++;
                                }

                                if (showData)
                                {
                                    report.Merge(midStart, 2, indexRow - 1, 2);
                                    report.AddSingleValue($"{ii11.ToShortDateString()}", midStart, 2);
                                    report.SetCellAlignmentToCenter(midStart, 2, indexRow - 1, 2);
                                    report.SetCellAlignmentToJustify(midStart, 2, indexRow - 1, 2);
                                }
                            }

                            report.Merge(headerStart, 1, indexRow - 1, 1);
                            report.SetBorders(headerStart, 1, indexRow - 1, 3);
                            report.SetCellAlignmentToCenter(headerStart, 1, indexRow - 1, 1);
                            report.SetCellAlignmentToJustify(headerStart, 1, indexRow - 1, 1);
                            report.SetWrapText(headerStart, 1, indexRow - 1, 1);
                            report.AddSingleValue($"Нет данных «Главная касса»", headerStart, 1);

                        }
                    }
                    #endregion

                    #region "Нет сверки с «Реал. SQL»"
                    if (i == 2)
                    {
                        //EnumerableRowCollection<DataRow> rowCollect = dtReport.AsEnumerable().Where(r => r.Field<int>("type") == 2);
                        if (rowCollect.Count() > 0)
                        {
                            if (isFirstTab) { isFirstTab = false; report.changeNameTab("Нет сверки с «Реал. SQL»"); } else { report.changeNameTab("Нет сверки с «Реал. SQL»"); }
                            int headerStart = indexRow;

                            var groupData = rowCollect.AsEnumerable()
                                .GroupBy(g => new { date = g.Field<DateTime>("Data") })
                                .Select(s => new { s.Key.date });
                            foreach (var gDate in groupData)
                            {
                                int midStart = indexRow;
                                bool showData = false;


                                foreach (DataRow row in rowCollect.AsEnumerable().Where(r => !r.Field<bool>("isVVO") && r.Field<DateTime>("Data").Date == gDate.date.Date))
                                {
                                    showData = true;
                                    report.AddSingleValue("Все отделы, кроме ВВО", indexRow, 3);
                                    indexRow++;
                                }

                                foreach (DataRow row in rowCollect.AsEnumerable().Where(r => r.Field<bool>("isVVO") && r.Field<DateTime>("Data").Date == gDate.date.Date))
                                {
                                    showData = true;
                                    report.AddSingleValue("Отдел ВВО", indexRow, 3);
                                    indexRow++;
                                }

                                if (showData)
                                {
                                    report.Merge(midStart, 2, indexRow - 1, 2);
                                    report.AddSingleValue($"{gDate.date.ToShortDateString()}", midStart, 2);
                                    report.SetCellAlignmentToCenter(midStart, 2, indexRow - 1, 2);
                                    report.SetCellAlignmentToJustify(midStart, 2, indexRow - 1, 2);
                                }
                            }

                            report.Merge(headerStart, 1, indexRow - 1, 1);
                            report.AddSingleValue($"Нет сверки с «Реал. SQL»", headerStart, 1);
                            report.SetBorders(headerStart, 1, indexRow - 1, 3);
                            report.SetCellAlignmentToCenter(headerStart, 1, indexRow - 1, 1);
                            report.SetCellAlignmentToJustify(headerStart, 1, indexRow - 1, 1);
                        }
                    }
                    #endregion

                    #region "Данные «Реал. SQL» отличаются от сохраненных"
                    if (i == 3)
                    {
                        //EnumerableRowCollection<DataRow> rowCollect = dtReport.AsEnumerable().Where(r => r.Field<int>("type") == 3);
                        if (rowCollect.Count() > 0)
                        {
                            if (isFirstTab) { isFirstTab = false; report.changeNameTab("Данные «Реал. SQL» отличаются от сохраненных"); } else { report.changeNameTab("Данные «Реал. SQL» отличаются от сохраненных"); }
                            int headerStart = indexRow;

                            var groupData = rowCollect.AsEnumerable()
                                .GroupBy(g => new { date = g.Field<DateTime>("Data") })
                                .Select(s => new { s.Key.date });
                            foreach (var gDate in groupData)
                            {
                                int midStart = indexRow;
                                bool showData = false;

                                foreach (DataRow row in rowCollect.AsEnumerable().Where(r => !r.Field<bool>("isVVO") && r.Field<DateTime>("Data").Date == gDate.date.Date))
                                {
                                    if (dtRealiz.Rows.Count > 0)
                                    {
                                        if (dtRealiz.AsEnumerable().Where(r => r.Field<DateTime>("dreal").Date == gDate.date.Date && r.Field<decimal>("RealSql") == (decimal)row["MainKass"]).Count() == 0)
                                        {
                                            showData = true;
                                            report.AddSingleValue("Все отделы, кроме ВВО", indexRow, 3);
                                            indexRow++;
                                        }
                                    }
                                    else
                                    {
                                        showData = true;
                                        report.AddSingleValue("Все отделы, кроме ВВО", indexRow, 3);
                                        indexRow++;
                                    }
                                }

                                foreach (DataRow row in rowCollect.AsEnumerable().Where(r => r.Field<bool>("isVVO") && r.Field<DateTime>("Data").Date == gDate.date.Date))
                                {
                                    if (dtRealizVVO.Rows.Count > 0)
                                    {
                                        if (dtRealizVVO.AsEnumerable().Where(r => r.Field<DateTime>("dreal").Date == gDate.date.Date && r.Field<decimal>("RealSql") == (decimal)row["MainKass"]).Count() == 0)
                                        {
                                            showData = true;
                                            report.AddSingleValue("Отдел ВВО", indexRow, 3);
                                            indexRow++;
                                        }
                                    }
                                    else
                                    {
                                        showData = true;
                                        report.AddSingleValue("Отдел ВВО", indexRow, 3);
                                        indexRow++;
                                    }
                                }

                                if (showData)
                                {
                                    report.Merge(midStart, 2, indexRow - 1, 2);
                                    report.AddSingleValue($"{gDate.date.ToShortDateString()}", midStart, 2);
                                    report.SetCellAlignmentToCenter(midStart, 2, indexRow - 1, 2);
                                    report.SetCellAlignmentToJustify(midStart, 2, indexRow - 1, 2);
                                }
                            }

                            report.Merge(headerStart, 1, indexRow - 1, 1);
                            report.SetBorders(headerStart, 1, indexRow - 1, 3);
                            report.SetCellAlignmentToCenter(headerStart, 1, indexRow - 1, 1);
                            report.SetCellAlignmentToJustify(headerStart, 1, indexRow - 1, 1);
                            report.SetWrapText(headerStart, 1, indexRow - 1, 1);
                            report.AddSingleValue($"Данные «Реал. SQL» отличаются от сохраненных", headerStart, 1);

                        }
                    }
                    #endregion
                }
             
                report.Show();

            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
