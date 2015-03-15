using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractFriendsRepository : AbstractRepository
    {
        IQueryable<Photographer> Users { get; }
        Photographer GetUser(string userName);
        Photographer GetUser(int userId);
        FriendRequest GetRequest(int requestId);
        void RemoveRequest(FriendRequest request);
        void AddRequest(FriendRequest request);
        IQueryable<UserUpdate> Updates { get; }
        Picture GetPicture(int pictureId);
        Album GetAlbum(int albumId);
        PhotographerCamera GetCamera(int cameraId);

        UserUpdate GetUpdate(int id);

        void RemoveBlind(int blindId);

        ContestPicture GetContestPicture(int contestPictureId);

        void AddConversation(Conversation conversation);
    }
}
