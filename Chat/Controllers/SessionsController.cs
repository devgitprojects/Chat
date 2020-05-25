using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionsController : BaseController<Session>
    {
        public SessionsController(ChatContext dataProvider, ILogger<Session> logger) : base(dataProvider, logger) { }

        protected override DbSet<Session> Models => DataProvider.Sessions;
    }
}
