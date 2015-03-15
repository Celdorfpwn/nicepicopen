using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using p.Database.Concrete.Entities;
using p.Database.Concrete.Repositories;

namespace p.WebUI
{
    public class MessMe : Hub
    {

        public void Connect(int id)
        {
            var database = new DatabaseContext();
            var connectionId = Context.ConnectionId;
            var user = database.Photographers.Find(id);
            user.ChatConnectionId = connectionId;
            user.Online = true;
            database.SaveChanges();
            Clients.Client(connectionId).connectionId(connectionId);
        }

        public void SendToUser(string message,int conversationId,int userId,string myConnectionId,int myId,bool record,string interval)
        {
            var database = new DatabaseContext();
            var send = new
            {
                ConversationId = conversationId,
                User = false,
                Message = message,
                Interval = interval
            };
            var my = new
            {
                ConversationId = conversationId,
                User = true,
                Message = message,
                Interval = interval
            };
            Clients.Client(myConnectionId).sendMessage(my);
            var clientId = database.Photographers.Find(userId).ChatConnectionId;
            Clients.Client(clientId).sendMessage(send);

            if (record)
            {
                var dbmessage = new ConversationMessage { ConversationId = conversationId, SenderId = myId, Message = message };
                database.ConversationsMessages.Add(dbmessage);
                database.SaveChanges();
            }

        }

        public void NiceToAll(int pictureId,int userId)
        {
            var send = new{
                Id = pictureId,
                UserId = userId
            };
            Clients.Others.addNice(send);
        }

        public void NotToAll(int pictureId, int userId)
        {
            var send = new
            {
                Id = pictureId,
                UserId = userId
            };
            Clients.Others.addNot(send);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            Clients.Caller.reconnect(Context.ConnectionId);
            return base.OnReconnected();
        }

        public override Task OnDisconnected()
        {
            DatabaseContext db = new DatabaseContext();

            var user = db.Photographers.FirstOrDefault(f => f.ChatConnectionId.Equals(Context.ConnectionId));
            if (user != null)
            {
                user.Online = false;
                db.SaveChanges();
            }
            return base.OnDisconnected();
        }

    }
}