using System;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Logging;
using System.Drawing;

namespace RepairRequestMN
{
    public partial class ViewRequestInWork : Form
    {
        private readonly Sql _sql = new Sql(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(),
            ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        private int _id;
        private DataTable _resultDt;
        private int _status;
        public ViewRequestInWork(int id)
        {
            InitializeComponent();

            DataTable dt = _sql.getSingleRepairRequest(id);
            _resultDt = dt;
        }

        private void ConfirmForm_Load(object sender, EventArgs e)
        {
            InitToolTips();

            Init();
            Show();
        }

        private void InitToolTips()
        {
            toolTip1.SetToolTip(button1, "Выход");
        }

        private void Init()
        {
            FillForm();
            //InitDatagrid();
            dataGridView1.AutoGenerateColumns = false;
            FillDatagrid();
        }

        private void InitDatagrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("date", "Дата");
            dataGridView1.Columns.Add("comment", "Комментарий");
            dataGridView1.Columns.Add("name", "ФИО");
        }

        private void FillDatagrid()
        {
            var dt = _sql.GetComments(_id);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = "DateComment ASC";
                dataGridView1.DataSource = dt;
            }
            /*foreach (DataRow row in dt.Rows)
            {
                var rowIndex = dataGridView1.Rows.Add();
                dataGridView1.Rows[rowIndex].Cells["date"].Value = row["DateComment"].ToString();
                dataGridView1.Rows[rowIndex].Cells["comment"].Value = row["Comment"].ToString().Trim();
                dataGridView1.Rows[rowIndex].Cells["name"].Value = row["FIO"].ToString().Trim();
            }*/
        }

        private void FillForm()
        {
            //_resultDt = _sql.GetDoneRequests(UserSettings.User.Id);
            if (_resultDt != null)
            {
                DataRow row = _resultDt.Rows[0];
                _id = int.Parse(row["id"].ToString());
                _status = int.Parse(row["Status"].ToString());
                textBox_Number.Text = row["Number"].ToString().Trim();
                var date = DateTime.Parse(row["DateSubmission"].ToString());
                textBox_Date.Text = date.ToShortDateString().Trim();
                textBox_Time.Text = date.ToLongTimeString().Trim();
                textBox_Hardware.Text = row["cName"].ToString().Trim();
                textBox_Cabinet.Text = row["Cabinet"].ToString().Trim();
                textBox_Name.Text = row["FIO"].ToString().Trim();
                richTextBox_Description.Text = row["Fault"].ToString().Trim();
                textBox_NameDone.Text = row["endName"].ToString().Trim();
                tbIp.Text = row["ip_address"].ToString();

                //tabControl1.SelectedTab = tabControl1.TabPages[1];


                Logging.StartFirstLevel(1556);
                Logging.Comment($"ID: {_id}");
                Logging.Comment("У заявки установлен  статус \"в работе’\". Предыдущий статус с \"выполнено\"");
                Logging.Comment("№ заявки:" + textBox_Number.Text);
                Logging.Comment("Дата подачи:" + textBox_Date.Text);
                Logging.Comment("Время подачи:" + textBox_Time.Text);
                Logging.Comment("Оборудование ID:" + _resultDt.Rows[0]["id_Hardware"].ToString() + ";Наименование: " + textBox_Hardware.Text);
                Logging.Comment("ФИО:" + textBox_Name.Text);
                Logging.Comment("ip адрес:" + tbIp.Text);
                Logging.Comment("Описание неисправности:" + richTextBox_Description.Text);
                //Logging.Comment("Комментарий:" + "");

                //Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                //+ " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewRequestInWork_FormClosing(object sender, FormClosingEventArgs e)
        {            
        }

        private void dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.CurrentCell != null)
            {
                Color RowColor = Color.White;

                dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor =
                  dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = RowColor;

                //dgwPriem.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.DimGray;
                dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
            }
        }

        private void dgw_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //Рисуем рамку для выделеной строки
            if (e.RowIndex != -1 && dgv.Rows[e.RowIndex].Selected)
            {
                int width = dgv.Width;
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