using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class ExplorerRepository : Repository,AbstractExplorerRepository
    {
        public Photographer GetPhotographer(int photographerId)
        {
            return _database.Photographers.Find(photographerId);
        }

        public IQueryable<Photographer> Photographers
        {
            get { return _database.Photographers; }
        }


        public Photographer GetUser(string username)
        {
            return _database.Photographers.FirstOrDefault(user => user.Name.Equals(username));
        }

        public Album GetAlbum(int albumId)
        {
            return _database.Albums.Find(albumId);
        }


        public Picture GetPicture(int pictureId)
        {
            return _database.Pictures.Find(pictureId);
        }


        public IEnumerable<Album> Albums
        {
            get { return _database.Albums; }
        }


        public IEnumerable<Picture> Pictures
        {
            get { return _database.Pictures; }
        }


        public IEnumerable<PictureCategory> Categories
        {
            get { return _database.Categories; }
        }


        public IEnumerable<Visitor> Visits
        {
            get { return _database.Visitors; }
        }

        public void AddVisitor(Visitor visitor)
        {
            _database.Visitors.Add(visitor);
        }
    }
}
