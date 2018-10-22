using System.IO;


namespace TestResultsReminder
{
    class Logger
    {
        public static void WriteResultToLog(string message)
        {
            var logFolder = ConfigReader.GetResultsLogFolder();
            var logFile = ConfigReader.GetResultsLogFile();

            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(Path.Combine(logFolder, logFile), true))
            {
                file.WriteLine(message);
            }
        }
    }
}
