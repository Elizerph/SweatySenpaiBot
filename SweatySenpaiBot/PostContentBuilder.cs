using System.Text.RegularExpressions;

namespace SweatySenpaiBot
{
    public class PostContentBuilder
    {
        private readonly IContentProvider _contentProvider;

        public PostContentBuilder(IContentProvider contentProvider) 
        { 
            _contentProvider = contentProvider;
        }

        public async Task<string> Build(Post post)
        {
            var rawContent = await _contentProvider.GetContent(post.Url);
            var resultParts = new List<string>
            { 
                post.Title
            };
            foreach (var parser in post.Parsers)
            {
                var parserParts = new List<string>();
                var regex = new Regex(parser.Regex);
                foreach (var match in regex.Matches(rawContent).Cast<Match>())
                {
                    var parserMatches = new List<string>();
                    var parserMatchResult = parser.Template;
                    foreach (var key in parser.Keys)
                        parserMatchResult = parserMatchResult.Replace(key, match.Groups[key.Trim(new[] { '<', '>' })].Value);
                    parserParts.Add(parserMatchResult);
                }
                resultParts.Add(string.Join(parser.Separator, parserParts.Where(e => !string.IsNullOrEmpty(e))));
            }
            return string.Join(post.Separator, resultParts.Where(e => !string.IsNullOrEmpty(e)));
        }
    }
}
