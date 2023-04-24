using SweatySenpaiBot.Content;

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
            var providerResult = await _contentProvider.GetContentAsync(post.Url);
            if (providerResult.Exception != null)
            {
                Log.Warn($"Unable to get content from {post.Title} ({post.Url})", providerResult.Exception);
                return string.Empty;
            }
            var resultParts = new List<string>
            { 
                post.Title
            };
            foreach (var parser in post.Parsers)
            {
                var parserParts = new List<string>();
                var regex = new Regex(parser.Regex);
                var matches = regex.Matches(providerResult.Content).Cast<Match>().ToArray();
                if (matches.Length == 0)
                    Log.Info($"No matches of {post.Title} ({post.Url}) content by {parser.Regex} regex was found");
                else
                {
                    foreach (var match in matches)
                    {
                        var parserMatches = new List<string>();
                        var parserMatchResult = parser.Template;
                        foreach (var key in parser.Keys)
                            parserMatchResult = parserMatchResult.Replace(key, match.Groups[key.Trim(new[] { '<', '>' })].Value);
                        parserParts.Add(parserMatchResult);
                    }
                    resultParts.Add(string.Join(parser.Separator, parserParts.Where(e => !string.IsNullOrEmpty(e))));
                }
            }
            return string.Join(post.Separator, resultParts.Where(e => !string.IsNullOrEmpty(e)));
        }
    }
}
