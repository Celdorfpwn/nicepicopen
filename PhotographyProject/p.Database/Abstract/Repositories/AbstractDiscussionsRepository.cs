using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractDiscussionsRepository : AbstractRepository
    {
        IEnumerable<Discussion> Discussions { get; }
        void AddDiscussion(Discussion discossion);

        Photographer GetUser(string userName);

        Discussion GetDiscussion(int discussionId);
    }
}
