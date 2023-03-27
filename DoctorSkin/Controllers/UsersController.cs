using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorSkin.Models;
using DoctorSkin.config;
using Azure.Core;
using CloudinaryDotNet.Actions;
using Microsoft.Ajax.Utilities;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Web.UI.WebControls;

namespace DoctorSkin.Controllers
{
    [Route("/user")]
    public class UsersController : Controller
    {
        private DoctorSkinEntities db = new DoctorSkinEntities();
        private Config config = new Config();

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        //[Route("forgot-pass")]
        //[Route("user/forgot-pass")]
        public ActionResult ResetPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String email, String Password)
        {
            //Users user = db.Users.Where(i=>i.email)
            return null;
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iduser,name,birth,gender,phone,email,password,ava")] Users users)
        {
            if (ModelState.IsValid && FileUpload.Equals(""))
            {
                var check = db.Users.FirstOrDefault(s => s.email == users.email);
                var cloudinary = new Cloudinary();

                if (check == null){
                    users.password = config.GetMD5(users.password);
                    db.Configuration.ValidateOnSaveEnabled = false;

                    
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(fileAvatar.FileName, fileAvatar.FileContent),
                        PublicId = "avatar/" + Guid.NewGuid().ToString() // Tên public id để lưu hình ảnh trên Cloudinary
                    };

                    // Tải lên hình ảnh lên Cloudinary
                    var uploadResult = cloudinary.Upload(uploadParams);

                    // Lưu URL của hình ảnh vào cơ sở dữ liệu của bạn hoặc sử dụng nó để hiển thị hình ảnh trong trang web của bạn
                    string imageUrl = uploadResult.Uri.ToString();

                    users.ava = imageUrl;

                    db.Users.Add(users);
                    db.SaveChanges();
                    ViewBag.noti = "Đăng ký thành công, vui lòng đang nhập tài khoản!";
                    
                    return View();
                }
                else
                {
                    ViewBag.noti = "Đăng ký tài khoản không thành công!";
                    return View();
                }
            }
            return View();
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iduser,name,birth,gender,phone,email,password,hide,block,ava")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
