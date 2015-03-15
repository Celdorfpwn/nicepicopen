using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractDiscussionPostsRepository : AbstractRepository
    {
        IEnumerable<DiscussionPost> DiscussionPosts { get; }
        DiscussionPost GetDiscussionPost(int discussionId);
        void AddDiscussionPost(DiscussionPost discussionPost);

        Photographer GetUser(string userName);

        Discussion GetDiscussion(int id);
    }
}
