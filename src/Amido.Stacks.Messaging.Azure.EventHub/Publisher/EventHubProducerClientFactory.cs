using Amido.Stacks.Configuration;
using Amido.Stacks.Messaging.Azure.EventHub.Configuration;
using Azure.Messaging.EventHubs.Producer;
using System.Threading.Tasks;

namespace Amido.Stacks.Messaging.Azure.EventHub.Publisher
{
    public class EventHubProducerClientFactory : IEventHubProducerClientFactory
    {
        public EventHubProducerClientFactory(ISecretResolver<string> secretResolver)
        {
            this.SecretResolver = secretResolver;
        }

        public ISecretResolver<string> SecretResolver { get; }

        public async Task<EventHubProducerClient> CreateEventHubProducerClient(EventHubPublisherConfiguration configuration)
        {
            return new EventHubProducerClient(
                connectionString: await GetConnectionString(configuration.NamespaceConnectionString), 
                eventHubName: configuration.EventHubName);
        }

        private async Task<string> GetConnectionString(Secret connectionStringSecret)
        {
            return await SecretResolver.ResolveSecretAsync(connectionStringSecret);
        }
    }
}
