using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class Blind
    {
        public int Id { get; set; }

        public int BlinderId { get; set; }
        [ForeignKey("BlinderId")]
        public virtual Photographer Blinder { get; set; }

        public int BlindedId { get; set; }
        [ForeignKey("BlindedId")]
        public virtual Photographer Blinded { get; set; }

    }
}
