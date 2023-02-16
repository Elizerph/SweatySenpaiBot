namespace SweatySenpaiBot.Content
{
    public class ContentProviderResult
    {
        public bool IsSuccess { get; }
        public string? Content { get; }
        public Exception? Exception { get; }

        public ContentProviderResult(bool isSuccess, string? content = null, Exception? exception = null)
        {
            IsSuccess = isSuccess;
            Content = content;
            Exception = exception;
        }

        public static ContentProviderResult FromSuccess(string content)
        { 
            return new ContentProviderResult(true, content);
        }

        public static ContentProviderResult FromException(Exception exception) 
        {
            return new ContentProviderResult(false, null, exception);
        }
    }
}
