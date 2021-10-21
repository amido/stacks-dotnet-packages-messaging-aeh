using Amido.Stacks.Messaging.Azure.EventHub.Serializers;
using Amido.Stacks.Messaging.Events;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Text;
using Xunit;

namespace Amido.Stacks.Messaging.Azure.EventHub.Tests.Serializers
{
    public class JsonMessageSerializerTests
    {
        [Fact]
        public void GivenTheParametersCorrectTheBodyOfTheApplicationEventWillBeParsed()
        {
            // Arrange
            var parser = new JsonMessageSerializer();
            var correlationId = Guid.NewGuid();
            var menuCreatedEvent = new DummyEvent(101, correlationId);
            var jsonString = JsonConvert.SerializeObject(menuCreatedEvent);
            var byteArray = Encoding.UTF8.GetBytes(jsonString);
            var eventData = new EventData(byteArray);

            // Act
            var result = parser.Read<DummyEvent>(eventData);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType(typeof(DummyEvent));
            result.CorrelationId.ShouldBe(correlationId);
        }
    }
}
