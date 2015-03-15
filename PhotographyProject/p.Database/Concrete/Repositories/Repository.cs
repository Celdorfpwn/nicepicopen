using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;

namespace p.Database.Concrete.Repositories
{
    public class Repository : AbstractRepository
    {
        protected DatabaseContext _database = new DatabaseContext();

        public void Save()
        {
            _database.SaveChanges();
        }
    }
}
