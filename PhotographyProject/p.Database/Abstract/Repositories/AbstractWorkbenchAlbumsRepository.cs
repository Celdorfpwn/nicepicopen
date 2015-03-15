using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractWorkbenchAlbumsRepository : AbstractRepository
    {
        Album GetAlbum(int albumId);

        Photographer GetPhotographer(string userName);

        IEnumerable<PictureCategory> Categories { get; }

        IEnumerable<CommunityUpdate> CommunityUpdates { get; }

        void AddUpdate(CommunityUpdate communityUpdate);

        void AddUserUpdate(UserUpdate userUpdate);

        int? GetCameraId(string cameraModel);

    }
}
