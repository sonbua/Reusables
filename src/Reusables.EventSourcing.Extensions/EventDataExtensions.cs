using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Reusables.EventSourcing.Extensions
{
    public static class EventDataExtensions
    {
        private const string _EVENT_CLR_TYPE = "EventClrType";

        private static readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.None};

        public static EventData ToEventData<TEvent>(this TEvent @event, string aggregateType, Guid aggregateId, long version)
        {
            var eventId = Guid.NewGuid();
            var data = JsonConvert.SerializeObject(@event, _serializerSettings);
            var eventHeaders = new Dictionary<string, object>
            {
                {
                    _EVENT_CLR_TYPE, @event.GetType().AssemblyQualifiedName
                }
            };
            var metadata = JsonConvert.SerializeObject(eventHeaders, _serializerSettings);

            return new EventData
            {
                Id = eventId,
                Created = DateTime.UtcNow,
                AggregateType = aggregateType,
                AggregateId = aggregateId,
                Version = version,
                Event = data,
                Metadata = metadata,
            };
        }

        public static object FromEventData(this EventData eventData)
        {
            var eventClrTypeName = JObject.Parse(eventData.Metadata).Property(_EVENT_CLR_TYPE).Value;

            return JsonConvert.DeserializeObject(eventData.Event, Type.GetType((string) eventClrTypeName));
        }
    }
}