using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PicturesProvider.Abstract;
using PicturesProvider.Concrete;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class ResizerController : Controller
    {

        private IResizerContext _context { get; set; }

        public ResizerController(IResizerContext context)
        {
            _context = context;
        }

        public ActionResult ResizePicture(int id)
        {
            var model = _context.GetPicture(id);
            return PartialView(model);
        }

        public ActionResult ShowImage(int id, int width, int height)
        {
            var imgData = _context.GetResizePicture(id, width, height);
            return File(imgData,"image/jpeg");
        }

        public ActionResult PreviewResize(string preview,PictureImageModel model)
        {
            if (Request.IsAjaxRequest())
            {
                if (preview != null && preview != String.Empty)
                    return PartialView("ImagePreview", model);
                else
                    return null;
            }
            else
            {
                _context.ResizePicture(model);
                return RedirectToAction("PictureProfile", "WorkbenchPictures", new { id = model.PictureId });
            }
        }

    }
}
