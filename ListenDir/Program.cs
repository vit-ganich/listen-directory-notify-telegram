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

                var foldersList = Helper.GetFoldersToListen();

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

                while (true)
                {
                    foreach (var objectDir in objectDirList)
                    {
                        objectDir.RemindAboutNewFile();
                    }
                    Thread.Sleep(ConfigReader.GetSearchTimeout());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
