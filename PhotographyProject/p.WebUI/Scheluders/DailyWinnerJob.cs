using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using p.Database.Concrete.Entities;
using p.Database.Concrete.Repositories;
using Quartz;

namespace p.WebUI.Scheluders
{
    public class DailyWinnerJob : IJob
    {
        private DatabaseContext db { get; set; }

        public DailyWinnerJob()
        {
            db = new DatabaseContext();
        }

        public void Execute(IJobExecutionContext context)
        {
            DeclareWinner();
        }

        private void DeclareWinner()
        {
            if (db.DailyContests != null)
                if (db.DailyContests.Count() > 0)
                {
                    var contests = db.DailyContests.ToList();
                    var contest = contests.Last();

                    if (contest.Pictures != null)
                        if (contest.Pictures.Count != 0)
                        {

                            var pictures = db.ContestsPictures.Include("Nices").Include("NotSo").ToList();

                            var sorted = pictures.Where(p => p.DailyContestId.Equals(contest.Id)).OrderByDescending(p => (p.Nices.Count - p.NotSo.Count))
                                .ThenByDescending(p => p.Id);
                            var winner = sorted.ElementAt(0);
                            contest.WinnerId = winner.Id;
                            var userId = winner.PhotographerId;

                            var userUpdate = new UserUpdate { DailyContestWinner = contest, PhotographerId = userId };
                            db.UserUpdates.Add(userUpdate);

                            db.SaveChanges();
                        }
                }

        }
    }
}