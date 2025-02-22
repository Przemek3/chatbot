using ChatbotApi.Domain.Enums;

namespace ChatbotApi.Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ConversationId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
