using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class SimpleMessage
    {
        public string Text { get; set; }
        public int SessionUserId { get; set; }
    }
}
