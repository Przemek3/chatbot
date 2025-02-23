namespace ChatbotApi.Application.Dtos
{
    public class ChatResponse
    {
        public Guid UserMessageId { get; set; }
        public string ResponseText { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ConversationId { get; set; }
    }
}
