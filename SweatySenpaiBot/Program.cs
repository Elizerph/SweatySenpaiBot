using ElizerBot;
using ElizerBot.Adapter;
using ElizerBot.Adapter.Triggers;

using ElizerWork;

using log4net.Config;

using SweatySenpaiBot.Content;

using System.Reflection;

namespace SweatySenpaiBot
{
    internal class Program
    {
        private const string TokenVariableName = "discordbottoken";

        static async Task Main(string[] args)
        {
            XmlConfigurator.Configure();
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyVersion = assembly.GetName().Version;
            Log.Info($"Version {assemblyVersion}");

            var token = Environment.GetEnvironmentVariable(TokenVariableName);
            if (string.IsNullOrEmpty(token))
            {
                Log.Info("Token is not set");
                return;
            }

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, _) => cts.Cancel();

            var provider = new HttpContentProvider(TimeSpan.FromSeconds(2))
                .WithAttempts(new[] { 1, 2, 3 }.Select(e => TimeSpan.FromMinutes(e)));
            var worker = new Worker(TimeSpan.FromMinutes(1), () => DateTime.UtcNow);
            var context = new WorkerContext(worker, provider);

            var updateHandler = new TriggerBasedBotUpdateHandler<WorkerContext>(context,
                null,
                new[]
                {
                    new CommandTrigger<WorkerContext>("chatinfo", async (c, a) =>
                    {
                        var message = new NewMessageAdapter(a.Chat)
                        {
                            Text = a.Chat.Id
                        };
                        await a.Bot.SendMessage(message);
                    }),
                    new CommandTrigger<WorkerContext>("getsettings", async (c, a) =>
                    {
                        var message = new NewMessageAdapter(a.Chat)
                        {
                            Attachments = new[]
                            {
                                new FileDescriptorAdapter("Posts.json", async () => WorkerContext.GetPostBundlesStream())
                            }
                        };
                        await a.Bot.SendMessage(message);
                    })
                },
                new[] 
                {
                    new SettingsMessageTrigger(async (c, a) =>
                    {
                        await c.RefreshPostBundles(a.Bot, a.Message.Attachments.First().ReadFile());
                        var message = new NewMessageAdapter(a.Message.Chat)
                        {
                            Text = "New settings accepted"
                        };
                        await a.Bot.SendMessage(message);
                    })
                });
            var bot = updateHandler.BuildAdapter(SupportedMessenger.Discord, token);
            await bot.Init();
            await bot.SetCommands(new Dictionary<string, string>
            {
                { "chatinfo", "Get chat id" },
                { "getsettings", "Get posts settings" }
            });
            await context.RefreshPostBundles(bot, Task.FromResult(WorkerContext.GetPostBundlesStream()));
            await Task.Delay(-1, cts.Token);
        }
    }
}