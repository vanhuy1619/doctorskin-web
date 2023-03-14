using DoctorSkin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorSkin.Controllers
{
    public class DefaultController : Controller
    {
        DoctorSkinEntities db = new DoctorSkinEntities();
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getListCat()
        {
            ViewBag.meta = "san-pham";
            var v = from t in db.Categories where t.hide == false select t;
            return PartialView(v.ToList());
        }

        public ActionResult getListProduct(int typep)
        {
            //ViewBag.meta = "san-pham";
            var v = from t in db.Products
                    where t.typep == typep && t.hide == false
                    orderby t.date_create ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getListBlogType()
        {
            ViewBag.meta = "blog";
            var v = from t in db.BlogType
                    where t.hide == false
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getListBlogByType(int idbt)
        {
            //ViewBag.meta = "san-pham";
            var v = from t in db.BlogDetails
                    where t.idbt == idbt && t.hideblog == false
                    orderby t.date_up descending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getListDetailByService(int id_dt)
        {
            var v = from t in db.ServicesDetails
                    where t.id_dt == id_dt && t.hide_sd == false
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getListServices()
        {
            ViewBag.meta = "dich-vu";
            var v = from t in db.Services
                    where t.hide_dt == false
                    select t;
            return PartialView(v.ToList());
        }
    }
}