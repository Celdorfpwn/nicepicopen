using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Explorer.Abstract
{
    public interface AbstractExplorerPortofolioContext
    {
        Portofolio GetPortofolio(int id);

        Picture GetPicture(int id);

        int GetPictureViews(int id);

        int GetPictureNices(int id);

        bool HasNice(int id,string userName);

        void AddNice(int id,string userName);

        void AddView(int id, string userName);

        void AddComment(int id, string userName,string comment);

        IEnumerable<PortofolioPictureComment> GetPictureComments(int id);
    }
}
