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

            string url = "C:/Users/mikemueller/Source/Repos/M152CompetenceApp/CompetenceAppM152/Content/app/img/{0}";
            url = string.Format(url, viewModel.SelectedGallery.Name);
            if (!Directory.Exists(url))
            {
                Directory.CreateDirectory(url);
            }

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
                string url = "C:/Users/mikemueller/Source/Repos/M152CompetenceApp/CompetenceAppM152/Content/app/img/{0}/{1}";
                url = string.Format(url, viewModel.SelectedGallery.Name, viewModel.SelectedTitlePicture.FileName);
                viewModel.SelectedTitlePicture.SaveAs(Url.Content(url));
                Picture picture = new Picture(Guid.NewGuid(), "Titlepicture", "Titlepicture", viewModel.SelectedTitlePicture.FileName, viewModel.SelectedGallery.Identifier);
                viewModel.SelectedGallery.TitlePicture = picture;
                _galleryService.SaveTitlePicture(picture);
            }

            _viewModel = new GalleryViewModel();
            _viewModel.Galleries = _galleryService.GetGalleries();
            return View("Index", _viewModel);
        }

        public ActionResult ChooseNewPicture(GalleryPicturesViewModel viewModel)
        {
            viewModel.Gallery = (Gallery)TempData["SelectedGallery"];
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UploadPicture(GalleryPicturesViewModel viewModel)
        {
            viewModel.Gallery = (Gallery)TempData["SelectedGallery"];

            if (viewModel.NewPicture != null && viewModel.NewPicture.ContentLength > 0)
            {
                string url = "C:/Users/mikemueller/Source/Repos/M152CompetenceApp/CompetenceAppM152/Content/app/img/{0}/{1}";
                url = string.Format(url, viewModel.Gallery.Name, viewModel.NewPicture.FileName);
                viewModel.NewPicture.SaveAs(Url.Content(url));
                Picture picture = new Picture(Guid.NewGuid(), "Bild", "Bild", viewModel.NewPicture.FileName, viewModel.Gallery.Identifier);
                _galleryService.SavePicture(picture);
            }

            _viewModel = new GalleryViewModel();
            _viewModel.Galleries = _galleryService.GetGalleries();
            return View("Index", _viewModel);
        }

        public ActionResult ShowPicture(string id)
        {
            PictureViewModel viewModel = new PictureViewModel();

            Picture picture = _galleryService.GetPictureByID(new Guid(id));
            Gallery gallery = _galleryService.GetGalleryByPicture(picture);

            if (picture == null || gallery == null)
            {
                return Index();
            }
            else
            {
                viewModel.Picture = picture;
                viewModel.Gallery = gallery;
                return View(viewModel);
            }

        }
    }
}