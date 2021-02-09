using System;
using System.ComponentModel.DataAnnotations;

namespace MessagesStore.Models
{
    public class Message
    {
        [Key]
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public string MessageBody { get; private set; }
        public string UserName { get; private set; }

        [DataType(DataType.Date)]
        public DateTime? CurrentDate { get; set; }

        public Message()
        {

        }

        public void Initialize(string userId, string message, string userName)
        {
            UserId = userId;
            MessageBody = message;
            UserName = userName;
            CurrentDate = DateTime.Now;
        }
    }
}
