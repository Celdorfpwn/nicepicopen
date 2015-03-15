using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Workbench.Abstract
{
    public interface AbstractWorkbenchPicturesContext
    {
        IEnumerable<Picture> UserPictures(int categoryId, int albumId,string userName);

        IEnumerable<PictureCategory> Categories { get; }

        void AddPictures(int albumId, int categoryId, IEnumerable<System.IO.Stream> stream);

        IEnumerable<Picture> GetByCategory(int id, int categoryId);

        Picture GetPicture(int id);

        void ChangeCategory(int id, int categoryId);

        void ChangeDownload(int id, bool Donwloadable);

        IEnumerable<WatermarkType> PictureWatermarks(int id,string userName);

        void ChangeWatermark(int id, string watermark);

        IEnumerable<Album> Albums(int id);

        IEnumerable<PhotographerCamera> GetCameras(int pictureId,string username);

        void ChangeCamera(int id, int cameraId);

        void ChangeDownloadMark(int id, bool DownloadWithWatermark);

        object NotSo(int pictureId, string username);

        object Nice(int pictureId, string username);

        IEnumerable<Picture> UserFavorites(int take,int skip,string username);

        IEnumerable<Picture> GetUserPictures(int take,int skip,string userName);
    }
}
