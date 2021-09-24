using Amido.Stacks.Messaging.Azure.EventHub.Configuration;
using Azure.Messaging.EventHubs;
using System.Threading.Tasks;

namespace Amido.Stacks.Messaging.Azure.EventHub.Consumer
{
    public interface IEventProcessorClientFactory
    {
        Task<EventProcessorClient> CreateEventProcessorClient(EventHubConsumerConfiguration configuration);
    }
}
