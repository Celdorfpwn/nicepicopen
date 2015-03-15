using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;

namespace p.Database.Concrete.Repositories
{
    public class ConversationsRepository : Repository,AbstractConversationsRepository
    {

        public IQueryable<Conversation> Conversations
        {
            get
            {
                var conv =  _database.Conversations.Include(c => c.Sender)
                    .Include(c => c.Receiver).Include(c => c.Messages);

                return conv;
            }
        }


        public void AddConversation(Conversation conversation)
        {
            _database.Conversations.Add(conversation);
        }

        public Photographer GetUser(string username)
        {
            return _database.Photographers.FirstOrDefault(user=> user.Name.Equals(username));
        }


        public Conversation GetConversation(int conversationId)
        {
            return _database.Conversations.Find(conversationId);
        }


        public IEnumerable<ConversationMessage> ConversationMessages(int conversationId)
        {
            var mess = from message in _database.ConversationsMessages
                       where message.ConversationId.Equals(conversationId)
                       select message;
            return mess;
        }


        public IEnumerable<Conversation> LazyConversations
        {
            get { return _database.Conversations; }
        }



        public IQueryable<ConversationMessage> Messages
        {
            get { return _database.ConversationsMessages.Include("Sender"); }
        }


        public Photographer GetPhotographer(int partenerId)
        {
            return _database.Photographers.Include(p => p.ActiveConversations).FirstOrDefault(p =>  p.Id.Equals(partenerId));
        }


        public void AddOnline(int id)
        {
            _database.Photographers.Find(id).Online = true;
        }

        public void GoOffline(int id)
        {
            _database.Photographers.Find(id).Online = false;
        }


        public void AddMessage(ConversationMessage convMessage)
        {
            _database.ConversationsMessages.Add(convMessage);
        }


        public Photographer GetUserActive(int userId)
        {
            return _database.Photographers.Find(userId);
        }


        public void NoChange()
        {
            _database.NotChange();
        }
    }
}
