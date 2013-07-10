using System;
using System.IO;
using Green.App.Dao.FluentMigrator;
using Green.App.Dao.NHibernate;
using Green.App.Dao.SqlCe;
using Green.App.Service;
using Green.App.Service.WebApi;
using Green.App.Service.WebServer;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Topshelf;
using log4net;
using log4net.Config;

namespace Green.App
{
    class Program
    {
        private static void Main(string[] args)
        {
            var databaseFileName = "db.sdf";
            var webApiUri = "http://localhost/api/";
            var webServerUri = "http://localhost/www/";
            var webServerFileDir = @"C:\Data\Development\Source\Microsoft.Net\Green.App\src\Green.App\";
            var log4netConfigFile = @"Config\log4net.config";
            var connectionString = SqlCeEngineHelper.CreateConnectionString(databaseFileName);

            XmlConfigurator.ConfigureAndWatch(new FileInfo(log4netConfigFile));

            SqlCeEngineHelper.DeleteDatabaseIfExists(databaseFileName);
            SqlCeEngineHelper.CreateDatabaseIfNotExists(databaseFileName);

            FluentMigratorRunner.MigrateToLatest(connectionString, "Green.App.Dao.FluentMigrator");

            var configuration = new Configuration();
            configuration.CurrentSessionContext<ThreadStaticSessionContext>();
            configuration.SessionFactory()
                .Integrate.Using<MsSqlCe40Dialect>()
                .Connected.By<SqlServerCeDriver>()
                .Using(connectionString);

            var mapper = new ModelMapper();
            mapper.AddMapping<SchemaVersionMap>();
            configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            try
            {
                var validator = new SchemaValidator(configuration);
                validator.Validate();
            }
            catch (Exception)
            {
                var schema = new SchemaUpdate(configuration);
                schema.Execute(LogManager.GetLogger(typeof(SchemaUpdate)).Info, false);
                throw;
            }

            var sessionFactory = configuration.BuildSessionFactory();

            var repo = new NHibernateRepository(sessionFactory);

            var webApiService = new SelfHostWebApiService(new Uri(webApiUri));
            var webServer = new SelfHostWebServer(new Uri(webServerUri), new DirectoryInfo(webServerFileDir));

            HostFactory.Run(x =>
                {
                    x.Service<WindowsServiceHost>(s =>
                        {
                            s.ConstructUsing(name => new WindowsServiceHost(webApiService, webServer));
                            s.WhenStarted(tc => tc.Start());
                            s.WhenStopped(tc => tc.Stop());
                        });
                    x.RunAsLocalSystem();
                    x.SetServiceName("GreenAppService");
                    x.SetDisplayName("Green App Service");
                    x.SetDescription("This service host the webapi and the webservice");
                    x.UseLog4Net();
                });
        }
    }
}
