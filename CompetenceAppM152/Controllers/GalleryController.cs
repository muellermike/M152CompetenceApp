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

        // GET: Gallery
        public ActionResult Index()
        {

            GalleryService galleryService = new GalleryService();
            _viewModel = new GalleryViewModel();
            _viewModel.Galleries = galleryService.GetGalleries();

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
            return null;
        }
    }
}