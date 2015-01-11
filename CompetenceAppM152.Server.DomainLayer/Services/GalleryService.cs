using CompetenceAppM152.Common.Models;
using CompetenceAppM152.Server.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetenceAppM152.Server.DomainLayer.Services
{
    public class GalleryService
    {
        private GalleryRepository _galleryRepo = new GalleryRepository();

        public List<Gallery> GetGalleries()
        {
            return _galleryRepo.GetGalleries();
        }

        public bool CreateNewGallery(Gallery gallery)
        {
            return _galleryRepo.InsertNewGallery(gallery);
        }

        public List<Picture> GetPicturesByGallery(Guid identifier)
        {
            return _galleryRepo.GetPicturesByGalleryID(identifier);
        }

        public Gallery GetGalleryByID(Guid identifier)
        {
            return _galleryRepo.GetGalleryByID(identifier);
        }

        public bool DeleteGallery(Guid identifier)
        {
            return _galleryRepo.DeleteGallery(identifier);
        }

        public bool SaveTitlePicture(Picture titlePicture)
        {
            return _galleryRepo.SaveTitlePicture(titlePicture);
        }

        public bool SavePicture(Picture picture)
        {
            return _galleryRepo.SavePicture(picture);
        }

        public Picture GetPictureByID(Guid id)
        {
            return _galleryRepo.GetPictureByID(id);
        }

        public Gallery GetGalleryByPicture(Picture picture)
        {
            return _galleryRepo.GetGalleryByPicture(picture);
        }
    }
}
