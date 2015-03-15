using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p.Database.Concrete.Entities;
using Workbench.Abstract;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class WorkbenchAlbumsController : Controller
    {
        private AbstractWorkbenchAlbumsContext _context { get; set; }

        public WorkbenchAlbumsController(AbstractWorkbenchAlbumsContext context)
        {
            _context = context;
        }

        public ActionResult Albums()
        {
            return PartialView();
        }

        public JsonResult AllAlbums(int id)
        {
            var albums = _context.Albums(User.Identity.Name).Skip(id).Take(15);
            var model = from album in albums
                        select new
                        {
                            Id = album.Id,
                            Name = album.Name,
                        };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AlbumProfilePicture(int id)
        {
            var picture = _context.AlbumBestPicture(id);
            var model = new
            {
                Id = picture.Id
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AlbumCaruselPictures(int id)
        {
            var pictures = _context.AlbumCaruselPictures(id);
            var model = from picture in pictures
                        select new
                        {
                            Id = picture.Id
                        };
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AlbumDetails(int id)
        {

            var model = _context.GetAlbum(id);
            SetCategoriesViewBagWithAll();
            return View(model);
        }

        private void SetCategoriesViewBagWithAll()
        {
            var categories = new List<PictureCategory>();
            categories.Add(new PictureCategory { Id = 0, Name = "All" });
            categories.AddRange(_context.CategoriesFiltered(User.Identity.Name));
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }


        public ActionResult AlbumCarusel(int id)
        {
            var model = _context.AlbumCaruselPictures(id);
            return PartialView(model);
        }

        public ActionResult AddAlbum()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return PartialView();
        }

        public ActionResult AddPictureQuick(int id, HttpPostedFileBase file)
        {
            if(file!=null)
            _context.AddPictureQuick(id, file.InputStream);
            return RedirectToAction("AlbumDetails", new { id = id });
        }

        [HttpPost]
        public ActionResult AddAlbum(Album model,int category, IEnumerable<HttpPostedFileBase> files)
        {
            if(files!=null)
            if (ModelState.IsValid)
            {
                IEnumerable<Stream> streams = null;
                if (files.ElementAt(0) != null)
                    streams = files.Select(file => file.InputStream);
                else
                    streams = new List<Stream>();
                _context.AddAlbum(model, streams, User.Identity.Name,category);
            }
            return RedirectToAction("Index", "WorkbenchProfile");
        }

        public ActionResult Download(int id)
        {
            string albumName = _context.GetAlbum(id).Name;
            Stream outputStream = _context.GetAlbumZip(id);
            return File(outputStream, "application/zip", albumName + ".zip");
        }

        public ActionResult IsViral(int id)
        {
            if (_context.IsViral(id))
            {
                var model = _context.GetAlbum(id);
                return PartialView("Viral",model);
            }
            else
                return PartialView("GoViral", id);
        }

        public ActionResult AlbumBestPicture(int id)
        {
            var model = _context.AlbumBestPicture(id);
            if (model != null)
                return PartialView(model);
            else
                return PartialView("Empty");
        }

        public ActionResult GoViral(int id)
        {
            _context.AlbumGoViral(id);
            var model = _context.GetAlbum(id);
            return PartialView("Viral",model);
        }
        public ActionResult ChangeDownload(int id,bool Downloadable)
        {
            _context.ChangeDownload(id, Downloadable);
            return null;
        }

        public ActionResult DownloadAlbum(int id)
        {
            string albumName = _context.GetAlbum(id).Name;
            Stream outputStream = _context.GetAlbumZipForUsers(id);
            return File(outputStream, "application/zip", albumName + ".zip");
        }

    }
}
