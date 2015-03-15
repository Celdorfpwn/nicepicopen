using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class WorkbenchProfileRepository :Repository, AbstractWorkbenchProfileRepository
    {
        public Photographer GetPhotographer(string userName)
        {
            return _database.Photographers.FirstOrDefault(p => p.Name.Equals(userName));
        }


        public UserProfilePicture UserProfilePicture(int userId)
        {
            return _database.UserProfilePictures.Find(userId);
        }
    }
}
