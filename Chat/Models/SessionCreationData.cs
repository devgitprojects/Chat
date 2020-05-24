using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class SessionCreationData
    {
        public int UserId { get; set; }
        public bool IsAdminSession { get; set; }
    }
}
