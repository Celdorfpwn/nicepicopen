using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;
using PortofolioManager.Models;

namespace PortofolioManager.Abstract
{
    public interface IPortofolioContext
    {
        IEnumerable<Picture> GetUserPictures(string userName);
        IEnumerable<Picture> GetPictures(int choosedPictureId,string userName);
        Portofolio GetPortofolio(string userName);
        void CreatePortofolio(CreatePortofolioModel createPortofolioModel, string username);
        void GoViral(int id);
        bool IsViral(int id);
    }
}
