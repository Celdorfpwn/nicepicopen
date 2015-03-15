using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p.Database.Concrete.Entities
{
    public class CommunityUpdate
    {
        public int Id { get; set; }

        public int? AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        public Album Album { get; set; }

        public int? PortofolioId { get; set; }
        [ForeignKey("PortofolioId")]
        public Portofolio Portofolio { get; set; }

        public bool HasAlbum()
        {
            if (AlbumId != null)
                return true;
            else
                return false;
        }

        public bool HasPortofolio()
        {
            if (PortofolioId != null)
            {
                return true;
            }
            else
                return false;
        }

        public bool IsAlbumUpdate(int albumId)
        {
            if (AlbumId != null)
                if (AlbumId.Equals(albumId))
                    return true;
            return false;
        }

        public bool IsPortofolioUpdate(int portofolioId)
        {
            if (PortofolioId != null)
                if (PortofolioId.Equals(portofolioId))
                    return true;
            return false;
        }
    }
}
