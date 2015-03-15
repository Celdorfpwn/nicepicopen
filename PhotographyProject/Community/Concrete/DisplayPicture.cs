using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Concrete
{
    public class DisplayPicture
    {
        public int PhotographerId { get; set; }

        public string PhotographerName { get; set; }

        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        public int? CameraId { get; set; }

        public int PictureId { get; set; }

        public bool HasNice { get; set; }

        public bool HasNot { get; set; }
    }
}
