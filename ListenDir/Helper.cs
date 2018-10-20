using System;
using System.Configuration;
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
            return DateTime.Now.ToString(ConfigManager.DateTimeFormat);
        }

        public static void DisplayGeneralInfo()
        {
            Console.WriteLine("------------------- Configuration info -------------------");
            Console.WriteLine($"Directory: {ConfigManager.TestResultsDir}");
            Console.WriteLine($"Files extension: {ConfigManager.FilesExtension.ToUpper()}");
            Console.WriteLine($"Search timeout: {ConfigManager.SearchTimeout/1000} seconds");
            Console.WriteLine($"Telegram message from user: +{ConfigManager.UserPhoneNumber}");
            Console.Write($"Telegram message to {ConfigManager.RecipientType}: ");
            Console.WriteLine(GetRecipientInfo());
            Console.WriteLine("----------------------------------------------------------\n");
        }

        /// <summary>
        /// Method displays recipient info according to recipient type from config
        /// </summary>
        /// <returns>String recipient name</returns>
        public static string GetRecipientInfo()
        {
            if (ConfigManager.RecipientType == "user" || ConfigManager.RecipientType == "channel")
            {
                return $"'{ConfigManager.RecipientName}'";
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
            string code = ConfigManager.CodeFromTelegram;

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
            return ConfigManager.TestResultsDir.Split(',');
        }
    }
}
