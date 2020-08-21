using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using Nwuram.Framework.Settings.Connection;

namespace RealCompare
{
    class SqlWorker : SqlProvider
    {
        ArrayList ap = new ArrayList();

        public SqlWorker(string server, string database, string username, string password, string appName)
            : base(server, database, username, password, appName)
        {
        }

        /// <summary>
        /// Получение списка отделов
        /// </summary>
        /// <returns></returns>
        public DataTable GetDepartments()
        {
            ap.Clear();
            return executeProcedure("[RealCompare].[GetDeps]", new string[] {}, new DbType[] {}, ap);
            
        }

        /// <summary>
        /// Получение пути к dbf из prog_config
        /// </summary>
        /// <returns></returns>
        public string GetPathToDbf(int type)
        {
            ap.Clear();
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            ap.Add(type);
            DataTable result = executeProcedure("[RealCompare].[GetPath]", new string[2] { "@id_prog", "@type" }, new DbType[2] { DbType.Int32, DbType.Int32 }, ap);

            if (result.Rows.Count == 0)
            {
                return "";
            }
            else
            {
                return result.Rows[0][0].ToString();
            }
           
        }

        /// <summary>
        /// Получение ТУ групп для отдела
        /// </summary>
        /// <param name="id_department">id отдела</param>
        /// <returns></returns>
        public DataTable GetGroups(int id_department)
        {
            ap.Clear();
            ap.Add(id_department);
            return executeProcedure("[RealCompare].[GetGroups]", new string[1] { "@id_department" }, new DbType[1] { DbType.Int32 }, ap);
        }
        

