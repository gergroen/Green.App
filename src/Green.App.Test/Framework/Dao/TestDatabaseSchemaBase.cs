using Green.App.Dao.FluentMigrator;
using NUnit.Framework;

namespace Green.App.Test.Framework.Dao
{
    [TestFixture]
    public abstract class TestDatabaseSchemaBase : TestDatabaseFileBase
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            FluentMigratorRunner.MigrateToLatest(ConnectionString, FluentMigratorNamespace);
        }

        protected virtual string FluentMigratorNamespace
        {
            get { return "Green.App.Dao.FluentMigrator"; }
        }
    }
}