using ChatbotApi.Application.Dtos;
using MediatR;

namespace ChatbotApi.Application.Commands
{
    public class SendMessageCommand : IRequest<SendMessageResponse>
    {
        public Guid ConversationId { get; set; }
        public string Text { get; set; }
    }
}
