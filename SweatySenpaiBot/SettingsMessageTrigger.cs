using ElizerBot.Adapter.Triggers;

using Newtonsoft.Json;

namespace SweatySenpaiBot
{
    internal class SettingsMessageTrigger : MessageTrigger<WorkerContext>
    {
        public SettingsMessageTrigger(Func<WorkerContext, MessageTriggerArgument, Task> action) 
            : base(action)
        {
        }

        public override async Task<bool> Validate(MessageTriggerArgument arg)
        {
            var attachments = arg.Message.Attachments;
            if (attachments != null && attachments.Count == 1)
            {
                var attachment = attachments.First();
                try
                {
                    using var stream = await attachment.ReadFile();
                    using var reader = new StreamReader(stream);
                    var text = await reader.ReadToEndAsync();
                    var postSets = JsonConvert.DeserializeObject<IReadOnlyCollection<PostBundle>>(text);
                    return postSets != null && postSets.Any();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
