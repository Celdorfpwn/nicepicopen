using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Community.Abstract;
using p.Database.Concrete.Entities;

namespace p.WebUI.Controllers
{
    public class DiscussionsController : Controller
    {

        private AbstractDiscussionsContext _context { get; set; }

        public DiscussionsController(AbstractDiscussionsContext context)
        {
            _context = context;
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                string userName = User.Identity.Name;
                _context.AddDiscussion(userName, discussion);
            }
            return RedirectToAction("Index", "WorkbenchProfile");
        }

        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult All(string search)
        {
            var model = _context.AllDiscussions("");
            return PartialView(model);
        }

        public ActionResult MyDiscussions()
        {
            var model = _context.UserDiscussions(User.Identity.Name);
            return PartialView("All", model);
        }

        public ActionResult Participating()
        {
            var model = _context.UserParticipatingDiscussions(User.Identity.Name);
            return PartialView("All", model);
        }

        public ActionResult Seen()
        {
            var model = _context.UserSeenDiscussions(User.Identity.Name);
            return PartialView("All", model);
        }

        public ActionResult Discussion(int id)
        {
            var model = _context.GetDiscussion(id,User.Identity.Name);
            return PartialView(model);
        }

    }
}
