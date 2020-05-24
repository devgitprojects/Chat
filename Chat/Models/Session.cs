using System;
using System.Collections.Generic;

namespace Chat.Models
{
    public partial class Session
    {
        public Session()
        {
            SessionsUsersMap = new HashSet<SessionUserMap>();
        }

        public Session(bool isAdminSession)
        {
            IsAdminSession = isAdminSession;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsAdminSession { get; set; }

        public virtual ICollection<SessionUserMap> SessionsUsersMap { get; set; }
    }
}
