using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionsController : Controller
    {
        public SessionsController(ChatContext dataProvider)
        {
            DataProvider = dataProvider;
        }

        protected ChatContext DataProvider { get; private set; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionUserMap>>> Get()
        {
            return await DataProvider.SessionsUsersMap.ToArrayAsync();
        }

        [HttpGet("[action]/{userId}")]
        public async Task<ActionResult<IEnumerable<RecentlyActiveSessionData>>> GetRecentlyActiveSessionIDs(int userId)
        {
            return await DataProvider.RecentlyActiveSessionData
                .FromSqlRaw("GetRecentlyActiveSessionIDs @userId", new SqlParameter("@userId", userId)).ToArrayAsync();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<SessionUserMap>> CreateSession(SessionCreationData sessionCreationData)
        {
            User user = await DataProvider.Users.FirstOrDefaultAsync(x => x.Id == sessionCreationData.UserId);
            return await CreateNewSessionUserMap(new SessionUserMap(user, new Session(sessionCreationData.IsAdminSession), isAdmin: true));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<SessionUserMap>> ConnectUserToSession(SimpleSessionUserMap sessionUserMap)
        {
            Session session = await DataProvider.Sessions.FirstOrDefaultAsync(x => x.Id == sessionUserMap.SessionId);
            User user = await DataProvider.Users.FirstOrDefaultAsync(x => x.Id == sessionUserMap.UserId);            
            return await CreateNewSessionUserMap(new SessionUserMap(user, session, isAdmin: false));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<SessionUserMap>> CreateNewSessionUserMap(SessionUserMap sessionUserMap)
        {
            if (sessionUserMap == null || sessionUserMap.User == null || sessionUserMap.Session == null)
            {
                return BadRequest();
            }

            if (sessionUserMap.Session.IsAdminSession)
            {
                if (!DataProvider.SessionsUsersMap
                    .Where(x => x.UserId == sessionUserMap.User.Id)
                    .Any(x => x.IsAdmin))
                {
                    return BadRequest("Regular user could not be connected to admin session.");
                }
            }

            DataProvider.SessionsUsersMap.Add(sessionUserMap);
            await DataProvider.SaveChangesAsync();
            return Ok(sessionUserMap);
        }
    }
}
