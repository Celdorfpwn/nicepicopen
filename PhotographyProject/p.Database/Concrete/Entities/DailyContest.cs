using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class DailyContest
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public virtual ICollection<ContestPicture> Pictures { get; set; }

        public int? WinnerId { get; set; }
        [ForeignKey("WinnerId")]
        public virtual ContestPicture Winner { get; set; }
    }
}
