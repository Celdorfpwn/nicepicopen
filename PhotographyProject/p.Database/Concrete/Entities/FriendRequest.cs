using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class FriendRequest
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }

        public int FriendRequestId { get; set; }

        public virtual Photographer PhotographerRequesting { get; set; }
    }
}
