using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class CommunityRepository : Repository , AbstractCommunityRepository
    {
        public IQueryable<Picture> Pictures
        {
            get { return _database.Pictures.Include("PhotographerNotNice").Include("PhotographerNice")
                .Include("Category"); }
        }
    }
}
