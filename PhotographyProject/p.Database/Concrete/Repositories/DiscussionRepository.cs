using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;
using System.Data.Entity;

namespace p.Database.Concrete.Repositories
{
    public class DiscussionRepository : Repository, AbstractDiscussionsRepository
    {
        public IEnumerable<Discussion> Discussions
        {
            get
            {
                return _database.Discussions.Include("Creator").
                    Include("UserViews").Include("Participants");
            }
        }

        public void AddDiscussion(Discussion discossion)
        {
            _database.Discussions.Add(discossion);
        }


        public Photographer GetUser(string userName)
        {
           return _database.Photographers.FirstOrDefault(p => p.Name.Equals(userName)); 
        }


        public Discussion GetDiscussion(int discussionId)
        {
            return _database.Discussions.Find(discussionId);
        }
    }
}
