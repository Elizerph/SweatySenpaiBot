namespace SweatySenpaiBot
{
    public class RawContentParser
    {
        public string Regex { get; set; }
        public HashSet<string> Keys { get; set; }
        public string Template { get; set; }
        public string Separator { get; set; }
    }
}
