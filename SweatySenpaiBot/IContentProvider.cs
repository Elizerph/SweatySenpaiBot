namespace SweatySenpaiBot
{
    public interface IContentProvider
    {
        Task<string> GetContent(string url);
    }
}
