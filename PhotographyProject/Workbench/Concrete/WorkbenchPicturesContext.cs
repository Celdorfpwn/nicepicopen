using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;
using Workbench.Abstract;

namespace Workbench.Concrete
{
    public class WorkbenchPicturesContext : AbstractWorkbenchPicturesContext
    {
        private AbstractWorkbenchPicturesRepository _repository { get; set; }

        public WorkbenchPicturesContext(AbstractWorkbenchPicturesRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Picture> UserPictures(int categoryId, int albumId,string userName)
        {
            var user = _repository.GetPhotographer(userName);
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

        public IEnumerable<PictureCategory> Categories
        {
            get { return _repository.Categories; }
        }

        public void AddPictures(int albumId, int categoryId, IEnumerable<System.IO.Stream> stream)
        {
            var album = _repository.GetAlbum(albumId);
            var pictures = CreatePictures(categoryId, stream);
            foreach (var picture in pictures)
            {
                album.Pictures.Add(picture);
            }
            var update = CreatePictureUpdate(album.PhotographerId);
            _repository.AddUpdate(update);
            foreach (var picture in pictures)
            {
                update.Pictures.Add(picture);
            }
            _repository.Save();
        }

        private UserUpdate CreatePictureUpdate(int userId)
        {
            return new UserUpdate { Pictures = new List<Picture>(), PhotographerId = userId };
        }

        private ICollection<Picture> CreatePictures(int categoryId,IEnumerable<Stream> streams)
        {
            List<Picture> pictures = new List<Picture>();
            foreach (var stream in streams)
            {
                var imageData = ImageProcessor.GetImageDataFromStream(stream, Convert.ToInt32(stream.Length));
                if (ImageProcessor.CheckIfFileIsImage(imageData))
                {
                    var picture = CreatePicture(categoryId, imageData);

                    pictures.Add(picture);
                }
            }

            return pictures;
        }

        private Picture CreatePicture(int categoryId,byte[] imageData)
        {
            var picture = new Picture { CategoryId = categoryId, Image = imageData };
            string cameraModel = ImageProcessor.GetImageCameraModel(imageData);
            if (cameraModel != null)
            {
                var cameraId = _repository.GetCameraId(cameraModel);
                picture.CameraId = cameraId;
            }
            return picture;
        }


        public IEnumerable<Picture> GetByCategory(int id, int categoryId)
        {
            var album = _repository.GetAlbum(id);

            if (categoryId == 0)
                return album.Pictures;

            return album.Pictures.Where(picture => picture.CategoryId.Equals(categoryId))
                .OrderByDescending(picture => picture.Id);
        }


        public Picture GetPicture(int id)
        {
            Picture picture = _repository.GetPicture(id);
            return picture;
        }


        public void ChangeCategory(int id, int categoryId)
        {
            var category = _repository.Categories.FirstOrDefault(c => c.Id.Equals(categoryId));
            if (category != null)
            {
                _repository.GetPicture(id).CategoryId = categoryId;
                _repository.Save();
            }
        }


        public void ChangeDownload(int id, bool Donwloadable)
        {
            _repository.GetPicture(id).Downloadable = Donwloadable;
            _repository.Save();
        }


        public IEnumerable<WatermarkType> PictureWatermarks(int id,string userName)
        {
            var picture = _repository.GetPicture(id);
            int watermarkId;
            WatermarkType type = null;
            List<WatermarkType> marksList = new List<WatermarkType>();
            if (picture.ImageWatermarkId != null)
            {
                watermarkId = (int)picture.ImageWatermarkId;
                type = BuildMark(watermarkId, "image", picture.ImageWatermark.WatermarkName);
                marksList.Add(type);
                marksList.Add(BuildMark(0, "none", "none"));
            }
            else if (picture.TextWatermarkId != null)
            {
                watermarkId = (int)picture.TextWatermarkId;
                type = BuildMark(watermarkId, "text", picture.TextWatermark.WatermarkName);
                marksList.Add(type);
                marksList.Add(BuildMark(0, "none", "none"));
            }
            else
            {
                watermarkId = 0;
                type = BuildMark(watermarkId, "none","none");
                marksList.Add(type);
            }

            var userId = _repository.GetPhotographer(userName).Id;
            marksList.AddRange(BuildMarks(watermarkId,userId));
            return marksList;

        }

        private IEnumerable<WatermarkType> BuildMarks(int id,int userId)
        {
            List<WatermarkType> textList = BuildTextMarks(id,userId);
            List<WatermarkType> imageList = BuildImageMarks(id,userId);
            return textList.Concat(imageList);
        }

        private List<WatermarkType> BuildImageMarks(int id,int userId)
        {
            IEnumerable<ImageWatermark> imageWatermarks = _repository.ImageWatermarks
                .Where(mark => !mark.Id.Equals(id) & mark.PhotographerId.Equals(userId));
            List<WatermarkType> marks = new List<WatermarkType>();
            foreach (var textMark in imageWatermarks)
            {
                marks.Add(BuildMark(textMark.Id, "image", textMark.WatermarkName));
            }
            return marks;
        }

        private List<WatermarkType> BuildTextMarks(int id,int userId)
        {
            IEnumerable<TextWatermark> textWatermarks = _repository.TextWatermarks
                .Where(mark => !mark.Id.Equals(id) & mark.PhotographerId.Equals(userId));
            List<WatermarkType> marks = new List<WatermarkType>();
            foreach (var textMark in textWatermarks)
            {
                marks.Add(BuildMark(textMark.Id,"text",textMark.WatermarkName));
            }
            return marks;
        }

        private WatermarkType BuildMark(int watermarkId, string type, string name)
        {
            return new WatermarkType(watermarkId, type, name);
        }

        public void ChangeWatermark(int id, string watermark)
        {
            string[] split = watermark.Split('.');
            int watemarkId = Convert.ToInt32(split[0]);
            string type = split[1];
            var picture = _repository.GetPicture(id);
            if (watemarkId.Equals(0))
            {
                picture.ImageWatermarkId = null;
                picture.TextWatermarkId = null;
                _repository.Save();
                return;
            }
            else if (type.Equals("text"))
            {
                var mark = _repository.TextWatermarks.FirstOrDefault(m => m.Id.Equals(watemarkId));
                if (mark != null)
                {
                    picture.ImageWatermarkId = null;
                    picture.TextWatermarkId = watemarkId;
                }
            }
            else if (type.Equals("image"))
            {
                var mark = _repository.ImageWatermarks.FirstOrDefault(m => m.Id.Equals(watemarkId));
                if (mark != null)
                {
                    picture.TextWatermarkId = null;
                    picture.ImageWatermarkId = watemarkId;
                }
            }
            _repository.Save();

        }

        public IEnumerable<Album> Albums(int id)
        {
            return _repository.Albums(id);
        }


        public IEnumerable<PhotographerCamera> GetCameras(int pictureId,string usernmae)
        {
            List<PhotographerCamera> cameras = new List<PhotographerCamera>();
            var picture = _repository.GetPicture(pictureId);
            var userCameras = _repository.Cameras;
            if (picture.CameraId == null)
            {
                cameras.Add(new PhotographerCamera { Id = 0, Name = "None" });
                cameras.AddRange(userCameras);
            }
            else
            {
                cameras.Add(picture.Camera);
                cameras.Add(new PhotographerCamera { Id = 0, Name = "None" });
                cameras.AddRange(userCameras.Where(cam => !cam.Id.Equals(picture.CameraId)));
            }
            return cameras;
        }


        public void ChangeCamera(int id, int cameraId)
        {
            var picture = _repository.GetPicture(id);
            if (id == 0)
            {
                picture.CameraId = null;
            }
            else
            {
                picture.CameraId = cameraId;
            }
            _repository.Save();
        }


        public void ChangeDownloadMark(int id, bool DownloadWithWatermark)
        {
            var picture = _repository.GetPicture(id);
            picture.DownloadWithWatermark = DownloadWithWatermark;
            _repository.Save();
        }


        public object NotSo(int pictureId, string userId)
        {
            var picture = _repository.GetPicture(pictureId);
            var user = _repository.GetPhotographer(userId);
            if (!picture.PhotographerNotNice.Contains(user))
            {
                picture.PhotographerNotNice.Add(user);
                picture.PhotographerNice.Remove(user);
                picture.Rating = picture.Rating - 0.1f;
                _repository.Save();
                return new
                {
                    Id = user.Id
                };
            }
            return null;
                
        }

        public object Nice(int pictureId, string userId)
        {
            var picture = _repository.GetPicture(pictureId);
            var user = _repository.GetPhotographer(userId);
            if (!picture.PhotographerNice.Contains(user))
            {
                picture.PhotographerNice.Add(user);
                picture.PhotographerNotNice.Remove(user);
                picture.Rating = picture.Rating + 0.1f;
                _repository.Save();
                return new
                {
                    Id = user.Id
                };
            }
            return null;
        }


        public IEnumerable<Picture> UserFavorites(int take,int skip,string username)
        {
            var user = _repository.GetPhotographer(username);

            return user.Favorites.Reverse()
                .Skip(skip).Take(take);
        }


        public IEnumerable<Picture> GetUserPictures(int take, int skip, string userName)
        {
            var userId = _repository.GetPhotographer(userName).Id;

            return _repository.Pictures.Where(p => p.PhotographerId.Equals(userId))
                .OrderByDescending(p => p.Id)
                .Skip(skip).Take(take).ToList();
        }
    }
}
