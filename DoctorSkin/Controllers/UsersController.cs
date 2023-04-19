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
using System.Web.Helpers;
using System.Net.Mail;

namespace DoctorSkin.Controllers
{
    [Route("/user")]
    public class UsersController : Controller
    {
        private DoctorSkinEntities db = new DoctorSkinEntities();
        private Config config = new Config();

        bool checkEmail(string email)
        {
            var user = db.Users.FirstOrDefault(s=>s.email==email);
            if (user != null)
                return true;
            return
                false;
        }

        public string createAt()
        {
            DateTime now = DateTime.UtcNow;
            long secondsSinceEpoch = (long)(now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

            string finalString = secondsSinceEpoch.ToString();
            return finalString;
        }

        public string getToken()
        {
            var chars = "0123456789012365412587489632145698741230";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }


        [HttpGet]
        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Forgot(string email)
        {
            try
            {
                if(checkEmail(email)==true)
                {
                    string token = getToken();

                    string subject = "Chúng tôi đã khôi phục thành công Tài khoản DoctorSkin của bạn.";
                    string url = "https://localhost:44307/users/reset?email=" + email;
                    string body = "<p>Mã xác thực tồn tại trong 3 phút là: <b>" + token + "</b><br></p>" +
                                  "<p>Truy cập vào đường link này để cập nhật lại mật khẩu <a href='" + url + "'>Tại đây</a></p>";

                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("daylataikhoanguiemail@gmail.com");
                    message.To.Add(new MailAddress(email));
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("daylataikhoanguiemail@gmail.com", "upfmzrqgdnlbatzi");

                    Forgots forgot = new Forgots();
                    forgot.email = email;
                    forgot.token = token;
                    forgot.createAt = createAt();

                    db.Forgots.Add(forgot);
                    db.SaveChanges();
                    smtpClient.Send(message);

                    return Json(new { code = 0, message = "Gửi email thành công" });
                }
                else
                {
                    return Json(new { code = 1, message = "Gửi email thất bại" });
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, message = "Gửi email thất bại" });
            }
        }

        [HttpGet]
        public ActionResult Reset(string email)
        {
            int now = int.Parse(createAt());
            var item = db.Forgots.Where(s => s.email == email)
                                 .OrderByDescending(s => s.stt)
                                 .FirstOrDefault();

            if (item != null && now - int.Parse(item.createAt) <= 180)
            {
                return View();
            }
            return Redirect("/");

        }


        [HttpPut]
        public ActionResult ResetPass(string email, string token, string password)
        {
            string message = "";
            int code = 0;
            int now = int.Parse(createAt());
            var item = db.Forgots.FirstOrDefault(s => s.email == email && s.token == token);
            if(item!=null)
            {
                if (now - int.Parse(item.createAt) < 180)
                {
                    Users u = db.Users.FirstOrDefault(s => s.email == email);

                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

                    u.password = hash;

                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Forgots.Remove(item);
                    db.SaveChanges();

                    code = 0;
                    message = "Cập nhật mật khẩu thành công. Vui lòng quay lại trang đăng nhập";
                }
                else
                {
                    code = 1;
                    message = "Mã xác thực đã hết hạn, Vui lòng lấy lại mã xác thực mới.";
                }
            }    
            else
            {
                code = 1;
                message = "Mã xác thực không hợp lệ";
            }
            return Json(new { code = code, message = message });
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

            return Redirect("/");
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
            ViewBag.Title = "Thông tin cá nhân";
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

        public ActionResult Purchase(string type)
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
            ViewBag.Title = "Đơn hàng của bạn";

            var bills = db.Bills.Where(s => s.iduser == iduser).ToList();
            switch (type)
            {
                case "choxacnhan":
                    bills = bills.Where(s => s.status == "Chờ xác nhận").ToList();
                    break;
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
