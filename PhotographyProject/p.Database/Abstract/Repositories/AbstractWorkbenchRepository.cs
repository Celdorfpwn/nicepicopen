using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractWorkbenchRepository : AbstractRepository
    {
        Album GetAlbum(int albumId);
        Photographer GetPhotographer(string userName);
        IQueryable<PictureCategory> Categories { get; }
        Picture GetPicture(int pictureId);
        TextWatermark GetTextWatermark(int watermarkId);
        ImageWatermark GetImageWatermark(int watermarkId);
        PhotographerCamera GetCamera(int cameraId);
        void AddUpdate(UserUpdate update);
        IEnumerable<Album> Albums { get; }
        IEnumerable<Picture> Pictures { get; }
    }
}
