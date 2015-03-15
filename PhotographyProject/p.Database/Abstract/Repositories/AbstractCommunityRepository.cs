using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractCommunityRepository : AbstractRepository
    {
        IQueryable<Picture> Pictures { get; }
    }
}
