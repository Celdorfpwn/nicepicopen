using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using p.Database.Abstract.Repositories;
using PicturesProvider.Abstract;

namespace PicturesProvider.Concrete
{
    public class ResizerContext : IResizerContext
    {
        private AbstractImagesRepository _repository { get; set; }

        public ResizerContext(AbstractImagesRepository repository)
        {
            _repository = repository;
        }

        public PictureImageModel GetPicture(int pictureId)
        {
            var imageData = _repository.GetPicture(pictureId).Image;
            WebImage image = new WebImage(imageData);

            return new PictureImageModel(pictureId, image.Width, image.Height);
        }


        public byte[] GetResizePicture(int pictureId, int width, int height)
        {
            var img = _repository.GetPicture(pictureId).Image;
            var result = Resizer.Resize(img, width.ToString(), height.ToString());
            return result;
        }


        public void ResizePicture(PictureImageModel model)
        {
            var picture = _repository.GetPicture(model.PictureId);
            var image = picture.Image;
            picture.Image = Resizer.Resize(image, model.Width.ToString(), model.Height.ToString());
            _repository.Save();
        }


        public byte[] CompAndRes(byte[] data,int quality,Constant constant)
        {
            return CompAndRes(data, quality,constant);
        }


        private byte[] CompressAndResize(byte[] image, int quality, Constant constant)
        {
            var compressed = Compressor.Compress(image, quality);
            var result = Resizer.Resize(compressed, constant.Width, constant.Height);
            return result;
        }
    }
}
