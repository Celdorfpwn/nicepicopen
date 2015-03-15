using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractWorkbenchCamerasRepository : AbstractRepository
    {
        PhotographerCamera GetCamera(int cameraId);
        IEnumerable<PhotographerCamera> Cameras { get; }
        Photographer GetUser(string username);
        void AddUpdate(UserUpdate update);

        void AddCamera(PhotographerCamera camera);
    }
}
