using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p.Database.Concrete.Entities;
using p.Database.Concrete.Repositories;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(PictureCategory category)
        {
            DatabaseContext db = new DatabaseContext();
            db.Categories.Add(category);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
