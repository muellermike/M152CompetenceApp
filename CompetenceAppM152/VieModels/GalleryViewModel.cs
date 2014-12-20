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
        private Gallery _selectedGallery;
        private HttpPostedFileBase _selectedTitlePicture;

        public List<Gallery> Galleries
        {
            get { return _galleries; }
            set { _galleries = value; }
        }

        public Gallery SelectedGallery
        {
            get { return _selectedGallery; }
            set { _selectedGallery = value; }
        }

        public HttpPostedFileBase SelectedTitlePicture
        {
            get { return _selectedTitlePicture; }
            set { _selectedTitlePicture = value; }
        }

    }
}