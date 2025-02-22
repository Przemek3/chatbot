using ChatbotApi.Domain.Entities;
using ChatbotApi.Domain.Enums;
using ChatbotApi.Domain.Interfaces;
using ChatbotApi.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatbotApi.Infra.Persistance
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ChatDbContext _context;

        public AnswerRepository(ChatDbContext context)
        {
            _context = context;
        }

        // Dodaje wiadomość do kontekstu, która później zostanie zapisana w bazie danych.
        public async Task AddAsync(Answer message)
        {
            await _context.Answers.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        // Pobiera wiadomość z bazy danych po jej identyfikatorze.
        // Może się kiedyś przyda w razie potrzeby odczytywania historii
        public async Task<Answer> GetByIdAsync(Guid messageId)
        {
            return await _context.Answers.FirstOrDefaultAsync(m => m.Id == messageId);
        }

        // Zapisuje wszystkie zmiany wprowadzone w kontekście do bazy danych.
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReactionAsync(Guid answerId, Reaction reaction)
        {
            var existingAnswer = await _context.Answers.FindAsync(answerId);
            if (existingAnswer == null)
            {
                throw new KeyNotFoundException($"Answer with ID {answerId} not found.");
            }

            existingAnswer.Reaction = reaction;

            _context.Answers.Update(existingAnswer);
            await _context.SaveChangesAsync();
        }

    }
}
