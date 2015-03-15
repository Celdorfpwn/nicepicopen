using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class Album
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }

        public string Description { get; set; }

        public bool Downloadable { get; set; }

        public virtual Photographer Photographer { get; set; }

        [Required(ErrorMessage="This album needs a name")]
        public string Name { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public string GetShorterDescription()
        {
            if (Description != null)
            {
                if (Description.Length > 45)
                {
                    return Description.Substring(0, 42) + "...";
                }
                else
                {
                    return Description;
                }
            }
            return String.Empty;
        }
    }
}
