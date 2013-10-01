using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Code.Pizza.Core.Data;
using Code.Pizza.Core.Services.Impl;
using Code.Pizza.Data.NH.Impl;
using Code.Pizza.Data.NH.Interfaces;
using Code.Pizza.Web.Factories;

namespace Code.Pizza.Web.Windsor
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            UnitOfWorkFactory unitOfWorkFactory = new UnitOfWorkFactory();

            container
                .Register(Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactory>().LifestyleSingleton())
                .Register(Classes.FromAssemblyContaining<WindsorControllerFactory>().BasedOn<IController>().LifestylePerWebRequest())

                .Register(Component.For<IUnitOfWork>()
                                   .UsingFactoryMethod(unitOfWorkFactory.CreateUnitOfWork)
                                   .LifestylePerWebRequest())

                .Register(Types.FromAssemblyContaining<UserService>()
                               .Where(type => type.Name.EndsWith("Service"))
                               .WithServiceAllInterfaces()
                               .LifestylePerWebRequest())

                .Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(Repository<>)).LifestylePerWebRequest())
                ;
        }
    }
}
