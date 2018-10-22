using System;
using System.Configuration;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace TestResultsReminder
{
    /// <summary>
    /// Class with auxiliary methods
    /// </summary>
    class Helper
    {
        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString(ConfigReader.GetDateTimeFormat());
        }

        public static void DisplayGeneralInfo()
        {
            Console.WriteLine("------------------- Configuration info -------------------");
            Console.WriteLine($"Directory: {ConfigReader.GetTestResultsDir()}");
            Console.WriteLine($"Files extension: {ConfigReader.GetFilesExtension().ToUpper()}");
            Console.WriteLine($"Search timeout: {ConfigReader.GetSearchTimeout()/1000} seconds");
            Console.WriteLine($"Telegram message from user: +{ConfigReader.GetUserPhoneNumber()}");
            Console.Write($"Telegram message to {ConfigReader.GetRecipientType()}: ");
            Console.WriteLine(GetRecipientInfo());
            Console.WriteLine("----------------------------------------------------------\n");
        }

        /// <summary>
        /// Method displays recipient info according to recipient type from config
        /// </summary>
        /// <returns>String recipient name</returns>
        public static string GetRecipientInfo()
        {
            if (ConfigReader.GetRecipientType() == "user" || ConfigReader.GetRecipientType() == "channel")
            {
                return $"'{ConfigReader.GetRecipientName()}'";
            }
            else
            {
                throw new Exception("\nApp.config: invalid value for key['RecipientType'].\nValue must be 'user' or 'channel' only!");
            }
        }

        /// <summary>
        /// Method changes the value of specified key in App.config
        /// </summary>
        /// <param name="newValue">String new value</param>
        /// <param name="key">String key to change</param>
        public static void ChangeValueInConfig(string newValue, string key="CodeFromTelegram")
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = newValue;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// Method takes a Telegram code from console
        /// </summary>
        /// <returns>String 5-digit code</returns>
        public static string ReadTelegramCodeFromConsole()
        {
            Console.WriteLine("Telegram code request for authorization was succesfully sent.");
            Console.WriteLine("You will receive the Telegram code via SMS, please, wait a minute...\n");
            string code = ConfigReader.GetCodeFromTelegram();

            while (true)
            {
                Console.Write("Enter the Telegram authorization code here: ");
                code = Console.ReadLine();

                if (code.Length == 5 && code.All(char.IsDigit))
                {
                    Helper.ChangeValueInConfig(code);
                    break;
                }
                Console.WriteLine("Valid code must have contain five digits. Try again...\n");
            }
            return code;
        }

        /// <summary>
        /// Method gets a string with comma-separated folders paths from App.config
        /// </summary>
        /// <returns>String[] array with folders paths</returns>
        public static string[] GetFoldersToListen()
        {
            return ConfigReader.GetTestResultsDir();
        }

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
