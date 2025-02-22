using ChatbotApi.Application.Dtos;
using MediatR;

namespace ChatbotApi.Application.Commands
{
    public class SendChatMessageCommand : IRequest<MessageSavedResponse>
    {
        public Guid ConversationId { get; set; }
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
    }
}
