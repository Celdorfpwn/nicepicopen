using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractWorkbenchWatermarksRepository : AbstractRepository
    {
        void AddImageWatermark(ImageWatermark watermark);

        Photographer GetPhotographer(string userName);

        TextWatermark GetTextWatermark(int id);

        ImageWatermark GetImageWatermark(int id);

        void RemoveImageWatermark(int id);

        void RemoveTextWatermark(int id);
    }
}
