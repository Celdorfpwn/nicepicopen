using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p.Database.Concrete.Repositories;

namespace p.WebUI.Controllers
{
    public class DownloadController : Controller
    {

        private DatabaseContext database { get; set; }

        public DownloadController()
        {
            database = new DatabaseContext();
        }

        public FileResult MessageImage(int id)
        {
            var imageData = database.ConversationsMessages.Find(id).Image;
            return File(imageData,"image/jpeg","picture");
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
