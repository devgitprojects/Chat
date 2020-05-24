using Chat.Models;
using Chat.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : BaseController<Message>
    {
        public MessagesController(ChatContext dataProvider) : base(dataProvider) { }

        protected override DbSet<Message> Models => DataProvider.Messages;

        [HttpGet("[action]/{sessionId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBySessionId(int sessionId)
        {
            return await Models
                .FromSqlRaw("GetMessagesBySessionId @sessionId", new SqlParameter("@sessionId", sessionId))
                .ToArrayAsync();
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
            Models.Add(newMessage);
            await DataProvider.SaveChangesAsync();
            return Ok(newMessage);
        }
    }
}
