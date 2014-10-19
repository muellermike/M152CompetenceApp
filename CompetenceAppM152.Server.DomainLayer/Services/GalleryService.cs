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
    }
}
