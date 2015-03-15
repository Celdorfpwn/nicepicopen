using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using p.Database.Concrete.Repositories;
using p.WebUI.Scheluders;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System.Data.Entity;

namespace p.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            StartScheduler();
        }

        private void StartScheduler()
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler();
            scheduler.Start();
            JobDetailImpl jobContest = new JobDetailImpl("DailyContest", null, typeof(DailyContestsJob));
            JobDetailImpl jobWinner = new JobDetailImpl("DailyWinner", null, typeof(DailyWinnerJob));
            JobDetailImpl jobState = new JobDetailImpl("State", null, typeof(UsersState));
            CronTriggerImpl triggerWinner = new CronTriggerImpl("dailyWinner");
            CronTriggerImpl triggerContest = new CronTriggerImpl("dailyContest");
            CronTriggerImpl triggerState = new CronTriggerImpl("triggerState");
            triggerWinner.CronExpression = new CronExpression("0 1 0 * * ?");
            triggerContest.CronExpression = new CronExpression("0 5 0 * * ?");
            scheduler.ScheduleJob(jobWinner, triggerWinner);
            scheduler.ScheduleJob(jobContest, triggerContest);
            //scheduler.ScheduleJob(jobState, triggerState);

        }

        //trigger.CronExpression = new CronExpression("0 1 0 * * ?");
    }
}