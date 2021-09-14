using Amido.Stacks.Application.CQRS.Events;
using Amido.Stacks.Messaging.Azure.EventHub.Serializers;
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
            var menuCreatedEvent = new MenuCreatedEvent(101, correlationId, new Guid("C8A14B73F2A14696BEEFBD432AF27296"));
            var jsonString = JsonConvert.SerializeObject(menuCreatedEvent);
            var byteArray = Encoding.UTF8.GetBytes(jsonString);
            var eventData = new EventData(byteArray);

            // Act
            var result = parser.Read<MenuCreatedEvent>(eventData);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType(typeof(MenuCreatedEvent));
            result.CorrelationId.ShouldBe(correlationId);
        }
    }
}
