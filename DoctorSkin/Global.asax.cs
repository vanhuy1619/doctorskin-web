using DoctorSkin.config;
using DoctorSkin.Mappings;
using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DoctorSkin
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //AutoMapperConfiguration.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //SwaggerConfig.Register(GlobalConfiguration.Configuration);
            //SwaggerConfig.Register(MyOtherApiConfiguration);
            //SwaggerConfig.Register(MyThirdApiConfiguration);

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = schedulerFactory.GetScheduler().Result;

            IJobDetail job = JobBuilder.Create<MyJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever())
                .Build();
        }
    }
}
