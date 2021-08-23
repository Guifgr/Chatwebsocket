using Microsoft.EntityFrameworkCore;
using SimpleChatSolution.Model;

namespace SimpleChatSolution.Database
{
    public class Context : DbContext
    {
        public Context()
        {
            Database.EnsureCreated();
        }

        public DbSet<ChatMessage> ChatMessages {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=Messages.db");
        }
    }
}
