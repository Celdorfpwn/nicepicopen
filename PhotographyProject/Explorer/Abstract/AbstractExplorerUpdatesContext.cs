using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Explorer.Abstract
{
    public interface AbstractExplorerUpdatesContext
    {
        IEnumerable<UserUpdate> GetUpdates(int id);

        PhotographerCamera GetCamera(int id);

        Quote GetQuote(int id);

        IEnumerable<Picture> GetPictures(int id);

        Album GetAlbum(int id);

        Photographer GetUser(int id);

        UserUpdate GetUpdate(int id);

        ContestPicture GetContestPicture(int id);

        PicturesUpdate GetPicturesModel(int updateId, int photographerId);

        int GetUserId(string username);

        IQueryable<UserUpdate> Updates(string username);
    }
}
