using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using PicturesProvider.Abstract;

namespace PicturesProvider.Concrete
{
    public class ImageProviderContext : IImageProviderContext
    {
        private AbstractImagesRepository _repository { get; set; }

        public ImageProviderContext(AbstractImagesRepository repository)
        {
            _repository = repository;
        }

        public byte[] GetPictureCaruselImage(int pictureId)
        {
            var image = _repository.GetPicture(pictureId).Image;
            var result = CompressAndResize(image, 15, ResizerContants.ImageCarusel);

            return result;
        }

        public byte[] GetPictureAlbumCaruselImage(int pictureId)
        {
            var image = _repository.GetPicture(pictureId).Image;
            var result = CompressAndResize(image, 40, ResizerContants.AlbumPictureCarusel);
            return result;
        }

        public byte[] AlbumProfilePicture(int albumId)
        {
            var album = _repository.GetAlbum(albumId);

            if (album.Pictures.Count != 0)
            {
                var image = album.Pictures.OrderByDescending(picture => picture.Rating)
                    .First().Image;
                var result = CompressAndResize(image, 70, ResizerContants.AlbumProfile);
                return result;
            }

            return null;
        }

        public byte[] UserProfile(int userId)
        {
            var image = _repository.ProfilePicture(userId);
            var result = CompressAndResize(image, 40, ResizerContants.UserProfile);
            return result;
        }

        private byte[] CompressAndResize(byte[] image,int quality,Constant constant)
        {
            var compressed = Compressor.Compress(image,quality);
            var result = Resizer.Resize(compressed,constant.Width,constant.Height);
            return result;
        }



        public byte[] UserCameraImage(int cameraId)
        {
            var image = _repository.GetCamera(cameraId).Camera;
            var result = CompressAndResize(image, 30, ResizerContants.ImageCarusel);
            return result;
        }


        public byte[] AlbumDetailsPictureWithWatermark(int pictureId)
        {
            var image = _repository.GetPicture(pictureId).Image;
            var result = CompressAndResize(image, 20, ResizerContants.PictureWithWatermarkDetails);
            return image;
        }


        public byte[] GetDetailsPictureImage(int pictureId)
        {
            var image = _repository.GetPicture(pictureId).Image;
            var result = CompressAndResize(image, 100, ResizerContants.PictureWithWatermarkDetails);
            return result;
        }


        public byte[] GetComunityPicture(int id)
        {
            var image = _repository.GetPicture(id).Image;
            var result = CompressAndResize(image, 50, ResizerContants.AlbumPictureCarusel);
            return result;
        }


        public byte[] UserProfileTexWatermark(string userName, int id)
        {
            return null;
        }

        public byte[] UserProfileImageWatermark(string userName, int id)
        {
            return null;
        }


        public byte[] GetUserProfileSmall(int id)
        {
            var image = _repository.ProfilePicture(id);
            var result = CompressAndResize(image, 10, ResizerContants.UserProfileVerySmall);
            return result;
        }


        public byte[] GetQuote(int id)
        {
            return _repository.GetQuote(id).Image;
        }


        public byte[] GetMessageImage(int id)
        {
            var imageData = _repository.GetMessage(id).Image;
            var result = CompressAndResize(imageData, 10, ResizerContants.ImageCarusel);
            return result;
        }
    }
}
