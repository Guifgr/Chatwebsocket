using ChatMessageServer.Model;
using Microsoft.EntityFrameworkCore;

namespace ChatMessageServer.Database
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
