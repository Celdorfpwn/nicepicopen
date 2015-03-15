using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractAccountRepository : AbstractRepository
    {
        Photographer GetPhotographer(string userName);
        IEnumerable<Photographer> Photographers { get; }
        void AddAccount(Photographer photographer);

        void AddProfilePicture(UserProfilePicture profilePicture);
    }
}
