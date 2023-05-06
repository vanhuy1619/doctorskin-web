using DoctorSkin.config;
using DoctorSkin.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DoctorSkin
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static Timer timer;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            timer = new Timer(30000); // Chạy mỗi 30 giây
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
        }

        protected void Application_End()
        {
            // Dừng tác vụ định kỳ khi ứng dụng kết thúc
            timer.Stop();
            timer.Dispose();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            // Gọi phương thức xóa dữ liệu
            var dataCleanupService = new MyJob();
            dataCleanupService.DeleteOldData();
        }
    }
}
