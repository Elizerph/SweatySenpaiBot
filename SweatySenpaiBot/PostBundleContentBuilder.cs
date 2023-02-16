using SweatySenpaiBot.Content;

namespace SweatySenpaiBot
{
    public class PostBundleContentBuilder
    {
        private readonly IContentProvider _provider;

        public PostBundleContentBuilder(IContentProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public async Task<string> Build(PostBundle postSet)
        {
            var result = new List<string>();
            foreach (var post in postSet.Posts)
            {
                var postBuilder = new PostContentBuilder(_provider);
                var postText = await postBuilder.Build(post);
                result.Add(postText);
            }
            return string.Join(Environment.NewLine, result.Where(e => !string.IsNullOrWhiteSpace(e)));
        }
    }
}
