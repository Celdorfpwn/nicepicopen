using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class PortofolioPictureView
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }

        public virtual Photographer Photographer { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public int PortofolioId { get; set; }
    }
}
