using CompetenceAppM152.Client.UI.Web.VieModels;
using CompetenceAppM152.Server.DomainLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompetenceAppM152.Client.UI.Web.Controllers
{
    public class PictureController : Controller
    {
        private GalleryService _galleryService = new GalleryService();
        private GalleryPicturesViewModel _viewModel;

        // GET: Picture
        public ActionResult Index(string id)
        {
            Guid identifier = new Guid(id);
            _viewModel = new GalleryPicturesViewModel();
            _viewModel.Pictures = _galleryService.GetPicturesByGallery(identifier);
            return View();
        }
    }
}