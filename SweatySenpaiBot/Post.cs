namespace SweatySenpaiBot
{
    public class Post
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Separator { get; set; }
        public IReadOnlyCollection<RawContentParser> Parsers { get; set; }
    }
}
