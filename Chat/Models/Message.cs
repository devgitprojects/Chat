using System;
using System.Collections.Generic;

namespace Chat.Models
{

    public partial class Message : BaseModel
    {
        public Message()
        {
            HiddenMessages = new HashSet<HiddenMessage>();
        }

        public Message(SessionUserMap sessionUserMap)
        {
            SessionUserMap = sessionUserMap;
        }

        public string Text { get; set; }
        public int SessionUserId { get; set; }
        public DateTime Date { get; set; }
        public bool IsHidden { get; set; }

        public virtual SessionUserMap SessionUserMap { get; set; }
        public virtual ICollection<HiddenMessage> HiddenMessages { get; set; }
    }
}