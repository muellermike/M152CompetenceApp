using CompetenceAppM152.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompetenceAppM152.Client.UI.Web.VieModels
{
    public class GalleryViewModel
    {
        private List<Gallery> _galleries;
        private Gallery _newGallery;

        public List<Gallery> Galleries
        {
            get { return _galleries; }
            set { _galleries = value; }
        }

        public Gallery NewGallery
        {
            get { return _newGallery; }
            set { _newGallery = value; }
        }

    }
}