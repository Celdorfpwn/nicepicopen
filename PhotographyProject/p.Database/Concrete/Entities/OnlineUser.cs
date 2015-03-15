using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class OnlineUser
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
    }
}
