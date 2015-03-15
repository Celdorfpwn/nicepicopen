using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Community.Abstract;
using p.Database.Concrete.Entities;
using p.Database.Concrete.Repositories;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class WorldController : Controller
    {
        private ICommunityContext _context { get; set; }

        public WorldController(ICommunityContext context)
        {
            _context = context;
        }

        public ActionResult Pictures()
        {
            return PartialView();
        }

        public ActionResult FreeView()
        {
            return PartialView();
        }

        public ActionResult Newest()
        {
            return PartialView();
        }

        private const int take = 10;


        public JsonResult NewestPictures(int id)
        {
            var pictures = _context.Newest(take, id);
            var model = SerializeForJson(pictures);
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public JsonResult BestPictures(int id)
        {
            var pictures = _context.BestPictures(take, id);
            var model = SerializeForJson(pictures);
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public JsonResult RandomPictures(int id)
        {
            var pictures = _context.FreeView(take, id);
            var model = SerializeForJson(pictures);
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        private object SerializeForJson(IEnumerable<Picture> pictures)
        {
            var model = from picture in pictures
                        select new
                        {
                            PictureId = picture.Id,
                            AlbumId = picture.AlbumId,
                            AlbumName = picture.AlbumName,
                            PhoName = picture.PhotographerName,
                            PhoId = picture.PhotographerId,
                            //Image = picture.ImageToBase64,
                            HasNice = picture.HasNice(User.Identity.Name),
                            HasNot = picture.HasNotSo(User.Identity.Name),
                            CameraId = picture.CameraId,
                            CategoryName = picture.Category.Name,
                            Nicers = from nice in picture.PhotographerNice
                                     select nice.Id

                        };
            return model;
        }

    }
}
