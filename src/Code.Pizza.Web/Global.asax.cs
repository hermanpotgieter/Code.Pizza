using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Code.Pizza.Web.Windsor;

namespace Code.Pizza.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private IWindsorContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this.container = new WindsorContainer();
            this.container.Register(Component.For<IWindsorContainer>().Instance(this.container));

            this.container.Install(new WindsorInstaller());

            IControllerFactory controllerFactory = this.container.Resolve<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            //AutoMapperFactory.CreateMaps();
            //Mapper.AssertConfigurationIsValid();
        }

        protected void Session_End()
        {
            this.Context.Cache.Remove(this.Context.User.Identity.Name);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if(this.container != null)
            {
                this.container.Dispose();
            }
        }
    }
}
