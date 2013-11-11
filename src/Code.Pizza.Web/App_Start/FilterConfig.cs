using System.Web.Mvc;
using Code.Pizza.Web.Filters;

namespace Code.Pizza.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AjaxAuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
