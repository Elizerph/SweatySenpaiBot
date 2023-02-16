namespace SweatySenpaiBot.Content
{
    public class DelayedContentProvider : ContentProviderDecorator
    {
        private readonly TimeSpan _delay;

        public DelayedContentProvider(IContentProvider provider, TimeSpan delay) 
            : base(provider)
        {
            _delay = delay;
        }

        public override async Task<ContentProviderResult> GetContentAsync(string url)
        {
            await Task.Delay(_delay);
            return await _provider.GetContentAsync(url);
        }
    }
}
