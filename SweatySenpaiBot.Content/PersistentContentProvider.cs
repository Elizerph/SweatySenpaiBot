namespace SweatySenpaiBot.Content
{
    public class PersistentContentProvider : IContentProvider
    {
        private readonly IEnumerable<IContentProvider> _providers;

        public PersistentContentProvider(IEnumerable<IContentProvider> providers)
        {
            _providers = providers;
        }

        public async Task<ContentProviderResult> GetContentAsync(string url)
        {
            var exceptions = new List<Exception>();
            foreach (var provider in _providers)
            {
                var result = await provider.GetContentAsync(url);
                if (result.IsSuccess)
                    return result;
                else 
                    exceptions.Add(result.Exception);
            }
            return ContentProviderResult.FromException(new AggregateException(exceptions));
        }
    }
}
