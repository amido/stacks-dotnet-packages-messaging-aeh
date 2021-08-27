namespace Amido.Stacks.Messaging.Azure.EventHub.Configuration
{
    public class EventHubConsumerConfiguration
    {
        public string EventHubNamespaceConnectionString { get; set; }

        public string EventHubName { get; set; }

        public string BlobStorageConnectionString { get; set; }

        public string BlobContainerName { get; set; }
    }
}
