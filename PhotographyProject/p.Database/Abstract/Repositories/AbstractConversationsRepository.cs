using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractConversationsRepository : AbstractRepository
    {
        Photographer GetUser(string username);
        Photographer GetUserActive(int id);
        Conversation GetConversation(int conversationId);
        IQueryable<Conversation> Conversations { get; }
        void AddConversation(Conversation conversation);
        IEnumerable<Conversation> LazyConversations { get; }
        IEnumerable<ConversationMessage> ConversationMessages(int conversationId);


        IQueryable<ConversationMessage> Messages { get; }

        Photographer GetPhotographer(int partenerId);

        void AddOnline(int id);

        void GoOffline(int id);

        void AddMessage(ConversationMessage convMessage);

        void NoChange();
    }
}
