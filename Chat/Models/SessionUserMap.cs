using System.Collections.Generic;

namespace Chat.Models
{
    public partial class SessionUserMap : BaseModel
    {
        public SessionUserMap() { }
        public SessionUserMap(User user, Session session, bool isAdmin)
        {
            User = user;
            Session = session;
            IsAdmin = isAdmin;
        }

        public bool IsAdmin { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }

        public virtual Session Session { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
