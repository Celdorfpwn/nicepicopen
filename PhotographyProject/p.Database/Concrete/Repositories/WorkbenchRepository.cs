using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class WorkbenchRepository : Repository,AbstractWorkbenchRepository
    {
        public Photographer GetPhotographer(string userName)
        {
            return _database.Photographers.FirstOrDefault(user => user.Name.Equals(userName));
        }


        public IQueryable<PictureCategory> Categories
        {
            get { return _database.Categories; }
        }

        public Album GetAlbum(int albumId)
        {
            return _database.Albums.Find(albumId);
        }


        public Picture GetPicture(int pictureId)
        {
            return _database.Pictures.Find(pictureId);
        }


        public TextWatermark GetTextWatermark(int watermarkId)
        {
            return _database.TextWatermarks.Find(watermarkId);
        }

        public ImageWatermark GetImageWatermark(int watermarkId)
        {
            return _database.ImageWatermarks.Find(watermarkId);
        }


        public PhotographerCamera GetCamera(int cameraId)
        {
            return _database.PhotographerCameras.Find(cameraId);
        }


        public void AddUpdate(UserUpdate update)
        {
            _database.UserUpdates.Add(update);
        }


        public IEnumerable<Album> Albums
        {
            get { return _database.Albums; }
        }


        public IEnumerable<Picture> Pictures
        {
            get { return _database.Pictures; }
        }
    }
}
