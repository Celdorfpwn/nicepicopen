using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class ImagesRepository : Repository,AbstractImagesRepository
    {

        public Photographer GetUser(int userId)
        {
            return _database.Photographers.Find(userId);
        }

        public Photographer GetUser(string username)
        {
            return _database.Photographers.FirstOrDefault(p => p.Name.Equals(username));
        }

        public Album GetAlbum(int albumId)
        {
            return _database.Albums.Find(albumId);
        }

        public Picture GetPicture(int pictureId)
        {
            var picture = _database.Pictures.Find(pictureId);

            return _database.Pictures.Find(pictureId);
        }

        public PhotographerCamera GetCamera(int cameraId)
        {
            return _database.PhotographerCameras.Find(cameraId);
        }


        public Quote GetQuote(int id)
        {
            return _database.Quotes.Find(id);
        }


        public ConversationMessage GetMessage(int id)
        {
            return _database.ConversationsMessages.Find(id);
        }


        public byte[] ProfilePicture(int id)
        {
            return _database.UserProfilePictures.Find(id).Image;
        }
    }
}
