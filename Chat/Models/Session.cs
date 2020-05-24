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

        public int Id { get; set; }
        public DateTime? Date { get; set; }

        public virtual ICollection<SessionUserMap> SessionsUsersMap { get; set; }
    }
}
