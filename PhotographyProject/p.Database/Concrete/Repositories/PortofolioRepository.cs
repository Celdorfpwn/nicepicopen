using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class PortofolioRepository : Repository,AbstractPortofolioRepository
    {
        public void AddPortofolio(Portofolio portofolio)
        {
            _database.Portofolio.Add(portofolio);
        }

        public Portofolio GetPortofolio(int portofolioId)
        {
            return _database.Portofolio.Find(portofolioId);
        }

        public Portofolio GetPortofolio(string userName)
        {
            int userId = _database.Photographers.FirstOrDefault(p => p.Name.Equals(userName)).Id;
            return _database.Portofolio.Find(userId);
        }


        public Photographer GetPhotographer(string userName)
        {
            return _database.Photographers.FirstOrDefault(p => p.Name.Equals(userName));
        }


        public IEnumerable<Picture> Pictures
        {
            get
            {
                return _database.Pictures;
            }
        }


        public IEnumerable<CommunityUpdate> CommunityUpdates
        {
            get { return _database.CommunityUpdates; }
        }


        public void AddUpdate(CommunityUpdate communityUpdate)
        {
            _database.CommunityUpdates.Add(communityUpdate);
        }
    }
}
