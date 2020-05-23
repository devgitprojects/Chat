using System;
using System.Collections.Generic;

namespace Chat.Models
{
    public partial class Message
    {
        public Message()
        {
            HiddenMessages = new HashSet<HiddenMessage>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int? UserId { get; set; }
        public int? SessionId { get; set; }
        public bool Ishidden { get; set; }

        public virtual Session Session { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<HiddenMessage> HiddenMessages { get; set; }
    }
}
