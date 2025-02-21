namespace ChatbotApi.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ConversationId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
