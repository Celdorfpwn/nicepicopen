using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Community.Abstract
{
    public interface AbstractDiscussionPostsContext
    {
        IEnumerable<DiscussionPost> GetDiscussionPosts(int id);

        DiscussionPost GetDiscussion(int id);

        void AddDiscussionPost(int id, string userName, DiscussionPost post);
    }
}
