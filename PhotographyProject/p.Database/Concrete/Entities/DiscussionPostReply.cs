using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class DiscussionPostReply
    {
        public int Id { get; set; }

        public int DiscussionPostId { get; set; }
        [ForeignKey("DiscussionPostId")]
        public virtual DiscussionPost DiscussionPost { get; set; }

        public string Text { get; set; }

        public int PhotographerId { get; set; }
        [ForeignKey("PhotographerId")]
        public virtual Photographer Photographer { get; set; }
    }
}
