using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class WorkbenchCamerasRepository : Repository,AbstractWorkbenchCamerasRepository
    {

        public PhotographerCamera GetCamera(int cameraId)
        {
            return _database.PhotographerCameras.Find(cameraId);
        }

        public IEnumerable<PhotographerCamera> Cameras
        {
            get { return _database.PhotographerCameras; }
        }

        public Photographer GetUser(string username)
        {
            return _database.Photographers.FirstOrDefault(user => user.Name.Equals(username));
        }


        public void AddUpdate(UserUpdate update)
        {
            _database.UserUpdates.Add(update);
        }


        public void AddCamera(PhotographerCamera camera)
        {
            _database.PhotographerCameras.Add(camera);
        }
    }
}
