using System;
using System.Configuration;


namespace TestResultsReminder
{
    /// <summary>
    /// Method for interaction with App.config
    /// </summary>
    class ConfigReader
    {
        #region "New test results search parameters"
        public static string GetFilesExtension()
        {
            return ConfigurationManager.AppSettings ["FilesExtension"];
        }

        public static string[] GetTestResultsDir()
        {
            return ConfigurationManager.AppSettings["TestResultsDir"].Split(',');
        }

        public static string GetDateTimeFormat()
        {
            return ConfigurationManager.AppSettings["DateTimeFormat"];
        }
        public static int GetSearchTimeout()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["NewFilesSearchTimeout"]) * 1000;
        }

        public static string GetResultsLogFolder()
        {
            return ConfigurationManager.AppSettings["ResultsLogFolder"];
        }
        public static string GetResultsLogFile()
        {
            return ConfigurationManager.AppSettings["ResultsLogFile"];
        }
        #endregion

        #region "Telegram API extension parameters"
        public static int GetApiId()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["ApiId"]);
        }
        public static string GetApiHash() {
            return ConfigurationManager.AppSettings["ApiHash"];
        }
        public static string GetUserPhoneNumber()
        {
            return ConfigurationManager.AppSettings["UserPhoneNumber"];
        }
        public static string GetCodeFromTelegram()
        {
            return ConfigurationManager.AppSettings["CodeFromTelegram"];
        }
        public static string GetRecipientType()
        {
            return ConfigurationManager.AppSettings["RecipientType"];
        }
        public static string GetRecipientName() {
            return ConfigurationManager.AppSettings["RecipientName"];
            }
        #endregion
    }
}
