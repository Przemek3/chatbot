using ChatbotApi.Application.Dtos;
using MediatR;

namespace ChatbotApi.Application.Commands
{
    public class SendChatMessageCommand : IRequest<BotMessageSavedResponse>
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
    }
}
