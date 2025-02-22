using ChatbotApi.Domain.Entities;
using ChatbotApi.Domain.Interfaces;
using ChatbotApi.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatbotApi.Infra.Persistance
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ChatDbContext _context;

        public QuestionRepository(ChatDbContext context)
        {
            _context = context;
        }

        // Dodaje wiadomość do kontekstu, która później zostanie zapisana w bazie danych.
        public async Task AddAsync(Question message)
        {
            await _context.Questions.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        // Pobiera wiadomość z bazy danych po jej identyfikatorze.
        // Może się kiedyś przyda w razie potrzeby odczytywania historii
        public async Task<Question> GetByIdAsync(Guid messageId)
        {
            return await _context.Questions.FirstOrDefaultAsync(m => m.Id == messageId);
        }

        // Zapisuje wszystkie zmiany wprowadzone w kontekście do bazy danych.
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
