using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesProvider.Abstract
{
    public interface IImageProviderContext
    {
        byte[] GetPictureCaruselImage(int pictureId);
        byte[] AlbumProfilePicture(int albumId);
        byte[] UserProfile(int userId);
        byte[] UserCameraImage(int cameraId);
        byte[] GetPictureAlbumCaruselImage(int pictureId);
        byte[] AlbumDetailsPictureWithWatermark(int pictureId);
        byte[] GetDetailsPictureImage(int id);
        byte[] GetComunityPicture(int id);
        byte[] UserProfileTexWatermark(string userName, int id);
        byte[] UserProfileImageWatermark(string userName, int id);
        byte[] GetUserProfileSmall(int id);
        byte[] GetQuote(int id);

        byte[] GetMessageImage(int id);
    }
}
