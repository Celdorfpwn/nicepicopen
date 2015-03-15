using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Abstract;
using p.Database.Concrete.Entities;

namespace Community.Concrete
{
    public class RatedPictures : RatedPicturesModel
    {
        private Picture _picture { get; set; }
        private Photographer _photographer { get; set; }
        private Album _album { get; set; }

        public RatedPictures(Picture picture, Photographer photographer, Album album)
        {
            _picture = picture;
            _photographer = photographer;
            _album = album;
        }

        public Picture Picture
        {
            get { return _picture; }
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
