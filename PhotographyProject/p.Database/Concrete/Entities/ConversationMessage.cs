using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class ConversationMessage
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ConversationId { get; set; }

        public virtual Photographer Sender { get; set; }

        public string Message { get; set; }

        public bool Seen { get; set; }

        public byte[] Image { get; set; }

        public bool HasImage()
        {
            return Image != null;
        }

        public bool IsUserMessage(int userId)
        {
            return SenderId.Equals(userId);
        }
    }
}
