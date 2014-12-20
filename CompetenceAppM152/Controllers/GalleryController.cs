using CompetenceAppM152.Client.UI.Web.VieModels;
using CompetenceAppM152.Common.Models;
using CompetenceAppM152.Server.DomainLayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
            _viewModel.SelectedGallery = new Gallery();
            return View(_viewModel);
        }

        public ActionResult Create(GalleryViewModel viewModel)
        {
            _galleryService.CreateNewGallery(viewModel.SelectedGallery);
            _viewModel = viewModel;

            TempData["SelectedGallery"] = viewModel.SelectedGallery;

            return View("SelectTitlePicture", _viewModel);
        }

        public ActionResult ShowPictures(string id)
        {
            GalleryPicturesViewModel viewModel = new GalleryPicturesViewModel();
            Guid identifier = new Guid(id);
            viewModel.Pictures = _galleryService.GetPicturesByGallery(identifier);
            viewModel.Gallery = _galleryService.GetGalleryByID(identifier);
            return View(viewModel);
        }

        public ActionResult DeleteGallery(string id)
        {
            Guid identifier = new Guid(id);
            _galleryService.DeleteGallery(identifier);

            _viewModel = new GalleryViewModel();
            _viewModel.Galleries = _galleryService.GetGalleries();
            return View("Index", _viewModel);
        }

        [HttpPost]
        public ActionResult UploadTitlePicture(GalleryViewModel viewModel)
        {
            viewModel.SelectedGallery = (Gallery)TempData["SelectedGallery"];

            if (viewModel.SelectedTitlePicture != null && viewModel.SelectedTitlePicture.ContentLength > 0)
            {
                string url = "C:/Users/mikemueller/Source/Repos/M152CompetenceApp/CompetenceAppM152/Content/app/img/{0}";
                url = string.Format(url, viewModel.SelectedTitlePicture.FileName);
                viewModel.SelectedTitlePicture.SaveAs(Url.Content(url));
                Picture picture = new Picture(Guid.NewGuid(), "Titlepicture", "Titlepicture", url, viewModel.SelectedGallery.Identifier);
                viewModel.SelectedGallery.TitlePicture = picture;
                _galleryService.SaveTitlePicture(picture);
            }

            _viewModel = new GalleryViewModel();
            _viewModel.Galleries = _galleryService.GetGalleries();
            return View("Index", _viewModel);
        }
    }
}