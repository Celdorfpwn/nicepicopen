using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contests.Abstract;

namespace p.WebUI.Controllers
{
    public class DailyContestsController : Controller
    {

        private AbstractDailyContestsContext _context { get; set; }

        public DailyContestsController(AbstractDailyContestsContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LatestContests()
        {
            var model = _context.GetLatestsContests();
            if (model != null)
                return PartialView(model);
            else return null;
        }

        public ActionResult CreateContest()
        {
            _context.CreateContext();
            return View();
        }

        public ActionResult TodaysContest()
        {
            var model = _context.Todays();
            if (model == null)
            {
                var now = DateTime.Now;
                var n = now.Hour;
                var m = 20 - n;
                return PartialView("NoContest",m);
            }
            return PartialView(model);
        }

        public ActionResult DateTimeNow()
        {
            string date = DateTime.Now.ToString();
            return Content(date);
        }

        public ActionResult BestTodays()
        {
            var model = _context.BestTodays();
            return PartialView("ContestPictures", model);
        }

        public ActionResult ContestPictures(int id)
        {
            var model = _context.ContestPictures(id);
            return PartialView(model);
        }

        public ActionResult CurrentUser()
        {
            string userName = User.Identity.Name;
            if (_context.UserParticipating(userName))
            {

                return PartialView("UserPicture");
            }
            else
            {
                var user = _context.GetUser(userName);
                return PartialView("UserParticipate",user);
            }
        }

        public ActionResult MyPicture()
        {
            var userName = User.Identity.Name;
            var picture = _context.GetUserPicture(userName);
            return PartialView(picture);
        }

        public ActionResult Nice(int id)
        {
            var userName = User.Identity.Name;
            _context.Nice(userName, id);
            return Content("Nice");
        }

        public ActionResult NotSo(int id)
        {
            var userName = User.Identity.Name;
            _context.NotNice(userName, id);
            return Content("NotNice");
        }

        public ActionResult UserParticipate()
        {
            var user = _context.GetUser(User.Identity.Name);
            return PartialView(user);
        }

        public ActionResult UserPictures()
        {
            var model = _context.GetUserPictures(User.Identity.Name);
            return PartialView(model);
        }

        public ActionResult Participate(int id)
        {
            var username = User.Identity.Name;
            _context.Participate(username,id);
            return RedirectToAction("Index", "WorkbenchProfile");
        }

        public ActionResult ContestPictureDetails(int id)
        {
            var model = _context.GetContestPicture(id);
            return PartialView(model);
        }

    }
}
