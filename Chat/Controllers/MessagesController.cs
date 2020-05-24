using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        public MessagesController(ChatContext dataProvider)
        {
            DataProvider = dataProvider;
        }

        protected ChatContext DataProvider { get; private set; }

        // POST <controller>
        [HttpPost]
        [Route("SendMessage")]
        public async Task<ActionResult<int?>> SendMessage(int sessionUserMapId, string message)
        {
            SessionUserMap sessionUser = await DataProvider.SessionsUsersMap.FirstOrDefaultAsync(x => x.Id == sessionUserMapId);
            if (sessionUser == null)
            {
                return BadRequest();
            }

            Message newMessage = new Message();
            newMessage.SessionUserMap = sessionUser;
            newMessage.SessionUserId = sessionUser.Id;
            newMessage.Text = message;
            DataProvider.Messages.Add(newMessage);
            await DataProvider.SaveChangesAsync();
            return Ok(newMessage.Id);
        }
    }
}
