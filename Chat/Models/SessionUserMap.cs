using System;
using System.Collections.Generic;

namespace Chat.Models
{
    public partial class SessionUserMap
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? SessionId { get; set; }
        public bool Isadmin { get; set; }

        public virtual Session Session { get; set; }
        public virtual User User { get; set; }
    }
}
