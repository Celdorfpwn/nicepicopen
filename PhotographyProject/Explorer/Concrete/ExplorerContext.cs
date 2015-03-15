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
    public class ExplorerContext : IExplorerContext
    {
        private AbstractExplorerRepository _repository { get; set; }

        public ExplorerContext(AbstractExplorerRepository repository)
        {
            _repository = repository;
        }

        public Photographer GetPhotographer(int userId,string username)
        {
            var user = _repository.GetPhotographer(userId);
            var visitor = _repository.GetUser(username);
            if (!VisitorExists(visitor.Id, user.Id))
            {
                var visit = new Visitor { VisitedId = user.Id, UserVisitorId = visitor.Id, Date = TodaysDate() };
                _repository.AddVisitor(visit);
                _repository.Save();
            }

            return user;
        }

        private string TodaysDate()
        {
            var date = DateTime.Today;
            return date.ToShortDateString();
        }

        private bool VisitorExists(int visitorId, int visitedId)
        {
            string date = TodaysDate();
            var visitor = _repository.Visits
                .FirstOrDefault(v => v.UserVisitorId.Equals(visitorId)
                    && v.VisitedId.Equals(visitedId)
                    && v.Date.Equals(date));
            if (visitor == null)
                return false;
            else
                return true;
        }


        public IEnumerable<Photographer> SearchFriends(int userId, string contains)
        {
            var user = _repository.GetPhotographer(userId);
            if (contains.Equals(String.Empty))
                return user.Friends;
            contains = contains.ToLower();

            return user.Friends.Where(friend => friend.Name.ToLower().Contains(contains))
                .OrderBy(r => r.Name.IndexOf(contains));
        }

        public Album GetAlbum(int albumId)
        {
            return _repository.GetAlbum(albumId);
        }


        public byte[] GetPictureImage(int pictureId)
        {
            return _repository.GetPicture(pictureId).ImageWithWatermark;
        }

        public Picture GetPicture(int pictureId,string username)
        {
            var picture = _repository.GetPicture(pictureId);
            var user = _repository.GetUser(username);
            if (!picture.PhotographerViews.Contains(user))
            {
                picture.PhotographerViews.Add(user);
                _repository.Save();
            }
            return _repository.GetPicture(pictureId);
        }


        public void AddNice(int pictureId, string userName)
        {
            var picture = _repository.GetPicture(pictureId);
            if (!picture.HasNice(userName))
            {
                var user = _repository.GetUser(userName);
                picture.PhotographerNice.Add(user);
                _repository.Save();
            }
        }


        public int GetPictureNices(int pictureId)
        {
            return _repository.GetPicture(pictureId).PhotographerNice.Count;
        }


        public void AddPictureView(int pictureId, string username)
        {
            var picture = _repository.GetPicture(pictureId);
            var user = _repository.GetUser(username);
            if (!picture.PhotographerViews.Contains(user))
            {
                picture.PhotographerViews.Add(user);
                _repository.Save();
            }
        }


        public IEnumerable<PictureComment> GetPictureComments(int pictureId)
        {
            return _repository.GetPicture(pictureId).Comments.OrderByDescending(comment => comment.Id);
        }

        public void AddComment(int pictureId, string username, string comment)
        {
            var picture = _repository.GetPicture(pictureId);
            var user = _repository.GetUser(username);
            picture.Comments.Add(CreateComment(comment,user.Id));
            _repository.Save();

        }

        private PictureComment CreateComment(string comment, int userId)
        {
            return new PictureComment { Comment = comment, UserId = userId};
        }

        public IEnumerable<Picture> AllPictures(int userId, int categoryId, int albumId)
        {
            var user = _repository.GetPhotographer(userId);
            IEnumerable<Picture> result = null;
            if (albumId == 0)
            {
                var albumIds = user.Albums.Select(album => album.Id);
                var pictures = _repository.Pictures.Where(picture => albumIds.Contains(picture.AlbumId));
                if (categoryId == 0)
                {
                    result = pictures;
                }
                else
                {
                    result = pictures.Where(picture => picture.CategoryId.Equals(categoryId));
                }
            }
            else
            {
                if (categoryId == 0)
                {
                    result = _repository.GetAlbum(albumId).Pictures;
                }
                else
                {
                    result = _repository.GetAlbum(albumId).Pictures.Where(pic => pic.CategoryId.Equals(categoryId));
                }
            }
            if (result == null)
                return new List<Picture>();
            else
                return result.OrderByDescending(picture => picture.Rating);
        }


        public IEnumerable<Album> UserAlbums(int userId)
        {
            return _repository.GetPhotographer(userId).Albums.Where(album => album.Pictures.Count!=0);
        }

        public IEnumerable<PictureCategory> Categories(int userId)
        {
            var user = _repository.GetPhotographer(userId);
            var pictures = user.Albums.SelectMany(album => album.Pictures);
            List<int> categoriesId = new List<int>();
            foreach (var picture in pictures)
            {
                if (!categoriesId.Contains(picture.CategoryId))
                    categoriesId.Add(picture.CategoryId);
            }
            return _repository.Categories.Where(category => categoriesId.Contains(category.Id)); 
        }


        public Portofolio GetPortofolio(int id)
        {
            return _repository.GetPhotographer(id).Portofolio;
        }


        public IEnumerable<Picture> GetTopPictures(int userId, int count)
        {
            var user = _repository.GetPhotographer(userId);
            var albums = user.Albums;
            var pictures = albums.SelectMany(album => album.Pictures);
            
            return pictures
                .OrderByDescending(picture => ((picture.PhotographerNice.Count*picture.PhotographerViews.Count)+picture.PhotographerViews.Count))
                .Take(count);
        }


        public int GetPictureAlbumId(int pictureId)
        {
            return _repository.GetPicture(pictureId).AlbumId;
        }


        public IEnumerable<Visitor> UserVisitors(string username)
        {
            var userId = _repository.GetUser(username).Id;

            return _repository.Visits.Where(v => v.VisitedId.Equals(userId))
                .OrderByDescending(v => v.Id);
        }
    }
}
