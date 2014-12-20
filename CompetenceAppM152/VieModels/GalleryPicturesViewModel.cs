using CompetenceAppM152.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompetenceAppM152.Client.UI.Web.VieModels
{
    public class GalleryPicturesViewModel
    {
        public List<Picture> Pictures { get; set; }

        public Gallery Gallery { get; set; }
    }
}