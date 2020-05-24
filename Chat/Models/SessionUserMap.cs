using System.Collections.Generic;

namespace Chat.Models
{
    public partial class SessionUserMap : SimpleSessionUserMap
    {
        public SessionUserMap() { }
        public SessionUserMap(User user, Session session, bool isAdmin)
        {
            User = user;
            Session = session;
            IsAdmin = isAdmin;
        }

        public int Id { get; set; }
        public bool IsAdmin { get; set; }

        public virtual Session Session { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
