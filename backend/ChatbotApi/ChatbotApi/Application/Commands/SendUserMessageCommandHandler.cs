using ChatbotApi.Application.Dtos;
using ChatbotApi.Domain.Entities;
using ChatbotIntegration;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using static Azure.Core.HttpHeader;
using System.Security.Cryptography;
using ChatbotApi.Infra.Interfaces;

namespace ChatbotApi.Application.Commands
{
    public class SendUserMessageCommandHandler : IRequestHandler<SendUserMessageCommand, ChatResponse>
    {
        private readonly IQuestionRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IChatbotService _chatbotService;

        public SendUserMessageCommandHandler(IQuestionRepository messageRepository, IConversationRepository conversationRepository, IChatbotService chatbotService)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
            _chatbotService = chatbotService;
        }

        public async Task<ChatResponse> Handle(SendUserMessageCommand request, CancellationToken cancellationToken)
        {
            var conversationId = request.ConversationId;
            if (conversationId == null)
            {
                var conversation = await _conversationRepository.CreateAsync();
                conversationId = conversation.Id;
            }
            
            // Zapisujemy oryginalną wiadomość w bazie
            var message = new Question
            {
                ConversationId = conversationId ?? new Guid(),
                Text = request.Text,
                CreatedAt = DateTime.UtcNow
            };
            await _messageRepository.AddAsync(message);

            // Generujemy odpowiedź przy pomocy serwisu chatbota
            var botResponse = await _chatbotService.GetResponseAsync(request.Text);

            // Zwracamy odpowiedź
            return new ChatResponse
            {
                UserMessageId = message.Id,
                ResponseText = botResponse,
                CreatedAt = DateTime.UtcNow,
                ConversationId = message.ConversationId
            };
        }
    }
}
