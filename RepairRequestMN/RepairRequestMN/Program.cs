using System;
using System.Diagnostics;
using System.Windows.Forms;
using Nwuram.Framework.Project;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Logging;
using Nwuram.Framework.Settings.Connection;

namespace RepairRequestMN
{
  internal static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
      if (args.Length > 0)
        Project.FillSettings(args);

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Logging.Init(ConnectionSettings.GetServer(), ConnectionSettings.GetDatabase(), ConnectionSettings.GetUsername(), ConnectionSettings.GetPassword(), ConnectionSettings.ProgramName);
      Logging.StartFirstLevel(1);
      Logging.Comment("Вход в программу");
      Logging.StopFirstLevel();
      
      Application.Run(new CreateRequest());
      
      Logging.StartFirstLevel(2);
      Logging.Comment("Выход из программы");
      Logging.StopFirstLevel();
      Project.clearBufferFiles();
    }
  }
}