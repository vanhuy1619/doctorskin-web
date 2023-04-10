using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorSkin.Models;

namespace DoctorSkin.Controllers
{
    public class WishlistsController : Controller
    {
        private DoctorSkinEntities db = new DoctorSkinEntities();

        // GET: Wishlists
        public ActionResult Index()
        {
            return View(db.Wishlists.ToList());
        }


        [HttpPost]
        public ActionResult Add(Wishlists wishlists)
        {
            if (ModelState.IsValid)
            {
                db.Wishlists.Add(wishlists);
                db.SaveChanges();
                return Json(new { code = 0, message = "Wishlist ok" });
            }

            return Json(new { code = 1, message = "Wishlist false" });
        }

        [HttpDelete]
        public ActionResult remove(Wishlists wishlists)
        {
            Wishlists item = db.Wishlists.Where(s=>s.idp==wishlists.idp && s.iduser==wishlists.iduser).FirstOrDefault();
            if(item!=null)
            {
                db.Wishlists.Remove(item);
                db.SaveChanges();
                return Json(new { code = 0, message = "remove Wishlist ok" });
            }
            else
                return Json(new { code = 1, message = "No wishlist" });

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
