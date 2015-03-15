using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class UserUpdate
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }

        public virtual Photographer Photographer { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public int? AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        public virtual Album Album { get; set; }

        public int? QuoteId { get; set; }
        [ForeignKey("QuoteId")]
        public virtual Quote Quote { get; set; }

        public int? ContestPictureId { get; set; }
        [ForeignKey("ContestPictureId")]
        public virtual ContestPicture ContestPicture { get; set; }

        public int? DailyContestWinnerId { get; set; }
        [ForeignKey("DailyContestWinnerId")]
        public virtual DailyContest DailyContestWinner { get; set; }

        public bool HasAlbum()
        {
            if (AlbumId == null)
                return false;
            else
                return true;
        }

        public bool HasPicture()
        {

            if (Pictures == null)
                return false;
            if (Pictures.Count == 0)
                return false;
            return true;
        }

        public bool HasQuote()
        {
            if (QuoteId != null)
                return true;
            else
                return false;
        }

        public bool HasContestPicture()
        {
            if (ContestPictureId != null)
                return true;
            else
                return false;
        }

        public bool HasDailyContestWinner()
        {
            if (DailyContestWinnerId != null)
                return true;
            else
                return false;
        }
    }
}
