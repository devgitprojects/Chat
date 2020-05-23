using System;
using System.Collections.Generic;

namespace Chat.Models
{
    public partial class Session
    {
        public Session()
        {
            Messages = new HashSet<Message>();
            SessionsUsersMap = new HashSet<SessionUserMap>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<SessionUserMap> SessionsUsersMap { get; set; }
    }
}
