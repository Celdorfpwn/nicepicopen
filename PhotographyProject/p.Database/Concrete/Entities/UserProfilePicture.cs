using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class UserProfilePicture
    {
        [Key,ForeignKey("User")]
        public int UserId { get; set; }

        public byte[] Image { get; set; }

        public virtual Photographer User { get; set; }
    }
}
