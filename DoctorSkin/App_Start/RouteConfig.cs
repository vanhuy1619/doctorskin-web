﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoctorSkin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Products", "{type}/{meta}",
                new {Controller = "Products",action="Index",meta = UrlParameter.Optional},
                new RouteValueDictionary
                {
                    {"type","san-pham" }
                },
                new[] { "DoctorSkin.Controllers"});

            routes.MapRoute("BlogByType", "{type}/{meta}",
                new { Controller = "BlogByType", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type","blog" }
                },
                new[] { "DoctorSkin.Controllers" });

            routes.MapRoute("Doctors", "{type}",
                new { Controller = "Doctors", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type","doi-ngu-bac-si" }
                },
                new[] { "DoctorSkin.Controllers" });

            routes.MapRoute("Blog", "{type}",
                new { Controller = "Blog", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type","kien-thuc" }
                },
                new[] { "DoctorSkin.Controllers" });

            routes.MapRoute("ServicesDetails", "{type}/{meta}",
                new { Controller = "ServicesDetails", action = "Index", meta = UrlParameter.Optional },
                new RouteValueDictionary
                {
                    {"type","dich-vu" }
                },
                new[] { "DoctorSkin.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

    }
}