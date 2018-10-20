using System;
using System.Configuration;


namespace TestResultsReminder
{
    /// <summary>
    /// Method for interaction with App.config
    /// </summary>
    class ConfigManager
    {
        #region "New test results search parameters"
        public static string FilesExtension = ConfigurationManager.AppSettings["FilesExtension"];
        public static string TestResultsDir = ConfigurationManager.AppSettings["TestResultsDir"];
        public static string DateTimeFormat = ConfigurationManager.AppSettings["DateTimeFormat"];
        public static int SearchTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["NewFilesSearchTimeout"]) * 1000;
        #endregion

        #region "Telegram API extension parameters"
        public static int ApiId = Convert.ToInt32(ConfigurationManager.AppSettings["ApiId"]);
        public static string ApiHash = ConfigurationManager.AppSettings["ApiHash"];
        public static string UserPhoneNumber = ConfigurationManager.AppSettings["UserPhoneNumber"];
        public static string CodeFromTelegram = ConfigurationManager.AppSettings["CodeFromTelegram"];
        public static string RecipientType = ConfigurationManager.AppSettings["RecipientType"];
        public static string RecipientName = ConfigurationManager.AppSettings["RecipientName"];
        #endregion
    }
}
