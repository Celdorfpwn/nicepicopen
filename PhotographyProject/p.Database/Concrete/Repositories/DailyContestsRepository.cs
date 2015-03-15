using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Abstract.Repositories;
using p.Database.Concrete.Entities;

namespace p.Database.Concrete.Repositories
{
    public class DailyContestsRepository :Repository, AbstractDailyContestsRepository
    {

        public void AddContestPicture(ContestPicture picture)
        {
            _database.ContestsPictures.Add(picture);
        }

        public void AddContest(DailyContest contest)
        {
            _database.DailyContests.Add(contest);
        }

        public Photographer GetPhotographer(string userName)
        {
            return _database.Photographers.FirstOrDefault(p => p.Name.Equals(userName));
        }

        public IEnumerable<DailyContest> Contests
        {
            get { return _database.DailyContests; }
        }

        public IEnumerable<ContestPicture> Pictures
        {
            get { return _database.ContestsPictures.Include("Picture")
                .Include("Photographer").Include("Nices").Include("NotSo"); }
        }


        public DailyContest GetContest(int contestId)
        {
            return _database.DailyContests.Find(contestId);
        }

        public ContestPicture GetContestPicture(int contestPictureId)
        {
            return _database.ContestsPictures.Find(contestPictureId);
        }

        public Picture GetPicture(int pictureId)
        {
            return _database.Pictures.Find(pictureId);
        }


        public bool ContestExists(string date)
        {
            var dbcontest = _database.DailyContests.FirstOrDefault(contest => contest.Date.Equals(date));
            if (dbcontest != null)
                return true;
            else
                return false;
        }


        public DailyContest GetContest(string date)
        {
            var dbcontest = _database.DailyContests.FirstOrDefault(contest => contest.Date.Equals(date));
            return dbcontest;
        }


        public bool PictureExists(int userId, int pictureId, int contestId)
        {
            var pictures = _database.ContestsPictures.Where(p => p.DailyContestId.Equals(contestId));
            var picture = pictures.FirstOrDefault(p => p.PhotographerId.Equals(userId) || p.PictureId.Equals(pictureId));
            if (picture != null)
                return true;
            else
                return false;
        }


        public IEnumerable<Picture> UserPictures(int userId)
        {
            var albums = _database.Albums.Where(album => album.PhotographerId.Equals(userId));

            return albums.SelectMany(album => album.Pictures);
        }


        public void AddUpdate(UserUpdate update)
        {
            _database.UserUpdates.Add(update);
        }
    }
}
