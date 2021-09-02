using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Amido.Stacks.Testing.Settings;
using Config = Amido.Stacks.Testing.Settings.Configuration;
using Amido.Stacks.Messaging.Azure.EventHub.Configuration;
using FluentAssertions;

namespace Amido.Stacks.Messaging.Azure.EventHub.Tests
{
    public class  ConfigurationTests
    {
        [Fact]
        public void Ensure_Publisher_Configuration_Is_Parsed_Correctly()
        {
            var config = Config.For<EventHubConfiguration>("EventHub");

            config.Should().NotBeNull();
            config.Publisher.Should().NotBeNull();
            config.Publisher.EventHubNamespaceConnectionString.Should().NotBeNullOrEmpty();
            config.Publisher.EventHubName.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Ensure_Consumer_Configuration_Is_Parsed_Correctly()
        {
            var config = Config.For<EventHubConfiguration>("EventHub");

            config.Should().NotBeNull();
            config.Consumer.Should().NotBeNull();
            config.Consumer.EventHubNamespaceConnectionString.Should().NotBeNullOrEmpty();
            config.Consumer.EventHubName.Should().NotBeNullOrEmpty();
            config.Consumer.BlobStorageConnectionString.Should().NotBeNullOrEmpty();
            config.Consumer.BlobContainerName.Should().NotBeNullOrEmpty();
        }
    }
}
