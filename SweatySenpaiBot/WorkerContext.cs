using ElizerBot.Adapter;

using ElizerWork;

using Newtonsoft.Json;

namespace SweatySenpaiBot
{
    internal class WorkerContext
    {
        private const string PostBundlesFile = "Data/Posts.json";

        private Worker? _worker;
        private WorkerManager? _manager;
        private readonly TimeSpan _workerBeatPeriod;
        private readonly IContentProvider _contentProvider;

        public WorkerContext(TimeSpan workerBeatPeriod, IContentProvider contentProvider)
        {
            _workerBeatPeriod = workerBeatPeriod;
            _contentProvider = contentProvider;
        }

        public async Task RefreshPostBundles(IBotAdapter bot, Task<Stream> postBundleStream)
        {
            if (_manager != null)
                await _manager.Stop();

            _worker = new Worker(_workerBeatPeriod);
            _manager = new WorkerManager(_worker);

            using var stream = await postBundleStream;
            using var reader = new StreamReader(stream);
            var bundlesText = await reader.ReadToEndAsync();
            reader.Close();
            await File.WriteAllTextAsync(PostBundlesFile, bundlesText);
            var bundles = JsonConvert.DeserializeObject<IReadOnlyCollection<PostBundle>>(bundlesText);
            foreach (var bundle in bundles) 
            {
                _worker.QueueRecurring(bundle.Time, bundle.Period, async () => 
                {
                    var builder = new PostBundleContentBuilder(_contentProvider);
                    var text = await builder.Build(bundle);
                    var chat = new ChatAdapter(bundle.ChatId, false);
                    var message = new NewMessageAdapter(chat)
                    {
                        Text = text
                    };
                    await bot.SendMessage(message);
                });
            }
            _manager.Start();
        }

        public static Stream GetPostBundlesStream()
        { 
            return File.OpenRead(PostBundlesFile);
        }
    }
}
