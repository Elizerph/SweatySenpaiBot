namespace SweatySenpaiBot.Content
{
    public interface IContentProvider
    {
        Task<ContentProviderResult> GetContentAsync(string url);
    }
}
