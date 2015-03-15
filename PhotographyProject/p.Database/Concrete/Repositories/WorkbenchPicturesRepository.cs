using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class WorkbenchPicturesRepository : Repository,AbstractWorkbenchPicturesRepository
    {
        public Photographer GetPhotographer(string userName)
        {
            return _database.Photographers.FirstOrDefault(p => p.Name.Equals(userName));
        }


        public IQueryable<Picture> Pictures
        {
            get { return _database.Pictures; }
        }

        public Album GetAlbum(int albumId)
        {
            return _database.Albums.Find(albumId);
        }


        public IEnumerable<PictureCategory> Categories
        {
            get
            {
                return _database.Categories ;
            }
        }

        public Picture GetPicture(int id)
        {
            return _database.Pictures.Find(id);
        }


        public IEnumerable<ImageWatermark> ImageWatermarks
        {
            get
            {
                return _database.ImageWatermarks;
            }
        }

        public IEnumerable<TextWatermark> TextWatermarks
        {
            get
            {
                return _database.TextWatermarks;
            }
        }

        public IEnumerable<Album> Albums(int id)
        {
            return _database.Albums.Where(album => album.PhotographerId.Equals(id));
        }


        public void AddUpdate(UserUpdate update)
        {
            _database.UserUpdates.Add(update);
        }


        public int? GetCameraId(string cameraModel)
        {
            string eq = cameraModel.ToLower();

            var camera = _database.PhotographerCameras.FirstOrDefault(c => c.Name.ToLower().Equals(eq));

            if (camera != null)
                return camera.Id;
            else
                return null;
        }


        public IEnumerable<PhotographerCamera> Cameras
        {
            get { return _database.PhotographerCameras; }
        }


        public Photographer GetPhotographer(int userId)
        {
            return _database.Photographers.Find(userId);
        }
    }
}
