using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Explorer.Abstract;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class ExplorerPortofolioController : Controller
    {

        private AbstractExplorerPortofolioContext _context { get; set; }

        public ExplorerPortofolioController(AbstractExplorerPortofolioContext context)
        {
            _context = context;
        }

        public ActionResult Index(int id)
        {
            var model = _context.GetPortofolio(id);
            return View(model);
        }

        public ActionResult Picture(int id)
        {
            var model = _context.GetPicture(id);
            _context.AddView(id, User.Identity.Name);
            return PartialView(model);
        }

        public ActionResult PictureViews(int id)
        {
            var content = _context.GetPictureViews(id);
            return Content(content.ToString());
        }

        public ActionResult PictureNices(int id)
        {
            var content = _context.GetPictureNices(id);
            return Content(content.ToString());
        }

        public ActionResult HasNice(int id)
        {
            if (_context.HasNice(id,User.Identity.Name))
                return PartialView("Niced",id);
            else
            {
                return PartialView("Nice",id);
            }
        }

        public ActionResult AddNice(int id)
        {
            _context.AddNice(id,User.Identity.Name);
            var model = _context.GetPictureNices(id);
            return Content(model.ToString());
        }

        public ActionResult AddView(int id)
        {
            _context.AddView(id, User.Identity.Name);
            return null;
        }

        public ActionResult AddComment(int id,string comment)
        {
            _context.AddComment(id, User.Identity.Name,comment);
            var model = _context.GetPictureComments(id);
            return PartialView("PictureComments",model);
        }

        public ActionResult DonwloadPicture(int id)
        {
            var model = _context.GetPicture(id);
            return File(model.ImageWithWatermark, "image/jpeg", model.Id.ToString());
        }

        public ActionResult PictureComments(int id)
        {
            var model = _context.GetPictureComments(id);
            return PartialView(model);
        }

    }
}
