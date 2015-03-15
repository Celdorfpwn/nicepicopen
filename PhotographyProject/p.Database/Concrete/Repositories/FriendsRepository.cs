using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class FriendsRepository : Repository,AbstractFriendsRepository
    {
        public IQueryable<Photographer> Users
        {
            get { return _database.Photographers; }
        }

        public Photographer GetUser(string userName)
        {
            return _database.Photographers.FirstOrDefault(photographer => photographer.Name.Equals(userName));
        }

        public Photographer GetUser(int userId)
        {
            return _database.Photographers.Find(userId);
        }

        public FriendRequest GetRequest(int requestId)
        {
            return _database.FriendRequests.Find(requestId);
        }

        public void RemoveRequest(FriendRequest request)
        {
            _database.FriendRequests.Remove(request);
        }

        public void AddRequest(FriendRequest request)
        {
            _database.FriendRequests.Add(request);
        }


        public IQueryable<UserUpdate> Updates
        {
            get { return _database.UserUpdates.Include("Pictures"); }
        }


        public Picture GetPicture(int pictureId)
        {
            return _database.Pictures.Find(pictureId);
        }


        public Album GetAlbum(int albumId)
        {
            return _database.Albums.Find(albumId);
        }


        public PhotographerCamera GetCamera(int cameraId)
        {
            return _database.PhotographerCameras.Find(cameraId);
        }


        public UserUpdate GetUpdate(int id)
        {
            return _database.UserUpdates.FirstOrDefault(update => update.Id.Equals(id));
        }


        public void RemoveBlind(int blindId)
        {
            var blind = _database.Blinds.Find(blindId);
            _database.Blinds.Remove(blind);
        }


        public ContestPicture GetContestPicture(int contestPictureId)
        {
            return _database.ContestsPictures.Include("Picture").Include("Photographer")
                .FirstOrDefault(picture => picture.Id.Equals(contestPictureId));
        }


        public void AddConversation(Conversation conversation)
        {
            _database.Conversations.Add(conversation);
        }
    }
}
