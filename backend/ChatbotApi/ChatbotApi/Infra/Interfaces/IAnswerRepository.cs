using ChatbotApi.Domain.Entities;
using ChatbotApi.Domain.Enums;

namespace ChatbotApi.Infra.Interfaces
{
    public interface IAnswerRepository
    {
        Task AddAsync(Answer message);
        Task<Answer> GetByIdAsync(Guid messageId);
        Task SaveChangesAsync();
        Task UpdateReactionAsync(Guid answerId, Reaction reaction);
    }
}
