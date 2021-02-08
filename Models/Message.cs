using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesStore.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string MessageBody { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateTime { get; set; }
        public virtual User User { get; set; }
    }
}
