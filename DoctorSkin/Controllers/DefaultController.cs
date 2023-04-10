using DoctorSkin.Models;
using DoctorSkin.Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorSkin.Controllers
{
    public class DefaultController : Controller
    {
        DoctorSkinEntities db = new DoctorSkinEntities();
        // GET: Default
        // public ActionResult Index()
        // {
        //     return View();
        // }

        public ActionResult getListCat()
        {
            ViewBag.meta = "san-pham";
            var v = from t in db.Categories 
                     where t.hide == false select t;
            return PartialView(v.ToList());
        }

        [HttpGet]
        public ActionResult getListProduct(int? typep, string sortOrder, int? page)
        {
            ViewBag.meta = "san-pham";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "new" : "";
            ViewBag.Type = null;
            var v = (from t in db.Products
                    where t.typep == typep && t.hide == false
                    select t).ToList();
            if (!typep.HasValue)
                v = db.Products.ToList();
            switch (sortOrder)
            {
                case "new":
                    v = v.OrderByDescending(p => p.date_up).ToList();
                    break;
                case "price_desc":
                    v = v.OrderByDescending(p => p.newprice).ToList();
                    break;
                default:
                    v = v.OrderBy(p => p.newprice).ToList();
                    break;
            }
            int pageSize = 21;
            int pageNumber = (page ?? 1);
            return PartialView(v.ToPagedList(pageNumber, pageSize));
        }

        // [Route("san-pham")]
        // public ActionResult getListProductAll()
        // {
        //     return View(db.Products.ToList());
        // }

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

        public ActionResult getRandomBrand()
        {
            Random rnd = new Random();
            var v = db.Brands.Where(p=>p.idbrand!=1).ToList();
            return PartialView(v.OrderBy(x => rnd.Next()).Take(4).ToList());
        }

        public ActionResult GetListFeedbackByProductID(int? idp, int? pagecmt)
        {
            
            var query = from a in db.Feedbacks
                        join b in db.Users on a.iduser equals b.iduser
                        where a.idp == idp
                        orderby a.datefb descending
                        select new
                        {
                        sttfb = a.sttfb,
                        cmt = a.cmt,
                        datefb = a.datefb,
                        nameu = b.name,
                        avau = b.ava
                    };
            var products = query.ToList().Select(r => new UserFeedBackViewModel
            {
                sttfb = r.sttfb,
                cmt = r.cmt,
                datefb = r.datefb,
                nameu = r.nameu,
                avau = r.avau
            }).ToList();
            int pageSize = 1;
            int pageNumber = (pagecmt ?? 1);
            ViewBag.listfb = products.Count();
            return PartialView(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult getListRepFeedbacks(int? sttfb)
        {
            var v = from a in db.RepFeedbacks
                    join b in db.Feedbacks on a.sttfb equals b.sttfb
                    where a.hide_rep == false && b.sttfb == sttfb && a.sttfb == sttfb
                    select a;
            
            return PartialView(v.ToList());
        }

        public ActionResult getListProductSale ()
        {
            Random rnd = new Random();
            var v = db.Products.Where(p => p.statep=="Sale").ToList();
            return PartialView(v.OrderBy(x => rnd.Next()).Take(3).ToList());
        }

        public ActionResult getListProductTopRate()
        {
            Random rnd = new Random();
            var v = (from a in db.Products orderby a.rated descending select a).ToList();
            return PartialView(v.OrderBy(x => rnd.Next()).Take(3).ToList());
        }


    }
}