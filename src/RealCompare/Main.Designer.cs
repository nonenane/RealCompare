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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbEAN = new System.Windows.Forms.TextBox();
            this.lbEAN = new System.Windows.Forms.Label();
            this.grpGroups = new System.Windows.Forms.GroupBox();
            this.rbDateAndVVO = new System.Windows.Forms.RadioButton();
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
            this.DateReal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMainKass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KsSql = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealSql = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cGraphRealiz = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDelta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isRealEquals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsMainGridContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.установитьСверкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.снятьСверкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьЗаявкуНаРемонтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpSources = new System.Windows.Forms.GroupBox();
            this.chbGraphRealiz = new System.Windows.Forms.CheckBox();
            this.chbMainKass = new System.Windows.Forms.CheckBox();
            this.chkRealSql = new System.Windows.Forms.CheckBox();
            this.chkKsSql = new System.Windows.Forms.CheckBox();
            this.pbData = new System.Windows.Forms.ProgressBar();
            this.ldRashExists = new System.Windows.Forms.Label();
            this.tbTotalKsSql = new System.Windows.Forms.TextBox();
            this.tbTotalRealSql = new System.Windows.Forms.TextBox();
            this.lbTotal = new System.Windows.Forms.Label();
            this.ttButtons = new System.Windows.Forms.ToolTip(this.components);
            this.btReportMainKass = new System.Windows.Forms.Button();
            this.btViewRepair = new System.Windows.Forms.Button();
            this.btCheckExisting = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.btPrint = new System.Windows.Forms.Button();
            this.btRefresh = new System.Windows.Forms.Button();
            this.bgwGetCompare = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkShowOnlyRash = new System.Windows.Forms.CheckBox();
            this.tbTotalcDelta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvRepaireRequest = new System.Windows.Forms.DataGridView();
            this.cNumRequest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDateRequest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRequestInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRequestConfirm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRequestStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbFio = new System.Windows.Forms.TextBox();
            this.tbDateAdd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btDel = new System.Windows.Forms.Button();
            this.btEdit = new System.Windows.Forms.Button();
            this.btAdd = new System.Windows.Forms.Button();
            this.tbTotalcMainKass = new System.Windows.Forms.TextBox();
            this.bsMain = new System.Windows.Forms.BindingSource(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tbTotalcDiscount = new System.Windows.Forms.TextBox();
            this.tbTotalcGraphRealiz = new System.Windows.Forms.TextBox();
            this.grpSearch.SuspendLayout();
            this.grpGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.cmsMainGridContext.SuspendLayout();
            this.grpSources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepaireRequest)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).BeginInit();
            this.statusStrip1.SuspendLayout();
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
            this.grpSearch.Location = new System.Drawing.Point(220, 73);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(899, 51);
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
            this.tbName.Size = new System.Drawing.Size(563, 20);
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
            this.grpGroups.Controls.Add(this.rbDateAndVVO);
            this.grpGroups.Controls.Add(this.rbDateAndDepAndGood);
            this.grpGroups.Controls.Add(this.rbDate);
            this.grpGroups.Controls.Add(this.rbDateAndDep);
            this.grpGroups.Location = new System.Drawing.Point(12, 12);
            this.grpGroups.Name = "grpGroups";
            this.grpGroups.Size = new System.Drawing.Size(199, 112);
            this.grpGroups.TabIndex = 0;
            this.grpGroups.TabStop = false;
            this.grpGroups.Text = "Группировки:";
            // 
            // rbDateAndVVO
            // 
            this.rbDateAndVVO.AutoSize = true;
            this.rbDateAndVVO.Location = new System.Drawing.Point(6, 88);
            this.rbDateAndVVO.Name = "rbDateAndVVO";
            this.rbDateAndVVO.Size = new System.Drawing.Size(148, 17);
            this.rbDateAndVVO.TabIndex = 12;
            this.rbDateAndVVO.Text = "Дата и разделение ВВО";
            this.rbDateAndVVO.UseVisualStyleBackColor = true;
            this.rbDateAndVVO.CheckedChanged += new System.EventHandler(this.rbDateAndVVO_CheckedChanged);
            // 
            // rbDateAndDepAndGood
            // 
            this.rbDateAndDepAndGood.AutoSize = true;
            this.rbDateAndDepAndGood.Location = new System.Drawing.Point(6, 65);
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
            this.rbDate.Location = new System.Drawing.Point(6, 19);
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
            this.rbDateAndDep.Location = new System.Drawing.Point(6, 42);
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
            this.lbDateFrom.Location = new System.Drawing.Point(217, 29);
            this.lbDateFrom.Name = "lbDateFrom";
            this.lbDateFrom.Size = new System.Drawing.Size(57, 13);
            this.lbDateFrom.TabIndex = 1;
            this.lbDateFrom.Text = "Период с:";
            // 
            // lbDateTo
            // 
            this.lbDateTo.AutoSize = true;
            this.lbDateTo.Location = new System.Drawing.Point(473, 29);
            this.lbDateTo.Name = "lbDateTo";
            this.lbDateTo.Size = new System.Drawing.Size(22, 13);
            this.lbDateTo.TabIndex = 2;
            this.lbDateTo.Text = "по:";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(277, 25);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(162, 20);
            this.dtpStart.TabIndex = 3;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(538, 25);
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
            this.cbDeps.Location = new System.Drawing.Point(277, 50);
            this.cbDeps.Name = "cbDeps";
            this.cbDeps.Size = new System.Drawing.Size(162, 21);
            this.cbDeps.TabIndex = 5;
            this.cbDeps.SelectedValueChanged += new System.EventHandler(this.cbDeps_SelectedValueChanged);
            // 
            // cbTUGroups
            // 
            this.cbTUGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTUGroups.FormattingEnabled = true;
            this.cbTUGroups.Location = new System.Drawing.Point(538, 50);
            this.cbTUGroups.Name = "cbTUGroups";
            this.cbTUGroups.Size = new System.Drawing.Size(162, 21);
            this.cbTUGroups.TabIndex = 6;
            this.cbTUGroups.SelectedValueChanged += new System.EventHandler(this.cbTUGroups_SelectedValueChanged);
            // 
            // lbDep
            // 
            this.lbDep.AutoSize = true;
            this.lbDep.Location = new System.Drawing.Point(217, 54);
            this.lbDep.Name = "lbDep";
            this.lbDep.Size = new System.Drawing.Size(41, 13);
            this.lbDep.TabIndex = 7;
            this.lbDep.Text = "Отдел:";
            // 
            // lbTgrp
            // 
            this.lbTgrp.AutoSize = true;
            this.lbTgrp.Location = new System.Drawing.Point(473, 54);
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
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvMain.ColumnHeadersHeight = 40;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateReal,
            this.Department,
            this.EAN,
            this.cName,
            this.cMainKass,
            this.cDiscount,
            this.KsSql,
            this.RealSql,
            this.cGraphRealiz,
            this.cDelta,
            this.isRealEquals});
            this.dgvMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMain.Location = new System.Drawing.Point(12, 130);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(1107, 273);
            this.dgvMain.TabIndex = 9;
            this.dgvMain.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMain_CellFormatting);
            this.dgvMain.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMain_CellMouseClick);
            this.dgvMain.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvMain_CellPainting);
            this.dgvMain.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgwMain_ColumnWidthChanged);
            this.dgvMain.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvMain_RowPostPaint);
            this.dgvMain.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvMain_RowPrePaint);
            this.dgvMain.SelectionChanged += new System.EventHandler(this.dgvMain_SelectionChanged);
            // 
            // DateReal
            // 
            this.DateReal.DataPropertyName = "date";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.Format = "d";
            dataGridViewCellStyle14.NullValue = null;
            this.DateReal.DefaultCellStyle = dataGridViewCellStyle14;
            this.DateReal.FillWeight = 50F;
            this.DateReal.HeaderText = "Дата реал.";
            this.DateReal.MinimumWidth = 20;
            this.DateReal.Name = "DateReal";
            this.DateReal.ReadOnly = true;
            // 
            // Department
            // 
            this.Department.DataPropertyName = "depName";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Department.DefaultCellStyle = dataGridViewCellStyle15;
            this.Department.FillWeight = 50F;
            this.Department.HeaderText = "Отдел";
            this.Department.MinimumWidth = 20;
            this.Department.Name = "Department";
            this.Department.ReadOnly = true;
            // 
            // EAN
            // 
            this.EAN.DataPropertyName = "ean";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.EAN.DefaultCellStyle = dataGridViewCellStyle16;
            this.EAN.FillWeight = 60F;
            this.EAN.HeaderText = "EAN";
            this.EAN.MinimumWidth = 20;
            this.EAN.Name = "EAN";
            this.EAN.ReadOnly = true;
            // 
            // cName
            // 
            this.cName.DataPropertyName = "goodsName";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cName.DefaultCellStyle = dataGridViewCellStyle17;
            this.cName.FillWeight = 190F;
            this.cName.HeaderText = "Наименование товара";
            this.cName.MinimumWidth = 20;
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // cMainKass
            // 
            this.cMainKass.DataPropertyName = "MainKass";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "N2";
            dataGridViewCellStyle18.NullValue = null;
            this.cMainKass.DefaultCellStyle = dataGridViewCellStyle18;
            this.cMainKass.HeaderText = "Главная касса";
            this.cMainKass.Name = "cMainKass";
            this.cMainKass.ReadOnly = true;
            this.cMainKass.Visible = false;
            // 
            // cDiscount
            // 
            this.cDiscount.DataPropertyName = "discount";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle19.Format = "N2";
            dataGridViewCellStyle19.NullValue = null;
            this.cDiscount.DefaultCellStyle = dataGridViewCellStyle19;
            this.cDiscount.HeaderText = "Скидка";
            this.cDiscount.Name = "cDiscount";
            this.cDiscount.ReadOnly = true;
            this.cDiscount.Visible = false;
            // 
            // KsSql
            // 
            this.KsSql.DataPropertyName = "KsSql";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle20.Format = "N2";
            this.KsSql.DefaultCellStyle = dataGridViewCellStyle20;
            this.KsSql.FillWeight = 50F;
            this.KsSql.HeaderText = "Шахматка";
            this.KsSql.MinimumWidth = 20;
            this.KsSql.Name = "KsSql";
            this.KsSql.ReadOnly = true;
            // 
            // RealSql
            // 
            this.RealSql.DataPropertyName = "RealSql";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle21.Format = "N2";
            this.RealSql.DefaultCellStyle = dataGridViewCellStyle21;
            this.RealSql.FillWeight = 50F;
            this.RealSql.HeaderText = "Реал. SQL";
            this.RealSql.MinimumWidth = 20;
            this.RealSql.Name = "RealSql";
            this.RealSql.ReadOnly = true;
            // 
            // cGraphRealiz
            // 
            this.cGraphRealiz.DataPropertyName = "graphRealiz";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle22.Format = "N2";
            dataGridViewCellStyle22.NullValue = null;
            this.cGraphRealiz.DefaultCellStyle = dataGridViewCellStyle22;
            this.cGraphRealiz.HeaderText = "График реализации";
            this.cGraphRealiz.Name = "cGraphRealiz";
            this.cGraphRealiz.ReadOnly = true;
            this.cGraphRealiz.Visible = false;
            // 
            // cDelta
            // 
            this.cDelta.DataPropertyName = "delta";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle23.Format = "N2";
            dataGridViewCellStyle23.NullValue = null;
            this.cDelta.DefaultCellStyle = dataGridViewCellStyle23;
            this.cDelta.HeaderText = "Дельта";
            this.cDelta.Name = "cDelta";
            this.cDelta.ReadOnly = true;
            this.cDelta.Visible = false;
            // 
            // isRealEquals
            // 
            this.isRealEquals.DataPropertyName = "isRealEquals";
            this.isRealEquals.HeaderText = "isRealEquals";
            this.isRealEquals.Name = "isRealEquals";
            this.isRealEquals.ReadOnly = true;
            this.isRealEquals.Visible = false;
            // 
            // cmsMainGridContext
            // 
            this.cmsMainGridContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.установитьСверкуToolStripMenuItem,
            this.снятьСверкуToolStripMenuItem,
            this.создатьЗаявкуНаРемонтToolStripMenuItem});
            this.cmsMainGridContext.Name = "cmsMainContext";
            this.cmsMainGridContext.Size = new System.Drawing.Size(210, 70);
            this.cmsMainGridContext.Opening += new System.ComponentModel.CancelEventHandler(this.cmsMainGridContext_Opening);
            // 
            // установитьСверкуToolStripMenuItem
            // 
            this.установитьСверкуToolStripMenuItem.Name = "установитьСверкуToolStripMenuItem";
            this.установитьСверкуToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.установитьСверкуToolStripMenuItem.Text = "Установить сверку";
            this.установитьСверкуToolStripMenuItem.Click += new System.EventHandler(this.установитьСверкуToolStripMenuItem_Click);
            // 
            // снятьСверкуToolStripMenuItem
            // 
            this.снятьСверкуToolStripMenuItem.Name = "снятьСверкуToolStripMenuItem";
            this.снятьСверкуToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.снятьСверкуToolStripMenuItem.Text = "Снять сверку";
            this.снятьСверкуToolStripMenuItem.Click += new System.EventHandler(this.снятьСверкуToolStripMenuItem_Click);
            // 
            // создатьЗаявкуНаРемонтToolStripMenuItem
            // 
            this.создатьЗаявкуНаРемонтToolStripMenuItem.Name = "создатьЗаявкуНаРемонтToolStripMenuItem";
            this.создатьЗаявкуНаРемонтToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.создатьЗаявкуНаРемонтToolStripMenuItem.Text = "Создать заявку на ремонт";
            this.создатьЗаявкуНаРемонтToolStripMenuItem.Click += new System.EventHandler(this.создатьЗаявкуНаРемонтToolStripMenuItem_Click);
            // 
            // grpSources
            // 
            this.grpSources.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpSources.Controls.Add(this.chbGraphRealiz);
            this.grpSources.Controls.Add(this.chbMainKass);
            this.grpSources.Controls.Add(this.chkRealSql);
            this.grpSources.Controls.Add(this.chkKsSql);
            this.grpSources.Location = new System.Drawing.Point(12, 461);
            this.grpSources.Name = "grpSources";
            this.grpSources.Size = new System.Drawing.Size(183, 110);
            this.grpSources.TabIndex = 10;
            this.grpSources.TabStop = false;
            this.grpSources.Text = "Источники данных:";
            // 
            // chbGraphRealiz
            // 
            this.chbGraphRealiz.AutoSize = true;
            this.chbGraphRealiz.Location = new System.Drawing.Point(13, 88);
            this.chbGraphRealiz.Name = "chbGraphRealiz";
            this.chbGraphRealiz.Size = new System.Drawing.Size(128, 17);
            this.chbGraphRealiz.TabIndex = 7;
            this.chbGraphRealiz.Text = "График Реализации";
            this.chbGraphRealiz.UseVisualStyleBackColor = true;
            this.chbGraphRealiz.CheckedChanged += new System.EventHandler(this.chbGraphRealiz_CheckedChanged);
            // 
            // chbMainKass
            // 
            this.chbMainKass.AutoSize = true;
            this.chbMainKass.Location = new System.Drawing.Point(13, 66);
            this.chbMainKass.Name = "chbMainKass";
            this.chbMainKass.Size = new System.Drawing.Size(101, 17);
            this.chbMainKass.TabIndex = 6;
            this.chbMainKass.Text = "Главная касса";
            this.chbMainKass.UseVisualStyleBackColor = true;
            this.chbMainKass.CheckedChanged += new System.EventHandler(this.chbMainKass_CheckedChanged);
            // 
            // chkRealSql
            // 
            this.chkRealSql.AutoSize = true;
            this.chkRealSql.Checked = true;
            this.chkRealSql.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRealSql.Location = new System.Drawing.Point(13, 44);
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
            this.chkKsSql.Location = new System.Drawing.Point(13, 22);
            this.chkKsSql.Name = "chkKsSql";
            this.chkKsSql.Size = new System.Drawing.Size(77, 17);
            this.chkKsSql.TabIndex = 1;
            this.chkKsSql.Text = "Шахматка";
            this.chkKsSql.UseVisualStyleBackColor = true;
            this.chkKsSql.CheckedChanged += new System.EventHandler(this.chkKsSql_CheckedChanged);
            // 
            // pbData
            // 
            this.pbData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbData.Location = new System.Drawing.Point(1002, 439);
            this.pbData.MarqueeAnimationSpeed = 200;
            this.pbData.Name = "pbData";
            this.pbData.Size = new System.Drawing.Size(117, 23);
            this.pbData.Step = 20;
            this.pbData.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbData.TabIndex = 11;
            this.pbData.Visible = false;
            // 
            // ldRashExists
            // 
            this.ldRashExists.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ldRashExists.AutoSize = true;
            this.ldRashExists.Location = new System.Drawing.Point(38, 413);
            this.ldRashExists.Name = "ldRashExists";
            this.ldRashExists.Size = new System.Drawing.Size(101, 13);
            this.ldRashExists.TabIndex = 17;
            this.ldRashExists.Text = "Есть расхождения";
            // 
            // tbTotalKsSql
            // 
            this.tbTotalKsSql.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbTotalKsSql.BackColor = System.Drawing.Color.White;
            this.tbTotalKsSql.Location = new System.Drawing.Point(661, 408);
            this.tbTotalKsSql.Name = "tbTotalKsSql";
            this.tbTotalKsSql.ReadOnly = true;
            this.tbTotalKsSql.Size = new System.Drawing.Size(87, 20);
            this.tbTotalKsSql.TabIndex = 19;
            this.tbTotalKsSql.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbTotalRealSql
            // 
            this.tbTotalRealSql.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbTotalRealSql.BackColor = System.Drawing.Color.White;
            this.tbTotalRealSql.Location = new System.Drawing.Point(747, 408);
            this.tbTotalRealSql.Name = "tbTotalRealSql";
            this.tbTotalRealSql.ReadOnly = true;
            this.tbTotalRealSql.Size = new System.Drawing.Size(87, 20);
            this.tbTotalRealSql.TabIndex = 19;
            this.tbTotalRealSql.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotal.AutoSize = true;
            this.lbTotal.Location = new System.Drawing.Point(380, 412);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(40, 13);
            this.lbTotal.TabIndex = 20;
            this.lbTotal.Text = "Итоги:";
            // 
            // btReportMainKass
            // 
            this.btReportMainKass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btReportMainKass.BackgroundImage = global::RealCompare.Properties.Resources.print1;
            this.btReportMainKass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btReportMainKass.Location = new System.Drawing.Point(961, 465);
            this.btReportMainKass.Name = "btReportMainKass";
            this.btReportMainKass.Size = new System.Drawing.Size(35, 35);
            this.btReportMainKass.TabIndex = 30;
            this.ttButtons.SetToolTip(this.btReportMainKass, "Отчёт");
            this.btReportMainKass.UseVisualStyleBackColor = true;
            this.btReportMainKass.Click += new System.EventHandler(this.btReportMainKass_Click);
            // 
            // btViewRepair
            // 
            this.btViewRepair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btViewRepair.BackgroundImage = global::RealCompare.Properties.Resources.old_edit_find;
            this.btViewRepair.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btViewRepair.Enabled = false;
            this.btViewRepair.Location = new System.Drawing.Point(677, 99);
            this.btViewRepair.Name = "btViewRepair";
            this.btViewRepair.Size = new System.Drawing.Size(35, 35);
            this.btViewRepair.TabIndex = 26;
            this.ttButtons.SetToolTip(this.btViewRepair, "Проверка наличия чеков");
            this.btViewRepair.UseVisualStyleBackColor = true;
            this.btViewRepair.Click += new System.EventHandler(this.btViewRepair_Click);
            // 
            // btCheckExisting
            // 
            this.btCheckExisting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCheckExisting.Image = ((System.Drawing.Image)(resources.GetObject("btCheckExisting.Image")));
            this.btCheckExisting.Location = new System.Drawing.Point(961, 513);
            this.btCheckExisting.Name = "btCheckExisting";
            this.btCheckExisting.Size = new System.Drawing.Size(35, 35);
            this.btCheckExisting.TabIndex = 23;
            this.ttButtons.SetToolTip(this.btCheckExisting, "Проверка наличия чеков");
            this.btCheckExisting.UseVisualStyleBackColor = true;
            this.btCheckExisting.Visible = false;
            this.btCheckExisting.Click += new System.EventHandler(this.btCheckExisting_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btExit.BackgroundImage = global::RealCompare.Properties.Resources.door_in;
            this.btExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btExit.Location = new System.Drawing.Point(1084, 514);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(35, 35);
            this.btExit.TabIndex = 16;
            this.ttButtons.SetToolTip(this.btExit, "Выход");
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btPrint
            // 
            this.btPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrint.BackgroundImage = global::RealCompare.Properties.Resources.printer;
            this.btPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btPrint.Enabled = false;
            this.btPrint.Location = new System.Drawing.Point(1043, 513);
            this.btPrint.Name = "btPrint";
            this.btPrint.Size = new System.Drawing.Size(35, 35);
            this.btPrint.TabIndex = 15;
            this.ttButtons.SetToolTip(this.btPrint, "Печать");
            this.btPrint.UseVisualStyleBackColor = true;
            this.btPrint.Click += new System.EventHandler(this.btPrint_Click);
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btRefresh.BackgroundImage = global::RealCompare.Properties.Resources.arrow_refresh;
            this.btRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btRefresh.Location = new System.Drawing.Point(1002, 513);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(35, 35);
            this.btRefresh.TabIndex = 14;
            this.ttButtons.SetToolTip(this.btRefresh, "Обновить");
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
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
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(139)))), ((int)(((byte)(115)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(12, 409);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(20, 20);
            this.panel1.TabIndex = 21;
            // 
            // chkShowOnlyRash
            // 
            this.chkShowOnlyRash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowOnlyRash.AutoSize = true;
            this.chkShowOnlyRash.Location = new System.Drawing.Point(12, 438);
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
            this.tbTotalcDelta.Location = new System.Drawing.Point(1032, 408);
            this.tbTotalcDelta.Name = "tbTotalcDelta";
            this.tbTotalcDelta.ReadOnly = true;
            this.tbTotalcDelta.Size = new System.Drawing.Size(87, 20);
            this.tbTotalcDelta.TabIndex = 24;
            this.tbTotalcDelta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTotalcDelta.Visible = false;
            this.tbTotalcDelta.TextChanged += new System.EventHandler(this.tbTotalcDelta_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Ошибка сверки";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(11)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Location = new System.Drawing.Point(17, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(20, 20);
            this.panel2.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Сверено";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(255)))), ((int)(((byte)(153)))));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Location = new System.Drawing.Point(158, 23);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(20, 20);
            this.panel3.TabIndex = 21;
            // 
            // dgvRepaireRequest
            // 
            this.dgvRepaireRequest.AllowUserToAddRows = false;
            this.dgvRepaireRequest.AllowUserToDeleteRows = false;
            this.dgvRepaireRequest.AllowUserToResizeRows = false;
            this.dgvRepaireRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRepaireRequest.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRepaireRequest.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.dgvRepaireRequest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRepaireRequest.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cNumRequest,
            this.cDateRequest,
            this.cRequestInfo,
            this.cRequestConfirm,
            this.cRequestStatus});
            this.dgvRepaireRequest.Location = new System.Drawing.Point(17, 49);
            this.dgvRepaireRequest.MultiSelect = false;
            this.dgvRepaireRequest.Name = "dgvRepaireRequest";
            this.dgvRepaireRequest.ReadOnly = true;
            this.dgvRepaireRequest.RowHeadersVisible = false;
            this.dgvRepaireRequest.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvRepaireRequest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRepaireRequest.Size = new System.Drawing.Size(613, 85);
            this.dgvRepaireRequest.TabIndex = 25;
            // 
            // cNumRequest
            // 
            this.cNumRequest.DataPropertyName = "Number";
            this.cNumRequest.HeaderText = "№ заявки";
            this.cNumRequest.Name = "cNumRequest";
            this.cNumRequest.ReadOnly = true;
            // 
            // cDateRequest
            // 
            this.cDateRequest.DataPropertyName = "DateSubmission";
            this.cDateRequest.HeaderText = "Дата";
            this.cDateRequest.Name = "cDateRequest";
            this.cDateRequest.ReadOnly = true;
            // 
            // cRequestInfo
            // 
            this.cRequestInfo.DataPropertyName = "Fault";
            this.cRequestInfo.HeaderText = "Описание неисправности";
            this.cRequestInfo.Name = "cRequestInfo";
            this.cRequestInfo.ReadOnly = true;
            // 
            // cRequestConfirm
            // 
            this.cRequestConfirm.DataPropertyName = "DateConfirm";
            this.cRequestConfirm.HeaderText = "Дата подтверждения";
            this.cRequestConfirm.Name = "cRequestConfirm";
            this.cRequestConfirm.ReadOnly = true;
            // 
            // cRequestStatus
            // 
            this.cRequestStatus.DataPropertyName = "cName";
            this.cRequestStatus.HeaderText = "Статус";
            this.cRequestStatus.Name = "cRequestStatus";
            this.cRequestStatus.ReadOnly = true;
            // 
            // tbFio
            // 
            this.tbFio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbFio.BackColor = System.Drawing.Color.White;
            this.tbFio.Location = new System.Drawing.Point(306, 23);
            this.tbFio.Name = "tbFio";
            this.tbFio.ReadOnly = true;
            this.tbFio.Size = new System.Drawing.Size(227, 20);
            this.tbFio.TabIndex = 27;
            // 
            // tbDateAdd
            // 
            this.tbDateAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDateAdd.BackColor = System.Drawing.Color.White;
            this.tbDateAdd.Location = new System.Drawing.Point(634, 23);
            this.tbDateAdd.Name = "tbDateAdd";
            this.tbDateAdd.ReadOnly = true;
            this.tbDateAdd.Size = new System.Drawing.Size(119, 20);
            this.tbDateAdd.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Добавил";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(539, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Дата добавления";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvRepaireRequest);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbDateAdd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbFio);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btDel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btEdit);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.btAdd);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.btViewRepair);
            this.groupBox1.Location = new System.Drawing.Point(196, 432);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 139);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // btDel
            // 
            this.btDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btDel.Image = global::RealCompare.Properties.Resources.Trash;
            this.btDel.Location = new System.Drawing.Point(718, 47);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(35, 35);
            this.btDel.TabIndex = 26;
            this.btDel.UseVisualStyleBackColor = true;
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btEdit
            // 
            this.btEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btEdit.Image = global::RealCompare.Properties.Resources.Edit;
            this.btEdit.Location = new System.Drawing.Point(677, 47);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(35, 35);
            this.btEdit.TabIndex = 26;
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btAdd.Image = global::RealCompare.Properties.Resources.Add;
            this.btAdd.Location = new System.Drawing.Point(636, 47);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(35, 35);
            this.btAdd.TabIndex = 26;
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // tbTotalcMainKass
            // 
            this.tbTotalcMainKass.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbTotalcMainKass.BackColor = System.Drawing.Color.White;
            this.tbTotalcMainKass.Location = new System.Drawing.Point(568, 408);
            this.tbTotalcMainKass.Name = "tbTotalcMainKass";
            this.tbTotalcMainKass.ReadOnly = true;
            this.tbTotalcMainKass.Size = new System.Drawing.Size(87, 20);
            this.tbTotalcMainKass.TabIndex = 29;
            this.tbTotalcMainKass.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTotalcMainKass.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 574);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1131, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 31;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripProgressBar1.RightToLeftLayout = true;
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar1.Visible = false;
            // 
            // tbTotalcDiscount
            // 
            this.tbTotalcDiscount.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbTotalcDiscount.BackColor = System.Drawing.Color.White;
            this.tbTotalcDiscount.Location = new System.Drawing.Point(476, 408);
            this.tbTotalcDiscount.Name = "tbTotalcDiscount";
            this.tbTotalcDiscount.ReadOnly = true;
            this.tbTotalcDiscount.Size = new System.Drawing.Size(87, 20);
            this.tbTotalcDiscount.TabIndex = 32;
            this.tbTotalcDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTotalcDiscount.Visible = false;
            // 
            // tbTotalcGraphRealiz
            // 
            this.tbTotalcGraphRealiz.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbTotalcGraphRealiz.BackColor = System.Drawing.Color.White;
            this.tbTotalcGraphRealiz.Location = new System.Drawing.Point(840, 408);
            this.tbTotalcGraphRealiz.Name = "tbTotalcGraphRealiz";
            this.tbTotalcGraphRealiz.ReadOnly = true;
            this.tbTotalcGraphRealiz.Size = new System.Drawing.Size(87, 20);
            this.tbTotalcGraphRealiz.TabIndex = 33;
            this.tbTotalcGraphRealiz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbTotalcGraphRealiz.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 596);
            this.Controls.Add(this.tbTotalcGraphRealiz);
            this.Controls.Add(this.tbTotalcDiscount);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btReportMainKass);
            this.Controls.Add(this.tbTotalcMainKass);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbTotalcDelta);
            this.Controls.Add(this.btCheckExisting);
            this.Controls.Add(this.chkShowOnlyRash);
            this.Controls.Add(this.lbTotal);
            this.Controls.Add(this.tbTotalRealSql);
            this.Controls.Add(this.tbTotalKsSql);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ldRashExists);
            this.Controls.Add(this.pbData);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.btPrint);
            this.Controls.Add(this.btRefresh);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepaireRequest)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsMain)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkRealSql;
        private System.Windows.Forms.CheckBox chkKsSql;
        private System.Windows.Forms.ContextMenuStrip cmsMainGridContext;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Button btPrint;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Label ldRashExists;
        private System.Windows.Forms.TextBox tbTotalKsSql;
        private System.Windows.Forms.TextBox tbTotalRealSql;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.ToolTip ttButtons;
        private System.ComponentModel.BackgroundWorker bgwGetCompare;
        private System.Windows.Forms.BindingSource bsMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkShowOnlyRash;
        private System.Windows.Forms.Button btCheckExisting;
        private System.Windows.Forms.TextBox tbTotalcDelta;
        private System.Windows.Forms.RadioButton rbDateAndVVO;
        private System.Windows.Forms.CheckBox chbMainKass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvRepaireRequest;
        private System.Windows.Forms.Button btViewRepair;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.Button btDel;
        private System.Windows.Forms.TextBox tbFio;
        private System.Windows.Forms.TextBox tbDateAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem установитьСверкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem снятьСверкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьЗаявкуНаРемонтToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbTotalcMainKass;
        private System.Windows.Forms.Button btReportMainKass;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumRequest;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDateRequest;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRequestInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRequestConfirm;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRequestStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TextBox tbTotalcDiscount;
        private System.Windows.Forms.CheckBox chbGraphRealiz;
        private System.Windows.Forms.TextBox tbTotalcGraphRealiz;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateReal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Department;
        private System.Windows.Forms.DataGridViewTextBoxColumn EAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMainKass;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn KsSql;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealSql;
        private System.Windows.Forms.DataGridViewTextBoxColumn cGraphRealiz;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDelta;
        private System.Windows.Forms.DataGridViewTextBoxColumn isRealEquals;
    }
}

