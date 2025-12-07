using Newtonsoft.Json;
using System;

namespace Umuna.Core.Contracts.Messages
{
    public static class MessageEnvelopeExtensions
    {
        public static MessageEnvelope Create<TPayload>(
            TPayload payload,
            string sender,
            MessageCategory category,
            Guid? correlationId = null)
        {
            return new MessageEnvelope
            {
                MessageTypeName = typeof(TPayload).AssemblyQualifiedName,
                Category = category,
                Sender = sender,
                PayloadJson = JsonConvert.SerializeObject(payload),
                Timestamp = DateTime.UtcNow,
                CorrelationId = correlationId
            };
        }

        public static TPayload? GetPayload<TPayload>(this MessageEnvelope envelope) where TPayload : class
        {
            if (string.IsNullOrEmpty(envelope.PayloadJson))
                return default;

            return JsonConvert.DeserializeObject<TPayload>(envelope.PayloadJson);
        }

        public static object? GetPayload(this MessageEnvelope envelope)
        {
            if (string.IsNullOrEmpty(envelope.PayloadJson) ||
                string.IsNullOrEmpty(envelope.MessageTypeName))
                return null;

            var type = Type.GetType(envelope.MessageTypeName);
            if (type == null)
                throw new InvalidOperationException(
                    $"Cannot deserialize unknown type: {envelope.MessageTypeName}");

            return JsonConvert.DeserializeObject(envelope.PayloadJson, type);
        }
    }
}
