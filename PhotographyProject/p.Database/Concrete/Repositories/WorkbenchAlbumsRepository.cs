using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class WorkbenchAlbumsRepository : Repository,AbstractWorkbenchAlbumsRepository
    {
        public Album GetAlbum(int albumId)
        {
            return _database.Albums.Find(albumId);
        }


        public Photographer GetPhotographer(string userName)
        {
            return _database.Photographers.FirstOrDefault(p => p.Name.Equals(userName));
        }


        public IEnumerable<PictureCategory> Categories
        {
            get { return _database.Categories; }
        }


        public IEnumerable<CommunityUpdate> CommunityUpdates
        {
            get { return _database.CommunityUpdates; }
        }


        public void AddUpdate(CommunityUpdate communityUpdate)
        {
            _database.CommunityUpdates.Add(communityUpdate);
        }


        public void AddUserUpdate(UserUpdate userUpdate)
        {
            _database.UserUpdates.Add(userUpdate);
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
    }
}
