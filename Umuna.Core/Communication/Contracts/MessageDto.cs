using System;

namespace Umuna.Core.Communication.Contracts
{
    [Serializable]
    public class MessageDto
    {
        public MessageType MessageType { get; set; }
        public string Sender { get; set; }
        public object Payload { get; set; }
        public DateTime Timestamp { get; set; }

        public MessageDto(string messageType, string sender, object payload)
        {
            MessageType = (MessageType)Enum.Parse(typeof(MessageType), messageType, ignoreCase: true);
            Sender = sender;
            Payload = payload;
            Timestamp = DateTime.UtcNow;
        }

        public MessageDto() { }
    }

    [Serializable]
    public class MessageDto<TPayload>
    {
        public MessageType MessageType { get; set; }
        public string Sender { get; set; }
        public TPayload Payload { get; set; }
        public DateTime Timestamp { get; set; }

        public MessageDto(string messageType, string sender, TPayload payload)
        {
            MessageType = (MessageType)Enum.Parse(typeof(MessageType), messageType, ignoreCase: true);
            Sender = sender;
            Payload = payload;
            Timestamp = DateTime.UtcNow;
        }

        public MessageDto() { }
    }
}
