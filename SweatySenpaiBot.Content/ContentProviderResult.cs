namespace SweatySenpaiBot.Content
{
    public class ContentProviderResult
    {
        public string? Content { get; }
        public Exception? Exception { get; }

        public ContentProviderResult(string? content, Exception? exception)
        {
            Content = content;
            Exception = exception;
        }

        public static ContentProviderResult FromSuccess(string content)
        { 
            return new ContentProviderResult(content, null);
        }

        public static ContentProviderResult FromException(Exception exception) 
        {
            return new ContentProviderResult(null, exception);
        }
    }
}
