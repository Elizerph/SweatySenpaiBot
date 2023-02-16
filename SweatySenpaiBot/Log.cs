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

        public static void Info(string text, Exception exception)
        {
            _instance.Info(text, exception);
        }

        public static void Error(string text, Exception exception)
        {
            _instance.Error(text, exception);
        }

        public static void Warn(string text)
        {
            _instance.Warn(text);
        }

        public static void Warn(string text, Exception exception)
        {
            _instance.Warn(text, exception);
        }

        public static void Debug(string text)
        {
            _instance.Debug(text);
        }

        public static void Debug(string text, Exception exception)
        {
            _instance.Debug(text, exception);
        }
    }
}
