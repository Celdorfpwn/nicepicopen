using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortofolioManager.Abstract;
using PortofolioManager.Models;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class PortofolioController : Controller
    {

        private IPortofolioContext _context { get; set; }

        public PortofolioController(IPortofolioContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var model = _context.GetPortofolio(User.Identity.Name);
            return View(model);
        }

        public ActionResult ImagePreview(int id)
        {
            return PartialView(id);
        }

        public ActionResult IsViral(int id)
        {
            if (_context.IsViral(id))
            {
                var model = _context.GetPortofolio(User.Identity.Name);
                return PartialView("Viral",model);
            }
            else
                return PartialView("GoViral",id);
        }

        public ActionResult GoViral(int id)
        {
            _context.GoViral(id);
            var model = _context.GetPortofolio(User.Identity.Name);
            return PartialView("Viral",model);
        }

        public ActionResult CreatePortofolio()
        {
            CreatePortofolioSession();
            var model = _context.GetUserPictures(User.Identity.Name);
            return View(model);
        }

        public ActionResult ChoosePictures()
        {
            var model = _context.GetPictures(GetCreateSession().CoverId,User.Identity.Name);
            return View(model);
        }

        public ActionResult ChoosedPicture(int id)
        {
            GetCreateSession().Pictures.Add(id);
            return PartialView(id);
        }

        public ActionResult RemovePicture(int id)
        {
            GetCreateSession().Pictures.Remove(id);
            return null;
        }

        public ActionResult SavePortofolio()
        {
            _context.CreatePortofolio(GetCreateSession(), User.Identity.Name);
            RemoveSession();
            return RedirectToAction("Index", "WorkbenchProfile");
        }

        public ActionResult ChoosedCover(int id)
        {
            GetCreateSession().CoverId = id;
            return PartialView(id);
        }


        private void CreatePortofolioSession()
        {
            Session.Add(User.Identity.Name, new CreatePortofolioModel());
        }

        private CreatePortofolioModel GetCreateSession()
        {
            return (CreatePortofolioModel)Session[User.Identity.Name];
        }

        private void RemoveSession()
        {
            Session.Remove(User.Identity.Name);
        }
    }
}
