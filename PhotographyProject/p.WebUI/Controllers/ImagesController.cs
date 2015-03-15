using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicturesProvider.Abstract;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class ImagesController : Controller
    {

        private IImageProviderContext _context { get; set; }

        public ImagesController(IImageProviderContext context)
        {
            _context = context;
        }

        public ActionResult PictureCaruselImage(int id,int? key)
        {
            var imageData = _context.GetPictureCaruselImage(id);
            return File(imageData,"image/jpeg");
        }

        public ActionResult AlbumProfileImage(int id,int? key)
        {
            var imageData = _context.AlbumProfilePicture(id);
            if (imageData != null)
                return File(imageData, "image/jpeg");
            else
                return File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "empty.jpg"), "image/jpeg");
        }

        public ActionResult UserProfileImage(int id,int? key)
        {
            var imageData = _context.UserProfile(id);
            return File(imageData, "image/jpeg");
        }

        public ActionResult CameraImage(int id,int? key)
        {
            var imageData = _context.UserCameraImage(id);
            return File(imageData, "image/jpeg");
        }

        public ActionResult AlbumPictureCarusel(int id,int? key)
        {
            var imageData = _context.GetPictureAlbumCaruselImage(id);
            return File(imageData, "image/jpeg");
        }

        public ActionResult DetailsPicture(int id,int? key)
        {
            var imageData = _context.GetDetailsPictureImage(id);
            return File(imageData,"image/jpeg");
        }

        public ActionResult DetailsPictureWithWatermark(int id,int? randKey)
        {
            var imageData = _context.AlbumDetailsPictureWithWatermark(id);
            return File(imageData, "image/jpeg");
        }

        public ActionResult UserProfileSmall(int id)
        {
            var imageData = _context.GetUserProfileSmall(id);
            return File(imageData, "image/jpeg");
        }

        public ActionResult ComunityPicture(int id,int? key)
        {
            byte[] imageData = _context.GetComunityPicture(id);

            return File(imageData,"images/jpeg");
        }

        public ActionResult ImageWatermarkProfile(int id,int? key)
        {
            byte[] imageData = _context.UserProfileImageWatermark(User.Identity.Name, id);
            return File(imageData,"images/jpeg");
        }

        public ActionResult TextWatermarkProfile(int id,int? key)
        {
            byte[] imageData = _context.UserProfileTexWatermark(User.Identity.Name, id);
            return File(imageData, "images/jpeg");
        }

        public ActionResult Quote(int id)
        {
            byte[] imageData = _context.GetQuote(id);
            return File(imageData, "images/jpeg");
        }

        public ActionResult MessageImage(int id)
        {
            byte[] imageData = _context.GetMessageImage(id);
            return File(imageData, "image/jpeg");
        }


        public ActionResult Optim(string id)
        {
            return PartialView(id);
        }

        public ActionResult Empty()
        {
            return File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "empty.jpg"), "image/jpeg");
        }

    }
}
