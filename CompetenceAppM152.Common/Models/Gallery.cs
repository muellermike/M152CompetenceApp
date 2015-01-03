using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetenceAppM152.Common.Models
{
    public class Gallery
    {
        private Guid _identifier;
        private string _name;
        private string _description;
        private Picture _titlePicture = null;
        private List<Picture> _pictures = null;

        public Gallery()
        {
            _identifier = Guid.NewGuid();
        }

        public Gallery(Guid id, string name, string desc, Picture pic)
        {
            _identifier = id;
            _name = name;
            _description = desc;
            _titlePicture = pic;
        }

        public Gallery(Guid id, string name, string desc)
        {
            _identifier = id;
            _name = name;
            _description = desc;
        }

        public Guid Identifier
        {
            get { return _identifier; }
            set { _identifier = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Picture TitlePicture
        {
            get { return _titlePicture; }
            set { _titlePicture = value; }
        }

        public List<Picture> Pictures
        {
            get { return _pictures; }
            set { _pictures = value; }
        }
    }
}
