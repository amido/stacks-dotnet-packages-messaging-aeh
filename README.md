# Amido Stacks Messaging Azure EventHub

This library is wrapper around Azure Event Hub.
The main goal is:

    1.) to publish events to an event hub
    2.) to receive events from an event hub

## 1. Registration/Usage

### 1.1 Dependencies
- `Amido.Stacks.Application.CQRS.Events`
- `Amido.Stacks.Configuration`
- `Amido.Stacks.DependencyInjection`
- `Azure.Messaging.EventHubs`
- `Azure.Messaging.EventHubs.Processor`
- `Microsoft.Azure.WebJobs.Extensions.EventHubs`

### 1.2 Currently Supported messages

The library currently supports:
  - publishing and receiving events implementing `Amido.Stacks.Application.CQRS.ApplicationEvents.IApplicationEvent`

### 1.3 Usage in dotnet core application

#### 1.3.1 Event
In this case the `NotifyEvent` has a `NotifyEventHandler`. The handler implements
`Amido.Stacks.Application.CQRS.ApplicationEvents.IApplicationEventHandler<NotifyCommand, bool>` and the command implements
`Amido.Stacks.Application.CQRS.ApplicationEvents.IApplicationEvent` interfaces.

***NotifyEvent.cs***

```cs
   public class NotifyEvent : IApplicationEvent
    {
        public int OperationCode { get; }
        public Guid CorrelationId { get; }
        public int EventCode { get; }

        public NotifyEvent(int operationCode, Guid correlationId, int eventCode)
        {
            OperationCode = operationCode;
            CorrelationId = correlationId;
            EventCode = eventCode;
        }
    }
```

***NotifyEventHandler.cs***

```cs
     public class NotifyEventHandler : IApplicationEventHandler<NotifyEvent>
     {
         private readonly ITestable<NotifyEvent> _testable;

         public NotifyEventHandler(ITestable<NotifyEvent> testable)
         {
             _testable = testable;
         }

         public Task HandleAsync(NotifyEvent applicationEvent)
         {
            _testable.Complete(applicationEvent);
            return Task.CompletedTask;
         }
     }
```
#### 1.3.1.1 EventPublisher Configuration

***appsettings.json***

```json
{
    "EventHubConfiguration": {
        "Publisher": {
            "NamespaceConnectionString": {
                "Identifier": "EVENTHUB_CONNECTIONSTRING",
                "Source": "Environment"
            },
            "EventHubName": "stacks-event-hub"
        },
        "Consumer": {
            "NamespaceConnectionString": {
                "Identifier": "EVENTHUB_CONNECTIONSTRING",
                "Source": "Environment"
            },
            "EventHubName": "stacks-event-hub",
            "BlobStorageConnectionString": {
                "Identifier": "STORAGE_CONNECTIONSTRING",
                "Source": "Environment"
            },
            "BlobContainerName": "stacks-blob-container-name"
        }
    }
}
```
***Usage***
```Startup.cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSecrets();
        services.AddTransient<IApplicationEventPublisher, EventPublisher>();
        services.Configure<EventHubConfiguration>(context.Configuration.GetSection("EventHubConfiguration"));
        services.AddEventHub();
    }
}

public class Consumer
{
    private readonly IApplicationEventPublisher _eventPublisher;

    public Consumer(IApplicationEventPublisher eventPublisher) {
        _eventPublisher = eventPublisher;
    }

    public async Task PublishIt(Data dataToSend)
    {
        // Example usage for an example command
        await _eventPublisher.PublishAsync(new NotifyEvent(... , dataToSend, ...));
    }
}
```
