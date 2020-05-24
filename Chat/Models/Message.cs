using Chat.Models;
using System;
using System.Collections.Generic;

namespace Chat.Models
{

    public partial class Message : SimpleMessage
    {
        public Message()
        {
            HiddenMessages = new HashSet<HiddenMessage>();
        }

        public Message(SessionUserMap sessionUserMap)
        {
            SessionUserMap = sessionUserMap;
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsHidden { get; set; }

        public virtual SessionUserMap SessionUserMap { get; set; }
        public virtual ICollection<HiddenMessage> HiddenMessages { get; set; }
    }
}