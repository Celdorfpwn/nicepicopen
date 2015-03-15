using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Explorer.Abstract;
using p.Database.Concrete.Entities;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class ExplorerUpdatesController : Controller
    {
        private AbstractExplorerUpdatesContext _context { get; set; }

        public ExplorerUpdatesController(AbstractExplorerUpdatesContext context)
        {
            _context = context;
        }

        public ActionResult Updates(int id)
        {
            var model = _context.GetUpdates(id);
            return PartialView(model);
        }

        public ActionResult News()
        {
            return PartialView();
        }

        public JsonResult NewsData()
        {
            var updates = _context.Updates(User.Identity.Name);

            var model = SerializeJson(updates);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MoreNewsData(int id)
        {
            int checks = 0;
            IEnumerable<UserUpdate> updates = null;
            while (checks < 5)
            {
                updates = _context.Updates(User.Identity.Name).Where(u => u.Id > id);
                if (updates.Count() > 0)
                {
                    break;
                }
                Thread.Sleep(15000);
                checks++;
            }
            var model = SerializeJson(updates);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private object SerializeJson(IEnumerable<UserUpdate> updates)
        {

            var model = from update in updates
                        select new
                        {
                            Id = update.Id,
                            HasAlbum = update.HasAlbum(),
                            HasPictures = update.HasPicture(),
                            UserId = update.PhotographerId,
                            UserName = update.Photographer.Name
                        };

            return model;
        }

        public ActionResult Update(int id)
        {
            var update = _context.GetUpdate(id);
            if (update.HasQuote())
            {
                var quote = _context.GetQuote((int)update.QuoteId);
                return PartialView("QuoteUpdate", quote);
            }
            else if (update.HasPicture())
            {
                var picturesModel = _context.GetPicturesModel(update.Id, update.PhotographerId);
                return PartialView("PicturesUpdate", picturesModel);
            }
            else if (update.HasAlbum())
            {
                var model = _context.GetAlbum((int)update.AlbumId);
                return PartialView("AlbumUpdate", model);
            }
            else if (update.HasContestPicture())
            {
                var model = _context.GetContestPicture((int)update.ContestPictureId);
                return PartialView("ContestPictureUpdate",model);
            }
            else if (update.HasDailyContestWinner())
            {
                var winnerId = update.DailyContestWinner.WinnerId;
                var model = _context.GetContestPicture((int)winnerId);
                return PartialView("ContestWinner", model);
            }
            return null;
        }

        public ActionResult CameraUpdate(int id)
        {
            var model = _context.GetCamera(id);
            return PartialView(model);
        }

        public ActionResult QuoteUpdate(int id)
        {
            var model = _context.GetQuote(id);

            return PartialView(model);
        }

        public ActionResult PicturesUpdate(int id)
        {
            var model = _context.GetPictures(id);
            return PartialView(model);
        }

        public ActionResult AlbumUpdate(int id)
        {
            var model = _context.GetAlbum(id);
            return PartialView(model);
        }

        public ActionResult UserName(int id)
        {
            var model = _context.GetUser(id);
            return PartialView(model);
        }

    }
}
