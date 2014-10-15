using CompetenceAppM152.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompetenceAppM152.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            GalleryRepository repo = new GalleryRepository();
            return View();
        }
    }
}