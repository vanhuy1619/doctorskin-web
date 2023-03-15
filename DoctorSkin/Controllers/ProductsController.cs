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
    public class ProductsController : Controller
    {
        private DoctorSkinEntities db = new DoctorSkinEntities();

        // GET: Products

        public ActionResult Index(string meta)
        {
            ViewBag.meta = meta;
            ViewBag.title =  (from t in db.Categories where t.meta == meta select t.namec).FirstOrDefault();
            var v = from t in db.Categories where t.meta == meta select t;
            return View(v.FirstOrDefault());
        }

        public ActionResult getCat()
        {
            var v = from t in db.Categories where t.hide == false select t;
            return PartialView(v.ToList());
        }

        //// GET: Products/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Models.Products products = db.Products.Find(id);
        //    if (products == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(products);
        //}

        //// GET: Products/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "idp,namep,typep,newprice,oldprice,descr,date_create,hide,statep,img")] Models.Products products)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(products);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(products);
        //}

        //// GET: Products/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Models.Products products = db.Products.Find(id);
        //    if (products == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(products);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "idp,namep,typep,newprice,oldprice,descr,date_create,hide,statep,img")] Models.Products products)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(products).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(products);
        //}

        //// GET: Products/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Models.Products products = db.Products.Find(id);
        //    if (products == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(products);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Models.Products products = db.Products.Find(id);
        //    db.Products.Remove(products);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
