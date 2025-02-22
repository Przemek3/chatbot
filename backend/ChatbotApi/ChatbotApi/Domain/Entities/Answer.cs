using ChatbotApi.Domain.Enums;

namespace ChatbotApi.Domain.Entities
{
    public class Answer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid QuestionId { get; set; } = Guid.Empty;
        public Guid ConversationId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Reaction Reaction { get; set; }
    }
}
