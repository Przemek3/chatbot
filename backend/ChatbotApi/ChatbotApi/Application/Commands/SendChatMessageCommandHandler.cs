using ChatbotApi.Application.Dtos;
using ChatbotApi.Domain.Entities;
using ChatbotApi.Domain.Interfaces;
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
            var conversationId = request.ConversationId;

            // Zapisujemy oryginalną wiadomość w bazie
            var message = new Answer
            {
                ConversationId = conversationId,
                QuestionId = request.QuestionId,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow
            };
            await _chatRepository.AddAsync(message);

            // Generujemy odpowiedź przy pomocy serwisu chatbota
            //var botResponse = await _chatbotService.GetResponseAsync(request.Text);

            // Zwracamy odpowiedź
            return new BotMessageSavedResponse
            {
                BotMessageId = message.Id,
                OriginalMessageId = request.QuestionId,
                CreatedAt = DateTime.UtcNow,
                ConversationId = message.ConversationId
            };
        }
    }
}
