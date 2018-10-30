using log4net;


namespace TestResultsReminder
{
    class Logger
    {
        public static readonly ILog Log
            = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
