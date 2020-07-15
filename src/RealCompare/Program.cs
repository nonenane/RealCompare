using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Nwuram.Framework.Project;
using System.Data.OleDb;
using System.Reflection;
using System.IO;
using System.Data;

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
                Project.FillSettings(args);
                //Application.Run(new frmCreateReport());
                Application.Run(new Main());
            }
        }
    }
}
