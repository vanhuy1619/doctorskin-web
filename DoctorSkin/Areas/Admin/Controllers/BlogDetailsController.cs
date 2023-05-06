using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorSkin.Models;

namespace DoctorSkin.Areas.Admin.Controllers
{
    [Authorize]
    public class BlogDetailsController : Controller
    {
        private DoctorSkinEntities db = new DoctorSkinEntities();

        // GET: Admin/BlogDetails
        public ActionResult Index()
        {
            return View(db.BlogDetails.ToList());
        }

        // GET: Admin/BlogDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogDetails blogDetails = db.BlogDetails.Find(id);
            if (blogDetails == null)
            {
                return HttpNotFound();
            }
            return View(blogDetails);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BlogDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "idb,idbt,title,shortcontent,date_up,cardimg,hideblog")] BlogDetails blogDetails)
        {
            if (ModelState.IsValid)
            {
                db.BlogDetails.Add(blogDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogDetails);
        }

        // GET: Admin/BlogDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogDetails blogDetails = db.BlogDetails.Find(id);
            if (blogDetails == null)
            {
                return HttpNotFound();
            }
            return View(blogDetails);
        }

        // POST: Admin/BlogDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idb,idbt,title,shortcontent,date_up,cardimg,hideblog")] BlogDetails blogDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogDetails);
        }

        // GET: Admin/BlogDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogDetails blogDetails = db.BlogDetails.Find(id);
            if (blogDetails == null)
            {
                return HttpNotFound();
            }
            return View(blogDetails);
        }

        // POST: Admin/BlogDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogDetails blogDetails = db.BlogDetails.Find(id);
            db.BlogDetails.Remove(blogDetails);
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
