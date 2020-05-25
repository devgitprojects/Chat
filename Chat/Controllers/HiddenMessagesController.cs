using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HiddenMessagesController : BaseController<HiddenMessage>
    {
        public HiddenMessagesController(ChatContext dataProvider, ILogger<HiddenMessage> logger) : base(dataProvider, logger) { }

        protected override DbSet<HiddenMessage> Models => DataProvider.HiddenMessages;

        [HttpPost]
        [Route("[action]/{messageId}/{userId}")]
        public async Task<ActionResult<HiddenMessage>> HideMessage(int messageId, int userId)
        {
            return await Post(new HiddenMessage(messageId, userId));
        }

        [HttpDelete]
        [Route("[action]/{messageId}/{userId}")]
        public async Task<ActionResult<HiddenMessage>> UnHideMessage(int messageId, int userId)
        {
            HiddenMessage toUnhide = await Models.FirstOrDefaultAsync(x => x.MessageId == messageId && x.UserId == userId);
            return await Delete(toUnhide.Id);
        }
    }
}
