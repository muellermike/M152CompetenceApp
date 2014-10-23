using CompetenceAppM152.Client.UI.Web.VieModels;
using CompetenceAppM152.Common.Models;
using CompetenceAppM152.Server.DomainLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompetenceAppM152.Client.UI.Web.Controllers
{
    public class GalleryController : Controller
    {
        private GalleryViewModel _viewModel;
        private GalleryService _galleryService = new GalleryService();

        // GET: Gallery
        public ActionResult Index()
        {
            _viewModel = new GalleryViewModel();
            _viewModel.Galleries = _galleryService.GetGalleries();

            return View(_viewModel);
        }

        public ActionResult NewGallery()
        {
            _viewModel = new GalleryViewModel();
            _viewModel.NewGallery = new Gallery();
            return View(_viewModel);
        }

        public ActionResult Create(GalleryViewModel viewModel)
        {
            _galleryService.CreateNewGallery(viewModel.NewGallery);
            return null;
        }
    }
}