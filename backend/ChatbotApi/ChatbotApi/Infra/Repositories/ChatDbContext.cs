using ChatbotApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatbotApi.Infra.Repositories
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
    }
}
