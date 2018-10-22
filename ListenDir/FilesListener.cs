using System;
using System.Collections.Generic;
using System.IO;

namespace TestResultsReminder
{
    /// <summary>
    /// Class represents methods for directory listening
    /// </summary>
    class FilesListener
    {
        public  string dir;
        public FilesListener(string directory)
        {
            dir = directory;
        }
        public List<string> initialDirState = new List<string>();

        public string filesExtension = string.Format("*.{0}", ConfigReader.GetFilesExtension());

        /// <summary>
        /// Method gets the files list from the directory before the listening start.
        /// </summary>
        public void GetInitialDirectoryState()
        {
            if (!Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException();
            }
            initialDirState.AddRange(Directory.GetFiles(dir, filesExtension, SearchOption.AllDirectories));
        }

        /// <summary>
        /// Method gets a new files list and compares it with the initial directory state.
        /// If a new file was found, method adds this file to the initial files list.
        /// </summary>
        /// <returns>String name of a new file or empty string</returns>
        public string FindNewFileInDir()
        {
            var newDirState = new List<string>();

            newDirState.AddRange(Directory.GetFiles(dir, filesExtension, SearchOption.AllDirectories));

            foreach (var newFile in newDirState)
            {
                if (!initialDirState.Contains(newFile))
                {
                    initialDirState.Add(newFile);
                    return $"{newFile}";
                }   
            }
            return string.Empty;
        }

        /// <summary>
        /// Method sends a message via Telegram about every new file in directory.
        /// </summary>
        public void RemindAboutNewFile()
        {
            var message = FindNewFileInDir();

            if (message.Length != 0)
            {
                TelegramExtension.SendMessageAsync(message).GetAwaiter().GetResult();
                Logger.WriteResultToLog($"{Helper.GetCurrentTime()}. {message}");
                Console.WriteLine($"{Helper.GetCurrentTime()}. {message}");
            }
            else
            {
                Console.WriteLine($"{Helper.GetCurrentTime()}. No updates");
            }
        }   
    }
}
