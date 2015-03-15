using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class Portofolio
    {
        public Portofolio()
        {
            Pictures = new HashSet<Picture>();
        }

        public int PhotographerId { get; set; }

        public Photographer Photographer { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public int?
            CoverId { get; set; }
        [ForeignKey("CoverId")]
        public Picture Cover { get; set; }


        public bool HasCover()
        {
            if (CoverId != null)
                return true;
            else
                return false;
        }

        public virtual ICollection<PortofolioPictureNice> PicturesNices { get; set; }

        public virtual ICollection<PortofolioPictureView> PicturesViews { get; set; }

        public virtual ICollection<PortofolioPictureComment> PictureComments { get; set; }
    }
}
