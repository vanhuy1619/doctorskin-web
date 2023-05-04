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
            if (User.IsInRole(RoleName.Admin))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Unauthorized", "Error");
            }
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}