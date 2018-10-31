using System;
using System.Collections.Generic;
using System.Threading;

namespace TestResultsReminder
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Display.ShowGeneralInfo();

                var foldersList = ConfigReader.GetTestResultsDir();

                TelegramExtension.AuthUserAsync().GetAwaiter().GetResult();

                var objectDirList = new List<FilesListener>();

                foreach (var folder in foldersList)
                {
                    objectDirList.Add(new FilesListener(folder));
                }

                foreach (var objectDir in objectDirList)
                {
                    objectDir.GetInitialDirectoryState();
                }
                Logger.Log.Info("Waiting for new files...");
                while (true)
                {
                    foreach (var objectDir in objectDirList)
                    {
                        objectDir.RemindAboutNewFile();
                    }
                    Thread.Sleep(ConfigReader.GetSearchTimeout());
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex);
            }
        }
    }
}
