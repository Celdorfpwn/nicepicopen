using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Abstract;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace Community.Concrete
{
    public class DiscussionPostsContext : AbstractDiscussionPostsContext
    {
        private AbstractDiscussionPostsRepository _repository { get; set; }

        public DiscussionPostsContext(AbstractDiscussionPostsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DiscussionPost> GetDiscussionPosts(int id)
        {
            var discussionPosts = _repository.DiscussionPosts
                .Where(dP => dP.DiscussionId.Equals(id));

            return discussionPosts;
        }

        public DiscussionPost GetDiscussion(int id)
        {
            return _repository.GetDiscussionPost(id);
        }


        public void AddDiscussionPost(int id, string userName, DiscussionPost post)
        {
            var user = _repository.GetUser(userName);
            post.DiscussionId = id;
            post.PhotographerId = user.Id;
            var discussion = _repository.GetDiscussion(id);
            if (!discussion.Participants.Contains(user))
            {
                discussion.Participants.Add(user);
            }

            _repository.AddDiscussionPost(post);


            _repository.Save();
        }
    }
}
