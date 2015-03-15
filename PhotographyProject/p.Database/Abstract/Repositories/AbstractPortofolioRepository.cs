using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractPortofolioRepository : AbstractRepository
    {
        void AddPortofolio(Portofolio portofolio);
        Portofolio GetPortofolio(int portofolioId);
        Portofolio GetPortofolio(string userName);
        Photographer GetPhotographer(string userName);
        IEnumerable<Picture> Pictures { get; }
        IEnumerable<CommunityUpdate> CommunityUpdates { get; }

        void AddUpdate(CommunityUpdate communityUpdate);
    }
}
