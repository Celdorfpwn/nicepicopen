using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Abstract;
using p.Database.Concrete.Entities;

namespace Explorer.Concrete
{
    public class PicturesUpdateModel : PicturesUpdate
    {

        private Photographer _photographer { get; set; }
        private IEnumerable<Picture> _pictures { get; set; }
        private Album _album { get; set; }

        public PicturesUpdateModel(Photographer photographer,IEnumerable<Picture> pictures,Album album)
        {
            _photographer = photographer;
            _pictures = pictures;
            _album = album;
        }

        public Photographer Photographer
        {
            get { return _photographer; }
        }

        public IEnumerable<Picture> Pictures
        {
            get { return _pictures; }
        }


        public Album Album
        {
            get { return _album; }
        }
    }
}
