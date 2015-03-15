using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{

    public class ExplorerPortofolioRepository : Repository,AbstractExplorerPortofolioRepository
    {
        public Portofolio GetPortofolio(int portofolioId)
        {
            return _database.Portofolio.Find(portofolioId);
        }


        public Picture GetPicture(int id)
        {
            return _database.Pictures.Find(id);
        }


        public Photographer GetUser(string userName)
        {
            return _database.Photographers.FirstOrDefault(p => p.Name.Equals(userName));
        }
    }
}
