namespace ChatbotApi.Application.Dtos
{
    public class MessageSavedResponse
    {
        public Guid OriginalMessageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ConversationId { get; set; }
    }
}
