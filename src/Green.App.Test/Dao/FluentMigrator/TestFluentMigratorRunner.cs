using Green.App.Dao.FluentMigrator;
using Green.App.Test.Framework.Dao;
using NUnit.Framework;

namespace Green.App.Test.Dao.FluentMigrator
{
    [TestFixture]
    public class TestFluentMigratorRunner : TestDatabaseFileBase
    {
        [Test]
        public void MigrateToLatestTest()
        {
            FluentMigratorRunner.MigrateToLatest(ConnectionString, "Green.App.Dao.FluentMigrator");


        }
    }
}
