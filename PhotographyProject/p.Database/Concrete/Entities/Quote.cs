using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class Quote
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }
    }
}
