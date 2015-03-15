using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace MessMeLib.Concrete
{
    public class UserConversations
    {
        public int UserId { get; private set; }

        public IEnumerable<Conversation> Conversations { get; private set; }

        public UserConversations(int userId, IEnumerable<Conversation> conversations)
        {
            UserId = userId;
            Conversations = conversations;
        }
    }
}
