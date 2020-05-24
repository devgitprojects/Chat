using System.Collections.Generic;

namespace Chat.Models
{
    public partial class User
    {
        public User()
        {
            HiddenMessages = new HashSet<HiddenMessage>();
            SessionsUsersMap = new HashSet<SessionUserMap>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<HiddenMessage> HiddenMessages { get; set; }
        public virtual ICollection<SessionUserMap> SessionsUsersMap { get; set; }
    }
}
