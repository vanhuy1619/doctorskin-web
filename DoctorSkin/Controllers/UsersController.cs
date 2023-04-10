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
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string hash = BCrypt.Net.BCrypt.HashPassword(users.password, salt);

                db.Configuration.ValidateOnSaveEnabled = false;


                users.password = hash;
                users.ava = uploadResult?.SecureUri.ToString();
                users.iduser = randomID();
                users.point = 0;
                db.Users.Add(users);
                db.SaveChanges();

                return Json(new { code = 0, message = "Đăng ký thành công" });
            }

            else
                return Json(new { code = 1, message = "Đăng ký không thành công, số điện thoại hoặc Email đã được đăng ký" });
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

            if (data != null && BCrypt.Net.BCrypt.Verify(password, data.password) == true)
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

        [HttpPut]
        public JsonResult Edit(string iduser, string name, string phone, string birth, string gender)
        {
            var checkPhone = db.Users.Where(u => u.iduser != iduser && u.phone==phone).ToList();

            Users user = db.Users.Where(s=>s.iduser==iduser).FirstOrDefault();
            if (user != null)
            {
                if (checkPhone.Count()==0)
                {
                    user.name = name;
                    user.birth = Convert.ToDateTime(birth);
                    user.gender = gender;
                    user.phone = phone;

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    return Json(new { code = 0, message = "Cập nhật thông tin thành công", data = user });
                }
                return Json(new { code = 1, message = "Số điện thoại đã được đăng ký", data=user});
            }
            else if (user == null)
            {
                return Json(new { code = 1, message="Cập nhật thông tin thất bại"});
            }
            else  
                return Json(new { code = 1, message = "Cập nhật thông tin thất bại" });
        }

        public ActionResult Purchase()
        {
            return View();
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
