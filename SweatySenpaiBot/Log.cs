using log4net;

namespace SweatySenpaiBot
{
    internal static class Log
    {
        private static readonly ILog _instance = LogManager.GetLogger(typeof(Program));

        public static void Info(string text)
        {
            _instance.Info(text);
        }

        public static void Error(string text, Exception exception)
        {
            _instance.Error(text, exception);
        }
    }
}
