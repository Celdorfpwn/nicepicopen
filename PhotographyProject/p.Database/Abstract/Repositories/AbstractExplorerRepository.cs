using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractExplorerRepository : AbstractRepository
    {
        Photographer GetPhotographer(int photographerId);
        Photographer GetUser(string username);
        IQueryable<Photographer> Photographers { get; }
        Album GetAlbum(int albumId);
        Picture GetPicture(int pictureId);
        IEnumerable<Album> Albums { get; }
        IEnumerable<Picture> Pictures { get; }
        IEnumerable<Visitor> Visits { get; }
        void AddVisitor(Visitor visitor);
        IEnumerable<PictureCategory> Categories { get; }
    }
}
