using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Nwuram.Framework.Project;
using System.Data.OleDb;
using System.Reflection;
using System.IO;
using System.Data;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;

namespace RealCompare
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0)
            {
                if (Project.FillSettings(args))
                {
                    Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);

                    Logging.StartFirstLevel(1);
                    Logging.Comment("Вход в программу");
                    Logging.StopFirstLevel();

                    Application.Run(new Main());

                    Logging.StartFirstLevel(2);
                    Logging.Comment("Выход из программы");
                    Logging.StopFirstLevel();

                    Nwuram.Framework.Project.Project.clearBufferFiles();
                }
            }
        }
    }
}
