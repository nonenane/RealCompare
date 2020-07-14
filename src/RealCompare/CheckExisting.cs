using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RealCompare
{
    public partial class CheckExisting : Form
    {
        DataTable dtDbfCheck, dtKsRealiz;
        public bool isDataUpdated { get; set; }

        public CheckExisting()
        {
            InitializeComponent();
        }

        #region Methods

        private void SetData()
        {
            isDataUpdated = false;
            DateTime nowDate = Parameters.hConnect.GetDate().AddDays(-1);
            dtpDate.Value = nowDate;
            dtpDate.MaxDate = nowDate;
        }

        /// <summary>
        /// Загрузка данных из DBF
        /// </summary>
        /// <param name="dtDbfData">Таблица с данными dbf</param>
        /// <returns>Результат загрузки</returns>
        private bool GetCheckDbfData(ref DataTable dtDbfData)
        {
            bool isExcep = false;
            string pathToFiles = Parameters.hConnect.GetPathToDbf(1);
            if (pathToFiles != string.Empty && pathToFiles.Trim().Length > 0)
            {

                string fileName = dtpDate.Value.Year.ToString()
                                                 + dtpDate.Value.Month.ToString().PadLeft(2, '0')
                                                 + dtpDate.Value.Day.ToString().PadLeft(2, '0')
                                                 + ".dbf";

                pathToFiles = pathToFiles + "\\" + fileName;

                FileInfo fiDbf = new FileInfo(pathToFiles);
                if (!fiDbf.Exists)
                {
                    MessageBox.Show("Файл " + pathToFiles + "\n не найден.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                //Копируем файл в рабочую директорию
                try
                {
                    fiDbf.CopyTo(Application.StartupPath + "\\" + fileName, true);
                }
                catch (System.Security.SecurityException)
                {
                    isExcep = true;
                }
                catch (UnauthorizedAccessException)
                {
                    isExcep = true;
                }

                if (isExcep)
                {
                    MessageBox.Show("Не удалось скопировать файл " + fileName, "Предупреждение.", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }

                //Заполняем таблицу данными из dbf
                DBFWorker dbfWorker = new DBFWorker(Application.StartupPath + "\\" + fileName);
                dbfWorker.ExecuteCommand("PACK " + fileName);
                dtDbfData = dbfWorker.ExecuteCommand("SELECT tbl.*, " +
                                                        "(SELECT COUNT(*) FROM " + fileName + " WHERE Num_ecr = tbl.Num_ecr AND Num_ch = tbl.Num_ch AND F3 = tbl.F3 AND Num_op = tbl.Num_op AND F5 = tbl.F5 AND F4 = tbl.F4) AS cnt " +
                                                        //"IIF(INT(VAL(ALLTRIM(Num_op))) = 2 OR INT(VAL(ALLTRIM(Num_op))) = 21,0, F4) AS cval " +
                                                        "From " + fileName + " AS tbl WHERE INT(VAL(ALLTRIM(Num_op))) IN (1, 4, 32, 330, 132, 9, 21, 2, 0)");
                dtDbfData.Columns.Add("opType", typeof(int));
                int numOp;

                //обновляем коды операций
                foreach (DataRow drDbf in dtDbfData.Rows)
                {
                    if (int.TryParse(drDbf["Num_op"].ToString(), out numOp))
                    {
                        drDbf["opType"] = 0;
                        switch (numOp)
                        {
                            case 1:
                                drDbf["Num_op"] = 504;
                                break;
                            case 4:
                                drDbf["Num_op"] = 505;
                                break;
                            case 32:
                                drDbf["Num_op"] = 508;
                                break;
                            case 330:
                                drDbf["Num_op"] = 501;
                                break;
                            case 132:
                                drDbf["Num_op"] = 507;
                                break;
                            case 9:
                                drDbf["Num_op"] = 524;
                                break;
                            case 21:
                                drDbf["Num_op"] = 509;
                                drDbf["opType"] = 21;
                                break;
                            case 2:
                                drDbf["Num_op"] = 509;
                                drDbf["opType"] = 2;
                                break;
                            case 0:
                                drDbf["Num_op"] = 512;
                                break;
                        }
                    }
                }

                dtDbfData.AcceptChanges();

                //Добавляем анулированные чеки для записей valid = 0 и op_code != 0
                var nullCheck = (from dbf in dtDbfData.AsEnumerable()
                                where !dbf.Field<bool>("F7") && (int.Parse(dbf.Field<string>("Num_op")) != 512)
                                select dbf).Distinct();

                if (nullCheck.Count() != 0)
                {
                    DataTable dtNullCheck = DataTableExtensions.CopyToDataTable<DataRow>(nullCheck);

                    foreach (DataRow row in dtNullCheck.Rows)
                    {
                        row["cnt"] = 1;
                       row["Num_op"] = "512";
                    }
                                        
                    dtDbfData.Merge(dtNullCheck);
                }

                return true;
            }
            else
            {
                MessageBox.Show("Не указан путь\nк файлу с чеками.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            return false;
        }

        /// <summary>
        /// Получение данных с касс
        /// </summary>
        /// <param name="dtKsData">Таблица  данными касс</param>
        /// <returns>Результат загрузки</returns>
        private bool GetRealSqlData(ref DataTable dtKsData)
        {
            dtKsData = Parameters.hConnectKass.GetCheckData(dtpDate.Value.Date);
            return dtKsData != null;
        }

        /// <summary>
        /// Сравнение и загрузка расхождений по чекам
        /// </summary>
        /// <param name="dtDbf">Таблица из DBF</param>
        /// <param name="dtKs">Таблица с данными KassRealiz</param>
        private void CompareAndAcceptChanges(ref DataTable dtDbf, ref DataTable dtKs, DateTime dtpDateVal)
        {            
            int dpt;
            //Левый внешний джоин для данных из дбф (определяем расхождения)
            var changes = from dbf in dtDbf.AsEnumerable()
                            join Ks in dtKs.AsEnumerable()
                            on new
                            {
                                term = int.Parse(dbf.Field<decimal>("Num_ecr").ToString()),
                                doc = int.Parse(dbf.Field<decimal>("Num_ch").ToString()),
                                ean = dbf.Field<string>("F3").Trim(),
                                opCode = int.Parse(dbf.Field<string>("Num_op")),
                                count = Math.Abs(dbf.Field<decimal>("F5") * 1000),
                                cnt = int.Parse(dbf.Field<decimal>("cnt").ToString())
                            }
                            equals
                            new
                            {
                                term = int.Parse(Ks.Field<short>("terminal").ToString()),
                                doc = int.Parse(Ks.Field<short>("doc_id").ToString()),
                                ean = Ks.Field<string>("ean").Trim(),
                                opCode = int.Parse(Ks.Field<short>("op_code").ToString()),
                                count = Math.Abs(decimal.Parse(Ks.Field<int>("count").ToString())),
                                cnt = Ks.Field<int>("cnt")
                            }
                            into chgs
                            from ch in chgs.DefaultIfEmpty()
                            where ch == null
                            select new
                            {
                                terminal = int.Parse(dbf.Field<decimal>("Num_ecr").ToString()),
                                passwordLength = dbf.Field<string>("Password") == null ? 0 : dbf.Field<string>("Password").Trim().Length,
                                time_sec = int.Parse(dbf.Field<decimal>("Time").ToString()),
                                op_code = int.Parse(dbf.Field<string>("Num_op")),
                                doc_id = int.Parse(dbf.Field<decimal?>("Num_ch").ToString()),
                                ean = dbf.Field<string>("F3") == null ? "" : dbf.Field<string>("F3").Trim(),
                                count = dbf.Field<decimal>("F5") * 1000,
                                price = dbf.Field<decimal>("F6") * 100,
                                cash_val = dbf.Field<decimal>("F4") * 100,
                                dpt_no = int.TryParse(dbf.Field<string>("F8"), out dpt) ? dpt : 0,
                                opType = dbf.Field<int>("opType"),
                                cntDbf = int.Parse(dbf.Field<decimal>("cnt").ToString()),
                                cntKs = ch == null ? 0 : ch.Field<int>("cnt")
                            };

            //Добавляем различия на сервер.
            if (changes.Count() != 0)
            {
                if (MessageBox.Show("За " + dtpDateVal.Date.ToShortDateString() + " имеются расхождения.\nДобавить данные из DBF на КС SQL?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var change in changes)
                    {
                        Parameters.hConnectKass.InsertJournalData(dtpDateVal,
                                                                    change.terminal,
                                                                    change.passwordLength,
                                                                    change.time_sec,
                                                                    change.op_code,
                                                                    change.doc_id,
                                                                    change.ean,
                                                                    int.Parse(change.count.ToString().Remove(change.count.ToString().IndexOf(','))),
                                                                    int.Parse(change.price.ToString().Remove(change.price.ToString().IndexOf(','))),
                                                                    int.Parse(change.cash_val.ToString().Remove(change.cash_val.ToString().IndexOf(','))),
                                                                    change.dpt_no,
                                                                    change.opType,
                                                                    change.cntDbf);
                    }

                    MessageBox.Show("Данные DBF были успешно выгружены на КС SQL.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isDataUpdated = true;
                }
            }
            else
            {
                MessageBox.Show("За данный день различия отсутствуют.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Events

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckExisting_Load(object sender, EventArgs e)
        {
            SetData();
        }
               
        private void btCheck_Click(object sender, EventArgs e)
        {
            this.Controls.Cast<Control>().Where(x => !(x is PictureBox)).ToList().ForEach(x => x.Enabled = false);
            pbLoad.Visible = true;
            bgwCheck.RunWorkerAsync(new object[] { dtDbfCheck, dtKsRealiz, dtpDate.Value.Date });               
        } 
        
        
        private void bgwCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = e.Argument as object[];
            DataTable dtDbf = args[0] as DataTable;
            DataTable dtKs = args[1] as DataTable;
            DateTime dtpDateVal = (DateTime)args[2];

            if (GetCheckDbfData(ref dtDbf)
               && GetRealSqlData(ref dtKs))
            {
                CompareAndAcceptChanges(ref dtDbf, ref dtKs, dtpDateVal);
            }
        }

        private void bgwCheck_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbLoad.Visible = false;
            this.Controls.Cast<Control>().Where(x => !(x is PictureBox)).ToList().ForEach(x => x.Enabled = true);
        }
        
        #endregion

        private void bgwCheck_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
    }
}
