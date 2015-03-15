using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class WorkbenchWatermarksRepository : Repository,AbstractWorkbenchWatermarksRepository
    {
        public void AddImageWatermark(ImageWatermark watermark)
        {
            _database.ImageWatermarks.Add(watermark);
        }


        public Photographer GetPhotographer(string userName)
        {
            return _database.Photographers.FirstOrDefault(p => p.Name.Equals(userName));
        }


        public TextWatermark GetTextWatermark(int id)
        {
            return _database.TextWatermarks.Find(id);
        }

        public ImageWatermark GetImageWatermark(int id)
        {
            return _database.ImageWatermarks.Find(id);
        }


        public void RemoveImageWatermark(int id)
        {
            var mark = _database.ImageWatermarks.Find(id);
            _database.Entry(mark).State = System.Data.Entity.EntityState.Deleted;
        }

        public void RemoveTextWatermark(int id)
        {
            var mark = _database.TextWatermarks.Find(id);
            _database.Entry(mark).State = System.Data.Entity.EntityState.Deleted;
        }
    }
}
