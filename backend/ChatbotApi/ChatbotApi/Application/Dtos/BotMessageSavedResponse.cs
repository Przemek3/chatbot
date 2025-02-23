namespace ChatbotApi.Application.Dtos
{
    public class BotMessageSavedResponse
    {
        public Guid BotMessageId { get; set; }
        public Guid OriginalMessageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ConversationId { get; set; }
    }
}
