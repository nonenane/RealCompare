using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;

namespace RealCompare
{
    class DBFWorker
    {
        private OleDbConnection Connection;
        private string path;

        public DBFWorker(string path)
        {
            this.path = path;
            Connection = new OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + this.path.Substring(0,this.path.LastIndexOf("\\")) + ";Extended Properties=dBASE IV;");
        }
        
        /// <summary>
        /// Выполнение запросов к dbf файлу
        /// </summary>
        /// <param name="Command">SQL запрос</param>
        /// <returns></returns>
        public DataTable ExecuteCommand(string Command)
        {
            DataTable result = new DataTable();
            try
            {
                Connection.Open();
                OleDbCommand cmd = new OleDbCommand(Command, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    result = new DataTable();
                    result.Load(reader);
                }
                Connection.Close();  
            }
            catch(OleDbException ex)
            {

            }
            return result;

        }
    }
}
