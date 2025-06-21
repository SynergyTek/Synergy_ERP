using System;

namespace ERP.HRService.Models
{
    public class ChatMessage
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ConversationId { get; set; }
        public string Message { get; set; }
        public string Role { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public virtual Conversation Conversation { get; set; }
    }
} 