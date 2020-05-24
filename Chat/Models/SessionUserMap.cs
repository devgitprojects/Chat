using System;
using System.Collections.Generic;

namespace Chat.Models
{

    public class SimpleSessionUserMap
    {
        public int? UserId { get; set; }
        public int? SessionId { get; set; }
    }
    public partial class SessionUserMap : SimpleSessionUserMap
    {
        public SessionUserMap() { }
        public SessionUserMap(User user, Session session)
        {
            User = user;
            Session = session;
        }

        public int Id { get; set; }
        public bool Isadmin { get; set; }

        public virtual Session Session { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
