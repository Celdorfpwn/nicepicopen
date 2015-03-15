using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class DiscussionPostsRepository : Repository, AbstractDiscussionPostsRepository
    {
        public IEnumerable<DiscussionPost> DiscussionPosts
        {
            get { return _database.DiscussionPosts; }
        }

        public DiscussionPost GetDiscussionPost(int discussionId)
        {
            return _database.DiscussionPosts.Include("Photographer")
                .FirstOrDefault(dP => dP.Id.Equals(discussionId));
        }

        public void AddDiscussionPost(DiscussionPost discussionPost)
        {
            _database.DiscussionPosts.Add(discussionPost);
        }


        public Photographer GetUser(string userName)
        {
            return _database.Photographers.FirstOrDefault(user => user.Name.Equals(userName));
        }


        public Discussion GetDiscussion(int id)
        {
            return _database.Discussions.Include("Participants")
                .FirstOrDefault(d => d.Id.Equals(id));
        }
    }
}
