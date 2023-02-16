namespace SweatySenpaiBot.Content
{
    public static class ContentProviderExtension
    {
        public static IContentProvider WithDelay(this IContentProvider source, TimeSpan delay)
        {
            return new DelayedContentProvider(source, delay);
        }

        public static IContentProvider WithAttempts(this IContentProvider source, IEnumerable<TimeSpan> attemptDelays) 
        {
            var providers = new List<IContentProvider>
            { 
                source
            };
            providers.AddRange(attemptDelays.Select(source.WithDelay));
            return new PersistentContentProvider(providers);
        }
    }
}
