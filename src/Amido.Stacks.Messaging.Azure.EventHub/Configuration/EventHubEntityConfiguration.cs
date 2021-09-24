using Amido.Stacks.Configuration;

namespace Amido.Stacks.Messaging.Azure.EventHub.Configuration
{
    public abstract class EventHubEntityConfiguration
    {
        public Secret NamespaceConnectionString { get; set; }

        /// <summary>
        /// Name of the Event Hub
        /// </summary>
        public string Name { get; set; }
    }
}
