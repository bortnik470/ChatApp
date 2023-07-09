using PusherServer;

namespace ChatApp.Modules
{
    public class PusherModule
    {
        private readonly Pusher pusher;

        public PusherModule()
        {
            var options = new PusherOptions
            {
                Cluster = "eu",
            };

            pusher = new Pusher("1604644",
                                "1040badcf112c13f0948",
                                "6aada3100e64a58eb683",
                                options);
        }

        public void sendInfo(string channelName, string eventName, object data)
        {
            var result = pusher.TriggerAsync(channelName, eventName, data);
        }

        public string createChannel(string Name)
        {
            var channelName = "channel-" + Name;

            var result = pusher.TriggerAsync(channelName, "channel-created", new { });

            if (result.IsCompleted)
            {
                return channelName;
            }
            else return "";
        }
    }
}
