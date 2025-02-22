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

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
    }
}
