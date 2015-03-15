using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class Discussion
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public int CreatorId { get; set; }
        [ForeignKey("CreatorId")]
        public virtual Photographer Creator { get; set; }

        public virtual ICollection<Photographer> UserViews { get; set; }

        public virtual ICollection<Photographer> Participants { get; set; }
    }
}
