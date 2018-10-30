using System;
using System.Linq;

namespace TestResultsReminder
{
    /// <summary>
    /// Class with auxiliary methods
    /// </summary>
    class Helper
    {
        /// <summary>
        /// Method takes a Telegram code from console
        /// </summary>
        /// <returns>String 5-digit code</returns>
        public static string ReadTelegramCodeFromConsole()
        {
            Logger.Log.Info("Telegram code request for authorization was succesfully sent.");
            Logger.Log.Info("You will receive the Telegram code via SMS, please, wait a minute...\n");

            string code = "";

            while (true)
            {
                Logger.Log.Info("Enter the Telegram authorization code.");
                Console.Write("Code: ");
                code = Console.ReadLine();

                if (code.Length == 5 && code.All(char.IsDigit)) { break; }

                Logger.Log.Info("Valid code must have contain five digits. Try again...\n");
            }
            return code;
        }
        public static string GetCurrentTime()
        {
            return DateTime.Now.ToString(ConfigReader.GetDateTimeFormat());
        }
    }
}
