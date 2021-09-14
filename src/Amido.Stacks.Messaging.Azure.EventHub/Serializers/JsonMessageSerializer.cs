using System.Text;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

namespace Amido.Stacks.Messaging.Azure.EventHub.Serializers
{
    public class JsonMessageSerializer : IMessageReader
    {
        public T Read<T>(EventData eventData)
        {
            var jsonBody = Encoding.UTF8.GetString(eventData.Body.Array);
            return JsonConvert.DeserializeObject<T>(jsonBody);
        }
    }
}
