using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Abstract;
using p.Database.Concrete.Entities;

namespace Community.Concrete
{
    public class PictureDetailsModel : PictureDetails
    {
        private Photographer _photographer { get; set; }

        private Album _album { get; set; }

        public PictureDetailsModel(Photographer photographer, Album album)
        {
            _photographer = photographer;
            _album = album;
        }

        public Photographer Photographer
        {
            get { return _photographer; }
        }

        public Album Album
        {
            get { return _album; }
        }
    }
}
