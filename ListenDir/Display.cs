using System;

namespace TestResultsReminder
{
    class Display
    {
        public static void ShowGeneralInfo()
        {
            Logger.Log.Info("Configuration info -------------------");
            ShowFoldersForListen();
            Logger.Log.Info($"Files extension: {ConfigReader.GetFilesExtension().ToUpper()}");
            Logger.Log.Info($"Search timeout: {ConfigReader.GetSearchTimeout() / 1000} seconds");
            Logger.Log.Info($"Telegram message from user: +{ConfigReader.GetUserPhoneNumber()}");
            Logger.Log.Info($"Telegram message to {ConfigReader.GetRecipientType()}: {GetRecipientInfo()}");
            Logger.Log.Info("--------------------------------------\n");
        }

        public static void ShowFoldersForListen()
        {
            Logger.Log.Info("Folders to listen: ");
            foreach (var folder in ConfigReader.GetTestResultsDir())
            {
                Logger.Log.Info(folder + ", ");
            }
        }

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
