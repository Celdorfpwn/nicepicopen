using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractExplorerPortofolioRepository : AbstractRepository
    {
        Portofolio GetPortofolio(int portofolioId);

        Picture GetPicture(int id);

        Photographer GetUser(string userName);
    }
}
