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
    public class WorkbenchWatermarksContext : AbstractWorkbenchWatermarksContext
    {
        private AbstractWorkbenchWatermarksRepository _repository { get; set; }

        public WorkbenchWatermarksContext(AbstractWorkbenchWatermarksRepository repository)
        {
            _repository = repository;
        }

        public void AddImageWatermark(ImageWatermark mark, Stream stream, string userName)
        {
            var imageData = ImageProcessor.GetImageDataFromStream(stream, Convert.ToInt32(stream.Length));
            if (ImageProcessor.CheckIfFileIsImage(imageData))
            {
                mark.Image = imageData;
                _repository.GetPhotographer(userName).ImageWatermarks.Add(mark);
                _repository.Save();
            }
        }


        public void AddTextWatermark(TextWatermark watermark, string userName)
        {
            _repository.GetPhotographer(userName).TextWatermarks.Add(watermark);
            _repository.Save();
        }


        public TextWatermark GetTextWatermark(int id)
        {
            return _repository.GetTextWatermark(id);
        }

        public ImageWatermark GetImageWatermark(int id)
        {
            return _repository.GetImageWatermark(id);
        }


        public IEnumerable<Album> GetAlbums(string userName)
        {
            return _repository.GetPhotographer(userName).Albums;
        }

        public string SetImageWatermark(int watermarkId,int type,string userName)
        {
            string message = null;
            switch (type)
            {
                case -1:
                    var albums = _repository.GetPhotographer(userName).Albums;
                    var port = _repository.GetPhotographer(userName).Portofolio;
                    var pics = albums.SelectMany(album => album.Pictures);
                    var modify = pics.Where(p => !port.Pictures.Contains(p));
                    SetImageWatermarkForPictures(modify, watermarkId);
                    message = "all pictures";
                    break;
                case 0:
                    var user = _repository.GetPhotographer(userName);
                    var portofolio = user.Portofolio;
                    if (portofolio != null)
                    {
                        SetImageWatermarkForPictures(portofolio.Pictures, watermarkId);
                        message = "all pictures in portofolio";
                    }
                    break;
                default:
                    var alb = _repository.GetPhotographer(userName).Albums.FirstOrDefault(album => album.Id.Equals(type));
                    var porto = _repository.GetPhotographer(userName).Portofolio;
                    SetImageWatermarkForPictures(alb.Pictures.Where(pic => !porto.Pictures.Contains(pic)),watermarkId);
                    message = "all " + alb.Name + " pictures";
                    break;
            }
            _repository.Save();
            return message;
        }

        private void SetImageWatermarkForPictures(IEnumerable<Picture> pictures, int watermarkId)
        {
            foreach (var picture in pictures)
            {
                SetImageWatermarkForPicture(picture,watermarkId);
            }
        }

        private void SetImageWatermarkForPicture(Picture picture, int watermarkId)
        {
            picture.TextWatermarkId = null;
            picture.ImageWatermarkId = watermarkId;
        }


        public string SetTextWatermark(int watermarkId, int type, string userName)
        {
            string message = null;
            switch (type)
            {
                case -1:
                    var albums = _repository.GetPhotographer(userName).Albums;
                    var port = _repository.GetPhotographer(userName).Portofolio;
                    SetTextWatermarkForPictures(albums.SelectMany(album => album.Pictures
                        .Where(picture => !port.Pictures.Contains(picture))
                        ), watermarkId);
                    message = "all pictures";
                    break;
                case 0:
                    var user = _repository.GetPhotographer(userName);
                    var portofolio = user.Portofolio;
                    if (portofolio != null)
                    {
                        SetTextWatermarkForPictures(portofolio.Pictures, watermarkId);
                        message = "all pictures in portofolio";
                    }
                    break;
                default:
                    var alb = _repository.GetPhotographer(userName).Albums.FirstOrDefault(album => album.Id.Equals(type));
                    var portof = _repository.GetPhotographer(userName).Portofolio;
                    SetTextWatermarkForPictures(
                        alb.Pictures.Where(picture => portof.Pictures.Contains(picture))
                        , watermarkId);
                    message = "all " + alb.Name + " pictures";
                    break;
            }
            _repository.Save();
            return message;

        }

        private void SetTextWatermarkForPictures(IEnumerable<Picture> pictures, int watermarkId)
        {
            foreach (var picture in pictures)
            {
                SetTextWatermarkForPicture(picture,watermarkId);
            }
        }

        private void SetTextWatermarkForPicture(Picture picture, int watermarkId)
        {
            picture.ImageWatermarkId = null;
            picture.TextWatermarkId = watermarkId;
        }


        public void RemoveWatermark(int id, string type)
        {
            if (type.Equals(typeof(ImageWatermark).Name))
            {
                var imageWatermark = _repository.GetImageWatermark(id);
                RemoveImageWatermark(imageWatermark.Pictures);
                _repository.RemoveImageWatermark(id);
            }
            else if (type.Equals(typeof(TextWatermark).Name))
            {
                var textWatermark = _repository.GetTextWatermark(id);
                RemoveTextWatermarks(textWatermark.Pictures);
                _repository.RemoveTextWatermark(id);
            }
            _repository.Save();
        }

        private void RemoveTextWatermarks(IEnumerable<Picture> pictures)
        {
            foreach (var picture in pictures)
            {
                picture.TextWatermarkId = null;
            }
        }

        private void RemoveImageWatermark(IEnumerable<Picture> pictures)
        {
            foreach (var picture in pictures)
            {
                picture.ImageWatermarkId = null;
            }
        }
    }
}
