using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatMessageServer.Database;
using ChatMessageServer.Model;
using Microsoft.EntityFrameworkCore;

namespace ChatMessageServer.Repository
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        public async Task<ChatMessage> InsertAsync(ChatMessage message)
        {
            await using var context = new Context();
            await context.ChatMessages.AddAsync(message);
            await context.SaveChangesAsync();
            return message;
        }
        
        public async Task<ChatMessage> GetAsync(int id)
        {
            using (var context = new Context())
            {
                var message = await context.ChatMessages.FirstOrDefaultAsync(c => c.Id == id);
                return message;
            }
        }
        
        public async Task<List<ChatMessage>> GetNewMessagesAsync(int id)
        {
            using (var context = new Context())
            {
                var messages = await context.ChatMessages.Where(c => c.Id > id).ToListAsync();
                return messages;
            }
        }
    }

    public interface IChatMessageRepository
    {
        Task<ChatMessage> InsertAsync(ChatMessage message);

        Task<ChatMessage> GetAsync(int id);

        Task<List<ChatMessage>> GetNewMessagesAsync(int id);
    }
}