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
    public class UsersController : BaseController<User>
    {
        public UsersController(ChatContext dataProvider, ILogger<User> logger) : base(dataProvider, logger) { }

        protected override DbSet<User> Models => DataProvider.Users;
    }
}
