namespace SweatySenpaiBot
{
    public class PostBundle
    {
        public DateTime Time { get; set; }
        public TimeSpan Period { get; set; }
        public string ChatId { get; set; }
        public IReadOnlyCollection<Post> Posts { get; set; }
    }
}
