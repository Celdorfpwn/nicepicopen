using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using p.Database.Concrete.Entities;

namespace Contests.Abstract
{
    public interface AbstractDailyContestsContext
    {
        void CreateContext();

        DailyContest Todays();

        IEnumerable<ContestPicture> ContestPictures(int id);

        void Participate(string userName, int pictureId);

        bool UserParticipating(string username);

        Photographer GetUser(string userName);

        IEnumerable<Picture> GetUserPictures(string username);

        ContestPicture GetUserPicture(string userName);

        void Nice(string userName, int pictureId);

        void NotNice(string userName, int pictureId);

        IEnumerable<ContestPicture> BestTodays();

        IEnumerable<DailyContest> GetLatestsContests();

        ContestPicture GetContestPicture(int id);
    }
}
