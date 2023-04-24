using log4net;

namespace SweatySenpaiBot
{
    internal static class Log
    {
        public static ILog Instance { get; } = LogManager.GetLogger(typeof(Program));

        public static void Info(string text)
        {
            Instance.Info(text);
        }

        public static void Info(string text, Exception exception)
        {
            Instance.Info(text, exception);
        }

        public static void Error(string text, Exception exception)
        {
            Instance.Error(text, exception);
        }

        public static void Warn(string text)
        {
            Instance.Warn(text);
        }

        public static void Warn(string text, Exception exception)
        {
            Instance.Warn(text, exception);
        }

        public static void Debug(string text)
        {
            Instance.Debug(text);
        }

        public static void Debug(string text, Exception exception)
        {
            Instance.Debug(text, exception);
        }
    }
}
