namespace SweatySenpaiBot
{
    public class HttpContentProvider : IContentProvider
    {
        private readonly HttpClient _client = new();
        private readonly TimeSpan _delay;

        public HttpContentProvider(TimeSpan delay)
        { 
            _delay = delay;
        }

        public async Task<string> GetContent(string url)
        {
            await Task.Delay(_delay);
            return await _client.GetStringAsync(url);
        }
    }
}
