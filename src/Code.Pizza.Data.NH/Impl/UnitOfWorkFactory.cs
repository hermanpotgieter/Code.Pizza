using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.Pizza.Data.NH.Interfaces;
using Code.Pizza.Data.NH.Mappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Code.Pizza.Data.NH.Impl
{
    public class UnitOfWorkFactory
    {
        private readonly ISessionFactory sessionFactory;

        public UnitOfWorkFactory()
        {
            Type mappingType = typeof(UserMapping);

            IEnumerable<Type> mappingTypes =
                Assembly.GetAssembly(mappingType).GetTypes()
                    .Where(type => type.Namespace != null && type.Namespace.Equals(mappingType.Namespace))
                    .Where(type => type.BaseType != null &&
                                   (type.BaseType.IsGenericType &&
                                    type.BaseType.GetGenericTypeDefinition() == typeof(ClassMapping<>)))
                    .ToList();

            ModelMapper mapper = new ModelMapper();
            mapper.AddMappings(mappingTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            Configuration configuration = new Configuration();
            configuration.Configure(); // read config default style
            configuration.AddMapping(domainMapping);

            this.sessionFactory = configuration.BuildSessionFactory();
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            ISession session = this.sessionFactory.OpenSession();
            UnitOfWork unitOfWork = new UnitOfWork(session);

            return unitOfWork;
        }
    }
}
