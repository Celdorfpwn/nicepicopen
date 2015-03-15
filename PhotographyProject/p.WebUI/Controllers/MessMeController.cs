using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MessMeLib.Abstract;
using p.Database.Concrete.Entities;

namespace p.WebUI.Controllers
{
    public class MessMeController : Controller
    {
        private IMessMeContext _context { get; set; }

        private int UserId { get; set; }

        public MessMeController(IMessMeContext context)
        {
            _context = context;
        }

        public ActionResult Conversations()
        {
            var id = _context.UserId(User.Identity.Name);
            return PartialView(id);
        }

        public JsonResult ConversationsData()
        {

            var userId = _context.UserId(User.Identity.Name);
            var conversations = _context.Conversations(userId);
            var model = SerializeJson(conversations, userId);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private object SerializeJson(IEnumerable<Conversation> conversations,int userId)
        {
            var model = from conversation in conversations
                        select new
                        {
                            Id = conversation.Id,
                            UserId = conversation.GetPartenerId(userId),
                            Name = conversation.GetParneterName(userId)
                        };
            return model;

        }


        public ActionResult ActiveConversation(int id,int userId)
        {
            _context.ActiveConversation(id,userId);
            return null;
        }

        public ActionResult RemoveConversation(int id)
        {
            _context.RemoveConversation(id, User.Identity.Name);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddMessage(int id, string message)
        {
            if(message != String.Empty)
            _context.AddMessage(User.Identity.Name, id, message);
            return null;
        }

        public ActionResult NewMessages()
        {
            var model = _context.UserNewMessages(User.Identity.Name);
            return PartialView(model);
        }

    }
}
