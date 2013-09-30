using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.Pizza.Data.NH.Mappings;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Code.Pizza.Data.NH.Tests
{
    [TestFixture]
    public class TestBase
    {
        private Configuration configuration;
        protected ISessionFactory sessionFactory;

        [SetUp]
        public void SetUp()
        {
            this.DropAndRecreateSchema();
        }

        [Test]
        public void CreateSchema()
        {
            // Nothing to do here, it is just a convenience method to create the db schema.
            // Setup and Teardown will drop and recreate the db schema.
        }

        private void DropAndRecreateSchema()
        {
            SchemaExport schemaExport = new SchemaExport(this.configuration);

            schemaExport.SetOutputFile(@"..\..\nhibernate-schema.txt");

            schemaExport.Drop(script: false, export: true);
            schemaExport.Create(script: false, export: true);
        }

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            this.configuration = GetConfiguration();
            this.sessionFactory = this.configuration.BuildSessionFactory();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            this.DropAndRecreateSchema();
        }

        private static Configuration GetConfiguration()
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

            return configuration;
        }
    }
}
