using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;
using PortofolioManager.Abstract;
using PortofolioManager.Models;

namespace PortofolioManager.Concrete
{
    public class PortofolioContext : IPortofolioContext
    {
        private AbstractPortofolioRepository _repository { get; set; }

        public PortofolioContext(AbstractPortofolioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Picture> GetUserPictures(string userName)
        {
            var albums = _repository.GetPhotographer(userName).Albums;

            return albums.SelectMany(album => album.Pictures);
        }

        public IEnumerable<Picture> GetPictures(int choosedPictureId, string userName)
        {
            var albums = _repository.GetPhotographer(userName).Albums;

            return albums.SelectMany(album => album.Pictures).Where(picture => !picture.Id.Equals(choosedPictureId));
        }

        public void CreatePortofolio(CreatePortofolioModel model, string username)
        {
            var pictures = _repository.Pictures.Where(picture => model.Pictures.Contains(picture.Id));
            var potofolio = new Portofolio();
            potofolio.Pictures = pictures.ToList();
            potofolio.PhotographerId = _repository.GetPhotographer(username).Id;
            potofolio.CoverId = model.CoverId;
            _repository.AddPortofolio(potofolio);
            _repository.Save();
        }

        public Portofolio GetPortofolio(string userName)
        {
            return _repository.GetPortofolio(userName);
        }


        public bool IsViral(int id)
        {
            return PortofolioIsViral(id);
        }

        private bool PortofolioIsViral(int id)
        {
            var updates = _repository.CommunityUpdates;
            var update = updates.FirstOrDefault(up => up.IsPortofolioUpdate(id));
            if (update != null)
                return true;
            else
                return false;
        }


        public void GoViral(int id)
        {
            if (!PortofolioIsViral(id))
            {
                _repository.AddUpdate(new CommunityUpdate { PortofolioId = id });
                _repository.Save();
            }
        }
    }
}
