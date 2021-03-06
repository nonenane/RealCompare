﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace RealCompare
{
    static class Parameters
    {
        /// <summary>
        /// Основной коннект
        /// </summary>
        public static SqlWorker hConnect { get; set; }      
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

        /// <summary>
        /// Коннект к серверу с X14
        /// </summary>
        public static SqlWorker hConnectX14 { get; set; }
        #region Параметры для загрузки данных
        public static int groupType { get; set; }
        public static bool isKsSql { get; set; }        
        public static bool isRealSql { get; set; }               
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

        public static string centralText(string str)
        {
            int[] arra = new int[255];
            int count = 0;
            int maxLength = 0;
            int indexF = -1;
            arra[count] = 0;
            count++;
            indexF = str.IndexOf("\n");
            arra[count] = indexF;
            while (indexF != -1)
            {
                count++;
                indexF = str.IndexOf("\n", indexF + 1);
                arra[count] = indexF;
            }
            maxLength = arra[1] - arra[0];
            for (int i = 1; i < count; i++)
            {
                if (maxLength < (arra[i] - arra[i - 1]))
                {

                    maxLength = arra[i] - arra[i - 1];
                    if (i >= 2)
                    {
                        maxLength = maxLength - 1;
                    }
                }
            }
            string newString = "";
            string buffString = "";
            for (int i = 1; i < count; i++)
            {
                if (i >= 2)
                {

                    buffString = str.Substring(arra[i - 1] + 1, (arra[i] - arra[i - 1] - 1));
                    buffString = buffString.PadLeft(Convert.ToInt32(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2) * 1.8));
                    //    buffString = buffString.PadRight(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2)*2);
                    newString += buffString + "\n";
                }
                else
                {
                    buffString = str.Substring(arra[i - 1], arra[i]);
                    buffString = buffString.PadLeft(Convert.ToInt32(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2) * 1.8));
                    // buffString = buffString.PadRight(buffString.Length + ((maxLength - (arra[i] - arra[i - 1])) / 2)*2);
                    newString = buffString + "\n";
                }

            }

            return newString;
        }

        public static void DoOnUIThread(MethodInvoker d, Form _this)
        {
            if (_this.InvokeRequired) { _this.Invoke(d); } else { d(); }
        }
    }
}
