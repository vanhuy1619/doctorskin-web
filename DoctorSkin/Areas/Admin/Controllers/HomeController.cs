using DoctorSkin.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorSkin.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Login()
        //{
        //    return View();
        //}
    }
}