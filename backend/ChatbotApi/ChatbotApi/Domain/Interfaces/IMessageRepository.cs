using ChatbotApi.Domain.Entities;

namespace ChatbotApi.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
        Task<Message> GetByIdAsync(Guid messageId);
        Task SaveChangesAsync();
    }
}
