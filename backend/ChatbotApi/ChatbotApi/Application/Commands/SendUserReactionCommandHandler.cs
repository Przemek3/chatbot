using ChatbotApi.Application.Dtos;
using ChatbotApi.Domain.Interfaces;
using MediatR;

namespace ChatbotApi.Application.Commands
{
    public class SendUserReactionCommandHandler : IRequestHandler<SendUserReactionCommand, bool>
    {
        private readonly IAnswerRepository _chatRepository;

        public SendUserReactionCommandHandler(IAnswerRepository messageRepository)
        {
            _chatRepository = messageRepository;
        }

        public async Task<bool> Handle(SendUserReactionCommand request, CancellationToken cancellationToken)
        {

            await _chatRepository.UpdateReactionAsync(request.AnswerId, request.Reaction);

            return true;
        }
    }
}
