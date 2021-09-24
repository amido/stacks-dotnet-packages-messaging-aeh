using Amido.Stacks.Configuration;

namespace Amido.Stacks.Messaging.Azure.EventHub.Configuration
{
    public class EventHubConsumerConfiguration : EventHubEntityConfiguration
    {
        public Secret BlobStorageConnectionString { get; set; }

        public string BlobContainerName { get; set; }
    }
}
