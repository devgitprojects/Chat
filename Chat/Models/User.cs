using System;
using System.Collections.Generic;

namespace Chat.Models
{
    public partial class User
    {
        public User()
        {
            HiddenMessages = new HashSet<HiddenMessage>();
            Messages = new HashSet<Message>();
            SessionsUsersMap = new HashSet<SessionUserMap>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool? Isactive { get; set; }

        public virtual ICollection<HiddenMessage> HiddenMessages { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<SessionUserMap> SessionsUsersMap { get; set; }
    }
}
