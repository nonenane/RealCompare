namespace RealCompare
{
    partial class Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbEAN = new System.Windows.Forms.TextBox();
            this.lbEAN = new System.Windows.Forms.Label();
            this.grpGroups = new System.Windows.Forms.GroupBox();
            this.rbDateAndDepAndGood = new System.Windows.Forms.RadioButton();
            this.rbDate = new System.Windows.Forms.RadioButton();
            this.rbDateAndDep = new System.Windows.Forms.RadioButton();
            this.lbDateFrom = new System.Windows.Forms.Label();
            this.lbDateTo = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.cbDeps = new System.Windows.Forms.ComboBox();
            this.cbTUGroups = new System.Windows.Forms.ComboBox();
            this.lbDep = new System.Windows.Forms.Label();
            this.lbTgrp = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.cmsMainGridContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.grpSources = new System.Windows.Forms.GroupBox();
            this.chkRealDbf = new System.Windows.Forms.CheckBox();
            this.chkRealSql2 = new System.Windows.Forms.CheckBox();
            this.chkRealSql = new System.Windows.Forms.CheckBox();
            this.chkKsSql = new System.Windows.Forms.CheckBox();
            this.chkShah = new System.Windows.Forms.CheckBox();
            this.pbData = new System.Windows.Forms.ProgressBar();
            this.btRefresh = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.ldRashExists = new System.Windows.Forms.Label();
            this.tbTotalKsSql = new System.Windows.Forms.TextBox();
            this.tbTotalRealSql = new System.Windows.Forms.TextBox();
            this.lbTotal = new System.Windows.Forms.Label();
            this.ttButtons = new System.Windows.Forms.ToolTip(this.components);
            this.btCheckExisting = new System.Windows.Forms.Button();
            this.bgwGetCompare = new System.ComponentModel.BackgroundWorker();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkShowOnlyRash = new System.Windows.Forms.CheckBox();
            this.tbTotalcDelta = new System.Windows.Forms.TextBox();
            this.DateReal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isRealEquals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KsSql = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealSql = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDelta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpSearch.SuspendLayout();
            this.grpGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.cmsMainGridContext.SuspendLayout();
            this.grpSources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSearch
            // 
            this.grpSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSearch.Controls.Add(this.lbName);
            this.grpSearch.Controls.Add(this.tbName);
            this.grpSearch.Controls.Add(this.tbEAN);
            this.grpSearch.Controls.Add(this.lbEAN);
            this.grpSearch.Location = new System.Drawing.Point(156, 73);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(738, 51);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Поиск";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(236, 22);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(86, 13);
            this.lbName.TabIndex = 11;
            this.lbName.Text = "Наименование:";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbName.Enabled = false;
            this.tbName.Location = new System.Drawing.Point(330, 19);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(399, 20);
            this.tbName.TabIndex = 10;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            this.tbName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbName_KeyPress);
            // 
            // tbEAN
            // 
            this.tbEAN.Enabled = false;
            this.tbEAN.Location = new System.Drawing.Point(64, 19);
            this.tbEAN.MaxLength = 13;
            this.tbEAN.Name = "tbEAN";
            this.tbEAN.Size = new System.Drawing.Size(162, 20);
            this.tbEAN.TabIndex = 10;
            this.tbEAN.TextChanged += new System.EventHandler(this.tbEAN_TextChanged);
            this.tbEAN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEAN_KeyPress);
            // 
            // lbEAN
            // 
            this.lbEAN.AutoSize = true;
            this.lbEAN.Location = new System.Drawing.Point(13, 23);
            this.lbEAN.Name = "lbEAN";
            this.lbEAN.Size = new System.Drawing.Size(32, 13);
            this.lbEAN.TabIndex = 9;
            this.lbEAN.Text = "EAN:";
            // 
            // grpGroups
            // 
            this.grpGroups.Controls.Add(this.rbDateAndDepAndGood);
            this.grpGroups.Controls.Add(this.rbDate);
            this.grpGroups.Controls.Add(this.rbDateAndDep);
            this.grpGroups.Location = new System.Drawing.Point(12, 12);
            this.grpGroups.Name = "grpGroups";
            this.grpGroups.Size = new System.Drawing.Size(138, 112);
            this.grpGroups.TabIndex = 0;
            this.grpGroups.TabStop = false;
            this.grpGroups.Text = "Группировки:";
            // 
            // rbDateAndDepAndGood
            // 
            this.rbDateAndDepAndGood.AutoSize = true;
            this.rbDateAndDepAndGood.Enabled = false;
            this.rbDateAndDepAndGood.Location = new System.Drawing.Point(6, 76);
            this.rbDateAndDepAndGood.Name = "rbDateAndDepAndGood";
            this.rbDateAndDepAndGood.Size = new System.Drawing.Size(121, 17);
            this.rbDateAndDepAndGood.TabIndex = 11;
            this.rbDateAndDepAndGood.Text = "Дата, отдел, товар";
            this.rbDateAndDepAndGood.UseVisualStyleBackColor = true;
            this.rbDateAndDepAndGood.CheckedChanged += new System.EventHandler(this.rbDateAndDepAndGood_CheckedChanged);
            // 
            // rbDate
            // 
            this.rbDate.AutoSize = true;
            this.rbDate.Checked = true;
            this.rbDate.Location = new System.Drawing.Point(6, 30);
            this.rbDate.Name = "rbDate";
            this.rbDate.Size = new System.Drawing.Size(51, 17);
            this.rbDate.TabIndex = 9;
            this.rbDate.TabStop = true;
            this.rbDate.Text = "Дата";
            this.rbDate.UseVisualStyleBackColor = true;
            this.rbDate.CheckedChanged += new System.EventHandler(this.rbDate_CheckedChanged);
            // 
            // rbDateAndDep
            // 
            this.rbDateAndDep.AutoSize = true;
            this.rbDateAndDep.Location = new System.Drawing.Point(6, 53);
            this.rbDateAndDep.Name = "rbDateAndDep";
            this.rbDateAndDep.Size = new System.Drawing.Size(92, 17);
            this.rbDateAndDep.TabIndex = 10;
            this.rbDateAndDep.Text = "Дата и отдел";
            this.rbDateAndDep.UseVisualStyleBackColor = true;
            this.rbDateAndDep.CheckedChanged += new System.EventHandler(this.rbDateAndDep_CheckedChanged);
            // 
            // lbDateFrom
            // 
            this.lbDateFrom.AutoSize = true;
            this.lbDateFrom.Location = new System.Drawing.Point(160, 26);
            this.lbDateFrom.Name = "lbDateFrom";
            this.lbDateFrom.Size = new System.Drawing.Size(57, 13);
            this.lbDateFrom.TabIndex = 1;
            this.lbDateFrom.Text = "Период с:";
            // 
            // lbDateTo
            // 
            this.lbDateTo.AutoSize = true;
            this.lbDateTo.Location = new System.Drawing.Point(416, 26);
            this.lbDateTo.Name = "lbDateTo";
            this.lbDateTo.Size = new System.Drawing.Size(22, 13);
            this.lbDateTo.TabIndex = 2;
            this.lbDateTo.Text = "по:";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(220, 22);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(162, 20);
            this.dtpStart.TabIndex = 3;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(481, 22);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(162, 20);
            this.dtpEnd.TabIndex = 4;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // cbDeps
            // 
            this.cbDeps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDeps.Enabled = false;
            this.cbDeps.FormattingEnabled = true;
            this.cbDeps.Location = new System.Drawing.Point(220, 48);
            this.cbDeps.Name = "cbDeps";
            this.cbDeps.Size = new System.Drawing.Size(162, 21);
            this.cbDeps.TabIndex = 5;
            this.cbDeps.SelectedValueChanged += new System.EventHandler(this.cbDeps_SelectedValueChanged);
            // 
            // cbTUGroups
            // 
            this.cbTUGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTUGroups.FormattingEnabled = true;
            this.cbTUGroups.Location = new System.Drawing.Point(481, 48);
            this.cbTUGroups.Name = "cbTUGroups";
            this.cbTUGroups.Size = new System.Drawing.Size(162, 21);
            this.cbTUGroups.TabIndex = 6;
            this.cbTUGroups.SelectedValueChanged += new System.EventHandler(this.cbTUGroups_SelectedValueChanged);
            // 
            // lbDep
            // 
            this.lbDep.AutoSize = true;
            this.lbDep.Location = new System.Drawing.Point(160, 51);
            this.lbDep.Name = "lbDep";
            this.lbDep.Size = new System.Drawing.Size(41, 13);
            this.lbDep.TabIndex = 7;
            this.lbDep.Text = "Отдел:";
            // 
            // lbTgrp
            // 
            this.lbTgrp.AutoSize = true;
            this.lbTgrp.Location = new System.Drawing.Point(416, 51);
            this.lbTgrp.Name = "lbTgrp";
            this.lbTgrp.Size = new System.Drawing.Size(62, 13);
            this.lbTgrp.TabIndex = 8;
            this.lbTgrp.Text = "ТУ группа:";
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToResizeRows = false;
            this.dgvMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMain.ColumnHeadersHeight = 40;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateReal,
            this.Department,
            this.EAN,
            this.cName,
            this.isRealEquals,
            this.KsSql,
            this.RealSql,
            this.cDelta});
            this.dgvMain.ContextMenuStrip = this.cmsMainGridContext;
            this.dgvMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMain.Location = new System.Drawing.Point(12, 130);
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.Size = new System.Drawing.Size(882, 314);
            this.dgvMain.TabIndex = 9;
            this.dgvMain.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgwMain_ColumnWidthChanged);
            this.dgvMain.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvMain_RowPrePaint);
            this.dgvMain.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvMain_RowsAdded);
            this.dgvMain.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvMain_RowsRemoved);
            // 
            // cmsMainGridContext
            // 
            this.cmsMainGridContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRefresh,
            this.tsPrint});
            this.cmsMainGridContext.Name = "cmsMainContext";
            this.cmsMainGridContext.Size = new System.Drawing.Size(129, 48);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(128, 22);
            this.tsRefresh.Text = "Обновить";
            this.tsRefresh.Click += new System.EventHandler(this.tsRefresh_Click);
            // 
            // tsPrint
            // 
            this.tsPrint.Enabled = false;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(128, 22);
            this.tsPrint.Text = "Печать";
            // 
            // grpSources
            // 
            this.grpSources.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpSources.Controls.Add(this.chkRealDbf);
            this.grpSources.Controls.Add(this.chkRealSql2);
            this.grpSources.Controls.Add(this.chkRealSql);
            this.grpSources.Controls.Add(this.chkKsSql);
            this.grpSources.Controls.Add(this.chkShah);
            this.grpSources.Location = new System.Drawing.Point(12, 473);
            this.grpSources.Name = "grpSources";
            this.grpSources.Size = new System.Drawing.Size(293, 85);
            this.grpSources.TabIndex = 10;
            this.grpSources.TabStop = false;
            this.grpSources.Text = "Источники данных:";
            // 
            // chkRealDbf
            // 
            this.chkRealDbf.AutoSize = true;
            this.chkRealDbf.Location = new System.Drawing.Point(208, 28);
            this.chkRealDbf.Name = "chkRealDbf";
            this.chkRealDbf.Size = new System.Drawing.Size(78, 17);
            this.chkRealDbf.TabIndex = 5;
            this.chkRealDbf.Text = "Реал. DBF";
            this.chkRealDbf.UseVisualStyleBackColor = true;
            this.chkRealDbf.CheckedChanged += new System.EventHandler(this.chkRealDbf_CheckedChanged);
            // 
            // chkRealSql2
            // 
            this.chkRealSql2.AutoSize = true;
            this.chkRealSql2.Location = new System.Drawing.Point(92, 51);
            this.chkRealSql2.Name = "chkRealSql2";
            this.chkRealSql2.Size = new System.Drawing.Size(87, 17);
            this.chkRealSql2.TabIndex = 4;
            this.chkRealSql2.Text = "Реал. SQL2 ";
            this.chkRealSql2.UseVisualStyleBackColor = true;
            this.chkRealSql2.CheckedChanged += new System.EventHandler(this.chkRealSql2_CheckedChanged);
            // 
            // chkRealSql
            // 
            this.chkRealSql.AutoSize = true;
            this.chkRealSql.Checked = true;
            this.chkRealSql.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRealSql.Location = new System.Drawing.Point(92, 28);
            this.chkRealSql.Name = "chkRealSql";
            this.chkRealSql.Size = new System.Drawing.Size(78, 17);
            this.chkRealSql.TabIndex = 3;
            this.chkRealSql.Text = "Реал. SQL";
            this.chkRealSql.UseVisualStyleBackColor = true;
            this.chkRealSql.CheckedChanged += new System.EventHandler(this.chkRealSql_CheckedChanged);
            // 
            // chkKsSql
            // 
            this.chkKsSql.AutoSize = true;
            this.chkKsSql.Checked = true;
            this.chkKsSql.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKsSql.Location = new System.Drawing.Point(6, 51);
            this.chkKsSql.Name = "chkKsSql";
            this.chkKsSql.Size = new System.Drawing.Size(64, 17);
            this.chkKsSql.TabIndex = 2;
            this.chkKsSql.Text = "КС SQL";
            this.chkKsSql.UseVisualStyleBackColor = true;
            this.chkKsSql.CheckedChanged += new System.EventHandler(this.chkKsSql_CheckedChanged);
            // 
            // chkShah
            // 
            this.chkShah.AutoSize = true;
            this.chkShah.Location = new System.Drawing.Point(6, 28);
            this.chkShah.Name = "chkShah";
            this.chkShah.Size = new System.Drawing.Size(77, 17);
            this.chkShah.TabIndex = 1;
            this.chkShah.Text = "Шахматка";
            this.chkShah.UseVisualStyleBackColor = true;
            this.chkShah.CheckedChanged += new System.EventHandler(this.chkShah_CheckedChanged);
            // 
            // pbData
            // 
            this.pbData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbData.Location = new System.Drawing.Point(12, 563);
            this.pbData.MarqueeAnimationSpeed = 200;
            this.pbData.Name = "pbData";
            this.pbData.Size = new System.Drawing.Size(740, 23);
            this.pbData.Step = 20;
            this.pbData.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbData.TabIndex = 11;
            this.pbData.Visible = false;
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btRefresh.BackgroundImage = global::RealCompare.Properties.Resources.arrow_refresh;
            this.btRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btRefresh.Location = new System.Drawing.Point(777, 513);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(35, 35);
            this.btRefresh.TabIndex = 14;
            this.ttButtons.SetToolTip(this.btRefresh, "Обновить");
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.BackgroundImage = global::RealCompare.Properties.Resources.printer;
            this.btPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btPrint.Enabled = false;
            this.btPrint.Location = new System.Drawing.Point(818, 513);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(35, 35);
            this.btPrint.TabIndex = 15;
            this.ttButtons.SetToolTip(this.btPrint, "Печать");
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.BackgroundImage = global::RealCompare.Properties.Resources.door_in;
            this.btExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btExit.Location = new System.Drawing.Point(859, 514);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(35, 35);
            this.btExit.TabIndex = 16;
            this.ttButtons.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // ldRashExists
            // 
            this.ldRashExists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ldRashExists.AutoSize = true;
            this.ldRashExists.Location = new System.Drawing.Point(793, 561);
            this.ldRashExists.Name = "ldRashExists";
            this.ldRashExists.Size = new System.Drawing.Size(101, 13);
            this.ldRashExists.TabIndex = 17;
            this.ldRashExists.Text = "Есть расхождения";
            // 
            // tbTotalKsSql
            // 
            this.tbTotalKsSql.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbTotalKsSql.BackColor = System.Drawing.Color.White;
            this.tbTotalKsSql.Location = new System.Drawing.Point(549, 450);
            this.tbTotalKsSql.Name = "tbTotalKsSql";
            this.tbTotalKsSql.ReadOnly = true;
            this.tbTotalKsSql.Size = new System.Drawing.Size(87, 20);
            this.tbTotalKsSql.TabIndex = 19;
            // 
            // tbTotalRealSql
            // 
            this.tbTotalRealSql.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbTotalRealSql.BackColor = System.Drawing.Color.White;
            this.tbTotalRealSql.Location = new System.Drawing.Point(635, 450);
            this.tbTotalRealSql.Name = "tbTotalRealSql";
            this.tbTotalRealSql.ReadOnly = true;
            this.tbTotalRealSql.Size = new System.Drawing.Size(87, 20);
            this.tbTotalRealSql.TabIndex = 19;
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotal.AutoSize = true;
            this.lbTotal.Location = new System.Drawing.Point(417, 453);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(40, 13);
            this.lbTotal.TabIndex = 20;
            this.lbTotal.Text = "Итоги:";
            // 
            // btCheckExisting
            // 
            this.btCheckExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCheckExisting.Image = ((System.Drawing.Image)(resources.GetObject("btCheckExisting.Image")));
            this.btCheckExisting.Location = new System.Drawing.Point(736, 513);
            this.btCheckExisting.Name = "btCheckExisting";
            this.btCheckExisting.Size = new System.Drawing.Size(35, 35);
            this.btCheckExisting.TabIndex = 23;
            this.ttButtons.SetToolTip(this.btCheckExisting, "Проверка наличия чеков");
            this.btCheckExisting.UseVisualStyleBackColor = true;
            this.btCheckExisting.Click += new System.EventHandler(this.btCheckExisting_Click);
            // 
            // bgwGetCompare
            // 
            this.bgwGetCompare.WorkerReportsProgress = true;
            this.bgwGetCompare.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwGetCompare_DoWork);
            this.bgwGetCompare.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwGetCompare_ProgressChanged);
            this.bgwGetCompare.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwGetCompare_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(139)))), ((int)(((byte)(115)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(773, 557);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 20);
            this.panel1.TabIndex = 21;
            // 
            // chkShowOnlyRash
            // 
            this.chkShowOnlyRash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowOnlyRash.AutoSize = true;
            this.chkShowOnlyRash.Location = new System.Drawing.Point(711, 476);
            this.chkShowOnlyRash.Name = "chkShowOnlyRash";
            this.chkShowOnlyRash.Size = new System.Drawing.Size(183, 17);
            this.chkShowOnlyRash.TabIndex = 22;
            this.chkShowOnlyRash.Text = "Показать только расхождения";
            this.chkShowOnlyRash.UseVisualStyleBackColor = true;
            this.chkShowOnlyRash.CheckedChanged += new System.EventHandler(this.chkShowOnlyRash_CheckedChanged);
            // 
            // tbTotalcDelta
            // 
            this.tbTotalcDelta.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbTotalcDelta.BackColor = System.Drawing.Color.White;
            this.tbTotalcDelta.Location = new System.Drawing.Point(807, 450);
            this.tbTotalcDelta.Name = "tbTotalcDelta";
            this.tbTotalcDelta.ReadOnly = true;
            this.tbTotalcDelta.Size = new System.Drawing.Size(87, 20);
            this.tbTotalcDelta.TabIndex = 24;
            this.tbTotalcDelta.Visible = false;
            this.tbTotalcDelta.TextChanged += new System.EventHandler(this.tbTotalcDelta_TextChanged);
            // 
            // DateReal
            // 
            this.DateReal.DataPropertyName = "date";
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle31.Format = "d";
            dataGridViewCellStyle31.NullValue = null;
            this.DateReal.DefaultCellStyle = dataGridViewCellStyle31;
            this.DateReal.FillWeight = 50F;
            this.DateReal.HeaderText = "Дата реал.";
            this.DateReal.MinimumWidth = 20;
            this.DateReal.Name = "DateReal";
            // 
            // Department
            // 
            this.Department.DataPropertyName = "depName";
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Department.DefaultCellStyle = dataGridViewCellStyle32;
            this.Department.FillWeight = 50F;
            this.Department.HeaderText = "Отдел";
            this.Department.MinimumWidth = 20;
            this.Department.Name = "Department";
            // 
            // EAN
            // 
            this.EAN.DataPropertyName = "ean";
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.EAN.DefaultCellStyle = dataGridViewCellStyle33;
            this.EAN.FillWeight = 60F;
            this.EAN.HeaderText = "EAN";
            this.EAN.MinimumWidth = 20;
            this.EAN.Name = "EAN";
            // 
            // cName
            // 
            this.cName.DataPropertyName = "goodsName";
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cName.DefaultCellStyle = dataGridViewCellStyle34;
            this.cName.FillWeight = 190F;
            this.cName.HeaderText = "Наименование товара";
            this.cName.MinimumWidth = 20;
            this.cName.Name = "cName";
            // 
            // isRealEquals
            // 
            this.isRealEquals.DataPropertyName = "isRealEquals";
            this.isRealEquals.HeaderText = "isRealEquals";
            this.isRealEquals.Name = "isRealEquals";
            this.isRealEquals.Visible = false;
            // 
            // KsSql
            // 
            this.KsSql.DataPropertyName = "KsSql";
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle35.Format = "N2";
            this.KsSql.DefaultCellStyle = dataGridViewCellStyle35;
            this.KsSql.FillWeight = 50F;
            this.KsSql.HeaderText = "КС SQL";
            this.KsSql.MinimumWidth = 20;
            this.KsSql.Name = "KsSql";
            // 
            // RealSql
            // 
            this.RealSql.DataPropertyName = "RealSql";
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle36.Format = "N2";
            this.RealSql.DefaultCellStyle = dataGridViewCellStyle36;
            this.RealSql.FillWeight = 50F;
            this.RealSql.HeaderText = "Реал. SQL";
            this.RealSql.MinimumWidth = 20;
            this.RealSql.Name = "RealSql";
            // 
            // cDelta
            // 
            this.cDelta.DataPropertyName = "delta";
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cDelta.DefaultCellStyle = dataGridViewCellStyle37;
            this.cDelta.HeaderText = "Дельта";
            this.cDelta.Name = "cDelta";
            this.cDelta.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 596);
            this.Controls.Add(this.tbTotalcDelta);
            this.Controls.Add(this.btCheckExisting);
            this.Controls.Add(this.chkShowOnlyRash);
            this.Controls.Add(this.lbTotal);
            this.Controls.Add(this.tbTotalRealSql);
            this.Controls.Add(this.tbTotalKsSql);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ldRashExists);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btRefresh);
            this.Controls.Add(this.pbData);
            this.Controls.Add(this.grpSources);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.lbTgrp);
            this.Controls.Add(this.lbDep);
            this.Controls.Add(this.cbTUGroups);
            this.Controls.Add(this.cbDeps);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lbDateTo);
            this.Controls.Add(this.lbDateFrom);
            this.Controls.Add(this.grpGroups);
            this.Controls.Add(this.grpSearch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.grpGroups.ResumeLayout(false);
            this.grpGroups.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.cmsMainGridContext.ResumeLayout(false);
            this.grpSources.ResumeLayout(false);
            this.grpSources.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.GroupBox grpGroups;
        private System.Windows.Forms.Label lbDateFrom;
        private System.Windows.Forms.Label lbDateTo;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.RadioButton rbDateAndDepAndGood;
        private System.Windows.Forms.RadioButton rbDate;
        private System.Windows.Forms.RadioButton rbDateAndDep;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.ComboBox cbDeps;
        private System.Windows.Forms.ComboBox cbTUGroups;
        private System.Windows.Forms.Label lbDep;
        private System.Windows.Forms.Label lbTgrp;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbEAN;
        private System.Windows.Forms.Label lbEAN;
        private System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.GroupBox grpSources;
        private System.Windows.Forms.ProgressBar pbData;
        private System.Windows.Forms.CheckBox chkRealDbf;
        private System.Windows.Forms.CheckBox chkRealSql2;
        private System.Windows.Forms.CheckBox chkRealSql;
        private System.Windows.Forms.CheckBox chkKsSql;
        private System.Windows.Forms.CheckBox chkShah;
        private System.Windows.Forms.ContextMenuStrip cmsMainGridContext;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Label ldRashExists;
        private System.Windows.Forms.TextBox tbTotalKsSql;
        private System.Windows.Forms.TextBox tbTotalRealSql;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.ToolStripMenuItem tsRefresh;
        private System.Windows.Forms.ToolStripMenuItem tsPrint;
        private System.Windows.Forms.ToolTip ttButtons;
        private System.ComponentModel.BackgroundWorker bgwGetCompare;
        private System.Windows.Forms.BindingSource bsMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkShowOnlyRash;
        private System.Windows.Forms.Button btCheckExisting;
        private System.Windows.Forms.TextBox tbTotalcDelta;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateReal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Department;
        private System.Windows.Forms.DataGridViewTextBoxColumn EAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn isRealEquals;
        private System.Windows.Forms.DataGridViewTextBoxColumn KsSql;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealSql;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDelta;
    }
}

