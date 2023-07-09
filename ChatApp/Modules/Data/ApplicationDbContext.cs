using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Modules.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<ChatModel> Chats { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}