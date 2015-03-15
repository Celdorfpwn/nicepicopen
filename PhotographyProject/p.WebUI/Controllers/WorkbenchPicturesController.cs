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
    public class WorkbenchPicturesController : Controller
    {

        private AbstractWorkbenchPicturesContext _context { get; set; }

        public WorkbenchPicturesController(AbstractWorkbenchPicturesContext context)
        {
            _context = context;
        }

        public ActionResult AllPictures()
        {
            return PartialView();
        }

        private const int take = 27;

        // id = skip
        public JsonResult Pictures(int id)
        {
            var pictures = _context.GetUserPictures(take,id,User.Identity.Name);
            var model = from picture in pictures
                        select
                            new
                            {
                                Id = picture.Id,
                                AlbumId = picture.AlbumId
                            };
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult FavoritesPictures(int id)
        {
            var pictures = _context.UserFavorites(take, id, User.Identity.Name);
            var model = from picture in pictures
                        select
                        new
                        {
                            Id = picture.Id,
                            AlbumId = picture.AlbumId
                        };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Favorites()
        {
            return PartialView();
        }

        public ActionResult AddPictures(int id)
        {
            ViewBag.Categories = new SelectList(_context.Categories,"Id","Name");
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddPictures(int id, int categoryId,IEnumerable<HttpPostedFileBase> files)
        {
            if(files!=null)
            if (files.Count() != 0)
            {
                var stream = files.Select(file => file.InputStream);
                _context.AddPictures(id, categoryId, stream);
            }

            return RedirectToAction("Index", "WorkbenchProfile");
        }

        public ActionResult AddPicturesForAlbum(int id)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Albums = new SelectList(_context.Albums(id), "Id", "Name");
            return PartialView();
        }

        public ActionResult AlbumPictures(int id, int categoryId)
        {
            var model = _context.GetByCategory(id, categoryId);

            return PartialView(model);
        }

        public ActionResult AddAlbumPictures(int id)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddAlbumPictures(int id, int categoryId, IEnumerable<HttpPostedFileBase> files)
        {
            if (files.Count() != 0)
            {
                var streams = files.Select(file => file.InputStream);
                _context.AddPictures(id, categoryId, streams);
            }

            return RedirectToAction("AlbumDetails", "WorkbenchAlbums", new { id = id });
        }


        public ActionResult PictureProfile(int id)
        {
            var model = _context.GetPicture(id);
            return View(model);
        }

        public ActionResult PicturePreview(int id)
        {
            var model = _context.GetPicture(id);
            ViewBag.Categories = new SelectList(_context.Categories,"Id","Name");
            ViewBag.Watermarks = new SelectList(_context.PictureWatermarks(id,User.Identity.Name), "Id", "Name");
            ViewBag.Cameras = new SelectList(_context.GetCameras(id,User.Identity.Name), "Id", "Name");
            return PartialView(model);
        }
        public ActionResult PictureView(int id)
        {
            var model = _context.GetPicture(id);
            return PartialView(model);
        }


        public ActionResult PictureViewWatermark(int id)
        {
            var model = _context.GetPicture(id);
            return PartialView(model);
        }

        public FileResult Download(int id)
        {
            var model = _context.GetPicture(id);
            return File(model.Image, "image/jpeg",model.Id.ToString());
        }

        public FileResult DownloadWithWatermark(int id)
        {
            var model = _context.GetPicture(id);
            return File(model.ImageWithWatermark, "image/jpeg", model.Id.ToString());
        }

        public ActionResult ChangePictureCategory(int id, int CategoryId)
        {
            _context.ChangeCategory(id, CategoryId);
            return Content("Category Changed");
        }

        public ActionResult ChangeDonwload(int id, bool Downloadable)
        {
            _context.ChangeDownload(id, Downloadable);
            return Content("Download Changed");
        }

        public ActionResult ChangeDonwloadMark(int id, bool DownloadWithWatermark)
        {
            _context.ChangeDownloadMark(id, DownloadWithWatermark);
            return Content("Donwload Changed");
        }

        public ActionResult ChangeWatermark(int id, string watermark)
        {
            _context.ChangeWatermark(id, watermark);
            var picture = _context.GetPicture(id);
            return PartialView("PictureViewWatermark", picture);
        }

        public ActionResult ChangeCamera(int id,int cameraId)
        {
            _context.ChangeCamera(id, cameraId);
            return Content("Camera changed");
        }

        public JsonResult Nice(int id)
        {
            var userName = User.Identity.Name;
            var userData = _context.Nice(id, userName);


            return Json(userData,JsonRequestBehavior.AllowGet);
        }

        public JsonResult NotSo(int id)
        {
            var userName = User.Identity.Name;
            var result = _context.NotSo(id, userName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
