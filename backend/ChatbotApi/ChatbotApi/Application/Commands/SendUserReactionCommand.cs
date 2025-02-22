using ChatbotApi.Application.Dtos;
using ChatbotApi.Domain.Enums;
using MediatR;

namespace ChatbotApi.Application.Commands
{
    public class SendUserReactionCommand : IRequest<bool>
    {
        public Guid AnswerId { get; set; }
        public Reaction Reaction { get; set; }
    }
}
