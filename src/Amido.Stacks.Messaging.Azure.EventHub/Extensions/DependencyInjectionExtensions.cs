using Amido.Stacks.Messaging.Azure.EventHub.Configuration;
using Amido.Stacks.Messaging.Azure.EventHub.Consumer;
using Amido.Stacks.Messaging.Azure.EventHub.Publisher;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddEventHub(this IServiceCollection services)
        {
            var configuration = GetConfiguration(services);

            var publishersRegistered = services.AddEventHubPublishers(configuration.Publisher);
            var consumersRegistered = services.AddEventHubConsumers(configuration.Consumer);

            if (!publishersRegistered && !consumersRegistered)
            {
                throw new Exception("Unable to register any publishers or consumers for event hub. Make sure the configuration has been setup correctly.");
            }

            return services;
        }

        private static EventHubConfiguration GetConfiguration(IServiceCollection services)
        {
            var config = services.BuildServiceProvider()
                .GetService<IOptions<EventHubConfiguration>>()
                .Value;

            if (config == null || (config.Publisher == null && config.Consumer == null))
            {
                throw new Exception($"Configuration for '{nameof(IOptions<EventHubConfiguration>)}' not found. Ensure the call to 'service.Configure<{nameof(EventHubConfiguration)}>(configuration)' was called and the appsettings contains at least a definition for Publisher or Consumer. ");
            }

            return config;
        }

        private static bool AddEventHubPublishers(this IServiceCollection services, EventHubPublisherConfiguration configuration)
        {
            if (configuration == null)
            {
                return false;
            }

            services.TryAddTransient<IEventHubProducerClientFactory, EventHubProducerClientFactory>();

            services.AddSingleton(s => new EventHubProducerClient(
                configuration.EventHubNamespaceConnectionString,
                configuration.EventHubName));

            return true;
        }

        private static bool AddEventHubConsumers(this IServiceCollection services, EventHubConsumerConfiguration configuration)
        {
            if (configuration == null)
            {
                return false;
            }


            services.TryAddTransient<IEventProcessorClientFactory, EventProcessorClientFactory>();
            services.AddTransient<IEventConsumer, EventConsumer>();

            //services.AddSingleton(s => new EventProcessorClient(
            //    new BlobContainerClient(configuration.BlobStorageConnectionString, configuration.BlobContainerName),
            //    EventHubConsumerClient.DefaultConsumerGroupName,
            //    configuration.EventHubNamespaceConnectionString,
            //    configuration.EventHubName));

            return true;
        }
    }
}
