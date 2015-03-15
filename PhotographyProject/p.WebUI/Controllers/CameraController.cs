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
    public class CameraController : Controller
    {

        private AbstractWorkbenchCameraContext _context { get; set; }

        public CameraController(AbstractWorkbenchCameraContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var model = _context.Cameras;
            return View(model);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(PhotographerCamera camera, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null)
            {
                _context.AddCamera(file.InputStream, camera, User.Identity.Name);
            }
            return RedirectToAction("Index", "WorkbenchProfile");
        }

    }
}
