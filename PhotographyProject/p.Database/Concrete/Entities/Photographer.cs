using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using p.Database.Abstract.Entities;

namespace p.Database.Concrete.Entities
{
    public class Photographer
    {
        public Photographer()
        {
            FacebookCredentials = new ExternalInformation();
            TwitterCredentials = new ExternalInformation();
            FriendsIAccept = new HashSet<Photographer>();
            FriendsIAdd = new HashSet<Photographer>();
            FriendRequests = new HashSet<FriendRequest>();
        }

        public string DescriptionName
        {
            get
            {
                if (FullName != null)
                    return FullName;
                else
                    return Name;
            }
        }

        public bool Contains(string search)
        {
            if (FullName != null)
            {
                if (FullName.ToLower().Contains(search))
                    return true;
                else
                    return Name.ToLower().Contains(search);
            }
            else
                return Name.ToLower().Contains(search);
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string ChatConnectionId { get; set; }

        public bool Online { get; set; }

        public byte[] SmallProfile { get; set; }

        public ExternalInformation FacebookCredentials { get; set; }

        public ExternalInformation TwitterCredentials { get; set; }

        public virtual ICollection<Photographer> FriendsIAdd { get; set; }

        public virtual ICollection<Photographer> FriendsIAccept { get; set; }

        public virtual ICollection<Blind> MyBlinds { get; set; }

        public virtual ICollection<Blind> TheirBlinds { get; set; }

        public IEnumerable<Photographer> Friends
        {
            get
            {
                return FriendsIAdd.Concat(FriendsIAccept);
            }
        }

        public void RemoveFriend(Photographer friend)
        {
            if (FriendsIAdd.Contains(friend))
                FriendsIAdd.Remove(friend);
            else if(FriendsIAccept.Contains(friend))
            {
                FriendsIAccept.Remove(friend);
            }
        }

        public virtual ICollection<Album> Albums { get; set; }

        public virtual ICollection<FriendRequest> FriendRequests { get; set; }

        public virtual ICollection<FriendRequest> FriendRequestsISent { get; set; }

        public virtual ICollection<Conversation> MyConversations { get; set; }

        public virtual ICollection<Conversation> TheirConversations { get; set; }

        public virtual ICollection<Conversation> ActiveConversations { get; set; }

        public IEnumerable<Conversation> Conversations
        {
            get
            {
                return MyConversations.Concat(TheirConversations);
            }
        }

        public virtual ICollection<Picture> Favorites { get; set; }

        public virtual ICollection<TextWatermark> TextWatermarks { get; set; }

        public virtual ICollection<ImageWatermark> ImageWatermarks { get; set; }

        public virtual Portofolio Portofolio { get; set; }

        public virtual ICollection<CommunityUpdate> CheckedUpdates { get; set; }

        public IEnumerable<Watermark> Watermarks
        {
            get
            {
                return
                    TextWatermarks.AsEnumerable<Watermark>()
                    .Concat(ImageWatermarks.AsEnumerable<Watermark>());
            }
        }

        public virtual ICollection<Quote> Quotes { get; set; }

        public bool HasPortofolio()
        {
            if (Portofolio == null)
                return false;
            else
                return true;
        }

        public bool HasAlbums()
        {
            if (Albums != null)
                if (Albums.Count != 0)
                    return true;
            return false;
        }

        public void AddFacebookCredentials(ExternalInformation cred)
        {
            this.FacebookCredentials = cred;
        }

        public void AddTwitterCredentials(ExternalInformation cred)
        {
            this.TwitterCredentials = cred;
        }
    }
}
