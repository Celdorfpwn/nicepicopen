using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Explorer.Abstract;
using p.Database.Concrete.Entities;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class ExplorerController : Controller
    {

        private IExplorerContext _context { get; set; }

        public ExplorerController(IExplorerContext context)
        {
            _context = context;
        }

        public ActionResult Details(int id)
        {
            var username = User.Identity.Name;
            var model = _context.GetPhotographer(id, username);
            return View(model);
        }

        public ActionResult Friends(int id, string searchString)
        {
            var model = _context.SearchFriends(id, searchString);
            return PartialView(model);
        }

        public ActionResult Albums(int id)
        {
            var model = _context.UserAlbums(id);
            return PartialView(model);
        }

        public ActionResult Pictures(int id)
        {
            var model = _context.AllPictures(id, 0, 0);
            SetCategoriesViewBagWithAll(id);
            SetAlbumsViewBagWithAll(id);
            ViewBag.UserId = id;
            return PartialView(model);
        }

        public ActionResult AllPictures(int id,int categoryId,int albumId)
        {
            var model = _context.AllPictures(id, categoryId, albumId);
            return PartialView(model);
        }

        public ActionResult Album(int id,int picture)
        {
            var model = _context.GetAlbum(id);
            if (picture != 0)
            {
                ViewBag.picture = picture;
                _context.AddPictureView(picture, User.Identity.Name);
            }
            else
            {
                if (model.Pictures.Count != 0)
                {
                    ViewBag.picture = model.Pictures.ElementAt(0).Id;
                }
            }
            return View(model);
        }

        public ActionResult Picture(int id)
        {
            var model = _context.GetPicture(id,User.Identity.Name);
            return PartialView(model);
        }

        public ActionResult AddNice(int id)
        {
            _context.AddNice(id, User.Identity.Name);
            var count = _context.GetPictureNices(id);
            return PartialView("NicesCount",count);
        }

        public ActionResult AddComment(int id, string comment)
        {
            _context.AddComment(id, User.Identity.Name, comment);

            return RedirectToAction("PictureComments", new { id = id });
        }

        public ActionResult PictureComments(int id)
        {
            var model = _context.GetPictureComments(id);

            return PartialView(model);
        }

        public ActionResult Portofolio(int id)
        {
            Portofolio model = _context.GetPortofolio(id);
            return PartialView(model);
        }

        public ActionResult PortofolioPreview(int id)
        {
            Portofolio model = _context.GetPortofolio(id);
            return View(model);
        }

        public ActionResult ImagePreview(int id)
        {
            return PartialView(id);
        }

        public ActionResult TopPictures(int id)
        {
            IEnumerable<Picture> model = _context.GetTopPictures(id, 10);
            return PartialView(model);
        }

        public ActionResult PicToAlbum(int id)
        {
            var albumId = _context.GetPictureAlbumId(id);
            return RedirectToAction("Album", new { id = albumId, picture = id });
        }


        public ActionResult Visitors()
        {
            var model = _context.UserVisitors(User.Identity.Name);
            return PartialView(model);
        }

        private void SetCategoriesViewBagWithAll(int userId)
        {
            var categories = new List<PictureCategory>();
            categories.Add(new PictureCategory { Id = 0, Name = "All" });
            categories.AddRange(_context.Categories(userId));
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }

        private void SetAlbumsViewBagWithAll(int userId)
        {
            var albums = new List<Album>();
            albums.Add(new Album { Id = 0, Name = "All" });
            albums.AddRange(_context.UserAlbums(userId));
            ViewBag.Albums = new SelectList(albums, "Id", "Name");
        }

        private int GetSessionPictureId()
        {
            return (int)Session["pictureId"];
        }

        private void SetSessionPictureId(int id)
        {
            Session["pictureId"] = id;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["pictureId"] == null)
            {
                Session["pictureId"] = 0;
            }
        }
    }
}
