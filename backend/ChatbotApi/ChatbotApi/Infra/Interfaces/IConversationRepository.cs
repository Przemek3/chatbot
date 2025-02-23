using ChatbotApi.Domain.Entities;

namespace ChatbotApi.Infra.Interfaces
{
    public interface IConversationRepository
    {
        Task<Conversation> GetByIdAsync(Guid conversationId);
        Task<IEnumerable<Conversation>> GetAllAsync();
        Task<Conversation> CreateAsync();
        Task AddAsync(Conversation conversation);
        Task UpdateAsync(Conversation conversation);
        Task DeleteAsync(Guid conversationId);
    }
}
