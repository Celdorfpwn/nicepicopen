using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;
using Workbench.Abstract;

namespace Workbench.Concrete
{
    public class WorkbenchAlbumsContext : AbstractWorkbenchAlbumsContext
    {
        private AbstractWorkbenchAlbumsRepository _repository { get; set; }

        public WorkbenchAlbumsContext(AbstractWorkbenchAlbumsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Picture> AlbumPictures(int albumId)
        {
            return _repository.GetAlbum(albumId).Pictures;
        }


        public IEnumerable<Album> Albums(string userName)
        {
            var user = _repository.GetPhotographer(userName);
            return user.Albums;
        }


        public void AddAlbum(Album model, IEnumerable<System.IO.Stream> streams, string userName,int category)
        {
            var user = _repository.GetPhotographer(userName);
            model.Pictures = CreatePictures(streams,category,user,model.Name);
            user.Albums.Add(model);
            var userUpdate = CreateUpdate(model,user.Id);
            _repository.AddUserUpdate(userUpdate);
            _repository.Save();
        }

        private UserUpdate CreateUpdate(Album model,int userId)
        {
            return new UserUpdate { PhotographerId = userId, Album = model };
        }

        private ICollection<Picture> CreatePictures(IEnumerable<Stream> streams,int category,
            Photographer user,string albumName)
        {
            List<Picture> pictures = new List<Picture>();
            foreach (var stream in streams)
            {
                var imageData = ImageProcessor.GetImageDataFromStream(stream,Convert.ToInt32(stream.Length));
                if (ImageProcessor.CheckIfFileIsImage(imageData))
                {
                    pictures.Add(CreatePicture(imageData,category,user,albumName));
                }
            }

            return pictures;
        }

        private Picture CreatePicture(byte[] image,int category,Photographer user,string albumName)
        {
            var picture = new Picture
            { CategoryId = category, Image = image,
                AlbumName = albumName,
                PhotographerId = user.Id,
                PhotographerName = user.Name
            };

            string cameraModel = ImageProcessor.GetImageCameraModel(image);
            if (cameraModel != null)
            {
                picture.CameraId = _repository.GetCameraId(cameraModel);
            }

            return picture;
        }


        public Stream GetAlbumZip(int id)
        {
            var album = _repository.GetAlbum(id);
            var outpoutStream = new MemoryStream();
            using (var zip = new ZipFile())
            {
                foreach (var picture in album.Pictures)
                {
                    zip.AddEntry(picture.Id.ToString() + ".jpeg", picture.Image);
                }
                zip.Save(outpoutStream);
            }

            outpoutStream.Position = 0;
            return outpoutStream;
        }

        public Album GetAlbum(int id)
        {
            return _repository.GetAlbum(id);
        }


        public IEnumerable<PictureCategory> CategoriesFiltered(string userName)
        {
            var user = _repository.GetPhotographer(userName);
            var pictures = user.Albums.SelectMany(album => album.Pictures);
            List<int> categoriesId = new List<int>();
            foreach (var picture in pictures)
            {
                if (!categoriesId.Contains(picture.CategoryId))
                    categoriesId.Add(picture.CategoryId);
            }
            return _repository.Categories.Where(category => categoriesId.Contains(category.Id)); 
        }


        public void AddPictureQuick(int id, Stream stream)
        {
            var imageData = ImageProcessor.GetImageDataFromStream(stream, Convert.ToInt32(stream.Length));
            if (ImageProcessor.CheckIfFileIsImage(imageData))
            {
                var album = _repository.GetAlbum(id);
                int categoryId = _repository.Categories.First().Id;
                var picture = CreatePicture(imageData,categoryId,album.Photographer,album.Name);
                album.Pictures.Add(picture);
                _repository.Save();
            }
        }


        public bool IsViral(int albumId)
        {
            return AlbumIsViral(albumId);
        }

        private bool AlbumIsViral(int albumId)
        {
            var updates = _repository.CommunityUpdates;

            var update = updates.FirstOrDefault(up => up.IsAlbumUpdate(albumId));
            if (update != null)
                return true;
            else
                return false;
        }

        public void AlbumGoViral(int albumId)
        {
            if (!AlbumIsViral(albumId))
            {
                _repository.AddUpdate(new CommunityUpdate { AlbumId = albumId });
                _repository.Save();
            }
        }


        public void ChangeDownload(int id, bool Downloadable)
        {
            _repository.GetAlbum(id).Downloadable = Downloadable;
            _repository.Save();
        }


        public Stream GetAlbumZipForUsers(int id)
        {
            var album = _repository.GetAlbum(id);
            var outpoutStream = new MemoryStream();
            using (var zip = new ZipFile())
            {
                foreach (var picture in album.Pictures.Where(picture => picture.Downloadable))
                {
                    zip.AddEntry(picture.Id.ToString() + ".jpeg", picture.ImageWithWatermark);
                }
                zip.Save(outpoutStream);
            }

            outpoutStream.Position = 0;
            return outpoutStream;
        }




        public IEnumerable<PictureCategory> Categories
        {
            get { return _repository.Categories; }
        }


        public IEnumerable<Picture> AlbumCaruselPictures(int id)
        {
            return _repository.GetAlbum(id).Pictures.OrderByDescending(picture => picture.Rating)
                .Skip(1).Take(7);
        }


        public Picture AlbumBestPicture(int id)
        {
            var album = _repository.GetAlbum(id);
            if(album.Pictures!=null)
                if (album.Pictures.Count() != 0)
                {
                    var bestPicture = album.Pictures.OrderByDescending(picture => picture.Rating)
                        .Take(1);
                    return bestPicture.ElementAt(0);
                }
            return null;
        }
    }
}
