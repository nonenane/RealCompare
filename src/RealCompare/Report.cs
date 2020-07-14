using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace RealCompare
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
            this.Load += new EventHandler(report_Load);
        }

        private void report_Load(object sender, EventArgs e)
        {
            CompareReport crCompare = new CompareReport();
            ParameterFields paramFields;

            ParameterField paramField;
            ParameterDiscreteValue paramDiscreteValue;

            paramFields = new ParameterFields();

            //Заполняем шапку
            crCompare.DataDefinition.FormulaFields["dates"].Text = "\"" + Parameters.dates + "\"";
            crCompare.DataDefinition.FormulaFields["deps"].Text = "\""+Parameters.deps+"\"";
            crCompare.DataDefinition.FormulaFields["tuGrps"].Text = "\"" + Parameters.tuGrps + "\"";
            crCompare.DataDefinition.FormulaFields["grpBy"].Text = "\"" + Parameters.grpBy + "\"";
            crCompare.DataDefinition.FormulaFields["srcs"].Text = "\"" + Parameters.srcs + "\"";

            //Заполняем полей по количеству колонок в гриде
            for (int i = 1; i <= Parameters.Data.Columns.Count; i++ )
            {
                 DataColumn dtColumn = Parameters.Data.Columns[i-1];
                 if (dtColumn.ColumnName != "isPrint")
                 {
                     Parameters.Data.Columns[i - 1].ColumnName = "Col" + i.ToString();
                     paramField = new ParameterField();
                     paramField.Name = dtColumn.ColumnName;
                     paramDiscreteValue = new ParameterDiscreteValue();
                     paramDiscreteValue.Value = Parameters.Data.Rows[0][dtColumn.ColumnName].ToString();
                     paramField.CurrentValues.Add(paramDiscreteValue);
                     paramFields.Add(paramField);
                 }
            }

            //Оставшиеся колонки в отчете заполняем пустыми значениями
            for (int i = Parameters.Data.Columns.Count; i <= 8; i++)
            {
                paramField = new ParameterField();
                paramField.Name = "Col" + i.ToString();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = "";
                paramField.CurrentValues.Add(paramDiscreteValue);
                paramFields.Add(paramField);
            }

            //Вставляем список полей в отчет
            CompReportViewer.ParameterFieldInfo = paramFields;
            Parameters.Data.Rows.RemoveAt(0);

            crCompare.SetDataSource(Parameters.Data);
                       
            CompReportViewer.ReportSource = crCompare;
            CompReportViewer.Refresh();
        }
    }
}
