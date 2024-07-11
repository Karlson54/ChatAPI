using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Data.Models
{
    public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Chat> Chats { get; set; }
}

public class Chat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CreatedByUserId { get; set; }
    public User CreatedBy { get; set; }
    public ICollection<Message> Messages { get; set; }
}

public class Message
{
    public int Id { get; set; }
    public int ChatId { get; set; }
    public Chat Chat { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
}
    public class ChatUser
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }

}
