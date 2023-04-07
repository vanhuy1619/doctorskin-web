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
using System.IO;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Security;
using System.Security.Permissions;
using System.Web.Services;
using System.Security.Cryptography;
using System.Text;

namespace DoctorSkin.Controllers
{
    [Route("/user")]
    public class UsersController : Controller
    {
        private DoctorSkinEntities db = new DoctorSkinEntities();
        private Config config = new Config();


        //[Route("forgot-pass")]
        //[Route("user/forgot-pass")]
        public ActionResult ResetPass()
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (Session["iduser"] != null)
            {
                Response.Redirect("/");
            }
            return View();
        }

        public String randomID()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[10];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Users users, HttpPostedFileBase file)
        {
            ImageUploadResult uploadResult = null;

            var check = db.Users.FirstOrDefault(s => s.email.Equals(users.email) || s.phone.Equals(users.phone));

            var cloudinary = new Cloudinary(new Account("dnqk5fjla", "149557243487116", "n9moDShH0IrWNmotU4l5EWdhSuI"));

            using (var reader = new BinaryReader(file.InputStream))
            {
                var fileBytes = reader.ReadBytes(file.ContentLength);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, new MemoryStream(fileBytes)),
                    PublicId = Guid.NewGuid().ToString(), // Tên public id để lưu hình ảnh trên Cloudinary
                    Folder = "DoctorSkin"
                };
                uploadResult = cloudinary.Upload(uploadParams);
            }

            if (check == null)
            {
                byte[] bytes = Encoding.Unicode.GetBytes(users.password);
                byte[] inArray = HashAlgorithm.Create("SHA256").ComputeHash(bytes);
                string hashedPassword = Convert.ToBase64String(inArray);

                //users.password = config.GetMD5(users.password);
                users.password = hashedPassword;
                db.Configuration.ValidateOnSaveEnabled = false;

                //// Lưu URL của hình ảnh vào cơ sở dữ liệu của bạn hoặc sử dụng nó để hiển thị hình ảnh trong trang web của bạn
                //string imageUrl = uploadResult.Uri.ToString();

                users.ava = uploadResult?.SecureUri.ToString();
                users.iduser = randomID();
                db.Users.Add(users);
                db.SaveChanges();

                return Json(new { code = 0, message = "Đăng ký thành công" });
            }
            
            else
                return Json(new { code = 1 ,message="Đăng ký không thành công, số điện thoại hoặc Email đã được đăng ký" });
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA256").ComputeHash(bytes);

            for (int i = 0; i < inArray.Length; i++)
            {
                if (inArray[i] != hashBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

        [HttpPost]
        public JsonResult Login(string email, string phone, string password)
        {
            var data = db.Users.Where(s => s.email.Equals(email) || s.phone.Equals(phone)).FirstOrDefault();
            if (data!=null && VerifyHashedPassword(data.password,password)==true)
            {
                Session["iduser"] = data.iduser;
                Session.Timeout = 14400; //trong 4 ngày

                HttpCookie authCookie = new HttpCookie("AuthCookie");
                authCookie.Values["iduser"] = data.iduser;
                authCookie.Expires = DateTime.Now.AddDays(10);
                Response.Cookies.Add(authCookie);

                FormsAuthentication.SetAuthCookie(data.iduser, false);
                return Json(new { code = 0, message = "Đăng nhập thành công" });
            }
            else
            {
                return Json(new { code = 1, message = "Đăng nhập không thành công. Vui lòng kiểm tra lại" });
            }
        }

        public ActionResult Logout()
        {
            HttpCookie authCookie = Request.Cookies["AuthCookie"];
            if (authCookie != null)
            {
                authCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(authCookie);
            }

            Session.Abandon();

            return RedirectToAction("Index");
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

        public ActionResult Profile()
        {
            if (Session["iduser"] == null)
            {
                Response.Redirect("/dang-nhap");
            }
           
            string iduser = (string)Session["iduser"];
            if (iduser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(iduser);
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
