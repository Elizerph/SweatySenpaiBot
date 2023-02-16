namespace SweatySenpaiBot.Content
{
    public class HttpContentProvider : IContentProvider
    {
        private readonly HttpClient _client;

        public HttpContentProvider(TimeSpan timeout)
        {
            _client = new HttpClient()
            { 
                Timeout = timeout
            };
        }

        public async Task<ContentProviderResult> GetContentAsync(string url)
        {
            try
            {
                var content = await _client.GetStringAsync(url);
                return ContentProviderResult.FromSuccess(content);
            }
            catch (Exception e)
            {
                return ContentProviderResult.FromException(e);
            }
        }
    }
}
