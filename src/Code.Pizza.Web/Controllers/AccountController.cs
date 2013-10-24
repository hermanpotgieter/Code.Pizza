using System;
using System.Globalization;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
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

			if (Request.Cookies["username"] == null || Request.Cookies["username"].Value.ToString(CultureInfo.InvariantCulture).Trim() == "")
			{
				model.RememberMe = false;
			}
			else
			{
				model.UserName = Request.Cookies["username"].Value.ToString(CultureInfo.InvariantCulture).Trim();
				model.RememberMe = true;
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

			if (model.RememberMe)
			{
				int rememberMeCookieLiftime = Convert.ToInt32(WebConfigurationManager.AppSettings["RememberMeCookieLifetime"]);
				HttpCookie cookie = new HttpCookie("username") { Value = model.UserName.Trim(), Expires = DateTime.Now.AddDays(rememberMeCookieLiftime) };

				this.Response.Cookies.Add(cookie);
			}
			else
			{
				var httpCookie = this.Response.Cookies["username"];
				if(httpCookie != null)
				{
					httpCookie.Expires = DateTime.Now;
				}
				this.Response.Cookies.Remove("username");
			}

            User user = this.userService.Logon(model.UserName, model.Password);

            if(user == null)
            {
                this.ModelState.AddModelError("InvalidLogin", "Incorrect username or password.");

                return View(model);
            }

            long cookieExpires = 15;
            Configuration webConfiguration = WebConfigurationManager.OpenWebConfiguration("");
            ConfigurationSectionGroup configurationSectionGroup = webConfiguration.SectionGroups.Get("system.web");

            if(configurationSectionGroup != null)
            {
                AuthenticationSection authenticationSection = (AuthenticationSection)configurationSectionGroup.Sections.Get("authentication");
                cookieExpires = Convert.ToInt64(authenticationSection.Forms.Timeout.TotalMinutes);
            }

            FormsAuthenticationTicket authTicket =
                new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(cookieExpires), false, string.Empty);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { Expires = authTicket.Expiration };
            this.Response.Cookies.Add(faCookie);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.Redirect(returnUrl);
        }

        public ActionResult Logout()
        {
			// Get the authentication cookie and expire it
			string cookieName = FormsAuthentication.FormsCookieName;
			HttpCookie authCookie = Request.Cookies[cookieName];

			DateTime expired = DateTime.Now.AddDays(-1);

			if (authCookie != null)
			{
				authCookie.Expires = expired;
				Response.Cookies.Add(authCookie);
			}

			return View("Logout");
        }
    }
}
