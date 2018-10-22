using System;
using System.Linq;

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
                    ConfigReader.ChangeValueInConfig(code);
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
    }
}
