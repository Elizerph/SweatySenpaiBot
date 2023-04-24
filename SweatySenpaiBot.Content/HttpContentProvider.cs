using log4net;

namespace SweatySenpaiBot.Content
{
    public class HttpContentProvider : IContentProvider
    {
        private readonly HttpClient _client;
        private readonly ILog? _log;

        public HttpContentProvider(TimeSpan timeout, ILog? log = null)
        {
            _client = new HttpClient()
            { 
                Timeout = timeout
            };
            _log = log;
        }

        public async Task<ContentProviderResult> GetContentAsync(string url)
        {
            try
            {
                _log?.Debug($"{url}: Requesting...");
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                _log?.Debug($"{url}: Response length: {content.Length}");
                return ContentProviderResult.FromSuccess(content);
            }
            catch (Exception e)
            {
                return ContentProviderResult.FromException(e);
            }
        }
    }
}
