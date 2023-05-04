using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorSkin.Models;
using Microsoft.IdentityModel.Tokens;

namespace DoctorSkin.Controllers
{
    public class VouchersController : Controller
    {
        private DoctorSkinEntities db = new DoctorSkinEntities();

        //XỬ LÝ API
        [HttpGet]
        public JsonResult GetVoucherByID(string idvoucher)
        {
            var voucher = db.Vouchers.FirstOrDefault(s => s.idvoucher == idvoucher);
            if (voucher == null)
                return Json(new { code = 1, message = "Mã Voucher không hợp lệ" }, JsonRequestBehavior.AllowGet);
            return Json(new { code = 0, voucher = voucher }, JsonRequestBehavior.AllowGet);
        }


        // GET: Vouchers
        public ActionResult Index()
        {
            string iduser = (string)Session["iduser"];
            var usedVouchers = db.Bills.Where(b => b.iduser == iduser).Select(b => b.idvoucher).ToList();
            var unusedVouchers = db.Vouchers.Where(v => !usedVouchers.Contains(v.idvoucher)).ToList();
            return View(unusedVouchers);
        }

        // GET: Vouchers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vouchers vouchers = db.Vouchers.Find(id);
            if (vouchers == null)
            {
                return HttpNotFound();
            }
            return View(vouchers);
        }

        // GET: Vouchers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idvoucher,namevc,valuevc,quantity,dasudung,datefrom,dateto,hidevc")] Vouchers vouchers)
        {
            if (ModelState.IsValid)
            {
                db.Vouchers.Add(vouchers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vouchers);
        }

        // GET: Vouchers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vouchers vouchers = db.Vouchers.Find(id);
            if (vouchers == null)
            {
                return HttpNotFound();
            }
            return View(vouchers);
        }

        // POST: Vouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idvoucher,namevc,valuevc,quantity,dasudung,datefrom,dateto,hidevc")] Vouchers vouchers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vouchers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vouchers);
        }

        // GET: Vouchers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vouchers vouchers = db.Vouchers.Find(id);
            if (vouchers == null)
            {
                return HttpNotFound();
            }
            return View(vouchers);
        }

        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vouchers vouchers = db.Vouchers.Find(id);
            db.Vouchers.Remove(vouchers);
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
