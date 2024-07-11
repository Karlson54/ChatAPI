using ChatApp.Data.Models;

namespace ChatApp.Business
{
    public interface IChatService
    {
        Task<IEnumerable<Chat>> GetChatsAsync();
        Task<Chat> GetChatByIdAsync(int id);
        Task CreateChatAsync(Chat chat);
        Task DeleteChatAsync(int id, int userId);
        Task SaveChangesAsync();
    }

}
