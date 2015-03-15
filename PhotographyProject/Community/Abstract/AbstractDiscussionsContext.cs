using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Community.Abstract
{
    public interface AbstractDiscussionsContext
    {
        void AddDiscussion(string userName, Discussion discussion);
        Discussion GetDiscussion(int discussionId,string userName);
        IEnumerable<Discussion> AllDiscussions(string search);
        IEnumerable<Discussion> UserDiscussions(string userName);
        IEnumerable<Discussion> UserParticipatingDiscussions(string userName);
        IEnumerable<Discussion> UserSeenDiscussions(string userName);
    }
}
