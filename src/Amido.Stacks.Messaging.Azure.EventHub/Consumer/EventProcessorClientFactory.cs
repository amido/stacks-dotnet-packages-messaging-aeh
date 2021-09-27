//using Amido.Stacks.Configuration;
//using Amido.Stacks.Messaging.Azure.EventHub.Configuration;
//using Azure.Messaging.EventHubs;
//using Azure.Messaging.EventHubs.Consumer;
//using Azure.Storage.Blobs;
//using System.Threading.Tasks;

//namespace Amido.Stacks.Messaging.Azure.EventHub.Consumer
//{
//    public class EventProcessorClientFactory : IEventProcessorClientFactory
//    {
//        public EventProcessorClientFactory(ISecretResolver<string> secretResolver)
//        {
//            this.SecretResolver = secretResolver;
//        }

//        public ISecretResolver<string> SecretResolver { get; }

//        public async Task<EventProcessorClient> CreateEventProcessorClient(EventHubConsumerConfiguration configuration)
//        {
//            return new EventProcessorClient(
//                checkpointStore: new BlobContainerClient(
//                    connectionString: await GetConnectionString(configuration.BlobStorageConnectionString),
//                    blobContainerName: configuration.BlobContainerName),
//                consumerGroup: EventHubConsumerClient.DefaultConsumerGroupName,
//                connectionString: await GetConnectionString(configuration.NamespaceConnectionString),
//                eventHubName: configuration.EventHubName);
//        }

//        private async Task<string> GetConnectionString(Secret connectionStringSecret)
//        {
//            return await SecretResolver.ResolveSecretAsync(connectionStringSecret);
//        }
//    }
//}
