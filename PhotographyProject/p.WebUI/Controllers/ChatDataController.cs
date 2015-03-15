using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using p.Database.Concrete.Entities;
using p.Database.Concrete.Repositories;
using System.Data.Entity;
using Microsoft.AspNet.SignalR;

namespace p.WebUI.Controllers
{
    public class ChatDataController : Controller
    {

        private DatabaseContext database { get; set; }

        public ChatDataController()
        {
            database = new DatabaseContext();
        }

        private int UserId()
        {
            string username = User.Identity.Name;
            var userId = database.Photographers.FirstOrDefault(p => p.Name.Equals(username)).Id;
            return userId;
        }

        public ActionResult ConversationsData(int id)
        {
            database.NotChange();
            var userId = id;
            var convs = Conversations(id);
            var model = from conversation in convs
                        select new
                        {
                            Id = conversation.Id,
                            UserId = conversation.GetPartenerId(userId),
                            Name = conversation.GetParneterName(userId),
                            Online = conversation.GetPartener(userId).Online
                        };

            return Json(model.ToList(), JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Conversation> Conversations(int userId)
        {
            var conversations = from conversation in database.Conversations
                                where conversation.SenderId == userId || conversation.ReceiverId == userId
                                select conversation;
            return conversations.Include(c => c.Sender).Include(c => c.Receiver);
        }


        public JsonResult AllConversations()
        {
            var id = UserId();
            var active = ActiveConversationIds(id).ToList();
            var conversations = Conversations(id);

            var result = from conversation in conversations
                         select new
                         {
                             Id = conversation.Id,
                             Name = conversation.GetParneterName(id),
                             PartenerId = conversation.GetPartenerId(id),
                             MyId = id,
                             Active = active.Contains(conversation.Id),
                             Messages = (from message in database.ConversationsMessages
                                        where message.ConversationId == conversation.Id
                                        select new
                                        {
                                            ConversationId = message.ConversationId,
                                            Id = message.Id,
                                            User = message.SenderId == id,
                                            Message = message.Message,
                                        })
                         };
           
            return Json(result.ToList(), JsonRequestBehavior.AllowGet);

        }






        private string UserName(int userId)
        {
            var username = database.Database
                .SqlQuery<string>("SELECT Name FROM dbo.Photographers WHERE Id = " + userId);
            return username.ElementAt(0);
        }


        public ActionResult PipeMessages(int id)
        {
            database.NotChange();
            string username = User.Identity.Name;
            var userId = database.Photographers.FirstOrDefault(p => p.Name.Equals(username)).Id;
            var conversationIds = ActiveConversationIds(userId);
            var messages = from message in database.ConversationsMessages
                           where message.Id > id && conversationIds.Contains(message.ConversationId)
                           select new
                           {
                               ConversationId = message.ConversationId,
                               Id = message.Id,
                               User = message.SenderId == userId,
                               Message = message.Message,
                           };
            if (messages.Count() == 0)
            {
                var firstCount = conversationIds.Count();
                int checks = 0;

                while (checks < 50)
                {
                    conversationIds = ActiveConversationIds(userId);
                    messages = from message in database.ConversationsMessages
                               where message.Id > id && conversationIds.Contains(message.ConversationId)
                               select new
                               {
                                   ConversationId = message.ConversationId,
                                   Id = message.Id,
                                   User = message.SenderId == userId,
                                   Message = message.Message,
                               };
                    if (messages.Any())
                    {
                        break;
                    }
                    int newCount = conversationIds.Count();
                    if (firstCount < newCount)
                    {
                        break;
                    }
                    checks++;
                    Thread.Sleep(500);
                }
            }

            return Json(messages, JsonRequestBehavior.AllowGet);
        }


        private IEnumerable<int> ActiveConversationIds(int userId)
        {
            var conversationIds = database.Database.SqlQuery<int>
                ("SELECT conversationsId FROM dbo.activeConversations WHERE photographerId = " + userId);
            return conversationIds;
        }
    }

    public static class Extensions
    {
        public static IEnumerable<T> Last<T>(this IEnumerable<T> collection, int n)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (n < 0)
                throw new ArgumentOutOfRangeException("n", "n must be 0 or greater");

            LinkedList<T> temp = new LinkedList<T>();

            foreach (var value in collection)
            {
                temp.AddLast(value);
                if (temp.Count > n)
                    temp.RemoveFirst();
            }

            return temp;
        }
    }
}
