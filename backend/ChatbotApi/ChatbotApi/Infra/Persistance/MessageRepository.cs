using ChatbotApi.Domain.Entities;
using ChatbotApi.Domain.Interfaces;
using ChatbotApi.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatbotApi.Infra.Persistance
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;

        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }

        // Dodaje wiadomość do kontekstu, która później zostanie zapisana w bazie danych.
        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        // Pobiera wiadomość z bazy danych po jej identyfikatorze.
        // Może się kiedyś przyda w razie potrzeby odczytywania historii
        public async Task<Message> GetByIdAsync(Guid messageId)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
        }

        // Zapisuje wszystkie zmiany wprowadzone w kontekście do bazy danych.
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
