﻿using Chat.Models;
using Chat.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : BaseController<Message>
    {
        public MessagesController(ChatContext dataProvider, ILogger<Message> logger) : base(dataProvider, logger) { }

        protected override DbSet<Message> Models => DataProvider.Messages;

        [HttpGet("[action]/{sessionId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBySessionId(int sessionId)
        {
            return await GetMessagesBySessionIdQuery(sessionId).ToArrayAsync();
        }

        [HttpGet("[action]/{sessionId}/{userId}")]
        public ActionResult<IEnumerable<Message>> GetUserVisibleMessagesBySessionId(int sessionId, int userId)
        {
            var userhiddenMessages = GetUserHiddenMessages(userId);
            return GetMessagesBySessionIdEnumerable(sessionId)
                .Where(msg => !userhiddenMessages.Contains(msg.Id))
                .ToArray();
        }

        [HttpGet("[action]/{sessionId}/{lastMessageDate}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesBySessionIdAndMinimumDate(int sessionId, DateTime lastMessageDate)
        {
            return await GetMessagesBySessionIdAndMinimumDateQuery(sessionId, lastMessageDate).ToArrayAsync();
        }

        [HttpGet("[action]/{sessionId}/{userId}/{lastMessageDate}")]
        public ActionResult<IEnumerable<Message>> GetUserVisibleMessagesBySessionIdAndMinimumDate(int sessionId, int userId, DateTime lastMessageDate)
        {
            var userhiddenMessages = GetUserHiddenMessages(userId);
            return GetMessagesBySessionIdAndMinimumDateEnumerable(sessionId, lastMessageDate)                
                .Where(msg => !userhiddenMessages.Contains(msg.Id))
                .ToArray();
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

        private IEnumerable<int> GetUserHiddenMessages(int userId)
        {
            return DataProvider.HiddenMessages
                .Where(x => x.UserId == userId)
                .Select(userHiddenMsg => userHiddenMsg.MessageId)
                .ToArray();
        }

        private IQueryable<Message> GetMessagesBySessionIdAndMinimumDateQuery(int sessionId, DateTime lastMessageDate)
        {
            return Models
                .FromSqlRaw("GetMessagesBySessionIdAndMinimumDate @sessionId, @lastMessageDate",
                new SqlParameter("@sessionId", sessionId),
                new SqlParameter("@lastMessageDate", lastMessageDate));
        }
        private IEnumerable<Message> GetMessagesBySessionIdAndMinimumDateEnumerable(int sessionId, DateTime lastMessageDate)
        {
            return GetMessagesBySessionIdAndMinimumDateQuery(sessionId, lastMessageDate).AsEnumerable();
        }

        private IQueryable<Message> GetMessagesBySessionIdQuery(int sessionId)
        {
            return Models
                .FromSqlRaw("GetMessagesBySessionId @sessionId", new SqlParameter("@sessionId", sessionId));
        }

        private IEnumerable<Message> GetMessagesBySessionIdEnumerable(int sessionId)
        {
            return GetMessagesBySessionIdQuery(sessionId)
                .AsEnumerable();
        }
    }
}
