using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractWorkbenchPicturesRepository : AbstractRepository
    {
        Photographer GetPhotographer(string userName);
        IQueryable<Picture> Pictures { get; }
        Album GetAlbum(int albumId);

        IEnumerable<PictureCategory> Categories { get; }

        Picture GetPicture(int id);

        IEnumerable<ImageWatermark> ImageWatermarks { get; }

        IEnumerable<TextWatermark> TextWatermarks { get; }

        IEnumerable<Album> Albums(int id);

        void AddUpdate(UserUpdate update);

        int? GetCameraId(string cameraModel);

        IEnumerable<PhotographerCamera> Cameras { get;}

        Photographer GetPhotographer(int userId);
    }
}
