using System;
using Green.App.Dao.NHibernate;
using Green.App.Test.Framework.Dao;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using log4net;

namespace Green.App.Test.Dao.NHibernate
{
    [TestFixture]
    public class TestMapping : TestDatabaseSchemaBase
    {
        private Configuration _configuration;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _configuration = new Configuration();
            _configuration.CurrentSessionContext<ThreadStaticSessionContext>();
            _configuration.SessionFactory()
                .Integrate.Using<MsSqlCe40Dialect>()
                .Connected.By<SqlServerCeDriver>()
                .Using(ConnectionString);

            var mapper = new ModelMapper();
            mapper.AddMapping<SchemaVersionMap>();
            _configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
        }

        [Test]
        public void ValidateSchemaTest()
        {
            try
            {
                var validator = new SchemaValidator(_configuration);
                validator.Validate();
            }
            catch (Exception)
            {
                var schema = new SchemaUpdate(_configuration);
                schema.Execute(LogManager.GetLogger(typeof(SchemaUpdate)).Info, false);
                throw;
            }
        }
    }
}
