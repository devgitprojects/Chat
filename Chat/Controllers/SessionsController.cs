using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<SessionUserMap>> CreateSession([FromBody]int userId)
        {
            User user = await DataProvider.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return await CreateNewSessionUserMap(new SessionUserMap(user, new Session()));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<SessionUserMap>> ConnectUserToSession(SimpleSessionUserMap sessionUserMap)
        {
            Session session = await DataProvider.Sessions.FirstOrDefaultAsync(x => x.Id == sessionUserMap.SessionId);
            User user = await DataProvider.Users.FirstOrDefaultAsync(x => x.Id == sessionUserMap.UserId);
            return await CreateNewSessionUserMap(new SessionUserMap(user, session));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<SessionUserMap>> CreateNewSessionUserMap(SessionUserMap sessionUserMap)
        {
            if (sessionUserMap == null || sessionUserMap.User == null || sessionUserMap.Session == null)
            {
                return BadRequest();
            }

            DataProvider.SessionsUsersMap.Add(sessionUserMap);
            await DataProvider.SaveChangesAsync();
            return Ok(sessionUserMap);
        }
    }
}
