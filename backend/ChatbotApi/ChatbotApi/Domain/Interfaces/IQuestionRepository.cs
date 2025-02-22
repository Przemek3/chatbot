using ChatbotApi.Domain.Entities;

namespace ChatbotApi.Domain.Interfaces
{
    public interface IQuestionRepository
    {
        Task AddAsync(Question message);
        Task<Question> GetByIdAsync(Guid messageId);
        Task SaveChangesAsync();
    }
}
