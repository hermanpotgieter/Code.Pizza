using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Code.Pizza.Core.Domain;
using Code.Pizza.Core.Services.Interfaces;
using Code.Pizza.Web.Extensions;
using Code.Pizza.Web.Models;

namespace Code.Pizza.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private const string rememberMeCookie = "RememberMe";

        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            if(this.TempData.SessionIsExpiredOrInvalid())
            {
                this.ViewBag.InvalidSessionMessage = "Your session has expired. Please log in again.";
            }

            LoginModel model = new LoginModel();

            HttpCookie cookie = this.Request.Cookies[rememberMeCookie];

            if(cookie != null)
            {
                model.UserName = cookie.Value.ToString(CultureInfo.InvariantCulture);
                model.RememberMe = true;
            }

            if(this.Request.IsAjaxRequest())
            {
                return this.PartialView("_Login");
            }

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if(!this.ModelState.IsValid)
            {
                return View(model);
            }

            User user = this.userService.Logon(model.UserName, model.Password);

            if(user == null)
            {
                this.ModelState.AddModelError("InvalidLogin", "Incorrect username or password.");

                return View(model);
            }

            this.AddAuthenticationCookie(user);

            if(model.RememberMe)
            {
                this.AddRememberMeCookie(user);
            }
            else
            {
                this.RemoveRememberMeCookie();
            }

            if(string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.Redirect(returnUrl);
        }

        public ActionResult Logout()
        {
            // Get the authentication cookie and expire it
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = this.Request.Cookies[cookieName];

            if(authCookie != null)
            {
                authCookie.Expires = DateTime.Now.AddDays(-1);
                ;
                this.Response.Cookies.Add(authCookie);
            }

            return this.View("Logout");
        }

        private void AddAuthenticationCookie(User user)
        {
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now,
                DateTime.Now.AddMinutes(ConfigSettings.FormsTimeOut.Minutes), false, string.Empty);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { Expires = authTicket.Expiration };

            this.Response.Cookies.Add(faCookie);
        }

        private void AddRememberMeCookie(User user)
        {
            int cookieDays = ConfigSettings.RememberMeCookieDays;
            HttpCookie cookie = new HttpCookie(rememberMeCookie) { Value = user.Email, Expires = DateTime.Now.AddDays(cookieDays) };

            this.Response.Cookies.Add(cookie);
        }

        private void RemoveRememberMeCookie()
        {
            HttpCookie httpCookie = this.Response.Cookies[rememberMeCookie];

            if(httpCookie != null)
            {
                httpCookie.Expires = DateTime.Now.AddDays(-1);
                ;
            }

            this.Response.Cookies.Remove(rememberMeCookie);
        }
    }
}
