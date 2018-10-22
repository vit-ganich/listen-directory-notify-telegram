using System;

namespace TestResultsReminder
{
    class Display
    {
        public static void ShowGeneralInfo()
        {
            Console.WriteLine("------------------- Configuration info -------------------");
            ShowFoldersForListen();
            Console.WriteLine($"Files extension: {ConfigReader.GetFilesExtension().ToUpper()}");
            Console.WriteLine($"Search timeout: {ConfigReader.GetSearchTimeout() / 1000} seconds");
            Console.WriteLine($"Telegram message from user: +{ConfigReader.GetUserPhoneNumber()}");
            Console.Write($"Telegram message to {ConfigReader.GetRecipientType()}: ");
            Console.WriteLine(GetRecipientInfo());
            Console.WriteLine("----------------------------------------------------------\n");
        }

        public static void ShowFoldersForListen()
        {
            Console.Write("Folders to listen: ");
            foreach (var folder in ConfigReader.GetTestResultsDir())
            {
                Console.Write(folder + ", ");
            }
            Console.WriteLine();
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
    }
}
