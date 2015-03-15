using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Abstract;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace Explorer.Concrete
{
    public class ExplorerPortofolioContext : AbstractExplorerPortofolioContext
    {

        private AbstractExplorerPortofolioRepository _repository { get; set; }

        public ExplorerPortofolioContext(AbstractExplorerPortofolioRepository repository)
        {
            _repository = repository;
        }

        public Portofolio GetPortofolio(int id)
        {
            return _repository.GetPortofolio(id);
        }


        public Picture GetPicture(int id)
        {
            return _repository.GetPicture(id);
        }


        public int GetPictureViews(int id)
        {
            var picture = _repository.GetPicture(id);
            int portofolioId = (int)picture.PortofolioId;
            var portofolio = _repository.GetPortofolio(portofolioId);

            var views = portofolio.PicturesViews.Where(p => p.PictureId.Equals(id) & p.PortofolioId.Equals(portofolioId));

            if(views!=null)
                return views.Count();

            return 0;
        }

        public int GetPictureNices(int id)
        {
            var picture = _repository.GetPicture(id);
            int portofolioId = (int)picture.PortofolioId;
            var portofolio = _repository.GetPortofolio(portofolioId);

            var views = portofolio.PicturesNices.Where(p => p.PictureId.Equals(id) & p.PortofolioId.Equals(portofolioId));

            if (views != null)
                return views.Count();

            return 0;
        }

        private bool PictureHasNice(int id,int portofolioId,int userId)
        {
            var portofolio = _repository.GetPortofolio(portofolioId);

            var nice = portofolio.PicturesNices.FirstOrDefault
                (p => p.PictureId.Equals(id) & p.PhotographerId.Equals(userId));
            if (nice != null)
                return true;
            else
                return false;
        }

        public bool HasNice(int id,string userName)
        {
            var picture = _repository.GetPicture(id);
            int portofolioId = (int)picture.PortofolioId;
            var user = _repository.GetUser(userName);
            return PictureHasNice(id,portofolioId,user.Id);
        }


        public void AddNice(int id,string userName)
        {
            var picture = _repository.GetPicture(id);
            int portofolioId = (int)picture.PortofolioId;
            var user = _repository.GetUser(userName);
            if (!PictureHasNice(user.Id,portofolioId,user.Id))
            {
                var portofolio = _repository.GetPortofolio(portofolioId);
                portofolio.PicturesNices.Add(new PortofolioPictureNice { PhotographerId = user.Id, PictureId = picture.Id });
                _repository.Save();
            }
        }

        private bool PictureHasView(int id,int portofolioId,int userId)
        {
            var picture = _repository.GetPicture(id);
            var portofolio = _repository.GetPortofolio(portofolioId);

            var view = portofolio.PicturesViews.FirstOrDefault
                (p => p.PictureId.Equals(id) && p.PhotographerId.Equals(userId));
            if (view != null)
                return true;
            else
                return false;
        }


        public void AddView(int id, string userName)
        {
            var picture = _repository.GetPicture(id);
            int portofolioId = (int)picture.PortofolioId;
            var user = _repository.GetUser(userName);
            if (!PictureHasView(id,portofolioId,user.Id))
            {
                var portofolio = _repository.GetPortofolio(portofolioId);
                portofolio.PicturesViews.Add(new PortofolioPictureView { PhotographerId = user.Id, PictureId = picture.Id });
                _repository.Save();
            }
        }

        public void AddComment(int id, string userName,string comment)
        {
            var picture = _repository.GetPicture(id);
            int portofolioId = (int)picture.PortofolioId;
            var portofolio = _repository.GetPortofolio(portofolioId);
            var user = _repository.GetUser(userName);

            portofolio.PictureComments.Add(
                new PortofolioPictureComment { PhotographerId = user.Id, PictureId = picture.Id,Comment = comment });
            _repository.Save();
        }


        public IEnumerable<PortofolioPictureComment> GetPictureComments(int id)
        {
            var picture = _repository.GetPicture(id);
            int portofolioId = (int)picture.PortofolioId;
            var portofolio = _repository.GetPortofolio(portofolioId);
            return portofolio.PictureComments.Where(comment => comment.PictureId.Equals(id))
                .OrderByDescending(comment => comment.Id);
        }
    }
}
