using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Abstract;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace Explorer.Concrete
{
    public class ExplorerUpdatesContext : AbstractExplorerUpdatesContext
    {
        private AbstractExplorerUpdatesRepository _repository { get; set; }

        public ExplorerUpdatesContext(AbstractExplorerUpdatesRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserUpdate> GetUpdates(int id)
        {
            return _repository.UserUpdates(id).OrderByDescending(update => update.Id);
        }

        public PhotographerCamera GetCamera(int id)
        {
            return _repository.GetCamera(id);
        }


        public Quote GetQuote(int id)
        {
            return _repository.GetQuote(id);
        }


        public IEnumerable<Picture> GetPictures(int id)
        {
            var pictureIds = _repository.GetUpdate(id).Pictures.Select(picture => picture.Id);
            List<Picture> pictures = new List<Picture>();
            foreach(var picId in pictureIds)
            {
                pictures.Add(_repository.GetPicture(picId));
            }

            return pictures;
        }


        public Album GetAlbum(int id)
        {
            return _repository.GetAlbum(id);
        }


        public Photographer GetUser(int id)
        {
            return _repository.GetUser(id);
        }


        public UserUpdate GetUpdate(int id)
        {
            return _repository.GetUpdate(id);
        }


        public PicturesUpdate GetPicturesModel(int updateId, int userId)
        {
            var pictures = GetPictures(updateId);
            int albumId = pictures.ElementAt(0).AlbumId;
            var user = GetUser(userId);
            var album = GetAlbum(albumId);

            return new PicturesUpdateModel(user,pictures,album);
        }


        public ContestPicture GetContestPicture(int id)
        {
            ContestPicture picture = _repository.GetContestPicture(id);
            return picture;
        }


        public int GetUserId(string username)
        {
            return _repository.GetUser(username).Id;
        }


        public IQueryable<UserUpdate> Updates(string username)
        {
            var user = _repository.GetUser(username);
            var friendIds = user.Friends.Select(f => f.Id);

            var updates = _repository.Updates.Where(u => friendIds.Contains(u.PhotographerId))
                .OrderByDescending(p => p.Id);

            return updates;
        }
    }
}
