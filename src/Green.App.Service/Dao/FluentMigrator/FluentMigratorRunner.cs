using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using log4net;

namespace Green.App.Dao.FluentMigrator
{
    public static class FluentMigratorRunner
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(FluentMigratorRunner));

        private class GreenMigrationRunner : MigrationRunner
        {
            public GreenMigrationRunner(Assembly assembly, IRunnerContext runnerContext, IMigrationProcessor processor) : base(assembly, runnerContext, processor)
            {
            }
        }

        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
            public string ProviderSwitches { get; private set; }
        }

        public static void MigrateToLatest(string connectionString, string nameSpace)
        {
            // var announcer = new NullAnnouncer();
            var announcer = new TextWriterAnnouncer(s => _logger.Info(s));
            var assembly = Assembly.GetExecutingAssembly();

            var migrationContext = new RunnerContext(announcer)
                {
                    Namespace = nameSpace
                };

            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new global::FluentMigrator.Runner.Processors.SqlServer.SqlServerCeProcessorFactory();
            var processor = factory.Create(connectionString, announcer, options);
            var runner = new GreenMigrationRunner(assembly, migrationContext, processor);
            runner.MigrateUp(true);
        }
    }
}