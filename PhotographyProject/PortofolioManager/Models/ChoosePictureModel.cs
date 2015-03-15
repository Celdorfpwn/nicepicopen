using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace PortofolioManager.Models
{
    public class ChoosePictureModel
    {
        public ChoosePictureModel() { }
        public ChoosePictureModel(int pictureId)
        {
            PictureId = pictureId;
        }

        public bool Choosed { get; set; }
        public int PictureId { get; set; }
    }
}
