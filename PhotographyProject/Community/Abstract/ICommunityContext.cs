using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Concrete;
using p.Database.Concrete.Entities;

namespace Community.Abstract
{
    public interface ICommunityContext
    {

        IQueryable<Picture> FreeView(int take, int skip);

        IQueryable<Picture> Newest(int take, int skip);

        IQueryable<Picture> BestPictures(int take, int id);
    }
}
