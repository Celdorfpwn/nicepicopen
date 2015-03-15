using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessMeLib.Abstract;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace MessMeLib.Concrete
{
    public class MessMeContext : IMessMeContext
    {
        private AbstractConversationsRepository _repository { get; set; }

        public MessMeContext(AbstractConversationsRepository repository)
        {
            _repository = repository;
        }


        public void AddMessage(string senderName, int conversationId, string message)
        {

            int senderId = _repository.GetUser(senderName).Id;
            var convMessage = CreateConversationMessage(senderId,conversationId, message);
            _repository.AddMessage(convMessage);
            _repository.Save();
        }

        private Conversation CreateConversation(int senderId, int receiverId)
        {
            return new Conversation
            {
                SenderId = senderId,
                ReceiverId = receiverId
            };
        }


        private ConversationMessage CreateConversationMessage(int senderId,int conversationId, string message)
        {
            return new ConversationMessage
            {
                SenderId = senderId,
                Message = message,
                ConversationId = conversationId
            };
        }

        private bool ConversationExists(int senderId, int receiverId)
        {
            var conversations = _repository.Conversations.ToList();
            if(conversations
                .Exists(conversation => conversation.SenderId.Equals(senderId) && conversation.ReceiverId.Equals(receiverId)))
                return true;
            else
             return conversations
                .Exists(conversation => conversation.SenderId.Equals(receiverId) && conversation.ReceiverId.Equals(senderId));
        }

        public int GetUserId(string username)
        {
            return _repository.GetUser(username).Id;
        }

        public int GetReceiverId(int conversationId, int userId)
        {
            var conversation = _repository.GetConversation(conversationId);
            if (conversation.SenderId.Equals(userId))
                return conversation.ReceiverId;
            else
                return conversation.SenderId;
        }

        public IEnumerable<ConversationMessage> GetMessages(int coversationId,string userName)
        {
            var messages = _repository.ConversationMessages(coversationId).ToList();
            var userId = GetUserId(userName);

            var underead = messages.Where(m => m.Seen.Equals(false) && !m.SenderId.Equals(userId));
            if (underead != null)
            {
                if (underead.Count() != 0)
                {
                    foreach (var m in underead)
                    {
                        m.Seen = true;
                    }
                    _repository.Save();
                }
            }

            return messages;
        }

        public IEnumerable<Photographer> GetFriends(string username)
        {
            return _repository.GetUser(username).Friends;
        }


        public Conversation GetConversation(int conversationId,string username)
        {
            var conversation = _repository.GetConversation(conversationId);
            var user = _repository.GetUser(username);

            if (user.ActiveConversations.Contains(conversation))
                return null;
            else
            {
                user.ActiveConversations.Add(conversation);
                _repository.Save();
                return conversation;
            }
            
        }


        private IEnumerable<Conversation> UserConversations(Photographer user)
        {
            int userId = user.Id;
            FillConversations(user);

            var conversations = _repository.Conversations
                .Where(conversation => conversation.ReceiverId.Equals(userId) || conversation.SenderId.Equals(userId));


            return conversations.ToList();
        }

        private void FillConversations(Photographer user)
        {
            var friends = user.Friends;
            foreach (var friend in friends)
            {
                if (!ConversationExists(friend.Id, user.Id))
                {
                    var c = CreateConversation(user.Id, friend.Id);
                    _repository.AddConversation(c);
                }
            }
            _repository.Save();
        }


        public UserConversations UserConversations(string userName)
        {
            var user = _repository.GetUser(userName);
            var conversations = UserConversations(user).ToList();
            return new UserConversations(user.Id, conversations);
        }

        private IEnumerable<ConversationMessage> GetUserNewMessages(string userName)
        {
            var user = _repository.GetUser(userName);
            var conversationIds = user.Conversations.Select(c => c.Id);

            List<ConversationMessage> result = new List<ConversationMessage>();

            foreach (var cId in conversationIds)
            {
                var messages = _repository.ConversationMessages(cId).Where(m => m.Seen.Equals(false));
                var newMessages = messages.Where(m => !m.SenderId.Equals(user.Id))
                    .OrderByDescending(m => m.Id);
                if (newMessages != null)
                {
                    if (newMessages.Count() != 0)
                    {
                        result.Add(newMessages.ElementAt(0));
                    }
                }
            }

            return result;
        }

        private IEnumerable<ConversationMessage> GetUserMessages(string userName)
        {
            var user = _repository.GetUser(userName);
            var conversationIds = user.Conversations.Select(c => c.Id);

            List<ConversationMessage> result = new List<ConversationMessage>();

            foreach (var cId in conversationIds)
            {
                var messages = _repository.ConversationMessages(cId);
                var newMessages = messages.Where(m => !m.SenderId.Equals(user.Id))
                    .OrderByDescending(m => m.Id);
                if (newMessages != null)
                {
                    if (newMessages.Count() != 0)
                    {
                        result.Add(newMessages.ElementAt(0));
                    }
                }
            }

            return result;
        }

        public IEnumerable<ConversationMessage> UserNewMessages(string userName)
        {
            var messages = GetUserMessages(userName);
            return messages.OrderByDescending(m => m.Id)
                .ThenByDescending(m => m.Seen);
        }

        public int CountNewMessages(string userName)
        {
            return GetUserNewMessages(userName).Count();
        }

        public IEnumerable<ConversationMessage> Messages(int conversationId,string username)
        {
            var messages = _repository.Messages.Where(m => m.ConversationId.Equals(conversationId));

            return messages;
        }


        public IEnumerable<ConversationMessage> ConversationNewMessages(int conversationId,int lastId)
        {
            var messages = _repository.Messages.Where(m => m.ConversationId.Equals(conversationId))
                .Where(m => m.Id > lastId);

            return messages;
        }


        public Photographer GetPartener(int conversationId, string userName)
        {
            var conversation = _repository.GetConversation(conversationId);
            var userId = _repository.GetUser(userName).Id;
            int partenerId;
            if (conversation.UserIsReceiver(userId))
                partenerId = conversation.SenderId;
            else
                partenerId = conversation.ReceiverId;

            var partener = _repository.GetPhotographer(partenerId);
            return partener;
        }


        public void MessagesSeen(int id, string userName)
        {
            var userId = _repository.GetUser(userName).Id;
            var messages = _repository.Messages.Where(m => m.ConversationId.Equals(id));
            var mustSeen = messages.Where(m => m.Seen.Equals(false) && !m.SenderId.Equals(userId));
            foreach (var m in mustSeen)
            {
                m.Seen = true;
            }
            _repository.Save();

        }



        public int UserId(string username)
        {
            return _repository.GetUser(username).Id;
        }

        public IQueryable<Conversation> Conversations(int userId)
        {
            return _repository.Conversations.Where(c => c.SenderId.Equals(userId) || c.ReceiverId.Equals(userId));
        }


        public void Online(int id)
        {
            var user = _repository.GetPhotographer(id);
            if (!user.Online)
            {
                user.Online = true;
                _repository.Save();
            }

        }

        public void Offline(int id)
        {
            var user = _repository.GetPhotographer(id);
            if (user.Online)
            {
                user.Online = false;
                _repository.Save();
            }
        }


        public IEnumerable<Conversation> UserActiveConversation(int userId)
        {
            var conversations = _repository.GetUserActive(userId).ActiveConversations;
            return conversations;
        }


        public void ActiveConversation(int id,int userId)
        {
            var conv = _repository.GetConversation(id);
            var user = _repository.GetPhotographer(userId);
            if (user.ActiveConversations.Contains(conv))
            {
            }
            else
            {
                user.ActiveConversations.Add(conv);
            }
            _repository.Save();
        }


        public void RemoveConversation(int id, string username)
        {
            var user = _repository.GetUser(username);
            var conversation = _repository.GetConversation(id);
            user.ActiveConversations.Remove(conversation);
            _repository.Save();
        }


        public IEnumerable<ConversationMessage> NewMessages(int lastId, IEnumerable<int> ids)
        {

            List<ConversationMessage> newMessages = new List<ConversationMessage>();
            foreach (var id in ids)
            {
                var messages = _repository.ConversationMessages(id).Where(m => m.Id > lastId);
                newMessages.AddRange(messages);
            }
            return newMessages.OrderBy(m => m.Id);
        }


        public void NoChange()
        {
            _repository.NoChange();
        }
    }
}
