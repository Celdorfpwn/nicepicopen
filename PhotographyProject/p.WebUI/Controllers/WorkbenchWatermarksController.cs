using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using p.Database.Concrete.Entities;
using Workbench.Abstract;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class WorkbenchWatermarksController : Controller
    {
        private AbstractWorkbenchWatermarksContext _context { get; set; }

        public WorkbenchWatermarksController(AbstractWorkbenchWatermarksContext context)
        {
            _context = context;
        }

        public ActionResult Create()
        {
            ViewBag.Watermarks = new SelectList(new List<string> {"", "Image", "Text" });
            return PartialView();
        }

        public ActionResult LoadWatermark(string watermark)
        {
            if(watermark.Equals("Image"))
                return PartialView("EditImageWatermark");
            else
                return PartialView("EditTextWatermark");
        }

        public ActionResult CreateImageWatermark(ImageWatermark watermark,HttpPostedFileBase file)
        {
            if (file != null & ModelState.IsValid)
            {
                _context.AddImageWatermark(watermark, file.InputStream, User.Identity.Name);
            }

            return RedirectToAction("Index", "WorkbenchProfile");
        }

        public ActionResult CreateTextWatermark(TextWatermark watermark)
        {
            if (ModelState.IsValid)
            {
                _context.AddTextWatermark(watermark, User.Identity.Name);
            }
            return RedirectToAction("Index","WorkbenchProfile");
        }

        public ActionResult WatermarkSwitcher(int id, string type)
        {
            ViewBagCreateSelectList();
            type = type.Split('_')[0];
            if (type.Equals(typeof(TextWatermark).Name))
            {
                var model = _context.GetTextWatermark(id);
                return PartialView("TextWatermark",model);
            }
            else if (type.Equals(typeof(ImageWatermark).Name))
            {
                var model = _context.GetImageWatermark(id);
                return PartialView("ImageWatermark",model);
            }
            else return null;
        }

        private void ViewBagCreateSelectList()
        {
            var albums = _context.GetAlbums(User.Identity.Name);
            List<Album> elements = new List<Album>();
            elements.Add(new Album { Id = -1, Name = "All pictures" });
            elements.Add(new Album { Id = 0, Name = "Portofolio" });
            elements.AddRange(albums);
            ViewBag.Albums = new SelectList(elements, "Id", "Name");
        }

        public ActionResult SetImageWatermark(int id,int type)
        {
            var message = "Watermark set for ";
            message += _context.SetImageWatermark(id,type,User.Identity.Name);
            return RedirectToAction("Index", "WorkbenchProfile");
           // return PartialView("Message",message);
        }

        public ActionResult SetTextWatermark(int id,int type)
        {
               var message = "Watermark set for ";
               message += _context.SetTextWatermark(id, type, User.Identity.Name);
               return RedirectToAction("Index", "WorkbenchProfile");
        }

        public ActionResult RemoveWatermark(int id, string type)
        {
            _context.RemoveWatermark(id, type.Split('_')[0]);
            return RedirectToAction("Index", "WorkbenchProfile");
        }
    }
}
