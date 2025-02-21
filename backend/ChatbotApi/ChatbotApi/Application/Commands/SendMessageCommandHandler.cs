using ChatbotApi.Application.Dtos;
using ChatbotApi.Domain.Entities;
using ChatbotApi.Domain.Interfaces;
using ChatbotIntegration;
using MediatR;

namespace ChatbotApi.Application.Commands
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, SendMessageResponse>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatbotService _chatbotService;

        public SendMessageCommandHandler(IMessageRepository messageRepository, IChatbotService chatbotService)
        {
            _messageRepository = messageRepository;
            _chatbotService = chatbotService;
        }

        public async Task<SendMessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            // Zapisujemy oryginalną wiadomość w bazie
            var message = new Message
            {
                ConversationId = request.ConversationId,
                Text = request.Text,
                CreatedAt = DateTime.UtcNow
            };
            await _messageRepository.AddAsync(message);

            // Generujemy odpowiedź przy pomocy serwisu chatbota
            var botResponse = await _chatbotService.GetResponseAsync(request.Text);

            // Zwracamy odpowiedź
            return new SendMessageResponse
            {
                OriginalMessageId = message.Id,
                ResponseText = botResponse,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
