using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Code.Pizza.Core.Domain;
using Code.Pizza.Core.Services.Interfaces;
using Code.Pizza.Web.Models;

namespace Code.Pizza.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
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

            FormsAuthenticationTicket authTicket =
                new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, string.Empty);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { Expires = authTicket.Expiration };
            this.Response.Cookies.Add(faCookie);

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            throw new NotImplementedException();
        }
    }
}
