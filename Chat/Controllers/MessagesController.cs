using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : Controller
    {
        public MessagesController(ChatContext dataProvider)
        {
            DataProvider = dataProvider;
        }

        protected ChatContext DataProvider { get; private set; }

        [HttpGet("[action]/{sessionId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBySessionId(int sessionId)
        {
            return await DataProvider.Messages
                .FromSqlRaw("GetMessagesBySessionId @sessionId", new SqlParameter("@sessionId", sessionId))
                .ToListAsync();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<Message>> SendMessage(SimpleMessage message)
        {
            SessionUserMap sessionUser = await DataProvider.SessionsUsersMap.FirstOrDefaultAsync(x => x.Id == message.SessionUserId);
            if (sessionUser == null)
            {
                return BadRequest();
            }

            Message newMessage = new Message(sessionUser);
            newMessage.Text = message.Text;
            DataProvider.Messages.Add(newMessage);
            await DataProvider.SaveChangesAsync();
            return Ok(newMessage);
        }
    }
}
