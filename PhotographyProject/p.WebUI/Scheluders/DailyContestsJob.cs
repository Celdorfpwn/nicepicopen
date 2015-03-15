using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using p.Database.Concrete.Entities;
using p.Database.Concrete.Repositories;
using Quartz;

namespace p.WebUI.Scheluders
{
    public class DailyContestsJob : IJob
    {
        private DatabaseContext db { get; set;}

        public DailyContestsJob()
        {
            db = new DatabaseContext();
        }
        public void Execute(IJobExecutionContext context)
        {
            var date = DateTime.Today;
            string todays = date.ToShortDateString();
            if (!ContestsExists(todays))
            {
                CreateContest(todays);
            }

        }

        private void CreateContest(string todays)
        {
            DailyContest contest = new DailyContest();
            contest.Date = todays;
            db.DailyContests.Add(contest);
            db.SaveChanges();
        }

        private bool ContestsExists(string today)
        {
            var contest = db.DailyContests.FirstOrDefault(c => c.Date.Equals(today));
            if (contest == null)
                return false;
            else
                return true;
        }
    }
}