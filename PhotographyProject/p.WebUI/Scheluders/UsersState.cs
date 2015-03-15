using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using p.Database.Concrete.Repositories;
using Quartz;

namespace p.WebUI.Scheluders
{
    public class UsersState : IJob
    {
        private DatabaseContext db = new DatabaseContext();

        public void Execute(IJobExecutionContext context)
        {
            var users = db.Photographers;
            foreach (var user in users)
            {
                if (user.Online)
                {
                    user.Online = false;
                }
                db.SaveChanges();
            }
        }
    }
}