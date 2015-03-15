using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Abstract;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace Community.Concrete
{
    public class CommunityContext : ICommunityContext
    {
        private AbstractCommunityRepository _repository { get; set; }

        public CommunityContext(AbstractCommunityRepository repository)
        {
            _repository = repository;
        }


        public IQueryable<Picture> FreeView(int take, int skip)
        {
            return _repository.Pictures.OrderBy(picture => Guid.NewGuid())
                .Skip(skip).Take(take);
        }

        public IQueryable<Picture> Newest(int take, int skip)
        {
            var pictures = _repository.Pictures.OrderByDescending(picture => picture.Id)
                .Skip(skip).Take(take);
            return pictures;
        }

        public IQueryable<Picture> BestPictures(int take, int id)
        {
            return _repository.Pictures.OrderByDescending(picture => picture.Rating)
                .Skip(id).Take(take);
        }
    }
}
