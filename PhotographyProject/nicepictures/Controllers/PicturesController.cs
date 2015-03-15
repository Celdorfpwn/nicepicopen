using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p.Database.Concrete.Repositories;
using nicepictures.Models;

namespace nicepictures.Controllers
{
    public class PicturesController : Controller
    {

        public ActionResult Picture(int id)
        {
            var database = new DatabaseContext();
            var data =  database.Pictures.Find(id).Image;
            return File(data, "image/jpeg");
        }

        public ActionResult SmallProfile(int id)
        {
            var database = new DatabaseContext();

            var data = database.Photographers.Find(id).SmallProfile;

            return File(data, "image/jpeg");
        }

        public ActionResult Profile(int id,int? rand)
        {
            var database = new DatabaseContext();

            var data = database.UserProfilePictures.Find(id).Image;

            return File(data, "image/jpeg");
        }

        public ActionResult Test()
        {
            var database = new DatabaseContext();
            var data = database.Pictures.Find(3).Image;

            return File(data, "image/jpeg");
        }
    }
}
