using System;
using System.Collections;
using System.Data;
using Nwuram.Framework.Data;

namespace RepairRequestMN
{
  internal class Sql : SqlProvider
  {
    public static DataTable bufferDataTable;

    private static readonly ArrayList ParameterValues = new ArrayList();

    public Sql(string server, string database, string username, string password, string appName)
      : base(server, database, username, password, appName)
    {
    }

    public DataTable GetActiveHardware()
    {
      ParameterValues.Clear();
      return executeProcedure("[Repair].[GetActiveHardware]", new string[] { }, new DbType[] { }, ParameterValues);
    }

    public DataTable CreateNewRequest(int hwId, DateTime date, int idCreator, string fio, string cabinet, string fault, string ip)
    {
      ParameterValues.Clear();
      ParameterValues.Add(hwId);
      ParameterValues.Add(date);
      ParameterValues.Add(idCreator);
      ParameterValues.Add(fio);
      ParameterValues.Add(cabinet);
      ParameterValues.Add(fault);
      ParameterValues.Add(ip);
      return executeProcedure("[Repair].[CreateNewRequest]",
          new[] { "@hwId", "@date", "@idCreator", "@fio", "@cabinet", "@fault", "@ip" },
          new[] { DbType.Int32, DbType.DateTime, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String },
          ParameterValues);
    }

    #region "Работа с файлами"

    public DataTable setScan(int id_Doc, byte[] byteFile, string nameFile, string Extension)
    {
      ParameterValues.Clear();
      ParameterValues.Add(id_Doc);
      ParameterValues.Add(byteFile);
      ParameterValues.Add(nameFile);
      ParameterValues.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
      ParameterValues.Add(Extension);

      return executeProcedure("[Repair].[setScan]",
        new string[] { "@id_Req", "@byteFile", "@nameFile", "@idUser", "@Extension" },
        new DbType[] { DbType.Int32, DbType.Binary, DbType.String, DbType.Int32, DbType.String }, ParameterValues);
    }

    public DataTable getScan(int id_Req, int id)
    {
      ParameterValues.Clear();
      ParameterValues.Add(id_Req);
      ParameterValues.Add(id);

      return executeProcedure("[Repair].[getScan]",
        new string[] { "@id_Req", "@id" },
        new DbType[] { DbType.Int32, DbType.Int32 }, ParameterValues);
    }

    public DataTable updateScanName(int id, string nameFile)
    {
      ParameterValues.Clear();
      ParameterValues.Add(id);
      ParameterValues.Add(nameFile);

      return executeProcedure("[Repair].[updateScanName]",
        new string[] { "@id", "@nameFile" },
        new DbType[] { DbType.Int32, DbType.String }, ParameterValues);
    }

    public DataTable delScan(int id)
    {
      ParameterValues.Clear();
      ParameterValues.Add(id);

      return executeProcedure("[Repair].[delScan]",
        new string[] { "@id" },
        new DbType[] { DbType.Int32 }, ParameterValues);
    }

    #endregion

    public DataTable GetDate()
    {
      ParameterValues.Clear();
      return executeProcedure("dbo.GetDate", new string[] { }, new DbType[] { }, ParameterValues);
    }
  }
}