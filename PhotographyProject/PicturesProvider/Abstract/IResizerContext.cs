using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicturesProvider.Concrete;

namespace PicturesProvider.Abstract
{
    public interface IResizerContext
    {
        PictureImageModel GetPicture(int pictureId);
        byte[] GetResizePicture(int pictureId, int width, int height);
        void ResizePicture(PictureImageModel model);
    }
}
