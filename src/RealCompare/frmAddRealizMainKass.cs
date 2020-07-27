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
    public partial class frmAddRealizMainKass : Form
    {
        private bool isEditData = false;
        //private int id = 0;

        public DateTime date { set; private get; }
        public bool isVVO { set; get; }
        public bool isEdit { set; private get; }

        public DateTime dateRealiz { private set; get; }
        public decimal Summa { private set; get; }
        public int id { private set; get; }

        private DateTime tmp_date;

        public frmAddRealizMainKass()
        {
            InitializeComponent();
            if(Parameters.hConnect==null)
                Parameters.hConnect = new SqlWorker(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);


            ToolTip tp = new ToolTip();
            tp.SetToolTip(btClose,"Выход");
            tp.SetToolTip(btSave, "Сохранить");
        }

        private void frmAddRealizMainKass_Load(object sender, EventArgs e)
        {                     
            dtpDate.MaxDate = Parameters.hConnect.GetDate().AddDays(-1);
            if (isEdit)
            {
                dtpDate.Value = date.Date;
                rbVVO.Checked = isVVO;
            }
            getData();

            tmp_date = dtpDate.Value.Date;

            isEditData = false;
        }

        private void frmAddRealizMainKass_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = isEditData && DialogResult.No == MessageBox.Show("На форме есть не сохранённые данные.\nЗакрыть форму без сохранения данных?\n", "Закрытие формы", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        private void tbRealiz_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.ToString().Contains(e.KeyChar) || (sender as TextBox).Text.ToString().Length == 0))
            {
                e.Handled = true;
            }
            else
                if ((!Char.IsNumber(e.KeyChar) && (e.KeyChar != ',')))
            {
                if (e.KeyChar != '\b')
                { e.Handled = true; }
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            decimal mainKass;
            if (tbRealiz.Text.Trim().Length == 0 || !decimal.TryParse(tbRealiz.Text, out mainKass))
            {
                MessageBox.Show(Parameters.centralText($"Необходимо заполнить\n \"{label2.Text}\"\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbRealiz.Focus();
                return;
            }

            if(mainKass == 0)
            {
                MessageBox.Show(Parameters.centralText($"\"{label2.Text}\" должна быть больше 0\n"), "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbRealiz.Focus();
                return;
            }


            Task<DataTable> task = Parameters.hConnect.setMainKass(id, dtpDate.Value.Date, mainKass, rbVVO.Checked, false, 0);
            task.Wait();

            DataTable dtResult = task.Result;

            if (dtResult == null || dtResult.Rows.Count == 0)
            {
                MessageBox.Show("Не удалось сохранить данные", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -1)
            {
                MessageBox.Show("Такие данные уже есть.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((int)dtResult.Rows[0]["id"] == -2)
            {
                MessageBox.Show(Parameters.centralText("Выбранная дата сверена.\nВыберите другую дату для реализации.\n"), "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if ((int)dtResult.Rows[0]["id"] == -9999)
            {
                MessageBox.Show("Произошла неведомая хрень.", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //if (id == 0)
            //{
            //    id = (int)dtResult.Rows[0]["id"];
            //    Logging.StartFirstLevel(1);
            //    Logging.Comment("Добавить Тип документа");
            //    Logging.Comment($"ID: {id}");
            //    //Logging.Comment($"Наименование: {tbNumber.Text.Trim()}");
            //    Logging.StopFirstLevel();
            //}
            //else
            //{
            //    Logging.StartFirstLevel(1);
            //    Logging.Comment("Редактировать Тип документа");
            //    Logging.Comment($"ID: {id}");
            //    //Logging.VariableChange("Наименование", tbNumber.Text.Trim(), oldName);
            //    Logging.StopFirstLevel();
            //}

            dateRealiz = dtpDate.Value.Date;
            Summa = mainKass;
            isVVO = rbVVO.Checked;
            id = (int)dtResult.Rows[0]["id"];

            isEditData = false;
            MessageBox.Show("Данные сохранены.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void rbAllWithOutVVO_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            Task<DataTable> task = Parameters.hConnect.getMainKass(dtpDate.Value.Date, rbVVO.Checked);
            task.Wait();

            if (task.Result != null && task.Result.Rows.Count > 0)
            {
                id = (int)task.Result.Rows[0]["id"];
                tbRealiz.Text = ((decimal)task.Result.Rows[0]["MainKass"]).ToString("0.00");
                rbVVO.Checked = (bool)task.Result.Rows[0]["isVVO"];
                this.Text = "Редактирование реализации";
            }
            else
            {
                id = 0;
                tbRealiz.Text = "0,00";
                this.Text = "Ввод реализации";
            }
            isEditData = true;
        }

        private void dtpDate_CloseUp(object sender, EventArgs e)
        {
            if (tmp_date != dtpDate.Value.Date)
                getData();
        }

        private void dtpDate_Leave(object sender, EventArgs e)
        {
            if (tmp_date != dtpDate.Value.Date)
                getData();
        }

        private void tbRealiz_TextChanged(object sender, EventArgs e)
        {
            isEditData = true;
        }
    }
}
