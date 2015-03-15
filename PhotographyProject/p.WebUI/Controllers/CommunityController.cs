using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Community.Abstract;
using p.Database.Concrete.Entities;

namespace p.WebUI.Controllers
{
    [Authorize]
    public class CommunityController : Controller
    {
        private ICommunityContext _context { get; set; }

        public CommunityController(ICommunityContext context)
        {
            _context = context;
        }

    }
}
