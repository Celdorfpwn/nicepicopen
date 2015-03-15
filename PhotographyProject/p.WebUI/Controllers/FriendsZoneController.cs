using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Friends.Abstract;
using p.Database.Concrete.Entities;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class FriendsZoneController : Controller
    {

        private IFriendsZoneContext _context { get; set; }

        public FriendsZoneController(IFriendsZoneContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var model = _context.GetUser(User.Identity.Name);
            return View(model);
        }

        public ActionResult Friends()
        {
            var model = _context.GetUser(User.Identity.Name).Friends;
            return PartialView(model);
        }

        public ActionResult FindFriend()
        {
            return PartialView();
        }

        public ActionResult UserFriends(string searchString)
        {
            var model = _context.GetFriends(User.Identity.Name,searchString);
            return PartialView(model);
        }

        public ActionResult Suggestions()
        {
            var model = _context.Suggestions(User.Identity.Name);
            return PartialView(model);
        }

        public ActionResult GetFriends(string containsString)
        {
            if(containsString.Equals(String.Empty))
            {
                return PartialView(new List<Photographer>());
            }
            var model = _context.GetUsers(User.Identity.Name, containsString);
            int count = model.Count();
            return PartialView(model);
        }

        public ActionResult FriendRequest(int id)
        {
            _context.AddRequest(User.Identity.Name, id);

            return null;
        }

        public ActionResult FriendRequests()
        {
            var model = _context.GetUserFriendRequests(User.Identity.Name);

            return PartialView(model);
        }

        public ActionResult AcceptRequest(int id)
        {
            var conversation = _context.AcceptRequest(id);
            var model = new
            {
                Id = conversation.Id,
                UserId = conversation.SenderId,
                Name = conversation.Sender.Name
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LatestUpdates()
        {
            var model = _context.GetLastestUpdates(User.Identity.Name,20,0);
            if (model.Count() != 0)
            {
                try
                {
                    int id = model.First().Id;
                    SetLastestUpdateId(id);
                }
                finally { }
            }

            return PartialView(model);
        }

        public ActionResult MoreUpdates()
        {
            int latestId = GetLatestsUpdateId();

            var model = _context.GetLastestUpdates(User.Identity.Name,20,0).Where(update => update.Id > latestId);
            if (model.Count() != 0)
            {
                try
                {
                    int id = model.First().Id;
                    SetLastestUpdateId(id);
                }
                finally
                {
                }
            }
            return PartialView(model);
        }

        public ActionResult UserName(int id)
        {
            var user = _context.GetUser(id);
            return PartialView(user);
        }

        public ActionResult RedirectToAlbum(int id)
        {
            var albumId = _context.GetPicture(id).AlbumId;
            return RedirectToAction("Album", "Explorer", new { id = albumId, picture = id });
        }

        public ActionResult UpdateLink(int id)
        {
            var update = _context.GetUpdate(id);
            if (update.HasAlbum())
            {
                return PartialView("AlbumLink", update.Album);
            }
            return null;
        }

        public ActionResult PictureText(int id)
        {
            var model = _context.GetPicture(id);
            return PartialView(model);
        }

        public ActionResult AlbumText(int id)
        {
            var model = _context.GetAlbum(id);
            return PartialView(model);
        }

        public ActionResult CameraText(int id)
        {
            var model = _context.GetCamera(id);
            return PartialView(model);
        }

        public ActionResult CountFriends()
        {
            int count = 0;
            try
            {
                var usr = User.Identity.Name;
                var user = _context.GetUser(usr);
                if(user!=null)
                count = user.Friends.Count();
            }finally{}
            return PartialView("Count", count);
        }

        public ActionResult CountRequests()
        {
            int count = 0;
            try
            {
                var usr = User.Identity.Name;
                var user = _context.GetUser(usr);
                if(user!=null)
                if(user.FriendRequests!=null)
                count = user.FriendRequests.Count();
            }
            finally 
            {
            }
            return PartialView("Count", count);
        }


        public ActionResult UpdatePictureCarusel(int id)
        {
            var model = _context.GetAlbumPicturesCarusel(id);
            return PartialView(model);
        }

        public ActionResult PicturesUpdate(int id)
        {
            var model = _context.GetUpdate(id).Pictures;
            return PartialView("UpdatePictureCarusel",model);
        }

        public ActionResult Blind(int id)
        {
            _context.BlindUser(User.Identity.Name, id);
            return Content("Blinded");
        }

        public ActionResult Blinded()
        {
            var model = _context.BlindedUsers(User.Identity.Name);
            var count = model.Count();
            return PartialView(model);
        }

        public ActionResult Unblind(int id)
        {
            _context.Unblind(User.Identity.Name,id);
            return Content("Unblinded");
        }

        public ActionResult TheirBlinds()
        {
            var model = _context.TheirBlinds(User.Identity.Name);
            return PartialView(model);
        }

        public void SetLastestUpdateId(int updateId)
        {
            Session["updateId"] = updateId;
        }

        public int GetLatestsUpdateId()
        {
            return (int)Session["updateId"];
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["updateId"] == null)
                Session["updateId"] = 0;
        }


    }
}
