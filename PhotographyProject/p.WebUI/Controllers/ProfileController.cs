using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using p.Database.Concrete.Repositories;

namespace p.WebUI.Controllers
{
    public class ProfileController : Controller
    {

        private DatabaseContext database = new DatabaseContext();


        public ActionResult UserView(int id)
        {
            return PartialView(id);
        }

        public ActionResult UserData(int id)
        {
            var user = database.Photographers.Find(id);


            var model = new
            {
                Id = user.Id,
                Name = user.FullName,

                Nices = user.Favorites.Count,

                Watchers = from watcher in user.FriendsIAccept
                           select
                           new
                           {
                               Id = watcher.Id
                           },
                Watching = from watcher in user.FriendsIAdd
                           select
                           new
                           {
                               Id = watcher.Id
                           }
            };

            return Json(model, JsonRequestBehavior.AllowGet);


        }


    }
}
