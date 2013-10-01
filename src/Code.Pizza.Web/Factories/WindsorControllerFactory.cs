using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace Code.Pizza.Web.Factories
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            IController controllerInstance = controllerType == null ? null : this.container.Resolve<IController>(controllerType.FullName);

            return controllerInstance;
        }

        public override void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }
    }
}
