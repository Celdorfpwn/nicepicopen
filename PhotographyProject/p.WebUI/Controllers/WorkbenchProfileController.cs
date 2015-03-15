using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workbench.Abstract;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class WorkbenchProfileController : Controller
    {

        private AbstractWorkbenchProfileContext _context { get; set; }

        public WorkbenchProfileController(AbstractWorkbenchProfileContext context)
        {
            _context = context;
        }

        public ActionResult ProfileLink()
        {
            var user = _context.GetPhotographer(User.Identity.Name);
            return PartialView(user);
        }

        public ActionResult Index()
        {
            var model = _context.GetPhotographer(User.Identity.Name);
            return View(model);
        }

        public ActionResult ProfileImage()
        {
            int model = _context.GetPhotographer(User.Identity.Name).Id;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult EditProfile(HttpPostedFileBase file)
        {
            if (file != null)
            {
                bool confirm = _context.EditProfilePicture(file.InputStream,file.ContentLength, User.Identity.Name);
                if (!confirm)
                {
                    SetErrorMessage("File could not be saved");
                }
            }
            else
            {
                SetErrorMessage("Image was empty");
            }
            return RedirectToAction("Index");
        }

        private void SetErrorMessage(string message)
        {
            ViewBag.ErrorMessage = message;
        }

        public ActionResult Credentials()
        {
            var name = User.Identity.Name;
            var model = _context.GetPhotographer(name);
            return PartialView(model);
        }

        public ActionResult CountRequests()
        {
            var count = _context.GetPhotographer(User.Identity.Name).FriendRequests.Count;
            if (count != 0)
                return Content(count.ToString());
            else
                return Content("");
        }

        public ActionResult Quote()
        {
            var model = _context.GetPhotographer(User.Identity.Name).Quotes;
            return PartialView(model);
        }

    }
}
