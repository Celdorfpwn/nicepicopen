using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Friends.Abstract
{
    public interface IFriendsZoneContext
    {
        IEnumerable<Photographer> GetUsers(string userName, string cointainsString);
        Photographer GetUser(string userName);
        void AddRequest(string userName, int friendId);
        Conversation AcceptRequest(int requestId);
        IEnumerable<Photographer> GetFriends(string userName,string containsString);
        IEnumerable<FriendRequest> GetUserFriendRequests(string userName);
        IEnumerable<UserUpdate> GetLastestUpdates(string userName,int take,int skip);
        Picture GetPicture(int pictureId);
        Album GetAlbum(int albumId);
        PhotographerCamera GetCamera(int cameraId);
        ContestPicture GetContestPicture(int contestPictureId);
        string GetUserName(int userId);
        Photographer GetUser(int id);
        IEnumerable<Picture> GetAlbumPicturesCarusel(int id);
        UserUpdate GetUpdate(int id);
        void BlindUser(string userName, int blindedUserId);
        IEnumerable<Blind> BlindedUsers(string userName);
        IEnumerable<Blind> TheirBlinds(string userName);
        void Unblind(string userName,int userId);

        IEnumerable<Photographer> Suggestions(string username);
    }
}
