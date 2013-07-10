using Green.App.Dao.SqlCe;
using NUnit.Framework;

namespace Green.App.Test.Framework.Dao
{
    [TestFixture]
    public abstract class TestDatabaseFileBase
    {
        [SetUp]
        public virtual void SetUp()
        {
            SqlCeEngineHelper.CreateDatabaseIfNotExists(DatabaseName);
        }

        [TearDown]
        public virtual void TearDown()
        {
            SqlCeEngineHelper.DeleteDatabaseIfExists(DatabaseName);
        }

        protected virtual string DatabaseName
        {
            get { return "test.db"; }
        }

        protected virtual string ConnectionString {
            get { return SqlCeEngineHelper.CreateConnectionString(DatabaseName); }
        }
    }
}
