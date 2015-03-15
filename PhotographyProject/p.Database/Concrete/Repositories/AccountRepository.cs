using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class AccountRepository: Repository,AbstractAccountRepository
    {
        public Photographer GetPhotographer(string userName)
        {
            return _database.Photographers.FirstOrDefault(photographer => photographer.Name.Equals(userName));
        }

        public IEnumerable<Photographer> Photographers
        {
            get { return _database.Photographers; }
        }

        public void AddAccount(Photographer photographer)
        {
            _database.Photographers.Add(photographer);
        }


        public void AddProfilePicture(UserProfilePicture profilePicture)
        {
            _database.UserProfilePictures.Add(profilePicture);
        }
    }
}
