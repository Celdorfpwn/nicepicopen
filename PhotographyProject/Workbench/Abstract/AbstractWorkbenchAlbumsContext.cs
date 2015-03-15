using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Workbench.Abstract
{
    public interface AbstractWorkbenchAlbumsContext
    {
        IEnumerable<Picture> AlbumPictures(int albumId);

        IEnumerable<PictureCategory> Categories { get; }

        IEnumerable<Album> Albums(string userName);

        void AddAlbum(Album model, IEnumerable<System.IO.Stream> streams, string userName,int category);

        Stream GetAlbumZip(int id);

        Album GetAlbum(int id);

        IEnumerable<PictureCategory> CategoriesFiltered(string userName);

        void AddPictureQuick(int id, Stream stream);

        bool IsViral(int albumId);

        void AlbumGoViral(int albumId);

        void ChangeDownload(int id, bool Downloadable);

        Stream GetAlbumZipForUsers(int id);

        IEnumerable<Picture> AlbumCaruselPictures(int id);

        Picture AlbumBestPicture(int id);
    }
}
