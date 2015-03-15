using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Workbench.Abstract
{
    public interface AbstractWorkbenchWatermarksContext
    {
        void AddImageWatermark(ImageWatermark mark, Stream stream, string userName);

        void AddTextWatermark(TextWatermark watermark, string userName);

        TextWatermark GetTextWatermark(int id);

        ImageWatermark GetImageWatermark(int id);

        IEnumerable<Album> GetAlbums(string userName);

        string SetImageWatermark(int watermarkId,int type,string userName);
        string SetTextWatermark(int watermarkId, int type, string userName);

        void RemoveWatermark(int id, string type);
    }
}
