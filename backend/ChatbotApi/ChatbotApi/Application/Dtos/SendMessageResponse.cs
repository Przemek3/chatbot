namespace ChatbotApi.Application.Dtos
{
    public class SendMessageResponse
    {
        public Guid OriginalMessageId { get; set; }
        public string ResponseText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
