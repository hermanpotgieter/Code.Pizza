using System.Net;
using System.Web;
using System.Web.Mvc;
using Code.Pizza.Web.Extensions;

namespace Code.Pizza.Web.Filters
{
    public class AjaxAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorizeCore = base.AuthorizeCore(httpContext);
            return authorizeCore;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else
            {
                bool isAuthenticated = filterContext.HttpContext.User.Identity.IsAuthenticated;
                bool isNewSession = filterContext.HttpContext.Session != null && filterContext.HttpContext.Session.IsNewSession;

                if (isAuthenticated && isNewSession)
                {
                    filterContext.Controller.TempData.SessionIsExpiredOrInvalid(true);
                }

                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}
