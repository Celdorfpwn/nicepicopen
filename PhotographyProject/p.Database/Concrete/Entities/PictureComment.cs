using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class PictureComment
    {
        public int Id { get; set; }

        public int PictureId { get; set; }

        public int UserId { get; set; }

        public string Comment { get; set; }

        public virtual Photographer Photographer { get; set; }
    }
}
