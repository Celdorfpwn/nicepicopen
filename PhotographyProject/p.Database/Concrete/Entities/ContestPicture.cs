using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class ContestPicture
    {
        public int Id { get; set; }

        public int PictureId { get; set; }
        [ForeignKey("PictureId")]
        public virtual Picture Picture { get; set; }

        public int PhotographerId { get; set; }
        [ForeignKey("PhotographerId")]
        public virtual Photographer Photographer { get; set; }

        public int DailyContestId { get; set; }

        public virtual ICollection<Photographer> Nices { get; set; }

        public virtual ICollection<Photographer> NotSo { get; set; }

        public bool HasNice(string userName)
        {
            var user = Nices.FirstOrDefault(u => u.Name.Equals(userName));
            if(user != null)
                return true;
            else
                return false;
        }

        public bool HasNotSo(string userName)
        {
            var user = NotSo.FirstOrDefault(u => u.Name.Equals(userName));
            if(user != null)
                return true;
            else
                return false;
        }
    }
}
