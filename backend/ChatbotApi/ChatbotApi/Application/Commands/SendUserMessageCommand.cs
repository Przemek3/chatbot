using ChatbotApi.Application.Dtos;
using MediatR;

namespace ChatbotApi.Application.Commands
{
    public class SendUserMessageCommand : IRequest<ChatResponse>
    {
        public Guid? ConversationId { get; set; }
        public string Text { get; set; }
    }
}
