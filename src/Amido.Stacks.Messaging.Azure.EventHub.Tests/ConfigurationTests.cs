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
            config.Publisher.NamespaceConnectionString.Should().NotBeNull();
            config.Publisher.Name.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Ensure_Consumer_Configuration_Is_Parsed_Correctly()
        {
            var config = Config.For<EventHubConfiguration>("EventHub");

            config.Should().NotBeNull();
            config.Consumer.Should().NotBeNull();
            config.Consumer.NamespaceConnectionString.Should().NotBeNull();
            config.Consumer.Name.Should().NotBeNullOrEmpty();
            config.Consumer.BlobStorageConnectionString.Should().NotBeNull();
            config.Consumer.BlobContainerName.Should().NotBeNullOrEmpty();
        }
    }
}
