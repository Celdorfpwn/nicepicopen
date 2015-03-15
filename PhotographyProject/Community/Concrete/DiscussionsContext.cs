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
    public class DiscussionsContext : AbstractDiscussionsContext
    {

        private AbstractDiscussionsRepository _repository { get; set; }

        public DiscussionsContext(AbstractDiscussionsRepository repository)
        {
            _repository = repository;
        }


        public void AddDiscussion(string userName, Discussion discussion)
        {
            discussion.CreatorId = _repository.GetUser(userName).Id;
            _repository.AddDiscussion(discussion);
            _repository.Save();
        }

        public IEnumerable<Discussion> AllDiscussions(string search)
        {
            return _repository.Discussions
                .OrderByDescending(d => d.Participants.Count * d.UserViews.Count);
        }

        public IEnumerable<Discussion> UserDiscussions(string userName)
        {
            var user = _repository.GetUser(userName);
            return _repository.Discussions.Where(d => d.CreatorId.Equals(1))
                .OrderByDescending(d => d.Id);
        }

        public IEnumerable<Discussion> UserParticipatingDiscussions(string userName)
        {
            var user = _repository.GetUser(userName);
            return _repository.Discussions.Where(d => d.Participants.Contains(user))
                .OrderByDescending(d => d.Participants.Count * d.UserViews.Count);
        }


        public IEnumerable<Discussion> UserSeenDiscussions(string userName)
        {
            var user = _repository.GetUser(userName);
            return _repository.Discussions.Where(d => d.UserViews.Contains(user))
                .OrderByDescending(d => d.Participants.Count * d.UserViews.Count);
        }


        public Discussion GetDiscussion(int discussionId,string userName)
        {
            var user = _repository.GetUser(userName);
            var discussion = _repository.GetDiscussion(discussionId);
            if (!discussion.UserViews.Contains(user))
            {
                discussion.UserViews.Add(user);
                _repository.Save();
            }
            return discussion;
        }
    }
}
