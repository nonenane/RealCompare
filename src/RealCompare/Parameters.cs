using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RealCompare
{
    static class Parameters
    {
        /// <summary>
        /// Основной коннект
        /// </summary>
        public static SqlWorker hConnect { get; set; }
        /// <summary>
        /// Путь к r_tovar.dbf
        /// </summary>
        public static string dbfPath { get; set; }
        /// <summary>
        /// Коннект к серверу ВВО
        /// </summary>
        public static SqlWorker hConnectVVO { get; set; }
        /// <summary>
        /// Коннект к серверу с KassRealiz
        /// </summary>
        public static SqlWorker hConnectKass { get; set; }
        /// <summary>
        /// Коннект к серверу с KassRealiz ВВО
        /// </summary>
        public static SqlWorker hConnectVVOKass { get; set; }
        #region Параметры для загрузки данных
        public static int groupType { get; set; }
        public static bool isShah { get; set; }
        public static bool isKsSql { get; set; }
        public static bool isRealSql { get; set; }
        public static bool isRealSql2 { get; set; }
        public static bool isRealDbf { get; set; }
        public static DateTime dateStart { get; set; }
        public static DateTime dateEnd { get; set; }
        public static int idDep { get; set; }
        #endregion

        #region Параметры для отчета
        public static string dates { get; set; }
        public static string deps { get; set; }
        public static string tuGrps { get; set; }
        public static string grpBy { get; set; }
        public static string srcs { get; set; }
        public static DataTable Data { get; set; }
        #endregion


    }
}
