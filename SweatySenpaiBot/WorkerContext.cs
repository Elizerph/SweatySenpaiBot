using ElizerBot.Adapter;

using ElizerWork;

using Newtonsoft.Json;

namespace SweatySenpaiBot
{
    internal class WorkerContext
    {
        private const string PostBundlesFile = "Data/Posts.json";

        private readonly Worker _worker;
        private readonly IContentProvider _contentProvider;

        public WorkerContext(Worker worker, IContentProvider contentProvider)
        {
            _worker = worker;
            _contentProvider = contentProvider;
        }

        public async Task RefreshPostBundles(IBotAdapter bot, Task<Stream> postBundleStream)
        {
            await _worker.Stop();

            using var stream = await postBundleStream;
            using var reader = new StreamReader(stream);
            var bundlesText = await reader.ReadToEndAsync();
            reader.Close();
            await File.WriteAllTextAsync(PostBundlesFile, bundlesText);
            var bundles = JsonConvert.DeserializeObject<IReadOnlyCollection<PostBundle>>(bundlesText);
            var workItems = bundles?.Select(e => new WorkItem(e.Time, e.Period, async () =>
            {
                var builder = new PostBundleContentBuilder(_contentProvider);
                var text = await builder.Build(e);
                var chat = new ChatAdapter(e.ChatId, false);
                var message = new NewMessageAdapter(chat)
                {
                    Text = text
                };
                await bot.SendMessage(message);
            })).ToArray();

            await _worker.Start(workItems ?? Array.Empty<WorkItem>());
        }

        public static Stream GetPostBundlesStream()
        { 
            return File.OpenRead(PostBundlesFile);
        }
    }
}
