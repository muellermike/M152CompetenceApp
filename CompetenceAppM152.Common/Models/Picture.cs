using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetenceAppM152.Common.Models
{
    public class Picture
    {
        private Guid _identifier;
        private string _title;
        private string _description;
        private string _path;
        private Guid _galleryFK;
        private Gallery _gallery;

        public Picture(Guid id, string title, string description, string path, Gallery gallery)
        {
            _identifier = id;
            _title = title;
            _description = description;
            _path = path;
            _gallery = gallery;
            _galleryFK = gallery.Identifier;
        }

        public Picture(Guid id, string title, string desc, string path, Guid galleryFK)
        {
            _identifier = id;
            _title = title;
            _description = desc;
            _path = path;
            _galleryFK = galleryFK;
        }

        public Picture(Guid id, string path)
        {
            _identifier = id;
            _path = path;
        }

        public Guid Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public Guid GalleryFK
        {
            get { return _galleryFK; }
        }

        public Gallery Gallery
        {
            get { return _gallery; }
            set { _gallery = value; }
        }
    }
}
