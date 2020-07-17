using System;
using System.Windows.Forms;
using Nwuram.Framework.Settings.Connection;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Logging;
using System.Text.RegularExpressions;
using System.Data;

namespace RepairRequestMN
{
    public partial class CreateRequest : Form
    {
        private Sql _sql = new Sql(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(),
            ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

        int id_Req;

        public void setComment(string text)
        {
            richTextBox_Description.Text = text;
        }

        public void setNextConnect(int indexConnect)
        {
            _sql = new Sql(ConnectionSettings.GetServer(indexConnect.ToString()), ConnectionSettings.GetDatabase(indexConnect.ToString()),
                ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
        }

        public int getIdRepairRequest()
        {
            return id_Req;
        }

        private bool correctIP = false;
        public CreateRequest()
        {
            InitializeComponent();
        }

        private void CreateRequest_Load(object sender, EventArgs e)
        {
            InitToolTips();
            DataTable dtDate = _sql.GetDate();
            if (dtDate != null)
            {
                dateTimePicker_Date.MinDate = (DateTime)dtDate.Rows[0][0];
                dateTimePicker_Time.MinDate = (DateTime)dtDate.Rows[0][0];
            }
            else
            {
                dateTimePicker_Date.MinDate = DateTime.Now;
                dateTimePicker_Time.MinDate = DateTime.Now;
            }

            var hwDt = _sql.GetActiveHardware();
            comboBox_Hardware.DataSource = hwDt;
            comboBox_Hardware.DisplayMember = "cName";
            comboBox_Hardware.ValueMember = "id";

            mtbIp.Text = "192.168.";            
        }

        private void InitToolTips()
        {
            toolTip1.SetToolTip(button_Save, "Сохранить");
            toolTip1.SetToolTip(button_Close, "Выход");
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (textBox_Name.Text.Length != 0 && textBox_Number.Text.Length != 0 &&
                richTextBox_Description.Text.Length != 0)
            {
                if (!correctIP)
                {
                    MessageBox.Show(@"Неправильный ip адрес!", @"Внимание");
                    return;
                }
                else
                {
                    mtbIp.Text = mtbIp.Text.Replace(" ", "");
                }

                DataTable dtDate = _sql.GetDate();
                DateTime nowTime = DateTime.Now;
                if (dtDate != null)
                {
                    nowTime = (DateTime)dtDate.Rows[0][0];
                }




                var hwId = (int)comboBox_Hardware.SelectedValue;
                var date = nowTime.Date;
                var time = nowTime.TimeOfDay;
                var finalDate = date + time;
                var idCreator = UserSettings.User.Id;
                var fio = textBox_Name.Text;
                var cabinet = textBox_Number.Text;
                var fault = richTextBox_Description.Text;
                var ip = mtbIp.Text;
                DataTable dtAddReq = _sql.CreateNewRequest(hwId, finalDate, idCreator, fio, cabinet, fault, ip);
                id_Req = (int)dtAddReq.Rows[0]["id"];

                Logging.StartFirstLevel(97);
                Logging.Comment("Создание заявки!");
                Logging.Comment("№ кабинета:" + textBox_Number.Text);
                Logging.Comment("Дата подачи:" + date.ToShortDateString());
                Logging.Comment("Время подачи:" + time.ToString());
                Logging.Comment("Оборудование ID:" + hwId + ";Наименование: " + comboBox_Hardware.Text);
                Logging.Comment("ФИО:" + textBox_Name.Text);
                Logging.Comment("Описание неисправности:" + richTextBox_Description.Text);

                Logging.Comment("Операцию выполнил: ID:" + Nwuram.Framework.Settings.User.UserSettings.User.Id
                                + " ; ФИО:" + Nwuram.Framework.Settings.User.UserSettings.User.FullUsername);
                Logging.StopFirstLevel();
                MessageBox.Show(@"Создание заявки прошло успешно!", @"Успех");
              
                if (Sql.bufferDataTable != null && Sql.bufferDataTable.Rows.Count > 0)
                    foreach (DataRow r in Sql.bufferDataTable.Rows)
                        _sql.setScan(id_Req, (byte[])r["Scan"], (string)r["cName"], (string)r["Extension"]);

                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(@"Не все поля заполнены!", @"Внимание");
            }
        }

        private void textBox_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar == '\b' || char.IsWhiteSpace(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void mtbIp_TextChanged(object sender, EventArgs e)
        {
            Regex ip = new Regex(@"^([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            string input = mtbIp.Text;
            correctIP = (ip.IsMatch(input) != true) ? true : false;
            if (ip != null) ip = null;

            if (ip == null || !ip.IsMatch(input))
            {
                string[] ip_code = input.Split('.');
                int testc = ip_code.Length;
                for (int i = 0; i < ip_code.Length; i++)
                {
                    if (i > 4)
                        ip_code[i] = "";
                    if (ip_code[i].Length > 3)
                        ip_code[i] = ip_code[i].Substring(0, 3);
                    if (ip_code[i] != "" && int.Parse(ip_code[i]) > 255)
                        ip_code[i] = "255";
                }

                input = "";
                foreach (string item in ip_code)
                    input += item + ".";

                input = input.Substring(0, input.Length - 1);

                mtbIp.Text = input;

                mtbIp.Focus();
                mtbIp.SelectionStart = mtbIp.Text.Length;
            }
        }

        private void mtbIp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btDoc_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentForm frmDoc = new DocumentForm(false) { id_Req = 0 };
                frmDoc.ShowDialog();
            }
            catch (Exception)
            { }
        }
     
        private void CreateRequest_FormClosing(object sender, FormClosingEventArgs e)
        {            
        }
    
    }
}