using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public UsersController(ChatContext dataProvider) : base(dataProvider) { }

        protected override DbSet<User> Models => DataProvider.Users;
    }
}
