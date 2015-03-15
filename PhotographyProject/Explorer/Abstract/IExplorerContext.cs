using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Explorer.Abstract
{
    public interface IExplorerContext
    {
        Photographer GetPhotographer(int userId,string username);
        byte[] GetPictureImage(int pictureId);
        IEnumerable<Photographer> SearchFriends(int userId, string contains);
        Album GetAlbum(int albumId);
        Picture GetPicture(int albumId,string username);
        int GetPictureAlbumId(int pictureId);
        void AddNice(int pictureId, string userName);
        int GetPictureNices(int pictureId);
        void AddPictureView(int pictureId, string username);
        IEnumerable<PictureComment> GetPictureComments(int pictureId);
        void AddComment(int pictureId, string username, string comment);
        IEnumerable<Picture> AllPictures(int userId, int categoryId, int albumId);
        IEnumerable<Album> UserAlbums(int userId);
        IEnumerable<PictureCategory> Categories(int userId);
        Portofolio GetPortofolio(int id);

        IEnumerable<Picture> GetTopPictures(int userId, int count);

        IEnumerable<Visitor> UserVisitors(string username);
    }
}
