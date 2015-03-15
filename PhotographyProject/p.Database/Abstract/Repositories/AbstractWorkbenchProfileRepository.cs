using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractWorkbenchProfileRepository : AbstractRepository
    {
        Concrete.Entities.Photographer GetPhotographer(string userName);

        UserProfilePicture UserProfilePicture(int userId);
    }
}
