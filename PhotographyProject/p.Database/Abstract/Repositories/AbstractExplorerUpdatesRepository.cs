using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractExplorerUpdatesRepository : AbstractRepository
    {
        IEnumerable<UserUpdate> UserUpdates(int userId);
        PhotographerCamera GetCamera(int cameraId);
        UserUpdate GetUpdate(int updateId);
        Quote GetQuote(int quoteId);
        Picture GetPicture(int pictureId);
        Album GetAlbum(int id);

        Photographer GetUser(int id);

        ContestPicture GetContestPicture(int id);

        Photographer GetUser(string username);
        IQueryable<UserUpdate> Updates { get; }
    }
}
