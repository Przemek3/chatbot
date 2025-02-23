using ChatbotApi.Domain.Entities;
using ChatbotApi.Infra.Interfaces;
using ChatbotApi.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatbotApi.Infra.Persistance
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ChatDbContext _context;

        public ConversationRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation> GetByIdAsync(Guid conversationId)
        {
            return await _context.Conversations.FirstOrDefaultAsync(c => c.Id == conversationId);
        }

        public async Task<IEnumerable<Conversation>> GetAllAsync()
        {
            return await _context.Conversations.ToListAsync();
        }

        public async Task<Conversation> CreateAsync()
        {
            var conversation = new Conversation();
            await _context.Conversations.AddAsync(conversation);
            await _context.SaveChangesAsync();
            return conversation;
        }

        public async Task AddAsync(Conversation conversation)
        {
            await _context.Conversations.AddAsync(conversation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Conversation conversation)
        {
            _context.Conversations.Update(conversation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid conversationId)
        {
            var conversation = await GetByIdAsync(conversationId);
            if (conversation != null)
            {
                _context.Conversations.Remove(conversation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
