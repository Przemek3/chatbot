using ChatbotApi.Domain.Entities;

namespace ChatbotApi.Infra.Interfaces
{
    public interface IQuestionRepository
    {
        Task AddAsync(Question message);
        Task<Question> GetByIdAsync(Guid messageId);
        Task SaveChangesAsync();
    }
}
