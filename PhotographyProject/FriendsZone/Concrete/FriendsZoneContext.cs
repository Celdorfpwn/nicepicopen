using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Friends.Abstract;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace Friends.Concrete
{
    public class FriendsZoneContext : IFriendsZoneContext
    {
        private AbstractFriendsRepository _repository { get; set; }

        public FriendsZoneContext(AbstractFriendsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Photographer> GetUsers(string userName, string containsString)
        {

            containsString = containsString.ToLower();

            var user = _repository.GetUser(userName);

            var userFriendRequest = user.FriendRequestsISent.Select(f => f.PhotographerId);
            var userRequests = user.FriendRequests.Select(f => f.FriendRequestId);
            var userFriends = user.Friends;
            var users = _repository.Users.ToList();
            var blinded = user.MyBlinds.Select(blind => blind.BlindedId).ToList();
            var theirBlinds = user.TheirBlinds.Select(blind => blind.BlinderId).ToList();

            var result = users.Where(p => p.Contains(containsString)
                    && !p.Name.Equals(userName)
                    && !userFriendRequest.Contains(p.Id)
                    && !userFriends.Contains(p)
                    && !userRequests.Contains(p.Id)
                    && !blinded.Contains(p.Id)
                    && !theirBlinds.Contains(p.Id));

            return result.OrderBy(r => r.DescriptionName.IndexOf(containsString));
        }

        public void AddRequest(string userName, int friendId)
        {
            int userId = _repository.GetUser(userName).Id;
            FriendRequest request = CreateRequest(userId, friendId);
            _repository.AddRequest(request);
            _repository.Save();
        }

        private FriendRequest CreateRequest(int requesterId, int targetId)
        {
            return new FriendRequest
            {
                FriendRequestId = requesterId,
                PhotographerId = targetId,
            };
        }

        public Conversation AcceptRequest(int requestId)
        {
            var request = _repository.GetRequest(requestId);
            var user = _repository.GetUser(request.FriendRequestId);
            var requester = _repository.GetUser(request.PhotographerId);

            Conversation conversation = new Conversation
            {
                ReceiverId = requester.Id,
                SenderId = user.Id
            };
            _repository.AddConversation(conversation);

            user.FriendsIAccept.Add(requester);
            _repository.RemoveRequest(request);
            _repository.Save();
            conversation.Sender = user;
            conversation.Receiver = requester;

            return conversation;
        }

        public IEnumerable<Photographer> GetFriends(string userName, string containsString)
        {
            var user = _repository.GetUser(userName);
            if (containsString.Equals(String.Empty))
                return user.Friends;
            containsString = containsString.ToLower();

            return user.Friends.Where(friend => friend.Contains(containsString))
                .OrderBy(r => r.DescriptionName.IndexOf(containsString));
        }

        public IEnumerable<FriendRequest> GetUserFriendRequests(string userName)
        {
            return _repository.GetUser(userName).FriendRequests;
        }

        public Photographer GetUser(string userName)
        {
            return _repository.GetUser(userName);
        }

        public IEnumerable<UserUpdate> GetLastestUpdates(string userName,int take,int skip)
        {
            var user = _repository.GetUser(userName);
            if (user != null)
            {
                var userFriends = _repository.GetUser(userName).Friends.Select(friend => friend.Id);

                var updates = _repository.Updates.OrderByDescending(update => update.Id);

                return updates.Where(update => userFriends.Contains(update.PhotographerId)).Skip(skip).Take(take);
            }
            else
                return new List<UserUpdate>();
           
        }

        public Picture GetPicture(int pictureId)
        {
            return _repository.GetPicture(pictureId);
        }

        public Album GetAlbum(int albumId)
        {
            return _repository.GetAlbum(albumId);
        }


        public PhotographerCamera GetCamera(int cameraId)
        {
            return _repository.GetCamera(cameraId);
        }


        public string GetUserName(int userId)
        {
            return _repository.GetUser(userId).Name;
        }


        public Photographer GetUser(int id)
        {
            return _repository.GetUser(id);
        }


        public IEnumerable<Picture> GetAlbumPicturesCarusel(int id)
        {
            var album = _repository.GetAlbum(id);
            var pictures = album.Pictures.OrderByDescending(picture => picture.Rating)
                .Take(12);
            return pictures;
        }


        public UserUpdate GetUpdate(int id)
        {
            var update = _repository.GetUpdate(id);
            return update;
        }


        public void BlindUser(string userName, int blindedUserId)
        {
            var user = _repository.GetUser(userName);
            var blinder = _repository.GetUser(blindedUserId);
            user.RemoveFriend(blinder);

            var blind = CreateBlind(user.Id, blinder.Id);
            user.MyBlinds.Add(blind);
            _repository.Save();
        }

        private Blind CreateBlind(int blinder, int blinded)
        {
            return new Blind { BlinderId = blinder, BlindedId = blinded };
        }


        public IEnumerable<Blind> BlindedUsers(string userName)
        {
            var user = _repository.GetUser(userName);
            return user.MyBlinds;
        }


        public void Unblind(string userName,int blindId)
        {

            _repository.RemoveBlind(blindId);
            _repository.Save();
        }


        public IEnumerable<Blind> TheirBlinds(string userName)
        {
            var user = _repository.GetUser(userName);
            return user.TheirBlinds;
        }


        public ContestPicture GetContestPicture(int contestPictureId)
        {
            var picture = _repository.GetContestPicture(contestPictureId);
            return picture;
        }


        public IEnumerable<Photographer> Suggestions(string userName)
        {
            var user = _repository.GetUser(userName);

            var userFriendRequest = user.FriendRequestsISent.Select(f => f.PhotographerId);
            var userRequests = user.FriendRequests.Select(f => f.FriendRequestId);
            var userFriends = user.Friends;
            var users = _repository.Users.ToList();
            var blinded = user.MyBlinds.Select(blind => blind.BlindedId).ToList();
            var theirBlinds = user.TheirBlinds.Select(blind => blind.BlinderId).ToList();

            var result = users.Where(p=>!p.Name.Equals(userName)
                    && !userFriendRequest.Contains(p.Id)
                    && !userFriends.Contains(p)
                    && !userRequests.Contains(p.Id)
                    && !blinded.Contains(p.Id)
                    && !theirBlinds.Contains(p.Id));

            return result.OrderBy(u => Guid.NewGuid());
        }
    }
}
