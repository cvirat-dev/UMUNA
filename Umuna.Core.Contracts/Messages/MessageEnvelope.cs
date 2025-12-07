using System;

namespace Umuna.Core.Contracts.Messages
{
    /// <summary>
    /// Envelope for all IPC messages. Handles serialization and routing.
    /// </summary>
    [Serializable]
    public class MessageEnvelope
    {
        /// <summary>
        /// Fully qualified type name of the payload (e.g., "MyApp.Core.Contracts.Messages.Commands.MovePlayerCommand")
        /// </summary>
        public string MessageTypeName { get; set; } = string.Empty;

        /// <summary>
        /// Category for routing/filtering (Command, Event, Query)
        /// </summary>
        public MessageCategory Category { get; set; }

        /// <summary>
        /// Identifier of the sender process/component
        /// </summary>
        public string Sender { get; set; } = string.Empty;

        /// <summary>
        /// JSON-serialized payload
        /// </summary>
        public string PayloadJson { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Optional: correlation ID for request/response tracking
        /// </summary>
        public Guid? CorrelationId { get; set; }

        public MessageEnvelope()
        {
            Timestamp = DateTime.UtcNow;
        }
    }

    public enum MessageCategory
    {
        Command,   // Request to do something
        Event,     // Something happened (notification)
        Query,     // Request for data
        Response   // Reply to a Query
    }
}
