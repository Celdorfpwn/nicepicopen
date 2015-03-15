using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractImagesRepository : AbstractRepository
    {
        Photographer GetUser(int userId);
        Photographer GetUser(string username);
        Album GetAlbum(int albumId);
        Picture GetPicture(int pictureId);
        PhotographerCamera GetCamera(int cameraId);

        Quote GetQuote(int id);

        ConversationMessage GetMessage(int id);

        byte[] ProfilePicture(int id);
    }
}
