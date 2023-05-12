using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Facebook;
using System.Configuration;
using DoctorSkin.Models;
using System.Data.Entity.Validation;
using System.Web.Security;
using DoctorSkin.Models.Role;

namespace DoctorSkin.Areas.Admin.Controllers
{
    public class FacebookController : Controller
    {
        private readonly DoctorSkinEntities db = new DoctorSkinEntities();
        //private readonly string appId = "1251345148845110";
        //private readonly string appSecret = "f3fa28b401be3393088481849d2527c1";
        private readonly string appId = ConfigurationManager.AppSettings["facebook:AppId"];
        private readonly string appSecret = ConfigurationManager.AppSettings["facebook:AppSecret"];

        DateTime current = DateTime.Now;

        public ActionResult Login()
        {
            var redirectUrl = Url.Action("Callback", "Facebook", null, Request.Url.Scheme);
            var loginUrl = GetLoginUrl(redirectUrl);

            return Redirect(loginUrl);
        }

        public ActionResult Callback(string code)
        {
            var redirectUrl = Url.Action("Callback", "Facebook", null, Request.Url.Scheme);
            var accessToken = GetAccessToken(code, redirectUrl);
            var userData = GetUserData(accessToken);

            // Lưu thông tin người dùng vào cơ sở dữ liệu
            var user = new Users
            {
                name = userData.name,
                email = userData.email,
                iduser = userData.id,
                ava = GetAvatarUrl(userData.id),
                gender = "Null",
                birth = current,
                phone = "LGfacebook",
                password = null,
                ConfirmPassword = null,
                dateregist = DateTime.Parse(current.ToString("yyyy-MM-dd HH:mm:ss"))

        };

            try
            {
                var checkUser = db.Users.FirstOrDefault(s => s.iduser == user.iduser || s.email == user.email);
                if(checkUser == null)
                {
                    db.Users.Add(user);

                    //Thêm vào bảng UserRoles
                    var userRole = new UserRoles();
                    userRole.email = user.email;
                    userRole.rolename = "User";
                    db.Roles.Add(userRole);

                    //Thêm vào bảng Mapping
                    var roleMapping = new UserRolesMappings();
                    roleMapping.email = user.email;
                    roleMapping.idrole = 2;
                    db.UserRolesMappings.Add(roleMapping);

                    db.SaveChanges();
                }

                Session["iduser"] = user.iduser;
                Session.Timeout = 14400; //trong 4 ngày

                HttpCookie authCookie = new HttpCookie("AuthCookie");
                authCookie.Values["iduser"] = user.iduser;
                authCookie.Expires = DateTime.Now.AddDays(10);
                Response.Cookies.Add(authCookie);

                var roleUser = db.Roles.FirstOrDefault(s => s.email == user.email).rolename;
                FormsAuthentication.SetAuthCookie(roleUser, false);
                //FormsAuthentication.SetAuthCookie(data.iduser, false);
                return RedirectToAction("Index", "Home");
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(e => e.ValidationErrors)
                    .Select(e => e.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);

                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                return Json(new { code = 0, error = exceptionMessage }, JsonRequestBehavior.AllowGet);
            }

        }

        private string GetAvatarUrl(string userId)
        {
            return $"https://graph.facebook.com/{userId}/picture?type=large";
        }

        private string GetLoginUrl(string redirectUrl)
        {
            var fb = new FacebookClient();

            dynamic parameters = new
            {
                client_id = appId,
                redirect_uri = redirectUrl,
                response_type = "code",
                scope = "email" // Phạm vi quyền truy cập (email, public_profile, ...)
            };

            var loginUrl = fb.GetLoginUrl(parameters);

            return loginUrl.AbsoluteUri;
        }

        private string GetAccessToken(string code, string redirectUrl)
        {
            var fb = new FacebookClient();

            dynamic parameters = new
            {
                client_id = appId,
                client_secret = appSecret,
                redirect_uri = redirectUrl,
                code = code
            };

            dynamic result = fb.Post("oauth/access_token", parameters);

            return result.access_token;
        }

        private dynamic GetUserData(string accessToken)
        {
            var fb = new FacebookClient(accessToken);
            dynamic userData = fb.Get("me?fields=id,name,email"); // Lấy thông tin người dùng

            return userData;
        }
    }

}