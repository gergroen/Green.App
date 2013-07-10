using System.Data.SqlServerCe;
using System.IO;

namespace Green.App.Dao.SqlCe
{
    public static class SqlCeEngineHelper
    {
        public static void CreateDatabaseIfNotExists(string databaseFilename)
        {
            var connectionString = CreateConnectionString(databaseFilename);
            if (!File.Exists(databaseFilename))
            {
                var sqlCeEngine = new SqlCeEngine(connectionString);
                sqlCeEngine.CreateDatabase();
                sqlCeEngine.Dispose();
            }
        }

        public static string CreateConnectionString(string databaseFilename)
        {
            var connectionStringBuilder = new SqlCeConnectionStringBuilder();
            connectionStringBuilder.DataSource = databaseFilename;
            return connectionStringBuilder.ToString();
        }

        public static void DeleteDatabaseIfExists(string databaseFileName)
        {
            if (File.Exists(databaseFileName))
            {
                File.Delete(databaseFileName);
            }
        }
    }
}