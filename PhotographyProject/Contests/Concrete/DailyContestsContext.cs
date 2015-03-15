using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contests.Abstract;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace Contests.Concrete
{
    public class DailyContestsContext : AbstractDailyContestsContext
    {
        private AbstractDailyContestsRepository _repository { get; set; }

        public DailyContestsContext(AbstractDailyContestsRepository repository)
        {
            _repository = repository;
        }

        private string TodaysDate()
        {
            DateTime date = DateTime.Today;
            string today = date.ToShortDateString();
            return today;
        }

        public void CreateContext()
        {
            string today = TodaysDate();

            if (!_repository.ContestExists(today))
            {
                var contest = new DailyContest { Date = today };
                _repository.AddContest(contest);
                _repository.Save();
            }
        }

        private DailyContest TodaysContext()
        {
            string today = TodaysDate();
            var contest = _repository.GetContest(today);
            return contest;
        }

        public DailyContest Todays()
        {
            return TodaysContext();
        }

        public IEnumerable<ContestPicture> ContestPictures(int id)
        {
            var contestPictures = _repository.Pictures.Where(picture => picture.DailyContestId.Equals(id));
            return contestPictures.OrderBy(picture => Guid.NewGuid());
        }

        public void Participate(string userName, int pictureId)
        {
            int id = _repository.GetPhotographer(userName).Id;
            int contestId = TodaysContext().Id;
            if (!_repository.PictureExists(id, pictureId, contestId))
            {
                var picture = new ContestPicture
                {
                    DailyContestId = contestId,
                    PhotographerId = id,
                    PictureId = pictureId
                };

                var update = new UserUpdate { PhotographerId = id, ContestPicture = picture };
                _repository.AddContestPicture(picture);
                _repository.AddUpdate(update);
                _repository.Save();
            }
        }



        public bool UserParticipating(string userName)
        {
            var userId = _repository.GetPhotographer(userName).Id;
            var contestId = TodaysContext().Id;
            var contestPic = _repository.Pictures.FirstOrDefault(picture => picture.PhotographerId.Equals(userId) &&
                picture.DailyContestId.Equals(contestId));
            if(contestPic!=null)
                return true;
            else
                return false;
        }


        public Photographer GetUser(string userName)
        {
            return _repository.GetPhotographer(userName);
        }


        public IEnumerable<Picture> GetUserPictures(string username)
        {
            var user = _repository.GetPhotographer(username);
            var pictures = _repository.UserPictures(user.Id);
            return pictures;
        }


        public ContestPicture GetUserPicture(string userName)
        {
            var userId = _repository.GetPhotographer(userName).Id;

            var contest = TodaysContext();

            var pictureId = contest.Pictures.FirstOrDefault(picture => picture.PhotographerId.Equals(userId)).Id;

            return _repository.GetContestPicture(pictureId);
        }


        public void Nice(string userName, int pictureId)
        {
            var picture = _repository.GetContestPicture(pictureId);
            if (!picture.HasNice(userName))
            {
                var user = _repository.GetPhotographer(userName);
                picture.NotSo.Remove(user);
                picture.Nices.Add(user);
                _repository.Save();
            }
        }

        public void NotNice(string userName, int pictureId)
        {
            var picture = _repository.GetContestPicture(pictureId);
            if (!picture.HasNotSo(userName))
            {
                var user = _repository.GetPhotographer(userName);
                picture.NotSo.Add(user);
                picture.Nices.Remove(user);
                _repository.Save();
            }
        }


        public IEnumerable<ContestPicture> BestTodays()
        {
            var contestId = TodaysContext().Id;
            var pictures = _repository.Pictures.Where(picture => picture.DailyContestId.Equals(contestId));

            return pictures.OrderByDescending(picture => (picture.Nices.Count - picture.NotSo.Count));
        }


        public IEnumerable<DailyContest> GetLatestsContests()
        {
            string todays = TodaysDate();

            return _repository.Contests.Where(contest => !contest.Date.Equals(todays))
                .OrderByDescending(contest => contest.Id);
        }


        public ContestPicture GetContestPicture(int id)
        {
            return _repository.GetContestPicture(id);
        }
    }
}
