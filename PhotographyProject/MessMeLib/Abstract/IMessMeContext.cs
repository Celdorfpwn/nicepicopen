using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessMeLib.Concrete;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace MessMeLib.Abstract
{
    public interface IMessMeContext
    {
        int GetReceiverId(int conversationId, int userId);
        void AddMessage(string senderName, int conversationId, string message);
        IEnumerable<ConversationMessage> GetMessages(int coversationId,string userName);
        IEnumerable<Photographer> GetFriends(string username);
        Conversation GetConversation(int conversationId,string userName);
        UserConversations UserConversations(string userName);
        IEnumerable<ConversationMessage> UserNewMessages(string userName);
        IEnumerable<ConversationMessage> Messages(int conversationId,string username);
        int CountNewMessages(string userName);
        IEnumerable<ConversationMessage> ConversationNewMessages(int conversationId,int lastId);
        Photographer GetPartener(int conversationId, string userName);
        void MessagesSeen(int id, string userName);

        int UserId(string username);
        IQueryable<Conversation> Conversations(int userId);

        void Online(int id);

        void Offline(int id);

        IEnumerable<Conversation> UserActiveConversation(int userId);

        void ActiveConversation(int id,int userId);

        void RemoveConversation(int id, string username);

        IEnumerable<ConversationMessage> NewMessages(int lastId,IEnumerable<int> ids);

        void NoChange();
    }
}
