using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace p.Database.Abstract.Repositories
{
    public interface AbstractDailyContestsRepository : AbstractRepository
    {
        void AddContestPicture(ContestPicture picture);
        void AddContest(DailyContest contest);
        Photographer GetPhotographer(string userName);
        IEnumerable<DailyContest> Contests { get; }
        DailyContest GetContest(string date);
        DailyContest GetContest(int contestId);
        ContestPicture GetContestPicture(int contestPictureId);
        Picture GetPicture(int pictureId);
        bool ContestExists(string date);
        IEnumerable<ContestPicture> Pictures { get; }
        bool PictureExists(int userId, int pictureId,int contestId);
        IEnumerable<Picture> UserPictures(int userId);
        void AddUpdate(UserUpdate update);
    }
}
