using ChatApp.Business;
using ChatApp.Data.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ChatApp.WebApi
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendMessage(int chatId, int userId, string message)
        {
            var chat = await _chatService.GetChatByIdAsync(chatId);
            if (chat != null)
            {
                var newMessage = new Message
                {
                    ChatId = chatId,
                    UserId = userId,
                    Text = message,
                    Timestamp = DateTime.UtcNow
                };
                chat.Messages.Add(newMessage);
                await _chatService.SaveChangesAsync();

                await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", newMessage);
            }
        }

        public async Task JoinChat(int chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }

        public async Task LeaveChat(int chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}
