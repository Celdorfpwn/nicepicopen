using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Community.Abstract;
using p.Database.Concrete.Entities;

namespace p.WebUI.Controllers
{
    public class DiscussionPostsController : Controller
    {

        private AbstractDiscussionPostsContext _context { get; set; }

        public DiscussionPostsController(AbstractDiscussionPostsContext context)
        {
            _context = context;
        }

        public ActionResult DiscussionPosts(int id)
        {
            var model = _context.GetDiscussionPosts(id);
            if(model!=null)
            return PartialView(model);
            return null;
        }

        public ActionResult DiscussionPost(int id)
        {
            var model = _context.GetDiscussion(id);
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult AddDiscussionPost(int id, string post)
        {
            string userName = User.Identity.Name;
            DiscussionPost model = new DiscussionPost { Text = post };
            _context.AddDiscussionPost(id, userName, model);

            return RedirectToAction("DiscussionPost", new { id = model.Id });
        }

    }
}
