using ChatApp.Data;
using ChatApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChatApp.Business
{
    public class ChatService : IChatService
    {
        private readonly ChatAppContext _context;

        public ChatService(ChatAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chat>> GetChatsAsync()
        {
            return await _context.Chats.Include(c => c.Messages).ToListAsync();
        }

        public async Task<Chat> GetChatByIdAsync(int id)
        {
            return await _context.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateChatAsync(Chat chat)
        {
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChatAsync(int id, int userId)
        {
            var chat = await _context.Chats.FindAsync(id);
            if (chat == null || chat.CreatedByUserId != userId)
            {
                throw new UnauthorizedAccessException("No permissions to delete this chat.");
            }
            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();
        }
    }

}
