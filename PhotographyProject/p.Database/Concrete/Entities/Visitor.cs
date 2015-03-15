using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class Visitor
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public int UserVisitorId { get; set; }
        [ForeignKey("UserVisitorId")]
        public virtual Photographer UserVisitor { get; set; }

        public int VisitedId { get; set; }
        [ForeignKey("VisitedId")]
        public virtual Photographer Visited { get; set; }
    }
}
