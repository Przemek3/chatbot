using ChatbotApi.Application.Dtos;
using ChatbotApi.Domain.Entities;
using ChatbotApi.Infra.Interfaces;
using ChatbotIntegration;
using MediatR;

namespace ChatbotApi.Application.Commands
{
    public class SendChatMessageCommandHandler : IRequestHandler<SendChatMessageCommand, BotMessageSavedResponse>
    {
        private readonly IAnswerRepository _chatRepository;

        public SendChatMessageCommandHandler(IAnswerRepository messageRepository)
        {
            _chatRepository = messageRepository;
        }

        public async Task<BotMessageSavedResponse> Handle(SendChatMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Answer
            {
                QuestionId = request.QuestionId,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow
            };
            // Zapisujemy wiadomość w bazie
            await _chatRepository.AddAsync(message);

            // Zwracamy odpowiedź
            return new BotMessageSavedResponse
            {
                BotMessageId = message.Id,
                OriginalMessageId = request.QuestionId,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