        /// <summary>
        /// Получение текущей даты
        /// </summary>
        /// <returns>Текущая дата</returns>
        public DateTime GetDate()
        {
            DateTime retDate;
            ap.Clear();
            DataTable result = executeProcedure("dbo.GetDate", new string[] { }, new DbType[] { }, ap);
            if (result != null
                && result.Rows.Count > 0
                && DateTime.TryParse(result.Rows[0][0].ToString(), out retDate))
            {
                return retDate;
            }
            else
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Получение данных по чекам
        /// </summary>
        /// <param name="date">Дата выборки </param>
        /// <returns></returns>
        public DataTable GetCheckData(DateTime date)
        {
            ap.Clear();
            ap.Add(date);
            return executeProcedure("[RealCompare].[GetCheckData]", new string[1] { "@date" }, new DbType[1] { DbType.DateTime }, ap);
        }

        /// <summary>
        /// Добавление данных в journal_
        /// </summary>
        /// <param name="date">Дата</param>
        /// <param name="terminal">Касса</param>
        /// <param name="passwordLength">Длинна пароля</param>
        /// <param name="time_sec">Время в секндах</param>
        /// <param name="op_code">Код операции</param>
        /// <param name="doc_id">Номер док-та</param>
        /// <param name="ean">Код товара</param>
        /// <param name="count">Кол-во</param>
        /// <param name="price">Цена</param>
        /// <param name="cash_val">Сумма</param>
        /// <param name="dpt_no">Отдел</param>
        /// <param name="opType">Тип операции из dbf</param>
        /// <param name="cnt">Кол-во товара в чеке</param>
        public void InsertJournalData(DateTime date, int terminal, int passwordLength, int time_sec, int op_code, int doc_id, string ean, int count, int price, int cash_val, int dpt_no, int opType, int cnt)
        {
            ap.Clear();
            ap.AddRange(new object[] {
                                    date.Date, 
                                    terminal, 
                                    passwordLength, 
                                    time_sec, 
                                    op_code, 
                                    doc_id, 
                                    ean, 
                                    (op_code == 507 ? Math.Abs(count) : count), 
                                    price, 
                                    (op_code == 507 ? Math.Abs(cash_val) : cash_val), 
                                    dpt_no,
                                    opType,
                                    cnt});
            executeProcedure("[RealCompare].[InsertJournalData]",
                            new string[] { "@date", "@terminal", "@passwordLength", "@time_sec", "@op_code", "@doc_id", "@ean", "@count", "@price", "@cash_val", "@dpt_no", "@opType", "@cnt" },
                            new DbType[] { DbType.DateTime, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 },
                            ap);     
        }

        /// <summary>
        /// Получение реализации из SQL
        /// </summary>
        /// <param name="dateStart">Назало периода</param>
        /// <param name="dateEnd">Конец периода</param>
        /// <param name="groupType">Тип группировки</param>
        /// <param name="isShah">Признак выбора шахматки</param>
        /// <param name="isKsSql">Признак выбора касс</param>
        /// <param name="isRealSql">Признак выбора j_realiz</param>
        /// <param name="isRealSql2">Признак выбора j_realiz2</param>
        /// <returns>Таблица с реализациями</returns>
        public DataTable GetCompareData(DateTime dateStart, DateTime dateEnd, int groupType, bool isShah, bool isKsSql, bool isRealSql, bool isRealSql2)
        {
            ap.Clear();
            ap.AddRange(new object[8] { dateStart, dateEnd, groupType, Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram(), isShah, isKsSql, isRealSql, isRealSql2 });
            return executeProcedure("[RealCompare].[GetCompareData]",
                                    new string[8] { "@dateStart", "@dateEnd", "@groupType", "@id_prog", "@isShah", "@isKsSql", "@isRealSql", "@isRealSql2" },
                                    new DbType[8] { DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.Boolean }, ap);
        }

        /// <summary>
        /// Получение информации о товарах
        /// </summary>
        /// <param name="dateStart">Начало периода</param>
        /// <param name="dateEnd">Конец периода</param>
        /// <returns>Таблица с информацией о товарах</returns>
        public DataTable GetGoodsData(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            return executeProcedure("[RealCompare].[GetGoodsData]", new string[2] { "@dateStart", "@dateEnd" }, new DbType[2] { DbType.DateTime, DbType.DateTime }, ap);
        }




        #region Разнос по коннектам от июня 2020
        /// <summary>
        /// Получение товаров основной коннект dbase1.dbo.j_realiz
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetJRealizMain(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            return executeProcedure("[RealCompare].[GetDataDBASE1]",
               new string[2] { "@dateStart", "@dateEnd" }, 
               new DbType[2] { DbType.DateTime, DbType.DateTime }, ap);
        }
        /// <summary>
        /// Получение списка KassRealiz.dbo.goods_updates
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetGoodsUpdates(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            return executeProcedure("[RealCompare].[GetDataGoodsUpdates]",
                    new string[2] { "@dateStart", "@dateEnd" },
                    new DbType[2] { DbType.DateTime, DbType.DateTime }, ap);
        }

        /// <summary>
        /// Получение данных dbase1.j_realiz VVO
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetJRealizVVO(DateTime dateStart, DateTime dateEnd)
        {

            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            return executeProcedure("[RealCompare].[GetDataDBASE1]",
               new string[2] { "@dateStart", "@dateEnd" },
               new DbType[2] { DbType.DateTime, DbType.DateTime }, ap);
        }
        /// <summary>
        /// Получение с KassRealiz основной коннект
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetKassRealizJournal(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            return executeProcedure("[RealCompare].[GetCompareDataJournal]",
               new string[2] { "@dateStart", "@dateEnd" },
               new DbType[2] { DbType.DateTime, DbType.DateTime }, ap);
        }
        /// <summary>
        /// Получение с KassRealiz ВВо-коннект
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public DataTable GetKassRealizJournalVVO(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            return executeProcedure("[RealCompare].[GetCompareDataJournal]",
               new string[2] { "@dateStart", "@dateEnd" },
               new DbType[2] { DbType.DateTime, DbType.DateTime }, ap);
        }


        #endregion


        #region "Реализация главной кассы"
    
        /// <summary>
        /// Запись\редактирование\удаление данных по главной кассе
        /// </summary>
        /// <param name="date">Дата выборки </param>
        /// <returns></returns>
        public async Task<DataTable> setMainKass(int id, DateTime date, decimal mainKass, bool isVVO, bool isDel, int result)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(date);
            ap.Add(mainKass);
            ap.Add(isVVO);
            ap.Add(UserSettings.User.Id);
            ap.Add(isDel);
            ap.Add(result);

            return executeProcedure("[RealCompare].[spg_setMainKass]",
                new string[7] { "@id", "@date", "@MainKass", "@isVVO", "@id_user", "@isDel", "@result" },
                new DbType[7] { DbType.Int32, DbType.DateTime, DbType.Decimal, DbType.Boolean, DbType.Int32, DbType.Boolean, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение данных по главной кассе
        /// </summary>
        /// <param name="date">Дата выборки </param>
        /// <returns></returns>
        public async Task<DataTable> getMainKass(DateTime date, bool isVVO)
        {
            ap.Clear();
            ap.Add(date);
            ap.Add(isVVO);

            return executeProcedure("[RealCompare].[spg_getMainKass]",
                new string[2] { "@date", "@isVVO"},
                new DbType[2] { DbType.DateTime,DbType.Boolean}, ap);
        }

        /// <summary>
        /// Получение данных по реализации на промежуток дат
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public async Task<DataTable> GetRealizForReportMainKass(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            return executeProcedure("[RealCompare].[GetRealizForReportMainKass]",
               new string[2] { "@dateStart", "@dateEnd" },
               new DbType[2] { DbType.DateTime, DbType.DateTime }, ap);
        }


        /// <summary>
        /// Получение данных по реализации на промежуток дат
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public async Task<DataTable> GetReportMainKass(DateTime dateStart, DateTime dateEnd, int type)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(type);

            return executeProcedure("[RealCompare].[GetReportMainKass]",
               new string[3] { "@dateStart", "@dateEnd", "@typeReport" },
               new DbType[3] { DbType.DateTime, DbType.DateTime, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Получение данных по главной кассе за промежуток дат
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public async Task<DataTable> getMainKassForListDate(DateTime dateStart, DateTime dateEnd)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            

            return executeProcedure("[RealCompare].[spg_getMainKassForListDate]",
               new string[2] { "@dateStart", "@dateEnd" },
               new DbType[2] { DbType.DateTime, DbType.DateTime}, ap);
        }

        /// <summary>
        /// Запись\редактирование\удаление данных по главной кассе
        /// </summary>
        /// <param name="date">Дата выборки </param>
        /// <returns></returns>
        public async Task<DataTable> setValidateMainKass(int id, decimal? RealSQL,decimal? ChessBoard)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(RealSQL);
            ap.Add(ChessBoard);
            ap.Add(UserSettings.User.Id);

            return executeProcedure("[RealCompare].[spg_setValidateMainKass]",
                new string[4] { "@id", "@RealSQL", "@ChessBoard", "@id_user"},
                new DbType[4] { DbType.Int32, DbType.Decimal, DbType.Decimal, DbType.Int32 }, ap);
        }

        /// <summary>
        /// Запись\редактирование\удаление данных по главной кассе
        /// </summary>
        /// <param name="date">Дата выборки </param>
        /// <returns></returns>
        public async Task<DataTable> setRequestOfDifference(int id_MainKass, int id_RequestRepair, bool isVVO, int SourceDifference)
        {
            ap.Clear();
            ap.Add(id_MainKass);
            ap.Add(id_RequestRepair);
            ap.Add(isVVO);
            ap.Add(SourceDifference);

            return executeProcedure("[RealCompare].[spg_setRequestOfDifference]",
                new string[4] { "@id_MainKass", "@id_RequestRepair", "@isVVO", "@SourceDifference" },
                new DbType[4] { DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Int32 }, ap);
        }


        /// <summary>
        /// Получение данных по заявкам на ремонт по главной кассе
        /// </summary>
        /// <param name="date">Дата выборки </param>
        /// <returns></returns>
        public async Task<DataTable> getListRepairRequestForMainKass(int id_MainKass)
        {
            ap.Clear();
            ap.Add(id_MainKass);
            bool is5Connect = ConnectionSettings.GetServer("5").Trim().Length > 0;
            ap.Add(is5Connect);

            return executeProcedure("[RealCompare].[spg_getListRepairRequestForMainKass]",
                new string[2] { "@id_MainKass","@is5Connect" },
                new DbType[2] { DbType.Int32,DbType.Boolean }, ap);
        }

        #endregion

        #region "Скидки"
        /// <summary>
        /// Получение данных по скидкам
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public async Task<DataTable> getDiscount(DateTime dateStart, DateTime dateEnd, bool isVVO)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(isVVO);
           
            return executeProcedure("[RealCompare].[spg_getDiscount]",
               new string[3] { "@dateStart", "@dateEnd","@isVVO" },
               new DbType[3] { DbType.Date, DbType.Date,DbType.Boolean}, ap);
        }
        #endregion

        #region "График Реализации"
        /// <summary>
        /// Получение данных по графику реализации
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public async Task<DataTable> getRealizHours(DateTime dateStart, DateTime dateEnd, bool withDeps)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(withDeps);

            return executeProcedure("[RealCompare].[spg_getRealizHours]",
               new string[3] { "@dateStart", "@dateEnd", "@withDeps" },
               new DbType[3] { DbType.Date, DbType.Date, DbType.Boolean }, ap);
        }
        #endregion

        #region "Новые процедуры для расчёта"
        /// <summary>
        /// Получение данных по реализации
        /// </summary>
        /// <param name="dateStart">Начальная дата выборки</param>
        /// <param name="dateEnd">Конечная дата выборки</param>
        /// <param name="isVVO">Признак ВВО</param>
        /// <param name="withTovar">Признак выборки по товарам</param>
        /// <returns></returns>
        public async Task<DataTable> getRealizForDate(DateTime dateStart, DateTime dateEnd, bool isVVO,bool withTovar)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(isVVO);
            ap.Add(withTovar);

            return executeProcedure("[RealCompare].[sgp_getRealizForDate]",
               new string[4] { "@dateStart", "@dateEnd", "@isVVO","@withTovar" },
               new DbType[4] { DbType.Date, DbType.Date, DbType.Boolean, DbType.Boolean }, ap);
        }


        /// <summary>
        /// Получение данных для сверки c журнала продаж
        /// </summary>
        /// <param name="dateStart">Начальная дата выборки</param>
        /// <param name="dateEnd">Конечная дата выборки</param>
        /// <param name="isVVO">Признак ВВО</param>        
        /// <returns></returns>
        public async Task<DataTable> getJournalForDate(DateTime dateStart, DateTime dateEnd, bool isVVO)
        {
            ap.Clear();
            ap.Add(dateStart);
            ap.Add(dateEnd);
            ap.Add(isVVO);

            return executeProcedure("[RealCompare].[sgp_getJournalForDate]",
               new string[3] { "@dateStart", "@dateEnd", "@isVVO" },
               new DbType[3] { DbType.Date, DbType.Date, DbType.Boolean}, ap);
        }

        #endregion
    }
}
