namespace SweatySenpaiBot.Content
{
    public abstract class ContentProviderDecorator : IContentProvider
    {
        protected readonly IContentProvider _provider;

        public ContentProviderDecorator(IContentProvider provider) 
        {
            _provider = provider;
        }

        public abstract Task<ContentProviderResult> GetContentAsync(string url);
    }
}
