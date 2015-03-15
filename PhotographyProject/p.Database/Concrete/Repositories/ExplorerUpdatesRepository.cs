using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;
using System.Data.Entity;

namespace p.Database.Concrete.Repositories
{
    public class ExplorerUpdatesRepository : Repository,AbstractExplorerUpdatesRepository
    {
        public IEnumerable<UserUpdate> UserUpdates(int userId)
        {
            return _database.UserUpdates.Where(update => update.PhotographerId.Equals(userId));
        }

        public PhotographerCamera GetCamera(int cameraId)
        {
            return _database.PhotographerCameras.Find(cameraId);
        }

        public UserUpdate GetUpdate(int updateId)
        {
            return _database.UserUpdates.Include("Pictures").FirstOrDefault(update => update.Id.Equals(updateId));
        }

        public Quote GetQuote(int quoteId)
        {
            return _database.Quotes.Find(quoteId);
        }

        public Picture GetPicture(int pictureId)
        {
            return _database.Pictures.Find(pictureId);
        }

        public Album GetAlbum(int id)
        {
            return _database.Albums.Find(id);
        }


        public Photographer GetUser(int id)
        {
            return _database.Photographers.Find(id);
        }


        public ContestPicture GetContestPicture(int id)
        {
            return _database.ContestsPictures.Include("Picture").Include("Photographer")
                .Include("Nices").Include("NotSo")
                .FirstOrDefault(picture => picture.Id.Equals(id));
        }


        public Photographer GetUser(string username)
        {
            return _database.Photographers.FirstOrDefault(p => p.Name.Equals(username));
        }

        public IQueryable<UserUpdate> Updates
        {
            get { return _database.UserUpdates.Include(u => u.Album)
                .Include(p => p.Photographer).Include(u => u.Pictures); }
        }
    }
}
