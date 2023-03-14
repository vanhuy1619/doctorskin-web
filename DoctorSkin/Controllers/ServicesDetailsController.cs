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
    public class ServicesDetailsController : Controller
    {
        private DoctorSkinEntities db = new DoctorSkinEntities();

        // GET: ServicesDetails
        public ActionResult Index(String meta)
        {
            ViewBag.title = (from t in db.Services where t.meta == meta select t.name_dt).FirstOrDefault().ToString();
            ViewBag.img = (from t in db.Services where t.meta == meta select t.slider_dt).FirstOrDefault().ToString()+".png";
            var v = from t in db.Services where t.meta == meta && t.hide_dt==false select t;
            return View(v.FirstOrDefault());
        }


        // GET: ServicesDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicesDetails servicesDetails = db.ServicesDetails.Find(id);
            if (servicesDetails == null)
            {
                return HttpNotFound();
            }
            return View(servicesDetails);
        }

        // GET: ServicesDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServicesDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_sd,name_sd,img_sd,hide_sd,price_sd")] ServicesDetails servicesDetails)
        {
            if (ModelState.IsValid)
            {
                db.ServicesDetails.Add(servicesDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servicesDetails);
        }

        // GET: ServicesDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicesDetails servicesDetails = db.ServicesDetails.Find(id);
            if (servicesDetails == null)
            {
                return HttpNotFound();
            }
            return View(servicesDetails);
        }

        // POST: ServicesDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_sd,name_sd,img_sd,hide_sd,price_sd")] ServicesDetails servicesDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicesDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servicesDetails);
        }

        // GET: ServicesDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServicesDetails servicesDetails = db.ServicesDetails.Find(id);
            if (servicesDetails == null)
            {
                return HttpNotFound();
            }
            return View(servicesDetails);
        }

        // POST: ServicesDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicesDetails servicesDetails = db.ServicesDetails.Find(id);
            db.ServicesDetails.Remove(servicesDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
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
