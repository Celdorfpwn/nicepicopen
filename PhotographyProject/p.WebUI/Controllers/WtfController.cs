using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using p.Database.Concrete.Entities;
using p.Database.Concrete.Repositories;
using PicturesProvider.Concrete;
using Workbench.Concrete;
using System.Data.Entity;

namespace p.WebUI.Controllers
{
    public class WtfController : Controller
    {
        //
        // GET: /Wtf/

        public ActionResult Index()
        {
            DatabaseContext db = new DatabaseContext();
            return View(db.Photographers);
        }

        public ActionResult Upload(int id, int userId, string conid,bool record,string interval, HttpPostedFileBase datafile)
        {
            var imgData = ImageProcessor.GetImageDataFromStream(datafile.InputStream,datafile.ContentLength);
            DatabaseContext db = new DatabaseContext();
            if (record)
            {

                if (ImageProcessor.CheckIfFileIsImage(imgData))
                {
                   
                    var user = db.Photographers.FirstOrDefault(p => p.Name.Equals(User.Identity.Name));
                    ConversationMessage message = new ConversationMessage
                    {
                        ConversationId = id,
                        Image = imgData,
                        SenderId = user.Id
                    };
                    db.ConversationsMessages.Add(message);
                    db.SaveChanges();

                    var send = new
                    {
                        ConversationId = id,
                        User = false,
                        Id = message.Id,
                        Interval = interval
                    };


                    var receive = new
                    {
                        ConversationId = id,
                        User = true,
                        Id = message.Id,
                        Interval = interval
                    };

                    var context = GlobalHost.ConnectionManager.GetHubContext<MessMe>();
                    context.Clients.Client(conid).sendMessage(receive);
                    var partenerId = db.Photographers.Find(userId).ChatConnectionId;
                    context.Clients.Client(partenerId).sendMessage(send);
                }
            }
            else
            {
                if (ImageProcessor.CheckIfFileIsImage(imgData))
                {
                    var send = new
                    {
                        ConversationId = id,
                        User = false,
                        Image = imgData,
                        Interval = interval
                    };
                    var receive = new
                    {
                        ConversationId = id,
                        User = true,
                        Image = imgData,
                        Interval = interval
                    };

                    var context = GlobalHost.ConnectionManager.GetHubContext<MessMe>();
                    context.Clients.Client(conid).sendMessage(receive);
                    var partenerId = db.Photographers.Find(userId).ChatConnectionId;
                    if(partenerId != null)
                    context.Clients.Client(partenerId).sendMessage(send);
                }
            }
            return null;
        }

        public ActionResult RemoveAllNices(int id)
        {
            if (!id.Equals(111192))
            {
                return Content("muie");
            }
            DatabaseContext db = new DatabaseContext();
            var pictures = db.Pictures.Include("PhotographerNice").Include("PhotographerNotNice");
            foreach (var picture in pictures)
            {
                var nices = picture.PhotographerNice.ToList();
                foreach (var nice in nices)
                {
                    picture.PhotographerNice.Remove(nice);
                }
                var not = picture.PhotographerNotNice.ToList();
                foreach (var n in not)
                {
                    picture.PhotographerNotNice.Remove(n);
                }
            }
            db.SaveChanges();
            return Content("finish");
        }

        public ActionResult SmallProfile()
        {
            DatabaseContext db = new DatabaseContext();
            foreach (var profile in db.UserProfilePictures.Include(p => p.User))
            {
                byte[] small = Resizer.Resize(profile.Image, "100", "100");
                byte[] result = Compressor.Compress(small, 50);

                
                profile.User.SmallProfile = result;
            }
            db.SaveChanges();

            return Content("finish");
        }

        public ActionResult RunPictureUpdateScript()
        {
            DatabaseContext db = new DatabaseContext();

            var pictures = db.Pictures;
            var albums = db.Albums.ToList();
            var users = db.Photographers.ToList();

            foreach (var picture in pictures)
            {
                var album = albums.FirstOrDefault(a => a.Id.Equals(picture.AlbumId));
                var user = users.FirstOrDefault(u => u.Id.Equals(album.PhotographerId));
                picture.AlbumName = album.Name;
                picture.PhotographerName = user.Name;
                picture.PhotographerId = user.Id;
            }
            db.SaveChanges();

            return Content("Finish");
        }

    }
}
