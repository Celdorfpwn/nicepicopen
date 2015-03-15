using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class Conversation
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string LastMessageDate { get; set; }

        public virtual Photographer Sender { get; set; }

        public virtual Photographer Receiver { get; set; }

        public bool UserIsSender(int userId)
        {
            return SenderId.Equals(userId);
        }

        public bool UserIsReceiver(int userId)
        {
            return ReceiverId.Equals(userId);
        }

        public int GetPartenerId(int userId)
        {
            if (SenderId.Equals(userId))
                return ReceiverId;
            else
                return SenderId;
        }

        public string GetParneterName(int userId)
        {
            if (SenderId.Equals(userId))
                return Receiver.DescriptionName;
            else
                return Sender.DescriptionName;
        }

        public Photographer GetPartener(int userId)
        {
            if (SenderId.Equals(userId))
                return Receiver;
            else
                return Sender;
        }

        public ICollection<ConversationMessage> Messages { get; set; }
    }
}
